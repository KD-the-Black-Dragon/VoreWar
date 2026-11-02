// Added by the VoreWar mercenary-company conversion fork.
// 2026-11-01: Leader creator, recruitment, canonical roster/debt and nine-slot commerce views (specs 07–08).
// 2026-11-01: Host mission/result views, preserve clear deadline decisions and exclude enemy-only natural weapons from shops (specs 09–10).
// 2026-11-01: Use singular species names in creation, recruitment and company views (09–10 feedback).
// 2026-11-01: Expose saved injury counts and detailed recovery/treatment from the canonical roster and former-member inspection (spec 11).
// 2026-11-01: Give actual departures a distinct OK-only notice with no payment actions or waiting reassurance (spec 11 feedback).
// 2026-11-02: Keep expired-contract acknowledgments separate from wage-payment decisions (spec 12).
// 2026-11-02: Show XP progress toward the next level rather than lifetime totals (spec 13 feedback).
// 2026-11-02: Keep town services alongside their pages and surface roster XP/readiness with direct level-up actions (spec 14).
// 2026-11-02: Enlarge and center paper-doll previews while compensating for their renderer offset (portrait feedback).
// 2026-11-02: Correct portrait magnification/alignment after visual feedback (spec 15D).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using System;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

internal sealed class CompanyManagementUI
{
    readonly CompanyCampaignView view;
    internal readonly GameObject Page;
    CompanyCharacter draft;
    CompanyCampaign draftWorld;
    CustomizerPanel creator;
    int offset;
    bool townLayout;
    public CompanyManagementUI(CompanyCampaignView owner, Transform parent)
    { view = owner; Page = view.Panel("People and economy", parent); }
    CompanyCampaign State { get { return view.Campaign; } }
    internal bool IsCreatingLeader { get { return draft != null && creator != null && creator.gameObject.activeInHierarchy; } }
    internal void ResumeCreator() { Creator(); }
    internal void Clear(string title, bool townServices = false, bool campaign = true)
    {
        townLayout = townServices;
        view.ShowManagement(townServices, campaign);
        foreach (Transform child in Page.transform) { child.gameObject.SetActive(false); UnityEngine.Object.Destroy(child.gameObject); }
        view.Label(Page.transform, title, townServices ? 250 : 30, 90, townServices ? 980 : 1180, 42, 25);
    }
    float ContentX(float x) { return townLayout ? 250 + (x - 30) * .81f : x; }
    float ContentWidth(float w) { return townLayout ? w * .81f : w; }
    internal void Text(string text, float x, float y, float w, float h, int size = 18)
    { view.Label(Page.transform, text, ContentX(x), y, ContentWidth(w), h, size); }
    internal void Button(string text, float x, float y, float w, Action callback, bool enabled = true)
    { view.Action(Page.transform, text, ContentX(x), y, ContentWidth(w), callback).interactable = enabled; }
    void Portrait(CompanyCharacter character, float x, float y, float scale = .9f)
    {
        var go = view.Panel("Character portrait", Page.transform); view.Place(go, ContentX(x) + 38, y + 22 + 30 * scale, 0, 0);
        go.transform.localScale = new Vector3(scale, scale, 1);
        var display = go.AddComponent<UIUnitSprite>();
        display.Name = view.Label(go.transform, "", 0, 0, 1, 1);
        display.SetIndex(-1);
        var actor = new Actor_Unit(character.Unit); actor.UpdateBestWeapons(); display.UpdateSprites(actor);
        foreach (var image in go.GetComponentsInChildren<Image>()) image.raycastTarget = false;
    }
    void Change(Action operation, Action redraw)
    {
        try { view.Mutate(operation); }
        catch { redraw(); throw; } // Rollback replaces object instances; rebuild callbacks from stable IDs.
        redraw();
    }
    public void StartCreator(bool ironman)
    {
        if (CompanyEconomy.Sapients.Length == 0) throw new InvalidOperationException(SpeciesPolicy.UnavailableMessage);
        draftWorld = CompanyCampaign.Generate(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0), ironman);
        MakeDraft(CompanyEconomy.Sapients[0]);
    }
    void MakeDraft(Race race)
    {
        int focus = race == Race.Avians ? 1 : race == Race.Lizards ? 2 : 0;
        draft = CompanyEconomy.Create(draftWorld, race, focus, true);
        Creator();
    }
    void Focus(int value)
    {
        draft.Focus = value;
        for (int i = 5; i < 9; i++) draft.Unit.SetItem(null, i);
        draft.Unit.SetItem(CompanyEquipment.Create(value == 1 ? "weapon.bow" : "weapon.mace"), value == 1 ? 6 : 5);
        if (value == 2) draft.Unit.SetItem(CompanyEquipment.Create("spell.bolt"), 7);
        draft.Unit.UpdateSpells(); Creator();
    }
    void Creator()
    {
        Clear("Create your company leader", false, false);
        var races = CompanyEconomy.Sapients;
        for (int i = 0; i < races.Length; i++) { Race race = races[i]; Button(SpeciesPolicy.DisplayName(race), 30 + i * 160, 140, 150, () => MakeDraft(race)); }
        Button("Melee focus", 560, 140, 165, () => Focus(0));
        Button("Ranged focus", 735, 140, 165, () => Focus(1));
        Button("Magic focus", 910, 140, 165, () => Focus(2));
        Text("Current focus: " + new[] { "Melee", "Ranged", "Magic" }[draft.Focus] + ". Edit the portrait/name below. Name and appearance lock when confirmed. Scroll the appearance panel for more controls.", 30, 187, 1200, 35, 17);
        // Clone the existing appearance controls; never activate the retired recruitment mode.
        var original = global::State.GameManager.Recruit_Mode.CustomizerUI;
        var viewport = view.Panel("Appearance viewport", Page.transform); view.Place(viewport, 25, 230, 1230, 320);
        viewport.AddComponent<RectMask2D>(); viewport.AddComponent<Image>().color = new Color(.1f, .12f, .15f);
        var scroll = viewport.AddComponent<ScrollRect>(); scroll.horizontal = false; scroll.movementType = ScrollRect.MovementType.Clamped; scroll.scrollSensitivity = 35;
        var content = view.Panel("Appearance content", viewport.transform); view.Place(content, 0, 0, 1230, 800);
        scroll.content = content.GetComponent<RectTransform>(); scroll.viewport = viewport.GetComponent<RectTransform>();
        var clone = UnityEngine.Object.Instantiate(original.gameObject, content.transform, false);
        view.Place(clone, 0, 0, 1230, 800);
        clone.SetActive(true); creator = clone.GetComponent<CustomizerPanel>();
        foreach (var button in clone.GetComponentsInChildren<Button>(true))
        {
            button.onClick = new Button.ButtonClickedEvent();
            if (button.gameObject != creator.DisplayedSprite.Name.gameObject) button.gameObject.SetActive(false);
        }
        creator.DisplayedSprite.SetIndex(-1);
        creator.Gender.onValueChanged = new TMP_Dropdown.DropdownEvent();
        creator.Gender.onValueChanged.AddListener(_ => { if (creator.CompanyCustomizer != null) creator.CompanyCustomizer.ChangeGender(); });
        foreach (var field in new[] { creator.Nominative, creator.Accusative, creator.PronominalPossessive, creator.PredicativePossessive, creator.Reflexive })
        {
            field.onValueChanged = new TMP_InputField.OnChangeEvent();
            field.onEndEdit = new TMP_InputField.SubmitEvent();
            field.onEndEdit.AddListener(_ => { if (creator.CompanyCustomizer != null) creator.CompanyCustomizer.ChangePronouns(); });
        }
        creator.Quantification.onValueChanged = new TMP_Dropdown.DropdownEvent();
        creator.Quantification.onValueChanged.AddListener(_ => { if (creator.CompanyCustomizer != null) creator.CompanyCustomizer.ChangePronouns(); });
        creator.CompanyCustomizer = new UnitCustomizer(draft.Unit, creator);
        foreach (var canvas in clone.GetComponentsInChildren<Canvas>(true)) canvas.overrideSorting = false;
        Text("Leader name", 550, 550, 620, 24, 16);
        var nameGo = view.Panel("Leader name", Page.transform); view.Place(nameGo, 550, 575, 650, 38);
        nameGo.AddComponent<Image>().color = new Color(.2f, .25f, .3f);
        var nameField = nameGo.AddComponent<InputField>();
        nameField.textComponent = view.Label(nameGo.transform, draft.Unit.Name, 8, 2, 630, 34, 18);
        nameField.characterLimit = 100; nameField.text = draft.Unit.Name;
        nameField.onEndEdit.AddListener(value => { draft.Unit.Name = value.Trim(); creator.DisplayedSprite.Name.text = draft.Unit.Name; });
        var nameButton = creator.DisplayedSprite.Name.GetComponent<Button>();
        nameButton.onClick = new Button.ButtonClickedEvent();
        nameButton.onClick.AddListener(nameField.ActivateInputField);
        Button("Confirm leader and begin", 30, 575, 300, () =>
        {
            draft.Unit.Name = nameField.text.Trim();
            if (string.IsNullOrWhiteSpace(draft.Unit.Name)) throw new InvalidOperationException("Choose a leader name first.");
            byte[] before = CompanyCampaignSave.Encode(draftWorld);
            try { CompanyEconomy.Begin(draftWorld, draft); draftWorld.Validate(); }
            catch
            {
                CompanyCampaignSave.Restore(draftWorld, before);
                draft.Employment = CompanyEmployment.Candidate; draft.TownId = null;
                throw;
            }
            view.Install(draftWorld); draft = null; draftWorld = null;
        });
        Button("Cancel", 350, 575, 160, view.ShowSetup);
    }
    CompanyTown Town()
    {
        var town = State.Towns.Find(t => t.Tile == State.Position);
        if (town == null) throw new InvalidOperationException("Visit a town first.");
        return town;
    }
    public void Recruit(int page = 0)
    {
        offset = Math.Max(0, page); var town = Town(); Clear("Recruitment — " + town.Name, true);
        var offers = CompanyEconomy.Offers(State, town).ToArray();
        Text("Gold " + State.Gold + " | Advance wages cover the remaining week. Former members also require full old debt.", 30, 136, 1200, 40);
        for (int i = 0; i < 4 && offset + i < offers.Length; i++)
        {
            var c = offers[offset + i]; int y = 190 + i * 84;
            Portrait(c, 30, y);
            Text(c.Unit.Name + " — " + SpeciesPolicy.DisplayName(c.Unit.Race) + "  Lv" + c.Unit.Level + "  " + CompanyEconomy.Wage(State, c) + "/week\n" + c.Biography + (c.Employment == CompanyEmployment.Former ? " [Former member; debt " + c.Debt + "]" : ""), 150, y, 680, 77);
            int price = CompanyEconomy.HirePrice(State, c);
            Button("Details", 850, y, 150, () => CandidateDetails(c.Id));
            Button("Hire: " + price, 1020, y, 190, () => Change(() => CompanyEconomy.Hire(State, c, town), () => Recruit(offset)), State.Gold >= price && !State.Busy);
        }
        Button("Previous", 30, 550, 160, () => Recruit(offset - 4), offset > 0);
        Button("Next", 205, 550, 160, () => Recruit(offset + 4), offset + 4 < offers.Length);
    }
    public void Company(int page = 0)
    {
        offset = Math.Max(0, page); Clear("Company roster");
        var roster = State.Members.Select(m => State.Characters.Find(c => c.Id == m.CharacterId)).ToArray();
        Text("Gold " + State.Gold + " | Next payday wages " + roster.Sum(c => CompanyEconomy.Wage(State, c)) + " | Debt " + roster.Sum(c => c.Debt), 30, 136, 1180, 40);
        for (int i = 0; i < 5 && offset + i < roster.Length; i++)
        {
            var c = roster[offset + i]; var member = State.Members.Find(m => m.CharacterId == c.Id); int y = 180 + i * 82;
            bool leader = c.Id == State.LeaderId, ready = c.Unit.HasEnoughExpToLevelUp();
            Portrait(c, 30, y);
            string progress = c.Unit.CurrentLevelExperience.ToString("0.#") + "/" + c.Unit.CurrentLevelExperienceRequired;
            string status = "XP " + progress + (ready ? " <color=#F4D47B>LEVEL UP READY</color>" : "");
            Text(c.Unit.Name + " — " + SpeciesPolicy.DisplayName(c.Unit.Race) + " Lv" + c.Unit.Level + (leader ? " [Leader]" : "") +
                "\nHP " + c.Unit.Health + "/" + c.Unit.MaxHealth + " | " + status +
                "\nInjuries " + c.Unit.Injuries.Count(CompanyInjuries.Temporary) + " | Old " + c.Unit.Injuries.Count(injury => !CompanyInjuries.Temporary(injury)) +
                " | Wage " + CompanyEconomy.Wage(State, c) + "/week | Debt " + c.Debt, 112, y, 605, 77, 17);
            Button(leader ? "Always active" : member.Reserve ? "Reserve" : "Active", 735, y, 130, () => Change(() => member.Reserve = !member.Reserve, () => Company(offset)), !leader && !State.InBattle && !State.Busy);
            Button("Manage", 880, y, 140, () => Member(c.Id));
            Button(ready ? "Level up!" : "Level up", 1035, y, 180, () => ChooseLevel(c.Id), ready && !State.Busy);
            Button("Pay " + c.Debt, 735, y + 40, 130, () => Change(() => CompanyEconomy.Pay(State, c), () => Company(offset)), c.Debt > 0 && State.Gold >= c.Debt);
            Button("Injuries", 880, y + 40, 140, () => view.Recovery.Injuries(c.Id));
        }
        Button("Previous", 30, 600, 155, () => Company(offset - 5), offset > 0);
        Button("Next", 200, 600, 155, () => Company(offset + 5), offset + 5 < roster.Length);
    }

    public void Member(string id)
    {
        var c = State.Characters.Find(x => x.Id == id); if (c == null) throw new InvalidOperationException("Member is unavailable.");
        Clear(c.Unit.Name + " — loadout and progression");
        Text(c.Biography + "\nHP " + c.Unit.Health + "/" + c.Unit.MaxHealth + " | Debt " + c.Debt + " | Next weekly wage " + CompanyEconomy.Wage(State, c) + " | XP " + c.Unit.CurrentLevelExperience.ToString("0.#") + "/" + c.Unit.CurrentLevelExperienceRequired + "\n" + Stats(c.Unit), 30, 136, 1170, 75);
        for (int i = 0; i < 9; i++)
        {
            int slot = i; var item = c.Unit.Items[i]; int x = 30 + (i / 5) * 610, y = 220 + (i % 5) * 57;
            Text(((CompanySlot)i) + ": " + (item == null ? "Empty" : item.Name), x, y, 375, 45, 17);
            Button(item == null ? "Equip" : "Change / remove", x + 385, y, 200, () => Slot(c.Id, slot), !State.InBattle && !State.Busy);
        }
        if (c.Id != State.LeaderId)
        {
            Button("Nickname", 30, 535, 160, () => Nickname(c.Id), !State.Busy);
            Button("Dismiss", 205, 535, 160, () => ConfirmDismiss(c.Id), !State.Busy);
        }
        Button("Level up", 390, 535, 180, () => ChooseLevel(id), c.Unit.HasEnoughExpToLevelUp() && !State.Busy);
        Button("Injuries / recovery", 590, 535, 245, () => view.Recovery.Injuries(id));
    }
    static string Stats(Unit unit)
    {
        return "STR " + unit.GetStat(Stat.Strength) + "  DEX " + unit.GetStat(Stat.Dexterity) + "  MIND " + unit.GetStat(Stat.Mind) +
            "  WILL " + unit.GetStat(Stat.Will) + "  END " + unit.GetStat(Stat.Endurance) + "  AGI " + unit.GetStat(Stat.Agility) +
            "  VOR " + unit.GetStat(Stat.Voracity) + "  STOM " + unit.GetStat(Stat.Stomach);
    }
    void ChooseLevel(string id)
    {
        var c = State.Characters.Find(x => x.Id == id);
        if (c.LevelChoices == null) view.Mutate(() => CompanyEconomy.LevelChoices(State, c));
        Clear("Choose a level-up improvement — " + c.Unit.Name);
        Text(Stats(c.Unit), 30, 140, 1170, 60);
        var choices = c.LevelChoices ?? new Stat[0];
        for (int i = 0; i < choices.Length; i++)
        {
            Stat stat = choices[i];
            Button(stat.ToString(), 30 + i % 4 * 285, 230 + i / 4 * 70, 270,
                () => Change(() => CompanyEconomy.Level(State, c, stat), () => Company(offset)), !State.Busy);
        }
    }
    void CandidateDetails(string id)
    {
        var c = State.Characters.Find(x => x.Id == id); Clear(c.Unit.Name + " — " + SpeciesPolicy.DisplayName(c.Unit.Race), true);
        Portrait(c, 1070, 150, 1.6f);
        string traits = string.Join(", ", c.Unit.GetTraits.Select(t => t.ToString()).ToArray());
        string gear = string.Join("\n", c.Unit.Items.Select((item, i) => ((CompanySlot)i) + ": " + (item == null ? "Empty" : item.Name)).ToArray());
        Text(c.Biography + "\n\n" + Stats(c.Unit) + "\nTraits: " + traits + "\n" + CompanyInjuries.Summary(c.Unit) + "\n\n" + gear, 30, 150, 1010, 370);
    }
    void Nickname(string id)
    {
        var c = State.Characters.Find(x => x.Id == id); Clear("Give a nickname");
        var go = view.Panel("Nickname", Page.transform); view.Place(go, 30, 180, 650, 50); go.AddComponent<Image>().color = new Color(.2f,.25f,.3f);
        var field = go.AddComponent<InputField>(); var text = view.Label(go.transform, c.Unit.Name, 8, 5, 630, 40); field.textComponent = text; field.characterLimit = 100; field.text = c.Unit.Name;
        Button("Save nickname", 30, 280, 220, () =>
        {
            if (string.IsNullOrWhiteSpace(field.text)) throw new InvalidOperationException("Enter a nickname first.");
            Change(() => c.Unit.Name = field.text.Trim(), () => Member(id));
        });
        Button("Cancel", 270, 280, 180, () => Member(id));
    }
    void ConfirmDismiss(string id)
    {
        var c = State.Characters.Find(x => x.Id == id); Clear("Dismiss " + c.Unit.Name + "?");
        Text("They keep their equipment and any debt owed. They may seek work in a town again; this is not a death.", 30, 170, 1150, 100);
        Button("Confirm dismissal", 30, 320, 250, () => Change(() => CompanyEconomy.Leave(State, c), () => Company()));
        Button("Cancel", 300, 320, 180, () => Member(id));
    }
    void Slot(string id, int slot, int page = 0)
    {
        var c = State.Characters.Find(x => x.Id == id); Clear(c.Unit.Name + " — " + (CompanySlot)slot);
        var bag = State.Inventory.Where(item => CompanyEquipment.CanPlace(item, slot) && !State.Characters.Any(who => who.Unit.Items.Contains(item))).ToArray();
        for (int i = 0; i < 5 && page + i < bag.Length; i++)
        {
            Item item = bag[page + i]; int y = 175 + i * 63;
            Text(item.Name + " — " + item.Description, 30, y, 900, 55);
            Button("Equip", 1010, y, 180, () => Change(() => CompanyEquipment.Equip(State, c, item, slot), () => Member(id)));
        }
        Button("Remove current", 30, 520, 220, () => Change(() => CompanyEquipment.Equip(State, c, null, slot), () => Member(id)), c.Unit.Items[slot] != null);
        Button("Previous", 270, 520, 160, () => Slot(id, slot, page - 5), page > 0);
        Button("Next", 450, 520, 160, () => Slot(id, slot, page + 5), page + 5 < bag.Length);
        Button("Loadout", 640, 520, 160, () => Member(id));
    }
    public void Shop(int page = 0)
    {
        Town(); Clear("Merchant — gold " + State.Gold, true);
        var definitions = CompanyEquipment.Catalog.Where(d => !d.EnemyOnly).ToArray();
        for (int i = 0; i < 5 && page + i < definitions.Length; i++)
        {
            var d = definitions[page + i]; int y = 175 + i * 63;
            Text(d.Name + " — " + d.Slot + "\n" + d.Create().Description, 30, y, 910, 58);
            Button("Buy " + d.Cost, 1010, y, 180, () => Change(() => CompanyEquipment.Buy(State, d.Id), () => Shop(page)), State.Gold >= d.Cost && !State.Busy);
        }
        Button("Previous", 30, 520, 160, () => Shop(page - 5), page > 0);
        Button("Next", 210, 520, 160, () => Shop(page + 5), page + 5 < definitions.Length);
        Button("Sell spare gear", 390, 520, 220, () => Sell());
    }
    void Sell(int page = 0)
    {
        Town(); Clear("Sell unequipped gear", true);
        var bag = State.Inventory.Where(item => !State.Characters.Any(c => c.Unit.Items.Contains(item))).ToArray();
        for (int i = 0; i < 5 && page + i < bag.Length; i++)
        {
            var item = bag[page + i]; int y = 175 + i * 63; Text(item.Name, 30, y, 850, 45);
            int price = CompanyEquipment.Find(item.CompanyDefinitionId).Cost / 2;
            Button("Sell " + price, 1010, y, 180, () => Change(() => CompanyEquipment.Sell(State, item), () => Sell(page)), !State.Busy);
        }
        Button("Previous", 30, 520, 160, () => Sell(page - 5), page > 0);
        Button("Next", 210, 520, 160, () => Sell(page + 5), page + 5 < bag.Length);
    }
    internal void BrowseCheckpoints(string directory = null, int page = 0)
    {
        Clear(directory == null ? "Saved companies" : "Company checkpoints", false, false);
        string root = CompanyCampaignSave.DefaultDirectory;
        string[] entries = directory == null ?
            (Directory.Exists(root) ? Directory.GetDirectories(root).OrderByDescending(d => Path.GetFileName(CompanyCampaignSave.Generations(d).FirstOrDefault() ?? "")).ToArray() : new string[0]) :
            CompanyCampaignSave.Generations(directory);
        if (entries.Length == 0) Text("No saved companies yet.", 30, 170, 1100, 60);
        for (int i = 0; i < 5 && page + i < entries.Length; i++)
        {
            string entry = entries[page + i]; int y = 175 + i * 65;
            CompanyCampaign saved; string path; int skipped;
            try
            {
                if (directory == null) { saved = CompanyCampaignSave.LoadLatest(entry, out skipped, out path); }
                else { path = entry; saved = CompanyCampaignSave.Load(path); }
            }
            catch { Text("Unreadable checkpoint — file preserved", 30, y, 1100, 50); continue; }
            string label = saved.Preview ? "Map preview" : saved.Characters.First(c => c.Id == saved.LeaderId).Unit.Name;
            Text(label + " — day " + (saved.ElapsedDays + 1) + " — " + (saved.Ironman ? "Ironman" : "Normal"), 30, y, 710, 50);
            string chosen = path;
            if (saved.Ironman && directory != null) continue;
            Button("Load", 760, y, 170, () => view.LoadCheckpoint(chosen));
            if (!saved.Ironman && directory == null) Button("History", 950, y, 170, () => BrowseCheckpoints(entry));
        }
        Button("Previous", 30, 535, 160, () => BrowseCheckpoints(directory, page - 5), page > 0);
        Button("Next", 210, 535, 160, () => BrowseCheckpoints(directory, page + 5), page + 5 < entries.Length);
        Button("Campaign setup", 410, 535, 200, view.ShowSetup);
    }
    public void Decision()
    {
        Clear(State.PendingDecisionKind == "Departures" ? "Mercenaries left the company" : State.PendingDecisionKind == "Contracts" ? "Contracts expired" : "Unpaid wages");
        if (State.PendingDecisionKind == "Departures")
        {
            Text(CompanyWages.DepartureMessage(State), 30, 165, 1170, 270);
            Button("OK", 30, 470, 220, Acknowledge);
            return;
        }
        if (State.PendingDecisionKind == "Contracts")
        {
            Text(State.PendingDecision, 30, 165, 1170, 270);
            Button("OK", 30, 470, 220, Acknowledge);
            return;
        }
        int total = State.Characters.Where(c => c.Employment == CompanyEmployment.Employed).Sum(c => c.Debt);
        Text(State.PendingDecision + "\n\nGold: " + State.Gold + "   Total debt: " + total + "   Shortfall: " + Math.Max(0, total - State.Gold), 30, 165, 1170, 270);
        int group = CompanyEconomy.Threatened(State).Sum(c => c.Debt);
        Button("Pay threatened members: " + group, 30, 470, 350, () =>
        { view.Mutate(() => CompanyEconomy.PayThreatened(State)); Acknowledge(); }, group > 0 && State.Gold >= group);
        Button("View company", 410, 470, 220, () => Company());
        Button("OK", 660, 470, 220, Acknowledge);
    }
    void Acknowledge()
    { view.ResolveDecision(); }
}
