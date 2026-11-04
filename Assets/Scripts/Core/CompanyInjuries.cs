// Added by the VoreWar mercenary-company conversion fork.
// 2026-11-01: Persistent injuries, derived penalties, daily recovery, treatment and sparring (spec 11).
// 2026-11-01: Announce each new injury in the battle log and queue visible-unit tactical text (spec 11 feedback).
// 2026-11-02: Restore the inherited 60 percent daily mana recovery through the single company/target daily stage (spec 12).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
// 2026-11-03: Implement four active stats and agreed combat rules (spec 29).
using System;
using System.Collections.Generic;
using System.Linq;

internal enum CompanyBodyPart { Hand, Arm, Leg, Head, Torso }
internal enum CompanyInjuryGrade { Minor, Serious, Old }
[Serializable]
internal sealed class CompanyInjury
{
    public string Id = Guid.NewGuid().ToString("N");
    public CompanyBodyPart Part;
    public CompanyInjuryGrade Grade;
    public int DaysRemaining;
    public bool Treated;
}

internal static class CompanyInjuries
{
    // Provisional tuning; deliberately kept here rather than exposed as player balance options.
    internal const int MinorDays = 3, SeriousDays = 8, TreatmentCost = 10, TreatmentDays = 2;
    internal static bool Temporary(CompanyInjury injury) { return injury.Grade != CompanyInjuryGrade.Old; }
    internal static bool Leader(Unit unit) { return unit.PermanentInjuryImmune || unit.Type == UnitType.Leader; }
    internal static bool Healthy(Unit unit)
    { return !unit.Digested && unit.LastingHealth >= unit.MaxHealth && !unit.Injuries.Any(Temporary); }
    internal static void RecordDamage(Unit unit, int lastingDamage)
    {
        if (!unit.BattleActive || unit.Digested || unit.Type == UnitType.Summon || lastingDamage <= 0) return;
        double fraction = Math.Min(1, lastingDamage / (double)Math.Max(1, unit.MaxHealth));
        var part = RandomPart(unit);
        double protection = RiskMultiplier(unit, part);
        if (State.Rand.NextDouble() < (.015 + .10 * fraction) * protection) Add(unit, part, CompanyInjuryGrade.Serious);
        else if (State.Rand.NextDouble() < .08 * (.2 + .8 * fraction) * protection) Add(unit, part, CompanyInjuryGrade.Minor);
        // One deep-attrition opportunity per battle, triggered by lasting damage rather than grey HP.
        // Zero HP has no special multiplier over an otherwise comparable near-zero hit.
        if (!unit.AttritionInjuryChecked && unit.LastingHealth <= unit.MaxHealth * .20)
        {
            unit.AttritionInjuryChecked = true;
            part = RandomPart(unit);
            if (State.Rand.NextDouble() < .20 * RiskMultiplier(unit, part)) Add(unit, part, CompanyInjuryGrade.Serious);
        }
    }
    static CompanyBodyPart RandomPart(Unit unit)
    {
        bool animal = SpeciesPolicy.IsCurated(unit.Race) && SpeciesPolicy.GetKind(unit.Race) == SpeciesKind.Animal;
        return animal ? (State.Rand.Next(2) == 0 ? CompanyBodyPart.Head : CompanyBodyPart.Torso) : (CompanyBodyPart)State.Rand.Next(5);
    }
    internal static double RiskMultiplier(Unit unit, CompanyBodyPart part)
    {
        CompanySlot slot = part == CompanyBodyPart.Head ? CompanySlot.Helmet : part == CompanyBodyPart.Torso ? CompanySlot.Torso :
            part == CompanyBodyPart.Leg ? CompanySlot.Legs : CompanySlot.Arms;
        return 1.0 / (1 + CompanyEquipment.Protection(unit, slot));
    }
    static void Add(Unit unit, CompanyBodyPart part, CompanyInjuryGrade grade)
    {
        var injury = new CompanyInjury { Part = part, Grade = grade, DaysRemaining = grade == CompanyInjuryGrade.Minor ? MinorDays : SeriousDays };
        unit.Injuries.Add(injury);
        var mode = State.GameManager == null ? null : State.GameManager.TacticalMode;
        if (mode == null) return;
        mode.Log?.RegisterMiscellaneous(unit.Name + " suffered " + Description(injury) + ".");
        if (mode.turboMode || TacticalUtilities.Units == null) return;
        var actor = TacticalUtilities.GetActorOf(unit);
        if (actor != null && actor.Visible && actor.UnitSprite != null) actor.UnitSprite.DisplayInjury(Name(injury));
    }
    internal static int Severity(CompanyInjury injury, int minor, int serious, int old)
    { return injury.Grade == CompanyInjuryGrade.Minor ? minor : injury.Grade == CompanyInjuryGrade.Serious ? serious : old; }
    internal static int StatPenalty(Unit unit, Stat stat)
    { return unit.Injuries.Where(i => (stat == Stat.Agility && i.Part == CompanyBodyPart.Hand) || (stat == Stat.Intelligence && i.Part == CompanyBodyPart.Head)).Sum(i => Severity(i, 2, 5, 2)); }
    internal static int MovementPenalty(Unit unit)
    { return unit.Injuries.Where(i => i.Part == CompanyBodyPart.Leg).Sum(i => Severity(i, 1, 2, 1)); }
    internal static int DamagePenalty(Unit unit)
    { return unit.Injuries.Sum(i => i.Part == CompanyBodyPart.Arm ? Severity(i, 1, 5, 2) : i.Part == CompanyBodyPart.Torso ? Severity(i, 1, 4, 1) : 0); }
    internal static string Name(CompanyInjury injury)
    {
        string[][] names = {
            new[] { "Sore hand", "Battered hand", "Unsteady hand" },
            new[] { "Strained arm", "Battered arm", "Stiff arm" },
            new[] { "Sore leg", "Badly strained leg", "Old leg injury" },
            new[] { "Ringing head", "Rattled head", "Lingering headaches" },
            new[] { "Bruised body", "Heavy bruising", "Aching body" }
        };
        return names[(int)injury.Part][(int)injury.Grade];
    }
    internal static string Description(CompanyInjury injury)
    {
        string penalty = injury.Part == CompanyBodyPart.Hand ? "AGI -" + Severity(injury, 2, 5, 2) :
            injury.Part == CompanyBodyPart.Head ? "INT -" + Severity(injury, 2, 5, 2) :
            injury.Part == CompanyBodyPart.Leg ? "Movement -" + Severity(injury, 1, 2, 1) :
            "Weapon damage -" + (injury.Part == CompanyBodyPart.Arm ? Severity(injury, 1, 5, 2) : Severity(injury, 1, 4, 1));
        return Name(injury) + " [" + injury.Grade + "] — " + penalty +
            (Temporary(injury) ? " | Recovery remaining: " + injury.DaysRemaining + (injury.Treated ? " (treated)" : "") : " | Permanent");
    }
    internal static string Summary(Unit unit)
    {
        int temporary = unit.Injuries.Count(Temporary), old = unit.Injuries.Count - temporary;
        return unit.Injuries.Count == 0 ? "No injuries" : temporary + " recovering, " + old + " old injuries";
    }
    internal static void Recover(Unit unit, int progress)
    {
        foreach (var injury in unit.Injuries.Where(Temporary).ToArray())
        {
            injury.DaysRemaining = Math.Max(0, injury.DaysRemaining - progress);
            if (injury.DaysRemaining > 0) continue;
            if (injury.Grade == CompanyInjuryGrade.Serious && !Leader(unit)) injury.Grade = CompanyInjuryGrade.Old;
            else unit.Injuries.Remove(injury);
        }
    }
    internal static bool CanTreat(Unit unit)
    { return unit.Injuries.Any(i => Temporary(i) && !i.Treated && i.DaysRemaining > 1); }
    internal static void Treat(CompanyCampaign state, string id)
    {
        var c = state.Characters.Single(x => x.Id == id);
        if (state.Busy || state.Preview || !state.Towns.Any(t => t.Tile == state.Position) || !state.Members.Any(m => m.CharacterId == id) ||
            state.Gold < TreatmentCost || !CanTreat(c.Unit)) throw new InvalidOperationException("Visit town with enough gold and an untreated temporary injury.");
        state.Gold -= TreatmentCost;
        foreach (var injury in c.Unit.Injuries.Where(i => Temporary(i) && !i.Treated && i.DaysRemaining > 1))
        { injury.DaysRemaining = Math.Max(1, injury.DaysRemaining - TreatmentDays); injury.Treated = true; }
    }
    internal static void Validate(Unit unit)
    {
        var ids = new HashSet<string>();
        foreach (var i in unit.Injuries)
            if (i == null || string.IsNullOrEmpty(i.Id) || !ids.Add(i.Id) || !Enum.IsDefined(typeof(CompanyBodyPart), i.Part) ||
                !Enum.IsDefined(typeof(CompanyInjuryGrade), i.Grade) || i.DaysRemaining < 0 || i.DaysRemaining > SeriousDays ||
                (Temporary(i) && i.DaysRemaining == 0) || (!Temporary(i) && (i.DaysRemaining != 0 || Leader(unit))))
                throw new InvalidOperationException("Invalid persistent injury state.");
    }
    internal static bool Same(Unit first, Unit second)
    {
        return first.PermanentInjuryImmune == second.PermanentInjuryImmune && first.Injuries.Count == second.Injuries.Count &&
            first.Injuries.Zip(second.Injuries, (a, b) => a.Id == b.Id && a.Part == b.Part && a.Grade == b.Grade && a.DaysRemaining == b.DaysRemaining && a.Treated == b.Treated).All(equal => equal);
    }
}

internal sealed class CompanyDailyRecovery : ICompanyDailySystem
{
    public CompanyDailyStage Stage { get { return CompanyDailyStage.Recovery; } }
    public void Apply(CompanyCampaign state)
    {
        if (state.Preview) return;
        bool town = state.Towns.Any(t => t.Tile == state.Position);
        double rate = !state.RestingDay ? .03 : town ? .15 : .10;
        int progress = !state.RestingDay ? 1 : town ? 3 : 2;
        foreach (var member in state.Members)
            Recover(state.Characters.Single(c => c.Id == member.CharacterId).Unit, rate, progress);
        foreach (var contract in state.Contracts.Where(c => c.Status == CompanyContractStatus.Active))
            foreach (var c in CompanyContracts.Survivors(state, contract)) Recover(c.Unit, .02, 1);
    }
    static void Recover(Unit unit, double rate, int progress)
    {
        if (unit.Digested || unit.Type == UnitType.Summon) return;
        unit.Health = Math.Min(unit.MaxHealth, unit.Health + Math.Max(1, (int)Math.Ceiling(unit.MaxHealth * rate)));
        unit.RestoreManaPct(.6f); // Carry forward the original Army.Refresh daily mana rate, without its empire side effects.
        CompanyInjuries.Recover(unit, progress);
    }
}
internal sealed class CompanySparring : ICompanyDailySystem
{
    public CompanyDailyStage Stage { get { return CompanyDailyStage.Sparring; } }
    public void Apply(CompanyCampaign state)
    {
        if (state.Preview) return;
        foreach (var member in state.Members)
        {
            var unit = state.Characters.Single(c => c.Id == member.CharacterId).Unit;
            if (CompanyInjuries.Healthy(unit)) unit.GiveExp(2); // Existing trait XP modifiers and level choices still apply.
        }
    }
}
