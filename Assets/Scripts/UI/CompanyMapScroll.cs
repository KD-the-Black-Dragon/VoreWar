// Added by the VoreWar mercenary-company conversion fork.
// 2026-11-02: Support left/middle map dragging without treating a drag release as a tile move (spec 14 feedback).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public sealed class CompanyMapScroll : ScrollRect
{
    bool dragging;
    int lastDragFrame = -1;
    public bool SuppressTileClick { get { return dragging || Time.frameCount <= lastDragFrame; } }
    static bool Supported(PointerEventData e)
    { return e.button == PointerEventData.InputButton.Left || e.button == PointerEventData.InputButton.Middle; }
    static void Forward(PointerEventData e, Action action)
    {
        var button = e.button;
        try { e.button = PointerEventData.InputButton.Left; action(); }
        finally { e.button = button; }
    }
    public override void OnInitializePotentialDrag(PointerEventData e)
    { if (Supported(e)) Forward(e, () => base.OnInitializePotentialDrag(e)); }
    public override void OnBeginDrag(PointerEventData e)
    {
        if (!Supported(e)) return;
        dragging = true;
        Forward(e, () => base.OnBeginDrag(e));
    }
    public override void OnDrag(PointerEventData e)
    { if (Supported(e)) Forward(e, () => base.OnDrag(e)); }
    public override void OnEndDrag(PointerEventData e)
    {
        if (!Supported(e)) return;
        Forward(e, () => base.OnEndDrag(e));
        dragging = false; lastDragFrame = Time.frameCount;
    }
    protected override void OnDisable()
    { dragging = false; lastDragFrame = -1; base.OnDisable(); }
}
