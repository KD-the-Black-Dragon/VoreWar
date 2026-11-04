// Modified for the VoreWar mercenary-company conversion.
// 2026-11-04: Theme battle outcomes and compact the title/species/detail hierarchy.
// 2026-11-04: Enlarge outcome typography and add detail-line spacing after player review.
// See CHANGES.md and License.txt for GPL-3.0 terms.
using UnityEngine;
using UnityEngine.UI;

public class StatScreenPanel : MonoBehaviour
{
    public Text VictoryType;
    public Text AttackerTitle;
    public Text DefenderTitle;
    public Text AttackerText;
    public Text DefenderText;
    public Text CloseButtonText;
    public Button ExtendTime;

    internal bool AutoClose;
    internal float AutoCloseTime;

    void StyleResults()
    {
        UITheme.Surface(gameObject, UITheme.Background, true);
        UITheme.StylePopup(gameObject);
        Format(VictoryType, 48);
        Format(AttackerTitle, 32);
        Format(DefenderTitle, 32);
        Format(AttackerText, 24);
        AttackerText.lineSpacing = 1.15f;
        Format(DefenderText, 24);
        DefenderText.lineSpacing = 1.15f;
        if (transform.Find("Outcome divider") == null)
        {
            var divider = UITheme.Node("Outcome divider", transform);
            var rect = divider.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(.5f, .15f);
            rect.anchorMax = new Vector2(.5f, .82f);
            rect.offsetMin = new Vector2(-.5f, 0);
            rect.offsetMax = new Vector2(.5f, 0);
            var image = UITheme.Surface(divider, new Color(UITheme.Border.r, UITheme.Border.g, UITheme.Border.b, .5f));
            image.raycastTarget = false;
        }
    }

    static void Format(Text text, int size)
    {
        text.resizeTextForBestFit = false;
        text.fontSize = size;
        text.color = UITheme.Foreground;
    }

    public void Open(bool AIOnly)
    {
        StyleResults();
        State.GameManager.StatScreen.gameObject.SetActive(true);
        if (Config.TimedAIStats && AIOnly)
        {
            State.GameManager.StatScreen.AutoClose = true;
            State.GameManager.StatScreen.AutoCloseTime = 10;
            ExtendTime.gameObject.SetActive(true);
        }
        else
        {
            State.GameManager.StatScreen.AutoClose = false;
            State.GameManager.StatScreen.AutoCloseTime = 0;
            CloseButtonText.text = "Exit Stats Screen";
            ExtendTime.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (AutoClose && AutoCloseTime > 0)
        {
            AutoCloseTime -= Time.deltaTime;
            if (AutoCloseTime < 0)
                State.GameManager.CloseStatsScreen();
            CloseButtonText.text = $"Exit Stats Screen (automatically happens in {(int)AutoCloseTime})";
        }

    }

    public void AddTime()
    {
        AutoCloseTime += 15;
    }

}
