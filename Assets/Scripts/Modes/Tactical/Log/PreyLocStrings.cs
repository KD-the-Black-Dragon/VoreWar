// Modified for the VoreWar mercenary-company conversion.
// 2026-10-30: Replace randomized flavor synonyms with deterministic neutral terms (spec 04B2).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.

public static class PreyLocStrings
{
    // Existing API names remain for compatibility; these helpers do not consume randomness.
    public static string ToSyn(this PreyLocation preyLocation)
    {
        switch (preyLocation)
        {
            case PreyLocation.stomach:
            case PreyLocation.stomach2:
                return "stomach";
            case PreyLocation.womb:
                return "womb";
            case PreyLocation.breasts:
                return "breasts";
            case PreyLocation.leftBreast:
                return "left breast";
            case PreyLocation.rightBreast:
                return "right breast";
            case PreyLocation.balls:
                return "testicles";
            case PreyLocation.anal:
                return "anal route";
            case PreyLocation.tail:
                return "tail";
            case PreyLocation.bladder:
                return "bladder";
            default:
                return "unknown location";
        }
    }

    public static string ToCockSyn()
    {
        return "penis";
    }

    public static string ToBreastSynPlural()
    {
        return "breasts";
    }

    public static string ToBallSynPlural()
    {
        return "testicles";
    }

    public static string ToBallSynSing()
    {
        return "scrotum";
    }

    public static string ToFluid(this PreyLocation preyLocation)
    {
        return "fluid";
    }

    public static string ToVerb(this PreyLocation preyLocation)
    {
        return "release";
    }

    public static string ToVerbPastTense(this PreyLocation preyLocation)
    {
        return "released";
    }

    public static string GetOralVoreVPCT()
    {
        return "consuming";
    }

    public static string GetOralVoreVPastT()
    {
        return "consumed";
    }

    public static string GetOralVoreVPT()
    {
        return "consumes";
    }

    public static string DigestVerbSyn()
    {
        return "digest";
    }

    public static string DigestsVerbSyn()
    {
        return "digests";
    }

    public static string DigestedVerbSyn()
    {
        return "digested";
    }

    public static string DigestingVerbSyn()
    {
        return "digesting";
    }

    public static string RandomFartSFX()
    {
        return "[sound]";
    }

    public static string BellyStuffedAdjSyn()
    {
        return "full";
    }

    public static string AcidicDeadlySyn()
    {
        return "acidic";
    }

    public static string ScatSyn()
    {
        return "waste";
    }

    public static string ScatAdjSyn()
    {
        return "unspecified";
    }

    public static string PussyAdjSyn()
    {
        return "unspecified";
    }

    public static string CockAdjSyn()
    {
        return "unspecified";
    }

    public static string SpoogeAdjSyn()
    {
        return "unspecified";
    }

}
