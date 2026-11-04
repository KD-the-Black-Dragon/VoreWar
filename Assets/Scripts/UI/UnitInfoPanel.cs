// Modified for the VoreWar mercenary-company conversion.
// 2026-10-30: Remove the hidden Unit Editor click route while retaining ordinary inspection (spec 02B).
// 2026-11-02: Preserve item hover information for four tactical equipment readouts (spec 16).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
// 2026-11-03: Compact tactical stat rows and restrict hover to active controls (spec 29 feedback).
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitInfoPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool hovering;

    int prevNames = 0;

    public UIUnitSprite Sprite;
    public TextMeshProUGUI BasicInfo;
    public TextMeshProUGUI InfoText;
    public GameObject StatBlock;
    public Slider ExpBar;
    public Slider HealthBar;
    public Slider BarrierBar;
    public Slider ManaBar;
    public Image PotionHoverImage;
    public Image UnitTagHoverImage;
    internal Unit Unit;
    internal Actor_Unit Actor;
    internal TextMeshProUGUI[] TacticalEquipmentLabels;
    internal RectTransform[] TacticalEquipmentHeaders;
    internal RectTransform[] TacticalEquipmentBoxes;

    private int nameInstances;

    public string HoveringName;

    private void Update()
    {
        if (hovering == false)
            return;

        if (Unit == null)
            return;
        TextMeshProUGUI HoverBox;

        if (Input.mousePosition.y > InfoText.transform.parent.position.y && BasicInfo)
            HoverBox = BasicInfo;
        else
            HoverBox = InfoText;

        if (StatBlock)
        {
            // Actually, I changed my mind. I love coding.
            if (IsMousingOver(StatBlock))
            {
                GameObject STRLabel = StatBlock.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
                GameObject DEXLabel = StatBlock.transform.GetChild(0).GetChild(1).GetChild(0).gameObject;
                GameObject MNDLabel = StatBlock.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
                GameObject WLLLabel = StatBlock.transform.GetChild(1).GetChild(1).GetChild(0).gameObject;
                GameObject ENDLabel = StatBlock.transform.GetChild(2).GetChild(0).GetChild(0).gameObject;
                GameObject AGILabel = StatBlock.transform.GetChild(2).GetChild(1).GetChild(0).gameObject;
                GameObject VORLabel = StatBlock.transform.GetChild(3).GetChild(0).GetChild(0).gameObject;
                GameObject STMLabel = StatBlock.transform.GetChild(3).GetChild(1).GetChild(0).gameObject;
                GameObject LDRLabel = StatBlock.transform.GetChild(4).GetChild(0).GetChild(0).gameObject;
                GameObject E1Label = StatBlock.transform.GetChild(5).GetChild(0).GetChild(0).gameObject;
                GameObject E2Label = StatBlock.transform.GetChild(5).GetChild(1).GetChild(0).gameObject;
                GameObject E3Label = StatBlock.transform.GetChild(5).GetChild(2).GetChild(0).gameObject;

                foreach (var item in new GameObject[] {STRLabel, DEXLabel, MNDLabel, WLLLabel, ENDLabel, AGILabel, VORLabel, STMLabel, LDRLabel, E1Label, E2Label, E3Label})
                {
                    if (IsMousingOver(item))
                        HoverBox = item.GetComponent<TextMeshProUGUI>();
                }
            }
        }

        if (PotionHoverImage != null)
        {
            if (IsMousingOver(PotionHoverImage.gameObject))            
                State.GameManager.HoveringTooltip.UpdateInformationPotion(Unit);
        }

        if (UnitTagHoverImage != null)
        {
            if (IsMousingOver(UnitTagHoverImage.gameObject))
                State.GameManager.HoveringTooltip.UpdateInformationTags(Unit);
        }

        if (TacticalEquipmentLabels != null)
            foreach (var label in TacticalEquipmentLabels)
                if (label != null && IsMousingOver(label.gameObject)) HoverBox = label;

        int wordIndex = TMP_TextUtilities.FindIntersectingWord(HoverBox, Input.mousePosition, null);
        //if (wordIndex <= -1 && BasicInfo)
        //{
        //    wordIndex = TMP_TextUtilities.FindIntersectingWord(BasicInfo, Input.mousePosition, null);
        //    HoverBox = BasicInfo;
        //}
        if (wordIndex > -1)
        {
            string[] words = new string[5];
            for (int i = 0; i < 5; i++)
            {
                if (wordIndex - 2 + i < 0 || wordIndex - 2 + i >= HoverBox.textInfo.wordCount || HoverBox.textInfo.wordInfo[wordIndex - 2 + i].characterCount < 1)
                {
                    words[i] = string.Empty;
                    continue;
                }
                words[i] = HoverBox.textInfo.wordInfo[wordIndex - 2 + i].GetWord();
            }

            State.GameManager.HoveringTooltip.UpdateInformation(words, Unit, Actor);
            if (Input.GetMouseButtonDown(0))
            {
                if (nameInstances <= 1)
                    DisplayInfoFor(HoveringName);
                else
                {
                    prevNames = 0;
                    for (int i = 2; i < wordIndex; i++) //Don't use your own name as the first name
                    {
                        if (HoverBox.textInfo.wordInfo[i].GetWord() == words[2])
                            prevNames += 1;
                        else if (wordIndex + 1 >= HoverBox.textInfo.wordCount && HoveringName == $"{HoverBox.textInfo.wordInfo[i].GetWord()} {HoverBox.textInfo.wordInfo[i + 1].GetWord()}")
                            prevNames += 1;
                        else if (wordIndex + 2 >= HoverBox.textInfo.wordCount && HoveringName == $"{HoverBox.textInfo.wordInfo[i].GetWord()} {HoverBox.textInfo.wordInfo[i + 1].GetWord()} {HoverBox.textInfo.wordInfo[i + 2].GetWord()}")
                            prevNames += 1;
                    }
                    DisplayInfoFor(HoveringName, prevNames);
                }
            }
            else
            {
                nameInstances = 0;
                if (Actor?.Unit != null && (bool)Actor?.Unit.Predator)
                {
                    foreach (var prey in Actor.PredatorComponent.GetAllPrey())
                    {
                        if (prey.Unit.Name == words[2])
                        {
                            State.GameManager.HoveringTooltip.HoveringValidName();
                            HoveringName = words[2];
                            nameInstances += 1;
                        }
                        else if (prey.Unit.Name == $"{words[2]} {words[3]}")
                        {
                            State.GameManager.HoveringTooltip.HoveringValidName();
                            HoveringName = $"{words[2]} {words[3]}";
                            nameInstances += 1;
                        }
                        else if (prey.Unit.Name == $"{words[2]} {words[3]} {words[4]}")
                        {
                            State.GameManager.HoveringTooltip.HoveringValidName();
                            HoveringName = $"{words[2]} {words[3]} {words[4]}";
                            nameInstances += 1;
                        }
                    }
                }
                if (nameInstances == 0)
                    HoveringName = "RAREASFARARA";
            }
        }
    }

    void DisplayInfoFor(string name)
    {
        if (Actor?.Unit.Predator == false || Actor?.PredatorComponent == null)
            return;
        foreach (var prey in Actor.PredatorComponent.GetAllPrey())
        {
            if (prey.Unit.Name == name)
            {
                State.GameManager.TacticalMode.InfoPanel.RefreshTacticalUnitInfo(prey.Actor);
            }
        }
    }

    void DisplayInfoFor(string name, int instance)
    {
        int count = 0;
        if (Actor?.Unit.Predator == false)
            return;
        foreach (var prey in Actor.PredatorComponent.GetAllPrey())
        {
            if (prey.Unit.Name == name)
            {

                if (count == prevNames)
                {
                    State.GameManager.TacticalMode.InfoPanel.RefreshTacticalUnitInfo(prey.Actor);
                    break;
                }
                else
                    count++;
            }
        }
    }

    public bool IsMousingOver(GameObject thing)
    {
        // Hidden legacy stat/equipment labels retain rectangles, but must never be hover targets.
        if (thing == null || !thing.activeInHierarchy) return false;
        Vector3[] v = new Vector3[4];
        thing.GetComponent<RectTransform>().GetWorldCorners(v);
        Rect rect = new Rect(v[0].x, v[0].y, v[2].x - v[0].x, v[2].y - v[0].y);
        return rect.Contains(Input.mousePosition);
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
