// Modified for the VoreWar mercenary-company conversion.
// 2026-11-03: Remove permanently retired identity branches/entries while preserving shared behavior (spec 21).
// See CHANGES.md and License.txt for GPL-3.0 terms.
using System.Collections.Generic;
using UnityEngine;

class SpringSlug : BlankSlate
{
    readonly Sprite[] Sprites = SpriteDictionary.SpringSlug;

    public SpringSlug()
    {
        CanBeGender = new List<Gender>() { Gender.None };
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.SlugSkin);
        GentleAnimation = true;

        Body = new SpriteExtraInfo(4, BodySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlugSkin, s.Unit.SkinColor));
        Head = new SpriteExtraInfo(5, HeadSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlugSkin, s.Unit.SkinColor));
        BodyAccent = new SpriteExtraInfo(1, BodyAccentSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlugSkin, s.Unit.SkinColor)); // tail end
        Belly = new SpriteExtraInfo(3, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlugSkin, s.Unit.SkinColor));
        BodySize = new SpriteExtraInfo(2, BodySizeSprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlugSkin, s.Unit.SkinColor)); // belly cover up

        clothingColors = 0;
    }

    protected override Sprite BodySprite(Actor_Unit actor)
    {
        if (actor.HasBelly == false)
            return Sprites[2];
        return Sprites[5 + actor.GetStomachSize(9)];
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (actor.IsOralVoring || actor.IsAttacking)
            return Sprites[1];
        return Sprites[0];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) => Sprites[4];

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly == false)
            return null;
        return Sprites[16 + actor.GetStomachSize(9)];
    }

    protected override Sprite BodySizeSprite(Actor_Unit actor) // belly cover up
    {
        if (actor.HasBelly == false)
            return Sprites[3];
        return null;
    }

}
