// Modified for the VoreWar mercenary-company conversion.
// 2026-10-30: Retire rejected-species name imports and exclusive pools; preserve generic names (spec 03B2).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class NameGenerator
{

    List<string> femaleNames;
    List<string> maleNames;
    List<string> monsterNames;

    List<string> bunnyTownNames;
    List<string> bunnyPreyTownNames;
    List<string> lizardTownNames;
    List<string> kangarooTownNames;
    List<string> equinesTownNames;
    List<string> sergalTownNames;
    List<string> beeTownNames;
    List<string> batTownNames;
    List<string> pantherTownNames;
    List<string> avianTownNames;
    List<string> antTownNames;
    List<string> frogTownNames;
    List<string> sharkTownNames;
    List<string> deerTownNames;
    List<string> miceTownNames;
    List<string> matronsminionsTownNames;
    List<string> gnollTownNames;
    List<string> lupineTownNames;
    List<string> jackalTownNames;
    List<string> demiDragonTownNames;

    List<string> serpentNames;
    List<string> wyvernNames;
    List<string> gryphonNames;
    List<string> feralLionNames;
    
	List<string> goodraNames;
	
    Dictionary<Race, List<string>> RaceMaleNames;
    Dictionary<Race, List<string>> RaceFemaleNames;
    Dictionary<Race, List<string>> RaceMonsterNames;

    Dictionary<Race, List<string>> ArmyNames;

    Dictionary<Race, string> ArmyNameDefault;

    public NameGenerator()
    {
        femaleNames = new List<string>
        {
            "Ai",
            "Aika",
            "Aiko",
            "Aimi",
            "Aina",
            "Aisa",
            "Aki",
            "Aoi",
            "Asami",
            "Atsuko",
            "Aya",
            "Ayumi",
            "Chie",
            "Chinami",
            "Chitose",
            "Eiko",
            "Emi",
            "Erika",
            "Fumiko",
            "Fusako",
            "Hana",
            "Haruko",
            "Hikari",
            "Hisa",
            "Honoka",
            "Ichiko",
            "Itsumi",
            "Juria",
            "Junko",
            "Kaho",
            "Kanna",
            "Kaneko",
            "Kaori",
            "Karin",
            "Kasumi",
            "Katsue",
            "Keiki",
            "Kira",
            "Kiyoshi",
            "Kotoe",
            "Kotomi",
            "Kurumi",
            "Kyoko",
            "Madoka",
            "Maiko",
            "Mari",
            "Masae",
            "Mayuko",
            "Miiko",
            "Mimori",
            "Minae",
            "Miu",
            "Miwa",
            "Moeko",
            "Nanae",
            "Natsuko",
            "Natsumi",
            "Nonoka",
            "Orime",
            "Reiko",
            "Reina",
            "Rie",
            "Riho",
            "Rumi",
            "Ryoko",
            "Sachie",
            "Sae",
            "Saeko",
            "Saki",
            "Sakura",
            "Sanae",
            "Saori",
            "Sawako",
            "Shiho",
            "Shizuko",
            "Sumie",
            "Tamani",
            "Terumi",
            "Tomoyo",
            "Toya",
            "Tsuru",
            "Umeko",
            "Yasuko",
            "Yuria",
            "Yoshiko",
            "Yukari",
            "Yuko",
            "Yuri",
        };
        monsterNames = new List<string>
        {
            "No names found",
        };
        maleNames = new List<string>
        {
            "Akihiro",
            "Akio",
            "Daiki",
            "Daishi",
            "Fumihiro",
            "Futoshi",
            "Genta",
            "Hakaru",
            "Haruto",
            "Hideki",
            "Hidemasa",
            "Hirotaka",
            "Hisao",
            "Itaru",
            "Jin",
            "Junpei",
            "Kaiji",
            "Kansuke",
            "Kazuma",
            "Kotaru",
            "Masaichi",
            "Minoru",
            "Munetoki",
            "Nagahide",
            "Naritaka",
            "Nobuo",
            "Noriaki",
            "Ryu",
            "Sadaaki",
            "Sadakazu",
            "Seigo",
            "Shigemi",
            "Shinta",
            "Suguru",
            "Tadafumi",
            "Tadanari",
            "Takayoshi",
            "Taketo",
            "Teiji",
            "Tetsuya",
            "Toru",
            "Tsuneo",
            "Yasuki",
            "Yoriyuki",
            "Yoshimoto",
            "Yukishige",
        };
        serpentNames = new List<string>
        {
            "Snaky",
            "Snoot",
            "Snek",
            "Slither",
            "Viper",
            "Tongy",
            "Bity",
            "Bulgy",
            "Huggy",
            "Nippy",
        };
        wyvernNames = new List<string>
        {
            "Swiftwing",
            "Deathtalon",
            "Sharpbeak",
            "Spineback",
        };
        gryphonNames = new List<string>
        {
            "Aquila",
            "Harpia",
            "Accipiter",
            "Kirkos",
            "Cathartes",
            "Necrosyrtes",
            "Neophron",
            "Sarcogyps",
            "Elanus",
            "Milvus",
            "Haliastur",
            "Pandion",
            "Buteo",
            "Falco",
            "Harpagus",
            "Milvago",
            "Caracara",
            "Ibycter",
            "Daptrius",
            "Ictinia",
            "Minerva",
            "Aegolius",
            "Sagittarius",
            "Lanius",
            "Vultur",
            "Surnia",
            "Strix",
            "Pulsatrix",
            "Ninox",
            "Ealonides",
            "Dryotriorchis",
            "Casuarius",
        };
        feralLionNames = new List<string>
        {
            "Kalahari",
            "Okavangu",
            "Zenobia"
        };
        bunnyTownNames = new List<string>
        {
            "Hoppington",
            "Lopdon",
            "Bunburg",
            "Pawdale",
            "Rabiton",
            "Watershed",
            "Cottontail Cove",
        };
        bunnyPreyTownNames = new List<string>
        {
            "The Warren",
            "Underbrush Shelter",
            "Tree Hollow",
            "Hidden Haven",
            "Sanctuary",
            "Bunny Burrow",
            "Felt Burrow",
            "Carrot Burrow",
        };
        lizardTownNames = new List<string>
        {
            "Lizotetca",
            "Reptula",
            "Lair of Bone",
            "Chalscale",
            "Komodoalco",
            "Lair of Venom",
            "Vale of Broken Teeth",
            "Crocotula",
            "Iguatepec",
            "Lair of the Beast",
            "Cult of the Wyrm",
            "Dragon Tongue",
        };
        kangarooTownNames = new List<string>
        {
            "Roostadt",
            "Pouchbottom",
            "Over'under",
            "Red-dust",
            "Sidney",
            "Marsupia",
            "Ayer",
            "Guardia",
            "Uluroo",
        };
        equinesTownNames = new List<string>
        {
            "Cataphracta",
            "Equus",
            "The Ranch",
            "Haciendo",
            "Alfarsan"
        };
        sergalTownNames = new List<string>
        {
            "Glorious City of Eltus",
            "Gold Ring",
            "Astna",
            "Tesae",
            "Col Hazma",
            "Adzma",
            "Nihazama",
            "Lon Sodd",
            "Reono",
            "Etai",
            "Magoe",
            "Salt Outpost"
        };
        beeTownNames = new List<string>
        {
            "Queen's Hive",
            "Honeycomb",
            "Buzzytown",
            "Golden Orchard",
            "Waxville",
            "Bumblebee Grove",
            "Beepolis",
            "Maya's Meadow",
            "Sweet Pollen",
            "Black Swarm",
            "Royal Jelly",
            "Workers Hive",
            "Apiarist Respite"
        };

        batTownNames = new List<string>
        {
            "Dark Grotto",
            "Black Hollow",
            "Nightpoint",
            "Echoing Cavern",
            "Gotham",
            "Batville",
            "Crystal Cave",
            "Deep Den",
            "Sylvania",
            "Strygos",
        };
        pantherTownNames = new List<string>
        {
            "Panthera",
            "Wakana",
            "Therishi",
            "Vogoma",
            "Kwa-Duka",
            "Hlobava",
            "Plundi",
            "Pumatra",
            "Khangela",
            "Qweni-Sho",
            "Sitting Belly",
            "Endless Feast",
            "Gurgling Tents",
        };


        avianTownNames = new List<string>
        {
            "Phoenix Nest",
            "Thunderbird City",
            "Swallow Falls",
            "Dovechester",
            "Garuda Rapids",
            "Duck Pond",
            "Cockatricefields",
            "Horus Springs",
            "Rocville",
            "Thothland",
            "Fort Aethon",
            "Siren Song",
            "Tengu Hills",
            "Strixport",
            "Eagleburg",
            "Flamingo Bay"
        };

        antTownNames = new List<string>
        {
            "Queen's Mound",
            "Formicia",
            "Antville",
            "Red Hill",
            "Leafcutter Heap",
            "Sugar Farm",
            "Paraponera City",
            "Myrmidon Point",
            "Bivouac Nest",
            "Fire Ant Fort",
            "Larvaburg",
            "Needlefields",
            "Flik's Town",
            "Lasius Rock"
        };

        frogTownNames = new List<string>
        {
            "Frogpolis",
            "Buzzing Bog",
            "Lily Pond",
            "Evermire",
            "Green Lake",
            "Toadburg",
            "Muddy Pool",
            "Flycatcher Swamp",
            "Tadpole City",
            "Misty Tarn",
            "Froppyville",
            "Willow Wetlands",
            "Loch Bufo",
            "Murky Puddle",
            "Soggy Marsh",
            "Backwater Borough",
            "Sawgrass Slough"
        };

        sharkTownNames = new List<string>
        {
            "Port Royal",
            "Tortuga",
            "New Providence",
            "Black Pearl",
            "Barbary Coast",
            "Isla De Muerta",
            "Barataria Bay",
            "Porto Farina",
            "Shipwreck Cove",
            "Dreaded Stronghold",
            "Jolly Roger",
            "Clew Bay",
            "Whydah",
            "Stormfort",
            "Kraken Keep",
            "Devil's Maw",
            "Sunken Trove",
            "Privateer's Lagoon",
            "Wanderer's Hideout",
            "Blackened Den"
        };

        deerTownNames = new List<string>
        {
            "Artemis Woods",
            "The Golden Stag",
            "Elkfurt",
            "Hoovechester",
            "Buckville",
            "The White Doe",
            "Cernunnos",
            "Dappled Hide",
            "Ceryneia",
            "Hindburg",
            "Peryton",
            "Antlertown",
            "Swiftbrook",
            "Red Hart",
            "Eikthyrnir",
            "Actaeon",
            "Darbywood",
            "Rohit",
            "Furfur",
            "Achlis"
        };
		goodraNames = new List<string>
        {
            "Goodra",
            "Sligoo",
            "Goomy",
            "Gooey",
            "Hugs",
            "Huggy",
            "Slimy",
            "Pudding",
            "Gumdrops",
            "Jelly",
            "Dragooze",
            "Escargoo",
            "Squishy",
            "Goober",
            "Oozy",
            "Goopy",
            "Nuri",
            "Dragoonite",
            "Spygoo",
            "Dragoo",
            "Gooigi",
        };
        miceTownNames = new List<string>
        {
            "Goldhollow",
            "Nibblewood",
            "Mousetail Meadow",
            "Squeakridge",
            "Cheesemere",
            "Furrowfield",
            "Silverwhisker",
            "Squeakstone",
            "Wheatenwood",
            "Thimbleholt",
            "Mousenook",
            "Cheddarcliff",
            "Cottagecreek",
            "Blueburrow",
            "Swisswood",
            "Rustle Warren",
            "Tanglewood",

        };
        matronsminionsTownNames = new List<string>
        {
            "Matron's Rest",
            "Giant's Pass",
            "Her Tranquility",
            "Aged Gate",
            "Fateland",
            "Grave of All",
            "Dragon's Lounge",
            "Beaten Path",
            "Lair of the Lady",
            "Ancestor's Pit",
        };
        lupineTownNames = new List<string>
        {
            "Denko", 
            "Zebooth", 
            "District 0", 
            "The Black Market", 
            "Fang City", 
            "Direfort", 
            "Howlburg", 
            "Bitetown", 
            "The Pack", 
            "Moonlight"
        };
        jackalTownNames = new List<string>
        {
            "The Shard",
            "Heliopolis",
            "First Oasis",
            "Ancient's Tomb",
            "Sandhaven",
            "Sandstone",
            "City of Gold",
            "The Grand Pyramid",
            "Judgement",
            "Golden Sands",
            "River's Edge",
            "Whispering Oasis",
            "Pillars of Gold",
            "Sunken Citadel",

        };
        gnollTownNames = new List<string>
        {
            "Meetin' Rock",
            "Huntin' Grounds",
            "Shalanzi",
            "Balaizi's Claim",
            "Corpsefeast",
            "Hyeeni's Cut",
            "Zefzi",
            "Breakbone Village",
            "The Boneyard",
            "Good Pickin's",
            "Oti' Koetzi",
            "Dark Place",
        };
        demiDragonTownNames = new List<string>
        {
            "Drakinia",
            "Nogard",
            "Stonescale",
            "Flamewesk",
            "Escale",
            "Sahrot Sahsun",
            "Vedviing",
            "Drakinheim",
            "Kapceasiin",
            "Drunyol",
            "Whetstane",
        };

        Encoding encoding = Encoding.GetEncoding("iso-8859-1");

        if (File.Exists($"{State.NameFileDirectory}males.txt"))
        {
            var logFile = File.ReadAllLines($"{State.NameFileDirectory}males.txt", encoding);
            if (logFile.Any())
                maleNames = new List<string>(logFile);
        }

        if (File.Exists($"{State.NameFileDirectory}females.txt"))
        {
            var logFile = File.ReadAllLines($"{State.NameFileDirectory}females.txt", encoding);
            if (logFile.Any())
                femaleNames = new List<string>(logFile);
        }

        if (File.Exists($"{State.NameFileDirectory}monsters.txt"))
        {
            var logFile = File.ReadAllLines($"{State.NameFileDirectory}monsters.txt", encoding);
            if (logFile.Any())
                monsterNames = new List<string>(logFile);
        }

        ArmyNames = new Dictionary<Race, List<string>>();
        ArmyNameDefault = new Dictionary<Race, string>();

        if (File.Exists($"{State.NameFileDirectory}armyNames.txt"))
        {
            var logFile = File.ReadAllLines($"{State.NameFileDirectory}armyNames.txt", encoding);
            bool expectingdefault = false;
            Race currentRace = Race.none;
            foreach (string entry in logFile)
            {
                if (entry.Length > 0)
                {
                    if (entry.StartsWith("The first entry"))
                        continue;
                    if (Enum.TryParse(entry, out Race race))
                    {
                        currentRace = (int)race >= 0 && Enum.IsDefined(typeof(Race), race) && !SpeciesPolicy.IsRetired(race) ? race : Race.none;
                        expectingdefault = true;
                    }
                    else if (currentRace == Race.none)
                    {
                        continue;
                    }
                    else if (expectingdefault)
                    {
                        ArmyNameDefault[currentRace] = entry;
                        expectingdefault = false;
                    }
                    else
                    {
                        if (ArmyNames.ContainsKey(currentRace))
                        {
                            ArmyNames[currentRace].Add(entry);
                        }
                        else
                        {
                            ArmyNames[currentRace] = new List<string>() { entry };
                        }
                    }
                }
            }
        }

        RaceMaleNames = new Dictionary<Race, List<string>>();
        RaceFemaleNames = new Dictionary<Race, List<string>>();
        RaceMonsterNames = new Dictionary<Race, List<string>>();

        foreach (Race race in ((Race[])Enum.GetValues(typeof(Race))).Where(r => (int)r >= 0 && !SpeciesPolicy.IsRetired(r)))
        {
            if (File.Exists($"{State.NameFileDirectory}male{race}.txt"))
            {
                var logFile = File.ReadAllLines($"{State.NameFileDirectory}male{race}.txt", encoding);
                var names = new List<string>(logFile);
                RaceMaleNames[race] = names;
            }
            if (File.Exists($"{State.NameFileDirectory}female{race}.txt"))
            {
                var logFile = File.ReadAllLines($"{State.NameFileDirectory}female{race}.txt", encoding);
                var names = new List<string>(logFile);
                RaceFemaleNames[race] = names;
            }
            if (File.Exists($"{State.NameFileDirectory}{race}.txt"))
            {
                var logFile = File.ReadAllLines($"{State.NameFileDirectory}{race}.txt", encoding);
                var names = new List<string>(logFile);
                RaceMonsterNames[race] = names;
            }
        }

    }

    public string GetArmyName(Race race, Village village)
    {
        if (SpeciesPolicy.IsRetired(race))
            return "Company";
        if (State.Rand.Next(10) == 0)
        {
            if (ArmyNames.ContainsKey(race))
            {
                List<string> items = new List<string>();
                foreach (var item in ArmyNames[race])
                {
                    items.Add(item);
                }
                var pick = items[State.Rand.Next(items.Count)];
                if (StrategicUtilities.GetAllArmies().Where(s => s.Name == pick).Any() == false)
                {
                    return pick;
                }
            }
        }
        if (village != null && ArmyNameDefault.ContainsKey(race))
        {
            for (int i = 0; i < 9; i++)
            {
                var name = $"{AddOrdinal(State.Rand.Next(1, 100))} {village.Name} {ArmyNameDefault[race]}";
                if (StrategicUtilities.GetAllArmies().Where(s => s.Name == name).Any() == false)
                    return name;
            }
            return $"{AddOrdinal(State.Rand.Next(1, 100))} {village.Name} {ArmyNameDefault[race]}";
        }
        return "";

        string AddOrdinal(int num)
        {
            if (num <= 0) return num.ToString();

            switch (num % 100)
            {
                case 11:
                case 12:
                case 13:
                    return num + "th";
            }

            switch (num % 10)
            {
                case 1:
                    return num + "st";
                case 2:
                    return num + "nd";
                case 3:
                    return num + "rd";
                default:
                    return num + "th";
            }

        }
    }

    public string GetMonsterName(bool male, Race race)
    {
        if (SpeciesPolicy.IsRetired(race))
            return "Unit";
        List<string> list;
        if (male)
        {
            RaceMaleNames.TryGetValue(race, out list);
            if (list != null && list.Any())
            {
                return list[State.Rand.Next(list.Count)];
            }
        }
        else
        {
            RaceFemaleNames.TryGetValue(race, out list);
            if (list != null && list.Any())
            {
                return list[State.Rand.Next(list.Count)];
            }
        }
        RaceMonsterNames.TryGetValue(race, out list);
        if (list != null && list.Any())
        {
            return list[State.Rand.Next(list.Count)];
        }
        list = null;
        switch (race)
        {
            case Race.Wyvern:
                list = wyvernNames;
                break;
            case Race.Serpents:
                list = serpentNames;
                break;
            case Race.Gryphons:
                list = gryphonNames;
                break;
			case Race.Goodra:
                list = goodraNames;
                break;
        }
        if (list != null)
        {
            if (list.Count > 20)
                return list[State.Rand.Next(list.Count)];
            int rand = State.Rand.Next(20);
            if (rand < list.Count)
                return list[rand];
        }
        return monsterNames[State.Rand.Next(monsterNames.Count)];

    }

    public string GetName(bool male, Race race)
    {
        if (SpeciesPolicy.IsRetired(race))
            return "Unit";

        int r;
        if (male)
        {
            RaceMaleNames.TryGetValue(race, out var list);
            if (list != null && list.Any())
            {
                return list[State.Rand.Next(list.Count)];
            }
            r = State.Rand.Next(maleNames.Count);
            return maleNames[r];
        }
        else
        {
            RaceFemaleNames.TryGetValue(race, out var list);
            if (list != null && list.Any())
            {
                return list[State.Rand.Next(list.Count)];
            }
            r = State.Rand.Next(femaleNames.Count);
            return femaleNames[r];
        }


    }

    public string GetTownName(Race race, int i)
    {
        if (SpeciesPolicy.IsRetired(race))
            return $"Town {i + 1}";
        if (race == Race.Bunnies)
        {
            if (i >= 0 && i < bunnyTownNames.Count)
                return bunnyTownNames[i];
        }
        else if (race == Race.Lizards)
        {
            if (i >= 0 && i < lizardTownNames.Count)
                return lizardTownNames[i];
        }
        else if (race == Race.Kangaroos)
        {
            if (i >= 0 && i < kangarooTownNames.Count)
                return kangarooTownNames[i];
        }
        else if (race == Race.Equines)
        {
            if (i >= 0 && i < equinesTownNames.Count)
                return equinesTownNames[i];
        }
        else if (race == Race.Sergal)
        {
            if (i >= 0 && i < sergalTownNames.Count)
                return sergalTownNames[i];
        }
        else if (race == Race.Bees)
        {
            if (i >= 0 && i < beeTownNames.Count)
                return beeTownNames[i];
        }
        else if (race == Race.Bats)
        {
            if (i >= 0 && i < batTownNames.Count)
                return batTownNames[i];
        }
        else if (race == Race.Panthers)
        {
            if (i >= 0 && i < pantherTownNames.Count)
                return pantherTownNames[i];
        }
        else if (race == Race.Avians)
        {
            if (i >= 0 && i < avianTownNames.Count)
                return avianTownNames[i];
        }
        else if (race == Race.Ants)
        {
            if (i >= 0 && i < antTownNames.Count)
                return antTownNames[i];
        }
        else if (race == Race.Frogs)
        {
            if (i >= 0 && i < frogTownNames.Count)
                return frogTownNames[i];
        }
        else if (race == Race.Sharks)
        {
            if (i >= 0 && i < sharkTownNames.Count)
                return sharkTownNames[i];
        }
        else if (race == Race.Deer)
        {
            if (i >= 0 && i < deerTownNames.Count)
                return deerTownNames[i];
        }        
        else if (race == Race.Mice)
        {
            if (i >= 0 && i < miceTownNames.Count)
                return miceTownNames[i];
        }
        else if (race == Race.MatronsMinions)
        {
            if (i >= 0 && i < matronsminionsTownNames.Count)
                return matronsminionsTownNames[i];
        }
        else if (race == Race.Lupine)
        {
            if (i >= 0 && i < lupineTownNames.Count)
                return lupineTownNames[i];
        }
        else if (race == Race.Jackals)
        {
            if (i >= 0 && i < jackalTownNames.Count)
                return jackalTownNames[i];
        }
        else if (race == Race.Gnolls)
        {
            if (i >= 0 && i < gnollTownNames.Count)
                return gnollTownNames[i];
        }
        else if (race == Race.Draconians)
        {
            if (i >= 0 && i < demiDragonTownNames.Count)
                return demiDragonTownNames[i];
        }



        return $"{race} town {i + 1}";

    }

    public string GetAlternateTownName(Race race, int i)
    {
        if (SpeciesPolicy.IsRetired(race))
            return $"Town {i + 1}";
        if (race == Race.Bunnies)
        {
            if (i >= 0 && i < bunnyPreyTownNames.Count)
                return bunnyPreyTownNames[i];
        }

        return $"{race} town {i + 1}";

    }

}
