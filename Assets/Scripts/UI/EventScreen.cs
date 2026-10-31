// Modified for the VoreWar mercenary-company conversion.
// 2026-10-30: Make the retired panel inert and clear callbacks without strategic regeneration (spec 04A).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventScreen : MonoBehaviour
{
    public TextMeshProUGUI MainText;
    public TextMeshProUGUI RaceText;

    public Button FirstChoice;
    public Button SecondChoice;
    public Button ThirdChoice;

    // Preserve the serialized layout for later presentation reuse, without legacy execution.
    private void OnEnable()
    {
        ClearLegacyState();
        gameObject.SetActive(false);
    }

    public void OnDisable()
    {
        ClearLegacyState();
    }

    private void ClearLegacyState()
    {
        if (MainText != null)
            MainText.text = "";
        if (RaceText != null)
            RaceText.text = "";
        DisableChoice(FirstChoice);
        DisableChoice(SecondChoice);
        DisableChoice(ThirdChoice);
    }

    private static void DisableChoice(Button button)
    {
        if (button == null)
            return;
        button.interactable = false;
        button.onClick.RemoveAllListeners();
        for (int i = 0; i < button.onClick.GetPersistentEventCount(); i++)
            button.onClick.SetPersistentListenerState(i, UnityEngine.Events.UnityEventCallState.Off);
    }
}
