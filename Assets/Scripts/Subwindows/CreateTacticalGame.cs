// Modified for the VoreWar mercenary-company conversion.
// 2026-10-30: Restrict setup, previews and bulk generation to approved species; handle empty pools (spec 03A1).
// 2026-11-01: Construct only the retained standalone battle World context (spec 05A5).
// 2026-11-01: Keep skirmish selections bound to stable species IDs with singular display labels (09–10 feedback).
// 2026-11-02: Host the retained Single Battle fields/previews in the shared blue two-column presentation (spec 15D).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CreateTacticalGame : MonoBehaviour
{
    public StartTacticalUI Attacker;
    public StartTacticalUI Defender;

    public InputField SizeX;
    public InputField SizeY;
    public Toggle AutoScale;

    public Dropdown AIOverride;
    public Dropdown TerrainType;

    public Text TooltipText;

    public GameObject BulkPanel;
    public Slider Bulk;
    public Toggle AllRaces;

    public Actor_Unit AttackerUnit;
    public Actor_Unit DefenderUnit;

    public UIUnitSprite AttackerSprite;
    public UIUnitSprite DefenderSprite;

    bool batching;

    Race batchingAttackerRace;
    Race batchingDefenderRace;

    bool refreshingSpeciesChoices;
    SingleBattleSetupView presentation;

    public void Start()
    {
        RefreshSpeciesChoices();
        EnsurePresentation();
    }

    internal void Open()
    {
        RefreshSpeciesChoices();
        EnsurePresentation();
    }

    void EnsurePresentation()
    {
        if (presentation == null)
        {
            presentation = new SingleBattleSetupView(this);
            UpdateAIBoxes();
            AutoScaleChanged();
            UpdateTextPercentages();
            UpdateBulkSlider();
        }
        presentation.Refresh();
    }

    void Update()
    {
        if (presentation != null) presentation.Refresh();
    }

    public void AttackerRaceChanged()
    {
        if (!refreshingSpeciesChoices)
            UpdateSpeciesPreview(Attacker.Race, ref AttackerUnit, AttackerSprite);
    }

    public void DefenderRaceChanged()
    {
        if (!refreshingSpeciesChoices)
            UpdateSpeciesPreview(Defender.Race, ref DefenderUnit, DefenderSprite);
    }

    void RefreshSpeciesChoices()
    {
        refreshingSpeciesChoices = true;
        Race[] available = SpeciesPolicy.GetAvailable(SpeciesUse.Skirmish);
        foreach (Dropdown dropdown in new[] { Attacker.Race, Defender.Race })
        {
            string previous = dropdown.options.Count > dropdown.value ? dropdown.options[dropdown.value].text : "Random";
            Race previousRace;
            if (SpeciesPolicy.TryParseName(previous, out previousRace)) previous = SpeciesPolicy.DisplayName(previousRace);
            dropdown.ClearOptions();
            var labels = available.Length == 0 ? new List<string> { "No approved species" } :
                new List<string> { "Random" };
            labels.AddRange(available.Select(SpeciesPolicy.DisplayName));
            dropdown.AddOptions(labels);
            dropdown.value = Math.Max(0, labels.IndexOf(previous));
            dropdown.interactable = available.Length > 0;
            dropdown.RefreshShownValue();
        }
        Text allSpeciesLabel = AllRaces.GetComponentInChildren<Text>(true);
        if (allSpeciesLabel != null)
            allSpeciesLabel.text = "Include animals";
        refreshingSpeciesChoices = false;
        AttackerRaceChanged();
        DefenderRaceChanged();
        TooltipText.text = available.Length == 0 ? SpeciesPolicy.UnavailableMessage : "";
    }

    bool IsSelectionAvailable(Dropdown dropdown)
    {
        if (dropdown.value < 0 || dropdown.value >= dropdown.options.Count)
            return false;
        string choice = dropdown.options[dropdown.value].text;
        Race race;
        return choice == "Random" ? SpeciesPolicy.GetAvailable(SpeciesUse.Skirmish).Length > 0 :
            SpeciesPolicy.TryParseName(choice, out race) && SpeciesPolicy.IsAllowed(race, SpeciesUse.Skirmish);
    }

    bool TrySelectedRace(Dropdown dropdown, out Race race)
    {
        race = Race.none;
        if (!IsSelectionAvailable(dropdown))
            return false;
        string choice = dropdown.options[dropdown.value].text;
        return choice == "Random" ? SpeciesPolicy.TryRandom(SpeciesUse.Skirmish, out race) : SpeciesPolicy.TryParseName(choice, out race);
    }

    void UpdateSpeciesPreview(Dropdown dropdown, ref Actor_Unit actor, UIUnitSprite sprite)
    {
        Race race;
        if (!TrySelectedRace(dropdown, out race))
        {
            actor = null;
            sprite.gameObject.SetActive(false);
            return;
        }
        sprite.gameObject.SetActive(true);
        if (actor == null || actor.Unit.Race != race)
            actor = new Actor_Unit(new Unit(race));
        actor.Unit.TotalRandomizeAppearance();
        sprite.UpdateSprites(actor);
        sprite.Name.text = SpeciesPolicy.DisplayName(race);
    }

    bool CanCreateBattle()
    {
        bool allowed = SpeciesPolicy.GetAvailable(SpeciesUse.Skirmish).Length > 0;
        if (batching)
            allowed &= SpeciesPolicy.IsAllowed(batchingAttackerRace, SpeciesUse.Skirmish) &&
                SpeciesPolicy.IsAllowed(batchingDefenderRace, SpeciesUse.Skirmish);
        else
            allowed &= IsSelectionAvailable(Attacker.Race) && IsSelectionAvailable(Defender.Race);
        if (!allowed)
            State.GameManager.CreateMessageBox(SpeciesPolicy.UnavailableMessage);
        return allowed;
    }

    public void UpdateAIBoxes()
    {
        Attacker.TacticalAI.interactable = Attacker.AIPlayer.isOn;
        Defender.TacticalAI.interactable = Defender.AIPlayer.isOn;
        AIOverride.interactable = Attacker.AIPlayer.isOn && Defender.AIPlayer.isOn;
        WatchOptionChanged();
    }

    public void UpdateTextPercentages()
    {
        Attacker.RangedText.text = $"{Attacker.RangedPercentage.value}%";
        Defender.RangedText.text = $"{Defender.RangedPercentage.value}%";
        Attacker.WeaponsText.text = $"{Attacker.HeavyWeaponsPercentage.value}%";
        Defender.WeaponsText.text = $"{Defender.HeavyWeaponsPercentage.value}%";
        Attacker.MagicText.text = $"{Attacker.MagicPercentage.value}%";
        Defender.MagicText.text = $"{Defender.MagicPercentage.value}%";
    }

    public void AutoScaleChanged()
    {
        SizeX.interactable = !AutoScale.isOn;
        SizeY.interactable = !AutoScale.isOn;
    }

    string TestTacticalSize()
    {
        int x = Convert.ToInt32(SizeX.text);
        int y = Convert.ToInt32(SizeY.text);

        int MaxUnitsOnHalf = Math.Max(Convert.ToInt32(Attacker.UnitCount.text), Convert.ToInt32(Defender.UnitCount.text));



        if (x < 8 || y < 8)
            return "Can't have a tactical dimension less than 8";

        if (MaxUnitsOnHalf > x * y / 14)
            return "Not enough space to comfortably fit specified number of units on at least one of the halves";

        return "";

    }

    string TestMisc()
    {
        if (Convert.ToInt32(Attacker.UnitCount.text) < 1 || Convert.ToInt32(Defender.UnitCount.text) < 1)
            return "Both sides need to have units";
        if (Convert.ToInt32(Attacker.Level.text) < 1 || Convert.ToInt32(Defender.Level.text) < 1)
            return "You can't have levels less than 1";
        return "";
    }

    public void UpdateBulkSlider()
    {
        Bulk.GetComponentInChildren<Text>(true).text = $"Runs per matchup: {Bulk.value}";
    }

    public void WatchOptionChanged()
    {
        BulkPanel.gameObject.SetActive(AIOverride.value == 0 && AIOverride.interactable);
    }

    public void SuperBulk()
    {
        if (!CanCreateBattle())
            return;
        StartCoroutine(CompleteBatch());
    }

    public void SuperBulkSingleRace()
    {
        if (!CanCreateBattle())
            return;
        StartCoroutine(SingleRaceBatch());
    }

    internal IEnumerator SingleRaceBatch()
    {
        if (!CanCreateBattle())
            yield break;
        float time = Time.realtimeSinceStartup;
        List<Race> participants = new List<Race>();
        batching = true;
        participants.AddRange(SpeciesPolicy.GetAvailable(SpeciesUse.Skirmish)
            .Where(r => AllRaces.isOn || SpeciesPolicy.GetKind(r) == SpeciesKind.Sapient));


        if (!TrySelectedRace(Attacker.Race, out batchingAttackerRace))
        {
            batching = false;
            yield break;
        }


        int[] grid = new int[participants.Count];
        int remaining = 5;

        for (int j = 0; j < participants.Count; j++)
        {
            if (participants[j] == batchingAttackerRace)
                continue;
            State.GameManager.TacticalMode.Wins = new Vector2Int();
            for (int k = 0; k < Bulk.value; k++)
            {
                batchingDefenderRace = participants[j];
                CreateTactical();
                State.GameManager.TacticalMode.CleanUp();
                remaining--;
                if (remaining == 0)
                {
                    remaining = 5;
                    yield return null;
                }
            }
            grid[j] = State.GameManager.TacticalMode.Wins.x;
        }
        StringBuilder sb = new StringBuilder();
        int total = 0;
        for (int y = 0; y < participants.Count; y++)
        {
            if (participants[y] == batchingAttackerRace)
            {
                sb.Append($"-\t");
                continue;
            }
            total += grid[y];
            sb.Append($"{grid[y]}\t");
        }
        sb.AppendLine();
        sb.AppendLine($"Total Wins: {total}");
        State.GameManager.CreateFullScreenMessageBox(sb.ToString());
        Debug.Log($"Batch completed in {Time.realtimeSinceStartup - time}");
        batching = false;
        WriteOutBatch(sb);
    }

    internal IEnumerator CompleteBatch()
    {
        if (!CanCreateBattle())
            yield break;
        float time = Time.realtimeSinceStartup;
        List<Race> participants = new List<Race>();

        participants.AddRange(SpeciesPolicy.GetAvailable(SpeciesUse.Skirmish)
            .Where(r => AllRaces.isOn || SpeciesPolicy.GetKind(r) == SpeciesKind.Sapient));

        if (!TrySelectedRace(Attacker.Race, out batchingAttackerRace))
        {
            batching = false;
            yield break;
        }
        if (participants.Count < 2)
        {
            State.GameManager.CreateMessageBox("At least two enabled species are needed for all-pairs battles.");
            yield break;
        }
        batching = true;
        int[,] grid = new int[participants.Count, participants.Count];
        int remaining = 5;
        for (int i = 0; i < participants.Count; i++)
        {
            for (int j = i + 1; j < participants.Count; j++)
            {
                State.GameManager.TacticalMode.Wins = new Vector2Int();
                for (int k = 0; k < Bulk.value; k++)
                {
                    batchingAttackerRace = participants[i];
                    batchingDefenderRace = participants[j];
                    CreateTactical();
                    State.GameManager.TacticalMode.CleanUp();
                    remaining--;
                    if (remaining == 0)
                    {
                        remaining = 5;
                        yield return null;
                    }
                }
                grid[i, j] = State.GameManager.TacticalMode.Wins.x;
                grid[j, i] = State.GameManager.TacticalMode.Wins.y;
            }
        }
        StringBuilder sb = new StringBuilder();
        for (int x = 0; x < participants.Count; x++)
        {
            int total = 0;
            sb.Append($"{participants[x]} \t");
            for (int y = 0; y < participants.Count; y++)
            {
                if (x == y)
                {
                    sb.Append($"-\t");
                }
                else
                {
                    total += grid[x, y];
                    sb.Append($"{grid[x, y]}\t");
                }
            }
            sb.AppendLine($"total Wins: {total}");
        }
        State.GameManager.CreateFullScreenMessageBox(sb.ToString());
        Debug.Log($"Batch completed in {Time.realtimeSinceStartup - time}");
        WriteOutBatch(sb);
        batching = false;
    }



    public void CreateTacticalBulk()
    {
        if (!CanCreateBattle())
            return;
        StartCoroutine(SingleBatch());
    }

    internal IEnumerator SingleBatch()
    {
        if (!CanCreateBattle())
            yield break;
        float time = Time.realtimeSinceStartup;
        batching = true;
        int remaining = 5;
        State.GameManager.TacticalMode.Wins = new Vector2Int();

        if (!TrySelectedRace(Attacker.Race, out batchingAttackerRace) ||
            !TrySelectedRace(Defender.Race, out batchingDefenderRace))
        {
            batching = false;
            yield break;
        }

        for (int i = 0; i < Bulk.value; i++)
        {
            CreateTactical();
            State.GameManager.TacticalMode.CleanUp();
            remaining--;
            if (remaining == 0)
            {
                remaining = 5;
                yield return null;
            }
        }
        State.GameManager.CreateMessageBox($"Attacker Wins:{State.GameManager.TacticalMode.Wins.x} Defender Wins:{State.GameManager.TacticalMode.Wins.y}");
        batching = false;
        Debug.Log($"Batch completed in {Time.realtimeSinceStartup - time}");
    }

    private static void WriteOutBatch(StringBuilder sb)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter($"UserData{Path.DirectorySeparatorChar}LastBatch.txt"))
            {
                sw.Write(sb.ToString());
            }
        }
        catch
        {
            Debug.Log("Write Failed");
        }
    }

    public void CreateTactical()
    {
        if (!CanCreateBattle())
            return;
        int fightersA;
        int fightersB;
        int levelA;
        int levelB;
        int maxMagicA;
        int maxMagicB;
        try
        {
            string errorText = "";
            if (AutoScale.isOn == false)
                errorText = TestTacticalSize();
            if (errorText != "")
            {
                State.GameManager.CreateMessageBox(errorText);
                return;
            }
            errorText = TestMisc();
            if (errorText != "")
            {
                State.GameManager.CreateMessageBox(errorText);
                return;
            }
            if (Attacker.AIPlayer.isOn && Defender.AIPlayer.isOn)
            {
                Config.WatchAIBattles = AIOverride.value == 1;
                Config.ShowStatsForSkippedBattles = AIOverride.value == 0;
            }

            fightersA = Convert.ToInt32(Attacker.UnitCount.text);
            fightersB = Convert.ToInt32(Defender.UnitCount.text);
            levelA = Convert.ToInt32(Attacker.Level.text);
            levelB = Convert.ToInt32(Defender.Level.text);
            maxMagicA = Convert.ToInt32(Attacker.MaxSpellLevel.text);
            maxMagicB = Convert.ToInt32(Defender.MaxSpellLevel.text);
            Config.World.TacticalSizeX = Convert.ToInt32(SizeX.text);
            Config.World.TacticalSizeY = Convert.ToInt32(SizeY.text);
        }
        catch
        {
            State.GameManager.CreateMessageBox("There is a blank textbox that needs to be filled in");
            return;
        }

        Config.World.SoftLevelCap = 999;

        Config.World.ItemSlots = Config.NewItemSlots;


        TacticalAIType attackerAI = Attacker.AIPlayer.isOn ? (TacticalAIType)(1 + Attacker.TacticalAI.value) : TacticalAIType.None;
        TacticalAIType defenderAI = Defender.AIPlayer.isOn ? (TacticalAIType)(1 + Defender.TacticalAI.value) : TacticalAIType.None;

        Config.World.AutoScaleTactical = AutoScale.isOn;

        Vec2i none = new Vec2i(0, 0);

        State.World = new World();
        int attackerRangedCount = fightersA * (int)Attacker.RangedPercentage.value / 100;
        int defenderRangedCount = fightersB * (int)Defender.RangedPercentage.value / 100;
        int attackerMeleeHeavyCount = (fightersA - attackerRangedCount) * (int)Attacker.HeavyWeaponsPercentage.value / 100;
        int defenderMeleeHeavyCount = (fightersB - defenderRangedCount) * (int)Defender.HeavyWeaponsPercentage.value / 100;
        int attackerRangedHeavyCount = attackerRangedCount * (int)Attacker.HeavyWeaponsPercentage.value / 100;
        int defenderRangedHeavyCount = defenderRangedCount * (int)Defender.HeavyWeaponsPercentage.value / 100;
        int attackerSide = 0;
        int defenderSide = 1;
        Func<Race> AttackerRace;
        Func<Race> DefenderRace;
        if (batching == false)
        {
            AttackerRace = SetupRace(Attacker.Race);
            DefenderRace = SetupRace(Defender.Race);

        }
        else
        {
            AttackerRace = () => batchingAttackerRace;
            DefenderRace = () => batchingDefenderRace;
        }


        Func<Race> SetupRace(Dropdown tacRace)
        {
            if (SpeciesPolicy.TryParseName(tacRace.options[tacRace.value].text, out Race race))
            {
                return () => race;
            }
            return GetRandomRace;
        }
        Army attacker = new Army(new Empire(), none, attackerSide);
        Army defender = new Army(new Empire(), none, defenderSide);
        attacker.Empire.ReplacedRace = AttackerRace();              
        defender.Empire.ReplacedRace = DefenderRace();
        if (Attacker.HasLeader.isOn)
        {
            attacker.Units.Add(new NPC_unit(levelA, Attacker.RangedPercentage.value >= 50, 3, attackerSide, AttackerRace(), 0, Attacker.CanVore.isOn));
            attacker.Units.Last().ImmuneToDefections = true;
            if (State.Rand.NextDouble() < Attacker.MagicPercentage.value / 100)
                attacker.Units.Last().SetItem(State.World.ItemRepository.GetRandomBook(1, maxMagicA, true), 1);
        }
        if (Defender.HasLeader.isOn)
        {
            defender.Units.Add(new NPC_unit(levelB, Defender.RangedPercentage.value >= 50, 3, defenderSide, DefenderRace(), 0, Defender.CanVore.isOn));
            defender.Units.Last().ImmuneToDefections = true;
            if (State.Rand.NextDouble() < Defender.MagicPercentage.value / 100)
                defender.Units.Last().SetItem(State.World.ItemRepository.GetRandomBook(1, maxMagicB, true), 1);
        }
        for (int x = 0; x < fightersA; x++)
        {
            if (x < attackerRangedCount)
            {
                attacker.Units.Add(new NPC_unit(levelA, attackerRangedHeavyCount > 0, 1, attackerSide, AttackerRace(), 0, Attacker.CanVore.isOn));
                attacker.Units.Last().ImmuneToDefections = true;
                attackerRangedHeavyCount--;
                if (State.Rand.NextDouble() < Attacker.MagicPercentage.value / 100)
                    attacker.Units.Last().SetItem(State.World.ItemRepository.GetRandomBook(1, maxMagicA, true), 1);
            }
            else
            {
                attacker.Units.Add(new NPC_unit(levelA, attackerMeleeHeavyCount > 0, 0, attackerSide, AttackerRace(), 0, Attacker.CanVore.isOn));
                attacker.Units.Last().ImmuneToDefections = true;
                attackerMeleeHeavyCount--;
                if (State.Rand.NextDouble() < Attacker.MagicPercentage.value / 100)
                    attacker.Units.Last().SetItem(State.World.ItemRepository.GetRandomBook(1, maxMagicA, true), 1);
            }
        }
        for (int x = 0; x < fightersB; x++)
        {
            if (x < defenderRangedCount)
            {
                defender.Units.Add(new NPC_unit(levelB, defenderRangedHeavyCount > 0, 1, defenderSide, DefenderRace(), 0, Defender.CanVore.isOn));
                defender.Units.Last().ImmuneToDefections = true;
                defenderRangedHeavyCount--;
                if (State.Rand.NextDouble() < Defender.MagicPercentage.value / 100)
                    defender.Units.Last().SetItem(State.World.ItemRepository.GetRandomBook(1, maxMagicB, true), 1);
            }
            else
            {
                defender.Units.Add(new NPC_unit(levelB, defenderMeleeHeavyCount > 0, 0, defenderSide, DefenderRace(), 0, Defender.CanVore.isOn));
                defender.Units.Last().ImmuneToDefections = true;
                defenderMeleeHeavyCount--;
                if (State.Rand.NextDouble() < Defender.MagicPercentage.value / 100)
                    defender.Units.Last().SetItem(State.World.ItemRepository.GetRandomBook(1, maxMagicB, true), 1);
            }
        }
        Village village = null;
        StrategicTileType tileType = StrategicTileType.grass;
        if (TerrainType.value == 1)
            tileType = StrategicTileType.forest;
        else if (TerrainType.value == 2)
            tileType = StrategicTileType.desert;
        else if (TerrainType.value == 3)
            tileType = StrategicTileType.snow;
        else if (TerrainType.value == 4)
            village = new Village("", none, 0, DefenderRace(), false);
        else if (TerrainType.value == 5)
        {
            village = new Village("", none, 0, DefenderRace(), false);
            village.buildings.Add(VillageBuilding.wall);
        }
        else if (TerrainType.value == 6)
        {
            tileType = StrategicTileType.desert;
            village = new Village("", none, 0, DefenderRace(), false);
        }
        else if (TerrainType.value == 7)
        {
            tileType = StrategicTileType.desert;
            village = new Village("", none, 0, DefenderRace(), false);
            village.buildings.Add(VillageBuilding.wall);
        }
        village?.UpdateNetBoosts();

        if (Attacker.Race.value == 0 && batching == false)
        {
            State.GameManager.TacticalMode.AttackerName = "Random";
            attacker.Name = "Random";
        }
        else
        {
            State.GameManager.TacticalMode.AttackerName = SpeciesPolicy.DisplayName(AttackerRace());
            attacker.Name = SpeciesPolicy.DisplayName(AttackerRace());
        }

        if (Defender.Race.value == 0 && batching == false)
        {
            State.GameManager.TacticalMode.DefenderName = "Random";
            defender.Name = "Random";
        }
        else
        {
            State.GameManager.TacticalMode.DefenderName = SpeciesPolicy.DisplayName(DefenderRace());
            defender.Name = SpeciesPolicy.DisplayName(DefenderRace());
        }

        State.GameManager.ActivatePureTacticalMode(tileType, village, attacker, defender, attackerAI, defenderAI);
        if (batching == false)
            gameObject.SetActive(false);
    }


    Race GetRandomRace()
    {
        Race race;
        SpeciesPolicy.TryRandom(SpeciesUse.Skirmish, out race);
        return race;
    }

    public void ChangeToolTip(int type)
    {
        TooltipText.text = DefaultTooltips.Tooltip(type);
    }
}

