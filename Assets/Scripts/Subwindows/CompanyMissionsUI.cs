// Added by the VoreWar mercenary-company conversion fork.
// 2026-11-01: Contracts, final deployment, saved results/revival and active-campaign record views (specs 09–10).
// 2026-11-01: Show individual species labels in enemy and deployment rosters (09–10 feedback).
// 2026-11-01: Show recovering and old injuries during every final deployment review (spec 11).
// 2026-11-02: Add paged campaign help that returns to setup, pending decisions or results without advancing time (spec 12).
// 2026-11-02: Present approved duel choices/results and forced slug deployment without contract rewards or cancel bypass (spec 13).
// 2026-11-02: Use persistent navigation for campaign/contract pages and retain the town column in offer details (spec 14).
// 2026-11-02: Retire the separate campaign settings page in favor of normal Options (spec 14 feedback).
// 2026-11-03: Use the shared Help overlay without replacing the originating company or creator page (spec 20).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using System;
using System.Linq;

internal sealed class CompanyMissionsUI
{
    readonly CompanyCampaignView view;
    readonly CompanyManagementUI ui;
    CompanyCampaign State { get { return view.Campaign; } }
    internal CompanyMissionsUI(CompanyCampaignView view, CompanyManagementUI ui) { this.view = view; this.ui = ui; }
    void Change(Action operation, Action redraw)
    {
        try { view.Mutate(operation); }
        catch { redraw(); throw; }
        redraw();
    }
    internal void Help(int page = 0, bool fromSetup = false, bool fromCreator = false)
    {
        global::State.GameManager.Menu.HelpUI.Open(page);
    }
    internal void Contracts(bool offers = false, int page = 0)
    {
        var town = State.Towns.FirstOrDefault(t => t.Tile == State.Position);
        ui.Clear(offers ? "Contracts — " + (town == null ? "visit a town" : town.Name) : "Active contracts", offers && town != null);
        ui.Text("Reputation " + State.Reputation + " | Gold " + State.Gold + " | Ratings represent fixed enemy power. Rewards are automatic, gold only.", 30, 140, 1190, 50);
        var list = State.Contracts.Where(c => offers ? town != null && c.TownId == town.Id && c.Status == CompanyContractStatus.Offered : c.Status == CompanyContractStatus.Active).ToArray();
        if (list.Length == 0) ui.Text(offers ? "No offers are currently available. Town pools replenish over time; species availability can limit generation." : "No active contracts. Visit a town for local offers.", 30, 220, 1150, 120);
        for (int i = 0; i < 4 && page + i < list.Length; i++)
        {
            var c = list[page + i]; int y = 210 + i * 75;
            ui.Text("Rout enemy force — rating " + c.Rating + " | " + c.Reward + " gold\n" +
                (offers ? "30 days after acceptance" : "Complete before day " + (c.Deadline + 1)) + " | Target (" + c.Tile % State.Width + ", " + c.Tile / State.Width + ")", 30, y, 750, 68);
            ui.Button("Enemy roster", 790, y, 190, () => Enemies(c.Id, false, 0, offers));
            if (offers) ui.Button("Accept", 1000, y, 180, () => Change(() => CompanyContracts.Accept(State, c), () => Contracts(true, page)), !State.Busy);
            else ui.Button("Abandon", 1000, y, 180, () => Abandon(c.Id), !State.Busy);
        }
        ui.Button("Previous", 30, 550, 150, () => Contracts(offers, page - 4), page > 0);
        ui.Button("Next", 195, 550, 150, () => Contracts(offers, page + 4), page + 4 < list.Length);
        ui.Button(offers ? "Active contracts" : "Town offers", 370, 550, 210, () => Contracts(!offers), offers || town != null);
    }
    void Abandon(string id)
    {
        var c = State.Contracts.Single(x => x.Id == id); ui.Clear("Abandon this contract?");
        ui.Text("Its target will disappear and reputation will decrease by " + c.Rating + ". This has the same penalty as missing the deadline.", 30, 180, 1150, 100);
        ui.Button("Abandon", 30, 350, 230, () => Change(() => CompanyContracts.Fail(State, c, true), () => Contracts()));
        ui.Button("Keep contract", 280, 350, 230, () => Contracts());
    }
    internal void Enemies(string id, bool deployment, int page = 0, bool offers = false)
    {
        var c = State.Contracts.Single(x => x.Id == id); ui.Clear("Target roster — rating " + c.Rating, offers && !deployment);
        var enemies = CompanyContracts.Survivors(State, c).ToArray();
        ui.Text("Quoted power " + c.Power.ToString("0.0") + " | Fixed budget " + CompanyContracts.Budget(c.Rating).ToString("0.0") + " ±8% | Remaining power " + enemies.Sum(e => CompanyContracts.Power(e.Unit)).ToString("0.0"), 30, 140, 1200, 55);
        for (int i = 0; i < 5 && page + i < enemies.Length; i++)
        {
            var enemy = enemies[page + i]; var u = enemy.Unit; int y = 205 + i * 63;
            string gear = string.Join(", ", u.Items.Where(item => item != null).Select(item => item.Name).ToArray());
            ui.Text(u.Name + " — " + SpeciesPolicy.DisplayName(u.Race) + " Lv" + u.Level + " | HP " + u.Health + "/" + u.MaxHealth + " | Power " + CompanyContracts.Power(u).ToString("0.0") + "\nGear: " + gear, 30, y, 1180, 60, 17);
        }
        ui.Button("Previous", 30, 550, 150, () => Enemies(id, deployment, page - 5, offers), page > 0);
        ui.Button("Next", 195, 550, 150, () => Enemies(id, deployment, page + 5, offers), page + 5 < enemies.Length);
        if (deployment) ui.Button("Deployment", 380, 550, 180, () => Deployment());
    }
    internal void RoadEvent()
    {
        var occurrence = State.RoadEvent;
        if (occurrence == null || !CompanyRoadEvents.AwaitingChoice(State)) { view.ShowMap(); return; }
        ui.Clear("Practice duel");
        var participants = occurrence.ParticipantIds.Select(id => State.Characters.Single(c => c.Id == id).Unit.Name).ToArray();
        if (occurrence.Phase == CompanyRoadEventPhase.Result)
        {
            ui.Text(occurrence.Result, 30, 180, 1160, 200);
            ui.Button("OK", 30, 470, 220, () => Change(() => CompanyRoadEvents.Acknowledge(State), view.ShowMap));
            return;
        }
        ui.Text(participants[0] + " and " + participants[1] + " ask to duel with blunted practice weapons.\n\n" +
            "Allow it? A successful duel grants each half their next-level XP requirement.\n" +
            "There is a 50% chance of an accident instead: each loses half their current HP, without injuries.", 30, 180, 1160, 230);
        ui.Button("Allow the duel", 30, 470, 240, () => Change(() => CompanyRoadEvents.ResolveDuel(State, true), view.ShowMap));
        ui.Button("Decline", 295, 470, 220, () => Change(() => CompanyRoadEvents.ResolveDuel(State, false), view.ShowMap));
    }
    internal void Deployment(int page = 0)
    {
        var battle = State.Battle; var contract = battle.RoadEventId == null ? State.Contracts.Single(c => c.Id == battle.ContractId) : null;
        ui.Clear(contract == null ? "Wild Spitter Slugs attack — final deployment" : "Final deployment — contract rating " + contract.Rating);
        ui.Text("Choose active/reserve status now. All active members deploy; the leader always participates.\n" +
            CompanyBattleSession.Survivors(State, battle).Count() + " enemies | " + (contract == null ? "No contract payment" : contract.Reward + " gold") + " | Leader revival " + (State.LeaderRevival ? "enabled" : "disabled"), 30, 140, 1180, 68);
        var members = State.Members.ToArray();
        for (int i = 0; i < 5 && page + i < members.Length; i++)
        {
            var member = members[page + i]; var c = State.Characters.Single(x => x.Id == member.CharacterId); bool leader = c.Id == State.LeaderId; int y = 230 + i * 55;
            ui.Text(c.Unit.Name + " — " + SpeciesPolicy.DisplayName(c.Unit.Race) + " Lv" + c.Unit.Level + " | HP " + c.Unit.Health + "/" + c.Unit.MaxHealth + "\n" + CompanyInjuries.Summary(c.Unit), 30, y, 850, 48);
            ui.Button(leader ? "Leader — active" : member.Reserve ? "Reserve" : "Active", 940, y, 240,
                () => Change(() => member.Reserve = !member.Reserve, () => Deployment(page)), !leader);
        }
        ui.Button("Previous", 30, 535, 135, () => Deployment(page - 5), page > 0);
        ui.Button("Next", 180, 535, 135, () => Deployment(page + 5), page + 5 < members.Length);
        ui.Button("Enemy roster", 335, 535, 190, () => { if (contract == null) RoadEnemies(); else Enemies(contract.Id, true); });
        ui.Button("Begin battle", 545, 535, 200, view.LaunchBattle);
        if (contract != null) ui.Button("Cancel", 765, 535, 170, () => Change(() =>
        {
            if (battle.PositionCommitted) State.Position = battle.ReturnTile;
            battle.Phase = CompanyBattlePhase.Returned;
        }, view.ShowMap));
    }
    void RoadEnemies(int page = 0)
    {
        ui.Clear("Wild Spitter Slugs");
        var enemies = CompanyBattleSession.Survivors(State, State.Battle).ToArray();
        for (int i = 0; i < 5 && page + i < enemies.Length; i++)
        {
            var unit = enemies[page + i].Unit;
            string gear = string.Join(", ", unit.Items.Where(item => item != null).Select(item => item.Name).ToArray());
            ui.Text(unit.Name + " | HP " + unit.Health + "/" + unit.MaxHealth + "\nGear: " + gear, 30, 180 + i * 65, 1180, 60);
        }
        ui.Button("Previous", 30, 550, 150, () => RoadEnemies(page - 5), page > 0);
        ui.Button("Next", 195, 550, 150, () => RoadEnemies(page + 5), page + 5 < enemies.Length);
        ui.Button("Deployment", 380, 550, 180, () => Deployment());
    }
    internal void Resume()
    {
        if (CompanyRoadEvents.AwaitingChoice(State)) { RoadEvent(); return; }
        if (State.Battle != null && State.Battle.Phase == CompanyBattlePhase.Deployment) { Deployment(); return; }
        if (State.Battle != null && State.Battle.Phase == CompanyBattlePhase.Summary)
        {
            ui.Clear("Battle results"); ui.Text(State.Battle.Summary, 30, 150, 1170, 350);
            ui.Button("Continue", 30, 540, 230, () => Change(() => CompanyBattleSession.Acknowledge(State), Resume)); return;
        }
        if (State.Battle != null && State.Battle.Phase == CompanyBattlePhase.Revival)
        {
            ui.Clear("Company leader lost"); var town = CompanyBattleSession.RevivalTown(State); int cost = CompanyBattleSession.RevivalCost(State);
            ui.Text("In the leader's absence, the remaining company headed straight to " + town.Name + " as quickly as possible and arranged revival services.\n\nThe fee is " + cost + " gold (20 × leader level). Treasury: " + State.Gold + ".\n" +
                (State.Gold < cost ? "All available gold will be spent; revival still succeeds, with no revival debt." : "The fee will be paid from the treasury.") +
                "\n\nThe leader returns at full health. No campaign days pass.", 30, 150, 1170, 350);
            ui.Button("Revive and continue", 30, 540, 300, () => Change(() => CompanyBattleSession.Revive(State), Resume)); return;
        }
        if (State.Ended) { Record(); return; }
        view.ShowMap();
    }
    internal void Record(int page = 0)
    {
        ui.Clear(State.Ended ? "Company ended" : "Company record and memorial");
        ui.Text((State.EndReason == null ? "" : State.EndReason + "\n") + "Days survived " + State.ElapsedDays + " | Reputation " + State.Reputation + " | Gold earned " + State.TotalGoldEarned +
            "\nMercenaries hired " + State.MercenariesHired + " | Mercenaries lost " + State.Memorial.Count + " | Contracts completed " + State.ContractsCompleted + " | Enemies defeated " + State.EnemiesDefeated, 30, 140, 1180, 110);
        if (State.Memorial.Count == 0) ui.Text("No fallen mercenaries. Dismissals and unpaid departures are not deaths.", 30, 270, 1180, 65);
        for (int i = 0; i < 4 && page + i < State.Memorial.Count; i++)
        {
            var m = State.Memorial[page + i]; ui.Text(m.Name + " — " + m.Species + " — day " + m.Day + "\n" + m.Fate, 30, 265 + i * 65, 1180, 62);
        }
        ui.Button("Previous", 30, 550, 150, () => Record(page - 4), page > 0);
        ui.Button("Next", 195, 550, 150, () => Record(page + 4), page + 4 < State.Memorial.Count);
        if (State.Ended) ui.Button("Campaign setup", 380, 550, 240, view.ShowSetup);
    }
}
