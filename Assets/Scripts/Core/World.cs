// Modified for the VoreWar mercenary-company conversion.
// 2026-10-31: Retire unused empire-campaign construction; retain standalone tactical context (spec 05A3A).
// 2026-11-01: Retire monster/empire world construction and maintenance; retain battle context (spec 05A5).
// 2026-11-01: Provide an isolated company battle context without restarting empire configuration/simulation (spec 10).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using MapObjects;
using OdinSerializer;
using System.Collections.Generic;


public class World
{
    internal const int MonsterCount = 52;//Be sure to increase when adding new monsters
    [OdinSerialize]
    public int Turn = 1;
    [OdinSerialize]
    public string SaveVersion;
    [OdinSerialize]
    public List<Empire> EmpireOrder;
    public StrategicTileType[,] Tiles;
    public StrategicDoodadType[,] Doodads;
    public Village[] Villages;

    public List<Empire> MainEmpires;
    /// <summary>
    /// Deprecated, only left in for compatibility
    /// </summary>
    public Empire[] Empires;
    public Empire ActingEmpire;
    public ItemRepository ItemRepository;
    public WorldConfig ConfigStorage;
    public BuildingConfig BuildingConfigStorage;
    public StrategicStats Stats;
    public TacticalData TacticalData;

    [OdinSerialize]
    internal Dictionary<int, Dictionary<int, Relationship>> Relations;



    [OdinSerialize]
    public bool crazyBuildings = false;

    [OdinSerialize]
    internal SavedCameraState SavedCameraState;

    public MonsterEmpire[] MonsterEmpires;

    public MercenaryHouse[] MercenaryHouses;

    public AncientTeleporter[] AncientTeleporters;

    [OdinSerialize]
    internal ClaimableBuilding[] Claimables;
    [OdinSerialize]
    internal ConstructibleBuilding[] Constructibles;

    public List<Empire> AllActiveEmpires;

    [OdinSerialize]
    public List<Reincarnator> Reincarnators;

    [OdinSerialize]
    public bool IsNight = false;
	
    internal World(ItemRepository battleItems)
    {
        State.World = this; ItemRepository = battleItems; ConfigStorage = Config.World;
        MainEmpires = new List<Empire>(); AllActiveEmpires = MainEmpires; Villages = new Village[0];
        MercenaryHouses = new MercenaryHouse[0]; AncientTeleporters = new AncientTeleporter[0];
        Claimables = new ClaimableBuilding[0]; Constructibles = new ConstructibleBuilding[0];
    }

    public World()
    {

        Config.World.VillagesPerEmpire = new int[Config.NumberOfRaces];
        Config.CenteredEmpire = new bool[Config.NumberOfRaces];
        State.World = this;
        ConfigStorage = Config.World;
        BuildingConfigStorage = Config.BuildConfig;
        ItemRepository = new ItemRepository();
        AllActiveEmpires = MainEmpires;
        MercenaryHouses = new MercenaryHouse[0];
        AncientTeleporters = new AncientTeleporter[0];
        Claimables = new ClaimableBuilding[0];
        Constructibles = new ConstructibleBuilding[0];
    }

    // The uncalled empire-campaign world constructor is retired.


    internal void UpdateBanditLimits()
    {
        // Retired empire maintenance; retained for old UI callers.
    }

    internal void RefreshTurnOrder()
    {
        // Retired empire maintenance; retained for old UI callers.
    }

    internal Empire GetEmpireOfRace(Race race)
    {
        if (AllActiveEmpires == null)
            return null;
        for (int i = 0; i < AllActiveEmpires.Count; i++)
        {
            if (AllActiveEmpires[i].Race == race)
                return AllActiveEmpires[i];
        }
        for (int i = 0; i < AllActiveEmpires.Count; i++)
        {
            if (AllActiveEmpires[i].ReplacedRace == race)
                return AllActiveEmpires[i];
        }
        return null;
    }

    internal Empire GetEmpireOfSide(int side)
    {
        if (AllActiveEmpires == null)
            return null;
        for (int i = 0; i < AllActiveEmpires.Count; i++)
        {
            if (AllActiveEmpires[i].Side == side)
                return AllActiveEmpires[i];
        }
        return null;
    }

}
