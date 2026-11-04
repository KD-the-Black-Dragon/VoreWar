// Added by the VoreWar mercenary-company conversion fork.
// 2026-11-03: Single-screen blue leader customization with retained native controls and a contained center portrait (spec 18).
// 2026-11-03: Share gold/gradient dropdown templates with the accepted leader creator (spec 28).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal sealed class CompanyLeaderCreatorView
{
    readonly CustomizerButton[] rows;
    readonly Transform root;

    internal CompanyLeaderCreatorView(CustomizerPanel creator, Transform parent, Race[] races, Race selected, Action<Race> select)
    {
        root = parent;
        rows = creator.ButtonPanel.GetComponentsInChildren<CustomizerButton>(true);
        // Copy the existing dropdown template, preserving its full scrollable future species list.
        var speciesObject = UnityEngine.Object.Instantiate(creator.Gender.gameObject, parent);
        speciesObject.name = "Species";
        var species = speciesObject.GetComponent<TMP_Dropdown>();
        species.onValueChanged = new TMP_Dropdown.DropdownEvent();
        species.ClearOptions(); species.AddOptions(races.Select(SpeciesPolicy.DisplayName).ToList());
        species.value = Array.IndexOf(races, selected); species.RefreshShownValue();
        species.onValueChanged.AddListener(index => select(races[index]));
        Move(species.transform, 20, 96, 380, 34); speciesObject.SetActive(true);
        Style(species.gameObject);
        Move(creator.Gender.transform, 20, 142, 380, 34); Style(creator.Gender.gameObject);
        var pronouns = new[] { creator.Nominative, creator.Accusative, creator.PronominalPossessive, creator.PredicativePossessive, creator.Reflexive };
        for (int i = 0; i < pronouns.Length; i++)
        {
            Move(pronouns[i].transform, 20 + (i % 3) * 130, 184 + (i / 3) * 38, 120, 32);
            Style(pronouns[i].gameObject);
            pronouns[i].customCaretColor = true; pronouns[i].caretColor = UITheme.Foreground;
        }
        Move(creator.Quantification.transform, 280, 222, 120, 32); Style(creator.Quantification.gameObject);
        foreach (var row in rows)
        {
            row.transform.SetParent(parent, false); row.transform.localScale = Vector3.one;
            foreach (var layout in row.GetComponents<LayoutGroup>()) layout.enabled = false;
            foreach (var fitter in row.GetComponents<ContentSizeFitter>()) fitter.enabled = false;
            UITheme.Surface(row.gameObject, UITheme.Row, true);
            Style(row.gameObject);
        }
        var card = UITheme.Node("Leader paper doll", parent);
        UITheme.Place(card.GetComponent<RectTransform>(), 420, 142, 440, 464);
        UITheme.Surface(card, UITheme.Panel, true); card.AddComponent<RectMask2D>();
        var doll = creator.DisplayedSprite;
        doll.transform.SetParent(card.transform, false);
        UITheme.Place(doll.GetComponent<RectTransform>(), 220, 292, 0, 0);
        doll.transform.localScale = new Vector3(2, 2, 1);
        doll.Name.gameObject.SetActive(false);
        foreach (var image in doll.GetComponentsInChildren<Image>(true)) image.raycastTarget = false;
        // All useful controls were moved out; suppress only the now-empty legacy chrome.
        foreach (Transform child in creator.transform) child.gameObject.SetActive(false);
        foreach (var image in creator.GetComponents<Image>()) image.enabled = false;
    }

    void Move(Transform control, float x, float y, float width, float height)
    {
        control.SetParent(root, false); control.localScale = Vector3.one;
        UITheme.Place(control.GetComponent<RectTransform>(), x, y, width, height);
    }

    static void Style(GameObject control)
    {
        UITheme.StylePopup(control);
        var image = control.GetComponent<Image>();
        if (image != null) UITheme.Surface(control, UITheme.Control, true);
        var dropdown = control.GetComponent<TMP_Dropdown>();
        if (dropdown != null && dropdown.template != null)
        {
            UITheme.Surface(dropdown.template.gameObject, UITheme.Panel, true);
            foreach (var bar in dropdown.template.GetComponentsInChildren<Scrollbar>(true))
            {
                UITheme.Surface(bar.gameObject, UITheme.Background, true);
                if (bar.targetGraphic != null) UITheme.Surface(bar.targetGraphic.gameObject, UITheme.Control, true);
                UITheme.StyleSelectable(bar);
            }
            foreach (var toggle in dropdown.template.GetComponentsInChildren<Toggle>(true))
                UITheme.StyleDropdownItem(toggle);
        }
        foreach (var text in control.GetComponentsInChildren<Text>(true))
        { text.fontSize = 14; text.resizeTextForBestFit = false; }
        foreach (var text in control.GetComponentsInChildren<TMP_Text>(true))
        { text.fontSize = 14; text.enableAutoSizing = false; }
    }

    internal void Refresh()
    {
        var visible = rows.Where(row => row.gameObject.activeSelf).ToArray();
        int perSide = Math.Max(1, (visible.Length + 1) / 2);
        float step = Math.Min(30, 434f / perSide);
        for (int i = 0; i < visible.Length; i++)
        {
            float x = i < perSide ? 20 : 880;
            float y = 264 + (i % perSide) * step;
            var row = visible[i];
            UITheme.Place(row.GetComponent<RectTransform>(), x, y, 380, step - 3);
            UITheme.Place(row.Decrease.GetComponent<RectTransform>(), 0, 0, 32, step - 3);
            UITheme.Place(row.Increase.GetComponent<RectTransform>(), 348, 0, 32, step - 3);
            UITheme.Place(row.Label.rectTransform, 38, 0, 304, step - 3);
            row.Label.alignment = TextAnchor.MiddleCenter;
        }
    }
}
