// Added by the VoreWar mercenary-company conversion fork.
// 2026-11-03: Authorized synthetic save/restore checks in the real Unity battle runtime (spec 19).
// See CHANGES.md and License.txt for GPL-3.0 terms; inherited notices remain applicable.
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

// Run in a separate batch editor. All saves remain under Logs; deletion is intercepted.
[InitializeOnLoad]
public static class CompanySavePersistenceAudit
{
    const string Key = "CompanySavePersistenceAudit";
    const BindingFlags Flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;
    static IEnumerator steps;
    static double started;
    static readonly List<string> checks = new List<string>();
    [Serializable] public class Report { public bool passed; public string error, directory; public List<string> checks; }
    static string directory;
    static CompanySavePersistenceAudit() { EditorApplication.update += Update; }
    public static void Run()
    {
        EditorSceneManager.OpenScene("Assets/base.unity");
        SessionState.SetBool(Key, true);
        EditorApplication.isPlaying = true;
    }
    static void Update()
    {
        int exit = SessionState.GetInt(Key + ".exit", 0);
        if (exit != 0 && !EditorApplication.isPlayingOrWillChangePlaymode)
        { SessionState.EraseInt(Key + ".exit"); EditorApplication.Exit(exit - 1); return; }
        if (!SessionState.GetBool(Key, false) || !EditorApplication.isPlaying) return;
        try
        {
            if (steps == null) { started = EditorApplication.timeSinceStartup; steps = Check(); }
            if (EditorApplication.timeSinceStartup - started > 180) throw new TimeoutException("Save runtime checks timed out.");
            if (!steps.MoveNext()) Finish(null);
        }
        catch (Exception ex) { Finish(ex); }
    }
    static void Finish(Exception ex)
    {
        SessionState.SetBool(Key, false);
        var report = new Report { passed = ex == null, error = ex == null ? null : ex.ToString(), directory = directory, checks = checks };
        File.WriteAllText("Logs/save19-runtime-result.json", JsonUtility.ToJson(report, true));
        Debug.Log("Company save audit: " + (ex == null ? "PASS" : ex.ToString()));
        SessionState.SetInt(Key + ".exit", ex == null ? 1 : 2);
        EditorApplication.isPlaying = false;
    }
    static Type Type(string name) { return typeof(GameManager).Assembly.GetType(name, true); }
    static object Get(object obj, string name)
    {
        var t = obj as Type ?? obj.GetType(); var f = t.GetField(name, Flags);
        return f != null ? f.GetValue(obj is Type ? null : obj) : t.GetProperty(name, Flags).GetValue(obj is Type ? null : obj, null);
    }
    static void Set(object obj, string name, object value)
    {
        var t = obj as Type ?? obj.GetType(); var f = t.GetField(name, Flags);
        if (f != null) f.SetValue(obj is Type ? null : obj, value); else t.GetProperty(name, Flags).SetValue(obj is Type ? null : obj, value, null);
    }
    static object Call(object obj, string name, params object[] args)
    {
        var t = obj as Type ?? obj.GetType();
        return t.GetMethods(Flags).First(m => m.Name == name && m.GetParameters().Length == args.Length).Invoke(obj is Type ? null : obj, args);
    }
    static void Expect(bool ok, string text) { if (!ok) throw new Exception(text); checks.Add(text); }
    static bool Rejects(Action action) { try { action(); return false; } catch { return true; } }
    static IList List(object obj, string field) { return (IList)Get(obj, field); }
    static object Character(object state, string id) { return List(state, "Characters").Cast<object>().Single(c => (string)Get(c, "Id") == id); }
    static IEnumerator Check()
    {
        // Let the normal scene/resource startup complete before using its initialized services.
        for (int frame = 0; frame < 12; frame++) yield return null;
        var gm = UnityEngine.Object.FindObjectOfType<GameManager>();
        Expect(gm != null && gm.CurrentScene == gm.Start_Mode, "Real base scene starts at the main menu");
        directory = Path.GetFullPath("Logs/save19-runtime-preserved-" + Guid.NewGuid().ToString("N"));
        Set(Type("CompanyCampaignSave"), "DiagnosticDirectory", directory);
        Directory.CreateDirectory(directory);
        var view = Get(gm.Start_Mode, "CompanySaveView");
        Expect(!((CompanyCampaignView)view).IsOpen, "Opening the load browser does not expose an uninitialized campaign page");
        var state = Call(Type("CompanyCampaign"), "Generate", 173, false);
        var leader = Call(Type("CompanyEconomy"), "Create", state, Race.Lizards, 2, true, 1);
        Call(Type("CompanyEconomy"), "Begin", state, leader);
        var town = List(state, "Towns").Cast<object>().Single(t => (int)Get(t, "Tile") == (int)Get(state, "Position"));
        foreach (var candidate in ((IEnumerable)Call(Type("CompanyEconomy"), "Offers", state, town)).Cast<object>().Take(2).ToArray())
            Call(Type("CompanyEconomy"), "Hire", state, candidate, town);
        Expect(List(state, "Members").Count == 3, "Synthetic company has a leader, an active mercenary and a reserve");
        var reserve = List(state, "Members")[2]; Set(reserve, "Reserve", true);
        var reserveUnit = (Unit)Get(Character(state, (string)Get(reserve, "CharacterId")), "Unit"); reserveUnit.Health = 17;
        var activeUnit = (Unit)Get(Character(state, (string)Get(List(state, "Members")[1], "CharacterId")), "Unit");
        Call(view, "Install", state);
        int mapGold = (int)Get(state, "Gold");
        var mapSave = (string)Call(Type("CompanySaveSlots"), "Manual", state, "Before battle");
        Call(view, "AdvanceDay", false);
        Expect((int)Get(state, "ElapsedDays") == 1, "Ordinary end day advances once");
        var entries = (IList)Call(Type("CompanySaveSlots"), "List");
        Expect(entries.Cast<object>().Any(e => (string)Get(e, "Name") == "Map autosave" && (int)Get(Get(e, "State"), "ElapsedDays") == 1), "Day autosave contains the newly started day");
        Call(view, "LoadCheckpoint", mapSave); state = Get(view, "Campaign");
        Expect((int)Get(state, "ElapsedDays") == 0 && (int)Get(state, "Gold") == mapGold, "Named map restore does not repeat days, healing or wages");
        var contract = List(state, "Contracts")[0];
        Call(Type("CompanyContracts"), "Accept", state, contract);
        Call(Type("CompanyBattleSession"), "Prepare", state, contract);
        Set(Get(state, "Battle"), "PositionCommitted", true);
        Call(view, "LaunchBattle");
        var session = Get(gm, "CompanyBattle");
        Expect(session != null && gm.CurrentScene == gm.TacticalMode, "Actual company battle launches through the native tactical runtime");
        // Freeze ordinary AI/input while checking a precise, synthetic action boundary.
        gm.enabled = false;
        var data = (TacticalData)Call(gm.TacticalMode, "Export");
        var leaderId = (string)Get(state, "LeaderId");
        var leaderUnit = (Unit)Get(Character(state, leaderId), "Unit");
        var actor = data.units.Single(a => a.Unit == leaderUnit);
        var activeId = (string)Get(List(state, "Members")[1], "CharacterId");
        activeUnit = (Unit)Get(Character(state, activeId), "Unit");
        var downed = data.units.Single(a => a.Unit == activeUnit);
        Set(activeUnit, "Downed", true); activeUnit.Health = 0;
        actor.Unit.Health = 23; Set(actor.Unit, "TemporaryHealth", 7); actor.Movement = 2;
        Set(actor.Unit, "AttritionInjuryChecked", true);
        ((IList)Get(actor.Unit, "Tags")).Add(Traits.KeenShot);
        var savedTraits = ((IList)Get(actor.Unit, "Tags")).Cast<Traits>().ToArray();
        Set(Get(actor.Unit, "TempBoosts"), "HealthBoost", 4); Set(Get(actor.Unit, "TempBoosts"), "ExtraAttacks", 2);
        Call(actor.Unit, "SetBarrier", 9); Set(actor.Unit, "Mana", 5);
        ((IList)Get(actor.Unit, "StatusEffects")).Add(new StatusEffect(StatusEffectType.Poisoned, 1.25f, 3, activeUnit));
        ((int[])Get(actor.Unit, "ItemCooldowns"))[7] = 2;
        var tileKey = Activator.CreateInstance(Type("Vec2"), new object[] { 0, 0 });
        ((IDictionary)Get(data, "activeEffects"))[tileKey] = Activator.CreateInstance(Type("TileEffect"), new object[] { 4, 2.5f, TileEffectType.Fire });
        Call(data.log, "RegisterMiscellaneous", "Synthetic save audit marker");
        var preyActor = data.units.First(a => a.Unit.Side == 1);
        var preyListType = typeof(List<>).MakeGenericType(Type("Prey"));
        var prey = Activator.CreateInstance(Type("Prey"), Flags, null, new[] { (object)preyActor, actor, Activator.CreateInstance(preyListType) }, null);
        Set(prey, "TurnsDigested", 2); Set(preyActor.Unit, "Downed", true); preyActor.Visible = false; preyActor.Targetable = false;
        ((IList)Get(actor.PredatorComponent, "prey")).Add(prey); ((IList)Get(actor.PredatorComponent, "stomach")).Add(prey);
        // A temporary summoned actor is not part of the permanent company registry.
        var summonUnit = new Unit(0, Race.SpitterSlugs, 0, true, UnitType.Summon);
        var summon = new Actor_Unit(new Vec2i(actor.Position.x, actor.Position.y + 1), summonUnit);
        data.units.Add(summon);
        Call(session, "Capture");
        var savedData = (TacticalData)Get(Get(state, "Battle"), "Tactical");
        int day = (int)Get(state, "ElapsedDays"), gold = (int)Get(state, "Gold");
        var savedPosition = actor.Position;
        string battleSave = (string)Call(Type("CompanySaveSlots"), "Manual", state, "Mid battle");
        var savedRandom = (int[])Get(Get(state, "Battle"), "RandomState");
        string savedUnityRandom = (string)Get(Get(state, "Battle"), "UnityRandomState");
        int savedLogCount = ((IList)Get(savedData.log, "events")).Count;
        actor.Unit.Health = 1; actor.Movement = 0; Set(state, "Gold", 999);
        Call(view, "LoadCheckpoint", battleSave); state = Get(view, "Campaign"); session = Get(gm, "CompanyBattle");
        data = (TacticalData)Call(gm.TacticalMode, "Export");
        leaderUnit = (Unit)Get(Character(state, leaderId), "Unit"); actor = data.units.Single(a => a.Unit == leaderUnit);
        Expect(actor.Unit.Health == 23 && (int)Get(actor.Unit, "TemporaryHealth") == 7 && actor.Movement == 2, "Native battle restore keeps lasting HP, temporary HP and spent AP");
        Expect(((IList)Get(actor.Unit, "Tags")).Cast<Traits>().SequenceEqual(savedTraits), "Battle restoration preserves exact acquired trait state rather than regenerating species traits");
        Expect(actor.Position.x == savedPosition.x && actor.Position.y == savedPosition.y && data.currentTurn == savedData.currentTurn && data.activeSide == savedData.activeSide, "Native restore keeps position, turn and side without starting another turn");
        Expect(data.units.Any(a => a.Unit == (Unit)Get(Character(state, activeId), "Unit") && (bool)Get(a.Unit, "Downed")), "Native restore keeps the downed company member bound to its canonical record");
        reserveUnit = (Unit)Get(Character(state, (string)Get(List(state, "Members")[2], "CharacterId")), "Unit");
        Expect(reserveUnit.Health == 17 && data.units.All(a => a.Unit != reserveUnit), "Reserve HP is preserved and reserve remains outside battle");
        Expect((bool)Get(actor.Unit, "AttritionInjuryChecked") && (bool)Get(actor.Unit, "BattleActive"), "Restoration preserves injury-roll latch and rebinds active combat HP semantics");
        Expect((int)Get(actor.Unit, "Barrier") == 9 && (int)Get(actor.Unit, "Mana") == 5 && ((int[])Get(actor.Unit, "ItemCooldowns"))[7] == 2 &&
            (int)Get(Get(actor.Unit, "TempBoosts"), "HealthBoost") == 4 && (int)Get(Get(actor.Unit, "TempBoosts"), "ExtraAttacks") == 2, "Barrier, mana, spell cooldown and temporary combat boosts survive native restoration");
        var restoredStatus = ((IList)Get(actor.Unit, "StatusEffects")).Cast<object>().Single(s => (StatusEffectType)Get(s, "Type") == StatusEffectType.Poisoned);
        Expect((int)Get(restoredStatus, "Duration") == 3 && (float)Get(restoredStatus, "Strength") == 1.25f && ReferenceEquals(Get(restoredStatus, "Applicator"), Get(Character(state, activeId), "Unit")), "Status duration, strength and applicator retain their canonical identity");
        var restoredTile = ((IDictionary)Get(data, "activeEffects"))[tileKey];
        Expect((int)Get(restoredTile, "RemainingDuration") == 4 && (float)Get(restoredTile, "Strength") == 2.5f && (TileEffectType)Get(restoredTile, "Type") == TileEffectType.Fire, "Terrain effects retain their type, strength and remaining turns");
        Expect(((IList)Get(data.log, "events")).Count == savedLogCount, "Battle log history survives native restoration");
        var restoredPrey = data.units.Select(a => Get(a, "SelfPrey")).First(p => p != null);
        Expect((int)Get(restoredPrey, "TurnsDigested") == 2 && ReferenceEquals(Get(restoredPrey, "Predator"), actor), "Native restore retains containment identity and digestion progress");
        Expect(data.units.Any(a => a.Unit.Type == UnitType.Summon && a.Unit.Race == Race.SpitterSlugs), "Summoned actor survives exact battle restoration without becoming a permanent recruit");
        var restoredRandom = (int[])Call(Get(Type("State"), "Rand"), "Capture");
        Expect(restoredRandom.SequenceEqual(savedRandom), "Rebuilding native sprites and UI does not advance the saved gameplay RNG");
        Expect(JsonUtility.ToJson(UnityEngine.Random.state) == savedUnityRandom, "Unity random state is also restored after rebuilding presentation");
        Expect((int)Get(state, "ElapsedDays") == day && (int)Get(state, "Gold") == gold, "Battle loading does not advance campaign time or grant rewards");
        // Remove only the synthetic containment fixture in memory before exercising a complete turn.
        Set(Get(restoredPrey, "Actor"), "SelfPrey", null);
        ((IList)Get(actor.PredatorComponent, "prey")).Clear(); ((IList)Get(actor.PredatorComponent, "stomach")).Clear();
        Set(gm.TacticalMode, "repeatingTurn", true); Call(gm.TacticalMode, "EndTurn");
        entries = (IList)Call(Type("CompanySaveSlots"), "List");
        var auto = entries.Cast<object>().First(e => (string)Get(e, "Name") == "Battle autosave");
        var autoData = (TacticalData)Get(Get(Get(auto, "State"), "Battle"), "Tactical");
        data = (TacticalData)Call(gm.TacticalMode, "Export");
        Expect(autoData.currentTurn == data.currentTurn && autoData.activeSide == data.activeSide, "Settled tactical turn writes the next side's exact autosave");
        Call(view, "LoadCheckpoint", (string)Get(auto, "Path"));
        Expect(((TacticalData)Call(gm.TacticalMode, "Export")).activeSide == autoData.activeSide, "Enemy-turn autosave restores without replaying turn effects");
        Call(Get(gm, "CompanyBattle"), "AbandonForLoad"); Call(view, "LoadCheckpoint", mapSave);
        state = Get(view, "Campaign");
        // Inspect retention/deletion selections while all actual file removal is disabled.
        for (int i = 0; i < 6; i++) Call(Type("CompanySaveSlots"), "Manual", state, "Repeated slot");
        var removed = (IList)Get(Type("CompanySaveSlots"), "DiagnosticRemovals");
        Expect(removed.Count >= 2 && removed.Cast<string>().All(File.Exists), "Retention selects old backups only after publishing; all probe files are physically preserved");
        entries = (IList)Call(Type("CompanySaveSlots"), "List");
        var repeated = entries.Cast<object>().Single(e => (string)Get(e, "Name") == "Repeated slot");
        removed.Clear(); Call(Type("CompanySaveSlots"), "Delete", repeated);
        Expect(removed.Count == 6 && removed.Cast<string>().All(p => p.Contains("~" + (string)Get(repeated, "Slot") + ".company") && File.Exists(p)), "Delete targets only the selected logical slot and its backups (deletion suppressed)");
        Expect(entries.Cast<object>().Count(e => (string)Get(Get(e, "State"), "Id") == (string)Get(state, "Id")) >= 3, "Several named saves and map/battle autosaves coexist for one campaign");
        File.WriteAllText(Path.Combine((string)Get(repeated, "Directory"), "9999999999999999999-corrupt~" + (string)Get(repeated, "Slot") + ".company"), "Synthetic interrupted manual save");
        entries = (IList)Call(Type("CompanySaveSlots"), "List");
        Expect((bool)Get(entries.Cast<object>().Single(e => (string)Get(e, "Name") == "Repeated slot"), "Recovered") &&
            !(bool)Get(entries.Cast<object>().Single(e => (string)Get(e, "Name") == "Before battle"), "Recovered"), "Normal corruption recovery is confined to the affected named slot");
        var iron = Call(Type("CompanyCampaignSave"), "Decode", (object)(byte[])Call(Type("CompanyCampaignSave"), "Encode", state));
        Set(iron, "Id", Guid.NewGuid().ToString("N")); Set(iron, "Ironman", true);
        string ironDir = (string)Call(Type("CompanySaveSlots"), "DirectoryFor", Get(iron, "Id"));
        string first = (string)Call(Type("CompanySaveSlots"), "Auto", iron, ironDir);
        Set(iron, "Gold", 11); string second = (string)Call(Type("CompanySaveSlots"), "Auto", iron, ironDir);
        Expect(Rejects(() => Call(Type("CompanySaveSlots"), "Manual", iron, "Forbidden")), "Ironman rejects manual save creation");
        Expect(Rejects(() => Call(Type("CompanyCampaignSave"), "LoadForResume", first)), "Ironman rejects resuming an older valid generation");
        File.WriteAllText(Path.Combine(ironDir, "9999999999999999999-corrupt~map.company"), "Synthetic interrupted write");
        entries = (IList)Call(Type("CompanySaveSlots"), "List");
        var recovery = entries.Cast<object>().Single(e => (string)Get(Get(e, "State"), "Id") == (string)Get(iron, "Id"));
        Expect((bool)Get(recovery, "Recovered") && (string)Get(recovery, "Path") == second, "Corrupt newest Ironman generation recovers to the nearest valid backup");
        removed.Clear(); Call(Type("CompanySaveSlots"), "RemoveCompany", Get(iron, "Id"));
        Expect(File.Exists(Path.Combine(ironDir, "fallen")) && Rejects(() => Call(Type("CompanyCampaignSave"), "LoadForResume", second)), "Durable defeat marker prevents Ironman resurrection even when save removal is interrupted");
        Expect(removed.Count == 3 && removed.Cast<string>().All(File.Exists), "Ironman loss targets every company generation, including corruption, with no actual deletion during checks");
        // Check the normal main-menu browser route, then leave the editor without saving assets.
        Call(view, "ReturnToMainMenu"); Call(Type("CompanySaveMenu"), "Show", view);
        Expect((bool)Get(Type("CompanySaveMenu"), "IsVisible"), "Unified save browser opens from the main menu");
    }
}
