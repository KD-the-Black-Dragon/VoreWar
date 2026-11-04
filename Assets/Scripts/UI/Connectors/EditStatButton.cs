// Modified for the VoreWar mercenary-company conversion.
// 2026-11-03: Implement four active stats and agreed combat rules (spec 29).
// See CHANGES.md and License.txt for GPL-3.0 terms.
using System;
using UnityEngine;
using UnityEngine.UI;

public class EditStatButton : MonoBehaviour
{
    public Text Label;
    public Stat Stat;
    public Unit Unit;
    private UnitEditorPanel Parent;
    public string defaultText;
    public Button Increase;
    public Button IncreaseLevel;
    public Button Decrease;
    public Button DecreaseLevel;

    internal void SetData(Stat stat, int value, Action<Stat, int> statAction, Action<Stat, int> levelAction, Action<Stat, int> manualSetAction, Unit unit, UnitEditorPanel parent)
    {
        Label.text = $"{WarbandStats.Name(stat)}\n{value.ToString()}";
        defaultText = $"{WarbandStats.Name(stat)}\n{value.ToString()}";
        Stat = stat;
        Unit = unit;
        Parent = parent;
        //Increase.onClick.AddListenerOnce(() => statAction(stat, 1));
        //IncreaseLevel.onClick.AddListenerOnce(() => levelAction(stat, 1));
        //Decrease.onClick.AddListenerOnce(() => statAction(stat, -1));
        //DecreaseLevel.onClick.AddListenerOnce(() => levelAction(stat, -1));
        var button = Label.gameObject.AddComponent<Button>();
        button.onClick.AddListener(() => manualSetAction(stat, 0));
        if (Stat == Stat.Leadership && Unit.Type != UnitType.Leader)
        {
            Label.text = $"{WarbandStats.Name(Stat)}\nN/A";
            defaultText = $"{WarbandStats.Name(Stat)}\nN/A";
        }

    }

    public void UpdateLabel()
    {
        Label.text = $"{WarbandStats.Name(Stat)}\n{Unit.GetStat(Stat)}";
        defaultText = $"{WarbandStats.Name(Stat)}\n{Unit.GetStat(Stat)}";
        if (Stat == Stat.Leadership && Unit.Type != UnitType.Leader)
        {
            Label.text = $"{WarbandStats.Name(Stat)}\nN/A";
            defaultText = $"{WarbandStats.Name(Stat)}\nN/A";
        }
    }

    public void UpdateStat(int baseNum)
    {
        if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.LeftShift) || UnityEngine.Input.GetKey(UnityEngine.KeyCode.RightShift))
            baseNum *= 5;
        Unit.ModifyStat(Stat.GetHashCode(), baseNum);
        Parent.UpdateButtons();
    }

    public void ChangeLevel(int change)
    {
        if (change > 0)
        {
            Unit.SetExp(Unit.ExperienceRequiredForNextLevel);
            Unit.LevelUp(Stat);
        }
        else
        {
            Unit.LevelDown(Stat);
            Unit.SetExp(Unit.GetExperienceRequiredForLevel(Unit.Level - 1));
        }
        Parent.UpdateButtons();
    }
}
