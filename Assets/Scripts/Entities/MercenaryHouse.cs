// Modified for the VoreWar mercenary-company conversion.
// 2026-10-30: Use approved sapient recruit pools and stop named-unique generation (spec 03A1).
// 2026-10-30: Retire the uncalled named-unique factory; preserve existing stock and individuals (spec 03B1).
// 2026-11-01: Separate stored species reads from intentional override creation (spec 03B3C).
// 2026-11-03: Remove permanently retired identity branches/entries while preserving shared behavior (spec 21).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using OdinSerializer;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class MercenaryHouse
{
    [OdinSerialize]
    internal List<MercenaryContainer> Mercenaries;

    static internal List<MercenaryContainer> UniqueMercs;

    [OdinSerialize]
    internal Vec2i Position;




    public MercenaryHouse(Vec2i position)
    {
        Position = position;
        Mercenaries = new List<MercenaryContainer>();
    }

    static internal void UpdateStaticStock()
    {
        // Named unique generation is retired. Do not discard previously existing individuals.
        if (UniqueMercs == null)
            UniqueMercs = new List<MercenaryContainer>();
    }

    internal void UpdateStock()
    {
        const int minimumReplacedPerTurn = 4;
        const int maxStock = 12;
        Race[] available = SpeciesPolicy.GetAvailable(SpeciesUse.Mercenary);
        if (available.Length == 0)
            return; // Preserve already offered individuals; disabling generation is not dismissal.
        int highestExp = State.GameManager.StrategyMode.ScaledExp;

        if (Mercenaries.Count > maxStock - minimumReplacedPerTurn)
        {
            List<Unit> units = Mercenaries.ConvertAll(merc => merc.Unit);
            Mercenaries.RemoveRange(0, Mercenaries.Count - (maxStock - minimumReplacedPerTurn));
            foreach (Unit u in units) {
                if (!Mercenaries.Any(mer => mer.Unit == u) && u.OnDiscard != null)
                {
                    u.OnDiscard();
                }
            }
        }
        for (int i = 0; i < 20; i++)
        {
            if (Mercenaries.Count < maxStock && available.Length > 0)
            {
                MercenaryContainer candidate = CreateMercenary(highestExp);
                if (candidate == null)
                    break;
                Mercenaries.Add(candidate);
            }
            else
                break;
        }
    }


    internal MercenaryContainer CreateMercenary(int highestExp)
    {
        MercenaryContainer merc = new MercenaryContainer();
        Race race;
        if (!SpeciesPolicy.TryRandom(SpeciesUse.Mercenary, out race))
            return null;


        int exp = (int)(highestExp * .8f) + State.Rand.Next(10);
        merc.Unit = new Unit((int)race, race, exp, true, UnitType.Mercenary, true);
        if (race < (Race)100 && merc.Unit.FixedGear == false)
        {
            if (merc.Unit.Items[0] == null)
            {
                if (merc.Unit.BestSuitedForRanged())
                    merc.Unit.Items[0] = State.World.ItemRepository.GetItem(ItemType.CompoundBow);
                else
                    merc.Unit.Items[0] = State.World.ItemRepository.GetItem(ItemType.Axe);
            }

            int r = State.Rand.Next(4);
            switch (r)
            {
                case 0:
                    merc.Unit.SetItem(State.World.ItemRepository.GetItem(ItemType.Shoes), 1);
                    break;

                case 1:
                    merc.Unit.SetItem(State.World.ItemRepository.GetItem(ItemType.Helmet), 1);
                    break;

                case 2:
                    merc.Unit.SetItem(State.World.ItemRepository.GetItem(ItemType.BodyArmor), 1);
                    break;

                case 3:
                    if (merc.Unit.BestSuitedForRanged())
                        merc.Unit.SetItem(State.World.ItemRepository.GetItem(ItemType.Gloves), 1);
                    else
                        merc.Unit.SetItem(State.World.ItemRepository.GetItem(ItemType.Gauntlet), 1);
                    break;

            }

            if (State.Rand.Next(10) == 0)
            {
                merc.Unit.AIClass = merc.Unit.BestSuitedForRanged() ? AIClass.MagicRanged : AIClass.MagicMelee;
                merc.Unit.SetItem(State.World.ItemRepository.GetRandomBook(), 1);
            }
        }

        var power = State.RaceSettings.GetPowerAdjustment(merc.Unit.Race);
        StrategicUtilities.SetAIClass(merc.Unit);
        StrategicUtilities.SpendLevelUps(merc.Unit);
        merc.Cost = (int)((25 + State.Rand.Next(15) + (.12 * exp)) * Random.Range(0.8f, 1.2f) * power);
        merc.Title = $"{InfoPanel.RaceSingular(merc.Unit)} - Mercenary";

        return merc;
    }

    // The former named-unique factory is retired. Existing stock remains data, not a generation source.
}

