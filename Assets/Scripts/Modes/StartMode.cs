// Modified for the VoreWar mercenary-company conversion.
// 2026-10-30: Retire startup authoring/tutorial/map routes and route trait warning through the gate (spec 02B).
// 2026-10-30: Guard the empty approved roster before entering old campaign setup (spec 03A1).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using UnityEngine;
using UnityEngine.UI;

public class StartMode : SceneBase
{
    public Text versionNumber;
    public Text miscText;

    public Button Preset1;
    public Button Preset2;
    public Button Preset3;

    public Button TagAlertButton;
    public Transform TaggedTraitFolder;
    public GameObject TaggedTraitObject;

    public TaggedTraitPrefab TaggedTraitPrefab;

    public void Start()
    {
        State.GameManager.Menu.Options.LoadFromStored();
        State.GameManager.Menu.CheatMenu.LoadFromStored();
        versionNumber.text = $"Version: {State.Version}";
        State.GameManager.Menu.ContentSettings.Refresh();
        Preset1.onClick.AddListener(() => SetPreset(1));
        Preset2.onClick.AddListener(() => SetPreset(2));
        Preset3.onClick.AddListener(() => SetPreset(3));
        State.LoadRaceData();
        RunTaggedTraitCheck();
    }

    public CreateStrategicGame CreateStrategicGame;
    public void CreateStrategic()
    {
        if (SpeciesPolicy.GetAvailable(SpeciesUse.Leader).Length == 0)
        {
            State.GameManager.CreateMessageBox(SpeciesPolicy.UnavailableMessage);
            return;
        }

        UI.SetActive(false);
        CreateStrategicGame.gameObject.SetActive(true);
        CreateStrategicGame.ClearState();
    }

    public CreateTacticalGame CreateTacticalGame;
    public void CreateTactical()
    {
        UI.SetActive(false);
        CreateTacticalGame.gameObject.SetActive(true);
        CreateTacticalGame.Open();

    }

    private void SetPreset(int value)
    {
        State.ChangeRaceSlotUsed(value);
        UpdatePresetsVisible();
    }

    public void UpdatePresetsVisible()
    {
        if (State.RaceSlot <= 1)
        {
            Preset1.interactable = false;
            Preset2.interactable = true;
            Preset3.interactable = true;
        }
        else if (State.RaceSlot == 2)
        {
            Preset1.interactable = true;
            Preset2.interactable = false;
            Preset3.interactable = true;
        }
        if (State.RaceSlot == 3)
        {
            Preset1.interactable = true;
            Preset2.interactable = true;
            Preset3.interactable = false;
        }
    }

    public void ChangeAssimilableTraits()
    {
        State.GameManager.Menu.ShowRetiredToolMessage();
    }

    public void ChangeRandomizableTraits()
    {
        State.GameManager.Menu.ShowRetiredToolMessage();
    }

    public void ModifyCustomTraits()
    {
        State.GameManager.Menu.ShowRetiredToolMessage();
    }

    public void ModifyCondTraits()
    {
        State.GameManager.Menu.ShowRetiredToolMessage();
    }

    public void ReturnToStart()
    {
        if (State.GameManager.Menu.RaceEditor.gameObject.activeSelf)
            State.GameManager.Menu.RaceEditor.CloseWithoutSave();
        State.TutorialMode = false;
        State.GameManager.TutorialScript = null;
        UI.SetActive(true);
        CreateStrategicGame.gameObject.SetActive(false);
        CreateTacticalGame.gameObject.SetActive(false);
        State.GameManager.Menu.ContentSettings.Refresh();
    }

    public void TutorialMode()
    {
        State.GameManager.Menu.ShowRetiredToolMessage();
    }

    public void LoadGame()
    {
        State.GameManager.OpenSaveLoadMenu();
    }

    public void MapEditor()
    {
        State.GameManager.Menu.ShowRetiredToolMessage();
    }

    public override void ReceiveInput()
    {
        State.GameManager.Menu.HandleRaceEditorInput();
        //Vector2 currentMousePos = GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
    }

    public override void CleanUp()
    {
    }


    public void RunTaggedTraitCheck()
    {
        TagAlertButton.gameObject.SetActive(State.UntaggedTraits.Count > 0);
        CloseTaggedTraitCheck();
    }

    public void OpenTaggedTraitCheck()
    {
        State.GameManager.Menu.OpenRaceEditor();
    }

    public void CloseTaggedTraitCheck()
    {
        TaggedTraitFolder.gameObject.SetActive(false);
        TaggedTraitObject.gameObject.SetActive(false);
    }


}
