// Modified for the VoreWar mercenary-company conversion.
// 2026-11-05: Refit the adjacent trait viewport when the combat log or resolution changes (spec 38 phase C).
// 2026-11-02: Keep a larger eight-line-or-more compact battle log after restoring from full view (spec 16).
// 2026-11-02: Fix stretched compact log width, restore size controls and refresh scroll layout (spec 16 feedback).
// 2026-11-03: Shorten compact log three pixels and join it to the bottom bar (spec 16 feedback).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageLogPanel : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public Button IncreaseSize;
    public Button DecreaseSize;

    public Button Restore;

    public Toggle ShowOdds;
    public Toggle ShowHealing;
    public Toggle ShowSpells;
    public Toggle ShowMisses;
    public Toggle ShowInformational;
    public Toggle ShowWeaponCombat;
    public Toggle ShowFluff;

    RectTransform rect;

    int size = 1;

    bool initialized = false;
    int layoutWidth, layoutHeight;

    void LateUpdate()
    {
        if (!initialized || (layoutWidth == Screen.width && layoutHeight == Screen.height)) return;
        layoutWidth = Screen.width;
        layoutHeight = Screen.height;
        // The side panel and log use different canvases. Refit after both have resized.
        Canvas.ForceUpdateCanvases();
        TacticalHudView.FitTraitViewport(State.GameManager.TacticalMode);
    }

    public void SetBase()
    {
        Text.enableAutoSizing = false; Text.fontSize = 22;
        if (size != 2) SetCompactLayout();
        initialized = false;
        TacticalUtilities.Log.ShowWeaponCombat = PlayerPrefs.GetInt("LogShowWeaponCombat", 1) == 1;
        TacticalUtilities.Log.ShowOdds = PlayerPrefs.GetInt("LogShowOdds", 0) == 1;
        TacticalUtilities.Log.ShowHealing = PlayerPrefs.GetInt("LogShowHealing", 1) == 1;
        TacticalUtilities.Log.ShowSpells = PlayerPrefs.GetInt("LogShowSpells", 1) == 1;
        TacticalUtilities.Log.ShowMisses = PlayerPrefs.GetInt("LogShowMisses", 1) == 1;
        TacticalUtilities.Log.ShowInformational = PlayerPrefs.GetInt("LogShowInformational", 1) == 1;
        TacticalUtilities.Log.ShowPureFluff = PlayerPrefs.GetInt("LogShowPureFluff", 1) == 1;
        ShowWeaponCombat.isOn = TacticalUtilities.Log.ShowWeaponCombat;
        ShowOdds.isOn = TacticalUtilities.Log.ShowOdds;
        ShowHealing.isOn = TacticalUtilities.Log.ShowHealing;
        ShowSpells.isOn = TacticalUtilities.Log.ShowSpells;
        ShowMisses.isOn = TacticalUtilities.Log.ShowMisses;
        ShowInformational.isOn = TacticalUtilities.Log.ShowInformational;
        ShowFluff.isOn = TacticalUtilities.Log.ShowPureFluff;
        initialized = true;
    }

    public void Refresh()
    {
        if (initialized == false)
            return;
        TacticalUtilities.Log.ShowWeaponCombat = ShowWeaponCombat.isOn;
        TacticalUtilities.Log.ShowOdds = ShowOdds.isOn;
        TacticalUtilities.Log.ShowHealing = ShowHealing.isOn;
        TacticalUtilities.Log.ShowSpells = ShowSpells.isOn;
        TacticalUtilities.Log.ShowMisses = ShowMisses.isOn;
        TacticalUtilities.Log.ShowInformational = ShowInformational.isOn;
        TacticalUtilities.Log.ShowPureFluff = ShowFluff.isOn;
        PlayerPrefs.SetInt("LogShowWeaponCombat", ShowWeaponCombat.isOn ? 1 : 0);
        PlayerPrefs.SetInt("LogShowOdds", ShowOdds.isOn ? 1 : 0);
        PlayerPrefs.SetInt("LogShowHealing", ShowHealing.isOn ? 1 : 0);
        PlayerPrefs.SetInt("LogShowSpells", ShowSpells.isOn ? 1 : 0);
        PlayerPrefs.SetInt("LogShowMisses", ShowMisses.isOn ? 1 : 0);
        PlayerPrefs.SetInt("LogShowInformational", ShowInformational.isOn ? 1 : 0);
        PlayerPrefs.SetInt("LogShowPureFluff", ShowFluff.isOn ? 1 : 0);
        PlayerPrefs.Save();
        TacticalUtilities.Log.RefreshListing();
        RefreshLayout();
    }


    public void Expand()
    {
        if (rect == null) rect = GetComponent<RectTransform>();
        if (size == 0)
        {
            Restore.gameObject.SetActive(false);
            gameObject.SetActive(true);
            SetCompactLayout();
            size = 1;
        }
        else if (size == 1)
        {
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.pivot = new Vector2(.5f, .5f);
            rect.offsetMax = Vector2.zero;
            rect.offsetMin = Vector2.zero;
            size = 2;
            IncreaseSize.interactable = false;
            ShowHealing.gameObject.SetActive(true);
            ShowMisses.gameObject.SetActive(true);
            ShowInformational.gameObject.SetActive(true);
            ShowOdds.gameObject.SetActive(true);
            ShowWeaponCombat.gameObject.SetActive(true);
            ShowFluff.gameObject.SetActive(true);
            ShowSpells.gameObject.SetActive(true);
        }
        RefreshLayout();
    }

    public void Shrink()
    {
        if (rect == null) rect = GetComponent<RectTransform>();
        if (size == 2)
        {
            SetCompactLayout();
            size = 1;
            IncreaseSize.interactable = true;
            ShowHealing.gameObject.SetActive(false);
            ShowMisses.gameObject.SetActive(false);
            ShowInformational.gameObject.SetActive(false);
            ShowOdds.gameObject.SetActive(false);
            ShowWeaponCombat.gameObject.SetActive(false);
            ShowFluff.gameObject.SetActive(false);
            ShowSpells.gameObject.SetActive(false);
        }
        else if (size == 1)
        {
            gameObject.SetActive(false);
            size = 0;
            Restore.gameObject.SetActive(true);
        }
        RefreshLayout();
    }

    void SetCompactLayout()
    {
        if (rect == null) rect = GetComponent<RectTransform>();
        // Width is already supplied by the stretched anchors; adding 1920 doubles it.
        rect.anchorMin = Vector2.zero; rect.anchorMax = new Vector2(1, 0);
        rect.pivot = new Vector2(.5f, 0);
        rect.sizeDelta = new Vector2(0, 257);
        rect.anchoredPosition = new Vector2(0, 100);
    }

    void RefreshLayout()
    {
        // Reuse the normal next-frame scroll-to-latest path after a resize/filter change.
        Canvas.ForceUpdateCanvases();
        TacticalHudView.FitTraitViewport(State.GameManager.TacticalMode);
        State.GameManager.TacticalMode.TacticalLogUpdated = true;
    }

}
