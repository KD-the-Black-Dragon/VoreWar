// Modified for the VoreWar mercenary-company conversion.
// 2026-11-03: Remove permanently retired identity branches/entries while preserving shared behavior (spec 21).
// See CHANGES.md and License.txt for GPL-3.0 terms.
using System.Collections.Generic;
using UnityEngine;

class Ant : BlankSlate
{
    public Ant()
    {
        CanBeGender = new List<Gender>() { Gender.None };

        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Ant);
        GentleAnimation = true;
        Head = new SpriteExtraInfo(1, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Ant, s.Unit.SkinColor)); // Head
        Belly = new SpriteExtraInfo(0, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Ant, s.Unit.SkinColor)); // Belly
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly) // Belly
    {
        if (actor.Unit.Predator == false)
            return SpriteDictionary.Ant[2];

        int size = actor.GetStomachSize(16, 0.75f);

        if (size > 15) size = 15;

        return SpriteDictionary.Ant[2 + actor.GetStomachSize(15)];
    }

    protected override Sprite HeadSprite(Actor_Unit actor) // Head
    {
        if (actor.IsOralVoring || actor.IsAttacking) return SpriteDictionary.Ant[1];

        return SpriteDictionary.Ant[0];
    }
}
