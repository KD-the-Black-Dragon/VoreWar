// Modified for the VoreWar mercenary-company conversion.
// 2026-11-01: Route company creator controls to their own customizer (spec 07).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using TMPro;
using UnityEngine;


public class CustomizerPanel : MonoBehaviour
{
    [System.NonSerialized] internal UnitCustomizer CompanyCustomizer;
    public UIUnitSprite DisplayedSprite;
    public GameObject ButtonPanel;
    public GameObject ButtonPrefab;
    public CustomizationLoaderWindow CustomizationLoader;
    public TMP_Dropdown Gender;
    public TMP_InputField Nominative;
    public TMP_InputField Accusative;
    public TMP_InputField PronominalPossessive;
    public TMP_InputField PredicativePossessive;
    public TMP_InputField Reflexive;
    public TMP_Dropdown Quantification;

    public virtual void ChangeGender()
    {
        if (CompanyCustomizer != null) { CompanyCustomizer.ChangeGender(); return; }
        if (CustomizationLoader.EnteredFromUnitEditor)
            State.GameManager.UnitEditor?.UnitEditor?.ChangeGender();
        else
            State.GameManager.Recruit_Mode?.Customizer?.ChangeGender();
    }

    public virtual void ChangePronouns()
    {
        if (CompanyCustomizer != null) { CompanyCustomizer.ChangePronouns(); return; }
        if (CustomizationLoader.EnteredFromUnitEditor)
            State.GameManager.UnitEditor?.UnitEditor?.ChangePronouns();
        else
            State.GameManager.Recruit_Mode?.Customizer?.ChangePronouns();
    }

    public virtual void ChangeName(Actor_Unit actor)
    {
        if (CustomizationLoader.EnteredFromUnitEditor)
            State.GameManager.UnitEditor?.UnitEditor?.ChangeGender();
        else
            State.GameManager.Recruit_Mode?.Customizer?.ChangeGender();
    }
}
