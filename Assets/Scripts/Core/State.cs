// Modified for the VoreWar mercenary-company conversion.
// 2026-10-30: Enforce code-owned settings and retained global preferences (spec 02C).
// 2026-10-30: Remove legacy event initialization and startup/reset event-file seeding (spec 04A).
// 2026-10-30: Retire rejected-species name seeds and reset active lists without deleting paths (spec 03B2).
// 2026-10-31: Retire legacy world load/preview and empire campaign entry routes (spec 05A1).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

public static class State
{
    static int saveErrors = 0;
    public const string Version = "45D";
    public static World World;
    public static Rand Rand = new Rand();
    public static NameGenerator NameGen;
    public static GameManager GameManager;
    public static AssimilateList AssimilateList;
    public static Dictionary<Traits, TaggedTrait> TieredTraitsList;
    public static List<String> TieredTraitsTagsList;
    public static List<RandomizeList> RandomizeLists;
    public static List<CustomTraitBoost> CustomTraitList;
    public static List<ConditionalTraitContainer> ConditionalTraitList;
    public static List<UnitTag> UnitTagList;
    public static Dictionary<Traits, List<int>> UnitTagAssociatedTraitDictionary;
    public static Dictionary<TaggedTrait, bool> UntaggedTraits;


    internal static RaceSettings RaceSettings;

    public static bool TutorialMode;
    public static bool Warned = false;

    public static string SaveDirectory;
    public static string StorageDirectory;
    public static string MapDirectory;
    public static string CustomTraitDirectory;
    public static string ConditionalTraitDirectory;
    public static string UnitTagDirectory;
    public static string NameFileDirectory;

    public static int RaceSlot;
    public static string RaceSaveDataName;
    // Only active generic/candidate name files are seeded or reset; old personal files remain untouched.
    public static string[] nameTextFileNames = new string[]
    {
        "armyNames", "males", "females", "monsters", "femaleFeralLions", "maleFeralLions",
        "CoralSlugs", "Dragonfly", "Catfish", "femaleAlligators", "maleAlligators", "femaleAnts",
        "maleAnts", "femaleAvians", "maleAvians", "femaleBats", "maleBats", "femaleBees",
        "maleBees", "femaleBunnies", "maleBunnies", "femaleDeer", "maleDeer", "femaleDragon",
        "maleDragon", "femaleDratopyr", "maleDratopyr", "femaleEasternDragon", "maleEasternDragon", "femaleEquines",
        "maleEquines", "femaleFeralBats", "maleFeralBats", "femaleFeralFox", "maleFeralFox", "femaleFeralHorses",
        "maleFeralHorses", "femaleFeralLizards", "maleFeralLizards", "femaleFrogs", "maleFrogs", "femaleGazelle",
        "maleGazelle", "femaleGryphons", "maleGryphons", "femaleHippos", "maleHippos", "femaleKangaroos",
        "maleKangaroos", "femaleKobolds", "maleKobolds", "femaleKomodos", "maleKomodos", "femaleLizards",
        "maleLizards", "femaleMantis", "maleMantis", "femaleMonitors", "maleMonitors", "femalePanthers",
        "malePanthers", "femaleSergal", "maleSergal", "femaleSharks", "maleSharks", "femaleTerrorbird",
        "maleTerrorbird", "femaleVargul", "maleVargul", "femaleWyvern", "maleWyvern", "FeralAnts",
        "FeralWolves", "Harvesters", "RockSlugs", "Salamanders", "Serpents", "SpitterSlugs",
        "SpringSlugs", "WarriorAnts", "WyvernMatron", "femaleGnolls", "maleGnolls", "FeralSlimes",
        "maleLupine", "femaleLupine", "femaleMatronsMinions", "maleMatronsMinions", "femaleJackals", "maleJackals",
        "TwistedVines", "femaleOtachi", "maleOtachi", "femaleRaiju", "maleRaiju", "femaleSmudger",
        "maleSmudger", "femaleBadgers", "maleBadgers", "femaleUtahraptor", "maleUtahraptor", "femaleTrex",
        "maleTrex", "femaleSpaceCroach", "maleSpaceCroach", "femaleMice", "maleMice", "Terminid",
        "femaleFeralUmbreon", "maleFeralUmbreon", "maleDraconians", "femaleDraconians", "maleBlackWidow", "femaleBlackWidow",
    };


    static State()
    {
        if (Application.platform == RuntimePlatform.OSXPlayer)
        {
            SaveDirectory = Application.persistentDataPath + $"Saves{Path.DirectorySeparatorChar}";
            StorageDirectory = Application.persistentDataPath + Path.DirectorySeparatorChar;
            MapDirectory = Application.persistentDataPath + $"Maps{Path.DirectorySeparatorChar}";
            CustomTraitDirectory = Application.persistentDataPath + $"CustomTraits{Path.DirectorySeparatorChar}";
            ConditionalTraitDirectory = Application.persistentDataPath + $"ConditionalTraits{Path.DirectorySeparatorChar}";
            UnitTagDirectory = Application.persistentDataPath + $"UnitTags{Path.DirectorySeparatorChar}";
            NameFileDirectory = Application.persistentDataPath + $"NameFiles{Path.DirectorySeparatorChar}";
        }
        else
        {
            SaveDirectory = $"UserData{Path.DirectorySeparatorChar}Saves{Path.DirectorySeparatorChar}";
            StorageDirectory = $"UserData{Path.DirectorySeparatorChar}";
            MapDirectory = $"UserData{Path.DirectorySeparatorChar}Maps{Path.DirectorySeparatorChar}";
            CustomTraitDirectory = $"UserData{Path.DirectorySeparatorChar}CustomTraits{Path.DirectorySeparatorChar}";
            ConditionalTraitDirectory = $"UserData{Path.DirectorySeparatorChar}ConditionalTraits{Path.DirectorySeparatorChar}";
            UnitTagDirectory = $"UserData{Path.DirectorySeparatorChar}UnitTags{Path.DirectorySeparatorChar}";
            NameFileDirectory = $"UserData{Path.DirectorySeparatorChar}NameFiles{Path.DirectorySeparatorChar}";
        }
        try
        {
            Directory.CreateDirectory(StorageDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(MapDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(SaveDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(CustomTraitDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(ConditionalTraitDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(UnitTagDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(NameFileDirectory.TrimEnd(new char[] { '\\', '/' }));
        }
        catch
        {
            SaveDirectory = Application.persistentDataPath + $"Saves{Path.DirectorySeparatorChar}";
            StorageDirectory = Application.persistentDataPath + Path.DirectorySeparatorChar;
            MapDirectory = Application.persistentDataPath + $"Maps{Path.DirectorySeparatorChar}";
            CustomTraitDirectory = Application.persistentDataPath + $"CustomTraits{Path.DirectorySeparatorChar}";
            ConditionalTraitDirectory = Application.persistentDataPath + $"ConditionalTraits{Path.DirectorySeparatorChar}";
            UnitTagDirectory = Application.persistentDataPath + $"UnitTags{Path.DirectorySeparatorChar}";
            NameFileDirectory = Application.persistentDataPath + $"NameFiles{Path.DirectorySeparatorChar}";
            Directory.CreateDirectory(StorageDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(MapDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(SaveDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(CustomTraitDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(ConditionalTraitDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(UnitTagDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(NameFileDirectory.TrimEnd(new char[] { '\\', '/' }));
        }



        string[] systemTextFileNames = new string[] { "customTraits" };

        try
        {
            foreach (string text in systemTextFileNames)
            {
                if (File.Exists($"{StorageDirectory}{text}.txt") == false)
                    File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}{text}.txt", $"{StorageDirectory}{text}.txt");
            }
        }
        catch
        {
            Debug.Log("Initial setup failed!");
        }
        try
        {
            foreach (string nameList in nameTextFileNames)
            {
                if (File.Exists($"{NameFileDirectory}{nameList}.txt") == false)
                    File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}{nameList}.txt", $"{NameFileDirectory}{nameList}.txt");
            }
        }
        catch
        {
            Debug.Log("Name setup failed!");
        }


        try
        {
            if (File.Exists($"{StorageDirectory}taggedTraits.json") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}taggedTraits.json", $"{StorageDirectory}taggedTraits.json");
            if (File.Exists($"{StorageDirectory}buildingConfig.json") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}buildingConfig.json", $"{StorageDirectory}buildingConfig.json");
        }
        catch
        {
            Debug.Log("Initial setup failed!");
        }

        string bundlePath = Path.Combine($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}AssetBundles", "abakhanskya");

        AssetBundleLoader.LoadAssetBundles();

        FlagLoader.FlagLoader flagLoader = new FlagLoader.FlagLoader();
        flagLoader.LoadFlags();
        NameGen = new NameGenerator();
        AssimilateList = new AssimilateList();
        CustomTraitList = new List<CustomTraitBoost>();
        ConditionalTraitList = new List<ConditionalTraitContainer>();
        UnitTagList = new List<UnitTag>();
        UnitTagAssociatedTraitDictionary = new Dictionary<Traits, List<int>>();
        UntaggedTraits = new Dictionary<TaggedTrait, bool>();

        TieredTraitsList = ExternalTraitHandler.TaggedTraitParser(); 
        TieredTraitsList = ExternalTraitHandler.TaggedTraitUpdater();
        TieredTraitsTagsList = new List<string>();
        ExternalTraitHandler.CustomTraitParser();
        ExternalTraitHandler.ConditionalTraitParser();
        ExternalTraitHandler.UnitTagParser();
        TagConditionChecker.CompileTraitTagAssociateDict();
        Encoding encoding = Encoding.GetEncoding("iso-8859-1");
        List<string> lines;
        RandomizeLists = new List<RandomizeList>();
        if (File.Exists($"{State.StorageDirectory}customTraits.txt"))
        {
            var logFile = File.ReadAllLines($"{State.StorageDirectory}customTraits.txt", encoding);
            if (logFile.Any())
            {
                lines = new List<string>(logFile);
                int count = 0;
                lines.ForEach(line =>
                {
                    count++;
                    RandomizeList custom = new RandomizeList();
                    line = new string(line
                       .Where(c => !Char.IsWhiteSpace(c)).ToArray());
                    string[] strings = line.Split(',');
                    if (strings.Length == 4)
                    {
                        custom.id = int.Parse(strings[0]);
                        custom.name = strings[1];
                        custom.chance = float.Parse(strings[2], new CultureInfo("en-US"));
                        custom.level = 0;
                        custom.count = 1;
                        custom.RandomTraits = strings[3].Split('|').ToList().ConvertAll(s => (Traits)int.Parse(s));
                        RandomizeLists.Add(custom);
                    } else if (strings.Length == 6)
                    {
                        custom.id = int.Parse(strings[0]);
                        custom.name = strings[1];
                        custom.chance = float.Parse(strings[2], new CultureInfo("en-US"));
                        custom.count = int.Parse(strings[3]);
                        custom.level = int.Parse(strings[4]);
                        custom.RandomTraits = strings[5].Split('|').ToList().ConvertAll(s => (Traits)int.Parse(s));
                        RandomizeLists.Add(custom);
                    }
                });
            }
        }

        foreach (Traits trait in (Traits[])Enum.GetValues(typeof(Traits)))
        {
            if (TieredTraitsList.Keys.Contains(trait))
            {
                if (TieredTraitsList[trait].tags == null)
                {
                    if (UntaggedTraits.ContainsKey(TieredTraitsList[trait]))
                    {
                        continue;
                    }
                    UntaggedTraits.Add(TieredTraitsList[trait], true);
                    continue;
                }
                if (TieredTraitsList[trait].tags.Count <= 0)
                {
                    UntaggedTraits.Add(TieredTraitsList[trait], true);
                }
            }
            else
            {
                TaggedTrait newTrait = new TaggedTrait();
                newTrait.name = trait.ToString();
                newTrait.tierValue = TraitTier.Neutral;
                newTrait.tier = newTrait.tierValue.ToString();
                newTrait.traitEnum = trait;
                UntaggedTraits.Add(newTrait, false);
            }
        }

        List<TaggedTrait> newTraits = new List<TaggedTrait>();
        foreach (var newTrait in UntaggedTraits)
        {
            if (newTrait.Value)
            {
                continue;
            }
            newTraits.Add(newTrait.Key);
        }

        ExternalTraitHandler.AppendTaggedTrait(newTraits);


    }

    public static void WipeUserdata()
    {
        if (Application.platform == RuntimePlatform.OSXPlayer)
        {
            if (Directory.Exists(Application.persistentDataPath))
                Directory.Delete(Application.persistentDataPath, true);
            SaveDirectory = Application.persistentDataPath + $"Saves{Path.DirectorySeparatorChar}";
            StorageDirectory = Application.persistentDataPath + Path.DirectorySeparatorChar;
            MapDirectory = Application.persistentDataPath + $"Maps{Path.DirectorySeparatorChar}";
            CustomTraitDirectory = Application.persistentDataPath + $"CustomTraits{Path.DirectorySeparatorChar}";
            ConditionalTraitDirectory = Application.persistentDataPath + $"ConditionalTraits{Path.DirectorySeparatorChar}";
            UnitTagDirectory = Application.persistentDataPath + $"UnitTags{Path.DirectorySeparatorChar}";
            NameFileDirectory = Application.persistentDataPath + $"NameFiles{Path.DirectorySeparatorChar}";
        }
        else
        {
            if (Directory.Exists($"UserData"))
                Directory.Delete($"UserData", true);
            SaveDirectory = $"UserData{Path.DirectorySeparatorChar}Saves{Path.DirectorySeparatorChar}";
            StorageDirectory = $"UserData{Path.DirectorySeparatorChar}";
            MapDirectory = $"UserData{Path.DirectorySeparatorChar}Maps{Path.DirectorySeparatorChar}";
            CustomTraitDirectory = $"UserData{Path.DirectorySeparatorChar}CustomTraits{Path.DirectorySeparatorChar}";
            ConditionalTraitDirectory = $"UserData{Path.DirectorySeparatorChar}ConditionalTraits{Path.DirectorySeparatorChar}";
            UnitTagDirectory = $"UserData{Path.DirectorySeparatorChar}UnitTags{Path.DirectorySeparatorChar}";
            NameFileDirectory = $"UserData{Path.DirectorySeparatorChar}NameFiles{Path.DirectorySeparatorChar}";
        }
        try
        {
            Directory.CreateDirectory(StorageDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(MapDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(SaveDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(CustomTraitDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(ConditionalTraitDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(UnitTagDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(NameFileDirectory.TrimEnd(new char[] { '\\', '/' }));
        }
        catch
        {
            SaveDirectory = Application.persistentDataPath + $"Saves{Path.DirectorySeparatorChar}";
            StorageDirectory = Application.persistentDataPath + Path.DirectorySeparatorChar;
            MapDirectory = Application.persistentDataPath + $"Maps{Path.DirectorySeparatorChar}";
            CustomTraitDirectory = Application.persistentDataPath + $"CustomTraits{Path.DirectorySeparatorChar}";
            ConditionalTraitDirectory = Application.persistentDataPath + $"ConditionalTraits{Path.DirectorySeparatorChar}";
            UnitTagDirectory = Application.persistentDataPath + $"UnitTags{Path.DirectorySeparatorChar}";
            NameFileDirectory = Application.persistentDataPath + $"NameFiles{Path.DirectorySeparatorChar}";
            Directory.CreateDirectory(StorageDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(MapDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(SaveDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(CustomTraitDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(ConditionalTraitDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(UnitTagDirectory.TrimEnd(new char[] { '\\', '/' }));
            Directory.CreateDirectory(NameFileDirectory.TrimEnd(new char[] { '\\', '/' }));
        }



        string[] systemTextFileNames = new string[] { "customTraits" };

        try
        {
            foreach (string text in systemTextFileNames)
            {
                if (File.Exists($"{StorageDirectory}{text}.txt") == false)
                    File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}{text}.txt", $"{StorageDirectory}{text}.txt");
            }
        }
        catch
        {
            Debug.Log("Initial setup failed!");
        }
        try
        {
            foreach (string nameList in nameTextFileNames)
            {
                if (File.Exists($"{NameFileDirectory}{nameList}.txt") == false)
                    File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}{nameList}.txt", $"{NameFileDirectory}{nameList}.txt");
            }
        }
        catch
        {
            Debug.Log("Name setup failed!");
        }


        try
        {
            if (File.Exists($"{StorageDirectory}taggedTraits.json") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}taggedTraits.json", $"{StorageDirectory}taggedTraits.json");
            if (File.Exists($"{StorageDirectory}buildingConfig.json") == false)
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}buildingConfig.json", $"{StorageDirectory}buildingConfig.json");
        }
        catch
        {
            Debug.Log("Initial setup failed!");
        }

        FlagLoader.FlagLoader flagLoader = new FlagLoader.FlagLoader();
        flagLoader.LoadFlags();
        NameGen = new NameGenerator();
        AssimilateList = new AssimilateList();
        CustomTraitList = new List<CustomTraitBoost>();
        ConditionalTraitList = new List<ConditionalTraitContainer>();
        UnitTagList = new List<UnitTag>();
        UnitTagAssociatedTraitDictionary = new Dictionary<Traits, List<int>>();
        UntaggedTraits = new Dictionary<TaggedTrait, bool>();

        TieredTraitsList = ExternalTraitHandler.TaggedTraitParser();
        TieredTraitsTagsList = new List<string>();
        ExternalTraitHandler.CustomTraitParser();
        ExternalTraitHandler.ConditionalTraitParser();
        ExternalTraitHandler.UnitTagParser();
        TagConditionChecker.CompileTraitTagAssociateDict();
        Encoding encoding = Encoding.GetEncoding("iso-8859-1");
        List<string> lines;
        RandomizeLists = new List<RandomizeList>();
        if (File.Exists($"{State.StorageDirectory}customTraits.txt"))
        {
            var logFile = File.ReadAllLines($"{State.StorageDirectory}customTraits.txt", encoding);
            if (logFile.Any())
            {
                lines = new List<string>(logFile);
                int count = 0;
                lines.ForEach(line =>
                {
                    count++;
                    RandomizeList custom = new RandomizeList();
                    line = new string(line
                       .Where(c => !Char.IsWhiteSpace(c)).ToArray());
                    string[] strings = line.Split(',');
                    if (strings.Length == 4)
                    {
                        custom.id = int.Parse(strings[0]);
                        custom.name = strings[1];
                        custom.chance = float.Parse(strings[2], new CultureInfo("en-US"));
                        custom.level = 0;
                        custom.count = 1;
                        custom.RandomTraits = strings[3].Split('|').ToList().ConvertAll(s => (Traits)int.Parse(s));
                        RandomizeLists.Add(custom);
                    } else if (strings.Length == 6)
                    {
                        custom.id = int.Parse(strings[0]);
                        custom.name = strings[1];
                        custom.chance = float.Parse(strings[2], new CultureInfo("en-US"));
                        custom.count = int.Parse(strings[3]);
                        custom.level = int.Parse(strings[4]);
                        custom.RandomTraits = strings[5].Split('|').ToList().ConvertAll(s => (Traits)int.Parse(s));
                        RandomizeLists.Add(custom);
                    }
                });
            }
        }

        foreach (Traits trait in (Traits[])Enum.GetValues(typeof(Traits)))
        {
            if (TieredTraitsList.Keys.Contains(trait))
            {
                if (TieredTraitsList[trait].tags == null)
                {
                    UntaggedTraits.Add(TieredTraitsList[trait], true);
                    continue;
                }
                if (TieredTraitsList[trait].tags.Count <= 0)
                {
                    UntaggedTraits.Add(TieredTraitsList[trait], true);
                }
            }
            else
            {
                TaggedTrait newTrait = new TaggedTrait();
                newTrait.name = trait.ToString();
                newTrait.tierValue = TraitTier.Neutral;
                newTrait.tier = newTrait.tierValue.ToString();
                newTrait.traitEnum = trait;
                UntaggedTraits.Add(newTrait, false);
            }
        }

        List<TaggedTrait> newTraits = new List<TaggedTrait>();
        foreach (var newTrait in UntaggedTraits)
        {
            if (newTrait.Value)
            {
                continue;
            }
            newTraits.Add(newTrait.Key);
        }

        ExternalTraitHandler.AppendTaggedTrait(newTraits);


    }

    public static void SaveEditedRaces()
    {
        try
        {
            byte[] bytes = SerializationUtility.SerializeValue(RaceSettings, DataFormat.Binary);
            File.WriteAllBytes(RaceSaveDataName, bytes);
        }
        catch
        {
            Debug.LogWarning("Failed to properly save edited races!");
        }
    }

    public static void LoadRaceData()
    {
        RaceSlot = PlayerPrefs.GetInt("RaceEditorSlot", 1);
        ChangeRaceSlotUsed(RaceSlot);
    }

    public static void ChangeRaceSlotUsed(int num)
    {
        RaceSlot = num;
        PlayerPrefs.SetInt("RaceEditorSlot", num);
        if (RaceSlot <= 1)
            RaceSaveDataName = $"{StorageDirectory}EditedRaces.dat";
        else if (RaceSlot == 2)
            RaceSaveDataName = $"{StorageDirectory}EditedRaces2.dat";
        else if (RaceSlot == 3)
            RaceSaveDataName = $"{StorageDirectory}EditedRaces3.dat";
        LoadEditedRaces();
    }

    public static void LoadEditedRaces()
    {
        try
        {
            if (File.Exists(RaceSaveDataName))
            {
                byte[] bytes = File.ReadAllBytes(RaceSaveDataName);
                RaceSettings = SerializationUtility.DeserializeValue<RaceSettings>(bytes, DataFormat.Binary);
                GameManager.Start_Mode.miscText.text = "Successfully read race settings";
                RaceSettings.Sanitize();
            }
            else
            {
                RaceSettings = new RaceSettings();
                GameManager.Start_Mode.miscText.text = "No modified race settings found, using default";
            }
        }
        catch
        {
            RaceSettings = new RaceSettings();
            GameManager.Start_Mode.miscText.text = "Failed to properly read race settings";
        }
    }


    public static void ResetNamelists()
    {
        try
        {
            foreach (string nameList in nameTextFileNames)
            {
                File.Copy($"{Application.streamingAssetsPath}{Path.DirectorySeparatorChar}{nameList}.txt", $"{NameFileDirectory}{nameList}.txt", true);
            }
            NameGen = new NameGenerator();
        }
        catch
        {
            Debug.LogWarning("Namelist refresh failed!");
        }
    }

    public static void ReloadNamelists()
    {
        NameGen = new NameGenerator();
    }


    public static void Save(string filename)
    {
        try
        {
            for (int i = 0; i < 3; i++)
            {
                if (filename.EndsWith("/") || filename.EndsWith("\\"))
                    filename = filename.Remove(filename.Length - 1, 1);
                else
                    break;
            }

            World.SaveVersion = Version;
            if (GameManager.CurrentScene == GameManager.TacticalMode)
            {
                GameManager.CameraController.SaveTacticalCamera();
                World.TacticalData = GameManager.TacticalMode.Export();
            }
            else
                World.TacticalData = null;
            byte[] bytes = SerializationUtility.SerializeValue(World, DataFormat.Binary);
            File.WriteAllBytes(filename, bytes);
        }
        catch
        {
            saveErrors++;
            if (saveErrors < 3)
            {
                GameManager.CreateMessageBox($"Unable to save properly, {filename} didn't work (will only warn 3 times in a single session)");
            }
            else if (saveErrors == 3)
            {
                GameManager.CreateMessageBox($"Unable to save properly, {filename} didn't work (will no longer warn you this session)");
            }

        }

    }

    public const string LegacyLoadUnavailableMessage = "Legacy VoreWar saves cannot be loaded in this conversion. Company save support will be added with the new campaign.";
    public const string CampaignUnavailableMessage = "The empire campaign is retired. The mercenary-company campaign is not available yet.";

    public static World PreviewSave(string filename)
    {
        // Do not deserialize the unsupported World format, even for a preview.
        return null;
    }

    public static void Load(string filename, bool tutorial = false)
    {
        GameManager.CreateMessageBox(LegacyLoadUnavailableMessage);
    }
}


