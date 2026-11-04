// Added by the VoreWar mercenary-company conversion fork.
// 2026-11-03: Render the selected UI gradients and aged-gold borders without changing sprites (spec 28).
// 2026-11-03: Allow nested combat-log surfaces to omit only their duplicate left edge (spec 28 feedback).
// See CHANGES.md and License.txt for GPL-3.0 terms; inherited notices remain applicable.
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Image))]
public sealed class UIThemeSurface : BaseMeshEffect
{
    // Dropdowns clone their styled templates; Unity copies serialized component settings.
    [SerializeField, HideInInspector] float depth;
    [SerializeField, HideInInspector] bool border;
    [SerializeField, HideInInspector] bool control;
    [SerializeField, HideInInspector] bool frameOnly;
    [SerializeField, HideInInspector] bool highlighted;
    [SerializeField, HideInInspector] bool leftBorder = true;

    internal void HideLeftBorder()
    {
        if (!leftBorder) return;
        leftBorder = false;
        graphic.SetVerticesDirty();
    }

    internal void Configure(float strength, bool outlined, bool isControl, bool onlyBorder = false, bool selected = false)
    {
        if (depth == strength && border == outlined && control == isControl && frameOnly == onlyBorder && highlighted == selected) return;
        depth = strength;
        border = outlined;
        control = isControl;
        frameOnly = onlyBorder;
        highlighted = selected;
        graphic.SetVerticesDirty();
    }

    public override void ModifyMesh(VertexHelper mesh)
    {
        if (!IsActive() || mesh.currentVertCount == 0 || (depth == 0 && !border)) return;
        var image = graphic as Image;
        // Never replace artwork, filled status graphics or their original geometry.
        if (image == null || image.sprite != null || image.overrideSprite != null || image.type != Image.Type.Simple) return;
        Rect rect = image.GetPixelAdjustedRect();
        if (rect.width <= 0 || rect.height <= 0) return;

        Color basis = image.color;
        Color light = Color.Lerp(basis, new Color(1, 1, 1, basis.a), .16f * depth);
        Color dark = Color.Lerp(basis, new Color(0, 0, 0, basis.a), .14f * depth);
        float middle = Mathf.Lerp(rect.yMin, rect.yMax, .52f);
        mesh.Clear();
        if (!frameOnly)
        {
            Quad(mesh, rect.xMin, rect.yMin, rect.xMax, middle, dark, basis);
            Quad(mesh, rect.xMin, middle, rect.xMax, rect.yMax, basis, light);
        }
        if (!border) return;

        // One physical pixel, drawn inside the existing rectangle: no layout or hit-area changes.
        float scale = graphic.canvas == null ? 1 : graphic.canvas.scaleFactor;
        float edge = Mathf.Min(1 / Mathf.Max(.01f, scale), Mathf.Min(rect.width, rect.height) * .5f);
        Color gold = highlighted ? UITheme.Accent : UITheme.Border;
        gold.a = basis.a * (control ? 1 : .7f);
        Quad(mesh, rect.xMin, rect.yMin, rect.xMax, rect.yMin + edge, gold, gold);
        Quad(mesh, rect.xMin, rect.yMax - edge, rect.xMax, rect.yMax, gold, gold);
        if (leftBorder)
            Quad(mesh, rect.xMin, rect.yMin + edge, rect.xMin + edge, rect.yMax - edge, gold, gold);
        Quad(mesh, rect.xMax - edge, rect.yMin + edge, rect.xMax, rect.yMax - edge, gold, gold);
    }

    static void Quad(VertexHelper mesh, float left, float bottom, float right, float top, Color low, Color high)
    {
        if (right <= left || top <= bottom) return;
        int first = mesh.currentVertCount;
        var vertex = UIVertex.simpleVert;
        vertex.uv0 = Vector2.zero;
        vertex.color = low; vertex.position = new Vector3(left, bottom); mesh.AddVert(vertex);
        vertex.color = high; vertex.position = new Vector3(left, top); mesh.AddVert(vertex);
        vertex.color = high; vertex.position = new Vector3(right, top); mesh.AddVert(vertex);
        vertex.color = low; vertex.position = new Vector3(right, bottom); mesh.AddVert(vertex);
        mesh.AddTriangle(first, first + 1, first + 2);
        mesh.AddTriangle(first + 2, first + 3, first);
    }
}
