// Modified for the VoreWar mercenary-company conversion.
// 2026-10-30: Restrict the retained nested tag editor to an authorized Race Editor session (spec 02B).
// 2026-11-03: Refresh species tag choices when the developer tag editor closes (spec 22).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using OdinSerializer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UnitTagEditor : MonoBehaviour
{
    public Transform Folder;
    public GameObject UnitTagPrefab;
    internal List<UnitTagListItem> CreatedUnitTags;

    public Button AddBtn;
    internal Button AddBtnInstance;

    public UnitTagMenu UnitTagMenu;

    internal void Open()
    {
        if (!State.GameManager.Menu.IsRaceEditorSessionActive)
            return;
        gameObject.SetActive(true);

        int children = Folder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(Folder.GetChild(i).gameObject);
        }

        Setup();
    }

    private void Setup()
    {
        CreatedUnitTags = new List<UnitTagListItem>();
        foreach (var entry in State.UnitTagList)
        {
            var usererTrait = CreateUnitTagListItem(entry);
            CreatedUnitTags.Add(usererTrait);
        }
        CreateAddButton();
    }

    private void CreateAddButton()
    {
        if (AddBtnInstance != null)
        {
            AddBtnInstance.onClick.RemoveAllListeners();
            Destroy(AddBtnInstance.gameObject);
        }
        AddBtnInstance = Instantiate(AddBtn, Folder);
        var btn = AddBtnInstance.GetComponent<Button>();
        var btnTxt = btn.GetComponentInChildren<Text>();
        btnTxt.text = "Add";
        btn.onClick.AddListener(() =>
        {
            var created = CreateUnitTagListItem();
            CreatedUnitTags.Add(created);
            CreateAddButton();
        });
    }

    private UnitTagListItem CreateUnitTagListItem(UnitTag existingTag = null)
    {
        if (existingTag != null)
        {
            var obj = Instantiate(UnitTagPrefab, Folder);
            var ut = obj.GetComponent<UnitTagListItem>();
            ut.name.text = existingTag.name;
            ut.id = existingTag.id;
            ut.modifybtn.onClick.AddListener(() =>
            {
                UnitTagMenu.Open(ut.id);
            });
            return ut;
        }
        else
        {
            var obj = Instantiate(UnitTagPrefab, Folder);
            var ut = obj.GetComponent<UnitTagListItem>();
            ut.name.text = "NewTag";
            var last = CreatedUnitTags.LastOrDefault();
            ut.id = last == null ? 0 : FindNewId();
            UnitTag newUnitTag = new UnitTag();
            newUnitTag.name = "NewTag";
            newUnitTag.id = ut.id;
            newUnitTag.modifiers = new List<UnitTagModifier>();
            newUnitTag.AssociatedTraits = new List<Traits>();
            State.UnitTagList.Add(newUnitTag);
            ut.modifybtn.onClick.AddListener(() =>
            {
                UnitTagMenu.Open(ut.id);
            });
            return ut;
        }
    }

    private int FindNewId()
    {
        bool taken = true;
        int index = 0;
        while (taken)
        {
            index++;
            taken = CreatedUnitTags.Any(rt => rt.id == (0 + index));
        }
        return 0 + index;
    }

    public void Close()
    {
        TagConditionChecker.CompileTraitTagAssociateDict();
        State.GameManager.Menu.RaceEditor.RefreshUnitTags();
        gameObject.SetActive(false);
        int children = Folder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(Folder.GetChild(i).gameObject);
        }
    }
}
