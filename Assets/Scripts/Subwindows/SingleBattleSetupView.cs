// Added by the VoreWar mercenary-company conversion fork.
// 2026-11-02: Arrange all retained Single Battle controls and previews in a blue two-sided setup screen (spec 15D).
// 2026-11-02: Enlarge and center paper-doll previews while compensating for their renderer offset (portrait feedback).
// 2026-11-02: Correct portrait magnification/alignment after visual feedback (spec 15D).
// 2026-11-02: Hide preview species captions and center sample dolls at 2x UI scale (spec 15D feedback).
// 2026-11-02: Enlarge 2x preview cards from their lower-left corner and make room in the combatant columns (spec 15D feedback).
// 2026-11-02: Nudge sample cards down approximately15screen pixels at1080p, preserving size and centered dolls (spec 15D feedback).
// 2026-11-02: Move only the sample doll upward inside its unchanged card to clear the feet (spec 15D feedback).
// 2026-11-03: Theme dropdown item surfaces while preserving checkmark graphics (spec 28).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

internal sealed class SingleBattleSetupView
{
    sealed class Binding
    {
        internal Selectable Control;
        internal Text Label;
        internal Button RowButton;
    }
    readonly List<Binding> bindings = new List<Binding>();
    readonly CreateTacticalGame owner;
    readonly RectTransform attackers, defenders, attackerViewport, defenderViewport, watch;
    readonly Text bulkValue;
    bool? bulkVisible;

    internal SingleBattleSetupView(CreateTacticalGame setup)
    {
        owner = setup;
        var legacy = owner.transform.Cast<Transform>().ToArray();
        var create = owner.transform.Find("Create").GetComponent<Button>();
        var back = owner.transform.Find("Back").GetComponent<Button>();
        var settings = owner.transform.Find("Content Settings Button (1)").GetComponent<Button>();
        var bulkButtons = owner.BulkPanel.GetComponentsInChildren<Button>(true);
        var scaler = owner.GetComponent<CanvasScaler>();
        scaler.referenceResolution = new Vector2(1280, 800);
        scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;

        var background = UITheme.Node("Single Battle presentation", owner.transform);
        UITheme.Stretch(background.GetComponent<RectTransform>()); UITheme.Surface(background, UITheme.Background);
        var frame = UITheme.Node("Centered battle setup", background.transform);
        var rect = frame.GetComponent<RectTransform>();
        rect.anchorMin = rect.anchorMax = rect.pivot = new Vector2(.5f, .5f);
        rect.sizeDelta = new Vector2(1240, 760); rect.anchoredPosition = Vector2.zero;
        UITheme.Surface(frame, UITheme.Panel, true);
        UITheme.Label(frame.transform, "Single Battle", 20, 8, 1200, 34, 26).alignment = TextAnchor.MiddleCenter;

        Button(create, frame.transform, 262, 48, 228, 38);
        Button(back, frame.transform, 506, 48, 228, 38);
        Button(settings, frame.transform, 750, 48, 228, 38);
        var x = Row(frame.transform, "Tactical Size X", 44, 98, 250, 40);
        Input(owner.SizeX, x, 156, 4, 84, 32);
        var y = Row(frame.transform, "Tactical Size Y", 310, 98, 250, 40);
        Input(owner.SizeY, y, 156, 4, 84, 32);
        var scale = Row(frame.transform, "Auto-Scale", 576, 98, 202, 40);
        Checkbox(owner.AutoScale, scale, 162, 7);
        var terrain = Row(frame.transform, "Field Type", 794, 98, 402, 40);
        Choice(owner.TerrainType, terrain, 116, 2, 276, 36);

        Divider(frame.transform, 20); Divider(frame.transform, 636);
        attackers = Side(frame.transform, "Attackers", owner.Attacker, owner.AttackerSprite, 20, out attackerViewport);
        defenders = Side(frame.transform, "Defenders", owner.Defender, owner.DefenderSprite, 636, out defenderViewport);

        var watchRow = Row(frame.transform, "AI vs AI battles", 20, 616, 1200, 40);
        watch = watchRow.GetComponent<RectTransform>();
        Choice(owner.AIOverride, watchRow, 260, 2, 920, 36);

        Move(owner.BulkPanel.transform, frame.transform, 20, 584, 1200, 76);
        UITheme.Surface(owner.BulkPanel, UITheme.Row, true);
        // Preserve the original panel's activation; WatchOptionChanged still owns it.
        var bulkRow = Row(owner.BulkPanel.transform, "Runs per matchup", 8, 0, 626, 34);
        Move(owner.Bulk.transform, bulkRow.transform, 222, 6, 292, 24);
        HideLabels(owner.Bulk); UITheme.StyleSlider(owner.Bulk); Bind(owner.Bulk, bulkRow);
        bulkValue = UITheme.Label(bulkRow.transform, "", 524, 0, 90, 34, 18);
        bulkValue.alignment = TextAnchor.MiddleRight;
        var animals = Row(owner.BulkPanel.transform, "Include animals", 660, 0, 520, 34);
        Checkbox(owner.AllRaces, animals, 480, 4);
        // Keep all three original bulk actions, including their long existing caption.
        Button(bulkButtons.First(b => b.name == "Current Pair Matches"), owner.BulkPanel.transform, 8, 40, 260, 34);
        Button(bulkButtons.First(b => b.name == "All Matches"), owner.BulkPanel.transform, 280, 40, 260, 34);
        Button(bulkButtons.First(b => b.name == "Current Attacker Matches"), owner.BulkPanel.transform, 552, 40, 640, 34);

        Move(owner.TooltipText.transform, frame.transform, 20, 672, 1200, 68);
        UITheme.StyleText(owner.TooltipText, 19); owner.TooltipText.color = UITheme.Muted;
        owner.TooltipText.raycastTarget = false; owner.TooltipText.gameObject.SetActive(true);
        // Only empty legacy containers/labels are hidden after controls and previews move out.
        foreach (var child in legacy)
            if (child.parent == owner.transform) child.gameObject.SetActive(false);
        Refresh();
    }

    static void Move(Transform control, Transform parent, float x, float y, float width, float height)
    {
        control.SetParent(parent, false); control.localScale = Vector3.one; control.localRotation = Quaternion.identity;
        UITheme.Place((RectTransform)control, x, y, width, height);
    }
    static GameObject Row(Transform parent, string label, float x, float y, float width, float height)
    {
        var row = UITheme.Node(label, parent); UITheme.Place(row.GetComponent<RectTransform>(), x, y, width, height);
        UITheme.Surface(row, UITheme.Row);
        UITheme.Label(row.transform, label, 12, 0, 220, height, 18);
        return row;
    }
    static void Divider(Transform parent, float x)
    {
        var line = UITheme.Node("Core options divider", parent);
        UITheme.Place(line.GetComponent<RectTransform>(), x, 150, 584, 1);
        UITheme.Surface(line, UITheme.Border).raycastTarget = false;
    }
    static void HideLabels(Selectable control)
    {
        foreach (var text in control.GetComponentsInChildren<Text>(true)) text.gameObject.SetActive(false);
    }
    void Bind(Selectable control, GameObject row, Button rowButton = null)
    {
        UITheme.StyleSelectable(control);
        var tip = control.GetComponentsInChildren<DisplayTooltip>(true).FirstOrDefault(t => t.enabled);
        if (tip != null) row.AddComponent<DisplayTooltip>().value = tip.value;
        bindings.Add(new Binding { Control = control, Label = row.GetComponentInChildren<Text>(), RowButton = rowButton });
    }
    void Input(InputField input, GameObject row, float x, float y, float width, float height)
    {
        Move(input.transform, row.transform, x, y, width, height); input.gameObject.SetActive(true);
        input.targetGraphic = UITheme.Surface(input.gameObject, UITheme.Control, true);
        foreach (var text in input.GetComponentsInChildren<Text>(true))
        {
            if (text != input.textComponent && text != input.placeholder) text.gameObject.SetActive(false);
            else
            {
                text.transform.localScale = Vector3.one; UITheme.StyleText(text, 18);
                UITheme.Stretch(text.rectTransform, 8, 8, 0, 0);
                if (text == input.placeholder) text.color = UITheme.Muted;
            }
        }
        Bind(input, row);
    }
    void Checkbox(Toggle toggle, GameObject row, float x, float y)
    {
        var label = row.GetComponentInChildren<Text>(); label.rectTransform.sizeDelta = new Vector2(x - 20, label.rectTransform.sizeDelta.y);
        Move(toggle.transform, row.transform, x, y, 26, 26); toggle.gameObject.SetActive(true); HideLabels(toggle);
        if (toggle.targetGraphic != null)
        {
            toggle.targetGraphic.transform.localScale = Vector3.one;
            UITheme.Stretch(toggle.targetGraphic.rectTransform); UITheme.Surface(toggle.targetGraphic.gameObject, UITheme.Control, true);
        }
        UITheme.StyleCheckmark(toggle);
        var button = row.AddComponent<Button>(); button.targetGraphic = row.GetComponent<Image>(); button.transition = Selectable.Transition.None;
        var navigation = button.navigation; navigation.mode = Navigation.Mode.None; button.navigation = navigation;
        button.onClick.AddListener(() => toggle.isOn = !toggle.isOn); Bind(toggle, row, button);
    }
    void Choice(Dropdown dropdown, GameObject row, float x, float y, float width, float height)
    {
        Move(dropdown.transform, row.transform, x, y, width, height); dropdown.gameObject.SetActive(true);
        dropdown.targetGraphic = UITheme.Surface(dropdown.gameObject, UITheme.Control, true);
        foreach (var text in dropdown.GetComponentsInChildren<Text>(true))
        {
            UITheme.StyleText(text, 18);
            if (text != dropdown.captionText && text != dropdown.itemText && !text.transform.IsChildOf(dropdown.template)) text.gameObject.SetActive(false);
        }
        dropdown.captionText.transform.localScale = Vector3.one;
        UITheme.Stretch(dropdown.captionText.rectTransform, 10, 30, 0, 0);
        var arrow = dropdown.transform.Find("Arrow") as RectTransform;
        if (arrow != null)
        {
            arrow.localScale = Vector3.one; arrow.anchorMin = arrow.anchorMax = arrow.pivot = new Vector2(1, .5f);
            arrow.sizeDelta = new Vector2(14, 9); arrow.anchoredPosition = new Vector2(-12, 0);
            arrow.GetComponent<Image>().color = UITheme.Accent;
        }
        var template = dropdown.template; template.localScale = Vector3.one;
        template.anchorMin = Vector2.zero; template.anchorMax = new Vector2(1, 0); template.pivot = new Vector2(.5f, 1);
        template.sizeDelta = new Vector2(0, Mathf.Min(240, dropdown.options.Count * 36 + 6)); template.anchoredPosition = new Vector2(0, -3);
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
            var rect = item.GetComponent<RectTransform>(); rect.sizeDelta = new Vector2(rect.sizeDelta.x, 36);
            UITheme.Stretch(dropdown.itemText.rectTransform, 30, 6, 0, 0);
            UITheme.StyleDropdownItem(item);
        }
        Bind(dropdown, row);
    }
    void Slider(Slider slider, Text value, GameObject row)
    {
        Move(slider.transform, row.transform, 248, 10, 210, 24); slider.gameObject.SetActive(true);
        HideLabels(slider); UITheme.StyleSlider(slider);
        Move(value.transform, row.transform, 468, 0, 72, 44); value.gameObject.SetActive(true);
        UITheme.StyleText(value, 18); value.alignment = TextAnchor.MiddleRight; value.raycastTarget = false;
        Bind(slider, row);
    }
    static void Button(Button button, Transform parent, float x, float y, float width, float height)
    {
        Move(button.transform, parent, x, y, width, height); button.gameObject.SetActive(true);
        button.targetGraphic = UITheme.Surface(button.gameObject, UITheme.Control, true); UITheme.StyleSelectable(button);
        foreach (var label in button.GetComponentsInChildren<Text>(true))
        {
            label.transform.localScale = Vector3.one; UITheme.StyleText(label, 18); label.alignment = TextAnchor.MiddleCenter;
            UITheme.Stretch(label.rectTransform, 8, 8, 0, 0);
        }
    }
    RectTransform Side(Transform parent, string title, StartTacticalUI side, UIUnitSprite sprite, float x, out RectTransform viewport)
    {
        var panel = UITheme.Node(title, parent); var rect = panel.GetComponent<RectTransform>();
        UITheme.Place(rect, x, 162, 596, 440); UITheme.Surface(panel, UITheme.Background, true);
        UITheme.Label(panel.transform, title, 16, 4, 552, 32, 24);
        var race = Row(panel.transform, "Race", 12, 48, 374, 44);
        Choice(side.Race, race, 100, 3, 262, 38);
        var enabled = Row(panel.transform, "AI Enabled", 12, 106, 374, 44);
        Checkbox(side.AIPlayer, enabled, 334, 9);

        var portrait = UITheme.Node("Species preview", panel.transform);
        UITheme.Place(portrait.GetComponent<RectTransform>(), 408, 3, 180, 180); UITheme.Surface(portrait, UITheme.Panel, true);
        portrait.AddComponent<RectMask2D>();
        Move(sprite.transform, portrait.transform, 90, 139, 0, 0); sprite.transform.localScale = new Vector3(2, 2, 1);
        // Keep the label reference for the existing preview callback, but don't display it.
        sprite.Name.gameObject.SetActive(false);

        var viewportObject = UITheme.Node("Combatant options viewport", panel.transform); viewport = viewportObject.GetComponent<RectTransform>();
        UITheme.Place(viewport, 12, 196, 544, 232); UITheme.Surface(viewportObject, UITheme.Background); viewportObject.AddComponent<RectMask2D>();
        var scroll = viewportObject.AddComponent<ScrollRect>(); scroll.viewport = viewport; scroll.horizontal = false;
        scroll.movementType = ScrollRect.MovementType.Clamped; scroll.scrollSensitivity = 36;
        var content = UITheme.Node("Combatant options", viewport); var contentRect = content.GetComponent<RectTransform>();
        contentRect.anchorMin = new Vector2(0, 1); contentRect.anchorMax = Vector2.one; contentRect.pivot = new Vector2(.5f, 1);
        contentRect.sizeDelta = new Vector2(0, 432); contentRect.anchoredPosition = Vector2.zero; scroll.content = contentRect;
        var bar = UITheme.Node("Options scrollbar", panel.transform);
        var barRect = bar.GetComponent<RectTransform>(); barRect.anchorMin = new Vector2(1, 0); barRect.anchorMax = Vector2.one; barRect.pivot = new Vector2(1, .5f);
        barRect.offsetMin = new Vector2(-20, 12); barRect.offsetMax = new Vector2(-8, -196);
        UITheme.Surface(bar, UITheme.Background);
        var handle = UITheme.Node("Handle", bar.transform); UITheme.Stretch(handle.GetComponent<RectTransform>());
        var scrollbar = bar.AddComponent<Scrollbar>(); scrollbar.direction = Scrollbar.Direction.BottomToTop;
        scrollbar.handleRect = handle.GetComponent<RectTransform>(); scrollbar.targetGraphic = UITheme.Surface(handle, UITheme.Control);
        UITheme.StyleSelectable(scrollbar); scroll.verticalScrollbar = scrollbar;

        Choice(side.TacticalAI, Row(content.transform, "AI Type", 0, 0, 544, 44), 248, 3, 284, 38);
        Input(side.UnitCount, Row(content.transform, "Units", 0, 48, 544, 44), 248, 3, 284, 38);
        Input(side.Level, Row(content.transform, "Level", 0, 96, 544, 44), 248, 3, 284, 38);
        Checkbox(side.CanVore, Row(content.transform, "Can Vore", 0, 144, 544, 44), 506, 9);
        Checkbox(side.HasLeader, Row(content.transform, "Has Leader", 0, 192, 544, 44), 506, 9);
        Slider(side.RangedPercentage, side.RangedText, Row(content.transform, "% Ranged", 0, 240, 544, 44));
        Slider(side.HeavyWeaponsPercentage, side.WeaponsText, Row(content.transform, "% Heavy Weapons", 0, 288, 544, 44));
        Slider(side.MagicPercentage, side.MagicText, Row(content.transform, "% Spell Books", 0, 336, 544, 44));
        Input(side.MaxSpellLevel, Row(content.transform, "Max Book Level", 0, 384, 544, 44), 248, 3, 284, 38);
        scroll.verticalNormalizedPosition = 1;
        return rect;
    }
    internal void Refresh()
    {
        bool visible = owner.BulkPanel.activeSelf;
        if (bulkVisible != visible)
        {
            bulkVisible = visible; float height = visible ? 360 : 440;
            attackers.sizeDelta = defenders.sizeDelta = new Vector2(596, height);
            attackerViewport.sizeDelta = defenderViewport.sizeDelta = new Vector2(544, height - 208);
            UITheme.Place(watch, 20, visible ? 536 : 616, 1200, 40);
        }
        bulkValue.text = owner.Bulk.value.ToString("0");
        owner.AttackerSprite.Name.gameObject.SetActive(false);
        owner.DefenderSprite.Name.gameObject.SetActive(false);
        foreach (var binding in bindings)
        {
            binding.Label.color = binding.Control.interactable ? UITheme.Foreground : UITheme.Muted;
            if (binding.RowButton != null) binding.RowButton.interactable = binding.Control.interactable;
        }
    }
}
