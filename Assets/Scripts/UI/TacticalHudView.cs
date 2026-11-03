// Added by the VoreWar mercenary-company conversion fork.
// 2026-11-02: Apply blue tactical chrome and four weapon/spell readouts without resizing core commands/stats (spec 16).
// 2026-11-02: Restore boxed/blank equipment slots and space primary, utility and AI controls (spec 16 feedback).
// 2026-11-02: Match selector pivot to command buttons and clear stats above equipment boxes (spec 16 feedback).
// 2026-11-03: Restore HP fill, brighten log checkmarks and adjust equipment clearance (spec 16 feedback).
// 2026-11-03: Force solid HP rendering and cover one-pixel tactical screen-edge seams (spec 16 feedback).
// 2026-11-03: Preserve transparent status-bar backgrounds so the barrier overlay cannot hide HP (spec 16 feedback).
// 2026-11-03: Theme the retained auto-resolve options popup (spec 17).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal sealed class TacticalHudView
{
    static readonly Dictionary<UnitInfoPanel, TextMeshProUGUI[]> equipment = new Dictionary<UnitInfoPanel, TextMeshProUGUI[]>();

    internal TacticalHudView(TacticalMode mode)
    {
        foreach (var image in mode.UI.GetComponentsInChildren<Image>(true))
        {
            // Barrier shares the HP rectangle; its transparent background must stay transparent.
            if (image.GetComponentInParent<Slider>() != null) continue;
            string name = image.name.ToLowerInvariant();
            if (name.Contains("panel") || name == "background" || name == "viewport" || name == "scroll view" || name == "stats block")
                image.color = UITheme.Panel;
        }
        foreach (var control in mode.UI.GetComponentsInChildren<Selectable>(true))
        {
            if (control is Slider) continue;
            UITheme.StyleSelectable(control);
            var image = control.targetGraphic as Image;
            if (image != null && !(control is Slider)) UITheme.Surface(image.gameObject, UITheme.Control, true);
            var toggle = control as Toggle;
            if (toggle != null && toggle.graphic != null)
            {
                var originalMark = toggle.graphic.rectTransform;
                UITheme.StyleCheckmark(toggle);
                // Preserve each legacy checkbox position while using the bright vector mark.
                toggle.graphic.transform.SetParent(originalMark.parent, false);
                var mark = toggle.graphic.rectTransform;
                mark.anchorMin = originalMark.anchorMin; mark.anchorMax = originalMark.anchorMax;
                mark.pivot = originalMark.pivot; mark.anchoredPosition = originalMark.anchoredPosition;
                mark.sizeDelta = originalMark.sizeDelta;
            }
        }
        foreach (var text in mode.UI.GetComponentsInChildren<Text>(true)) text.color = UITheme.Foreground;
        foreach (var text in mode.UI.GetComponentsInChildren<TMP_Text>(true)) text.color = UITheme.Foreground;
        mode.StatusUI.AttackerText.gameObject.SetActive(false);
        mode.StatusUI.DefenderText.gameObject.SetActive(false);
        mode.StatusUI.MeleeButton.GetComponentInChildren<Text>(true).text = "Attack";
        mode.StatusUI.RangedButton.gameObject.SetActive(false);
        // ActionMode places the selector at the selected button transform origin.
        var selection = mode.StatusUI.ButtonSelection.GetComponent<RectTransform>();
        selection.pivot = new Vector2(0, 1);
        selection.sizeDelta = new Vector2(120, 50);
        Button aiFinish = null;
        foreach (var text in mode.UI.GetComponentsInChildren<Text>(true))
        {
            if (text.text == "Skip Rest of Battle") text.text = "Auto-resolve Battle";
            if (text.text == "Let AI Finish Your Turn")
            {
                text.text = "AI: Finish Turn";
                aiFinish = text.GetComponentInParent<Button>();
            }
        }
        // Keep the established command fonts, but separate the three control groups.
        PlaceButton(mode.StatusUI.MeleeButton, 660, 25, 120, 50);
        PlaceButton(mode.StatusUI.MovementButton, 792, 25, 120, 50);
        PlaceButton(mode.StatusUI.VoreButton, 924, 25, 120, 50);
        PlaceButton(mode.StatusUI.EndTurn, 1056, 25, 160, 50);
        PlaceButton(mode.StatusUI.ZeroAPButton, 1248, 6, 140, 40);
        PlaceButton(mode.StatusUI.UndoMovement, 1248, 54, 140, 40);
        PlaceButton(mode.StatusUI.SkipToEndButton, 1432, 6, 280, 40);
        if (aiFinish != null) PlaceButton(aiFinish, 1432, 54, 280, 40);
        PlaceButton(mode.LogUI.Restore, 1744, 6, 140, 40);
        UITheme.Surface(mode.LogUI.gameObject, UITheme.Panel, true);

        UITheme.StylePopup(mode.SkipUI.gameObject);
        var info = mode.InfoUI;
        var healthFill = info.HealthBar.fillRect.GetComponent<Image>();
        healthFill.gameObject.SetActive(true);
        healthFill.sprite = null;
        healthFill.overrideSprite = null;
        healthFill.type = Image.Type.Simple;
        healthFill.fillCenter = true;
        healthFill.color = new Color32(190, 35, 45, 255);
        healthFill.canvasRenderer.SetAlpha(1);
        info.HealthBar.transition = Selectable.Transition.None;
        info.GetComponent<RectTransform>().anchoredPosition += Vector2.left;
        mode.CommandsUI.GetComponent<RectTransform>().anchoredPosition += Vector2.right;
        var bottom = mode.StatusUI.GetComponent<RectTransform>();
        bottom.sizeDelta += new Vector2(2, 0);
        var row = info.StatBlock.transform.GetChild(5);
        // Keep the old slot objects/references for existing generic hover code.
        row.gameObject.SetActive(false);
        var labels = new TextMeshProUGUI[4];
        string[] headings = { "Melee", "Ranged", "Spell 1", "Spell 2" };
        for (int i = 0; i < labels.Length; i++)
        {
            float x = 5 + (i % 2) * 118;
            float y = 318 + (i / 2) * 56;
            UITheme.Label(info.transform, headings[i], x, y, 112, 18, 14);
            var box = UITheme.Node("Tactical equipment box " + i, info.transform);
            UITheme.Place(box.GetComponent<RectTransform>(), x, y + 19, 112, 30);
            UITheme.Surface(box, UITheme.Control, true);
            var go = UITheme.Node("Item name", box.transform);
            UITheme.Stretch(go.GetComponent<RectTransform>(), 4, 4, 2, 2);
            var text = go.AddComponent<TextMeshProUGUI>();
            text.font = info.InfoText.font;
            text.fontSize = 16; text.enableAutoSizing = true;
            text.fontSizeMin = 12; text.fontSizeMax = 16; text.enableWordWrapping = false;
            text.color = UITheme.Foreground; text.alignment = TextAlignmentOptions.Midline;
            labels[i] = text;
        }
        equipment[info] = labels; info.TacticalEquipmentLabels = labels;
        // Two rows of labelled boxes replace the old single equipment row.
        var scroll = info.InfoText.transform.parent.parent as RectTransform;
        if (scroll != null) scroll.offsetMax -= new Vector2(0, 112);
    }

    static void PlaceButton(Button button, float x, float y, float width, float height)
    {
        var rect = button.GetComponent<RectTransform>();
        rect.localScale = Vector3.one;
        UITheme.Place(rect, x, y, width, height);
    }

    internal static void StyleButton(Button button, Color? tint = null)
    {
        UITheme.StyleSelectable(button);
        UITheme.Surface(button.gameObject, tint.HasValue ? Color.Lerp(UITheme.Control, tint.Value, .1f) : UITheme.Control, true);
        foreach (var text in button.GetComponentsInChildren<Text>(true)) text.color = UITheme.Foreground;
    }

    internal static bool IsTacticalPanel(UnitInfoPanel info)
    {
        return equipment.ContainsKey(info);
    }

    internal static bool FillEquipment(UnitInfoPanel info, Unit unit, Actor_Unit actor)
    {
        TextMeshProUGUI[] labels;
        if (!equipment.TryGetValue(info, out labels)) return false;
        Item melee, ranged, spell1, spell2;
        if (unit.CompanyLoadout)
        {
            melee = unit.GetItem((int)CompanySlot.Melee);
            ranged = unit.GetItem((int)CompanySlot.Ranged);
            spell1 = unit.GetItem((int)CompanySlot.Spell1);
            spell2 = unit.GetItem((int)CompanySlot.Spell2);
        }
        else
        {
            var items = unit.Items ?? new Item[0];
            melee = items.OfType<Weapon>().FirstOrDefault(w => w.Range <= 1);
            ranged = items.OfType<Weapon>().FirstOrDefault(w => w.Range > 1);
            var books = items.OfType<SpellBook>().Take(2).ToArray();
            spell1 = books.Length > 0 ? books[0] : null; spell2 = books.Length > 1 ? books[1] : null;
        }
        labels[0].text = melee?.Name ?? "";
        labels[1].text = ranged?.Name ?? "";
        labels[2].text = spell1?.Name ?? "";
        labels[3].text = spell2?.Name ?? "";
        return true;
    }
}
