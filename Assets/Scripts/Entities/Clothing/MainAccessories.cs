// Modified for the VoreWar mercenary-company conversion.
// 2026-11-03: Remove permanently retired identity branches/entries while preserving shared behavior (spec 21).
// See CHANGES.md and License.txt for GPL-3.0 terms.
class SharedMask : ClothingAccessory
{
    public SharedMask()
    {
        clothing1 = new SpriteExtraInfo(19, null, null);
    }

    public override void Configure(CompleteSprite sprite, Actor_Unit actor)
    {
        clothing1.GetSprite = (s) => SpriteDictionary.SharedMaskParts[1];
        switch (actor.Unit.Race)
        {
            default:
                clothing1.YOffset = 0;
                break;
        }
        base.Configure(sprite, actor);
    }
}

class SantaHat : ClothingAccessory
{
    public SantaHat()
    {
        clothing1 = new SpriteExtraInfo(28, null, null);
        ReqWinterHoliday = true;
    }

    public override void Configure(CompleteSprite sprite, Actor_Unit actor)
    {
        clothing1.GetSprite = (s) => SpriteDictionary.SantaHat;
        switch (actor.Unit.Race)
        {
            case Race.Frogs:
                clothing1.YOffset = 9 * .625f;
                break;
            case Race.Bees:
                clothing1.YOffset = 26 * .625f;
                break;
            case Race.Kangaroos:
                clothing1.YOffset = -3 * .625f;
                break;
            case Race.Sergal:
                clothing1.YOffset = 26 * .625f;
                break;
            default:
                clothing1.YOffset = 0;
                break;
        }
        base.Configure(sprite, actor);
    }
}
static class MainAccessories
{
    internal static SharedMask SharedMask = new SharedMask();
    internal static SantaHat SantaHat = new SantaHat();

}

