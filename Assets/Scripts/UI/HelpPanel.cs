// Modified for the VoreWar mercenary-company conversion.
// 2026-11-02: Present company help chapters and evaluate trait/spell references only when selected (spec 12).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HelpPanel : MonoBehaviour
{
    bool generatedHelp;
    public Transform ButtonFolder;
    public GameObject[] HelpPages;
    public GameObject ButtonPrefab;

    public TextMeshProUGUI Text;

    public void GenerateButtonsIfNeeded()
    {
        if (generatedHelp)
            return;
        CreatePageButton(HelpText.GameBasics, "Game Basics");
        CreatePageButton(HelpText.StrategyMode, "World Map and Days");
        CreatePageButton(HelpText.RecruitMode, "Company and Wages");
        CreatePageButton(HelpText.TacticalMode, "Tactical Mode");
        CreatePageButton(HelpText.Stats, "Character Stats");
        CreatePageButton(HelpText.Items, "Items");
        CreatePageButton(HelpText.Vore, "Vore");
        CreatePageButton(HelpText.Experience, "Experience");
        CreatePageButton(HelpText.Controls, "Controls");
        CreatePageButton(HelpText.Teams, "Injuries and Recovery");
        CreatePageButton(HelpText.Diplomacy, "Contracts and Reputation");
        CreatePageButton(HelpText.Saving, "Saving and Ironman");
        CreatePageButton(HelpText.Legal, "Project and License");
        CreatePageButton(HelpText.Traits, "Traits");
        CreatePageButton(HelpText.Spells, "Spells");
        Text.text = HelpText.GameBasics;



        Button exitButton = Instantiate(ButtonPrefab, ButtonFolder).GetComponent<Button>();
        exitButton.GetComponentInChildren<Text>().text = "Exit Help";
        exitButton.onClick.AddListener(FindObjectOfType<GameMenu>().CloseHelp);
        generatedHelp = true;

    }

    void CreatePageButton(string pageText, string pageName) { CreatePageButton(() => pageText, pageName); }
    void CreatePageButton(Func<string> pageText, string pageName)
    {
        Button button = Instantiate(ButtonPrefab, ButtonFolder).GetComponent<Button>();
        button.GetComponentInChildren<Text>().text = pageName;
        button.onClick.AddListener(() => Text.text = pageText());
    }
}
