// Added by this fork for the VoreWar mercenary-company conversion.
// 2026-11-05: Draw bounded, fog-aware road overlays without replacing the retained terrain artwork (spec 35).
// See CHANGES.md and License.txt for GPL-3.0 terms.
using UnityEngine;
using UnityEngine.UI;

public sealed class CompanyRoadGraphic : MaskableGraphic
{
    int connections;
    public void SetConnections(int mask)
    {
        if (connections == mask) return;
        connections = mask; SetVerticesDirty();
    }
    protected override void OnPopulateMesh(VertexHelper mesh)
    {
        mesh.Clear(); if (connections == 0) return;
        var rect = GetPixelAdjustedRect(); var center = rect.center;
        // Draw every edge first, then every inner strip, so junctions have no internal seams.
        for (int layer = 0; layer < 2; layer++)
        {
            float half = layer == 0 ? 3 : 2;
            Color32 tint = layer == 0 ? new Color32(49, 41, 25, 230) : new Color32(153, 128, 79, 235);
            Quad(mesh, new Rect(center.x - half, center.y - half, half * 2, half * 2), tint);
            if ((connections & 1) != 0) Quad(mesh, new Rect(rect.xMin, center.y - half, center.x - rect.xMin, half * 2), tint);
            if ((connections & 2) != 0) Quad(mesh, new Rect(center.x, center.y - half, rect.xMax - center.x, half * 2), tint);
            if ((connections & 4) != 0) Quad(mesh, new Rect(center.x - half, center.y, half * 2, rect.yMax - center.y), tint);
            if ((connections & 8) != 0) Quad(mesh, new Rect(center.x - half, rect.yMin, half * 2, center.y - rect.yMin), tint);
        }
    }
    static void Quad(VertexHelper mesh, Rect rect, Color32 tint)
    {
        int start = mesh.currentVertCount;
        mesh.AddVert(new Vector3(rect.xMin, rect.yMin), tint, Vector2.zero);
        mesh.AddVert(new Vector3(rect.xMin, rect.yMax), tint, Vector2.zero);
        mesh.AddVert(new Vector3(rect.xMax, rect.yMax), tint, Vector2.zero);
        mesh.AddVert(new Vector3(rect.xMax, rect.yMin), tint, Vector2.zero);
        mesh.AddTriangle(start, start + 1, start + 2); mesh.AddTriangle(start, start + 2, start + 3);
    }
}
