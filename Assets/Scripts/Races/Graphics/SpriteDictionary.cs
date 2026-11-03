// Modified for the VoreWar mercenary-company conversion.
// 2026-11-03: Remove unused retired sprite catalog/loader entries (spec 21).
// See CHANGES.md and License.txt for GPL-3.0 terms.
using UnityEngine;

public class SpriteDictionary : MonoBehaviour
{
    public static Sprite[] SharedMaskParts;
    public Sprite[] Bodies;
    public Sprite[] Hair;
    public Sprite[] Bellies;
    public Sprite[] SmoothBellies;
    public Sprite[] Weapons;

    public Sprite[] BodyAccents;
    
    public Sprite[] Eyes;
    public Sprite[] Legs;
    public Sprite[] Dicks;
    public Sprite[] ErectDicks;
    public Sprite[] Balls;
    public Sprite[] FurryDicks;
    public Sprite[] Breasts;
    public Sprite[] SquishedBreasts;
    public Sprite[] FeaturelessBreasts;
    public Sprite[] Mouths;

    public Sprite[] Eyebrows;

    public Sprite[] FurryHandsAndFeet;
    public Sprite[] FurryTorsos;

    [Header("Clothing")]

    public Sprite[] Loincloths;
    public Sprite[] BikiniBottom;
    public Sprite[] Shorts;
    public Sprite[] Belts;
    public Sprite[] Straps;
    public Sprite[] BikiniTop;
    public Sprite[] Leotards;
    public Sprite[] Rags;
    public Sprite[] BlackTop;
    public static Sprite[] FemaleVillager;
    public static Sprite[] MaleVillager;
    public static Sprite[] MalePlate;
    public static Sprite[] HumanRags;
    public static Sprite[] FemalePlate;


    public static Sprite[] LizardPeasant;
    public static Sprite[] LizardBlackTop;
    public static Sprite[] LizardBikiniTop;
    public static Sprite[] LizardCrossTop;
    public static Sprite[] LizardLeader;
    
    public static Sprite[] LizardBone;
    public static Sprite[] LizardLeather;
    public static Sprite[] LizardCloth;
    public static Sprite[] LizardsBootyArmor;

    public static Sprite[] EquineClothes;

    public static Sprite[] CowClothing;
    public static Sprite[] CowHoliday;

    public static Sprite[] RainCoats;

    public static Sprite[] TigerSpecial;
    public static Sprite[] CatLeader;

    public static Sprite[] Togas;
    public static Sprite[] SuccubusDress;
    public static Sprite[] SuccubusLeotard;

    public static Sprite[] SergalClothing;

    public static Sprite[] PantherBottoms;
    public static Sprite[] PantherFOvertops;
    public static Sprite[] PantherFTops;
    public static Sprite[] PantherGloves;
    public static Sprite[] PantherHats;
    public static Sprite[] PantherLegs;
    public static Sprite[] PantherMOvertops;
    public static Sprite[] PantherMTops;
    public static Sprite[] PantherOnePiece;
    public static Sprite[] PantherOverBottoms;
    
    public static Sprite[] HumenFundertops;
    public static Sprite[] HumenMundertops;
    public static Sprite[] HumenFOnePieces;
    public static Sprite[] HumenUnderbottoms;

    public static Sprite SantaHat;

    public static Sprite HatBat;

    public static Sprite[] DeerLeaderClothes;

    [Header("Races")]
    public static Sprite[] Lizards;
    public static Sprite[] LizardsBooty;
    public static Sprite[] Slimes;
    public static Sprite[] SlimeQueen;
    public static Sprite[] Kangaroos;
    public static Sprite[] Equines;
    public static Sprite[] Sergal;
    public static Sprite[] Bees1;
    public static Sprite[] Bees2;
    public static Sprite[] Bees3;
    public static Sprite[] Demibats1;
    public static Sprite[] Demibats2;
    public static Sprite[] Demibats3;
    public static Sprite[] Demibats4;
    public static Sprite[] Avians1;
    public static Sprite[] Avians2;
    public static Sprite[] Avians3;
    public static Sprite[] Avians4;
    public static Sprite[] Demiants1;
    public static Sprite[] Demiants2;
    public static Sprite[] Demiants3;
    public static Sprite[] AntQueen1;
    public static Sprite[] AntQueen2;
    public static Sprite[] Demifrogs1;
    public static Sprite[] Demifrogs2;
    public static Sprite[] Demifrogs3;
    public static Sprite[] Demifrogs3alt;
    public static Sprite[] Demifrogs4;
    public static Sprite[] Demifrogs5;
    public static Sprite[] Sharks1;
    public static Sprite[] Sharks2;
    public static Sprite[] Sharks3;
    public static Sprite[] Sharks4;
    public static Sprite[] Sharks5;
    public static Sprite[] Sharks6;
    public static Sprite[] Sharks7;
    public static Sprite[] Deer1;
    public static Sprite[] Deer2;
    public static Sprite[] Deer3;
    public static Sprite[] Deer4;
    public static Sprite[] Alligators;
    public static Sprite[] Hippos;
    public static Sprite[] Hippos2;
    public static Sprite[] Hippos3;
    public static Sprite[] Komodos1;
    public static Sprite[] Komodos2;
    public static Sprite[] Komodos3;
    public static Sprite[] Komodos4;
    public static Sprite[] Cockatrice2;
    public static Sprite[] Cockatrice3;
    public static Sprite[] Vargul1;
    public static Sprite[] Vargul2;
    public static Sprite[] Vargul3;
    public static Sprite[] Vargul4;
    public static Sprite[] Vargul5;
    public static Sprite[] BadgersBodies;
    public static Sprite[] BadgersClothes;
    public static Sprite[] BadgersVoreBlack;
    public static Sprite[] BadgersVoreWhite;

    public static Sprite[] PantherBase;
    public static Sprite[] PantherVoreParts;

    public static Sprite[] Horse;
    public static Sprite[] HorseClothing;
    public static Sprite[] HorseExtras1;
    
    public static Sprite[] HumansBodySprites1;
    public static Sprite[] HumansBodySprites2;
    public static Sprite[] HumansBodySprites3;
    public static Sprite[] HumansBodySprites4;
    public static Sprite[] HumansBodySprites5;
    public static Sprite[] HumansVoreSprites;
    public static Sprite[] AabayxLeader;

    public static Sprite[] Mice1;
    public static Sprite[] Mice2;
    public static Sprite[] MiceFBottoms;
    public static Sprite[] MiceFTops;
    public static Sprite[] MiceMBottoms;
    public static Sprite[] MiceMTops;
    public static Sprite[] MiceVore1;
    public static Sprite[] MiceVore2;
    public static Sprite[] MiceVore3;

    public static Sprite[] GnollClothes;
    public static Sprite[] GnollsBodyParts;
    public static Sprite[] GnollsVoreParts;

    public static Sprite[] TaurTorso;

    public static Sprite[] Lupine1;
    public static Sprite[] Lupine2;
    public static Sprite[] LupineClothes;
    public static Sprite[] LupineClothes2;
    public static Sprite[] LupineVoreSprites;

    public static Sprite[] JackalMain;
    public static Sprite[] JackalClothes;
    public static Sprite[] JackalJewel;

    public static Sprite[] DemidragonBodies;
    public static Sprite[] DemidragonBodies2;
    public static Sprite[] DemidragonBreasts;
    public static Sprite[] DemidragonClothes;
    public static Sprite[] DemidragonClothes2;
    public static Sprite[] DemidragonClothes3;
    public static Sprite[] DemidragonCustomisation1;
    public static Sprite[] DemidragonCustomisation2;
    public static Sprite[] DemidragonCustomisation3;
    public static Sprite[] DemidragonPatterns;
    public static Sprite[] DemidragonPatterns2;
    public static Sprite[] DemidragonBelliesBalls;
    public static Sprite[] Serpents;
    public static Sprite[] Wyvern;
    public static Sprite[] WyvernMatron;
    public static Sprite[] FeralWolf;
    public static Sprite[] Harvester;
    public static Sprite[] Bat;
    public static Sprite[] Kobolds;
    public static Sprite[] Dragon;
    public static Sprite[] Dragonfly;
    public static Sprite[] Plant;
    public static Sprite[] Ant;
    public static Sprite[] Gryphon;
    public static Sprite[] Griffin;
    public static Sprite[] SpitterSlug;
    public static Sprite[] SpringSlug;
    public static Sprite[] RockSlug;
    public static Sprite[] CoralSlug;
    public static Sprite[] Salamanders;
    public static Sprite[] Mantis;
    public static Sprite[] EasternDragon;
    public static Sprite[] Catfish;
    public static Sprite[] WarriorAnt;
    public static Sprite[] Gazelle1;
    public static Sprite[] Gazelle2;
    public static Sprite[] FeralLizards;
    public static Sprite[] Monitors;
    public static Sprite[] Terrorbird;
    public static Sprite[] Dratopyr;
    public static Sprite[] FeralLions;
    public static Sprite[] Goodra;
    public static Sprite[] FeralHorses;
    public static Sprite[] FeralFox;
    public static Sprite[] Terminid;
    public static Sprite[] Otachi;
    public static Sprite[] FeralSlime;
    public static Sprite[] FeralUmbreon;
    public static Sprite[] Smudger;
    public static Sprite[] SpaceCroach;
    public static Sprite[] Raiju;
    public static Sprite[] Raiju240;
    public static Sprite[] Utahraptor200;
    public static Sprite[] Utahraptor240A;
    public static Sprite[] Utahraptor240B;
    public static Sprite[] Trex;
    public static Sprite[] BlackWidow;
    public static Sprite[] AngelProjectile;


    [Header("Other")]
    public Sprite[] Scat;
    public Sprite[] ScatV2LBack;
    public Sprite[] ScatV2LFront;
    public Sprite[] ScatV2MBack;
    public Sprite[] ScatV2MFront;
    public Sprite[] ScatV2SBack;
    public Sprite[] ScatV2SFront;
    public Sprite[] ScatViralV2LBack;
    public Sprite[] ScatViralV2LFront;
    public Sprite[] ScatViralV2MBack;
    public Sprite[] ScatViralV2MFront;
    public Sprite[] ScatViralV2SBack;
    public Sprite[] ScatViralV2SFront;
    public Sprite[] BirdScat;
    public Sprite[] Bones;
    public Sprite[] CleanDisposalL;
    public Sprite[] CleanDisposalM;
    public Sprite[] CleanDisposalS;
    public Sprite[] SpellIcons;

}
