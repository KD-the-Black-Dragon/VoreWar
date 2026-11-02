// Added by the VoreWar mercenary-company conversion fork.
// 2026-11-01: Isolated tactical adapter, transactional outcomes, revival and memorial records (spec 10).
// 2026-11-01: Guard HP during detached battle settlement and use individual species labels in memorials (09–10 feedback).
// 2026-11-01: Clear leader wounds on special revival and consolidate enemy recovery into the shared daily recovery stage (spec 11).
// 2026-11-01: Award surviving deployed members challenge-scaled XP once on contract completion (spec 11 feedback).
// 2026-11-02: Reuse tactical deployment/fate/return for saved road encounters while keeping contract rewards isolated (spec 13).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using System;
using System.Collections.Generic;
using System.Linq;
using OdinSerializer;

internal enum CompanyBattlePhase { Deployment, Summary, Revival, Returned }
[Serializable]
internal sealed class CompanyBattleRecord
{
    public string Id, ContractId, RoadEventId, Summary;
    public CompanyBattlePhase Phase;
    public int ReturnTile, TargetTile, Seed;
    public bool PositionCommitted;
    public string[] DeployedIds;
}
[Serializable]
internal sealed class CompanyMemorial
{
    public string CharacterId, Name, Species, Fate;
    public int Day;
}

internal sealed class CompanyBattleSession
{
    readonly CompanyCampaignView view;
    readonly CompanyCampaign state;
    readonly World previousWorld;
    readonly Rand previousRandom;
    readonly UnityEngine.Random.State previousUnityRandom;
    bool finished;
    internal CompanyBattleSession(CompanyCampaignView view, CompanyCampaign state)
    {
        this.view = view; this.state = state; previousWorld = State.World; previousRandom = State.Rand;
        previousUnityRandom = UnityEngine.Random.state;
    }
    internal bool LeaderPermanentlyLost
    { get { return !state.LeaderRevival && state.Characters.Single(c => c.Id == state.LeaderId).Unit.Digested; } }
    internal static void Prepare(CompanyCampaign state, CompanyContract contract)
    {
        if (state.Busy || contract.Status != CompanyContractStatus.Active || !CompanyContracts.Survivors(state, contract).Any())
            throw new InvalidOperationException("This target is no longer available.");
        state.Battle = new CompanyBattleRecord { Id = Guid.NewGuid().ToString("N"), ContractId = contract.Id,
            ReturnTile = state.Position, TargetTile = contract.Tile, Seed = state.Next(int.MaxValue), Phase = CompanyBattlePhase.Deployment };
    }
    internal static List<string> EnemyIds(CompanyCampaign state, CompanyBattleRecord battle)
    { return battle.RoadEventId == null ? state.Contracts.Single(c => c.Id == battle.ContractId).EnemyIds : state.RoadEvent.EnemyIds; }
    internal static List<string> DefeatedIds(CompanyCampaign state, CompanyBattleRecord battle)
    { return battle.RoadEventId == null ? state.Contracts.Single(c => c.Id == battle.ContractId).DefeatedIds : state.RoadEvent.DefeatedIds; }
    internal static IEnumerable<CompanyCharacter> Survivors(CompanyCampaign state, CompanyBattleRecord battle)
    { return EnemyIds(state, battle).Except(DefeatedIds(state, battle)).Select(id => state.Characters.Single(c => c.Id == id)); }
    internal static int Rating(CompanyCampaign state, CompanyBattleRecord battle)
    { return battle.RoadEventId == null ? state.Contracts.Single(c => c.Id == battle.ContractId).Rating : state.RoadEvent.Rating; }
    internal void Start()
    {
        var deployed = state.Members.Where(m => !m.Reserve || m.CharacterId == state.LeaderId).Select(m => state.Characters.Single(c => c.Id == m.CharacterId)).ToArray();
        state.Battle.DeployedIds = deployed.Select(c => c.Id).ToArray();
        if (deployed.Length == 0) throw new InvalidOperationException("The company leader must deploy.");
        State.Rand = new Rand(state.Battle.Seed); UnityEngine.Random.InitState(state.Battle.Seed);
        new World(CompanyEquipment.Repository); // Battle services only; no empire simulation/configuration reset.
        var attacker = new Army(new Empire(), new Vec2i(0, 0), 0) { Name = "Your company" };
        var defender = new Army(new Empire(), new Vec2i(0, 0), 1) { Name = "Contract target" };
        attacker.Empire.ReplacedRace = deployed[0].Unit.Race;
        var enemies = Survivors(state, state.Battle).ToArray();
        defender.Empire.ReplacedRace = enemies[0].Unit.Race;
        foreach (var c in deployed) { c.Unit.Side = 0; attacker.Units.Add(c.Unit); }
        foreach (var c in enemies) { c.Unit.Side = 1; defender.Units.Add(c.Unit); }
        state.InBattle = true;
        var gm = State.GameManager; gm.CompanyBattle = this; gm.PureTactical = false;
        var terrain = new[] { StrategicTileType.grass, StrategicTileType.forest, StrategicTileType.hills, StrategicTileType.desert }[state.Terrain[state.Battle.TargetTile]];
        gm.ActivateCompanyBattle(terrain, attacker, defender, Rating(state, state.Battle));
    }
    internal bool RevivalEnabled { get { return state.LeaderRevival; } }
    internal void ToggleRevival()
    {
        if (finished) return;
        int skipped;
        var checkpoint = CompanyCampaignSave.LoadLatest(view.SaveDirectory, out skipped);
        checkpoint.LeaderRevival = !state.LeaderRevival;
        // Persist only the preference to deployment fallback; never pretend this is a live battle save.
        CompanyCampaignSave.Save(checkpoint, view.SaveDirectory);
        state.LeaderRevival = checkpoint.LeaderRevival;
    }
    internal void ExitToCheckpoint()
    {
        if (finished) return;
        int skipped;
        var checkpoint = CompanyCampaignSave.LoadLatest(view.SaveDirectory, out skipped);
        finished = true; view.QueueBattleReturn(checkpoint, this);
    }
    internal void RestoreServices()
    {
        State.World = previousWorld; State.Rand = previousRandom; UnityEngine.Random.state = previousUnityRandom;
        State.GameManager.CompanyBattle = null;
    }
    internal void Complete(IList<Actor_Unit> actors, bool stalemate)
    {
        if (finished) return;
        // Reconcile a detached saved graph, then install it atomically. Actors keep the old graph until cleanup.
        var result = SerializationUtility.DeserializeValue<CompanyCampaign>(CompanyCampaignSave.Encode(state), DataFormat.Binary);
        CompanyEconomy.Bind(result);
        CompanyCampaignSave.RequireHealthPreserved(state, result);
        var battle = result.Battle;
        var contract = battle.RoadEventId == null ? result.Contracts.Single(c => c.Id == battle.ContractId) : null;
        var enemyIds = EnemyIds(result, battle); var defeatedIds = DefeatedIds(result, battle);
        var enemyActors = actors.Where(a => enemyIds.Any(id => state.Characters.Single(c => c.Id == id).Unit == a.Unit)).ToArray();
        int defeated = 0;
        foreach (var a in enemyActors.Where(a => a.Unit.Digested || a.Fled || a.Unit.IsDead))
        {
            string id = state.Characters.Single(c => c.Unit == a.Unit).Id;
            if (!defeatedIds.Contains(id)) { defeatedIds.Add(id); defeated++; }
        }
        int temporaryEnemies = actors.Where(a => a.Unit.Side == 1 && (a.Unit.IsDead || a.Fled) && !enemyActors.Contains(a)).Select(a => a.Unit).Distinct().Count();
        result.EnemiesDefeated += defeated + temporaryEnemies;
        bool leaderLost = result.Characters.Single(c => c.Id == result.LeaderId).Unit.Digested;
        var losses = result.Members.Select(m => result.Characters.Single(c => c.Id == m.CharacterId)).Where(c => c.Unit.Digested && c.Id != result.LeaderId).ToArray();
        foreach (var c in losses)
        {
            if (!result.Memorial.Any(m => m.CharacterId == c.Id)) result.Memorial.Add(new CompanyMemorial
            { CharacterId = c.Id, Name = c.Unit.Name, Species = SpeciesPolicy.DisplayName(c.Unit.Race), Day = result.ElapsedDays + 1,
                Fate = "Digested by " + (c.Unit.KilledBy == null ? "an opponent" : c.Unit.KilledBy.Name + " (" + SpeciesPolicy.DisplayName(c.Unit.KilledBy.Race) + ")") });
            result.Members.RemoveAll(m => m.CharacterId == c.Id); c.Employment = CompanyEmployment.Unavailable;
            foreach (var item in c.Unit.Items.Where(i => i != null)) result.Inventory.Remove(item);
        }
        foreach (var c in result.Characters)
        {
            if (battle.DeployedIds.Contains(c.Id) || enemyIds.Contains(c.Id))
            {
                c.Unit.StatusEffects.Clear(); c.Unit.SetBarrier(0);
                c.Unit.TempBoosts.ResetAll(); c.Unit.CurrentLeader = null; c.Unit.SetSizeToDefault(); c.Unit.FinishCombat();
            }
        }
        bool paid = contract != null && CompanyContracts.Complete(result, contract);
        if (paid) CompanyProgression.AwardContract(result, contract);
        bool companyAlive = result.Members.Any(m => !result.Characters.Single(c => c.Id == m.CharacterId).Unit.Digested);
        result.Ended = !companyAlive || (leaderLost && !result.LeaderRevival);
        result.EndReason = result.Ended ? (!companyAlive ? "The company has been wiped out." : "The company leader was digested with revival disabled.") : null;
        if (contract == null)
        {
            result.RoadEvent.Phase = CompanyRoadEventPhase.Closed;
            // Random combat keeps its ordinary combat XP; it grants no contract payment, reputation or completion bonus.
            if (Survivors(result, battle).Any()) result.Position = battle.ReturnTile;
        }
        else if (!paid) result.Position = battle.ReturnTile;
        battle.Summary = (stalemate ? "Stalemate: everyone remaining fled and survived.\n" : "Battle settled.\n") +
            "Enemies defeated: " + (defeated + temporaryEnemies) + "\nMercenaries lost: " + losses.Length + "\n" +
            (contract == null ? "Road encounter settled. No gold or reputation awarded.\n" : paid ? "Contract completed: " + contract.Reward + " gold paid automatically; reputation +1. Surviving participants gained contract XP.\n" : "The surviving target remains available before its deadline.\n") +
            (leaderLost && !result.Ended ? "The leader requires revival after these results.\n" : "") + result.EndReason;
        battle.Phase = CompanyBattlePhase.Summary; result.InBattle = false; result.Validate();
        finished = true; view.QueueBattleReturn(result, this);
    }
    internal static void Acknowledge(CompanyCampaign state)
    {
        if (state.Battle.Phase != CompanyBattlePhase.Summary) throw new InvalidOperationException("No battle summary is pending.");
        var leader = state.Characters.Single(c => c.Id == state.LeaderId);
        state.Battle.Phase = !state.Ended && leader.Unit.Digested ? CompanyBattlePhase.Revival : CompanyBattlePhase.Returned;
    }
    internal static int RevivalCost(CompanyCampaign state) { return 20 * state.Characters.Single(c => c.Id == state.LeaderId).Unit.Level; }
    internal static CompanyTown RevivalTown(CompanyCampaign state)
    { return state.Towns.OrderBy(t => CompanyContracts.Distance(state, state.Position, t.Tile)).ThenBy(t => t.Id).First(); }
    internal static void Revive(CompanyCampaign state)
    {
        if (state.Ended || state.Battle.Phase != CompanyBattlePhase.Revival) throw new InvalidOperationException("No revival is pending.");
        var leader = state.Characters.Single(c => c.Id == state.LeaderId).Unit;
        var town = RevivalTown(state); state.Gold = Math.Max(0, state.Gold - RevivalCost(state)); state.Position = town.Tile;
        state.Reveal(); leader.Digested = false; leader.BattleActive = false; leader.Downed = false;
        leader.TemporaryHealth = 0; leader.Health = leader.MaxHealth; leader.StatusEffects.Clear(); leader.Injuries.Clear();
        state.Battle.Phase = CompanyBattlePhase.Returned;
        CompanyEconomy.Visit(state, town); // No elapsed days, wages or deadlines advance.
    }
}
