// Modified for the VoreWar mercenary-company conversion.
// 2026-10-30: Gate Race Editor sessions and nested tags; retire obsolete menu routes (spec 02B).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    // Convenience gate only; the project owner accepts source discoverability.
    private const string RaceEditorPassword = "214214214";
    private bool raceEditorUnlocked;
    private InputBox raceEditorPrompt;

    internal bool HasRaceEditorAccess => raceEditorUnlocked;
    internal bool IsRaceEditorSessionActive => raceEditorUnlocked && RaceEditor != null && RaceEditor.gameObject.activeInHierarchy;

    internal void EndRaceEditorSession()
    {
        raceEditorUnlocked = false;
        if (UnitTagEditor != null)
        {
            if (UnitTagEditor.UnitTagMenu != null)
                UnitTagEditor.UnitTagMenu.gameObject.SetActive(false);
            UnitTagEditor.gameObject.SetActive(false);
        }
    }

    internal void ShowRetiredToolMessage()
    {
        State.GameManager.CreateMessageBox("This tool is retired in this conversion.");
    }

    internal bool HandleRaceEditorInput()
    {
        if (!IsRaceEditorSessionActive)
            return false;
        if (Input.GetButtonDown("Menu"))
        {
            if (UnitTagEditor.UnitTagMenu.gameObject.activeSelf)
                UnitTagEditor.UnitTagMenu.DiscardClose();
            else if (UnitTagEditor.gameObject.activeSelf)
                UnitTagEditor.Close();
            else
                RaceEditor.CloseWithoutSave();
        }
        return true;
    }

    public Options Options;
    public GameObject UIPanel;
    public WorldSettings WorldSettingsUI;
    public EmpireSettings EmpireSettingsUI;

    public SaveLoad SaveLoadScreen;
    public CheatMenu CheatMenu;
    public RaceEditorPanel RaceEditor;
    public RandomizerTraitEditor RandomizerTraitEditor;
    public CustomTraitEditor CustomTraitEditor;
    public ConditionalTraitsEditor CondTraitEditor;
    public UnitTagEditor UnitTagEditor;

    public HelpPanel HelpUI;

    public Button SaveLoadButton;
    public Button OpenMapEditorButton;
    public Button WorldSettingsButton;
    public Button EmpireSettingsButton;

    public GameObject HelpScreen;

    public ContentSettings ContentSettings;

    public void UpdateButtons()
    {
        OpenMapEditorButton.gameObject.SetActive(false);
        WorldSettingsButton.gameObject.SetActive(false);
        EmpireSettingsButton.gameObject.SetActive(false);
        SaveLoadButton.interactable = State.TutorialMode == false;
    }

    public void OpenSaveLoadMenu()
    {
        if (State.GameManager.CurrentScene == State.GameManager.MapEditor)
        {
            State.GameManager.CreateMessageBox("Can't access normal game save/load from within the map editor, use the map editor save/load on the left panel");
            return;
        }
        SaveLoadScreen.gameObject.SetActive(true);
        SaveLoadScreen.ListSlots();
    }

    public void OpenCheats()
    {
        ShowRetiredToolMessage();
    }

    public void OpenRaceEditor()
    {
        if (IsRaceEditorSessionActive || raceEditorPrompt != null)
            return;
        raceEditorPrompt = State.GameManager.CreateInputBox();
        raceEditorPrompt.SetData((string value) =>
        {
            raceEditorPrompt = null;
            if (!string.Equals(value, RaceEditorPassword, StringComparison.Ordinal))
            {
                State.GameManager.CreateMessageBox("Incorrect developer password.");
                return;
            }
            raceEditorUnlocked = true;
            RaceEditor.gameObject.SetActive(true);
            RaceEditor.ShowPanel();
        }, "Unlock", "Cancel", "Enter the developer password to open Race Editor.", 64);
        raceEditorPrompt.InputField.contentType = TMP_InputField.ContentType.Password;
        raceEditorPrompt.InputField.text = "";
    }

    public void OpenOptions()
    {
        Options.gameObject.SetActive(true);
        Options.Open();
    }

    public void OpenRandomizerTraits()
    {
        ShowRetiredToolMessage();
    }

    public void OpenCustomTraits()
    {
        ShowRetiredToolMessage();
    }

    public void OpenCondTraits()
    {
        ShowRetiredToolMessage();
    }
    public void OpenUnitTags()
    {
        if (!IsRaceEditorSessionActive)
            return;
        UnitTagEditor.Open();
    }
    public void LoadHelp()
    {
        HelpUI.GenerateButtonsIfNeeded();
        HelpScreen.SetActive(true);
    }

    public void OpenEmpireTraits()
    {
        ShowRetiredToolMessage();
    }

    public void CloseHelp()
    {
        HelpScreen.SetActive(false);
    }

    public void ShowGameSettings()
    {
        ShowRetiredToolMessage();
    }

    public void UpdateDisplay()
    {
        if (HandleRaceEditorInput())
            return;
        Options.RefreshText();
        if (Input.GetButtonDown("Menu"))
        {
            if (HelpScreen.activeSelf)
                CloseHelp();
            else if (State.GameManager.VariableEditor.gameObject.activeSelf)
                State.GameManager.VariableEditor.Close();
            else if (SaveLoadScreen.gameObject.activeSelf)
                SaveLoadScreen.gameObject.SetActive(false);
            else if (Options.gameObject.activeSelf)
                Options.CloseAndSave();
            else if (ContentSettings.gameObject.activeSelf)
                ContentSettings.Exit();
            else
                CloseMenu();
        }

    }

    public void MainMenu()
    {
        if (State.GameManager.CurrentScene == State.GameManager.StrategyMode)
        {
            State.GameManager.StrategyMode.ClearData();
        }
        State.GameManager.SwitchToMainMenu();
        CloseMenu();
    }

    public void ExitGame()
    {
        Application.Quit();
        CloseMenu();
    }

    public void CloseMenu()
    {
        State.GameManager.CloseMenu();
        if (State.GameManager.CurrentScene == State.GameManager.StrategyMode)
        {
            State.GameManager.StrategyMode.Regenerate();
        }
        if (State.GameManager.CurrentScene == State.GameManager.TacticalMode)
        {
            State.GameManager.TacticalMode.RebuildInfo();
        }
    }



}
