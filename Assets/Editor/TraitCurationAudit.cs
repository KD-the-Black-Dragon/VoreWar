// Added by the VoreWar mercenary-company conversion fork.
// 2026-11-04: Authorized grouped trait/UI runtime checks using isolated synthetic company data (spec 31).
// 2026-11-04: Extend the grouped review to baseline tiers and approved injuries/recovery (specs 32–33).
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
public static class TraitCurationAudit
{
    const string Key = "TraitCurationAudit";
    const BindingFlags Flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;
    static IEnumerator steps;
    static double started;
    static readonly List<string> checks = new List<string>();
    [Serializable] public class Report { public bool passed; public string error, directory; public List<string> checks; }
    static string directory;
    static TraitCurationAudit() { EditorApplication.update += Update; }
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
        File.WriteAllText("Logs/traits-injuries33-runtime6-result.json", JsonUtility.ToJson(report, true));
        Debug.Log("Trait curation audit: " + (ex == null ? "PASS" : ex.ToString()));
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
        for (int frame = 0; frame < 12; frame++) yield return null;
        var gm = UnityEngine.Object.FindObjectOfType<GameManager>();
        Expect(gm != null, "Base scene initialized");
        directory = Path.GetFullPath("Logs/traits-injuries33-preserved-" + Guid.NewGuid().ToString("N"));
        Set(Type("CompanyCampaignSave"), "DiagnosticDirectory", directory);
        Directory.CreateDirectory(directory);
        var state = Call(Type("CompanyCampaign"), "Generate", 173, false);
        var leader = Call(Type("CompanyEconomy"), "Create", state, Race.Lizards, 2, true, 1);
        Call(Type("CompanyEconomy"), "Begin", state, leader);
        var view = Get(gm.Start_Mode, "CompanySaveView");
        Call(view, "Install", state);
        var contract = List(state, "Contracts")[0];
        Call(Type("CompanyContracts"), "Accept", state, contract);
        Call(Type("CompanyBattleSession"), "Prepare", state, contract);
        Set(Get(state, "Battle"), "PositionCommitted", true);
        Call(view, "LaunchBattle");
        gm.enabled = false;
        // Unity 2018's null graphics device crashes rendering populated tilemaps.
        // This audit checks gameplay/properties, not pixels; prevent headless drawing.
        foreach (var camera in Resources.FindObjectsOfTypeAll<Camera>()) camera.enabled = false;
        foreach (var renderer in Resources.FindObjectsOfTypeAll<UnityEngine.Tilemaps.TilemapRenderer>()) renderer.enabled = false;
        var data = Call(gm.TacticalMode, "Export");
        var actors = List(data, "units").Cast<Actor_Unit>().ToList();
        var a = actors.First(x => x.Unit.Side == (int)Get(data,"attackerSide"));
        var b = actors.First(x => x.Unit.Side != a.Unit.Side);
        a.SetPos(new Vec2i(4,4)); b.SetPos(new Vec2i(5,4));
        var traits = Type("TraitsMethods");
        foreach (Traits t in Enum.GetValues(typeof(Traits)))
            if ((bool)Call(traits,"IsRetired",t))
            {
                ((List<Traits>)Get(a.Unit,"Tags")).Add(t);
                Expect(!a.Unit.HasTrait(t), "Retired trait blocked: " + t);
            }
        Reset(a); Reset(b);
        string help = (string)Call(Type("HelpText"), "Traits");
        foreach (var pair in new[] {new[]{"AdaptiveTactics","QuickLearner"},new[]{"DoubleAttack","DoubleShot"},new[]{"AccuteDodge","Dodger I"},new[]{"Defenseless","EasyPrey I"},new[]{"DyingStrike","Vindication"},new[]{"AdeptLearner","Talented"},new[]{"MadScience","MadMage"},new[]{"PackDexterity","PackAgility"}})
            Expect(help.Contains(pair[1]+" - ") && !help.Contains("\n"+pair[0]+" - "), "Help display alias: "+pair[1]);
        CheckBaselineAndInjuries(a, b);
        Expect(!help.Contains("\nLightFrame - ") && !help.Contains("\nBlessingOfNature - "), "Help omits retired entries");
        foreach (var test in new[] {new[]{Traits.DoubleAttack},new[]{Traits.Maul},new[]{Traits.FastCaster}})
        {
            Reset(a); ((List<Traits>)Get(a.Unit,"Tags")).Add(test[0]); Call(a.Unit,"InitializeTraits");
            bool spell = test[0] == Traits.FastCaster;
            bool ranged = test[0] == Traits.DoubleAttack;
            a.Movement=2;
            if(spell) Call(a,"SpendSpellAP"); else Call(a,"SpendWeaponAP",ranged,1,false);
            Expect(a.Movement==1,"Standing first action leaves one AP: "+test[0]);
            if(spell) Call(a,"SpendSpellAP"); else Call(a,"SpendWeaponAP",ranged,1,false);
            Expect(a.Movement==0,"Second action spends final AP: "+test[0]);
            Set(a,"WeaponAttackThisTurn",false); Set(a,"SpellCastThisTurn",false); Set(a,"MovedThisTurn",true);a.Movement=2;
            if(spell) Call(a,"SpendSpellAP"); else Call(a,"SpendWeaponAP",ranged,1,false);
            Expect(a.Movement==0,"Moved actor gets no extra action: "+test[0]);
        }
        Reset(a); ((List<Traits>)Get(a.Unit,"Tags")).Add(Traits.DoubleAttack);Call(a.Unit,"InitializeTraits");a.Movement=2;Call(a,"SpendWeaponAP",false,1,false);
        Expect(a.Movement==0,"DoubleShot does not enhance melee");
        Reset(a); ((List<Traits>)Get(a.Unit,"Tags")).Add(Traits.Maul);Call(a.Unit,"InitializeTraits");a.Movement=2;Call(a,"SpendWeaponAP",true,1,false);
        Expect(a.Movement==0,"Maul does not enhance ranged");
        Reset(a); Reset(b);
        float baseChance=b.GetAttackChance(a,false);
        ((List<Traits>)Get(b.Unit,"Tags")).Add(Traits.AccuteDodge);Call(b.Unit,"InitializeTraits");
        Expect(Math.Abs(b.GetAttackChance(a,false)-Math.Max(0,baseChance-.10f))<.0001f,"Dodger subtracts ten percentage points");
        Reset(b);float vore=b.GetDevourChance(a);
        ((List<Traits>)Get(b.Unit,"Tags")).Add(Traits.Defenseless);Call(b.Unit,"InitializeTraits");
        Expect(Math.Abs(b.GetDevourChance(a)-Math.Min(1,vore+.10f))<.0001f,"EasyPrey I adds ten percentage points");
        Reset(b); b.Unit.SetStatBase(Stat.Intelligence,50);float unDazzled=b.GetAttackChance(a,false);
        ((List<Traits>)Get(b.Unit,"Tags")).Add(Traits.Dazzle);Call(b.Unit,"InitializeTraits");
        Expect(Math.Abs(b.GetAttackChance(a,false)-Math.Max(0,unDazzled-.10f))<.0001f,"Dazzle uses INT accuracy reduction");
        Reset(a);Reset(b);((List<Traits>)Get(a.Unit,"Tags")).Add(Traits.KeenEye);Call(a.Unit,"InitializeTraits");
        foreach(float wanted in new[]{.50f,.51001f})
        {
            Set(Get(b.Unit,"TraitBoosts"),"FlatHitReduction",1f);
            float raw=b.GetAttackChance(a,false);
            Set(Get(b.Unit,"TraitBoosts"),"FlatHitReduction",wanted/raw);
            float actual=b.GetAttackChance(a,false);
            int misses=0,grazes=0;
            for(int i=0;i<300;i++)
            {
                object[] args={b,false,0f};
                int result=(int)a.GetType().GetMethod("ResolveWeaponHit",Flags).Invoke(a,args);
                if(result==0)misses++; if(result==3)grazes++;
            }
            Expect(wanted>.5f ? misses==0 && grazes>0 : grazes==0 && misses>0,"KeenEye threshold sample at "+actual);
        }
        Reset(a);((List<Traits>)Get(a.Unit,"Tags")).Add(Traits.AdaptiveTactics);Call(a.Unit,"InitializeTraits");
        Expect(Math.Abs((float)Get(Get(a.Unit,"TraitBoosts"),"ExpGain")-1.1f)<.0001f,"QuickLearner grants ten percent XP");
        Reset(a);Set(a,"TurnsSlacking",0);
        ((List<Traits>)Get(a.Unit,"Tags")).Add(Traits.Juggernaut);Call(a.Unit,"InitializeTraits");
        Call(a,"RestoreMP");
        Expect(a.Movement>0 && (bool)Call(Type("OpportunityAttacks"),"CanAct",a),"Juggernaut active turn remains actionable");
        Call(a,"RestoreMP");
        Expect(a.Movement==0 && !(bool)Call(Type("OpportunityAttacks"),"CanAct",a),"Juggernaut skipped turn cannot act or react");
        Call(a,"RestoreMP");
        Expect(a.Movement>0 && (bool)Call(Type("OpportunityAttacks"),"CanAct",a),"Juggernaut acts again after skipped turn");
        Reset(a);Set(a,"MovedThisTurn",true);Set(a,"WeaponAttackThisTurn",true);Set(a,"SpellCastThisTurn",true);Set(a,"SkippedBySlacking",true);
        var bytes=OdinSerializer.SerializationUtility.SerializeValue(a,OdinSerializer.DataFormat.Binary);
        var restored=OdinSerializer.SerializationUtility.DeserializeValue<Actor_Unit>(bytes,OdinSerializer.DataFormat.Binary);
        Expect((bool)Get(restored,"MovedThisTurn") && (bool)Get(restored,"WeaponAttackThisTurn") &&
            (bool)Get(restored,"SpellCastThisTurn") && (bool)Get(restored,"SkippedBySlacking"),"New turn eligibility flags survive actor serialization");
        Reset(a);Reset(b);
        ((List<Traits>)Get(a.Unit,"Tags")).Add(Traits.ArcaneMagistrate);Call(a.Unit,"InitializeTraits");
        b.Unit.Health=1;
        var fireball=Get(Type("SpellList"),"Fireball");
        bool resist=(bool)Get(fireball,"Resistable");Set(fireball,"Resistable",false);
        try { Call(b,"DefendDamageSpell",fireball,a,10000); }
        finally { Set(fireball,"Resistable",resist); }
        var focus=Call(a.Unit,"GetStatusEffect",StatusEffectType.Focus);
        Expect(b.Surrendered && focus!=null && (int)Get(focus,"Duration")==5,"ArcaneMagistrate gets full bonus on newly downing a target");
        Reset(a);Reset(b);
        ((List<Traits>)Get(b.Unit,"Tags")).Add(Traits.DyingStrike);Call(b.Unit,"InitializeTraits");
        Set(Get(a.Unit,"TraitBoosts"),"CapacityMult",10000f);
        Set(Get(Get(a.Unit,"TraitBoosts"),"Outgoing"),"DigestionRate",1000000f);
        b.Unit.Health=1;Set(b.Unit,"Downed",true);a.Movement=2;
        bool swallowed=(bool)Call(a.PredatorComponent,"Devour",b,0f);
        Expect(swallowed,"Synthetic Vindication target swallowed through native vore action");
        Call(a.PredatorComponent,"Digest","");
        string battleLog=(string)Call(Get(gm.TacticalMode,"Log"),"DebugDump");
        Expect(battleLog.Contains("Vindication struck"),"Vindication executes on completed digestion");
        Reset(a);
        ((List<Traits>)Get(a.Unit,"Tags")).Add(Traits.EvasiveBattler);Call(a.Unit,"InitializeTraits");
        Set(gm.TacticalMode,"currentTurn",4);a.Movement=2;
        gm.TacticalMode.SelectedUnit=a;
        Call(gm.TacticalMode,"EvasiveRetreat");
        Expect(a.Fled,"EvasiveBattler can retreat from the interior on turn four");
        gm.StatScreen.VictoryType.text="Defenders victorious";
        gm.StatScreen.Open(false);
        Expect(gm.StatScreen.VictoryType.fontSize==48 && gm.StatScreen.AttackerTitle.fontSize==32 && gm.StatScreen.AttackerText.fontSize==24,"Outcome font hierarchy configured");
        Expect(gm.StatScreen.AttackerText.lineSpacing>1 && gm.StatScreen.transform.Find("Outcome divider")!=null,"Outcome line spacing and divider configured");
    }
    static object E(string type, string value) { return Enum.Parse(Type(type), value); }
    static void Injury(Actor_Unit actor, string part, string grade, double roll = .9, bool treated = false)
    {
        Call(Type("CompanyInjuries"), "Add", actor.Unit, E("CompanyBodyPart", part), E("CompanyInjuryGrade", grade));
        var injury = List(actor.Unit, "Injuries").Cast<object>().Last();
        Set(injury, "RecoveryRoll", roll); Set(injury, "Treated", treated);
    }
    static void Old(Actor_Unit actor, string part, string kind = "BodyPart")
    { Call(Type("CompanyInjuries"), "AddOld", actor.Unit, E("CompanyBodyPart", part), E("CompanyComplication", kind)); }
    static void CheckBaselineAndInjuries(Actor_Unit a, Actor_Unit b)
    {
        var tiers = Type("TieredTraits"); var injuries = Type("CompanyInjuries");
        var families = ((IEnumerable)Get(tiers, "Families")).Cast<object>().ToArray();
        Expect(families.Length == 28, "All 28 baseline families defined");
        foreach (var family in families)
        {
            var variants = (Traits[])Get(family, "Tiers"); var effect = Get(family, "Effect");
            string effectName = effect.ToString(), name = (string)Get(family, "Name");
            int sign = (int)Get(family, "Sign");
            for (int i = 0; i < 3; i++)
            {
                Reset(a); a.Unit.AddTrait(variants[i]);
                float expected = effectName == "CriticalChance" ? new[] { .05f, .10f, .15f }[i] :
                    effectName.Contains("Accuracy") ? new[] { .10f, .20f, .30f }[i] : new[] { 1f, 2f, 4f }[i];
                if (effectName == "MaxHealth" || effectName == "MaxMana") expected *= a.Unit.Level;
                float actual = (float)Call(tiers, "Amount", a.Unit, effect);
                Expect(Math.Abs(actual - expected * sign) < .0001f, name + " tier " + (i + 1) + " modifier");
                string label = (string)Call(Type("TraitsMethods"), "DisplayName", variants[i]);
                var parse = Type("TraitsMethods").GetMethod("TryParse", Flags); var args = new object[] { label, default(Traits) };
                Expect((bool)parse.Invoke(null, args) && (Traits)args[1] == variants[i], name + " tier " + (i + 1) + " hover label resolves");
            }
            Reset(a); a.Unit.AddTrait(variants[0]); a.Unit.AddTrait(variants[1]); a.Unit.AddTrait(variants[2]); a.Unit.AddTrait(variants[0]);
            Expect(!a.Unit.HasTrait(variants[0]) && !a.Unit.HasTrait(variants[1]) && a.Unit.HasTrait(variants[2]), name + " upgrades exclusively without downgrade");
        }
        Reset(a); a.Unit.AddTrait(Traits.PreciseIII); a.Unit.AddTrait(Traits.ClumsyII);
        Expect(!a.Unit.HasTrait(Traits.PreciseIII) && a.Unit.HasTrait(Traits.ClumsyII), "Opposite accuracy family replaces prior trait");
        Reset(a); Call(a.Unit, "AddSharedTrait", Traits.Deadeye); a.Unit.AddTrait(Traits.DeadeyeII);
        Expect(a.Unit.HasTrait(Traits.DeadeyeII) && !a.Unit.HasTrait(Traits.Deadeye), "Shared tier upgrades do not stack");
        Reset(a); ((List<Traits>)Get(a.Unit,"Tags")).Add(Traits.MetalBody); ((List<Traits>)Get(a.Unit,"Tags")).Add(Traits.AwkwardShape); Call(a.Unit,"InitializeTraits");
        Expect(List(a.Unit,"GetTraits").Cast<Traits>().Count(t => t == Traits.AwkwardShape) == 1 &&
            Math.Abs((float)Get(Get(Get(a.Unit,"TraitBoosts"),"Outgoing"),"Nutrition") - 1) < .0001f, "Legacy HardPrey folds into one tier without nutrition drawback");
        Reset(a); Reset(b);
        Set(b.Unit,"Type",UnitType.Soldier); Set(b.Unit,"PermanentInjuryImmune",false);
        int hp=b.Unit.MaxHealth, str=b.Unit.GetStat(Stat.Strength);
        b.Unit.AddTrait(Traits.Fit);
        Expect(b.Unit.MaxHealth==hp && b.Unit.GetStat(Stat.Strength)>str, "Modified STR never changes normal maximum HP");
        Reset(b); hp=b.Unit.MaxHealth;
        foreach(var test in new[]{new[]{.80f,0f},new[]{.70f,1f},new[]{.45f,2f},new[]{.20f,3f}})
        {
            Set(b.Unit,"BattleActive",false);b.Unit.Health=(int)(hp*test[0]);
            Expect((int)Call(injuries,"DamageTier",b.Unit)==(int)test[1], "Accumulated damage tier at "+test[0]);
            Expect(b.Unit.MaxHealth==hp && b.Unit.GetStat(Stat.Strength)==Mathf.RoundToInt(40*(1-.1f*test[1])), "Damage penalty changes combat stats, not HP denominator");
        }
        b.Unit.Health=(int)(hp*.45f); Set(b.Unit,"BattleActive",true); Set(b.Unit,"TemporaryHealth",0);
        b.Unit.Heal(10000);
        Expect((int)Call(injuries,"DamageTier",b.Unit)==0, "Temporary healing immediately removes accumulated damage penalty");
        Call(b.Unit,"FinishCombat");
        Expect((int)Call(injuries,"DamageTier",b.Unit)==2, "Expiring temporary HP restores appropriate damage penalty");
        Reset(b);Set(b.Unit,"BattleActive",false);hp=b.Unit.MaxHealth;b.Unit.Health=hp;
        Injury(b,"Chest","Bruised"); Old(b,"Chest");
        Expect((int)Get(b.Unit,"HealingCeiling")==Mathf.FloorToInt(hp*.85f+.0001f) && b.Unit.Health==hp && b.Unit.MaxHealth==hp, "Chest and old injury add a healing ceiling without losing current HP");
        Call(b.Unit,"HealLasting",10000);
        Expect(b.Unit.Health==hp, "Healing above ceiling never reduces existing HP");
        b.Unit.Health=hp/2;Call(b.Unit,"HealLasting",10000);
        Expect(b.Unit.Health==(int)Get(b.Unit,"HealingCeiling"), "Lasting healing respects combined ceiling");
        foreach(string part in new[]{"Head","Eyes","Arm","Hand","Chest","Abdomen","Leg","Knee"})
        {
            Reset(b);for(int i=0;i<4;i++)Old(b,part);
            var old=List(b.Unit,"Injuries").Cast<object>().Single();
            Expect((int)Get(old,"Tier")==3, "Old injury caps at III: "+part);
        }
        foreach(string kind in new[]{"PoorFocus","DulledInstincts","SluggishRecovery","SensitiveStomach"})
        {
            Reset(b); for(int i=0;i<4;i++)Old(b,"Head",kind);
            Expect(List(b.Unit,"Injuries").Count==1 && (int)Get(List(b.Unit,"Injuries")[0],"Tier")==3,"Systemic complication caps at III: "+kind);
        }
        foreach(var test in new[]{new[]{"Bruised","0","no"},new[]{"Battered","0.25","yes"},new[]{"Battered","0.25","no","treated"},new[]{"Mangled","0.5","yes"},new[]{"Mangled","0.5","no","treated"}})
        {
            Reset(b);Injury(b,"Arm",test[0],double.Parse(test[1],System.Globalization.CultureInfo.InvariantCulture),test.Length>3);
            Call(injuries,"Recover",b.Unit,100);
            Expect((List(b.Unit,"Injuries").Count>0)==(test[2]=="yes"),"Stored complication roll and treatment: "+string.Join("/",test));
        }
        Reset(b);Injury(b,"Eyes","Battered",.2,true);
        var bytes=OdinSerializer.SerializationUtility.SerializeValue(b.Unit,OdinSerializer.DataFormat.Binary);
        var restored=OdinSerializer.SerializationUtility.DeserializeValue<Unit>(bytes,OdinSerializer.DataFormat.Binary);
        Expect((bool)Call(injuries,"Same",b.Unit,restored),"Injury grade, tier, treatment and hidden roll survive serialization");
        Reset(b);for(int i=0;i<200;i++)Call(injuries,"RecordDamage",b.Unit,1);
        Expect(List(b.Unit,"Injuries").Count==0,"Sub-threshold hits cannot cause body-part injuries");
        Reset(b);Call(b.Unit,"ApplyCombatDamage",b.Unit.MaxHealth/2,false);
        Expect(List(b.Unit,"Injuries").Count==0,"Graze-exempt damage never rolls a body-part injury");
        Reset(b);Set(b.Unit,"TemporaryHealth",b.Unit.MaxHealth/2);Call(b.Unit,"ApplyCombatDamage",b.Unit.MaxHealth/3,true);
        Expect(List(b.Unit,"Injuries").Count==0,"Temporary HP alone shields against body-part injury rolls");
        Reset(b);Old(b,"Knee");Old(b,"Knee");Old(b,"Knee");
        Expect(b.CurrentMaxMovement()>=1,"Knee penalties cannot remove the final AP");
        Reset(b);int mana=(int)Get(b.Unit,"MaxMana");Old(b,"Head","PoorFocus");
        Expect((int)Get(b.Unit,"MaxMana")==Mathf.FloorToInt(mana*.95f),"Poor Focus reduces maximum mana by percentage: " + mana + " -> " + Get(b.Unit,"MaxMana"));
        Reset(b);Old(b,"Head","SluggishRecovery");Old(b,"Head","SluggishRecovery");Old(b,"Head","SluggishRecovery");
        Set(b.Unit,"BattleActive",false);b.Unit.Health=1;
        Call(Type("CompanyDailyRecovery"),"Recover",b.Unit,0.0,1);
        Expect(b.Unit.Health>=2,"Sluggish Recovery retains at least one daily HP recovery");
        Expect(Math.Abs((double)Get(injuries,"ModerateDailyRisk")-.05)<.00001 && Math.Abs((double)Get(injuries,"SevereDailyRisk")-.10)<.00001,"Approved daily complication chances are 5% and 10%");
        Reset(a);Reset(b);
        string[] parts={"Head","Eyes","Arm","Hand","Chest","Abdomen","Leg","Knee"};
        string[] grades={"Bruised","Battered","Mangled"};
        for(int grade=0;grade<3;grade++) foreach(string part in parts)
        {
            Reset(b); Injury(b,part,grades[grade]);
            if(part=="Head" || part=="Arm" || part=="Abdomen" || part=="Leg")
            {
                Stat stat=part=="Head"?Stat.Intelligence:part=="Arm"?Stat.Strength:part=="Leg"?Stat.Agility:Stat.Voracity;
                int baseline=part=="Head"?20:part=="Arm"?40:part=="Leg"?30:20;
                Expect(b.Unit.GetStat(stat)==Mathf.RoundToInt(baseline*(1-new[]{.2f,.4f,.6f}[grade])),grades[grade]+" "+part+" combat stat penalty");
            }
            if(part=="Eyes") Expect(Math.Abs((float)Call(injuries,"AccuracyPenalty",b.Unit)-(grade+1)*.10f)<.0001f,grades[grade]+" Eyes accuracy penalty");
            if(part=="Hand") Expect((int)Call(injuries,"DamagePenalty",b.Unit)==new[]{1,2,4}[grade],grades[grade]+" Hand damage penalty");
            if(part=="Knee") Expect((int)Call(injuries,"MovementPenalty",b.Unit)==grade+1,grades[grade]+" Knee AP penalty");
            if(part=="Chest") Expect(Math.Abs((float)Call(injuries,"HealingCeilingFraction",b.Unit)-(1-(grade+1)*.10f))<.0001f,grades[grade]+" Chest healing ceiling");
        }
        Reset(b);int originalHP=b.Unit.MaxHealth,originalHealth=b.Unit.Health;
        var torso=(Item)Call(Type("CompanyEquipment"),"Create","armor.torso");
        var prior=(Item[])Get(b.Unit,"Items"); var priorCopy=prior.ToArray(); bool oldLoadout=(bool)Get(b.Unit,"CompanyLoadout");
        try
        {
            if(prior.Length<9) Set(b.Unit,"Items",new Item[9]);
            Set(b.Unit,"CompanyLoadout",true);
            b.Unit.SetItem(torso,1,false);
            Expect(b.Unit.MaxHealth==originalHP && b.Unit.Health==originalHealth,"STR equipment does not grant or heal maximum HP");
            b.Unit.SetItem(null,1,false);
            var equipped=(Item[])Get(b.Unit,"Items");
            foreach(var mapping in new[]{new[]{"Head","Helmet"},new[]{"Eyes","Helmet"},new[]{"Arm","Arms"},new[]{"Hand","Arms"},new[]{"Chest","Torso"},new[]{"Abdomen","Torso"},new[]{"Leg","Legs"},new[]{"Knee","Legs"}})
            {
                Array.Clear(equipped,0,equipped.Length);
                var defs=((IEnumerable)Get(Type("CompanyEquipment"),"Catalog")).Cast<object>();
                var armor=defs.First(d=>Get(d,"Slot").ToString()==mapping[1]);
                int slot=(int)Convert.ChangeType(Get(armor,"Slot"),typeof(int));
                equipped[slot]=(Item)Call(Type("CompanyEquipment"),"Create",(string)Get(armor,"Id"));
                Expect((double)Call(injuries,"RiskMultiplier",b.Unit,E("CompanyBodyPart",mapping[0]))<1,"Armor protects "+mapping[0]);
            }
        }
        finally { Set(b.Unit,"Items",priorCopy);Set(b.Unit,"CompanyLoadout",oldLoadout); }
        Reset(a);Reset(b);
        float melee=b.GetAttackChance(a,false),ranged=b.GetAttackChance(a,true),vore=b.GetDevourChance(a);
        Injury(a,"Eyes","Bruised");
        Expect(Math.Abs(b.GetAttackChance(a,false)-Math.Max(0,melee-.1f))<.0001f && Math.Abs(b.GetAttackChance(a,true)-Math.Max(0,ranged-.1f))<.0001f &&
            Math.Abs(b.GetDevourChance(a)-Math.Max(0,vore-.1f))<.0001f,"Eye injury reduces final melee/ranged/vore odds by flat points");
        Reset(a);int spellBase=(int)Call(b,"ApplySpellTraitDamage",a,10);Injury(a,"Hand","Mangled");
        Expect((int)Call(b,"ApplySpellTraitDamage",a,10)==spellBase-4,"Hand injury reduces damaging spells too");
        Reset(a);Reset(b);b.Unit.AddTrait(Traits.ResilientIII);
        Expect(a.WeaponDamageAgainstTarget(b,false,.01f)==1,"A heavily reduced graze still deals at least one weapon damage");
        Reset(a);Reset(b);
        var bolt=Get(Type("SpellList"),"Fireball");
        float magicBefore=(float)Call(b,"GetMagicChance",a,bolt,0f,Stat.Intelligence);
        Injury(a,"Eyes","Battered");
        Expect(Math.Abs((float)Call(b,"GetMagicChance",a,bolt,0f,Stat.Intelligence)-Math.Max(0,magicBefore-.2f))<.0001f,"Eye injury applies flat penalty to magic accuracy");
        Reset(a);Reset(b);
        b.Unit.AddTrait(Traits.HotBlooded);int healthBefore=b.Unit.Health;
        Call(b,"Damage",1,true,true,DamageTypes.Fire,true,true);
        Expect(b.Unit.Health==healthBefore-1,"Successful spell hit retains minimum one after elemental resistance");
        Reset(a);Reset(b);
        var preyList=Activator.CreateInstance(typeof(List<>).MakeGenericType(Type("Prey")));
        var prey=Activator.CreateInstance(Type("Prey"),new object[]{b,a,preyList});
        Call(prey,"UpdateEscapeRate");float escapeBefore=(float)Get(prey,"EscapeRate");
        Set(a.Unit,"Type",UnitType.Soldier);Set(a.Unit,"PermanentInjuryImmune",false);
        try
        {
            for(int i=0;i<3;i++)Old(a,"Abdomen","SensitiveStomach");
            Call(prey,"UpdateEscapeRate");
            Expect(Math.Abs((float)Get(prey,"EscapeRate")-Math.Min(1,escapeBefore+.15f))<.0001f,"Sensitive Stomach III adds fifteen final escape percentage points");
            b.Unit.AddTrait(Traits.Submissive);Call(prey,"UpdateEscapeRate");
            Expect((float)Get(prey,"EscapeRate")==0,"Sensitive Stomach does not override Submissive escape prohibition");
        }
        finally { Set(a.Unit,"Type",UnitType.Leader);Set(a.Unit,"PermanentInjuryImmune",true);Set(b,"SelfPrey",null); }
        var originalStats=Get(b.Unit,"Stats");
        try { Set(b.Unit,"Stats",null);Expect(b.Unit.MaxHealth==1,"Incomplete preview units retain safe maximum HP getter"); }
        finally { Set(b.Unit,"Stats",originalStats); }
        Reset(a);Old(a,"Head");
        Expect(List(a.Unit,"Injuries").Count==0,"Company leader cannot receive permanent old injuries");
        Reset(a);Reset(b);
    }
    static void Reset(Actor_Unit actor)
    {
        ((List<Traits>)Get(actor.Unit,"Tags")).Clear();
        ((List<Traits>)Get(actor.Unit,"PermanentTraits")).Clear();
        ((IList)Get(actor.Unit,"StatusEffects")).Clear();
        List(actor.Unit,"Injuries").Clear();
        foreach(string field in new[]{"SharedTraits","PersistentSharedTraits","ActiveConditionalTraits"})
        { var list=Get(actor.Unit,field) as IList; if(list!=null)list.Clear(); }
        Set(actor.Unit,"BattleActive",true);
        actor.Unit.SetStatBase(Stat.Voracity,20);
        Set(actor.Unit,"TemporaryHealth",0);
        actor.Unit.SetStatBase(Stat.Strength,40);
        actor.Unit.SetStatBase(Stat.Agility,30);
        actor.Unit.SetStatBase(Stat.Intelligence,20);
        Set(actor.Unit,"Downed",false);
        Set(actor,"WeaponAttackThisTurn",false);Set(actor,"SpellCastThisTurn",false);Set(actor,"MovedThisTurn",false);
        Set(actor,"SkippedBySlacking",false);Set(actor,"TurnsSlacking",0);
        Call(actor.Unit,"InitializeTraits");actor.Unit.Health=actor.Unit.MaxHealth;actor.Movement=2;
    }
}
