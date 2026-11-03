// Modified for the VoreWar mercenary-company conversion.
// 2026-11-03: Resolve singular/spaced species dropdown labels for hover previews.
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class RaceHoverObjectAuto : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool hovering;

    Race race;

    private void Start()
    {
        var comp = GetComponentInChildren<TextMeshProUGUI>();
        if (comp == null)
        {
            Destroy(this);
            return;
        }

        if (SpeciesPolicy.TryParseName(comp.text, out Race result))
            race = result;
        else
            Destroy(this);
    }
    private void Update()
    {
        if (hovering == false)
            return;
        State.GameManager.HoveringRacePicture.UpdateInformation(race);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
    }
}