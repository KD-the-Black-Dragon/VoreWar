// Modified for the VoreWar mercenary-company conversion.
// 2026-11-04: Apply approved injury tiers, base-STR HP, healing ceilings and recovery rules (spec 33).
// 2026-10-30: Remove the retired Unit Editor hyperlink from unit inspection (spec 02B).
// 2026-11-01: Label out-of-fight units as downed in character information (spec 10).
// 2026-11-01: Use the shared individual display names for retained species (09–10 feedback).
// 2026-11-01: Show persistent injury penalties in unit inspection, including Pure Tactical (spec 11).
// 2026-11-02: Show per-level XP and remaining progress consistently in unit details (spec 13 feedback).
// 2026-11-02: Display tactical weapon/spell slots and reduce only trait text size (spec 16).
// 2026-11-03: Reduce tactical trait and innate-spell text to 70 percent (spec 16 feedback).
// 2026-11-03: Keep the final trait line at the same reduced font metrics as its predecessors (spec 16 feedback).
// 2026-11-03: Remove permanently retired identity branches/entries while preserving shared behavior (spec 21).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
// 2026-11-03: Implement four active stats and agreed combat rules (spec 29).
// 2026-11-03: Compact tactical stat rows and restrict hover to active controls (spec 29 feedback).
using System;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InfoPanel
{
    UnitInfoPanel UnitInfoPanel;
    string parentMenu;
    public InfoPanel(UnitInfoPanel infoPanel, string menu = "unknown")
    {
        UnitInfoPanel = infoPanel;
        parentMenu = menu;
    }

    public Actor_Unit lastActor;

    public void RefreshLastUnitInfo()
    {
        if (lastActor == null)
            return;
        UpdatePanel(lastActor);
    }

    internal void HidePanel()
    {
        ClearText();
        UnitInfoPanel.gameObject.SetActive(false);
        UnitInfoPanel.Sprite?.transform.parent.gameObject.SetActive(false);
    }

    public void RefreshTacticalUnitInfo(Actor_Unit actor)
    {
        if (actor != null)
            lastActor = actor;
        if (actor == null)
        {
            //ClearText();
            return;
        }
        UnitInfoPanel.gameObject.SetActive(true);
        UnitInfoPanel.Sprite?.transform.parent.gameObject.SetActive(Config.HideUnitViewer == false);
        UpdatePanel(actor);

    }

    private void UpdatePanel(Actor_Unit actor)
    {
        if (actor.Hidden)
            return;
        UpdateBars(actor.Unit);
        StringBuilder sb = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        BuildStatus(sb2, actor.Unit);
        BuildStats(sb, actor.Unit, actor.Unit.Predator, actor);
        if (actor.Unit.Predator)
            BuildPredStat(sb, actor);
        if (parentMenu == "unitEditor")
        {
            UnitInfoPanel.InfoText.text = sb2.ToString();
        }
        else
        {
            UnitInfoPanel.InfoText.text = sb.ToString();
            UnitInfoPanel.BasicInfo.text = sb2.ToString();
        }
        UnitInfoPanel.Unit = actor.Unit;
        UnitInfoPanel.Actor = actor;
        if (Config.HideUnitViewer == false)
        {
            UnitInfoPanel.Sprite?.ResetBellyScale(actor);
            UnitInfoPanel.Sprite?.UpdateSprites(actor, false);
        }

    }

    private void UpdateBars(Unit actor, bool showNextText = false)
    {
        UnitInfoPanel.ExpBar.GetComponentInChildren<Text>().text = $"EXP: {actor.CurrentLevelExperience:0.#}/{actor.CurrentLevelExperienceRequired} ";
        if (showNextText)
            UnitInfoPanel.ExpBar.GetComponentInChildren<Text>().text += $"(To Next: {Math.Max(0, actor.CurrentLevelExperienceRequired - actor.CurrentLevelExperience):0.#})";
        UnitInfoPanel.HealthBar.GetComponentInChildren<Text>().text = $"Health: {actor.Health}/{actor.MaxHealth}" + (actor.Barrier > 0 ? $" + {actor.Barrier}" : "");
        UnitInfoPanel.ManaBar.GetComponentInChildren<Text>().text = $"Mana: {actor.Mana}/{actor.MaxMana}";
        if (actor.CurrentLevelExperienceRequired != 0)
            UnitInfoPanel.ExpBar.value = actor.CurrentLevelExperience / actor.CurrentLevelExperienceRequired;
        else
            UnitInfoPanel.ExpBar.value = 1;
        UnitInfoPanel.HealthBar.value = actor.HealthPct;
        UnitInfoPanel.BarrierBar.value = actor.BarrierPct;
        UnitInfoPanel.ManaBar.value = (float)actor.Mana / actor.MaxMana;
    }

    public void RefreshStrategicUnitInfo(Unit unit)
    {
        if (unit == null)
        {
            ClearText();
            return;
        }
        UpdateBars(unit, true);
        StringBuilder sb = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        BuildStats(sb, unit, unit.Predator, null);
        UnitInfoPanel.InfoText.text = sb.ToString();
        BuildStatus(sb2, unit);
        UnitInfoPanel.BasicInfo.text = sb2.ToString();
        UnitInfoPanel.Unit = unit;
        UnitInfoPanel.Actor = null;
    }

    internal void AddCorpse(string name)
    {
        if (name == "")
            return;
        UnitInfoPanel.InfoText.text = UnitInfoPanel.InfoText.text += $"Corpse of {name}\n";
    }

    internal void AddClothes(string name)
    {
        if (name == "")
            return;
        UnitInfoPanel.InfoText.text = UnitInfoPanel.InfoText.text += $"Clothes from {name}\n";
    }

    internal void AddLine(string description)
    {
        if (description == "")
            return;
        UnitInfoPanel.InfoText.text = UnitInfoPanel.InfoText.text += $"{description}\n";
    }

    public void ClearText()
    {
        UnitInfoPanel.InfoText.text = "";
    }

    public void ClearPicture()
    {
        UnitInfoPanel.Sprite.transform.parent.gameObject.SetActive(false);
    }


    public static string RaceSingular(Unit unit)
    {
        if (!SpeciesPolicy.IsRetired(unit.Race)) return SpeciesPolicy.DisplayName(unit.Race);
        if (unit.Race >= (Race)170)
            return unit.Race.ToString();
        switch (unit.Race)
        {
            case Race.Bunnies:
                return "Bunny";
            case Race.Lizards:
                return "Lizard";
            case Race.Kangaroos:
                return "Kangaroo";
            case Race.Equines:
                return "Equine";
            case Race.Sergal:
                return "Sergal";
            case Race.Bees:
                return "Bee";
            case Race.Bats:
                return "Bat";
            case Race.Alligators:
                return "Alligator";
            case Race.Hippos:
                return "Hippo";
            case Race.Komodos:
                return "Komodo";
            case Race.Serpents:
                return "Serpent";
            case Race.Wyvern:
                return "Wyvern";
            case Race.WyvernMatron:
                return "Wyvern Matron";
            case Race.FeralWolves:
                return "Wolf";
            case Race.Harvesters:
                return "Harvester";
            case Race.FeralBats:
                return "Bat";
            case Race.Dragon:
                return "Dragon";
            case Race.Dragonfly:
                return "Dragonfly";
            case Race.TwistedVines:
                return "Plant";
            case Race.FeralAnts:
                return "Ant";
            case Race.Gryphons:
                return "Gryphon";
            case Race.SpitterSlugs:
                return "SpitterSlug";
            case Race.SpringSlugs:
                return "SpringSlug";
            case Race.RockSlugs:
                return "RockSlug";
            case Race.CoralSlugs:
                return "CoralSlug";
            case Race.Panthers:
                return "Panther";
            case Race.Salamanders:
                return "Salamander";
            case Race.EasternDragon:
                return "Eastern Dragon";
            case Race.Mantis:
                return "Mantis";
            case Race.Avians:
                return "Avian";
            case Race.Catfish:
                return "Catfish";
            case Race.Ants:
                return "Ant";
            case Race.WarriorAnts:
                return "Warrior Ants";
            case Race.Frogs:
                return "Frog";
            case Race.Gazelle:
                return "Gazelle";
            case Race.Sharks:
                return "Shark";
            case Race.FeralLizards:
                return "Lizard";
            case Race.Monitors:
                return "Monitor";
            case Race.Deer:
                return "Deer";
            case Race.Mice:
                return "Mouse";
            case Race.Terrorbird:
                return "Terrorbird";
            case Race.Dratopyr:
                return "Dratopyr";
            case Race.FeralLions:
                return "Lion";
            case Race.Kobolds:
                return "Kobold";
            case Race.FeralHorses:
                return "Horse";
            case Race.FeralFox:
                return "Fox";
            case Race.Terminid:
                return "Terminid";
            case Race.Gnolls:
                return "Gnoll";
            case Race.FeralSlime:
                return "Slime";
            case Race.FeralUmbreon:
                return "Umbreon";
            case Race.Otachi:
                return "Otachi";
            case Race.Raiju:
                return "Raiju";
            case Race.Smudger:
                return "Smudger";    
            case Race.SpaceCroach:
                return "SpaceRoach";
            case Race.Trex:
                return "T-Rex";
            case Race.Utahraptor:
                return "Utahraptor";
            case Race.Badgers:
                return "Badger";
            case Race.Draconians:
                return "Draconian";
            case Race.none:
                return "None";
        }
        return unit.Race.ToString(); //Updated this so a new race will return the race's name, instead of nothing
    }

    public static string RaceSingular(Empire empire)
    {
        if (empire?.ReplacedRace >= (Race)170)
            return "";
        switch (empire?.ReplacedRace)
        {
            case Race.Bunnies:
                return "Bunny";
            case Race.Lizards:
                return "Lizard";
            case Race.Kangaroos:
                return "Kangaroo";
            case Race.Equines:
                return "Equine";
            case Race.Sergal:
                return "Sergal";
            case Race.Bees:
                return "Bee";
            case Race.Bats:
                return "Bat";
            case Race.Alligators:
                return "Alligator";
            case Race.Hippos:
                return "Hippo";
            case Race.Komodos:
                return "Komodo";
            case Race.Serpents:
                return "Serpent";
            case Race.Wyvern:
                return "Wyvern";
            case Race.WyvernMatron:
                return "Wyvern Matron";
            case Race.FeralWolves:
                return "Feral Wolf";
            case Race.Harvesters:
                return "Harvester";
            case Race.FeralBats:
                return "Bat";
            case Race.Kobolds:
                return "Kobolds";
            case Race.Dragon:
                return "Dragon";
            case Race.Dragonfly:
                return "Dragonfly";
            case Race.TwistedVines:
                return "Plant";
            case Race.FeralAnts:
                return "Ant";
            case Race.Gryphons:
                return "Gryphon";
            case Race.SpitterSlugs:
                return "Spitter Slug";
            case Race.SpringSlugs:
                return "Spring Slug";
            case Race.RockSlugs:
                return "Rock Slug";
            case Race.CoralSlugs:
                return "Coral Slug";
            case Race.Panthers:
                return "Panther";
            case Race.Salamanders:
                return "Salamander";
            case Race.EasternDragon:
                return "Eastern Dragon";
            case Race.Mantis:
                return "Mantis";
            case Race.Avians:
                return "Avian";
            case Race.Catfish:
                return "Catfish";
            case Race.Ants:
                return "Ant";
            case Race.WarriorAnts:
                return "Warrior Ant";
            case Race.Frogs:
                return "Frog";
            case Race.Gazelle:
                return "Gazelle";
            case Race.Sharks:
                return "Shark";
            case Race.FeralLizards:
                return "Feral Lizard";
            case Race.Monitors:
                return "Monitor";
            case Race.Deer:
                return "Deer";
            case Race.Mice:
                return "Mouse";
            case Race.Terrorbird:
                return "Terrorbird";
            case Race.Dratopyr:
                return "Dratopyr";
            case Race.FeralLions:
                return "Lion";
            case Race.FeralHorses:
                return "Horse";
            case Race.FeralFox:
                return "Fox";
            case Race.Terminid:
                return "Terminid";
            case Race.Gnolls:
                return "Gnoll";
            case Race.FeralSlime:
                return "Slime";
            case Race.FeralUmbreon:
                return "Umbreon";
            case Race.Otachi:
                return "Otachi";
            case Race.Raiju:
                return "Raiju";
            case Race.Smudger:
                return "Smudger";    
            case Race.SpaceCroach:
                return "Space Roach";
            case Race.Trex:
                return "T-Rex";
            case Race.Utahraptor:
                return "Utahraptor";
            case Race.Badgers:
                return "Badger";
            case Race.Draconians:
                return "Draconian";
        }
        return empire.ReplacedRace.ToString();
    }

    void BuildStatus(StringBuilder sb, Unit unit)
    {
        // Add Name
        if (unit.Type == UnitType.Summon)
            sb.AppendLine(unit.Name + "(Summon)");
        else
            sb.AppendLine(unit.Name);

        // Add Level and Race
        sb.AppendLine($"Level {unit.Level} {RaceSingular(unit)}");

        // Add Gender and Type
        if (unit.Race >= (Race)170)
            sb.AppendLine($"{unit.Type}");
        else
        {
            var race = Races.GetRace(unit.Race);
            if (race != null && race.CanBeGender.Contains(Gender.None))
                sb.AppendLine($"{unit.Type}");
            else if (unit.GetGender() == Gender.Hermaphrodite)
                sb.AppendLine($"Herm {unit.Type}");
            else if (unit.GetGender() == Gender.Andromorph)
                sb.AppendLine($"Andro {unit.Type}");
            else
                sb.AppendLine($"{unit.GetGender()} {unit.Type}");
        }
    }

    void BuildStats(StringBuilder sb, Unit unit, bool CanVore, Actor_Unit actor)
    {
        if (parentMenu != "unitEditor")
        {
            if (unit.HasFixedSide() && TacticalUtilities.PlayerCanSeeTrueSide(unit))
            {
                if (State.World.MainEmpires == null)
                {
                    sb.AppendLine($"Special Allegiance: {(unit.FixedSide == State.GameManager.TacticalMode.GetDefenderSide() ? "Defender" : "Attacker")}");
                }
                else
                    sb.AppendLine($"Special Allegiance: {State.World.GetEmpireOfSide(unit.FixedSide)?.Name ?? "Unkown"}");
            }
            if (!TacticalHudView.FillEquipment(UnitInfoPanel, unit, actor))
            {
                UnityEngine.Transform EquipRow = UnitInfoPanel.StatBlock.transform.GetChild(5);

                if (unit.GetItem(0)?.Name == null)
                    EquipRow.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                else
                    EquipRow.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = unit.GetItem(0)?.Name;
                if (unit.GetItem(1)?.Name == null)
                    EquipRow.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                else
                    EquipRow.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = unit.GetItem(1)?.Name;
                if (unit.HasTrait(Traits.Resourceful))
                {
                    EquipRow.transform.GetChild(2).gameObject.SetActive(true);
                    if (unit.GetItem(2)?.Name == null)
                        EquipRow.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                    else
                        EquipRow.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = unit.GetItem(2)?.Name;
                }
                else
                    EquipRow.transform.GetChild(2).gameObject.SetActive(false);
            }
            // Add Equipment
            //for (int i = 0; i < unit.Items.Length; i++)
            //{
            //    sb.AppendLine(unit.GetItem(i)?.Name ?? "empty");
            //}


            for (int i = 0; i < WarbandStats.Active.Length; i++)
            {
                var cell = UnitInfoPanel.StatBlock.transform.GetChild(i / 2).GetChild(i % 2);
                var stat = WarbandStats.Active[i];
                var label = cell.GetChild(0).GetComponent<TMP_Text>();
                if (label != null) label.text = WarbandStats.ShortName(stat);
                var legacyLabel = cell.GetChild(0).GetComponent<Text>();
                if (legacyLabel != null) legacyLabel.text = WarbandStats.ShortName(stat);
                cell.GetChild(1).GetComponent<Text>().text = unit.GetStatInfo(stat);
                cell.gameObject.SetActive(true);
            }
            UnitInfoPanel.StatBlock.transform.GetChild(2).gameObject.SetActive(false);
            UnitInfoPanel.StatBlock.transform.GetChild(3).gameObject.SetActive(false);
            UnityEngine.Transform FifthLine = UnitInfoPanel.StatBlock.transform.GetChild(4);
            FifthLine.GetChild(0).gameObject.SetActive(false);

            UnityEngine.Transform killSection = FifthLine.GetChild(1).GetChild(0);
            UnityEngine.Transform digestSection = FifthLine.GetChild(1).GetChild(1);
            UnityEngine.Transform deathSection = FifthLine.GetChild(1).GetChild(2);
            // Set kill counter, hide otherwise
            if (unit.KilledUnits > 0)
            {
                FifthLine.gameObject.SetActive(true);
                FifthLine.GetChild(1).gameObject.SetActive(true);
                killSection.gameObject.SetActive(true);
                killSection.GetChild(1).GetComponent<Text>().text = unit.KilledUnits.ToString();
            }
            else
                killSection.gameObject.SetActive(false);
            // Set digestion counter, hide otherwise
            if (unit.DigestedUnits > 0)
            {
                FifthLine.gameObject.SetActive(true);
                FifthLine.GetChild(1).gameObject.SetActive(true);
                digestSection.gameObject.SetActive(true);
                digestSection.GetChild(1).GetComponent<Text>().text = unit.DigestedUnits.ToString();
            }
            else
                digestSection.gameObject.SetActive(false);
            // Set death counter, hide otherwise
            if (unit.TimesKilled > 0)
            {
                FifthLine.gameObject.SetActive(true);
                FifthLine.GetChild(1).gameObject.SetActive(true);
                deathSection.gameObject.SetActive(true);
                deathSection.GetChild(1).GetComponent<Text>().text = unit.TimesKilled.ToString();
            }
            else
                deathSection.gameObject.SetActive(false);
            if (!killSection.gameObject.activeSelf && !digestSection.gameObject.activeSelf && !deathSection.gameObject.activeSelf)
                FifthLine.GetChild(1).gameObject.SetActive(false);
            if (!FifthLine.GetChild(0).gameObject.activeSelf && !FifthLine.GetChild(1).gameObject.activeSelf)
                FifthLine.gameObject.SetActive(false);
            TacticalHudView.CompactStats(UnitInfoPanel);

            if (Config.ShowUnitSides && actor?.Unit.Side == State.GameManager.TacticalMode.GetDefenderSide())
                sb.AppendLine("DEFENDER");
            if (Config.ShowUnitSides && actor?.Unit.Side == State.GameManager.TacticalMode.GetAttackerSide())
                sb.AppendLine("ATTACKER");
            if (unit.SavedCopy != null && unit.SavedVillage != null)
                sb.AppendLine($"Imprinted");
            if (actor?.Surrendered ?? false)
                sb.AppendLine("Unit is downed for the remainder of this battle.");
            string traits = unit.ListTraits(!(TacticalUtilities.IsUnitControlledByPlayer(unit) && TacticalUtilities.PlayerCanSeeTrueSide(unit)));
            if (Config.RaceTraitsEnabled == false && (traits == ""))
                sb.AppendLine("Race Traits disabled");
            if (traits != "")
            {
                if (TacticalHudView.IsTacticalPanel(UnitInfoPanel))
                    sb.Append("<size=70%>Traits:\n" + traits + "\n</size>");
                else
                    sb.AppendLine("Traits:\n" + traits);
            }
            if (unit.Injuries.Count > 0) sb.AppendLine("Injuries:\n" + string.Join("\n", unit.Injuries.Select(CompanyInjuries.Description).ToArray()));
            StringBuilder sbSecond = new StringBuilder();
            sbSecond.AppendLine("Status:");
            if (CompanyInjuries.DamageTier(unit) > 0) sbSecond.AppendLine(CompanyInjuries.DamageDescription(unit));
            if (unit.HasTrait(Traits.Frenzy) && unit.EnemiesDigestedThisBattle > 0)
                sbSecond.AppendLine($"Frenzy ({unit.EnemiesDigestedThisBattle})");
            if (unit.HasTrait(Traits.Growth) && unit.BaseScale > 1)
                sbSecond.AppendLine($"Growth ({Math.Round(unit.BaseScale, 2)}x)");
            if (actor?.Slimed ?? false)
                sbSecond.AppendLine("Slimed");
            if (actor?.Paralyzed ?? false)
                sbSecond.AppendLine("Paralyzed");
            if (actor?.Corruption > 0 && !TacticalUtilities.IsUnitControlledByPlayer(unit))
                sbSecond.AppendLine($"Corruption ({actor.Corruption}/{unit.GetStatTotal() + unit.GetStat(Stat.Will)})");
            if (actor?.Possessed > 0 && !TacticalUtilities.IsUnitControlledByPlayer(unit))
                sbSecond.AppendLine($"Possessed ({actor.Corruption + actor.Possessed}/{unit.GetStatTotal() + unit.GetStat(Stat.Will)})");
            if (actor?.Infected ?? false)
                sbSecond.AppendLine($"Infected");
            if (unit.StatusEffects?.Any() ?? false)
            {
                foreach (StatusEffectType type in (StatusEffectType[])Enum.GetValues(typeof(StatusEffectType)))
                {
                    var effect = unit.GetLongestStatusEffect(type);
                    if (unit.GetLongestStatusEffect(type) != null)
                        sbSecond.AppendLine($"{type} ({effect.Duration})");
                }
            }

            if (sbSecond.Length > 10)
                sb.Append($"{sbSecond}");

            var racePar = RaceParameters.GetTraitData(unit);

            if ((unit.InnateSpells?.Any() ?? false) || (racePar.InnateSpells?.Any() ?? false))
            {
                if (TacticalHudView.IsTacticalPanel(UnitInfoPanel)) sb.Append("<size=70%>");
                sb.AppendLine("Innate Spells:");
                if (unit.InnateSpells != null)
                {
                    foreach (var spellType in unit.InnateSpells)
                    {
                        if (SpellList.SpellDict.TryGetValue(spellType, out Spell spell))
                        {
                            sb.AppendLine(spell.Name);
                        }
                        else
                        {
                            sb.AppendLine("Invalid Spell");
                        }
                    }
                }
                if (racePar.InnateSpells != null)
                {
                    foreach (var spellType in racePar.InnateSpells)
                    {
                        if (SpellList.SpellDict.TryGetValue(spellType, out Spell spell))
                        {
                            sb.AppendLine(spell.Name);
                        }
                        else
                        {
                            sb.AppendLine("Invalid Spell");
                        }
                    }
                }
                var grantedSpell = State.RaceSettings.GetInnateSpell(unit.Race);
                if (grantedSpell != SpellTypes.None)
                {
                    if (SpellList.SpellDict.TryGetValue(grantedSpell, out Spell spell))
                    {
                        sb.AppendLine(spell.Name);
                    }
                    else
                    {
                        sb.AppendLine("Invalid Spell");
                    }
                }
                if (TacticalHudView.IsTacticalPanel(UnitInfoPanel)) sb.Append("</size>");
            }
            if (actor != null)
            {
                if (actor.Unit.EquippedPotions != null)
                {
                    if (!actor.Unit.EquippedPotions.Any(p => p.Value[0] > 0) == false)
                        UnitInfoPanel.PotionHoverImage.gameObject.SetActive(true);
                    else
                        UnitInfoPanel.PotionHoverImage.gameObject.SetActive(false);
                }
                else
                {
                    UnitInfoPanel.PotionHoverImage.gameObject.SetActive(false);
                }

                List<UnitTag> Tags = TagConditionChecker.GetCombinedUnitTags(actor.Unit);
                if (Tags.Count > 0)
                    UnitInfoPanel.UnitTagHoverImage.gameObject.SetActive(true);
                else
                    UnitInfoPanel.UnitTagHoverImage.gameObject.SetActive(false);
                //if (Config.CheatUnitEditorEnabled)
                    //sb.AppendLine("<color=#AB5200ff>UnitEditor</color>");
            }
            else
            {
                UnitInfoPanel.PotionHoverImage.gameObject.SetActive(false);
                UnitInfoPanel.UnitTagHoverImage.gameObject.SetActive(false);
            }
        }
    }

    void BuildPredStat(StringBuilder sb, Actor_Unit unit)
    {
        sb.Append(unit.PredatorComponent.GetPreyInformation());

    }


}
