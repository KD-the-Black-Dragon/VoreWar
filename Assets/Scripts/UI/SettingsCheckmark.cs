// Added by the VoreWar mercenary-company conversion fork.
// 2026-11-02: Draw a bright, thick checkmark for the blue settings pages without importing a new sprite (spec 15B).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using UnityEngine;
using UnityEngine.UI;

// A light, solid tick independent of the dark pixels in the legacy checkmark sprite.
public sealed class SettingsCheckmark : MaskableGraphic
{
    protected override void OnPopulateMesh(VertexHelper mesh)
    {
        mesh.Clear();
        Segment(mesh, new Vector2(-8, 0), new Vector2(-2, -6));
        Segment(mesh, new Vector2(-2, -6), new Vector2(9, 8));
    }
    void Segment(VertexHelper mesh, Vector2 from, Vector2 to)
    {
        Vector2 edge = (to - from).normalized;
        Vector2 normal = new Vector2(-edge.y, edge.x) * 2;
        int first = mesh.currentVertCount;
        mesh.AddVert(from - normal, color, Vector2.zero);
        mesh.AddVert(from + normal, color, Vector2.zero);
        mesh.AddVert(to + normal, color, Vector2.zero);
        mesh.AddVert(to - normal, color, Vector2.zero);
        mesh.AddTriangle(first, first + 1, first + 2);
        mesh.AddTriangle(first + 2, first + 3, first);
    }
}
