// Modified for the VoreWar mercenary-company conversion.
// 2026-10-30: Return a missing-target context rather than constructing a Human narrative dummy (spec 04B1).
// 2026-10-30: Replace flavor descriptors with neutral unit/species labels (spec 04B2).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

static class LogUtilities
{
    static int rand;
    internal static string GetRandomStringFrom(List<string> messages)
    {
        if (messages.Count == 0)
            return "";
        rand = Random.Range(0, messages.Count);
        return messages[rand];
    }

    internal static string GetRandomStringFrom(params string[] messages)
    {
        rand = Random.Range(0, messages.Length);
        return messages[rand];
    }

    internal static string GetGenderString(Unit unit, string female, string male, string mixed)
    {
        if (unit.HasBreasts && (unit.HasDick != unit.HasVagina))
            return female;
        else if (!unit.HasBreasts && (unit.HasDick != unit.HasVagina))
            return male;
        return mixed;
    }

    internal static string Capitalize(String str)
    {
        if (str == null)
            return null;
        return char.ToUpper(str[0]) + str.Substring(1);

    }

    /// <summary>
    /// Returns given unit's nominative pronoun.<br></br>(e.g. he/she/they)
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GPPHe(Unit unit) => unit.GetPronoun(0);

    /// <summary>
    /// Returns given unit's nominative pronoun appended with present-tense auxillary.<br></br>(e.g. he is/she is/they are)
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GPPHeIs(Unit unit) => unit.GetPronoun(0) + (unit.GetPronoun(5) == "plural" ? " are" : " is");

    /// <summary>
    /// Returns given unit's nominative pronoun appended with present-tense auxillary as a contraction.<br></br>(e.g. he's/she's/they're)
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GPPHeIsAbbr(Unit unit) => unit.GetPronoun(0) + (unit.GetPronoun(5) == "plural" ? "'re" : "'s");

    /// <summary>
    /// Returns given unit's nominative pronoun appended with past-tense auxillary.<br></br>(e.g. he was/she was/they were)
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GPPHeWas(Unit unit) => unit.GetPronoun(0) + (unit.GetPronoun(5) == "plural" ? " were" : " was");

    /// <summary>
    /// Returns given unit's accusative pronoun.<br></br>(e.g. him/her/them)
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GPPHim(Unit unit) => unit.GetPronoun(1);

    /// <summary>
    /// Returns given unit's pronomial possessive pronoun.<br></br>(e.g. ...<u>their</u> belly...)
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GPPHis(Unit unit) => unit.GetPronoun(2);

    /// <summary>
    /// Returns given unit's reflexive pronoun.<br></br>(e.g. ...can't help <u>themself</u>...)
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GPPHimself(Unit unit) => unit.GetPronoun(4);

    /// <summary>
    /// Returns "s" if given unit is referred to with singular grammar.
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string SIfSingular(Unit unit) => (unit.GetPronoun(5) == "plural" ? "" : "s");

    /// <summary>
    /// Returns "es" if given unit is referred to with singular grammar.
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string EsIfSingular(Unit unit) => (unit.GetPronoun(5) == "plural" ? "" : "es");

    /// <summary>
    /// Returns "y" or "ies" based on plurality of given unit.
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string IesIfSingular(Unit unit) => (unit.GetPronoun(5) == "plural" ? "y" : "ies");

    /// <summary>
    /// Returns "has" or "have" based on plurality of given unit.
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string HasHave(Unit unit) => (unit.GetPronoun(5) == "plural" ? "have" : "has");

    /// <summary>
    /// Returns "is" or "are" based on plurality of given unit.
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string IsAre(Unit unit) => (unit.GetPronoun(5) == "plural" ? "are" : "is");

    /// <summary>
    /// Returns "was" or "were" based on plurality of given unit.
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string WasWere(Unit unit) => (unit.GetPronoun(5) == "plural" ? "were" : "was");

    internal static string PluralForPart(PreyLocation location)
    {
        switch (location)
        {
            case PreyLocation.breasts:
            case PreyLocation.balls:
                return "";
            case PreyLocation.stomach:
            case PreyLocation.stomach2:
            case PreyLocation.womb:
            case PreyLocation.tail:
            case PreyLocation.anal:
            case PreyLocation.bladder:
                return "s";
        }
        return "";
    }

    internal static string BoyGirl(Unit unit)
    {
        if (unit.DefaultBreastSize >= 0 && unit.DickSize < 0)
            return "girl";
        if (unit.DefaultBreastSize < 0 && unit.DickSize >= 0)
            return "boy";
        return unit.Race.ToString();
    }

    //internal static string FriendlyWarrior(Unit unit)
    //{
    //    var friendlies = TacticalUtilities.Units.Where(s => s.Unit.Side == unit.Side && s.Unit != unit && s.Visible && s.Targetable && s.Unit.IsDead == false).ToArray();
    //    if (friendlies.Length == 0)
    //        return "NULL";
    //    return friendlies[State.Rand.Next(friendlies.Length)].Unit.Name;  
    //}
    internal static Unit RandomAlliedWarrior(Unit unit)//Implementing modified code, but kept original because I'm not sure if someone else was planning on using it.
    {
        var friendlies = TacticalUtilities.Units.Where(s => s.Unit.Side == unit.Side && s.Unit != unit && s.Visible && s.Targetable && s.Unit.IsDead == false).ToArray();
        if (friendlies.Length == 0)
            return null;
        return friendlies[State.Rand.Next(friendlies.Length)].Unit;
    }

    internal static Unit CompetitionWarrior(Unit unit)
    {
        var friendlies = TacticalUtilities.Units.Where(s => s.Unit.Side == unit.Side && s.Unit != unit && s.Visible && s.Targetable && s.Unit.IsDead == false && RomanticTarget(unit, s.Unit) == false).ToArray();
        if (friendlies.Length == 0)
            return null;
        return friendlies[State.Rand.Next(friendlies.Length)].Unit;
    }

    internal static Unit PotentialNextPrey(Unit unit)
    {
        var preyList = TacticalUtilities.Units.Where(s => s.Unit.Side != unit.Side && s.Visible && s.Targetable && !s.Unit.IsDead);
        var preyChanceMap = new Dictionary<Actor_Unit, float>();
        foreach (Actor_Unit prey in preyList)
        {
            float chance = prey.GetDevourChance(TacticalUtilities.Units.Where(actor => actor.Unit == unit)?.FirstOrDefault(), true);
            preyChanceMap.Add(prey, chance);
        }
        var primePrey = preyChanceMap.OrderBy(x => x.Value).LastOrDefault();
        if (!primePrey.Equals(default(KeyValuePair<Actor_Unit, float>)))
        {
            return primePrey.Key.Unit;
        }
        return null; // No suitable existing target; never create a species as text context.
    }

    internal static Unit AttractedWarrior(Unit unit)
    {
        if (unit.AttractedTo != null)
        {
            var actor = TacticalUtilities.Units.Where(s => s.Unit == unit.AttractedTo && s.Unit.Side == unit.Side && s.Unit != unit && RomanticTarget(unit, s.Unit)).FirstOrDefault(); //If this fails, reassign
            if (actor != null)
            {
                if (actor.Visible && actor.Targetable && actor.Unit.IsDead == false)
                {
                    return actor.Unit;
                }
                return null; //Avoid picking a new target during the same battle
            }
        }

        var friendlies = TacticalUtilities.Units.Where(s => s.Unit.Side == unit.Side && s.Unit != unit && s.Visible && s.Targetable && s.Unit.IsDead == false && RomanticTarget(unit, s.Unit)).ToArray();
        if (friendlies.Length == 0)
            return null;
        return friendlies[State.Rand.Next(friendlies.Length)].Unit;
    }

    internal static bool ActorHumanoid(Unit s)
    {
        return s.Race < Race.Vagrants || s.Race >= Race.Selicia;
    }

    internal static bool RomanticTarget(Unit unit, Unit target)
    {
        if (unit.GetGender() == Gender.Hermaphrodite || target.GetGender() == Gender.Hermaphrodite)
            return true;
        if (unit.GetGender() == Gender.Female)
        {
            switch (Config.FemalesLike)
            {
                case Orientation.Straight:
                    return target.GetGender() == Gender.Male;
                case Orientation.Gay:
                    return target.GetGender() == Gender.Female;
                case Orientation.Bi:
                    return true;
            }
        }
        if (unit.GetGender() == Gender.Male)
        {
            switch (Config.MalesLike)
            {
                case Orientation.Straight:
                    return target.GetGender() == Gender.Female;
                case Orientation.Gay:
                    return target.GetGender() == Gender.Male;
                case Orientation.Bi:
                    return true;
            }
        }
        //Should never make it here
        return false;
    }



    /// <summary>
    /// Determines whether the string supplied should have either a or an before it and returns the original string with the right "thing" in front of it.
    /// Done this way since otherwise the string might get randomized again and wouldn't match the returned bit.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    internal static string GetAorAN(string str)
    {
        if (str.StartsWith("a", true, null) || str.StartsWith("e", true, null) || str.StartsWith("i", true, null) || str.StartsWith("o", true, null) || str.StartsWith("u", true, null) || str.StartsWith("y", true, null))
        {
            return "an " + str;
        }
        if (str.StartsWith("b", true, null) || str.StartsWith("c", true, null) || str.StartsWith("d", true, null) || str.StartsWith("f", true, null) || str.StartsWith("g", true, null) || str.StartsWith("h", true, null) ||
            str.StartsWith("j", true, null) || str.StartsWith("k", true, null) || str.StartsWith("l", true, null) || str.StartsWith("m", true, null) || str.StartsWith("n", true, null) || str.StartsWith("p", true, null) ||
            str.StartsWith("q", true, null) || str.StartsWith("r", true, null) || str.StartsWith("s", true, null) || str.StartsWith("t", true, null) || str.StartsWith("v", true, null) || str.StartsWith("w", true, null) || str.StartsWith("x", true, null) || str.StartsWith("z", true, null))
        {
            return "a " + str;
        }
        if (str.StartsWith("'", true, null))
        {
            if (str.StartsWith("'a", true, null) || str.StartsWith("'e", true, null) || str.StartsWith("'i", true, null) || str.StartsWith("'o", true, null) || str.StartsWith("'u", true, null) || str.StartsWith("'y", true, null))
            {
                return "an " + str;
            }
            if (str.StartsWith("'b", true, null) || str.StartsWith("'c", true, null) || str.StartsWith("'d", true, null) || str.StartsWith("'f", true, null) || str.StartsWith("'g", true, null) || str.StartsWith("'h", true, null) ||
                str.StartsWith("'j", true, null) || str.StartsWith("'k", true, null) || str.StartsWith("'l", true, null) || str.StartsWith("'m", true, null) || str.StartsWith("'n", true, null) || str.StartsWith("'p", true, null) ||
                str.StartsWith("'q", true, null) || str.StartsWith("'r", true, null) || str.StartsWith("'s", true, null) || str.StartsWith("'t", true, null) || str.StartsWith("'v", true, null) || str.StartsWith("'w", true, null) || str.StartsWith("'x", true, null) || str.StartsWith("'z", true, null))
            {
                return "a " + str;
            }
        }
        return str;
    }

    internal static string ApostrophizeWithOrWithoutS(string str)
    {
        return str + (str.EndsWith("s") ? "'" : "'s");
    }

    /// <summary>
    /// <para>Gets a descriptive string that fits sentences like "Edmond stuffs Sidney down his maw, enjoying the * morsels squirms on her way down."</para>
    /// <para>Generally meant for the prey/loser/weaker unit. Has mostly demeaning, belittling, weakness indicating or fear portraying terms.</para>
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GetPreyDesc(Unit unit)
    {
        return "unit";
    }

    /// <summary>
    ///<para>Gets a descriptive string that fits sentences like "Edmond stuffs Sidney down his maw, the prey filling his * body nicely."</para>
    ///<para>Generally meant for the predator/winner/stronger unit. Strength describing, contentment/pleasure indicating, etc. terms.</para>
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GetPredDesc(Unit unit)
    {
        return "unit";
    }

    /// <summary>
    /// <para>Gets a descriptive string that fits situations like "Jeanne graps Timothy's head, pushing the *'s face in her slit and soon forcing rest of him after it."</para>
    /// <para>This is either the species name, a name of the genus the species belongs to or something similar. Can also be a synonym of the species name.</para>
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GetRaceDescSingl(Unit unit)
    {
        return unit == null ? "unit" : unit.Race.ToString();
    }

    /// <summary>
    /// Gets a name that fits the weapon the unit's graphics show it using.
    /// </summary>
    /// <param name="weapon"></param>
    /// <param name="unit"></param>
    /// <returns></returns>
    internal static string GetWeaponTrueName(Weapon weapon, Unit unit)
    {
        if (unit.Race == Race.Vagrants) return "Stinger";
        else if (unit.Race == Race.Wyvern) return "Claws";
        else if (unit.Race == Race.WyvernMatron) return "Fierce Claws";
        else if (unit.Race == Race.Serpents) return "Tail Blade";
        else if (unit.Race == Race.FeralSharks) return "Jaws";
        else if (unit.Race == Race.FeralWolves) return "Fangs";
        else if (unit.Race == Race.DarkSwallower) return "Jaws";
        else if (unit.Race == Race.Harvesters) return "Scythes";
        else if (unit.Race == Race.Collectors) return "Maw";
        else if (unit.Race == Race.Ki) return "Jaws";
        else if (unit.Race == Race.Selicia) return "Claws";
        else if (unit.Race == Race.Xelhilde) return "Zweihänder";
        else if (unit.Race == Race.Olivia) return "Static Fist";
        else if (unit.Race == Race.Nectar) return "Claws";
        else if (unit.Race == Race.Renamon) return "Palm Strike";

        else if (unit.Race == Race.Kangaroos)
        {
            if (weapon.Name == "Mace") return "Club";
            else if (weapon.Name == "Axe") return "Spear";
            else if (weapon.Name == "Simple Bow") return "Boomerang";
            else if (weapon.Name == "Compound Bow") return "Woomera";
            else if (weapon.Name == "Claw") return "Claws";
        }
        else if (unit.Race == Race.Goblins)
        {
            if (weapon.Name == "Mace") return "Cleaver";
            else if (weapon.Name == "Axe") return "Sharpened Cleaver";
            else if (weapon.Name == "Simple Bow") return "Derringer";
            else if (weapon.Name == "Compound Bow") return "Pepperbox Pistol";
            else if (weapon.Name == "Claw") return "Fist";
        }
        else if (unit.Race == Race.Slimes)
        {
            if (weapon.Name == "Mace") return "Bone Blade";
            else if (weapon.Name == "Axe") return "Whip";
            else if (weapon.Name == "Simple Bow") return "Floating Slimey";
            else if (weapon.Name == "Compound Bow") return "Bioelectricity";
            else if (weapon.Name == "Claw") return "Hardened Lump";
        }
        else if (unit.Race == Race.Imps)
        {
            if (weapon.Name == "Mace") return "Morningstar";
            else if (weapon.Name == "Axe") return "Cleaver";
            else if (weapon.Name == "Simple Bow") return "Bow";
            else if (weapon.Name == "Compound Bow") return "Infernal Bow";
            else if (weapon.Name == "Claw") return "Fist";
        }
        else if (unit.Race == Race.Crypters)
        {
            if (weapon.Name == "Mace") return "Sword";
            else if (weapon.Name == "Axe") return "Power Fist";
            else if (weapon.Name == "Simple Bow") return "Crossbow";
            else if (weapon.Name == "Compound Bow") return "Cannon";
            else if (weapon.Name == "Claw") return "Metal Fist";
        }
        else if (unit.Race == Race.Scylla)
        {
            if (weapon.Name == "Mace") return "Knife";
            else if (weapon.Name == "Axe") return "Trident";
            else if (weapon.Name == "Simple Bow") return "Javelin";
            else if (weapon.Name == "Compound Bow") return "Medusa Launcher";
            else if (weapon.Name == "Claw") return "Tentacle";
        }
        else if (unit.Race == Race.Harpies)
        {
            if (weapon.Name == "Mace") return "Bronze Claws";
            else if (weapon.Name == "Axe") return "Steel Claws";
            else if (weapon.Name == "Simple Bow") return "Simple Bow";
            else if (weapon.Name == "Compound Bow") return "Compound Bow";
            else if (weapon.Name == "Claw") return "Talons";
        }
        else if (unit.Race == Race.Taurus)
        {
            if (weapon.Name == "Mace") return "Hammer";
            else if (weapon.Name == "Axe") return "Glaive";
            else if (weapon.Name == "Simple Bow") return "Revolver";
            else if (weapon.Name == "Compound Bow") return "Shotgun";
            else if (weapon.Name == "Claw") return "Fist";
        }
        else if (unit.Race == Race.Crux)
        {
            if (weapon.Name == "Mace" && unit.BasicMeleeWeaponType == 0) return "Bat";
            else if (weapon.Name == "Mace" && unit.BasicMeleeWeaponType == 1) return "Pipe";
            else if (weapon.Name == "Mace" && unit.BasicMeleeWeaponType == 2) return "Dildo";
            else if (weapon.Name == "Axe" && unit.AdvancedMeleeWeaponType == 0) return "Machete";
            else if (weapon.Name == "Axe" && unit.AdvancedMeleeWeaponType == 1) return "Axe";
            else if (weapon.Name == "Simple Bow") return "Handbow";
            else if (weapon.Name == "Compound Bow") return "Compound Bow";
            else if (weapon.Name == "Claw") return "Claws";
        }
        else if (unit.Race == Race.Alligators)
        {
            if (weapon.Name == "Mace") return "Turtle Club";
            else if (weapon.Name == "Axe") return "Flint Spear";
            else if (weapon.Name == "Claw") return "Claws";
        }
        else if (unit.Race == Race.Badgers)
        {
            if (weapon.Name == "Mace") return "Hand Axe";
            else if (weapon.Name == "Axe") return "Glaive";
            else if (weapon.Name == "Claw") return "Claws";
        }
        else if (unit.Race == Race.Komodos)
        {
            if (weapon.Name == "Mace") return "Hunting Knife";
            else if (weapon.Name == "Axe") return "War Scythe";
            else if (weapon.Name == "Claw") return "Claws";
        }
        else if (unit.Race == Race.Puca)
        {
            if (weapon.Name == "Mace") return "Shovel";
            else if (weapon.Name == "Axe") return "Shovel";
            else if (weapon.Name == "Simple Bow") return "Slingshot";
            else if (weapon.Name == "Compound Bow") return "Heavy Slingshot";
        }
        else if (unit.Race == Race.Hamsters)
        {
            if (weapon.Name == "Mace") return "Warhammer";
            else if (weapon.Name == "Axe") return "Heavy Warhammer";
            else if (weapon.Name == "Simple Bow") return "Throwing Axe";
            else if (weapon.Name == "Compound Bow") return "Throwing Axe";
            else if (weapon.Name == "Claw") return "Claws";
        }
        else if (unit.Race == Race.RwuMercenaries)
        {
            if (weapon.Name == "Mace") return "Combat Knife";
            else if (weapon.Name == "Axe" && unit.EyeType == 0) return "Machete";
            else if (weapon.Name == "Axe" && unit.EyeType == 1) return "Ikakalaka";
            else if (weapon.Name == "Axe" && unit.EyeType == 2) return "Sabre";
            else if (weapon.Name == "Axe" && unit.EyeType == 3) return "Cutlass";
            else if (weapon.Name == "Simple Bow") return "Sidearm";
            else if (weapon.Name == "Compound Bow" && unit.EyeType == 0) return "SMG";
            else if (weapon.Name == "Compound Bow" && unit.EyeType == 1) return "LMG";
            else if (weapon.Name == "Compound Bow" && unit.EyeType == 2) return "Sniper Rifle";
            else if (weapon.Name == "Compound Bow" && unit.EyeType == 3) return "Assault Rifle";
            else if (weapon.Name == "Claw") return "Fist";
        }
        else if (unit.Race == Race.Vipers)
        { /*V33B ADDITION*/
            if (weapon.Name == "Mace") return "Arc Blade";
            else if (weapon.Name == "Axe") return "Fusion Blade";
            else if (weapon.Name == "Simple Bow") return "Plasma Pistol";
            else if (weapon.Name == "Compound Bow") return "Plasma Rifle";
        }
        else if (unit.Race == Race.Sergal)
        {
            if (weapon.Name == "Mace") return "Lance";
            else if (weapon.Name == "Axe") return "Twin Glaive";
            else if (weapon.Name == "Simple Bow") return "Speargun";
            else if (weapon.Name == "Compound Bow") return "Prototype Railgun"; ////changed to "prototype railgun", thanks to Flame_Valxsarion
        }
        else if (unit.Race == Race.Bees)
        {
            if (weapon.Name == "Mace") return "Honeycomb Mace";
            else if (weapon.Name == "Axe") return "Quad Punch Claws";
            else if (weapon.Name == "Simple Bow") return "Javelin";
            else if (weapon.Name == "Compound Bow") return "War Javelin";
        }
        else if (unit.Race == Race.Driders)
        {
            if (weapon.Name == "Mace") return "Dagger";
            else if (weapon.Name == "Axe") return "Short Sword";
            else if (weapon.Name == "Simple Bow") return "Pistol Crossbow";
            else if (weapon.Name == "Compound Bow") return "Crossbow";
        }
        else if (unit.Race == Race.Alraune)
        {
            if (weapon.Name == "Mace") return "Vine Whip";
            else if (weapon.Name == "Axe") return "Stem Blade";
            else if (weapon.Name == "Simple Bow") return "Unbloomed Corolla";
            else if (weapon.Name == "Compound Bow") return "Blooming Flower";
        }
        else if (unit.Race == Race.Bats)
        {
            if (weapon.Name == "Mace") return "Push Dagger";
            else if (weapon.Name == "Axe") return "Claw Katar";
            else if (weapon.Name == "Simple Bow") return "Iron Throwing Knife";
            else if (weapon.Name == "Compound Bow") return "Steel Throwing Knife";
        }
        else if (unit.Race == Race.Panthers)
        {
            if (weapon.Name == "Mace") return "Karambit";
            else if (weapon.Name == "Axe") return "Kukri";
            else if (weapon.Name == "Simple Bow") return "Chakram";
            else if (weapon.Name == "Compound Bow") return "Onzil";
        }
        else if (unit.Race == Race.Merfolk)
        {
            if (weapon.Name == "Mace") return "Crude Trident";
            else if (weapon.Name == "Axe") return "Royal Trident";
            else if (weapon.Name == "Simple Bow") return "Scepter";
            else if (weapon.Name == "Compound Bow") return "Orb Staff";
        }
        else if (unit.Race == Race.Ants)
        {
            if (weapon.Name == "Mace") return "Barbed Spear";
            else if (weapon.Name == "Axe") return "Quad Blades";
            else if (weapon.Name == "Simple Bow") return "Simple Bow";
            else if (weapon.Name == "Compound Bow") return "Compound Bow";
        }
        else if (unit.Race == Race.Avians)
        {
            if (weapon.Name == "Mace") return "Knife";
            else if (weapon.Name == "Axe") return "Sword";
            else if (weapon.Name == "Simple Bow") return "Short Bow";
            else if (weapon.Name == "Compound Bow") return "Crossbow";
        }
        else if (unit.Race == Race.Sharks)
        {
            if (weapon.Name == "Mace") return "Sabre";
            else if (weapon.Name == "Axe") return "Cutlass";
            else if (weapon.Name == "Simple Bow") return "Harpoon";
            else if (weapon.Name == "Compound Bow") return "Speargun";
        }
        else if (unit.Race == Race.Frogs)
        {
            if (weapon.Name == "Mace") return "Mace";
            else if (weapon.Name == "Axe") return "Axe";
            else if (weapon.Name == "Simple Bow") return "Slingshot";
            else if (weapon.Name == "Compound Bow") return "Feathered Bow";
            else if (weapon.Name == "Claw") return "Fist";
        }
        else if (unit.Race == Race.Hippos)
        {
            if (weapon.Name == "Mace") return "Tribal Knife";
            else if (weapon.Name == "Axe") return "Axe";
            else if (weapon.Name == "Simple Bow") return "Simple Bow";
            else if (weapon.Name == "Compound Bow") return "Compound Bow";
            else if (weapon.Name == "Claw") return "Fist";
        }
        else if (unit.Race == Race.Kobolds)
        {
            if (weapon.Name == "Mace") return "Pickax";
            else if (weapon.Name == "Axe") return "Pickax";
            else if (weapon.Name == "Simple Bow") return "Dart";
            else if (weapon.Name == "Compound Bow") return "Dart";
            else if (weapon.Name == "Claw") return "Fist";
        }
        else if (unit.Race == Race.Aabayx)
        {
            if (weapon.Name == "Mace") return "Longscalpel";
            else if (weapon.Name == "Axe") return "Personsmasher";
            else if (weapon.Name == "Simple Bow") return "Razorflinger";
            else if (weapon.Name == "Compound Bow") return "Arrowbisector";
            else if (weapon.Name == "Claw") return "Fist";
        }
        else if (unit.Race == Race.Mice)
        {
            if (weapon.Name == "Mace") return "Shortsword";
            else if (weapon.Name == "Axe") return "Bardiche";
            else if (weapon.Name == "Simple Bow") return "Hand Crossbow";
            else if (weapon.Name == "Compound Bow") return "Greatbow";
        }
        else if (unit.Race == Race.Bears)
        {
            if (weapon.Name == "Mace") return "Hand Axe";
            else if (weapon.Name == "Axe") return "Battle Axe";
            else if (weapon.Name == "Simple Bow") return "Spear";
            else if (weapon.Name == "Compound Bow") return "Throwing Axe";
            else if (weapon.Name == "Claw") return "Claws";
        }
        else if (unit.Race == Race.Umbreon)
        {
            if (weapon.Name == "Mace") return "Claw Gauntlet";
            else if (weapon.Name == "Axe") return "Scythe";
            else if (weapon.Name == "Simple Bow") return "Slingbow";
            else if (weapon.Name == "Compound Bow") return "Mechanical Slingbow";
            else if (weapon.Name == "Claw") return "Claws";
        }
        else if (unit.Race == Race.Cats || unit.Race == Race.Dogs || unit.Race == Race.Foxes || unit.Race == Race.Wolves || unit.Race == Race.Bunnies || unit.Race == Race.Tigers)
        {
            if (weapon.Name == "Mace" && unit.BasicMeleeWeaponType == 1) return "Sword";
        }
        else if (weapon.Name == "Claw") return "Claws";
        return weapon.Name;
    }
}

