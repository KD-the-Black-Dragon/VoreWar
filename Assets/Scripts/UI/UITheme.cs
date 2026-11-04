// Added by the VoreWar mercenary-company conversion fork.
// 2026-11-02: Shared restrained blue palette and compact UI styling, introduced with Options (spec 15A).
// 2026-11-02: Share normalized sliders and bright vector checkmarks across Options and Content Settings (spec 15B).
// 2026-11-03: Share restrained blue styling for player-facing popup surfaces (spec 17).
// 2026-11-03: Apply the selected full-depth blue gradients and aged-gold control/panel edges (spec 28).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using UnityEngine;
using UnityEngine.UI;

internal static class UITheme
{
    internal static readonly Color Background = new Color32(12, 26, 46, 255);
    internal static readonly Color Panel = new Color32(20, 43, 72, 255);
    internal static readonly Color Row = new Color32(25, 51, 83, 255);
    internal static readonly Color Section = new Color32(30, 66, 105, 255);
    internal static readonly Color Control = new Color32(35, 72, 112, 255);
    internal static readonly Color Border = new Color32(145, 122, 81, 255);
    internal static readonly Color Foreground = new Color32(231, 240, 251, 255);
    internal static readonly Color Muted = new Color32(167, 193, 220, 255);
    internal static readonly Color Accent = new Color32(196, 165, 116, 255);

    internal static void StylePopup(GameObject root)
    {
        // Only popup hierarchies are passed here; preserve dimmers and non-background graphics.
        foreach (var image in root.GetComponentsInChildren<Image>(true))
        {
            if (image.GetComponentInParent<Slider>() != null || image.GetComponentInParent<UIUnitSprite>() != null) continue;
            Color old = image.color;
            if (old.b > old.r + .1f && old.b > old.g + .1f)
            {
                Color color = Panel; color.a = old.a; Surface(image.gameObject, color, true);
            }
        }
        foreach (var button in root.GetComponentsInChildren<Button>(true))
        {
            StyleSelectable(button);
            var image = button.targetGraphic as Image;
            if (image != null) Surface(image.gameObject, Control, true);
        }
        foreach (var text in root.GetComponentsInChildren<Text>(true)) text.color = Foreground;
        foreach (var text in root.GetComponentsInChildren<TMPro.TMP_Text>(true)) text.color = Foreground;
    }

    internal static GameObject Node(string name, Transform parent)
    {
        var go = new GameObject(name, typeof(RectTransform)); go.transform.SetParent(parent, false);
        return go;
    }
    internal static void Place(RectTransform rect, float x, float y, float width, float height)
    {
        rect.anchorMin = rect.anchorMax = rect.pivot = new Vector2(0, 1);
        rect.anchoredPosition = new Vector2(x, -y); rect.sizeDelta = new Vector2(width, height);
    }
    internal static void Stretch(RectTransform rect, float left = 0, float right = 0, float top = 0, float bottom = 0)
    {
        rect.anchorMin = Vector2.zero; rect.anchorMax = Vector2.one;
        rect.offsetMin = new Vector2(left, bottom); rect.offsetMax = new Vector2(-right, -top);
    }
    internal static Image Surface(GameObject go, Color color, bool border = false)
    {
        var image = go.GetComponent<Image>() ?? go.AddComponent<Image>();
        image.sprite = null; image.overrideSprite = null; image.type = Image.Type.Simple;
        image.color = color;
        bool control = SameColor(color, Control);
        float depth = control ? 1 : SameColor(color, Panel) ? .55f : SameColor(color, Row) ? .35f : SameColor(color, Section) ? .65f : 0;
        bool accent = SameColor(color, Accent) || SameColor(color, Border);
        if (depth == 0 && border && !accent && !SameColor(color, Background))
        { depth = 1; control = true; } // Explicitly framed, lightly tinted tactical commands.
        border |= depth > 0;
        var effect = go.GetComponent<UIThemeSurface>();
        if (depth > 0 || border)
        {
            if (effect == null) effect = go.AddComponent<UIThemeSurface>();
            effect.enabled = true;
            effect.Configure(depth, border, control);
            // The new border is drawn inside the image and must not be doubled by an old outline.
            var outline = go.GetComponent<Outline>();
            if (outline != null) outline.enabled = false;
        }
        else if (effect != null) effect.enabled = false;
        return image;
    }
    static bool SameColor(Color a, Color b)
    {
        return Mathf.Abs(a.r - b.r) < .001f && Mathf.Abs(a.g - b.g) < .001f && Mathf.Abs(a.b - b.b) < .001f;
    }
    internal static void Frame(GameObject go, bool selected = false)
    {
        var image = go.GetComponent<Image>() ?? go.AddComponent<Image>();
        image.sprite = null; image.overrideSprite = null; image.type = Image.Type.Simple;
        image.color = Color.white; image.raycastTarget = false;
        var effect = go.GetComponent<UIThemeSurface>() ?? go.AddComponent<UIThemeSurface>();
        effect.Configure(0, true, selected, true, selected);
    }
    internal static void StyleDropdownItem(Toggle item)
    {
        var image = item.targetGraphic as Image;
        if (image != null) Surface(image.gameObject, Control, true);
        StyleCheckmark(item, true);
        StyleSelectable(item);
    }
    internal static Text Label(Transform parent, string words, float x, float y, float width, float height, int size = 17)
    {
        var go = Node(words, parent); Place(go.GetComponent<RectTransform>(), x, y, width, height);
        var label = go.AddComponent<Text>(); label.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        StyleText(label, size); label.text = words; label.raycastTarget = false;
        return label;
    }
    internal static void StyleText(Text text, int size = 17)
    {
        text.fontSize = size; text.resizeTextForBestFit = false; text.color = Foreground;
        text.alignment = TextAnchor.MiddleLeft;
    }
    static void SliderRail(RectTransform rect)
    {
        rect.localScale = Vector3.one; rect.localRotation = Quaternion.identity; rect.pivot = new Vector2(.5f, .5f);
        rect.anchorMin = new Vector2(0, .5f); rect.anchorMax = new Vector2(1, .5f);
        rect.sizeDelta = new Vector2(-16, 6); rect.anchoredPosition = Vector2.zero;
    }
    internal static void StyleSlider(Slider slider)
    {
        var background = slider.transform.Find("Background") as RectTransform;
        if (background != null)
        {
            SliderRail(background);
            UITheme.Surface(background.gameObject, UITheme.Background).type = Image.Type.Simple;
        }
        if (slider.fillRect != null)
        {
            var area = (RectTransform)slider.fillRect.parent;
            SliderRail(area);
            slider.fillRect.localScale = Vector3.one; slider.fillRect.localRotation = Quaternion.identity;
            slider.fillRect.pivot = new Vector2(.5f, .5f);
            bool reversed = slider.direction == Slider.Direction.RightToLeft;
            slider.fillRect.anchorMin = new Vector2(reversed ? 1 - slider.normalizedValue : 0, 0);
            slider.fillRect.anchorMax = new Vector2(reversed ? 1 : slider.normalizedValue, 1);
            slider.fillRect.sizeDelta = Vector2.zero; slider.fillRect.anchoredPosition = Vector2.zero;
            UITheme.Surface(slider.fillRect.gameObject, UITheme.Accent).type = Image.Type.Simple;
        }
        if (slider.handleRect != null)
        {
            var area = (RectTransform)slider.handleRect.parent;
            area.localScale = Vector3.one; area.localRotation = Quaternion.identity; area.pivot = new Vector2(.5f, .5f);
            UITheme.Stretch(area, 8, 8);
            slider.handleRect.localScale = Vector3.one; slider.handleRect.localRotation = Quaternion.identity;
            slider.handleRect.pivot = new Vector2(.5f, .5f);
            float position = slider.direction == Slider.Direction.RightToLeft ? 1 - slider.normalizedValue : slider.normalizedValue;
            slider.handleRect.anchorMin = slider.handleRect.anchorMax = new Vector2(position, .5f);
            slider.handleRect.sizeDelta = new Vector2(16, 22); slider.handleRect.anchoredPosition = Vector2.zero;
            slider.targetGraphic = UITheme.Surface(slider.handleRect.gameObject, UITheme.Accent, true);
        }
    }

    internal static void StyleCheckmark(Toggle toggle, bool listItem = false)
    {
        if (toggle.graphic is SettingsCheckmark) return;
        if (toggle.graphic != null) toggle.graphic.gameObject.SetActive(false);
        var mark = Node("Light checkmark", toggle.transform);
        var rect = mark.GetComponent<RectTransform>();
        rect.anchorMin = rect.anchorMax = new Vector2(listItem ? 0 : .5f, .5f);
        rect.pivot = new Vector2(.5f, .5f);
        rect.anchoredPosition = new Vector2(listItem ? 14 : 0, 0); rect.sizeDelta = new Vector2(24, 24);
        var graphic = mark.AddComponent<SettingsCheckmark>();
        graphic.color = Foreground; graphic.raycastTarget = false;
        toggle.graphic = graphic;
        graphic.canvasRenderer.SetAlpha(toggle.isOn ? 1 : 0);
    }

    internal static void StyleSelectable(Selectable control)
    {
        var colors = control.colors;
        colors.normalColor = Color.white; colors.highlightedColor = new Color(1.1f, 1.1f, 1.1f);
        colors.pressedColor = new Color(.84f, .84f, .84f); colors.disabledColor = new Color(.45f, .55f, .68f, .6f);
        colors.colorMultiplier = 1; colors.fadeDuration = .08f;
        control.colors = colors;
    }
}
