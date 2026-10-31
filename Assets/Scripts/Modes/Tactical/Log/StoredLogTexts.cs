// Modified for the VoreWar mercenary-company conversion.
// 2026-10-30: Replace the legacy narrative registry with neutral, null-safe placeholders (spec 04B1).
// Upstream narrative contributions were credited to Tatltuae and Flame.
// Upstream notes also thank Ted ArCon (Discord) for brainstorming assistance.
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using System;
using System.Collections.Generic;
using System.Linq;
using static TacticalMessageLog;

static class StoredLogTexts
{
    static StoredLogTexts()
    {
        InitializeLists();
    }

    internal class EventString
    {
        internal Race ActorRace;
        internal Race TargetRace;
        internal Func<EventLog, string> GetString;
        internal Predicate<EventLog> Conditional;
        internal int Priority;

        public EventString(Func<EventLog, string> getString, int priority = 0, Race actorRace = (Race)4000, Race targetRace = (Race)4000, Predicate<EventLog> conditional = null)
        {
            Priority = priority;
            ActorRace = actorRace;
            TargetRace = targetRace;
            GetString = getString;
            if (conditional == null)
                Conditional = (s) => true;
            else
                Conditional = conditional;
        }
    }

    internal enum MessageTypes
    {
        SwallowMessages,
        RandomDigestionMessages,
        BellyRubMessages,
        CockVoreMessages,
        AnalVoreMessages,
        UnbirthMessages,
        TailVoreMessages,
        DigestionDeathMessages,
        BreastVoreMessages,
        BladderMessages,
        AbsorptionMessages,
        BreastRubMessages,
        TailRubMessages,
        BallMassageMessages,
        TransferMessages,
        KissTransferMessages,
        VoreStealMessages,
        BreastFeedMessages,
        CumFeedMessages,
        RebirthMessages,
        GreatEscapeKeep,
        GreatEscapeFlee,
    }

    static internal List<EventString> Redirect(MessageTypes type)
    {
        InitializeLists();
        switch (type)
        {
            case MessageTypes.SwallowMessages:
                return SwallowMessages;
            case MessageTypes.RandomDigestionMessages:
                return RandomDigestionMessages;
            case MessageTypes.BellyRubMessages:
                return BellyRubMessages;
            case MessageTypes.CockVoreMessages:
                return CockVoreMessages;
            case MessageTypes.AnalVoreMessages:
                return AnalVoreMessages;
            case MessageTypes.UnbirthMessages:
                return UnbirthMessages;
            case MessageTypes.TailVoreMessages:
                return TailVoreMessages;
            case MessageTypes.DigestionDeathMessages:
                return DigestionDeathMessages;
            case MessageTypes.BreastVoreMessages:
                return BreastVoreMessages;
            case MessageTypes.BladderMessages:
                return BladderMessages;
            case MessageTypes.AbsorptionMessages:
                return AbsorptionMessages;
            case MessageTypes.BreastRubMessages:
                return BreastRubMessages;
            case MessageTypes.TailRubMessages:
                return TailRubMessages;
            case MessageTypes.BallMassageMessages:
                return BallMassageMessages;
            case MessageTypes.TransferMessages:
                return TransferMessages;
            case MessageTypes.KissTransferMessages:
                return KissTransferMessages;
            case MessageTypes.VoreStealMessages:
                return VoreStealMessages;
            case MessageTypes.BreastFeedMessages:
                return BreastFeedMessages;
            case MessageTypes.CumFeedMessages:
                return CumFeedMessages;
            case MessageTypes.RebirthMessages:
                return RebirthMessages;
            case MessageTypes.GreatEscapeKeep:
                return GreatEscapeKeepMessages;
            case MessageTypes.GreatEscapeFlee:
                return GreatEscapeFleeMessages;
            default:
                return UnknownMessages;
        }
    }

    internal static List<EventString> SwallowMessages;
    internal static List<EventString> RandomDigestionMessages;
    internal static List<EventString> BellyRubMessages;
    internal static List<EventString> CockVoreMessages;
    internal static List<EventString> AnalVoreMessages;
    internal static List<EventString> UnbirthMessages;
    internal static List<EventString> TailVoreMessages;
    internal static List<EventString> DigestionDeathMessages;
    internal static List<EventString> BreastVoreMessages;
    internal static List<EventString> BladderMessages;
    internal static List<EventString> AbsorptionMessages;
    internal static List<EventString> BreastRubMessages;
    internal static List<EventString> TailRubMessages;
    internal static List<EventString> BallMassageMessages;
    internal static List<EventString> TransferMessages;
    internal static List<EventString> KissTransferMessages;
    internal static List<EventString> VoreStealMessages;
    internal static List<EventString> BreastFeedMessages;
    internal static List<EventString> CumFeedMessages;
    internal static List<EventString> RebirthMessages;
    internal static List<EventString> GreatEscapeKeepMessages;
    internal static List<EventString> GreatEscapeFleeMessages;

    private static readonly List<EventString> UnknownMessages = new List<EventString>
    {
        new EventString(e => Describe(e, "Unrecognized battle event"))
    };

    internal static void InitializeLists()
    {
        if (SwallowMessages == null)
            SwallowMessages = new List<EventString> { new EventString(e => Describe(e, "Consumed")) };
        if (RandomDigestionMessages == null)
            RandomDigestionMessages = new List<EventString> { new EventString(e => Describe(e, "Digestion continues")) };
        if (BellyRubMessages == null)
            BellyRubMessages = new List<EventString> { new EventString(e => Describe(e, "Support (BellyRub)")) };
        if (CockVoreMessages == null)
            CockVoreMessages = new List<EventString> { new EventString(e => Describe(e, "Consumed (CockVore)")) };
        if (AnalVoreMessages == null)
            AnalVoreMessages = new List<EventString> { new EventString(e => Describe(e, "Consumed (AnalVore)")) };
        if (UnbirthMessages == null)
            UnbirthMessages = new List<EventString> { new EventString(e => Describe(e, "Consumed (Unbirth)")) };
        if (TailVoreMessages == null)
            TailVoreMessages = new List<EventString> { new EventString(e => Describe(e, "Consumed (TailVore)")) };
        if (DigestionDeathMessages == null)
            DigestionDeathMessages = new List<EventString> { new EventString(e => Describe(e, "Digestion completed")) };
        if (BreastVoreMessages == null)
            BreastVoreMessages = new List<EventString> { new EventString(e => Describe(e, "BreastVore")) };
        if (BladderMessages == null)
            BladderMessages = new List<EventString> { new EventString(e => Describe(e, "BladderVore")) };
        if (AbsorptionMessages == null)
            AbsorptionMessages = new List<EventString> { new EventString(e => Describe(e, "Absorption completed")) };
        if (BreastRubMessages == null)
            BreastRubMessages = new List<EventString> { new EventString(e => Describe(e, "Support (BreastRub)")) };
        if (TailRubMessages == null)
            TailRubMessages = new List<EventString> { new EventString(e => Describe(e, "Support (TailRub)")) };
        if (BallMassageMessages == null)
            BallMassageMessages = new List<EventString> { new EventString(e => Describe(e, "Support (BallMassage)")) };
        if (TransferMessages == null)
            TransferMessages = new List<EventString> { new EventString(e => Describe(e, "Transfer completed")) };
        if (KissTransferMessages == null)
            KissTransferMessages = new List<EventString> { new EventString(e => Describe(e, "Transfer completed (KissTransfer)")) };
        if (VoreStealMessages == null)
            VoreStealMessages = new List<EventString> { new EventString(e => Describe(e, "Transfer completed (VoreSteal)")) };
        if (BreastFeedMessages == null)
            BreastFeedMessages = new List<EventString> { new EventString(e => Describe(e, "Support (Suckle)")) };
        if (CumFeedMessages == null)
            CumFeedMessages = new List<EventString> { new EventString(e => Describe(e, "Support (FeedCum)")) };
        if (RebirthMessages == null)
            RebirthMessages = new List<EventString> { new EventString(e => Describe(e, "Birth")) };
        if (GreatEscapeKeepMessages == null)
            GreatEscapeKeepMessages = new List<EventString> { new EventString(e => Describe(e, "Escape; remains in battle")) };
        if (GreatEscapeFleeMessages == null)
            GreatEscapeFleeMessages = new List<EventString> { new EventString(e => Describe(e, "Escape; fled the battle")) };
    }

    internal static string UnitName(Unit unit)
    {
        return unit == null || string.IsNullOrEmpty(unit.Name) ? "Unknown unit" : unit.Name;
    }

    internal static string Describe(EventLog action, string label)
    {
        if (action == null)
            return "Battle event unavailable.";
        string target = action.Target == null ? "No target" : UnitName(action.Target);
        string prey = action.Prey == null ? "" : "; contained unit: " + UnitName(action.Prey);
        return $"{UnitName(action.Unit)}: {label}; target: {target}{prey}.";
    }

    internal static string Format(MessageTypes type, EventLog action)
    {
        if (action == null)
            return "Battle event unavailable.";
        List<EventString> list = Redirect(type);
        EventString[] eligible = (list ?? UnknownMessages).Where(e => e != null && e.GetString != null &&
            (e.ActorRace == (Race)4000 || (action.Unit != null && e.ActorRace == action.Unit.Race)) &&
            (e.TargetRace == (Race)4000 || (action.Target != null && e.TargetRace == action.Target.Race)) &&
            (e.Conditional == null || e.Conditional(action))).ToArray();
        if (eligible.Length == 0)
            return Describe(action, "Battle event");
        int priority = eligible.Max(e => e.Priority);
        EventString[] choices = eligible.Where(e => e.Priority == priority).ToArray();
        // Placeholder lists are deterministic. Future variants retain the existing shared RNG.
        EventString chosen = choices.Length == 1 ? choices[0] : choices[State.Rand.Next(choices.Length)];
        string result = chosen.GetString(action);
        return string.IsNullOrEmpty(result) ? Describe(action, "Battle event") : result;
    }
}
