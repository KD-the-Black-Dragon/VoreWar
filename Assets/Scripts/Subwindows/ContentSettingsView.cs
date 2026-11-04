// Added by the VoreWar mercenary-company conversion fork.
// 2026-11-02: Restyle retained content pages with a centered blue scroll column, top page selector and visible checkmarks (spec 15B).
// 2026-11-02: Restore the user-preferred herm/herms wording in content labels (spec 15B feedback).
// 2026-11-02: Name content apply buttons by their current-game or saved-global-default scope (spec 15B feedback).
// 2026-11-03: Theme dropdown item surfaces while preserving checkmark graphics (spec 28).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal sealed class ContentSettingsView
{
    sealed class ControlRow
    {
        internal Selectable Control;
        internal Text Label, Value;
        internal Button Button;
        internal Func<float, string> Format;
    }

    readonly List<ControlRow> rows = new List<ControlRow>();
    readonly Dictionary<Selectable, int> originalPages = new Dictionary<Selectable, int>();
    readonly RectTransform[] pages;
    readonly float[] positions;
    readonly ScrollRect scroll;
    int selected = -1;

    internal ContentSettingsView(ContentSettings owner)
    {
        var legacy = owner.transform.Cast<Transform>().ToArray();
        // Capture ownership before moving anything: some sliders are children of toggles.
        for (int i = 0; i < owner.SettingsPanels.Length; i++)
            foreach (var control in owner.SettingsPanels[i].GetComponentsInChildren<Selectable>(true))
                originalPages[control] = i;

        var scaler = owner.GetComponent<CanvasScaler>();
        scaler.referenceResolution = new Vector2(1280, 800);
        scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
        var background = UITheme.Node("Content settings presentation", owner.transform);
        UITheme.Stretch(background.GetComponent<RectTransform>()); UITheme.Surface(background, UITheme.Background);
        var frame = UITheme.Node("Centered content column", background.transform);
        var rect = frame.GetComponent<RectTransform>();
        rect.anchorMin = rect.anchorMax = rect.pivot = new Vector2(.5f, .5f);
        rect.sizeDelta = new Vector2(960, 760); rect.anchoredPosition = Vector2.zero;
        UITheme.Surface(frame, UITheme.Panel, true);
        UITheme.Label(frame.transform, "Content Settings", 28, 18, 350, 48, 28);

        var viewport = UITheme.Node("Content viewport", frame.transform);
        UITheme.Place(viewport.GetComponent<RectTransform>(), 28, 92, 884, 500);
        UITheme.Surface(viewport, UITheme.Background); viewport.AddComponent<RectMask2D>();
        scroll = viewport.AddComponent<ScrollRect>(); scroll.viewport = viewport.GetComponent<RectTransform>();
        scroll.horizontal = false; scroll.movementType = ScrollRect.MovementType.Clamped; scroll.scrollSensitivity = 36;
        var bar = UITheme.Node("Content scrollbar", frame.transform);
        UITheme.Place(bar.GetComponent<RectTransform>(), 924, 92, 12, 500);
        UITheme.Surface(bar, UITheme.Background, true);
        var handle = UITheme.Node("Handle", bar.transform); UITheme.Stretch(handle.GetComponent<RectTransform>());
        var scrollbar = bar.AddComponent<Scrollbar>(); scrollbar.direction = Scrollbar.Direction.BottomToTop;
        scrollbar.handleRect = handle.GetComponent<RectTransform>(); scrollbar.targetGraphic = UITheme.Surface(handle, UITheme.Control);
        UITheme.StyleSelectable(scrollbar); scroll.verticalScrollbar = scrollbar;

        pages = new RectTransform[owner.SettingsPanels.Length]; positions = new float[pages.Length];
        for (int i = 0; i < pages.Length; i++)
        {
            var page = UITheme.Node(owner.PageSelector.options[i].text, viewport.transform);
            pages[i] = page.GetComponent<RectTransform>(); positions[i] = 1;
            pages[i].anchorMin = new Vector2(0, 1); pages[i].anchorMax = Vector2.one;
            pages[i].pivot = new Vector2(.5f, 1); pages[i].sizeDelta = Vector2.zero;
            var layout = page.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 4; layout.padding = new RectOffset(2, 2, 2, 12);
            layout.childControlHeight = layout.childControlWidth = true; layout.childForceExpandHeight = false;
            page.AddComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        }

        Checkbox("Downed flags", owner.SurrenderFlag);
        Checkbox("Show unit sides", owner.ShowUnitSides);
        SliderRow("Female percentage", owner.FemaleFraction, Percent);
        SliderRow("Herm percentage", owner.HermFraction, Percent);
        SliderRow("Female names for herms", owner.HermNameFraction, Percent);
        Checkbox("Females can have masculine hairstyles", owner.MaleHairForFemales);
        Checkbox("Males can have feminine hairstyles", owner.FemaleHairForMales);
        Checkbox("Herms use only feminine hairstyles", owner.HermsOnlyUseFemaleHair);
        Checkbox("Hide breasts", owner.HideBreasts);
        Checkbox("Hide dicks", owner.HideCocks);
        Checkbox("Herms have vaginas", owner.HermsCanUB);
        Checkbox("Allow huge breasts", owner.AllowHugeBreasts);
        Checkbox("Allow huge dicks", owner.AllowHugeDicks);
        Checkbox("Allow topless options for females", owner.AllowTopless);
        SliderRow("Breast size modifier", owner.BreastSizeModifier, Number);
        SliderRow("Herm breast size modifier", owner.HermBreastSizeModifier, Number);
        SliderRow("Dick size modifier", owner.CockSizeModifier, Number);
        Checkbox("Cock vore hides clothes", owner.CockVoreHidesClothes);
        Checkbox("Furry genitals", owner.FurryGenitals);
        Checkbox("Hide viper slits", owner.HideViperSlit);
        Checkbox("Erections from vore", owner.ErectionsFromVore);
        Checkbox("Erections from cock vore", owner.ErectionsFromCockVore);
        Checkbox("No breasts for Lizards", owner.LizardsHaveNoBreasts);
        SliderRow("Clothed percentage", owner.ClothedFraction, Percent);
        SliderRow("Furry percentage", owner.FurryFraction, Percent);
        Checkbox("Furry hands and feet for certain species", owner.FurryHandsAndFeet);
        Checkbox("Furry fluff patches for certain species", owner.FurryFluff);
        Checkbox("Hair color matches fur / body color", owner.HairMatchesFur);
        SliderRow("Starting weight", owner.DefaultStartingWeight, Number);
        Checkbox("Clothing discards", owner.ClothingDiscards);
        Checkbox("Extra random hair colors", owner.ExtraHairColors);
        SliderRow("Oral vore weight", owner.OralWeight, Number);
        Checkbox("Anal vore", owner.AnalVore);
        SliderRow("Anal vore weight", owner.AnalWeight, Number);
        Checkbox("Cock vore", owner.CockVore);
        SliderRow("Cock vore weight", owner.CockWeight, Number);
        Checkbox("Unbirth", owner.Unbirth);
        SliderRow("Unbirth weight", owner.UnbirthWeight, Number);
        Checkbox("Tail vore", owner.TailVore);
        SliderRow("Tail vore weight", owner.TailWeight, Number);
        Checkbox("Friendly regurgitation", owner.FriendlyRegurgitation);
        Checkbox("Allow belly rubbing enemies", owner.CanUseStomachRubOnEnemies);
        Checkbox("Animated bellies", owner.AnimatedBellies);
        Checkbox("Skull effect on digestion death", owner.DigestionSkulls);
        Checkbox("Hand effect for belly rubs", owner.BellyRubHands);
        Checkbox("Burp on digest", owner.BurpOnDigest);
        SliderRow("Burp percentage", owner.BurpFraction, Percent);
        Checkbox("Bones", owner.Bones);
        Checkbox("Milk / Cumstains", owner.Cumstains);
        Checkbox("Condoms for CV", owner.CondomsForCV);

        var speciesPage = pages[pages.Length - 1];
        var description = UITheme.Node("Species explanation", speciesPage);
        description.AddComponent<LayoutElement>().preferredHeight = 84;
        var words = UITheme.Label(description.transform,
            "Choose which approved species can be generated. Existing characters are preserved when their species is disabled.",
            20, 8, 840, 68);
        words.color = UITheme.Muted;
        foreach (var species in owner.SpeciesChoices)
            Checkbox(SpeciesPolicy.DisplayName(species.Key) + " (" + SpeciesPolicy.GetKind(species.Key) + ")", species.Value);

        StylePageSelector(owner.PageSelector, frame.transform);
        owner.TooltipText.transform.SetParent(frame.transform, false); owner.TooltipText.transform.localScale = Vector3.one;
        UITheme.Place(owner.TooltipText.rectTransform, 28, 608, 884, 76);
        UITheme.StyleText(owner.TooltipText, 19); owner.TooltipText.color = UITheme.Muted;
        owner.TooltipText.raycastTarget = false; owner.TooltipText.gameObject.SetActive(true);
        ActionButton(frame.transform, "Load saved", 28, owner.ConfirmRefresh);
        ActionButton(frame.transform, "Apply to this game", 484, owner.Exit);
        ActionButton(frame.transform, "Apply now and as global default", 720, owner.ExitAndSave);
        foreach (var child in legacy)
            if (child.parent == owner.transform) child.gameObject.SetActive(false);
        // Old panels own the retired controls/labels only; retained controls now have active new parents.
        foreach (var panel in owner.SettingsPanels) panel.SetActive(false);
        ShowPage(owner.PageSelector.value);
    }

    static string Percent(float value) { return Mathf.RoundToInt(value * 100) + "%"; }
    static string Number(float value) { return value.ToString("0.##"); }

    ControlRow Row(string title, Selectable control)
    {
        var row = UITheme.Node(title, pages[originalPages[control]]);
        row.AddComponent<LayoutElement>().preferredHeight = 68; UITheme.Surface(row, UITheme.Row, true);
        var binding = new ControlRow { Control = control, Label = UITheme.Label(row.transform, title, 20, 8, 390, 52) };
        // Take only this control's help, not a nested slider's tooltip.
        var tip = control.GetComponent<DisplayTooltip>();
        if (tip != null && tip.enabled) row.AddComponent<DisplayTooltip>().value = tip.value;
        control.transform.SetParent(row.transform, false); control.transform.localScale = Vector3.one;
        control.transform.localRotation = Quaternion.identity; control.gameObject.SetActive(true);
        foreach (var label in control.GetComponentsInChildren<Text>(true)) label.gameObject.SetActive(false);
        foreach (var label in control.GetComponentsInChildren<TMP_Text>(true)) label.gameObject.SetActive(false);
        UITheme.StyleSelectable(control); rows.Add(binding);
        return binding;
    }

    void Checkbox(string title, Toggle toggle)
    {
        var binding = Row(title, toggle); var row = binding.Label.transform.parent.gameObject;
        binding.Label.rectTransform.sizeDelta = new Vector2(770, 52);
        UITheme.Place(toggle.GetComponent<RectTransform>(), 818, 20, 28, 28);
        if (toggle.targetGraphic != null)
        {
            toggle.targetGraphic.transform.localScale = Vector3.one;
            UITheme.Stretch(toggle.targetGraphic.rectTransform);
            UITheme.Surface(toggle.targetGraphic.gameObject, UITheme.Control, true);
        }
        UITheme.StyleCheckmark(toggle);
        var button = row.AddComponent<Button>(); button.targetGraphic = row.GetComponent<Image>();
        button.transition = Selectable.Transition.None;
        var navigation = button.navigation; navigation.mode = Navigation.Mode.None; button.navigation = navigation;
        button.onClick.AddListener(() => toggle.isOn = !toggle.isOn); binding.Button = button;
    }

    void SliderRow(string title, Slider slider, Func<float, string> format)
    {
        var binding = Row(title, slider);
        UITheme.Place(slider.GetComponent<RectTransform>(), 430, 22, 328, 24);
        UITheme.StyleSlider(slider);
        binding.Value = UITheme.Label(binding.Label.transform.parent, "Value", 766, 12, 94, 44, 16);
        binding.Value.alignment = TextAnchor.MiddleRight; binding.Format = format;
    }

    static void StylePageSelector(TMP_Dropdown dropdown, Transform parent)
    {
        dropdown.transform.SetParent(parent, false); dropdown.transform.localScale = Vector3.one;
        UITheme.Place(dropdown.GetComponent<RectTransform>(), 400, 24, 532, 44);
        dropdown.targetGraphic = UITheme.Surface(dropdown.gameObject, UITheme.Control, true);
        UITheme.StyleSelectable(dropdown); dropdown.gameObject.SetActive(true);
        foreach (var text in dropdown.GetComponentsInChildren<TMP_Text>(true))
        { text.enableAutoSizing = false; text.fontSize = 17; text.color = UITheme.Foreground; }
        UITheme.Stretch(dropdown.captionText.rectTransform, 12, 34, 2, 2);
        var arrow = dropdown.transform.Find("Arrow") as RectTransform;
        if (arrow != null)
        {
            arrow.localScale = Vector3.one; arrow.anchorMin = arrow.anchorMax = arrow.pivot = new Vector2(1, .5f);
            arrow.sizeDelta = new Vector2(16, 10); arrow.anchoredPosition = new Vector2(-12, 0);
            arrow.GetComponent<Image>().color = UITheme.Accent;
        }
        var template = dropdown.template; template.localScale = Vector3.one;
        template.anchorMin = Vector2.zero; template.anchorMax = new Vector2(1, 0); template.pivot = new Vector2(.5f, 1);
        template.sizeDelta = new Vector2(0, 240); template.anchoredPosition = new Vector2(0, -4);
        UITheme.Surface(template.gameObject, UITheme.Panel, true);
        foreach (var bar in template.GetComponentsInChildren<Scrollbar>(true))
        {
            UITheme.Surface(bar.gameObject, UITheme.Background);
            if (bar.handleRect != null) bar.targetGraphic = UITheme.Surface(bar.handleRect.gameObject, UITheme.Control);
            UITheme.StyleSelectable(bar);
        }
        var item = template.GetComponentInChildren<Toggle>(true);
        if (item != null)
        {
            var rect = item.GetComponent<RectTransform>(); rect.sizeDelta = new Vector2(rect.sizeDelta.x, 38);
            UITheme.Stretch(dropdown.itemText.rectTransform, 30, 6, 0, 0);
            UITheme.StyleDropdownItem(item);
        }
    }

    static void ActionButton(Transform parent, string title, float x, Action action)
    {
        var go = UITheme.Node(title, parent); UITheme.Place(go.GetComponent<RectTransform>(), x, 700, 212, 40);
        var button = go.AddComponent<Button>(); button.targetGraphic = UITheme.Surface(go, UITheme.Control, true);
        UITheme.StyleSelectable(button); button.onClick.AddListener(() => action());
        UITheme.Label(go.transform, title, 8, 0, 196, 40).alignment = TextAnchor.MiddleCenter;
    }

    internal void ShowPage(int page)
    {
        page = Mathf.Clamp(page, 0, pages.Length - 1);
        if (selected != page)
        {
            if (selected >= 0) positions[selected] = scroll.verticalNormalizedPosition;
            scroll.StopMovement();
            for (int i = 0; i < pages.Length; i++) pages[i].gameObject.SetActive(i == page);
            selected = page; scroll.content = pages[page];
            LayoutRebuilder.ForceRebuildLayoutImmediate(pages[page]);
            scroll.verticalNormalizedPosition = positions[page];
        }
        Refresh();
    }

    internal void Refresh()
    {
        foreach (var row in rows)
        {
            row.Label.color = row.Control.interactable ? UITheme.Foreground : UITheme.Muted;
            if (row.Button != null) row.Button.interactable = row.Control.interactable;
            if (row.Value != null) row.Value.text = row.Format(((Slider)row.Control).value);
        }
    }
}
