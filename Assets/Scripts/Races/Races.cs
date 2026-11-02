// Modified for the VoreWar mercenary-company conversion.
// 2026-11-01: Disconnect rejected graphics instances and their typed rendering hooks (spec 03B4).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
static class Races
{
    static internal Bunnies Bunnies = new Bunnies();
    static internal Lizards Lizards = new Lizards();
    static internal Kangaroos Kangaroos = new Kangaroos();
    static internal Equines Equines = new Equines();
    static internal Sergal Sergal = new Sergal();
    static internal Bees Bees = new Bees();
    static internal Demibats Demibats = new Demibats();
    static internal Panthers Panthers = new Panthers();
    static internal Avians Avians = new Avians();
    static internal Demiants Demiants = new Demiants();
    static internal AntQueen AntQueen = new AntQueen();
    static internal Demifrogs Demifrogs = new Demifrogs();
    static internal Demisharks Demisharks = new Demisharks();
    static internal Deer Deer = new Deer();
    static internal Mice Mice = new Mice();
    static internal MatronsMinions MatronsMinions = new MatronsMinions();
    static internal Gnolls Gnolls = new Gnolls();
    static internal Lupine Lupine = new Lupine();
    static internal Jackals Jackals = new Jackals();
    static internal DemiDragons DemiDragons = new DemiDragons();


    static internal Alligators Alligators = new Alligators();
    static internal Hippos Hippos = new Hippos();
    static internal Komodos Komodos = new Komodos();
    static internal Vargul Vargul = new Vargul();
    static internal Badgers Badgers = new Badgers();

    static internal Serpents Serpents = new Serpents();
    static internal Wyvern Wyvern = new Wyvern();
    static internal WyvernMatron WyvernMatron = new WyvernMatron();
    static internal FeralWolf FeralWolf = new FeralWolf();
    static internal Harvester Harvester = new Harvester();
    static internal Bat Bat = new Bat();
    static internal Kobolds Kobolds = new Kobolds();
    static internal Dragon Dragon = new Dragon();
    static internal Dragonfly Dragonfly = new Dragonfly();
    static internal Plant Plant = new Plant();
    static internal Ant Ant = new Ant();
    static internal Gryphon Gryphon = new Gryphon();
    static internal SpitterSlug SpitterSlug = new SpitterSlug();
    static internal SpringSlug SpringSlug = new SpringSlug();
    static internal RockSlug RockSlug = new RockSlug();
    static internal CoralSlug CoralSlug = new CoralSlug();
    static internal Salamander Salamander = new Salamander();
    static internal Mantis Mantis = new Mantis();
    static internal EasternDragon EasternDragon = new EasternDragon();
    static internal Catfish Catfish = new Catfish();
    static internal WarriorAnt WarriorAnt = new WarriorAnt();
    static internal Gazelle Gazelle = new Gazelle();
    static internal FeralLizards FeralLizards = new FeralLizards();
    static internal Monitors Monitors = new Monitors();
    static internal Terrorbird Terrorbird = new Terrorbird();
    static internal Dratopyr Dratopyr = new Dratopyr();
    static internal FeralLions FeralLions = new FeralLions();
    static internal FeralSlime FeralSlime = new FeralSlime();
    static internal FeralUmbreon FeralUmbreon = new FeralUmbreon();
    static internal Otachi Otachi = new Otachi();
    static internal Raiju Raiju = new Raiju();
    static internal Smudger Smudger = new Smudger();
    static internal SpaceCroach SpaceCroach = new SpaceCroach();
    static internal Trex Trex = new Trex();
    static internal Utahraptor Utahraptor = new Utahraptor();

    static internal Goodra Goodra = new Goodra();
    static internal FeralHorses FeralHorses = new FeralHorses();
    static internal FeralFox FeralFox = new FeralFox();
    static internal Terminid Terminid = new Terminid();
    static internal BlankSlate BlankSlate = new BlankSlate();
    static internal BlackWidow BlackWidow = new BlackWidow();


    static internal DefaultRaceData GetRace(Unit unit)
    {
        if (unit.Race == Race.Ants && unit.Type == UnitType.Leader)
        {
            return AntQueen;
        }
        return GetRace(unit.Race);
    }

    /// <summary>
    /// Direct species graphics lookup; unit-specific leader variants use the Unit overload
    /// </summary>    
    static internal DefaultRaceData GetRace(Race race)
    {
        // Retired IDs must never render through an unrelated substitute species.
        if (SpeciesPolicy.IsRetired(race))
            throw new System.InvalidOperationException("Species graphics have been retired: " + race);
        //race = State.RaceSettings.GetDisplayedGraphic(race);
        switch (race)
        {
            case Race.Bunnies:
                return Bunnies;
            case Race.Lizards:
                return Lizards;
            case Race.Kangaroos:
                return Kangaroos;
            case Race.Equines:
                return Equines;
            case Race.Sergal:
                return Sergal;
            case Race.Bees:
                return Bees;
            case Race.Bats:
                return Demibats;
            case Race.Alligators:
                return Alligators;
            case Race.Hippos:
                return Hippos;
            case Race.Komodos:
                return Komodos;
            case Race.Serpents:
                return Serpents;
            case Race.Wyvern:
                return Wyvern;
            case Race.WyvernMatron:
                return WyvernMatron;
            case Race.FeralWolves:
                return FeralWolf;
            case Race.Harvesters:
                return Harvester;
            case Race.FeralBats:
                return Bat;
            case Race.Kobolds:
                return Kobolds;
            case Race.Dragon:
                return Dragon;
            case Race.Dragonfly:
                return Dragonfly;
            case Race.TwistedVines:
                return Plant;
            case Race.FeralAnts:
                return Ant;
            case Race.Gryphons:
                return Gryphon;
            case Race.SpitterSlugs:
                return SpitterSlug;
            case Race.SpringSlugs:
                return SpringSlug;
            case Race.RockSlugs:
                return RockSlug;
            case Race.CoralSlugs:
                return CoralSlug;
            case Race.Panthers:
                return Panthers;
            case Race.Salamanders:
                return Salamander;
            case Race.EasternDragon:
                return EasternDragon;
            case Race.Mantis:
                return Mantis;
            case Race.Avians:
                return Avians;
            case Race.Catfish:
                return Catfish;
            case Race.Ants:
                return Demiants;
            case Race.WarriorAnts:
                return WarriorAnt;
            case Race.Frogs:
                return Demifrogs;
            case Race.Gazelle:
                return Gazelle;
            case Race.Sharks:
                return Demisharks;
            case Race.FeralLizards:
                return FeralLizards;
            case Race.Monitors:
                return Monitors;
            case Race.Deer:
                return Deer;
            case Race.Terrorbird:
                return Terrorbird;
            case Race.Vargul:
                return Vargul;
            case Race.Dratopyr:
                return Dratopyr;
            case Race.FeralLions:
                return FeralLions;
            case Race.Goodra:
                return Goodra;
            case Race.Mice:
                return Mice;
            case Race.FeralHorses:
                return FeralHorses;
            case Race.MatronsMinions:
                return MatronsMinions;
            case Race.FeralFox:
                return FeralFox;
            case Race.Terminid:
                return Terminid;
            case Race.Gnolls:
                return Gnolls;
            case Race.FeralSlime:
                return FeralSlime;
            case Race.Lupine:
                return Lupine;
            case Race.FeralUmbreon:
                return FeralUmbreon;
            case Race.Jackals:
                return Jackals;
            case Race.Otachi:
                return Otachi;
            case Race.Raiju:
                return Raiju;
            case Race.Smudger:
                return Smudger;
            case Race.SpaceCroach:
               return SpaceCroach;
            case Race.Draconians:
                return DemiDragons;
            case Race.Trex:
                return Trex;
            case Race.Utahraptor:
                return Utahraptor;
            case Race.Badgers:
                return Badgers;
            case Race.BlackWidow:
                return BlackWidow;
        }
        UnityEngine.Debug.LogWarning("Couldn't find race, substituting the Blank Slate");
        return BlankSlate;
    }

}

