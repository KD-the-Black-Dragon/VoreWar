// Modified for the VoreWar mercenary-company conversion.
// 2026-10-31: Retire legacy world load/preview and empire campaign entry routes (spec 05A1).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoad : MonoBehaviour
{
    public SaveSlot[] SaveSlots;
    public SaveLoadInfo SaveInfo;
    public Transform SaveNamesFolder;
    public GameObject DefaultButton;
    public Toggle RequireConfirmation;
    //public Toggle SkipPreviewForHugeFiles; To be implemented later

    private void Start()
    {
        RequireConfirmation.isOn = PlayerPrefs.GetInt("SaveLoadConfirmation", 0) == 1 ? true : false;
    }

    public void ConfirmationChanged()
    {
        PlayerPrefs.SetInt("SaveLoadConfirmation", RequireConfirmation.isOn ? 1 : 0);
    }

    public void InputChanged()
    {
        SaveInfo.LoadGame.interactable = false;
        SaveInfo.DeleteSave.interactable = false;
        SaveInfo.LeftText.text = State.LegacyLoadUnavailableMessage;
        SaveInfo.RightText.text = "";
    }

    public void Save()
    {
        State.Save($"{State.SaveDirectory}{SaveInfo.SavedGameName.text}.sav");
        gameObject.SetActive(false);
        State.GameManager.CloseMenu();
    }

    public void Load()
    {
        State.GameManager.CreateMessageBox(State.LegacyLoadUnavailableMessage);
    }

    private void LoadSave()
    {
        State.Load($"{State.SaveDirectory}{SaveInfo.SavedGameName.text}.sav");
        gameObject.SetActive(false);
        State.GameManager.CloseMenu();
    }

    public void DeleteSave()
    {
        var box = State.GameManager.CreateDialogBox();
        box.SetData(ActuallyDelete, "Delete", "Cancel", "Are you sure you want to delete this saved game?");
    }

    void ActuallyDelete()
    {
        if (File.Exists($"{State.SaveDirectory}{SaveInfo.SavedGameName.text}.sav"))
            File.Delete($"{State.SaveDirectory}{SaveInfo.SavedGameName.text}.sav");
        InputChanged();
        ListSlots();
    }

    internal void ListSlots()
    {
        int children = SaveNamesFolder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(SaveNamesFolder.GetChild(i).gameObject);
        }
        BuildFiles(State.SaveDirectory, "sav");
        bool InMainMenu = State.GameManager.CurrentScene == State.GameManager.Start_Mode;
        SaveInfo.SaveGame.interactable = !InMainMenu;
        InputChanged();
    }

    private void BuildFiles(string directory, string extension)
    {
        if (Directory.Exists(directory) == false)
            Directory.CreateDirectory(directory);
        string[] files = Directory.GetFiles(directory);

        foreach (string file in files)
        {
            if (!File.Exists(file)) continue;

            if (CompatibleFileExtension(file, extension))
            {
                var filename = Path.GetFileNameWithoutExtension(file);

                var button = Instantiate(DefaultButton, SaveNamesFolder).GetComponent<Button>();
                button.GetComponentInChildren<Text>().text = $"{filename}";
                button.GetComponent<Button>().onClick.AddListener(() => SaveInfo.SavedGameName.text = filename);
            }
        }
    }

    public bool CompatibleFileExtension(string file, string extension)
    {
        if (extension.Length == 0)
        {
            return true;
        }

        if (file.EndsWith("." + extension))
        {
            return true;
        }

        // Not found, return not compatible
        return false;
    }


}


