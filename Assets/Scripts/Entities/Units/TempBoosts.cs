// Modified for the VoreWar mercenary-company conversion.
// 2026-11-03: Preserve temporary maximum HP/mana and remaining extra attacks in battle saves (spec 19).
// See CHANGES.md and License.txt for GPL-3.0 terms.
using OdinSerializer;

public class TempBoosts
{
    [OdinSerialize] internal int HealthBoost = 0;
    [OdinSerialize] internal int ManaBoost = 0;

    [OdinSerialize] internal int ExtraAttacks = 0;
    

    internal void ResetAll()
    {
        HealthBoost = 0;
        ManaBoost = 0;
        ExtraAttacks = 0;
    }
    internal int ModifyExtraAttacks(int amnt)
    {
        ExtraAttacks += amnt;
        if (ExtraAttacks < 0)
            ExtraAttacks = 0;
        return ExtraAttacks;
    }
}