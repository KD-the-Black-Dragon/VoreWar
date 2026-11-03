// Added by the VoreWar mercenary-company conversion fork.
// 2026-11-03: Open the requested Warband Patreon link from the credits (spec 23).
// See CHANGES.md and License.txt for GPL-3.0 terms; inherited credits remain applicable.
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CreditsLink : MonoBehaviour, IPointerClickHandler
{
    const string PatreonUrl = "https://www.patreon.com/KD_Dragon";

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        var text = GetComponent<TextMeshProUGUI>();
        int index = TMP_TextUtilities.FindIntersectingLink(text, eventData.position, eventData.pressEventCamera);
        if (index >= 0 && text.textInfo.linkInfo[index].GetLinkID() == PatreonUrl)
            Application.OpenURL(PatreonUrl);
    }
}
