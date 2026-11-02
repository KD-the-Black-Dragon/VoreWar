// Modified for the VoreWar mercenary-company conversion.
// 2026-10-30: Delegate preference/default loading to ContentSettingsData; retain menu bindings (spec 02A).
// 2026-10-30: Retire settings widgets and apply only retained preferences (spec 02C).
// 2026-10-30: Add the approved-species availability page without legacy spawning controls (spec 03A1).
// 2026-11-01: Display singular spaced species labels while preserving numeric preference keys (09–10 feedback).
// 2026-11-02: Present retained content pages in a compact blue scroll column and retire nine controls (spec 15B).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContentSettings : MonoBehaviour
{
    public GameObject[] SettingsPanels;
    public Slider FemaleFraction;
    public Slider HermFraction;
    public Slider HermNameFraction;
    public Slider ClothedFraction;
    public Toggle FurryHandsAndFeet;
    public Toggle FurryFluff;
    public Slider FurryFraction;
    public Toggle FriendlyRegurgitation;
    public Toggle HairMatchesFur;
    public Toggle MaleHairForFemales;
    public Toggle FemaleHairForMales;
    public Toggle HermsOnlyUseFemaleHair;
    public Toggle HideBreasts;
    public Toggle HideCocks;
    public Toggle RagsForSlaves;
    public Toggle VisibleCorpses;
    public Toggle LeadersUseCustomizations;
    public Toggle HermsCanUB;
    public Toggle Unbirth;
    public Toggle CockVore;
    public Toggle AnalVore;
    public Toggle TailVore;
    public Toggle CanUseStomachRubOnEnemies;
    public Toggle LeadersRerandomizeOnDeath;
    public Toggle AllowHugeBreasts;
    public Toggle AllowHugeDicks;
    public Toggle AllowTopless;
    public Slider BreastSizeModifier;
    public Slider HermBreastSizeModifier;
    public Slider CockSizeModifier;
    public Slider DefaultStartingWeight;
    public Toggle CockVoreHidesClothes;
    public Toggle LewdDialog;
    public TMP_Dropdown FemalesLike;
    public TMP_Dropdown MalesLike;
    public TMP_Dropdown WinterStuff;
    public Slider OralWeight;
    public Slider AnalWeight;
    public Slider CockWeight;
    public Slider UnbirthWeight;
    public Slider TailWeight;
    public Toggle FurryGenitals;
    public Toggle LamiaUseTailAsSecondBelly;
    public Toggle HideViperSlit;
    public Toggle AnimatedBellies;
    public Toggle DigestionSkulls;
    public Toggle BellyRubHands;
    public Toggle SurrenderFlag;
    public Toggle ShowUnitSides;
    public Slider BurpFraction;
    public Toggle BurpOnDigest;
    public Toggle Bones;
    public Toggle ClothingDiscards;
    public Toggle ErectionsFromVore;
    public Toggle ErectionsFromCockVore;
    public Toggle ExtraHairColors;
    public Toggle LizardsHaveNoBreasts;
    public Toggle Cumstains;
    public Toggle CondomsForCV;
    public Text TooltipText;
    public TMP_Dropdown PageSelector;

    List<ToggleObject> Toggles;
    GameObject speciesPanel;
    ContentSettingsView presentation;
    internal IEnumerable<KeyValuePair<Race, Toggle>> SpeciesChoices { get { return speciesToggles; } }
    readonly Dictionary<Race, Toggle> speciesToggles = new Dictionary<Race, Toggle>();


    class ToggleObject
    {
        internal Toggle Toggle;
        internal string Name;

        public ToggleObject(Toggle toggle, string name)
        {
            Toggle = toggle;
            Name = name;
        }
    }

    void CreateList()
    {
        CreateSpeciesPage();
        Toggles = new List<ToggleObject>
        {
            new ToggleObject(FurryHandsAndFeet, "FurryHandsAndFeet"),
            new ToggleObject(FurryFluff, "FurryFluff"),
            new ToggleObject(FriendlyRegurgitation, "FriendlyRegurgitation"),
            new ToggleObject(HairMatchesFur, "HairMatchesFur"),
            new ToggleObject(MaleHairForFemales, "MaleHairForFemales"),
            new ToggleObject(FemaleHairForMales, "FemaleHairForMales"),
            new ToggleObject(HermsOnlyUseFemaleHair, "HermsOnlyUseFemaleHair"),
            new ToggleObject(HideBreasts, "HideBreasts"),
            new ToggleObject(HideCocks, "HideCocks"),
            new ToggleObject(HermsCanUB, "HermsCanUB"),
            new ToggleObject(Unbirth, "Unbirth"),
            new ToggleObject(CockVore, "CockVore"),
            new ToggleObject(AnalVore, "AnalVore"),
            new ToggleObject(TailVore, "TailVore"),
            new ToggleObject(CockVoreHidesClothes, "CockVoreHidesClothes"),
            new ToggleObject(AllowHugeBreasts, "AllowHugeBreasts"),
            new ToggleObject(AllowHugeDicks, "AllowHugeDicks"),
            new ToggleObject(AllowTopless, "AllowTopless"),
            new ToggleObject(FurryGenitals, "FurryGenitals"),
            new ToggleObject(AnimatedBellies, "AnimatedBellies"),
            new ToggleObject(DigestionSkulls, "DigestionSkulls"),
            new ToggleObject(BellyRubHands, "BellyRubHands"),
            new ToggleObject(SurrenderFlag, "SurrenderFlag"),
            new ToggleObject(ShowUnitSides, "ShowUnitSides"),
            new ToggleObject(Bones, "Bones"),
            new ToggleObject(CondomsForCV, "CondomsForCV"),
            new ToggleObject(ErectionsFromVore, "ErectionsFromVore"),
            new ToggleObject(ErectionsFromCockVore, "ErectionsFromCockVore"),
            new ToggleObject(ClothingDiscards, "ClothingDiscards"),
            new ToggleObject(ExtraHairColors, "ExtraRandomHairColors"),
            new ToggleObject(LizardsHaveNoBreasts, "LizardsHaveNoBreasts"),
            new ToggleObject(HideViperSlit, "HideViperSlit"),
            new ToggleObject(Cumstains, "Cumstains"),
            new ToggleObject(BurpOnDigest, "BurpOnDigest"),
            new ToggleObject(CanUseStomachRubOnEnemies, "CanUseStomachRubOnEnemies"),
        };
    }

    // Build the small availability page from retained UI styling. No legacy page/prefab is re-enabled.
    void CreateSpeciesPage()
    {
        if (speciesPanel != null)
            return;
        RectTransform template = (RectTransform)SettingsPanels[0].transform;
        speciesPanel = new GameObject("Species Availability", typeof(RectTransform));
        speciesPanel.SetActive(false);
        speciesPanel.layer = template.gameObject.layer;
        RectTransform panelRect = (RectTransform)speciesPanel.transform;
        panelRect.SetParent(template.parent, false);
        panelRect.anchorMin = template.anchorMin;
        panelRect.anchorMax = template.anchorMax;
        panelRect.pivot = template.pivot;
        panelRect.sizeDelta = template.sizeDelta;
        panelRect.anchoredPosition = template.anchoredPosition;
        panelRect.localScale = template.localScale;

        RectTransform descriptionRect = CreateSpeciesArea("Description", panelRect);
        descriptionRect.anchorMin = new Vector2(0, 1);
        descriptionRect.anchorMax = Vector2.one;
        descriptionRect.pivot = new Vector2(.5f, 1);
        descriptionRect.offsetMin = new Vector2(20, -110);
        descriptionRect.offsetMax = new Vector2(-20, -20);
        Text description = descriptionRect.gameObject.AddComponent<Text>();
        description.font = TooltipText.font;
        description.fontSize = 22;
        description.color = Color.white;
        description.alignment = TextAnchor.UpperLeft;
        description.raycastTarget = false;
        Race[] curated = SpeciesPolicy.GetCuratedSpecies();
        description.text = curated.Length == 0 ? SpeciesPolicy.UnavailableMessage :
            "Choose which approved species can be generated. Existing characters are preserved when their species is disabled.";

        RectTransform viewport = CreateSpeciesArea("Viewport", panelRect);
        viewport.offsetMin = new Vector2(20, 20);
        viewport.offsetMax = new Vector2(-20, -120);
        viewport.gameObject.AddComponent<RectMask2D>();
        ScrollRect scroll = viewport.gameObject.AddComponent<ScrollRect>();
        scroll.viewport = viewport;
        scroll.horizontal = false;
        scroll.movementType = ScrollRect.MovementType.Clamped;
        RectTransform content = CreateSpeciesArea("Species", viewport);
        content.anchorMin = new Vector2(0, 1);
        content.anchorMax = Vector2.one;
        content.pivot = new Vector2(.5f, 1);
        content.sizeDelta = Vector2.zero;
        scroll.content = content;
        VerticalLayoutGroup layout = content.gameObject.AddComponent<VerticalLayoutGroup>();
        layout.childControlWidth = true;
        layout.childControlHeight = true;
        layout.childForceExpandWidth = true;
        layout.childForceExpandHeight = false;
        layout.spacing = 8;
        ContentSizeFitter fitter = content.gameObject.AddComponent<ContentSizeFitter>();
        fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        foreach (Race race in curated)
        {
            Toggle toggle = Instantiate(ShowUnitSides, content, false);
            toggle.gameObject.name = "Species " + race;
            toggle.gameObject.SetActive(true);
            toggle.onValueChanged.RemoveAllListeners();
            for (int i = 0; i < toggle.onValueChanged.GetPersistentEventCount(); i++)
                toggle.onValueChanged.SetPersistentListenerState(i, UnityEngine.Events.UnityEventCallState.Off);
            foreach (DisplayTooltip tooltip in toggle.GetComponentsInChildren<DisplayTooltip>(true))
                tooltip.enabled = false;
            Text label = toggle.GetComponentInChildren<Text>();
            label.text = SpeciesPolicy.DisplayName(race) + " (" + SpeciesPolicy.GetKind(race) + ")";
            label.fontSize = 22;
            LayoutElement element = toggle.GetComponent<LayoutElement>();
            if (element == null)
                element = toggle.gameObject.AddComponent<LayoutElement>();
            element.preferredHeight = 45;
            speciesToggles.Add(race, toggle);
        }
        Array.Resize(ref SettingsPanels, SettingsPanels.Length + 1);
        SettingsPanels[SettingsPanels.Length - 1] = speciesPanel;
        PageSelector.AddOptions(new List<string> { "Species Availability" });
    }

    RectTransform CreateSpeciesArea(string name, Transform parent)
    {
        GameObject area = new GameObject(name, typeof(RectTransform));
        area.layer = parent.gameObject.layer;
        RectTransform rect = (RectTransform)area.transform;
        rect.SetParent(parent, false);
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
        return rect;
    }

    public void ActivatePage()
    {
        int selected = Mathf.Clamp(PageSelector.value, 0, SettingsPanels.Length - 1);
        if (presentation != null)
        {
            presentation.ShowPage(selected);
            ChangeToolTip(0);
            return;
        }
        for (int i = 0; i < SettingsPanels.Length; i++)
            SettingsPanels[i].SetActive(i == selected);
    }

    public void ConfirmRefresh()
    {
        var box = Instantiate(State.GameManager.DialogBoxPrefab).GetComponent<DialogBox>();
        box.SetData(LoadSaved, "Load Data", "Cancel", "This will load all of your saved global settings into this game, useful if you wish to apply your new settings to old saved games quickly.  This is not undoable.");
    }

    void LoadSaved()
    {
        Refresh();
        Open();
    }

    public void Refresh()
    {
        if (Toggles == null)
            CreateList();
        ContentSettingsData.LoadSaved(Config.World);
        Config.BuildConfig = new BuildingConfig();
    }

    internal void ChangeToolTip(int value)
    {
        TooltipText.text = DefaultTooltips.Tooltip(value);
    }

    public void Open()
    {
        if (Toggles == null)
            CreateList();
        ContentSettingsData.ApplyLockedValues(Config.World);
        gameObject.SetActive(true);
        foreach (ToggleObject toggle in Toggles)
            toggle.Toggle.isOn = Config.World.GetValue(toggle.Name);
        FemaleFraction.value = 1 - Config.MaleFraction;
        HermFraction.value = Config.HermFraction;
        HermNameFraction.value = Config.HermNameFraction;
        ClothedFraction.value = Config.ClothedFraction;
        FurryFraction.value = Config.FurryFraction;
        BreastSizeModifier.value = Config.BreastSizeModifier;
        HermBreastSizeModifier.value = Config.HermBreastSizeModifier;
        CockSizeModifier.value = Config.CockSizeModifier;
        DefaultStartingWeight.value = Config.DefaultStartingWeight;
        FemalesLike.value = (int)Config.FemalesLike;
        MalesLike.value = (int)Config.MalesLike;
        WinterStuff.value = (int)Config.World.WinterStuff;
        OralWeight.value = Config.OralWeight;
        AnalWeight.value = Config.AnalWeight;
        CockWeight.value = Config.CockWeight;
        UnbirthWeight.value = Config.UnbirthWeight;
        TailWeight.value = Config.TailWeight;
        BurpFraction.value = Config.BurpFraction;
        FemalesLike.RefreshShownValue();
        MalesLike.RefreshShownValue();
        WinterStuff.RefreshShownValue();
        PageSelector.RefreshShownValue();
        foreach (var species in speciesToggles)
            species.Value.isOn = SpeciesPolicy.IsEnabled(species.Key);
        if (presentation == null)
            presentation = new ContentSettingsView(this);
        ActivatePage();
        VoreTypesChanged();
        ChangeToolTip(0);
    }

    void Update()
    {
        if (presentation != null) presentation.Refresh();
    }

    void SetValues()
    {
        if (Config.HermsCanUB != HermsCanUB.isOn)
        {
            if (State.World?.MainEmpires != null)
            {
                foreach (Unit unit in StrategicUtilities.GetAllUnits())
                {
                    if (unit.GetGender() == Gender.Hermaphrodite || unit.GetGender() == Gender.Gynomorph)
                    {
                        unit.HasVagina = HermsCanUB.isOn;
                    }
                }
            }
            else
            {
                if (TacticalUtilities.Units != null)
                {
                    foreach (var actor in TacticalUtilities.Units)
                    {
                        if (actor.Unit.GetGender() == Gender.Hermaphrodite || actor.Unit.GetGender() == Gender.Gynomorph)
                        {
                            actor.Unit.HasVagina = HermsCanUB.isOn;
                        }
                    }
                }

            }
        }
        foreach (ToggleObject toggle in Toggles)
            Config.World.Toggles[toggle.Name] = toggle.Toggle.isOn;
        Config.World.MaleFraction = 1 - FemaleFraction.value;
        Config.World.HermFraction = HermFraction.value;
        Config.World.HermNameFraction = HermNameFraction.value;
        Config.World.ClothedFraction = ClothedFraction.value;
        Config.World.FurryFraction = FurryFraction.value;
        Config.World.BreastSizeModifier = (int)BreastSizeModifier.value;
        Config.World.HermBreastSizeModifier = (int)HermBreastSizeModifier.value;
        Config.World.CockSizeModifier = (int)CockSizeModifier.value;
        Config.World.DefaultStartingWeight = (int)DefaultStartingWeight.value;
        Config.World.OralWeight = (int)OralWeight.value;
        Config.World.AnalWeight = (int)AnalWeight.value;
        Config.World.CockWeight = (int)CockWeight.value;
        Config.World.UnbirthWeight = (int)UnbirthWeight.value;
        Config.World.TailWeight = (int)TailWeight.value;
        Config.World.BurpFraction = BurpFraction.value;
        foreach (var species in speciesToggles)
            SpeciesPolicy.SetEnabled(species.Key, species.Value.isOn);
        ContentSettingsData.ApplyLockedValues(Config.World);
    }

    public void Exit()
    {
        SetValues();
        gameObject.SetActive(false);
    }

    public void ExitAndSave()
    {
        SetValues();
        ContentSettingsData.SavePreferences(Config.World);
        gameObject.SetActive(false);
    }

    public void VoreTypesChanged()
    {
        AnalWeight.interactable = AnalVore.isOn;
        TailWeight.interactable = TailVore.isOn;
        UnbirthWeight.interactable = Unbirth.isOn;
        CockWeight.interactable = CockVore.isOn;
    }
}
