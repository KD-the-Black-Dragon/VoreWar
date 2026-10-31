// Added by the VoreWar mercenary-company conversion fork.
// 2026-10-30: Approved species, explicit roles and global generation preferences (spec 03A1).
// 2026-10-30: Make new copies, spawns and forms explicit policy uses; preserve restoration separately (spec 03A2).
// 2026-10-30: Expose permanent retirement separately from approval and player preferences (spec 03B1).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal enum SpeciesKind { Sapient, Animal }
internal enum SpeciesUse { Leader, Mercenary, Enemy, Skirmish, CreatureSummon, Copy, Spawn, Transformation }

internal static class SpeciesPolicy
{
    internal const string UnavailableMessage = "No approved species are available yet. Species will be added individually during curation.";

    // Intentionally empty. Approval and classification are developer decisions, not save/preset data.
    private static readonly Dictionary<Race, SpeciesKind> CuratedSpecies = new Dictionary<Race, SpeciesKind>();
    private static readonly HashSet<Race> RetiredSpecies = new HashSet<Race>
    {
        Race.Aabayx,
        Race.Abakhanskya,
        Race.Alraune,
        Race.Asura,
        Race.Auri,
        Race.Bears,
        Race.Bella,
        Race.BoomBunnies,
        Race.Cake,
        Race.Cats,
        Race.Centaur,
        Race.Cherub,
        Race.Cierihaka,
        Race.Cockatrice,
        Race.Collectors,
        Race.Compy,
        Race.Crux,
        Race.Crypters,
        Race.DRACO,
        Race.DarkSwallower,
        Race.DewSprites,
        Race.Dogs,
        Race.Driders,
        Race.EarthDryad,
        Race.Earthworms,
        Race.Eevee,
        Race.Equaleon,
        Race.Erin,
        Race.Fairies,
        Race.Feit,
        Race.FeralEevee,
        Race.FeralEqualeon,
        Race.FeralFrogs,
        Race.FeralOrcas,
        Race.FeralSharks,
        Race.Foxes,
        Race.FungalDryad,
        Race.Ghosts,
        Race.Goblins,
        Race.Hamsters,
        Race.Harpies,
        Race.Humans,
        Race.Iliijiith,
        Race.Imps,
        Race.Ki,
        Race.Konane,
        Race.Lamia,
        Race.MainlandElves,
        Race.Merfolk,
        Race.Nectar,
        Race.Olivia,
        Race.OoviKat,
        Race.Puca,
        Race.Pudding,
        Race.Raptor,
        Race.Renamon,
        Race.RiverDryad,
        Race.RwuMercenaries,
        Race.Ryan,
        Race.Salix,
        Race.Schiwardez,
        Race.Scorch,
        Race.Scylla,
        Race.Selicia,
        Race.Seraph,
        Race.Seville,
        Race.Singularity,
        Race.Skapa,
        Race.Slimes,
        Race.SoulSprite,
        Race.Succubi,
        Race.Taraluxia,
        Race.Tatltuae,
        Race.Taurus,
        Race.Tigers,
        Race.Umbreon,
        Race.Vagrants,
        Race.Viisels,
        Race.Vipers,
        Race.ViraeUltimae,
        Race.Vision,
        Race.Voilin,
        Race.Whisp,
        Race.Wolves,
        Race.WoodDryad,
        Race.Xelhilde,
        Race.Yordles,
        Race.Youko,
        Race.Zera,
        Race.Zoey,
    };
    private static readonly Dictionary<Race, bool> PlayerChoices = new Dictionary<Race, bool>();

    internal static bool IsRetired(Race race)
    {
        return RetiredSpecies.Contains(race);
    }

    internal static bool IsCurated(Race race)
    {
        return (int)race >= 0 && Enum.IsDefined(typeof(Race), race) && !IsRetired(race) && CuratedSpecies.ContainsKey(race);
    }

    internal static Race[] GetCuratedSpecies()
    {
        return CuratedSpecies.Keys.Where(IsCurated).OrderBy(r => r.ToString()).ToArray();
    }

    internal static SpeciesKind GetKind(Race race)
    {
        return CuratedSpecies[race];
    }

    private static string PreferenceKey(Race race)
    {
        return "CuratedSpeciesEnabled " + (int)race;
    }

    internal static bool IsEnabled(Race race)
    {
        if (!IsCurated(race))
            return false;
        bool enabled;
        return !PlayerChoices.TryGetValue(race, out enabled) || enabled;
    }

    internal static void SetEnabled(Race race, bool enabled)
    {
        if (IsCurated(race))
            PlayerChoices[race] = enabled;
    }

    internal static bool IsAllowed(Race race, SpeciesUse use)
    {
        if (!IsEnabled(race))
            return false;
        SpeciesKind kind = GetKind(race);
        if (use == SpeciesUse.Leader || use == SpeciesUse.Mercenary)
            return kind == SpeciesKind.Sapient;
        if (use == SpeciesUse.CreatureSummon)
            return kind == SpeciesKind.Animal;
        return use == SpeciesUse.Enemy || use == SpeciesUse.Skirmish || use == SpeciesUse.Copy ||
            use == SpeciesUse.Spawn || use == SpeciesUse.Transformation;
    }

    internal static Race[] GetAvailable(SpeciesUse use)
    {
        return GetCuratedSpecies().Where(r => IsAllowed(r, use)).ToArray();
    }

    internal static bool TryRandom(SpeciesUse use, out Race race)
    {
        Race[] available = GetAvailable(use);
        race = Race.none;
        if (available.Length == 0)
            return false;
        race = available[State.Rand.Next(available.Length)];
        return true;
    }

    internal static void LoadSavedPreferences()
    {
        PlayerChoices.Clear();
        foreach (Race race in GetCuratedSpecies())
            PlayerChoices[race] = PlayerPrefs.GetInt(PreferenceKey(race), 1) == 1;
    }

    internal static void SavePreferences()
    {
        foreach (Race race in GetCuratedSpecies())
            PlayerPrefs.SetInt(PreferenceKey(race), IsEnabled(race) ? 1 : 0);
    }
}
