// Modified for the VoreWar mercenary-company conversion.
// 2026-11-03: Remove permanently retired identity branches/entries while preserving shared behavior (spec 21).
// See CHANGES.md and License.txt for GPL-3.0 terms.
public class Spawn : Unit
{

    public Spawn(int side, Race race, int startingXP) : base(side, race, startingXP, true, UnitType.Spawn)
    {
        var raceStats = State.RaceSettings.GetRaceStats(Race);

        Stats[(int)Stat.Strength] = 5 + raceStats.Strength.Minimum;
        Stats[(int)Stat.Dexterity] = 5 + raceStats.Dexterity.Minimum;
        Stats[(int)Stat.Endurance] = 6 + raceStats.Endurance.Minimum;
        Stats[(int)Stat.Mind] = 6 + raceStats.Mind.Minimum;
        Stats[(int)Stat.Will] = 6 + raceStats.Will.Minimum;
        Stats[(int)Stat.Agility] = 9 + raceStats.Agility.Minimum;
        Stats[(int)Stat.Voracity] = 8 + raceStats.Voracity.Minimum;
        Stats[(int)Stat.Stomach] = 4 + raceStats.Stomach.Minimum;

        Health = MaxHealth;
        if (race == Race.Lizards)
            Races.GetRace(Race.Lizards).RandomCustom(this);
        ExpMultiplier = 2;
        Type = UnitType.Spawn;
        var list = CustomizationDataStorer.GetCompatibleCustomizations(race, UnitType.Spawn, false);
        if (list != null && list.Count > 0)
        {
            CustomizationDataStorer.ExternalCopyToUnit(list[State.Rand.Next(list.Count)], this);
        }
    }

    public override int GetStatTotal()
    {
        return base.GetStatTotal();
    }
}

