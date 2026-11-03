// Modified for the VoreWar mercenary-company conversion.
// 2026-11-03: Remove permanently retired identity branches/entries while preserving shared behavior (spec 21).
// See CHANGES.md and License.txt for GPL-3.0 terms.
using System.Collections.Generic;
using UnityEngine;

class FeralSlime : BlankSlate
{
    internal FeralSlime()
    {
        CanBeGender = new List<Gender>() { Gender.None };
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.SlimeMain); ;
        GentleAnimation = true;

        BodyAccessory = new SpriteExtraInfo(5, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeMain, s.Unit.SkinColor));
        BodyAccent = new SpriteExtraInfo(10, BodyAccentSprite, null, null);
        Belly = new SpriteExtraInfo(2, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeMain, s.Unit.SkinColor));

    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        if (actor.IsAttacking)
        {
            AddOffset(Belly, 0, 10);
            AddOffset(BodyAccessory, 0, 10);
            AddOffset(BodyAccent, 0, 10);
        }
    }

    internal override void RandomCustom(Unit unit)
    {
        unit.SkinColor = State.Rand.Next(SkinColors);
    }
    //protected override Sprite BodySprite(Actor_Unit actor) => SpriteDictionary.FeralSlime[0];
    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly == false)
            return SpriteDictionary.FeralSlime[0];

        int size = actor.GetStomachSize(8);
        return SpriteDictionary.FeralSlime[0 + actor.GetStomachSize(6)];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        if (actor.HasBelly == false)
            return SpriteDictionary.FeralSlime[8];

        int size = actor.GetStomachSize(8);
        return SpriteDictionary.FeralSlime[8 + actor.GetStomachSize(6)];
    }
    protected override Sprite AccessorySprite(Actor_Unit actor)
    {
        if (actor.HasBelly == false)
            return null;
        int size = actor.GetStomachSize(24, 0.7f);
        return SpriteDictionary.FeralSlime[16 + actor.GetStomachSize(20, 0.7f)];
    }
}

