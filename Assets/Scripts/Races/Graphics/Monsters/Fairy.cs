// Modified for the VoreWar mercenary-company conversion.
// 2026-11-01: Retire the disconnected Fairy graphics class payload; retain this path and notices (spec 03B8).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.

using System;
using System.Collections.Generic;
using UnityEngine;

enum FairyType : int
{
    Spring,
    Summer,
    Fall,
    Winter
}

static class FairyUtil
{

    internal static void SetSeason(Unit unit, FairyType season)
    {
        unit.BodyAccentType1 = (int)season;
        switch (season)
        {
            case FairyType.Spring:
                unit.InnateSpells = new List<SpellTypes>() { SpellTypes.Speed };
                break;
            case FairyType.Summer:
                unit.InnateSpells = new List<SpellTypes>() { SpellTypes.Valor };
                break;
            case FairyType.Fall:
                unit.InnateSpells = new List<SpellTypes>() { SpellTypes.Predation };
                break;
            case FairyType.Winter:
                unit.InnateSpells = new List<SpellTypes>() { SpellTypes.Shield };
                break;
        }
    }
    internal static FairyType GetSeason(Unit unit) => (FairyType)unit.BodyAccentType1;

    internal static ColorSwapPalette GetClothesColor(Actor_Unit actor)
    {
        switch (GetSeason(actor.Unit))
        {
            case FairyType.Spring:
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FairySpringClothes, actor.Unit.ClothingColor);
            case FairyType.Summer:
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FairySummerClothes, actor.Unit.ClothingColor);
            case FairyType.Fall:
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FairyFallClothes, actor.Unit.ClothingColor);
            default:
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FairyWinterClothes, actor.Unit.ClothingColor);
        }
    }
}
