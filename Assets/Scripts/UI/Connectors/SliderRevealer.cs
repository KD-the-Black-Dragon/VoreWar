// Modified for the VoreWar mercenary-company conversion.
// 2026-11-03: Resolve slider hover from current raycasts and open one value dialog per right-button press (spec 17 feedback).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderRevealer : MonoBehaviour
{
    Slider slider;
    static int hitFrame = -1;
    static Slider hoveredSlider;
    static readonly List<RaycastResult> hits = new List<RaycastResult>();

    void Start()
    {
        slider = GetComponentInParent<Slider>();
    }

    bool IsCurrentTarget()
    {
        if (slider == null || !slider.interactable || State.GameManager.ActiveInput || EventSystem.current == null)
            return false;
        if (hitFrame != Time.frameCount)
        {
            hitFrame = Time.frameCount;
            hoveredSlider = null;
            hits.Clear();
            var pointer = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
            EventSystem.current.RaycastAll(pointer, hits);
            if (hits.Count > 0)
                hoveredSlider = hits[0].gameObject.GetComponentInParent<Slider>();
        }
        return hoveredSlider == slider && PointerInsideSlider(slider);
    }

    static bool PointerInsideSlider(Slider target)
    {
        var canvas = target.GetComponentInParent<Canvas>();
        var camera = canvas != null && canvas.renderMode != RenderMode.ScreenSpaceOverlay ? canvas.worldCamera : null;
        return RectTransformUtility.RectangleContainsScreenPoint(target.GetComponent<RectTransform>(), Input.mousePosition, camera);
    }

    void Update()
    {
        if (!IsCurrentTarget()) return;
        State.GameManager.HoveringTooltip.UpdateInformation(slider);
        // Poll only the press edge: revealers live below Slider's pointer handlers.
        if (!Input.GetMouseButtonDown(1)) return;
        var box = State.GameManager.CreateInputBox();
        box.SetData(TrySetValue, "Set Value", "Cancel", $"Change the value of this slider?  Allowed Range {slider.minValue} - {slider.maxValue}", 4);
    }

    void TrySetValue(string value)
    {
        if (float.TryParse(value, out float result)) slider.value = result;
    }
}
