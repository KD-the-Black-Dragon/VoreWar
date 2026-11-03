// Added by the VoreWar mercenary-company conversion fork.
// 2026-11-02: Temporary matched-appearance portrait comparison on the main menu (spec 15E).
// 2026-11-02: Preserve comparison A/B as fixed crisp references after production adaptive sampling (spec 15E feedback).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public sealed class PortraitComparisonPanel : MonoBehaviour
{
    GameObject root;
    readonly List<GameObject> dolls = new List<GameObject>();
    readonly List<UnityEngine.Object> temporary = new List<UnityEngine.Object>();
    readonly Dictionary<string, Texture2D> originals = new Dictionary<string, Texture2D>();
    RectTransform[] cards;
    Text heading, details, preference;
    Race[] species;
    Actor_Unit actor;
    int selected, sizeIndex = 1;
    readonly int[] sizes = { 120, 180, 240 };
    readonly string[] names = { "A: Legacy image (crisp)", "B: Simple image (crisp)", "C: Always smooth", "D: Original PNG + smooth" };

    internal static void Attach(StartMode start)
    {
        var controller = start.gameObject.AddComponent<PortraitComparisonPanel>();
        var button = UITheme.Node("Portrait comparison", start.UI.transform);
        var rect = button.GetComponent<RectTransform>(); rect.anchorMin = rect.anchorMax = rect.pivot = Vector2.zero;
        rect.anchoredPosition = new Vector2(20, 440); rect.sizeDelta = new Vector2(400, 68);
        var control = button.AddComponent<Button>(); control.targetGraphic = UITheme.Surface(button, UITheme.Control, true);
        UITheme.StyleSelectable(control);
        UITheme.Label(button.transform, "Portrait comparison", 8, 0, 384, 68, 30).alignment = TextAnchor.MiddleCenter;
        control.onClick.AddListener(controller.Open);
    }
    void Open()
    {
        species = SpeciesPolicy.GetAvailable(SpeciesUse.Skirmish);
        if (species.Length == 0) { State.GameManager.CreateMessageBox(SpeciesPolicy.UnavailableMessage); return; }
        if (root == null) Build();
        root.SetActive(true); selected = Mathf.Clamp(selected, 0, species.Length - 1);
        NewAppearance();
    }
    void Build()
    {
        root = UITheme.Node("Temporary portrait comparison", transform);
        var canvas = root.AddComponent<Canvas>(); canvas.renderMode = RenderMode.ScreenSpaceOverlay; canvas.sortingOrder = 250;
        root.AddComponent<GraphicRaycaster>();
        var scaler = root.AddComponent<CanvasScaler>(); scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1280, 800); scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
        var backdrop = UITheme.Node("Backdrop", root.transform); UITheme.Stretch(backdrop.GetComponent<RectTransform>()); UITheme.Surface(backdrop, UITheme.Background);
        var frame = UITheme.Node("Comparison", backdrop.transform); var r = frame.GetComponent<RectTransform>();
        r.anchorMin = r.anchorMax = r.pivot = new Vector2(.5f, .5f); r.sizeDelta = new Vector2(1240, 760);
        heading = UITheme.Label(frame.transform, "Portrait comparison", 20, 12, 1200, 40, 26);
        Action(frame.transform, "Previous species", 20, 64, 210, () => { selected = (selected + species.Length - 1) % species.Length; NewAppearance(); });
        Action(frame.transform, "Next species", 244, 64, 190, () => { selected = (selected + 1) % species.Length; NewAppearance(); });
        Action(frame.transform, "New appearance", 448, 64, 230, NewAppearance);
        Action(frame.transform, "Change size", 692, 64, 200, () => { sizeIndex = (sizeIndex + 1) % sizes.Length; Draw(); });
        Action(frame.transform, "Close", 1010, 64, 210, () => root.SetActive(false));
        cards = new RectTransform[4];
        for (int i = 0; i < 4; i++)
        {
            int choice = i; var card = UITheme.Node(names[i], frame.transform); cards[i] = card.GetComponent<RectTransform>();
            UITheme.Place(cards[i], 20 + 304 * i, 130, 288, 350); UITheme.Surface(card, UITheme.Panel, true);
            UITheme.Label(card.transform, names[i], 10, 4, 268, 40, 18).alignment = TextAnchor.MiddleCenter;
            Action(frame.transform, "Prefer " + (char)('A' + i), 20 + 304 * i, 494, 288,
                () => preference.text = "Marked for review: " + names[choice] + ". This does not change game settings.");
        }
        details = UITheme.Label(frame.transform, "", 20, 554, 1200, 118, 17);
        preference = UITheme.Label(frame.transform, "Compare edges, colors and clothing. Mark a preference and tell me its letter and size.", 20, 688, 1200, 54, 18);
    }
    static void Action(Transform parent, string title, float x, float y, float width, UnityEngine.Events.UnityAction action)
    {
        var go = UITheme.Node(title, parent); UITheme.Place(go.GetComponent<RectTransform>(), x, y, width, 42);
        var button = go.AddComponent<Button>(); button.targetGraphic = UITheme.Surface(go, UITheme.Control, true); UITheme.StyleSelectable(button);
        UITheme.Label(go.transform, title, 6, 0, width - 12, 42, 18).alignment = TextAnchor.MiddleCenter; button.onClick.AddListener(action);
    }
    void NewAppearance()
    {
        actor = new Actor_Unit(new Unit(species[selected])); actor.Unit.TotalRandomizeAppearance(); Draw();
    }
    void ClearCopies()
    {
        foreach (var doll in dolls) { doll.SetActive(false); Destroy(doll); } dolls.Clear();
        foreach (var item in temporary) Destroy(item); temporary.Clear(); originals.Clear();
    }
    void Draw()
    {
        ClearCopies();
        heading.text = "Portrait comparison — " + SpeciesPolicy.DisplayName(species[selected]) + " — size " + sizes[sizeIndex];
        var original = UITheme.Node("Matched paper doll", cards[0]); dolls.Add(original);
        var display = original.AddComponent<UIUnitSprite>(); display.Name = UITheme.Label(original.transform, "", 0, 0, 1, 1);
        display.SetIndex(-1); display.UpdateSprites(actor); display.Name.gameObject.SetActive(false);
        float scale = sizes[sizeIndex] / 100f;
        var rect = original.GetComponent<RectTransform>(); rect.anchorMin = rect.anchorMax = rect.pivot = new Vector2(.5f, .5f);
        rect.sizeDelta = Vector2.zero; rect.anchoredPosition = new Vector2(0, -30 * scale); rect.localScale = new Vector3(scale, scale, 1);
        var sourceImages = original.GetComponentsInChildren<Image>(true).Where(im => im.sprite != null).ToArray();
        // Keep A/B useful as fixed crisp references, without mutating shared materials.
        foreach (var image in sourceImages)
        {
            var material = new Material(image.material); temporary.Add(material);
            material.SetFloat("_SmoothWhenSmall", 0); image.material = material;
            image.type = Image.Type.Sliced;
        }
        int rawCount = 0;
        for (int i = 1; i < 4; i++)
        {
            var copy = Instantiate(original, cards[i], false); dolls.Add(copy);
            foreach (var image in copy.GetComponentsInChildren<Image>(true))
            {
                if (image.sprite == null) continue;
                image.raycastTarget = false; image.type = Image.Type.Simple;
                if (i == 3)
                {
                    var replacement = RawSprite(image.sprite);
                    if (replacement != null) { image.sprite = replacement; rawCount++; }
                }
                if (i >= 2)
                {
                    var material = new Material(image.material); temporary.Add(material);
                    material.shader = Resources.Load<Shader>("PortraitPaletteSmooth");
                    Rect uv = image.sprite.textureRect; var tex = image.sprite.texture;
                    material.SetVector("_PortraitUVRect", new Vector4(uv.xMin / tex.width, uv.yMin / tex.height, uv.xMax / tex.width, uv.yMax / tex.height));
                    image.material = material;
                }
            }
        }
        foreach (var image in sourceImages) image.raycastTarget = false;
        details.text = "Same individual, pose and display size in every card. Source layers (loaded texture / sprite frame / sampling):\n" +
            string.Join("; ", sourceImages.Select(im => im.sprite.texture.name + " " + im.sprite.texture.width + "×" + im.sprite.texture.height +
                " / " + im.sprite.rect.width + "×" + im.sprite.rect.height + " / " + im.sprite.texture.filterMode).Distinct().Take(5).ToArray()) +
            "\nD: " + rawCount + "/" + sourceImages.Length + " layers loaded from original PNGs. " +
            (rawCount == sourceImages.Length ? "" : "Missing PNG layers retain their loaded sprites; D is a mixed-source comparison.") +
            "\nChange size cycles 120 / 180 / 240. No production artwork, texture settings or saves are changed.";
    }
    Sprite RawSprite(Sprite sprite)
    {
#if UNITY_EDITOR
        string name = sprite.texture.name;
        Texture2D texture;
        if (!originals.TryGetValue(name, out texture))
        {
            string path = Directory.GetFiles(Path.Combine(Application.dataPath, "Sprites"), "*.png", SearchOption.AllDirectories)
                .FirstOrDefault(p => string.Equals(Path.GetFileNameWithoutExtension(p), name, StringComparison.OrdinalIgnoreCase));
            if (path == null) return null;
            texture = new Texture2D(2, 2, TextureFormat.RGBA32, false); texture.LoadImage(File.ReadAllBytes(path));
            texture.filterMode = FilterMode.Point; texture.wrapMode = TextureWrapMode.Clamp;
            originals.Add(name, texture); temporary.Add(texture);
        }
        Rect r = sprite.rect; float x = (float)texture.width / sprite.texture.width, y = (float)texture.height / sprite.texture.height;
        var result = Sprite.Create(texture, new Rect(r.x * x, r.y * y, r.width * x, r.height * y),
            new Vector2(sprite.pivot.x / r.width, sprite.pivot.y / r.height), sprite.pixelsPerUnit, 0, SpriteMeshType.FullRect);
        temporary.Add(result); return result;
#else
        return null;
#endif
    }
    void Update() { if (root != null && root.activeSelf && Input.GetButtonDown("Menu")) root.SetActive(false); }
    void OnDestroy() { ClearCopies(); }
}
