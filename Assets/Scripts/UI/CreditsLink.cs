// Added by the VoreWar mercenary-company conversion fork.
// 2026-11-03: Open the requested Warband Patreon link from the credits (spec 23).
// 2026-11-03: Apply the shared blue palette and smaller credits typography (spec 26).
// See CHANGES.md and License.txt for GPL-3.0 terms; inherited credits remain applicable.
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CreditsLink : MonoBehaviour, IPointerClickHandler
{
    const string PatreonUrl = "https://www.patreon.com/KD_Dragon";

    void OnEnable()
    {
        var canvas = GetComponentInParent<Canvas>();
        if (canvas == null) return;

        // This component belongs only to the existing credits page.
        foreach (var image in canvas.GetComponentsInChildren<Image>(true))
            UITheme.Surface(image.gameObject, image.GetComponentInParent<ScrollRect>() != null
                ? UITheme.Panel : UITheme.Background);
        foreach (var button in canvas.GetComponentsInChildren<Button>(true))
        {
            if (button.targetGraphic != null)
                UITheme.Surface(button.targetGraphic.gameObject, UITheme.Control, true);
            UITheme.StyleSelectable(button);
        }
        foreach (var scrollbar in canvas.GetComponentsInChildren<Scrollbar>(true))
        {
            UITheme.Surface(scrollbar.gameObject, UITheme.Row);
            if (scrollbar.targetGraphic != null)
                UITheme.Surface(scrollbar.targetGraphic.gameObject, UITheme.Accent);
            UITheme.StyleSelectable(scrollbar);
        }
        foreach (var label in canvas.GetComponentsInChildren<Text>(true))
        {
            label.color = UITheme.Foreground;
            label.fontSize = 24;
            label.resizeTextForBestFit = false;
        }
        foreach (var label in canvas.GetComponentsInChildren<TMP_Text>(true))
        {
            label.color = UITheme.Foreground;
            label.fontSize = 24;
            label.enableAutoSizing = false;
        }
        var text = GetComponent<TextMeshProUGUI>();
        text.text = text.text.Replace("<size=36>", "<size=30>").Replace("<size=32>", "<size=26>");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        var text = GetComponent<TextMeshProUGUI>();
        int index = TMP_TextUtilities.FindIntersectingLink(text, eventData.position, eventData.pressEventCamera);
        if (index >= 0 && text.textInfo.linkInfo[index].GetLinkID() == PatreonUrl)
            Application.OpenURL(PatreonUrl);
    }
}
