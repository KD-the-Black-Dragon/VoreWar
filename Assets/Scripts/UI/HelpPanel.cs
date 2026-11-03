// Modified for the VoreWar mercenary-company conversion.
// 2026-11-02: Present company help chapters and evaluate trait/spell references only when selected (spec 12).
// 2026-11-03: Unify campaign help in a compact blue chapter browser with a visible text scrollbar (spec 20).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HelpPanel : MonoBehaviour
{
    bool generatedHelp;
    public Transform ButtonFolder;
    public GameObject[] HelpPages;
    public GameObject ButtonPrefab;

    public TextMeshProUGUI Text;

    readonly List<Button> chapterButtons = new List<Button>();
    readonly List<Func<string>> chapters = new List<Func<string>>();
    readonly List<string> titles = new List<string>();
    ScrollRect reading;
    RectTransform viewport;
    UnityEngine.UI.Text heading;

    public void Open(int chapter = 0)
    {
        gameObject.SetActive(true);
        GenerateButtonsIfNeeded();
        ShowChapter(Mathf.Clamp(chapter, 0, chapters.Count - 1));
    }

    internal void ReceiveInput()
    {
        if (Input.GetButtonDown("Menu")) State.GameManager.Menu.CloseHelp();
    }

    public void GenerateButtonsIfNeeded()
    {
        if (generatedHelp)
            return;
        // Retain the original scene objects/bindings, but replace their presentation at runtime.
        var font = Text.font;
        foreach (Transform child in transform) child.gameObject.SetActive(false);
        transform.localScale = Vector3.one;
        var canvas = GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay; canvas.sortingOrder = 400;
        var scaler = GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1280, 800);
        scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
        var background = UITheme.Node("Help background", transform);
        UITheme.Stretch(background.GetComponent<RectTransform>()); UITheme.Surface(background, UITheme.Background);
        var frame = UITheme.Node("Campaign guide", transform);
        var rect = frame.GetComponent<RectTransform>();
        rect.anchorMin = rect.anchorMax = rect.pivot = new Vector2(.5f, .5f);
        rect.sizeDelta = new Vector2(1160, 744); UITheme.Surface(frame, UITheme.Panel, true);
        heading = UITheme.Label(frame.transform, "Campaign help", 24, 18, 1112, 40, 26);
        Surface(frame.transform, "Header border", 24, 72, 1112, 1, UITheme.Border);
        Surface(frame.transform, "Chapters", 24, 88, 232, 574, UITheme.Row);
        Surface(frame.transform, "Reading panel", 272, 88, 864, 574, UITheme.Row);
        for (int i = 0; i < HelpText.CompanyPages.Length; i++)
        {
            int index = i;
            CreatePageButton(() => HelpText.CompanyPages[index], HelpText.CompanyTitles[index], frame.transform);
        }
        CreatePageButton(() => HelpText.Stats, "Character Stats", frame.transform);
        CreatePageButton(HelpText.Traits, "Traits", frame.transform);
        CreatePageButton(HelpText.Spells, "Spells", frame.transform);

        var area = Surface(frame.transform, "Reading viewport", 288, 104, 824, 542, UITheme.Row);
        viewport = area.GetComponent<RectTransform>(); area.AddComponent<RectMask2D>();
        reading = area.AddComponent<ScrollRect>(); reading.viewport = viewport;
        reading.horizontal = false; reading.vertical = true; reading.inertia = false;
        reading.movementType = ScrollRect.MovementType.Clamped; reading.scrollSensitivity = 34;
        var article = UITheme.Node("Help text", area.transform);
        Text = article.AddComponent<TextMeshProUGUI>(); Text.font = font;
        Text.fontSize = 18; Text.enableAutoSizing = false; Text.enableWordWrapping = true;
        Text.color = UITheme.Foreground; Text.alignment = TextAlignmentOptions.TopLeft;
        Text.overflowMode = TextOverflowModes.Overflow; Text.raycastTarget = false;
        Text.margin = new Vector4(16, 12, 16, 12);
        var content = Text.rectTransform;
        content.anchorMin = new Vector2(0, 1); content.anchorMax = Vector2.one; content.pivot = new Vector2(0, 1);
        reading.content = content;
        var track = Surface(frame.transform, "Text scrollbar", 1118, 104, 12, 542, UITheme.Background);
        var handle = UITheme.Node("Handle", track.transform); UITheme.Stretch(handle.GetComponent<RectTransform>());
        var graphic = UITheme.Surface(handle, UITheme.Accent);
        var bar = track.AddComponent<Scrollbar>(); bar.handleRect = handle.GetComponent<RectTransform>(); bar.targetGraphic = graphic;
        bar.direction = Scrollbar.Direction.BottomToTop; UITheme.StyleSelectable(bar);
        reading.verticalScrollbar = bar; reading.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.Permanent;
        MakeButton(frame.transform, "Exit Help", 926, 684, 210, 40, State.GameManager.Menu.CloseHelp);
        generatedHelp = true;
    }

    void CreatePageButton(Func<string> pageText, string pageName, Transform parent)
    {
        int index = chapters.Count; chapters.Add(pageText); titles.Add(pageName);
        chapterButtons.Add(MakeButton(parent, pageName, 32, 96 + index * 37, 216, 31, () => ShowChapter(index)));
    }
    void ShowChapter(int chapter)
    {
        heading.text = "Campaign help — " + titles[chapter]; Text.text = chapters[chapter]();
        Canvas.ForceUpdateCanvases();
        float height = Text.GetPreferredValues(Text.text, viewport.rect.width - 32, Mathf.Infinity).y + 24;
        Text.rectTransform.sizeDelta = new Vector2(0, Mathf.Max(viewport.rect.height, height));
        Text.rectTransform.anchoredPosition = Vector2.zero;
        Canvas.ForceUpdateCanvases(); reading.verticalNormalizedPosition = 1;
        for (int i = 0; i < chapterButtons.Count; i++)
            chapterButtons[i].GetComponent<Image>().color = i == chapter ? UITheme.Section : UITheme.Control;
    }
    static GameObject Surface(Transform parent, string name, float x, float y, float width, float height, Color color)
    {
        var go = UITheme.Node(name, parent); UITheme.Place(go.GetComponent<RectTransform>(), x, y, width, height);
        UITheme.Surface(go, color, true); return go;
    }
    static Button MakeButton(Transform parent, string words, float x, float y, float width, float height, Action action)
    {
        var go = Surface(parent, words, x, y, width, height, UITheme.Control);
        var button = go.AddComponent<Button>(); UITheme.StyleSelectable(button);
        var label = UITheme.Label(go.transform, words, 6, 1, width - 12, height - 2, 17); label.alignment = TextAnchor.MiddleCenter;
        button.onClick.AddListener(() => action()); return button;
    }
}
