// Added by the VoreWar mercenary-company conversion fork.
// 2026-11-01: Runtime company-map preview, daily travel and new-checkpoint navigation (spec 06).
// 2026-11-01: Connect curated leader/company economy, wage decisions and retained terrain artwork (specs 07–08).
// 2026-11-01: Connect contract targets, final deployment, saved result/revival routing and campaign records (specs 09–10).
// 2026-11-01: Use individual species labels in campaign setup (09–10 feedback).
// 2026-11-01: Connect explicit chosen-day rest, recovery/sparring stages and town treatment without advancing time on load or menus (spec 11).
// 2026-11-01: Acknowledge departures separately before any simultaneous wage warning, without advancing the grace day (spec 11 feedback).
// 2026-11-02: Make help and failed-save retry reachable on all company pages and share resume/Ironman validation (spec 12).
// 2026-11-02: Show the existing weekly payday countdown on the world map (spec 12 feedback).
// 2026-11-02: Commit travel-triggered road events before presenting saved choices or forced deployment (spec 13).
// 2026-11-02: Add persistent campaign/town navigation, a larger retained-art map and grouped company status with full-action rest controls (spec 14).
// 2026-11-02: Center a bounded interface and virtualized draggable map, move contracts/records into navigation and open normal Options (spec 14 feedback).
// 2026-11-02: Expose the existing company checkpoint browser to the main-menu load button (spec 15C).
// 2026-11-03: Connect unified saves and exact battle resumption to campaign screens (spec 19).
// 2026-11-03: Retire Continue/map preview setup entries and compact the remaining creation/load choices (spec 20).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public sealed class CompanyCampaignView : MonoBehaviour
{
    StartMode owner;
    CompanyManagementUI management;
    CompanyMissionsUI missions;
    internal CompanyRecoveryUI Recovery { get; private set; }
    Text contractList, financesText, conditionText, calendarText, townDetails;
    CompanyCampaign returnedCampaign;
    CompanyBattleSession returningSession;
    string returningError;
    internal static CompanyCampaignView ActiveView;
    internal bool HasActiveCompany => state != null && !state.Preview && IsOpen && !setupPage.activeSelf;
    internal string SaveDirectory { get { return saveDirectory; } }
    internal void PrepareSaveBrowser(StartMode start)
    { owner = start; if (root == null) { Build(); root.SetActive(false); } ActiveView = this; }
    internal void ReturnToMainMenu()
    { Close(); State.GameManager.Start_Mode.ReturnToStart(); }

    Text stageBanner;
    internal CompanyCampaign Campaign { get { return state; } }
    GameObject root, frame, mapPage, setupPage, townPage, companyPage, navigation, townNavigation, statusPanel;
    CompanyCampaign state;
    readonly CompanyClock clock = new CompanyClock();
    Text heading, message, townHeading, companyText;
    Text[] tileLabels;
    Image[] townIcons;
    Outline[] tileOutlines;
    Button[] tiles;
    const int TileSize = 42, MapColumns = 29, MapRows = 12;
    int[] tileIndices;
    CompanyMapScroll mapScroll;
    RectTransform mapContent, mapViewport;
    string mapCompanyId;
    int mapWidth, mapHeight, centeredTile = -1, firstMapX = int.MinValue, firstMapY = int.MinValue;
    bool optionsOpen, optionsTownServices, optionsCampaign;
    internal bool HasCampaignOptions { get { return optionsOpen && optionsCampaign && CanNavigate && !state.Preview; } }
    internal bool LeaderRevivalEnabled { get { return state.LeaderRevival; } }
    internal void SetLeaderRevival(bool enabled)
    {
        if (!HasCampaignOptions) throw new InvalidOperationException("No active company options are available.");
        if (state.LeaderRevival != enabled) Mutate(() => state.LeaderRevival = enabled);
    }
    Toggle ironman;
    Button saveButton, townButton, mapButton, companyButton, contractsButton, recordsButton, dayButton, restOneButton, restChosenButton, resumeRestButton, cancelRestButton, retrySaveButton;
    bool checkpointFailed;
    string saveDirectory;
    public bool IsOpen { get { return root != null && root.activeSelf; } }

    public void Open(StartMode start)
    {
        owner = start; ActiveView = this;
        if (root == null) Build();
        owner.UI.SetActive(false);
        root.SetActive(true);
        ShowSetup();
    }
    internal void OpenSavedGames(StartMode start)
    {
        PrepareSaveBrowser(start);
        CompanySaveMenu.Show(this);
    }
    public void Close()
    {
        if (root != null) root.SetActive(false);
        if (owner != null) owner.UI.SetActive(true);
    }
    internal GameObject Panel(string name, Transform parent)
    {
        var go = new GameObject(name, typeof(RectTransform)); go.transform.SetParent(parent, false);
        var rect = go.GetComponent<RectTransform>(); rect.anchorMin = Vector2.zero; rect.anchorMax = Vector2.one; rect.offsetMin = rect.offsetMax = Vector2.zero;
        return go;
    }
    internal void Place(GameObject go, float x, float y, float w, float h)
    {
        var rect = go.GetComponent<RectTransform>();
        rect.anchorMin = rect.anchorMax = rect.pivot = new Vector2(0, 1);
        rect.anchoredPosition = new Vector2(x, -y); rect.sizeDelta = new Vector2(w, h);
    }
    internal Text Label(Transform parent, string words, float x, float y, float w, float h, int size = 20)
    {
        var go = Panel("Label", parent); Place(go, x, y, w, h);
        var text = go.AddComponent<Text>(); text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.text = words; text.color = Color.white; text.fontSize = size; text.raycastTarget = false;
        return text;
    }
    internal Button Action(Transform parent, string words, float x, float y, float w, Action callback)
    {
        var go = Panel(words, parent); Place(go, x, y, w, 38);
        go.AddComponent<Image>().color = new Color(.18f, .23f, .29f);
        var button = go.AddComponent<Button>(); button.onClick.AddListener(() => Guard(callback));
        var label = Label(go.transform, words, 3, 3, w - 6, 32, 17); label.alignment = TextAnchor.MiddleCenter;
        return button;
    }
    void Box(Transform parent, string name, float x, float y, float w, float h, Color color)
    {
        var go = Panel(name, parent); Place(go, x, y, w, h);
        var image = go.AddComponent<Image>(); image.color = color; image.raycastTarget = false;
    }
    bool CanNavigate
    {
        get { return state != null && !state.Ended && !state.InBattle &&
            (state.Battle == null || state.Battle.Phase == CompanyBattlePhase.Returned) && !CompanyRoadEvents.AwaitingChoice(state); }
    }
    void RefreshNavigation(bool townServices = false, bool campaign = true)
    {
        if (navigation == null) return;
        bool visible = campaign && state != null && !setupPage.activeSelf;
        navigation.SetActive(visible); statusPanel.SetActive(visible);
        bool available = CanNavigate && !checkpointFailed;
        mapButton.interactable = companyButton.interactable = contractsButton.interactable = recordsButton.interactable = available;
        townButton.interactable = available && !state.Busy && state.Towns.Any(t => t.Tile == state.Position) && state.Actions >= state.TownEntryCost;
        townNavigation.SetActive(visible && townServices && !state.Preview);
        if (!visible) return;
        var roster = state.Members.Select(m => state.Characters.Single(c => c.Id == m.CharacterId)).ToArray();
        long hp = roster.Sum(c => (long)Math.Max(0, c.Unit.Health));
        long max = roster.Sum(c => (long)c.Unit.MaxHealth);
        int wounds = roster.Sum(c => c.Unit.Injuries.Count(CompanyInjuries.Temporary));
        int reserves = state.Members.Count(m => m.Reserve);
        financesText.text = "Gold: " + state.Gold + "    Debt: " + roster.Sum(c => (long)c.Debt) +
            "\nNext payday wages: " + roster.Sum(c => (long)CompanyEconomy.Wage(state, c));
        conditionText.text = "Members: " + roster.Length + " (" + (roster.Length - reserves) + " active, " + reserves + " reserve)" +
            "\nHealth: " + hp + "/" + max + " (" + (max == 0 ? 0 : hp * 100 / max) + "%)" + "\nCurrent injuries: " + wounds;
        int days = Math.Max(0, state.NextPayday - state.ElapsedDays);
        calendarText.text = "Day " + (state.ElapsedDays + 1) + "    Actions: " + state.Actions + "/" + state.DailyActions +
            "\n" + (state.Preview ? "Map preview" : days == 0 ? "Payday due" : "Payday in " + days + (days == 1 ? " day" : " days")) + (state.Ironman ? "\nIronman" : "");
    }
    void OpenOptions()
    {
        optionsTownServices = townNavigation.activeSelf; optionsCampaign = navigation.activeSelf; optionsOpen = true;
        root.SetActive(false);
        try { State.GameManager.Menu.OpenOptions(); }
        catch { optionsOpen = false; root.SetActive(true); throw; }
    }
    void BindMap()
    {
        if (mapCompanyId != state.Id || mapWidth != state.Width || mapHeight != state.Height)
        {
            mapCompanyId = state.Id; mapWidth = state.Width; mapHeight = state.Height;
            mapContent.sizeDelta = new Vector2(mapWidth * TileSize, mapHeight * TileSize);
            firstMapX = firstMapY = int.MinValue; centeredTile = -1;
        }
        if (centeredTile != state.Position) CenterMap();
        RefreshMapTiles(true);
    }
    void CenterMap()
    {
        if (state == null) return;
        mapScroll.StopMovement();
        float x = mapContent.rect.width / 2 - (state.Position % state.Width + .5f) * TileSize;
        float y = (state.Position / state.Width + .5f) * TileSize - mapContent.rect.height / 2;
        float limitX = Mathf.Max(0, (mapContent.rect.width - mapViewport.rect.width) / 2);
        float limitY = Mathf.Max(0, (mapContent.rect.height - mapViewport.rect.height) / 2);
        mapContent.anchoredPosition = new Vector2(Mathf.Clamp(x, -limitX, limitX), Mathf.Clamp(y, -limitY, limitY));
        centeredTile = state.Position; RefreshMapTiles(true);
    }
    void RefreshMapTiles(bool force = false)
    {
        if (state == null || !mapPage.activeInHierarchy || mapContent == null) return;
        var corners = new Vector3[4]; mapViewport.GetWorldCorners(corners);
        Vector3 topLeft = mapContent.InverseTransformPoint(corners[1]);
        int startX = Mathf.FloorToInt((topLeft.x + mapContent.rect.width / 2) / TileSize) - 1;
        int startY = Mathf.FloorToInt((mapContent.rect.height / 2 - topLeft.y) / TileSize) - 1;
        if (!force && startX == firstMapX && startY == firstMapY) return;
        firstMapX = startX; firstMapY = startY;
        var sprites = State.GameManager.StrategyMode.TileSprites;
        var towns = State.GameManager.StrategyMode.VillageSprites;
        int[] terrainTypes = { (int)StrategicTileType.grass, (int)StrategicTileType.forest, (int)StrategicTileType.hills, (int)StrategicTileType.desert };
        for (int i = 0; i < tiles.Length; i++)
        {
            int x = startX + i % MapColumns, y = startY + i / MapColumns;
            bool inside = x >= 0 && y >= 0 && x < state.Width && y < state.Height;
            tiles[i].gameObject.SetActive(inside);
            tileIndices[i] = inside ? y * state.Width + x : -1;
            if (!inside) continue;
            int tile = tileIndices[i]; bool seen = state.Discovered[tile];
            Place(tiles[i].gameObject, x * TileSize, y * TileSize, TileSize, TileSize);
            int distance = Math.Abs(x - state.Position % state.Width) + Math.Abs(y - state.Position / state.Width);
            tiles[i].interactable = !checkpointFailed && !state.Busy && distance == 1 && state.Actions >= state.MoveCost(tile);
            int art = StrategicTileInfo.GetTileType((StrategicTileType)terrainTypes[state.Terrain[tile]], x, y);
            var image = tiles[i].GetComponent<Image>();
            image.sprite = seen && sprites != null && art >= 0 && art < sprites.Length ? sprites[art] : null;
            image.color = seen ? Color.white : new Color(.08f, .1f, .12f);
            bool town = seen && state.Towns.Any(t => t.Tile == tile);
            townIcons[i].sprite = towns != null && towns.Length > 6 ? towns[6] : null;
            townIcons[i].gameObject.SetActive(town && townIcons[i].sprite != null);
            bool target = state.Contracts.Any(c => c.Status == CompanyContractStatus.Active && c.Tile == tile);
            tileLabels[i].text = tile == state.Position ? "C" : target ? "!" : town && townIcons[i].sprite == null ? "T" : "";
            tileLabels[i].color = tile == state.Position ? new Color(.55f, .9f, 1) : target ? new Color(1, .55f, .35f) : new Color(1, .87f, .55f);
            tileOutlines[i].enabled = tiles[i].interactable || tile == state.Position;
            tileOutlines[i].effectColor = tile == state.Position ? new Color(1, .8f, .35f) : new Color(.55f, .9f, .65f);
        }
    }
    void Guard(Action action)
    {
        try { action(); }
        catch (Exception ex) { message.text = "Could not complete this action: " + ex.Message + " Existing saves were preserved."; }
    }
    void Build()
    {
        root = Panel("Company campaign", transform);
        var canvas = root.AddComponent<Canvas>(); canvas.renderMode = RenderMode.ScreenSpaceOverlay; canvas.sortingOrder = 100;
        var scaler = root.AddComponent<CanvasScaler>(); scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1280, 800); scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
        root.AddComponent<GraphicRaycaster>();
        var background = Panel("Background", root.transform); var rect = background.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero; rect.anchorMax = Vector2.one; rect.offsetMin = rect.offsetMax = Vector2.zero;
        background.AddComponent<Image>().color = new Color(.055f, .07f, .09f);
        frame = Panel("Centered campaign frame", root.transform);
        var frameRect = frame.GetComponent<RectTransform>(); frameRect.anchorMin = frameRect.anchorMax = frameRect.pivot = new Vector2(.5f, .5f);
        frameRect.sizeDelta = new Vector2(1280, 800); frameRect.anchoredPosition = Vector2.zero;
        Box(frame.transform, "Header", 0, 0, 1280, 72, new Color(.09f, .12f, .16f));
        heading = Label(frame.transform, "Mercenary company", 80, 76, 1120, 28, 21);
        message = Label(frame.transform, "", 480, 741, 545, 55, 15);
        stageBanner = Label(frame.transform, "", 30, 640, 1220, 20, 15);
        Action(frame.transform, "Main menu", 80, 746, 130, Close);
        Action(frame.transform, "Help", 222, 746, 100, () => missions.Help(0, setupPage.activeSelf || state == null, management.IsCreatingLeader));
        Action(frame.transform, "Options", 334, 746, 120, OpenOptions);
        retrySaveButton = Action(frame.transform, "Retry save", 1045, 746, 155, () => { if (checkpointFailed) Save("Game saved; you can continue."); });
        retrySaveButton.gameObject.SetActive(false);
        setupPage = Panel("Setup", frame.transform);
        Label(setupPage.transform, "Create your leader, recruit and equip a small company, then choose contracts and fight.\nNumeric tuning is provisional.", 30, 105, 1150, 90);
        var toggleGo = Panel("Ironman", setupPage.transform); Place(toggleGo, 30, 220, 25, 25);
        var check = toggleGo.AddComponent<Image>(); check.color = new Color(.3f, .65f, .4f);
        ironman = toggleGo.AddComponent<Toggle>(); ironman.targetGraphic = check;
        var mark = Panel("Check", toggleGo.transform); Place(mark, 5, 5, 15, 15);
        ironman.graphic = mark.AddComponent<Image>(); ironman.isOn = false;
        Label(setupPage.transform, "Ironman (locked at creation; autosaves only; recovery backups)", 70, 216, 1100, 40);
        Action(setupPage.transform, "Create company", 30, 285, 250, () => management.StartCreator(ironman.isOn));
        mapPage = Panel("World map", frame.transform);
        var viewport = Panel("Map viewport", mapPage.transform); Place(viewport, 80, 156, 1120, 420);
        viewport.AddComponent<Image>().color = new Color(.08f, .1f, .12f); viewport.AddComponent<RectMask2D>();
        mapViewport = viewport.GetComponent<RectTransform>(); mapScroll = viewport.AddComponent<CompanyMapScroll>();
        mapScroll.viewport = mapViewport; mapScroll.horizontal = mapScroll.vertical = true;
        mapScroll.movementType = ScrollRect.MovementType.Clamped; mapScroll.inertia = false; mapScroll.scrollSensitivity = 42;
        var content = Panel("Map tiles", viewport.transform); mapContent = content.GetComponent<RectTransform>();
        mapContent.anchorMin = mapContent.anchorMax = mapContent.pivot = new Vector2(.5f, .5f); mapContent.sizeDelta = Vector2.zero;
        mapScroll.content = mapContent;
        mapScroll.onValueChanged.AddListener(_ => RefreshMapTiles());
        int tileCount = MapColumns * MapRows;
        tileLabels = new Text[tileCount]; tiles = new Button[tileCount]; townIcons = new Image[tileCount]; tileOutlines = new Outline[tileCount]; tileIndices = new int[tileCount];
        for (int i = 0; i < tiles.Length; i++)
        {
            int index = i; tileIndices[i] = -1;
            tiles[i] = Action(content.transform, "", 0, 0, TileSize, () => { if (!mapScroll.SuppressTileClick && tileIndices[index] >= 0) Move(tileIndices[index]); });
            Place(tiles[i].gameObject, 0, 0, TileSize, TileSize); tiles[i].transition = Selectable.Transition.None;
            tileOutlines[i] = tiles[i].gameObject.AddComponent<Outline>(); tileOutlines[i].effectDistance = new Vector2(1, -1);
            var townIcon = Panel("Town", tiles[i].transform); Place(townIcon, 5, 3, 32, 36);
            townIcons[i] = townIcon.AddComponent<Image>(); townIcons[i].preserveAspect = true; townIcons[i].raycastTarget = false;
            tileLabels[i] = tiles[i].GetComponentInChildren<Text>(); tileLabels[i].transform.SetAsLastSibling();
            Place(tileLabels[i].gameObject, 0, 0, TileSize, TileSize); tileLabels[i].fontSize = 24;
            tileLabels[i].gameObject.AddComponent<Outline>().effectColor = Color.black;
        }
        dayButton = Action(mapPage.transform, "End turn", 80, 108, 130, EndDay);
        restOneButton = Action(mapPage.transform, "Rest 1 day", 222, 108, 130, () => Rest(1));
        var restInput = Panel("Rest days", mapPage.transform); Place(restInput, 364, 108, 60, 38);
        restInput.AddComponent<Image>().color = new Color(.2f, .25f, .3f);
        var restDays = restInput.AddComponent<InputField>(); restDays.contentType = InputField.ContentType.IntegerNumber;
        restDays.characterLimit = 3; restDays.textComponent = Label(restInput.transform, "1", 8, 2, 44, 34, 18); restDays.text = "1";
        restChosenButton = Action(mapPage.transform, "Rest chosen days", 436, 108, 150, () =>
        {
            int days;
            if (!int.TryParse(restDays.text, out days) || days < 1 || days > 365) throw new InvalidOperationException("Choose 1–365 rest days.");
            Rest(days);
        });
        resumeRestButton = Action(mapPage.transform, "Resume rest", 598, 108, 170, ResumeRest);
        cancelRestButton = Action(mapPage.transform, "Cancel rest", 780, 108, 120, () => { state.RestDaysRemaining = 0; Save("Remaining rest cancelled."); ShowMap(); });
        Box(mapPage.transform, "Active contracts", 80, 582, 1120, 75, new Color(.09f, .12f, .16f));
        contractList = Label(mapPage.transform, "", 96, 588, 1088, 64, 16);
        saveButton = Action(mapPage.transform, "Save / Load", 912, 108, 140, () => CompanySaveMenu.Show(this));
        Action(mapPage.transform, "Center company", 1064, 108, 136, CenterMap);
        townPage = Panel("Town", frame.transform);
        townHeading = Label(townPage.transform, "", 250, 110, 950, 65, 32);
        townDetails = Label(townPage.transform, "", 250, 210, 900, 250, 22);
        companyPage = Panel("Company", frame.transform);
        companyText = Label(companyPage.transform, "Map-only preview: no company members.", 30, 120, 1150, 320);
        management = new CompanyManagementUI(this, frame.transform);
        missions = new CompanyMissionsUI(this, management);
        Recovery = new CompanyRecoveryUI(this, management);
        Action(setupPage.transform, "Load Game", 310, 285, 250, () => CompanySaveMenu.Show(this));
        clock.Register(new CompanyWages()); clock.Register(new CompanyRecruitRefresh());
        clock.Register(new CompanyContractDeadlines()); clock.Register(new CompanyDailyRecovery()); clock.Register(new CompanySparring());
        navigation = Panel("Campaign navigation", frame.transform);
        contractsButton = Action(navigation.transform, "Contracts", 80, 20, 145, () => missions.Contracts());
        companyButton = Action(navigation.transform, "Company", 390, 20, 155, ShowCompany);
        mapButton = Action(navigation.transform, "Map", 562.5f, 20, 155, ShowMap);
        townButton = Action(navigation.transform, "Town", 735, 20, 155, EnterTown);
        recordsButton = Action(navigation.transform, "Records", 1055, 20, 145, () => missions.Record());
        townNavigation = Panel("Town services", frame.transform);
        Box(townNavigation.transform, "Service column", 20, 140, 205, 485, new Color(.09f, .12f, .16f));
        Label(townNavigation.transform, "TOWN SERVICES", 30, 153, 185, 30, 17);
        Action(townNavigation.transform, "Recruitment", 30, 200, 185, () => management.Recruit());
        Action(townNavigation.transform, "Merchant", 30, 258, 185, () => management.Shop());
        Action(townNavigation.transform, "Contracts", 30, 316, 185, () => missions.Contracts(true));
        Action(townNavigation.transform, "Treatment", 30, 374, 185, () => Recovery.Treatment());
        statusPanel = Panel("Company status", frame.transform);
        Box(statusPanel.transform, "Status background", 80, 0, 1120, 72, new Color(.09f, .12f, .16f));
        financesText = Label(statusPanel.transform, "", 96, 9, 330, 60, 17);
        conditionText = Label(statusPanel.transform, "", 454, 5, 420, 66, 17);
        calendarText = Label(statusPanel.transform, "", 900, 9, 285, 60, 17);
        var footerRect = statusPanel.GetComponent<RectTransform>(); footerRect.anchorMin = Vector2.zero; footerRect.anchorMax = new Vector2(1, 0);
        footerRect.pivot = new Vector2(0, 1); footerRect.sizeDelta = new Vector2(0, 72); footerRect.anchoredPosition = new Vector2(0, 138);
        navigation.SetActive(false); townNavigation.SetActive(false); statusPanel.SetActive(false);
    }
    void Pages(GameObject page)
    {
        retrySaveButton.gameObject.SetActive(checkpointFailed && state != null);
        if (management != null) management.Page.SetActive(page == management.Page);
        setupPage.SetActive(page == setupPage); mapPage.SetActive(page == mapPage);
        townPage.SetActive(page == townPage); companyPage.SetActive(page == companyPage);
        heading.gameObject.SetActive(page == setupPage || page == mapPage);
        RefreshNavigation(page == townPage);
    }
    internal void ShowSetup() { Pages(setupPage); heading.text = "Mercenary campaign"; message.text = ""; stageBanner.text = ""; }

    void Move(int tile)
    {
        if (checkpointFailed || state.Busy) return;
        var target = state.Contracts.FirstOrDefault(c => c.Status == CompanyContractStatus.Active && c.Tile == tile);
        if (target != null && CompanyContracts.Distance(state, tile, state.Position) == 1 && state.Actions >= state.MoveCost(tile))
        { Mutate(() => CompanyBattleSession.Prepare(state, target)); missions.Deployment(); return; }
        int previousTile = state.Position;
        byte[] before = CompanyCampaignSave.Encode(state);
        string previousEvent = state.RoadEvent == null ? null : state.RoadEvent.Id;
        try
        {
            if (!state.Move(tile)) return;
            CompanyRoadEvents.AfterTravel(state, previousTile);
            state.Validate();
        }
        catch { CompanyCampaignSave.Restore(state, before); throw; }
        if (state.RoadEvent != null && state.RoadEvent.Id != previousEvent) Save("Road event saved.");
        else message.text = "";
        ShowMap();
    }
    internal void ShowMap()
    {
        if (state.Battle != null && state.Battle.Phase != CompanyBattlePhase.Returned || state.Ended) { missions.Resume(); return; }
        if (CompanyRoadEvents.AwaitingChoice(state)) { missions.RoadEvent(); return; }
        Pages(mapPage);
        stageBanner.text = ""; heading.text = "World map";
        BindMap();
        var active = state.Contracts.Where(c => c.Status == CompanyContractStatus.Active).ToArray();
        contractList.text = "Active contracts: " + active.Length + "    Reputation: " + state.Reputation + "\n" +
            string.Join("    |    ", active.Take(3).Select(c => "Rating " + c.Rating + " · " + c.Reward + " gold · day " + (c.Deadline + 1) +
                " · (" + c.Tile % state.Width + ", " + c.Tile / state.Width + ")").ToArray()) + (active.Length > 3 ? "    More in Contracts." : "");
        saveButton.interactable = true;
        dayButton.interactable = !checkpointFailed;
        dayButton.GetComponentInChildren<Text>().text = !string.IsNullOrEmpty(state.PendingDecision) ? "Review notice" : state.UpdatingDay ? "Resume turn" : "End turn";
        restOneButton.interactable = restChosenButton.interactable = !checkpointFailed && state.CanStartRest;
        resumeRestButton.interactable = !checkpointFailed && state.CanStartRest && state.RestDaysRemaining > 0;
        cancelRestButton.interactable = !checkpointFailed && !state.Busy && state.RestDaysRemaining > 0;
        resumeRestButton.gameObject.SetActive(state.RestDaysRemaining > 0);
        cancelRestButton.gameObject.SetActive(state.RestDaysRemaining > 0);
        resumeRestButton.GetComponentInChildren<Text>().text = "Resume rest (" + state.RestDaysRemaining + ")";

    }
    void EnterTown()
    {
        var town = state.Towns.Find(t => t.Tile == state.Position);
        if (town == null || checkpointFailed || !state.Spend(state.TownEntryCost)) return;
        CompanyEconomy.Visit(state, town);
        if (!state.Preview) Save("Town pool saved.");
        Pages(townPage); heading.text = "Town"; stageBanner.text = ""; townHeading.text = town.Name;
        townDetails.text = state.Preview ? "Map preview town" :
            "Recruitment: " + CompanyEconomy.Offers(state, town).Count() + " candidates available\n\n" +
            "Contracts: " + state.Contracts.Count(c => c.TownId == town.Id && c.Status == CompanyContractStatus.Offered) + " offers available\n\n" +
            "Visit the merchant for equipment, or arrange treatment for recovering injuries.";
    }
    void ShowCompany()
    {
        if (!state.Preview) { management.Company(); return; }
        Pages(companyPage);
        companyText.text = "Company preview\n\nNo leader or mercenaries have been created.\nCharacters will have stable identities shared by the roster, recruitment and battle views.\n\nTreasury: " + state.Gold + "\nFirst payday: after seven elapsed days\nElapsed days: " + state.ElapsedDays;
    }
    bool Save(string success)
    {
        try { CompanyCampaignSave.Save(state, saveDirectory); checkpointFailed = false; message.text = success; return true; }
        catch (Exception ex) { checkpointFailed = true; message.text = "Autosave failed: " + ex.Message + " Use Retry save before continuing."; return false; }
        finally
        {
            retrySaveButton.gameObject.SetActive(checkpointFailed && state != null);
            if (state != null && mapPage.activeSelf) ShowMap();
        }
    }
    void EndDay() { if (!string.IsNullOrEmpty(state.PendingDecision)) management.Decision(); else AdvanceDay(false); }
    void AdvanceDay(bool resting)
    {
        if (state == null || checkpointFailed || state.Ended || state.InBattle || (state.Battle != null && state.Battle.Phase != CompanyBattlePhase.Returned) || !string.IsNullOrEmpty(state.PendingDecision)) return;
        clock.EndDay(state, resting);
        Save("Day advanced; game saved."); ShowMap();
        if (!string.IsNullOrEmpty(state.PendingDecision)) management.Decision();
    }
    internal void ShowManagement(bool townServices = false, bool campaign = true)
    {
        Pages(management.Page); RefreshNavigation(townServices, campaign);
        heading.text = townServices ? "Town" : "Mercenary company"; stageBanner.text = "";
    }
    internal void LoadCheckpoint(string path)
    {
        var candidate = CompanyCampaignSave.LoadForResume(path);
        string directory = Path.GetDirectoryName(Path.GetFullPath(path));
        if (State.GameManager.CompanyBattle != null) State.GameManager.CompanyBattle.AbandonForLoad();
        else if (State.GameManager.CurrentScene == State.GameManager.TacticalMode) State.GameManager.ReturnFromCompanyBattle();
        state = candidate; saveDirectory = directory; checkpointFailed = false;
        ActiveView = this; root.SetActive(true); owner.UI.SetActive(false);
        if (state.InBattle)
        {
            root.SetActive(false);
            new CompanyBattleSession(this, state).Resume();
        }
        else missions.Resume();
        message.text = "Company loaded without advancing time.";
        if (!string.IsNullOrEmpty(state.PendingDecision)) management.Decision();
    }
    internal void Install(CompanyCampaign company)
    {
        company.Validate(); state = company; CompanyEconomy.Bind(state);
        saveDirectory = Path.Combine(CompanyCampaignSave.DefaultDirectory, state.Id);
        checkpointFailed = false; Save("Company created. Hire a couple of starters in this town."); ShowMap();
    }
    internal void LaunchBattle()
    {
        if (checkpointFailed || state.Battle == null || state.Battle.Phase != CompanyBattlePhase.Deployment) return;
        Mutate(() =>
        {
            var battle = state.Battle;
            if (!battle.PositionCommitted)
            {
                state.Battle = null;
                if (!state.Move(battle.TargetTile)) throw new InvalidOperationException("Not enough actions to reach the target.");
                state.Battle = battle; battle.PositionCommitted = true;
            }
            battle.DeployedIds = state.Members.Where(m => !m.Reserve || m.CharacterId == state.LeaderId).Select(m => m.CharacterId).ToArray();
        });
        if (checkpointFailed) return;
        byte[] deployment = CompanyCampaignSave.Encode(state);
        var session = new CompanyBattleSession(this, state); root.SetActive(false);
        try { session.Start(); }
        catch (Exception ex)
        {
            var restored = new CompanyCampaign(); CompanyCampaignSave.Restore(restored, deployment);
            QueueBattleReturn(restored, session);
            returningError = "Could not start this battle: " + ex.Message + " The deployment save was preserved.";
        }
    }
    internal void QueueBattleReturn(CompanyCampaign result, CompanyBattleSession session)
    { returnedCampaign = result; returningSession = session; }
    void Update()
    {
        if (CompanySaveMenu.IsVisible) return;
        if (optionsOpen)
        {
            var options = State.GameManager.Menu.Options;
            if (options.gameObject.activeSelf)
            {
                if (Input.GetButtonDown("Menu")) options.CloseAndSave();
                return;
            }
            optionsOpen = false; root.SetActive(true); RefreshNavigation(optionsTownServices, optionsCampaign);
        }
        if (returnedCampaign == null) return;
        State.GameManager.ReturnFromCompanyBattle(); returningSession.RestoreServices();
        state = returnedCampaign; returnedCampaign = null; returningSession = null;
        root.SetActive(true); owner.UI.SetActive(false);
        checkpointFailed = false; Save("Battle results saved."); missions.Resume();
        if (!string.IsNullOrEmpty(returningError)) { message.text = returningError; returningError = null; }
    }
    internal void Mutate(Action operation)
    {
        if (state.InBattle) throw new InvalidOperationException("Company management is unavailable during battle.");
        if (checkpointFailed) throw new InvalidOperationException("Use Retry save before making more changes.");
        byte[] before = CompanyCampaignSave.Encode(state);
        try { operation(); state.Validate(); }
        catch { CompanyCampaignSave.Restore(state, before); throw; }
        Save("Company saved.");
    }
    internal void ResolveDecision()
    {
        if (checkpointFailed) throw new InvalidOperationException("Save the current game first.");
        if (state.PendingDecisionKind == "Departures" && !string.IsNullOrEmpty(state.DeferredWageDecision))
        {
            state.PendingDecision = state.DeferredWageDecision; state.DeferredWageDecision = null;
            state.PendingDecisionKind = "Payday"; state.PendingCharacterIds = new string[0];
            Save("Departure acknowledged; a separate wage warning requires your response."); management.Decision(); return;
        }
        state.PendingDecision = null; state.PendingDecisionKind = null; state.PendingCharacterIds = new string[0];
        clock.Resume(state); Save("Daily update settled. Any remaining rest days wait for your next rest command."); ShowMap();
        if (!string.IsNullOrEmpty(state.PendingDecision)) management.Decision();
    }
    void Rest(int days)
    {
        if (state == null || checkpointFailed || state.Busy) return;
        if (!state.CanStartRest) throw new InvalidOperationException("Rest requires all daily actions. End the turn first.");
        state.RestDaysRemaining = days;
        ResumeRest();
    }
    void ResumeRest()
    {
        if (state == null || checkpointFailed || state.Busy) return;
        if (!state.CanStartRest) throw new InvalidOperationException("Rest requires all daily actions. End the turn first.");
        while (state.RestDaysRemaining > 0 && !checkpointFailed && !state.Busy) AdvanceDay(true);
    }
}
