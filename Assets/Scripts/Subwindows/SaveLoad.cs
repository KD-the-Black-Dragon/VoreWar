// Modified for the VoreWar mercenary-company conversion.
// 2026-10-31: Retire legacy world load/preview and empire campaign entry routes (spec 05A1).
// 2026-11-03: Theme save/load panels and generated slot buttons (spec 17).
// 2026-11-03: Retain legacy scene bindings only as forwarding routes to company save/load (spec 19).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
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

    void OpenNew() { gameObject.SetActive(false); CompanySaveMenu.Show(State.GameManager.Start_Mode.CompanySaveView); }
    public void ConfirmationChanged() { }
    public void InputChanged() { }
    public void Save() { OpenNew(); }
    public void Load() { OpenNew(); }
    public void DeleteSave() { OpenNew(); }
    internal void ListSlots() { OpenNew(); }
    public bool CompatibleFileExtension(string file, string extension) { return false; }
}
