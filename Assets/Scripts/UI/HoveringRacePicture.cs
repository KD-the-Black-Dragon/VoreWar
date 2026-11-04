// Modified for the VoreWar mercenary-company conversion.
// 2026-10-30: Use the requested metadata species for hover; restrict unapproved previews to the gated editor (spec 03A2).
// 2026-10-30: Block permanently retired species even during developer inspection (spec 03B1).
// 2026-11-03: Match species hover-card chrome to the selected palette without changing unit sprites (spec 28).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoveringRacePicture : MonoBehaviour
{
    TextMeshProUGUI text;
    RectTransform rect;
    int remainingFrames = 0;
    Race LastRace = Race.none;
    public UIUnitSprite ActorSprite;
    Actor_Unit Actor;
    float lastUpdate;

    private void Start()
    {
        UITheme.StylePopup(gameObject);
        rect = GetComponent<RectTransform>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Update()
    {
        if (remainingFrames > 0)
            remainingFrames--;
        else
            gameObject.SetActive(false);
    }

    public void UpdateInformation(Race race)
    {
        bool developerInspection = State.GameManager.Menu.IsRaceEditorSessionActive;
        if ((int)race < 0 || !System.Enum.IsDefined(typeof(Race), race) || SpeciesPolicy.IsRetired(race) ||
            (!SpeciesPolicy.IsEnabled(race) && !developerInspection))
        {
            gameObject.SetActive(false);
            return;
        }
        if (rect == null)
            rect = GetComponent<RectTransform>();
        if (text == null)
            text = GetComponentInChildren<TextMeshProUGUI>();
        if (Actor == null || LastRace != race)
        {
            Actor = new Actor_Unit(new Unit(race));
            Actor.Unit.TotalRandomizeAppearance();
            ActorSprite.UpdateSprites(Actor);
            ActorSprite.Name.text = race.ToString();
            var images = ActorSprite.GetComponentsInChildren<Image>();
            foreach (Image image in images)
            {
                image.raycastTarget = false;
            }
            LastRace = race;
        }
        gameObject.SetActive(true);
        remainingFrames = 3;
        text.text = "";
        float xAdjust = 10;
        float exceeded = Input.mousePosition.x + (rect.rect.width * Screen.width / 1920) - Screen.width;
        if (exceeded > 0)
            xAdjust = -exceeded;
        float yAdjust = 0;
        exceeded = Input.mousePosition.y - (rect.rect.height * Screen.height / 1080);
        if (exceeded < 0)
            yAdjust = -exceeded;
        transform.position = Input.mousePosition + new Vector3(xAdjust, yAdjust, 0);
    }
}
