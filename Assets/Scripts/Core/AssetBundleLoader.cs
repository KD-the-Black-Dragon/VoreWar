// Modified for the VoreWar mercenary-company conversion.
// 2026-11-03: Remove unused retired sprite catalog/loader entries (spec 21).
// See CHANGES.md and License.txt for GPL-3.0 terms.
// Modified for the VoreWar mercenary-company conversion.
// 2026-11-01: Clear 156 retired-only sprite bindings without loading their bundle assets (spec 03B5).
// 2026-11-01: Clear 14 newly orphaned rejected graphics/effect bindings at cleanup closure.
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using System.IO;
using System.Linq;
using UnityEngine;

public class AssetBundleLoader
{
    static public void LoadAssetBundles()
    {
        //Load the bundle into memory
        AssetBundle empireBundle = AssetBundle.LoadFromFile(Path.Combine($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}AssetBundles", "empire"));
        AssetBundle monsterBundle = AssetBundle.LoadFromFile(Path.Combine($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}AssetBundles", "monster"));
        AssetBundle mercBundle = AssetBundle.LoadFromFile(Path.Combine($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}AssetBundles", "merc"));

        SpriteDictionary.SharedMaskParts = mercBundle.LoadAssetWithSubAssets<Sprite>("SharedMaskParts");

        // Load shared/retained sheets by asset name. Retired-only static fields are
        // cleared explicitly so a repeated load cannot preserve stale bindings.
        // Their declarations remain for disconnected legacy source; see spec 03B5.
        SpriteDictionary.FemaleVillager = empireBundle.LoadAssetWithSubAssets<Sprite>("female_villager");
        SpriteDictionary.MaleVillager = empireBundle.LoadAssetWithSubAssets<Sprite>("manpeasant");
        SpriteDictionary.MalePlate = empireBundle.LoadAssetWithSubAssets<Sprite>("manplatearmor");
        SpriteDictionary.FemalePlate = empireBundle.LoadAssetWithSubAssets<Sprite>("femaleplatearmor");
        SpriteDictionary.HumanRags = empireBundle.LoadAssetWithSubAssets<Sprite>("HumanRags");
        SpriteDictionary.LizardPeasant = empireBundle.LoadAssetWithSubAssets<Sprite>("LizardPeasant");
        SpriteDictionary.LizardBlackTop = empireBundle.LoadAssetWithSubAssets<Sprite>("LizardBlackTop");
        SpriteDictionary.LizardBikiniTop = empireBundle.LoadAssetWithSubAssets<Sprite>("LizardBikiniTop");
        SpriteDictionary.LizardCrossTop = empireBundle.LoadAssetWithSubAssets<Sprite>("LizardCrossTop");
        SpriteDictionary.LizardLeader = empireBundle.LoadAssetWithSubAssets<Sprite>("LizardLeader");
        SpriteDictionary.LizardBone = empireBundle.LoadAssetWithSubAssets<Sprite>("LizardBone");
        SpriteDictionary.LizardLeather = empireBundle.LoadAssetWithSubAssets<Sprite>("LizardLeather");
        SpriteDictionary.LizardCloth = empireBundle.LoadAssetWithSubAssets<Sprite>("LizardCloth");
        SpriteDictionary.LizardsBootyArmor = empireBundle.LoadAssetWithSubAssets<Sprite>("LizardsBootyArmor");
        SpriteDictionary.EquineClothes = empireBundle.LoadAssetWithSubAssets<Sprite>("horse_clothes");
        SpriteDictionary.CowClothing = empireBundle.LoadAssetWithSubAssets<Sprite>("cowclothes");
        SpriteDictionary.CowHoliday = empireBundle.LoadAssetWithSubAssets<Sprite>("cowhristmas");
        SpriteDictionary.RainCoats = empireBundle.LoadAssetWithSubAssets<Sprite>("raincoats");
        SpriteDictionary.TigerSpecial = mercBundle.LoadAssetWithSubAssets<Sprite>("tigeroutfit");
        SpriteDictionary.CatLeader = empireBundle.LoadAssetWithSubAssets<Sprite>("catLeader");
        SpriteDictionary.Togas = empireBundle.LoadAssetWithSubAssets<Sprite>("toga");
        SpriteDictionary.SuccubusDress = mercBundle.LoadAssetWithSubAssets<Sprite>("succubus_dress");
        SpriteDictionary.SuccubusLeotard = mercBundle.LoadAssetWithSubAssets<Sprite>("succubusLeotard");
        SpriteDictionary.SergalClothing = empireBundle.LoadAssetWithSubAssets<Sprite>("SergalClothes");
        SpriteDictionary.PantherBottoms = empireBundle.LoadAssetWithSubAssets<Sprite>("Panther bottoms");
        SpriteDictionary.PantherFOvertops = empireBundle.LoadAssetWithSubAssets<Sprite>("panther F overtops");
        SpriteDictionary.PantherFTops = empireBundle.LoadAssetWithSubAssets<Sprite>("Panther F tops");
        SpriteDictionary.PantherGloves = empireBundle.LoadAssetWithSubAssets<Sprite>("panther gloves");
        SpriteDictionary.PantherHats = empireBundle.LoadAssetWithSubAssets<Sprite>("panther hats");
        SpriteDictionary.PantherLegs = empireBundle.LoadAssetWithSubAssets<Sprite>("panther legs");
        SpriteDictionary.PantherMOvertops = empireBundle.LoadAssetWithSubAssets<Sprite>("panther M overtops");
        SpriteDictionary.PantherMTops = empireBundle.LoadAssetWithSubAssets<Sprite>("Panther M tops");
        SpriteDictionary.PantherOnePiece = empireBundle.LoadAssetWithSubAssets<Sprite>("panther one piece");
        SpriteDictionary.PantherOverBottoms = empireBundle.LoadAssetWithSubAssets<Sprite>("panther overbottoms");
        SpriteDictionary.HumenFundertops = empireBundle.LoadAssetWithSubAssets<Sprite>("hu_men_F_undertops");
        SpriteDictionary.HumenMundertops = empireBundle.LoadAssetWithSubAssets<Sprite>("hu_men_M_undertops");
        SpriteDictionary.HumenFOnePieces = empireBundle.LoadAssetWithSubAssets<Sprite>("hu_men_one_pieces");
        SpriteDictionary.HumenUnderbottoms = empireBundle.LoadAssetWithSubAssets<Sprite>("hu_men_underbottoms");
        SpriteDictionary.DeerLeaderClothes = empireBundle.LoadAssetWithSubAssets<Sprite>("DeerLeaderClothes");
        SpriteDictionary.Lizards = empireBundle.LoadAssetWithSubAssets<Sprite>("Lizards");
        SpriteDictionary.LizardsBooty = empireBundle.LoadAssetWithSubAssets<Sprite>("LizardsBooty");
        SpriteDictionary.Slimes = empireBundle.LoadAssetWithSubAssets<Sprite>("Slimes");
        SpriteDictionary.SlimeQueen = null;
        SpriteDictionary.Kangaroos = empireBundle.LoadAssetWithSubAssets<Sprite>("Kangaroos");
        SpriteDictionary.Equines = empireBundle.LoadAssetWithSubAssets<Sprite>("horses_base");
        SpriteDictionary.Sergal = empireBundle.LoadAssetWithSubAssets<Sprite>("Sergal");
        SpriteDictionary.Bees1 = empireBundle.LoadAssetWithSubAssets<Sprite>("Bees1");
        SpriteDictionary.Bees2 = empireBundle.LoadAssetWithSubAssets<Sprite>("Bees2");
        SpriteDictionary.Bees3 = empireBundle.LoadAssetWithSubAssets<Sprite>("Bees3");
        SpriteDictionary.Demibats1 = empireBundle.LoadAssetWithSubAssets<Sprite>("Demibats1");
        SpriteDictionary.Demibats2 = empireBundle.LoadAssetWithSubAssets<Sprite>("Demibats2");
        SpriteDictionary.Demibats3 = empireBundle.LoadAssetWithSubAssets<Sprite>("Demibats3");
        SpriteDictionary.Demibats4 = empireBundle.LoadAssetWithSubAssets<Sprite>("Demibats4");
        SpriteDictionary.Avians1 = empireBundle.LoadAssetWithSubAssets<Sprite>("Avians1");
        SpriteDictionary.Avians2 = empireBundle.LoadAssetWithSubAssets<Sprite>("Avians2");
        SpriteDictionary.Avians3 = empireBundle.LoadAssetWithSubAssets<Sprite>("Avians3");
        SpriteDictionary.Avians4 = empireBundle.LoadAssetWithSubAssets<Sprite>("Avians4");
        SpriteDictionary.Demiants1 = empireBundle.LoadAssetWithSubAssets<Sprite>("Demiants1");
        SpriteDictionary.Demiants2 = empireBundle.LoadAssetWithSubAssets<Sprite>("Demiants2");
        SpriteDictionary.Demiants3 = empireBundle.LoadAssetWithSubAssets<Sprite>("Demiants3");
        SpriteDictionary.AntQueen1 = empireBundle.LoadAssetWithSubAssets<Sprite>("AntQueen1");
        SpriteDictionary.AntQueen2 = empireBundle.LoadAssetWithSubAssets<Sprite>("AntQueen2");
        SpriteDictionary.Demifrogs1 = empireBundle.LoadAssetWithSubAssets<Sprite>("Demifrogs1");
        SpriteDictionary.Demifrogs2 = empireBundle.LoadAssetWithSubAssets<Sprite>("Demifrogs2");
        SpriteDictionary.Demifrogs3 = empireBundle.LoadAssetWithSubAssets<Sprite>("Demifrogs3");
        SpriteDictionary.Demifrogs3alt = empireBundle.LoadAssetWithSubAssets<Sprite>("Demifrogs3alt");
        SpriteDictionary.Demifrogs4 = empireBundle.LoadAssetWithSubAssets<Sprite>("Demifrogs4");
        SpriteDictionary.Demifrogs5 = empireBundle.LoadAssetWithSubAssets<Sprite>("Demifrogs5");
        SpriteDictionary.Sharks1 = empireBundle.LoadAssetWithSubAssets<Sprite>("Sharks1");
        SpriteDictionary.Sharks2 = empireBundle.LoadAssetWithSubAssets<Sprite>("Sharks2");
        SpriteDictionary.Sharks3 = empireBundle.LoadAssetWithSubAssets<Sprite>("Sharks3");
        SpriteDictionary.Sharks4 = empireBundle.LoadAssetWithSubAssets<Sprite>("Sharks4");
        SpriteDictionary.Sharks5 = empireBundle.LoadAssetWithSubAssets<Sprite>("Sharks5");
        SpriteDictionary.Sharks6 = empireBundle.LoadAssetWithSubAssets<Sprite>("Sharks6");
        SpriteDictionary.Sharks7 = empireBundle.LoadAssetWithSubAssets<Sprite>("Sharks7");
        SpriteDictionary.Deer1 = empireBundle.LoadAssetWithSubAssets<Sprite>("Deer1");
        SpriteDictionary.Deer2 = empireBundle.LoadAssetWithSubAssets<Sprite>("Deer2");
        SpriteDictionary.Deer3 = empireBundle.LoadAssetWithSubAssets<Sprite>("Deer3");
        SpriteDictionary.Deer4 = empireBundle.LoadAssetWithSubAssets<Sprite>("Deer4");
        SpriteDictionary.Alligators = mercBundle.LoadAssetWithSubAssets<Sprite>("AlligatorSprites");
        SpriteDictionary.Hippos = mercBundle.LoadAssetWithSubAssets<Sprite>("Hippos");
        SpriteDictionary.Hippos2 = mercBundle.LoadAssetWithSubAssets<Sprite>("Hippos2");
        SpriteDictionary.Hippos3 = mercBundle.LoadAssetWithSubAssets<Sprite>("Hippos3");
        SpriteDictionary.Komodos1 = monsterBundle.LoadAssetWithSubAssets<Sprite>("Komodos1");
        SpriteDictionary.Komodos2 = monsterBundle.LoadAssetWithSubAssets<Sprite>("Komodos2");
        SpriteDictionary.Komodos3 = monsterBundle.LoadAssetWithSubAssets<Sprite>("Komodos3");
        SpriteDictionary.Komodos4 = monsterBundle.LoadAssetWithSubAssets<Sprite>("Komodos4");
        SpriteDictionary.Cockatrice2 = mercBundle.LoadAssetWithSubAssets<Sprite>("Cockatrice2");
        SpriteDictionary.Cockatrice3 = mercBundle.LoadAssetWithSubAssets<Sprite>("Cockatrice3");
        SpriteDictionary.Vargul1 = mercBundle.LoadAssetWithSubAssets<Sprite>("Vargul1");
        SpriteDictionary.Vargul2 = mercBundle.LoadAssetWithSubAssets<Sprite>("Vargul2");
        SpriteDictionary.Vargul3 = mercBundle.LoadAssetWithSubAssets<Sprite>("Vargul3");
        SpriteDictionary.Vargul4 = mercBundle.LoadAssetWithSubAssets<Sprite>("Vargul4");
        SpriteDictionary.Vargul5 = mercBundle.LoadAssetWithSubAssets<Sprite>("Vargul5");
        SpriteDictionary.BadgersBodies = mercBundle.LoadAssetWithSubAssets<Sprite>("BadgersBodies");
        SpriteDictionary.BadgersClothes = mercBundle.LoadAssetWithSubAssets<Sprite>("BadgersClothes");
        SpriteDictionary.BadgersVoreBlack = mercBundle.LoadAssetWithSubAssets<Sprite>("BadgersVoreBlack");
        SpriteDictionary.BadgersVoreWhite = mercBundle.LoadAssetWithSubAssets<Sprite>("BadgersVoreWhite");
        SpriteDictionary.PantherBase = empireBundle.LoadAssetWithSubAssets<Sprite>("panther base");
        SpriteDictionary.PantherVoreParts = empireBundle.LoadAssetWithSubAssets<Sprite>("panther vore parts");
        SpriteDictionary.Horse = empireBundle.LoadAssetWithSubAssets<Sprite>("horses_base");
        SpriteDictionary.HorseClothing = empireBundle.LoadAssetWithSubAssets<Sprite>("horse_clothes");
        SpriteDictionary.HorseExtras1 = empireBundle.LoadAssetWithSubAssets<Sprite>("horseaddsheet1");
        SpriteDictionary.HumansBodySprites1 = empireBundle.LoadAssetWithSubAssets<Sprite>("HumansBodySprites1");
        SpriteDictionary.HumansBodySprites2 = empireBundle.LoadAssetWithSubAssets<Sprite>("HumansBodySprites2");
        SpriteDictionary.HumansBodySprites3 = empireBundle.LoadAssetWithSubAssets<Sprite>("HumansBodySprites3");
        SpriteDictionary.HumansBodySprites4 = empireBundle.LoadAssetWithSubAssets<Sprite>("HumansBodySprites4");
        SpriteDictionary.HumansBodySprites5 = empireBundle.LoadAssetWithSubAssets<Sprite>("HumansBodySprites5");
        SpriteDictionary.HumansVoreSprites = empireBundle.LoadAssetWithSubAssets<Sprite>("HumansVoreSprites");
        SpriteDictionary.AabayxLeader = empireBundle.LoadAssetWithSubAssets<Sprite>("AabayxHighPriest");
        SpriteDictionary.Mice1 = empireBundle.LoadAssetWithSubAssets<Sprite>("Mice1").Concat(empireBundle.LoadAssetWithSubAssets<Sprite>("MiceRags")).ToArray();
        SpriteDictionary.Mice2 = empireBundle.LoadAssetWithSubAssets<Sprite>("Mice2");
        SpriteDictionary.MiceFBottoms = empireBundle.LoadAssetWithSubAssets<Sprite>("MiceFBottoms");
        SpriteDictionary.MiceFTops = empireBundle.LoadAssetWithSubAssets<Sprite>("MiceFTops");
        SpriteDictionary.MiceMBottoms = empireBundle.LoadAssetWithSubAssets<Sprite>("MiceMBottoms");
        SpriteDictionary.MiceMTops = empireBundle.LoadAssetWithSubAssets<Sprite>("MiceMTops");
        SpriteDictionary.MiceVore1 = empireBundle.LoadAssetWithSubAssets<Sprite>("MiceVore1").Concat(empireBundle.LoadAssetWithSubAssets<Sprite>("MiceVore1Dark")).ToArray();
        SpriteDictionary.MiceVore2 = empireBundle.LoadAssetWithSubAssets<Sprite>("MiceVore2");
        SpriteDictionary.MiceVore3 = empireBundle.LoadAssetWithSubAssets<Sprite>("MiceVore3");
        SpriteDictionary.GnollClothes = empireBundle.LoadAssetWithSubAssets<Sprite>("GnollClothes");
        SpriteDictionary.GnollsBodyParts = empireBundle.LoadAssetWithSubAssets<Sprite>("GnollsBodyParts");
        SpriteDictionary.GnollsVoreParts = empireBundle.LoadAssetWithSubAssets<Sprite>("GnollsVoreParts");
        SpriteDictionary.TaurTorso = mercBundle.LoadAssetWithSubAssets<Sprite>("TaurTorso");
        SpriteDictionary.Lupine1 = empireBundle.LoadAssetWithSubAssets<Sprite>("Lupine1");
        SpriteDictionary.Lupine2 = empireBundle.LoadAssetWithSubAssets<Sprite>("Lupine2");
        SpriteDictionary.LupineClothes = empireBundle.LoadAssetWithSubAssets<Sprite>("LupineClothes");
        SpriteDictionary.LupineClothes2 = empireBundle.LoadAssetWithSubAssets<Sprite>("LupineClothes2");
        SpriteDictionary.LupineVoreSprites = empireBundle.LoadAssetWithSubAssets<Sprite>("LupineVoreParts");
        SpriteDictionary.JackalMain = empireBundle.LoadAssetWithSubAssets<Sprite>("JackalsMain");
        SpriteDictionary.JackalClothes = empireBundle.LoadAssetWithSubAssets<Sprite>("JackalsClothes");
        SpriteDictionary.JackalJewel = empireBundle.LoadAssetWithSubAssets<Sprite>("JackalsJewel");
        SpriteDictionary.DemidragonBodies = empireBundle.LoadAssetWithSubAssets<Sprite>("DemidragonBodies");
        SpriteDictionary.DemidragonBodies2 = empireBundle.LoadAssetWithSubAssets<Sprite>("DemidragonBodies2");
        SpriteDictionary.DemidragonBreasts = empireBundle.LoadAssetWithSubAssets<Sprite>("DemidragonBreasts");
        SpriteDictionary.DemidragonClothes = empireBundle.LoadAssetWithSubAssets<Sprite>("DemidragonClothes");
        SpriteDictionary.DemidragonClothes2 = empireBundle.LoadAssetWithSubAssets<Sprite>("DemidragonClothes2");
        SpriteDictionary.DemidragonClothes3 = empireBundle.LoadAssetWithSubAssets<Sprite>("DemidragonClothes3");
        SpriteDictionary.DemidragonCustomisation1 = empireBundle.LoadAssetWithSubAssets<Sprite>("DemidragonCustomisation1");
        SpriteDictionary.DemidragonCustomisation2 = empireBundle.LoadAssetWithSubAssets<Sprite>("DemidragonCustomisation2");
        SpriteDictionary.DemidragonCustomisation3 = empireBundle.LoadAssetWithSubAssets<Sprite>("DemidragonCustomisation3");
        SpriteDictionary.DemidragonPatterns = empireBundle.LoadAssetWithSubAssets<Sprite>("DemidragonPatterns");
        SpriteDictionary.DemidragonPatterns2 = empireBundle.LoadAssetWithSubAssets<Sprite>("DemidragonPatterns2");
        SpriteDictionary.DemidragonBelliesBalls = empireBundle.LoadAssetWithSubAssets<Sprite>("DemidragonBelliesBalls");
        SpriteDictionary.Serpents = monsterBundle.LoadAssetWithSubAssets<Sprite>("Serpents");
        SpriteDictionary.Wyvern = monsterBundle.LoadAssetWithSubAssets<Sprite>("Wyvern");
        SpriteDictionary.WyvernMatron = monsterBundle.LoadAssetWithSubAssets<Sprite>("WyvernMatron");
        SpriteDictionary.FeralWolf = monsterBundle.LoadAssetWithSubAssets<Sprite>("feralWolves");
        SpriteDictionary.Harvester = monsterBundle.LoadAssetWithSubAssets<Sprite>("Harvester");
        SpriteDictionary.Bat = monsterBundle.LoadAssetWithSubAssets<Sprite>("Bat");
        SpriteDictionary.Kobolds = monsterBundle.LoadAssetWithSubAssets<Sprite>("kobold").Concat(monsterBundle.LoadAssetWithSubAssets<Sprite>("kobold_Sel")).ToArray();
        SpriteDictionary.Dragon = monsterBundle.LoadAssetWithSubAssets<Sprite>("Dragon");
        SpriteDictionary.Dragonfly = monsterBundle.LoadAssetWithSubAssets<Sprite>("Dragonfly");
        SpriteDictionary.Plant = monsterBundle.LoadAssetWithSubAssets<Sprite>("PlantRace");
        SpriteDictionary.Ant = monsterBundle.LoadAssetWithSubAssets<Sprite>("Ant");
        SpriteDictionary.Gryphon = monsterBundle.LoadAssetWithSubAssets<Sprite>("Gryphon");
        SpriteDictionary.Griffin = monsterBundle.LoadAssetWithSubAssets<Sprite>("Griffin");
        SpriteDictionary.SpitterSlug = monsterBundle.LoadAssetWithSubAssets<Sprite>("SpitterSlug");
        SpriteDictionary.SpringSlug = monsterBundle.LoadAssetWithSubAssets<Sprite>("SpringSlug");
        SpriteDictionary.RockSlug = monsterBundle.LoadAssetWithSubAssets<Sprite>("RockSlug");
        SpriteDictionary.CoralSlug = monsterBundle.LoadAssetWithSubAssets<Sprite>("CoralSlug");
        SpriteDictionary.Salamanders = monsterBundle.LoadAssetWithSubAssets<Sprite>("Salamanders");
        SpriteDictionary.Mantis = monsterBundle.LoadAssetWithSubAssets<Sprite>("Mantis");
        SpriteDictionary.EasternDragon = monsterBundle.LoadAssetWithSubAssets<Sprite>("LungDragon");
        SpriteDictionary.Catfish = monsterBundle.LoadAssetWithSubAssets<Sprite>("Catfish");
        SpriteDictionary.WarriorAnt = monsterBundle.LoadAssetWithSubAssets<Sprite>("WarriorAnt");
        SpriteDictionary.Gazelle1 = monsterBundle.LoadAssetWithSubAssets<Sprite>("Gazelle1");
        SpriteDictionary.Gazelle2 = monsterBundle.LoadAssetWithSubAssets<Sprite>("Gazelle2");
        SpriteDictionary.FeralLizards = monsterBundle.LoadAssetWithSubAssets<Sprite>("FeralLizards");
        SpriteDictionary.Monitors = monsterBundle.LoadAssetWithSubAssets<Sprite>("Monitors");
        SpriteDictionary.Terrorbird = monsterBundle.LoadAssetWithSubAssets<Sprite>("Terrorbird");
        SpriteDictionary.Dratopyr = monsterBundle.LoadAssetWithSubAssets<Sprite>("Dratopyr");
        SpriteDictionary.FeralLions = monsterBundle.LoadAssetWithSubAssets<Sprite>("FeralLions");
        SpriteDictionary.Goodra = monsterBundle.LoadAssetWithSubAssets<Sprite>("Goodra");
        SpriteDictionary.FeralHorses = monsterBundle.LoadAssetWithSubAssets<Sprite>("FeralHorses");
        SpriteDictionary.FeralFox = monsterBundle.LoadAssetWithSubAssets<Sprite>("FeralFox");
        SpriteDictionary.Terminid = monsterBundle.LoadAssetWithSubAssets<Sprite>("Terminid");
        SpriteDictionary.Otachi = monsterBundle.LoadAssetWithSubAssets<Sprite>("Otachi");
        SpriteDictionary.FeralSlime = monsterBundle.LoadAssetWithSubAssets<Sprite>("FeralSlime");
        SpriteDictionary.FeralUmbreon = monsterBundle.LoadAssetWithSubAssets<Sprite>("FeralUmbreon");
        SpriteDictionary.Smudger = monsterBundle.LoadAssetWithSubAssets<Sprite>("Smudger");
        SpriteDictionary.SpaceCroach = monsterBundle.LoadAssetWithSubAssets<Sprite>("SpaceCroach");
        SpriteDictionary.Raiju = monsterBundle.LoadAssetWithSubAssets<Sprite>("Raiju");
        SpriteDictionary.Raiju240 = monsterBundle.LoadAssetWithSubAssets<Sprite>("Raiju240");
        SpriteDictionary.Utahraptor200 = monsterBundle.LoadAssetWithSubAssets<Sprite>("Utahraptor200");
        SpriteDictionary.Utahraptor240A = monsterBundle.LoadAssetWithSubAssets<Sprite>("Utahraptor240A");
        SpriteDictionary.Utahraptor240B = monsterBundle.LoadAssetWithSubAssets<Sprite>("Utahraptor240B");
        SpriteDictionary.Trex = monsterBundle.LoadAssetWithSubAssets<Sprite>("Trex");
        SpriteDictionary.BlackWidow = monsterBundle.LoadAssetWithSubAssets<Sprite>("BlackWidow");
        SpriteDictionary.AngelProjectile = null;

    }
}
