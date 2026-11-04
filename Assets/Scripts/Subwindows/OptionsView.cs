// Added by the VoreWar mercenary-company conversion fork.
// 2026-11-02: Center and categorize normal Options using retained controls and a compact blue scroll layout (spec 15A).
// 2026-11-02: Align slider track/fill/handle geometry, enlarge hover text and restore preferred vore terminology (spec 15A feedback).
// 2026-11-02: Use shared bright checkmarks and slider styling for the Content Settings milestone (spec 15B).
// 2026-11-03: Theme option-list item surfaces through the shared blue/gold treatment (spec 28).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

internal sealed class OptionsView
{
    sealed class SliderValue
    {
        internal Slider Control;
        internal Text Text;
        internal Func<float, string> Format;
    }
    sealed class ControlRow
    {
        internal Selectable Control;
        internal Text Label;
        internal Button RowButton;
    }
    readonly List<SliderValue> values = new List<SliderValue>();
    readonly List<ControlRow> rows = new List<ControlRow>();
    readonly RectTransform content;
    readonly GameObject campaignSection, campaignRow;
    readonly Text campaignLabel;
    Toggle campaignControl;
    int refreshedFrame = -1;

    internal OptionsView(Options owner)
    {
        var legacy = owner.transform.Cast<Transform>().ToArray();
        var scaler = owner.GetComponent<CanvasScaler>();
        scaler.referenceResolution = new Vector2(1280, 800);
        scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
        var background = UITheme.Node("Options presentation", owner.transform);
        UITheme.Stretch(background.GetComponent<RectTransform>()); UITheme.Surface(background, UITheme.Background);
        var frame = UITheme.Node("Centered options column", background.transform);
        var rect = frame.GetComponent<RectTransform>(); rect.anchorMin = rect.anchorMax = rect.pivot = new Vector2(.5f, .5f);
        rect.sizeDelta = new Vector2(960, 760); rect.anchoredPosition = Vector2.zero;
        UITheme.Surface(frame, UITheme.Panel, true);
        UITheme.Label(frame.transform, "Options", 28, 18, 850, 48, 28);
        var viewport = UITheme.Node("Options viewport", frame.transform);
        UITheme.Place(viewport.GetComponent<RectTransform>(), 28, 92, 884, 536);
        UITheme.Surface(viewport, UITheme.Background); viewport.AddComponent<RectMask2D>();
        var scroll = viewport.AddComponent<ScrollRect>(); scroll.horizontal = false;
        scroll.movementType = ScrollRect.MovementType.Clamped; scroll.scrollSensitivity = 36;
        scroll.viewport = viewport.GetComponent<RectTransform>();
        var column = UITheme.Node("Option categories", viewport.transform); content = column.GetComponent<RectTransform>();
        content.anchorMin = new Vector2(0, 1); content.anchorMax = Vector2.one; content.pivot = new Vector2(.5f, 1);
        content.sizeDelta = Vector2.zero; content.anchoredPosition = Vector2.zero;
        var layout = column.AddComponent<VerticalLayoutGroup>();
        layout.spacing = 12; layout.padding = new RectOffset(2, 2, 2, 12);
        layout.childControlWidth = layout.childControlHeight = true; layout.childForceExpandHeight = false;
        column.AddComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        scroll.content = content;
        var scrollbarObject = UITheme.Node("Options scrollbar", frame.transform);
        UITheme.Place(scrollbarObject.GetComponent<RectTransform>(), 924, 92, 12, 536);
        UITheme.Surface(scrollbarObject, UITheme.Background, true);
        var handle = UITheme.Node("Handle", scrollbarObject.transform); UITheme.Stretch(handle.GetComponent<RectTransform>());
        var handleImage = UITheme.Surface(handle, UITheme.Control);
        var scrollbar = scrollbarObject.AddComponent<Scrollbar>(); scrollbar.direction = Scrollbar.Direction.BottomToTop;
        scrollbar.handleRect = handle.GetComponent<RectTransform>(); scrollbar.targetGraphic = handleImage;
        UITheme.StyleSelectable(scrollbar); scroll.verticalScrollbar = scrollbar;

        var delays = Section("Move Delays");
        Delay(delays, "Player movement", owner.TacticalPlayerMovementDelaySlider);
        Delay(delays, "Friendly AI movement", owner.TacticalFriendlyAIMovementDelay);
        Delay(delays, "Enemy AI movement", owner.TacticalAIMovementDelaySlider);
        Delay(delays, "AI attacks", owner.TacticalAttackDelaySlider);
        Delay(delays, "AI vore actions", owner.TacticalVoreDelaySlider);

        var audio = Section("Audio");
        Checkbox(audio, "Sound enabled", owner.SoundEnabled);
        SliderRow(audio, "Combat volume", owner.CombatSoundVolume, Percent);
        SliderRow(audio, "Vore volume", owner.VoreSoundVolume, Percent);
        Checkbox(audio, "Continuous digestion sounds", owner.CloseInDigestionNoises);
        SliderRow(audio, "Digestion volume", owner.PassiveVoreSoundVolume, Percent);

        var graphics = Section("Graphics");
        Checkbox(graphics, "Damage numbers", owner.DamageNumbers);
        Checkbox(graphics, "Show unit levels", owner.ShowLevelText);
        Checkbox(graphics, "Green borders for allies", owner.AltFriendlyColor);
        Choice(graphics, "Unit side markers", owner.AllianceSquares);
        Checkbox(graphics, "Extra tactical tooltip information", owner.ExtraTacticalInfo);
        Checkbox(graphics, "Simple field graphics", owner.SimpleFarms);
        Checkbox(graphics, "Simple forest graphics", owner.SimpleForests);
        Checkbox(graphics, "Hard volcanic / lava edges", owner.HardLava);
        Checkbox(graphics, "Desaturate snow and sand", owner.DesaturatedTiles);
        SliderRow(graphics, "Unit banner size", owner.BannerScale, value => (.5f + value / 10).ToString("0.0") + "×");
        Checkbox(graphics, "Hide unit preview picture", owner.HideUnitViewer);
        Checkbox(graphics, "Hide base stats", owner.HideBaseStats);

        var controls = Section("Controls");
        Choice(controls, "When only one side can move", owner.AutoAdvance);
        Choice(controls, "Tactical camera tracking", owner.TacticalMoveCamera);
        Choice(controls, "Strategic camera tracking", owner.StrategicMoveCamera);
        Checkbox(controls, "Right click opens the tile menu", owner.RightClickMenu);
        Checkbox(controls, "Camera edge scrolling", owner.EdgeScrolling);
        Checkbox(controls, "Confirm ending a turn with unused actions", owner.PromptEndTurn);
        Checkbox(controls, "Scroll to starting battle", owner.ScrollToBattleLocation);
        Checkbox(controls, "End-of-battle review", owner.StopAtEndOfBattle);
        Checkbox(controls, "AI always finishes tactical turns", owner.AutoUseAI);
        Checkbox(controls, "Run while the game is not in focus", owner.RunInBackground);

        campaignSection = Section("Campaign");
        Text label;
        campaignRow = Row(campaignSection, "Leader revival", 68, out label); campaignLabel = label;
        campaignSection.SetActive(false);
        var maintenance = Section("Maintenance");
        ActionRow(maintenance, "Restore shipped namelists", "Reset namelists", owner.ResetNamelists);
        ActionRow(maintenance, "Reload edited namelists", "Reload namelists", owner.UpdateNamelists);
        ActionRow(maintenance, "Restore user data", "Reset / wipe user data", owner.ResetUserdata);
        ActionRow(maintenance, "Clear saved preferences", "Reset Options and content settings", owner.AskClearAllSettings);

        owner.TooltipText.transform.SetParent(frame.transform, false); owner.TooltipText.transform.localScale = Vector3.one;
        UITheme.Place(owner.TooltipText.rectTransform, 28, 644, 680, 94); UITheme.StyleText(owner.TooltipText, 19);
        owner.TooltipText.color = UITheme.Muted; owner.TooltipText.raycastTarget = false;
        owner.TooltipText.gameObject.SetActive(true);
        Button(frame.transform, "Save and close", 724, 692, 208, 44, owner.CloseAndSave);
        // Serialized references still own their controls/values; only the old presentation is retired.
        foreach (var child in legacy) if (child.parent == owner.transform) child.gameObject.SetActive(false);
        owner.StrategicDelaySlider.gameObject.SetActive(false); owner.Notifications.gameObject.SetActive(false);
        owner.IgnoreMonsterBattles.gameObject.SetActive(false); owner.WatchAIBattles.gameObject.SetActive(false);
        owner.SoundEnabledChanged();
        scroll.verticalNormalizedPosition = 1;
    }
    static string Percent(float value) { return Mathf.RoundToInt(value * 100) + "%"; }
    GameObject Section(string title)
    {
        var section = UITheme.Node(title, content);
        var layout = section.AddComponent<VerticalLayoutGroup>(); layout.spacing = 3;
        layout.childControlHeight = layout.childControlWidth = true; layout.childForceExpandHeight = false;
        var header = UITheme.Node(title + " heading", section.transform);
        header.AddComponent<LayoutElement>().preferredHeight = 42; UITheme.Surface(header, UITheme.Section);
        UITheme.Label(header.transform, title, 18, 0, 820, 42, 21);
        return section;
    }
    static GameObject Row(GameObject section, string title, float height, out Text label)
    {
        var row = UITheme.Node(title, section.transform); row.AddComponent<LayoutElement>().preferredHeight = height;
        UITheme.Surface(row, UITheme.Row, true);
        label = UITheme.Label(row.transform, title, 20, 8, 390, height - 16);
        return row;
    }
    void Bind(Selectable control, Text label, GameObject row, Button rowButton = null)
    {
        UITheme.StyleSelectable(control);
        var tooltip = control.GetComponentsInChildren<DisplayTooltip>(true).FirstOrDefault();
        if (tooltip != null) row.AddComponent<DisplayTooltip>().value = tooltip.value;
        rows.Add(new ControlRow { Control = control, Label = label, RowButton = rowButton });
    }
    void Delay(GameObject section, string title, Slider slider)
    { SliderRow(section, title, slider, value => value.ToString("0.###") + " sec"); }
    void SliderRow(GameObject section, string title, Slider slider, Func<float, string> format)
    {
        Text label; var row = Row(section, title, 72, out label);
        slider.transform.SetParent(row.transform, false); slider.transform.localScale = Vector3.one; slider.gameObject.SetActive(true);
        UITheme.Place(slider.GetComponent<RectTransform>(), 430, 24, 328, 24);
        foreach (var oldLabel in slider.GetComponentsInChildren<Text>(true)) oldLabel.gameObject.SetActive(false);
        UITheme.StyleSlider(slider);
        var valueLabel = UITheme.Label(row.transform, "", 766, 14, 94, 44, 16); valueLabel.alignment = TextAnchor.MiddleRight;
        values.Add(new SliderValue { Control = slider, Text = valueLabel, Format = format });
        slider.onValueChanged.AddListener(_ => Refresh(true));
        Bind(slider, label, row);
    }
    void Checkbox(GameObject section, string title, Toggle toggle)
    {
        Text label; var row = Row(section, title, 68, out label);
        AttachCheckbox(row, label, toggle);
    }
    void AttachCheckbox(GameObject row, Text label, Toggle toggle)
    {
        label.rectTransform.sizeDelta = new Vector2(770, 52);
        toggle.transform.SetParent(row.transform, false); toggle.transform.localScale = Vector3.one; toggle.gameObject.SetActive(true);
        UITheme.Place(toggle.GetComponent<RectTransform>(), 818, 20, 28, 28);
        foreach (var oldLabel in toggle.GetComponentsInChildren<Text>(true)) oldLabel.gameObject.SetActive(false);
        if (toggle.targetGraphic != null)
        {
            UITheme.Stretch(toggle.targetGraphic.rectTransform);
            UITheme.Surface(toggle.targetGraphic.gameObject, UITheme.Control, true);
        }
        UITheme.StyleCheckmark(toggle);
        var rowButton = row.AddComponent<Button>(); rowButton.targetGraphic = row.GetComponent<Image>(); rowButton.transition = Selectable.Transition.None;
        var navigation = rowButton.navigation; navigation.mode = Navigation.Mode.None; rowButton.navigation = navigation;
        rowButton.onClick.AddListener(() => toggle.isOn = !toggle.isOn);
        toggle.onValueChanged.AddListener(_ => Refresh(true));
        Bind(toggle, label, row, rowButton);
    }
    void Choice(GameObject section, string title, Dropdown dropdown)
    {
        Text label; var row = Row(section, title, 78, out label);
        dropdown.transform.SetParent(row.transform, false); dropdown.transform.localScale = Vector3.one; dropdown.gameObject.SetActive(true);
        UITheme.Place(dropdown.GetComponent<RectTransform>(), 430, 16, 434, 46);
        dropdown.targetGraphic = UITheme.Surface(dropdown.gameObject, UITheme.Control, true);
        foreach (var text in dropdown.GetComponentsInChildren<Text>(true))
        {
            UITheme.StyleText(text);
            if (text != dropdown.captionText && text != dropdown.itemText && !text.transform.IsChildOf(dropdown.template)) text.gameObject.SetActive(false);
        }
        UITheme.Stretch(dropdown.captionText.rectTransform, 12, 34, 2, 2);
        var arrow = dropdown.transform.Find("Arrow") as RectTransform;
        if (arrow != null)
        {
            arrow.anchorMin = arrow.anchorMax = arrow.pivot = new Vector2(1, .5f); arrow.sizeDelta = new Vector2(16, 10);
            arrow.anchoredPosition = new Vector2(-12, 0); arrow.GetComponent<Image>().color = UITheme.Accent;
        }
        var template = dropdown.template;
        template.anchorMin = Vector2.zero; template.anchorMax = new Vector2(1, 0); template.pivot = new Vector2(.5f, 1);
        template.sizeDelta = new Vector2(0, Mathf.Min(240, dropdown.options.Count * 38 + 6)); template.anchoredPosition = new Vector2(0, -4);
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
            var itemRect = item.GetComponent<RectTransform>(); itemRect.sizeDelta = new Vector2(itemRect.sizeDelta.x, 38);
            UITheme.Stretch(dropdown.itemText.rectTransform, 26, 6, 0, 0);
            UITheme.StyleDropdownItem(item);
        }
        Bind(dropdown, label, row);
    }
    static void Button(Transform parent, string title, float x, float y, float width, float height, Action action)
    {
        var go = UITheme.Node(title, parent); UITheme.Place(go.GetComponent<RectTransform>(), x, y, width, height);
        var image = UITheme.Surface(go, UITheme.Control, true); var button = go.AddComponent<Button>(); button.targetGraphic = image;
        UITheme.StyleSelectable(button); button.onClick.AddListener(() => action());
        UITheme.Label(go.transform, title, 8, 0, width - 16, height).alignment = TextAnchor.MiddleCenter;
    }
    static void ActionRow(GameObject section, string labelText, string buttonText, Action action)
    {
        Text label; var row = Row(section, labelText, 68, out label);
        Button(row.transform, buttonText, 430, 14, 434, 40, action);
    }
    internal void Show(Toggle revival, bool available)
    {
        if (revival != null && campaignControl != revival)
        {
            campaignControl = revival; AttachCheckbox(campaignRow, campaignLabel, revival);
        }
        campaignSection.SetActive(available);
        if (campaignControl != null) campaignControl.gameObject.SetActive(available);
        LayoutRebuilder.ForceRebuildLayoutImmediate(content);
        Refresh(true);
    }
    internal void Refresh(bool force = false)
    {
        if (!force && refreshedFrame == Time.frameCount) return;
        refreshedFrame = Time.frameCount;
        foreach (var value in values) value.Text.text = value.Format(value.Control.value);
        foreach (var row in rows)
        {
            row.Label.color = row.Control.interactable ? UITheme.Foreground : UITheme.Muted;
            if (row.RowButton != null) row.RowButton.interactable = row.Control.interactable;
        }
    }
}
