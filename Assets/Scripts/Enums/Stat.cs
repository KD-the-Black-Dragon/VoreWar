// Modified for the VoreWar mercenary-company conversion.
// 2026-11-03: Implement four active stats and agreed combat rules (spec 29).
// See CHANGES.md and License.txt for GPL-3.0 terms.
public enum Stat : int
{
    Strength,
    Dexterity,
    Voracity,
    Agility,
    Will,
    Intelligence,
    Mind = Intelligence, // Serialized compatibility alias; not an active fifth stat.
    Endurance,
    Stomach,
    Leadership,
    /// <summary>Used as ArraySize</summary>
    None
}
