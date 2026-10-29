# Species Curation Checklist

**Status:** Discovery and planning. No species implementation has changed.

## Intended species categories across both modes

- Disable the existing species roster at code level across both the mercenary campaign and Pure Tactical at the start of the conversion. Curate and re-enable species individually as the user selects them; remove some species from code as far as practical if the user rejects them entirely. The user will decide which species belong in each category later.
- Approved sapient species may be the company leader, mercenaries, enemies, or later random encounters.
- Non-sapient animal species may be enemies, later random encounters, or temporary magical summons. They cannot be company members.
- A global species-availability page, shared by both modes, lists only curated species. Players may disable any listed species so it does not appear in-game, but cannot enable species that have not been curated and added.
- Named unique characters from the current roster are removed from the conversion.
- This is a reference inventory, not a demand to approve the whole roster at once. The user wants to curate selected species one at a time and validate that each meets their goals. Do not guess which species to keep, re-enable, or remove. Record each approved species as sapient or animal; some may instead be removed from code as far as practical.
- Personal appeal is the primary selection criterion. The MVP should have a very small playable sapient cast, roughly one species for each of melee, ranged, and magic; add more over time. Start with a small animal roster and expand later.
- MVP choices must come from existing species. Keep strong natural specialties, but allow characters to choose any role. Add further species in development updates, not as in-campaign unlocks.

## Shortlist status

A trait-based trio of Avians, Taurus, and Mainland Elves was offered as an example and rejected by the user. It is not an approved MVP shortlist. Base the eventual roster on the user's personal favorites. The user does not want to classify the full roster up front; once a species is selected for curation, re-enable and validate it individually, organizing discussion in groups where useful.

The categories below reflect the legacy numeric groupings in `Assets/Scripts/Enums/Race.cs`, not the new sapient/animal distinction. Names and IDs below are copied from that enum. The `none` sentinel is not a species and is omitted. All entries are disabled. Names in the outright-removal section below are designated for removal; other entries remain pending individual curation. The table's “Pending” value indicates that a legacy entry has not yet been mapped to a new sapient/animal disposition. Legacy group placement does not determine whether a species is sapient or an animal.

## User-designated outright removals

**Recording only:** These names are designated for removal from the conversion to the fullest degree practical. No code or assets have been changed. Names are preserved as supplied; mapping them to their corresponding code and content can be done when implementation is authorized. These entries are no longer pending a keep/re-enable decision.

- Aabayx
- Abakhanskya
- Alraune
- Asura
- Auri
- Bears
- Bella
- BoomBunnies
- Cake
- Cats
- Centaur
- Cherub
- Cierihaka
- Cockatrice
- Collectors
- Compy
- Crux
- Crypters
- DarkSwallower
- DewSprites
- Dogs
- DRACO
- Driders
- EarthDryad
- Earthworms
- Eevee
- Equaleon
- Erin
- Fairies
- Feit
- FeralEevee
- FeralEqualeon
- FeralFrogs
- FeralOrcas
- FeralSharks
- Foxes
- FungalDryad
- Ghosts
- Goblins
- Hamsters
- Harpies
- Humans
- Iliijiith
- Imps
- Ki
- Konane
- Lamia
- MainlandElves
- Merfolk
- Nectar
- Olivia
- OoviKat
- Puca
- Pudding
- Raptor
- Renamon
- RiverDryad
- RwuMercenaries
- Ryan
- Salix
- Schiwardez
- Scorch
- Scylla
- Selicia
- Seraph
- Seville
- Singularity
- Skapa
- Slimes
- SoulSprite
- Succubi
- Taraluxia
- Tatltuae
- Taurus
- Tigers
- Umbreon
- Vagrants
- Vipers
- Viisels
- ViraeUltimae
- Vision
- Voilin
- Whisp
- Wolves
- WoodDryad
- Xelhilde
- Yordles
- Youko
- Zera
- Zoey

## Main-race range (0–40)

| ID | Existing entry | New disposition |
|---:|---|---|
| 0 | Cats | Pending |
| 1 | Dogs | Pending |
| 2 | Foxes | Pending |
| 3 | Wolves | Pending |
| 4 | Bunnies | Pending |
| 5 | Lizards | Pending |
| 6 | Slimes | Pending |
| 7 | Scylla | Pending |
| 8 | Harpies | Pending |
| 9 | Imps | Pending |
| 10 | Humans | Pending |
| 11 | Crypters | Pending |
| 12 | Lamia | Pending |
| 13 | Kangaroos | Pending |
| 14 | Taurus | Pending |
| 15 | Crux | Pending |
| 16 | Equines | Pending |
| 17 | Sergal | Pending |
| 18 | Bees | Pending |
| 19 | Driders | Pending |
| 20 | Alraune | Pending |
| 21 | Bats | Pending |
| 22 | Panthers | Pending |
| 23 | Merfolk | Pending |
| 24 | Avians | Pending |
| 25 | Ants | Pending |
| 26 | Frogs | Pending |
| 27 | Sharks | Pending |
| 28 | Deer | Pending |
| 29 | Aabayx | Pending |
| 30 | Mice | Pending |
| 31 | MatronsMinions | Pending |
| 32 | Gnolls | Pending |
| 33 | MainlandElves | Pending |
| 34 | Bears | Pending |
| 35 | Umbreon | Pending |
| 36 | Lupine | Pending |
| 37 | Jackals | Pending |
| 38 | Ghosts | Pending |
| 39 | Draconians | Pending |
| 40 | Yordles | Pending |

## Mercenary range (50–70)

| ID | Existing entry | New disposition |
|---:|---|---|
| 50 | Succubi | Pending |
| 51 | Tigers | Pending |
| 52 | Goblins | Pending |
| 53 | Alligators | Pending |
| 54 | Puca | Pending |
| 55 | Kobolds | Pending |
| 56 | DewSprites | Pending |
| 57 | Hippos | Pending |
| 58 | Vipers | Pending |
| 59 | Komodos | Pending |
| 60 | Cockatrice | Pending |
| 61 | Vargul | Pending |
| 62 | Youko | Pending |
| 63 | Hamsters | Pending |
| 64 | RwuMercenaries | Pending |
| 65 | Centaur | Pending |
| 66 | Eevee | Pending |
| 67 | Equaleon | Pending |
| 68 | Badgers | Pending |
| 69 | Renamon | Pending |
| 70 | OoviKat | Pending |

## Monster range (100–164)

| ID | Existing entry | New disposition |
|---:|---|---|
| 100 | Vagrants | Pending |
| 101 | Serpents | Pending |
| 102 | Wyvern | Pending |
| 103 | WyvernMatron | Pending |
| 104 | Compy | Pending |
| 105 | FeralSharks | Pending |
| 106 | FeralWolves | Pending |
| 107 | DarkSwallower | Pending |
| 108 | Cake | Pending |
| 109 | Harvesters | Pending |
| 110 | Collectors | Pending |
| 111 | Voilin | Pending |
| 112 | FeralBats | Pending |
| 113 | FeralFrogs | Pending |
| 114 | Dragon | Pending |
| 115 | Dragonfly | Pending |
| 116 | TwistedVines | Pending |
| 117 | Fairies | Pending |
| 118 | FeralAnts | Pending |
| 119 | Gryphons | Pending |
| 120 | SpitterSlugs | Pending |
| 121 | SpringSlugs | Pending |
| 122 | RockSlugs | Pending |
| 123 | CoralSlugs | Pending |
| 124 | Salamanders | Pending |
| 125 | Mantis | Pending |
| 126 | EasternDragon | Pending |
| 127 | Catfish | Pending |
| 128 | Raptor | Pending |
| 129 | WarriorAnts | Pending |
| 130 | Gazelle | Pending |
| 131 | Earthworms | Pending |
| 132 | FeralLizards | Pending |
| 133 | Monitors | Pending |
| 134 | Schiwardez | Pending |
| 135 | Terrorbird | Pending |
| 136 | Dratopyr | Pending |
| 137 | FeralLions | Pending |
| 138 | Goodra | Pending |
| 139 | Whisp | Pending |
| 140 | FeralHorses | Pending |
| 141 | FeralFox | Pending |
| 142 | Terminid | Pending |
| 143 | FeralOrcas | Pending |
| 144 | BoomBunnies | Pending |
| 145 | Otachi | Pending |
| 146 | FeralSlime | Pending |
| 147 | ViraeUltimae | Pending |
| 148 | Viisels | Pending |
| 149 | FeralEevee | Pending |
| 150 | FeralUmbreon | Pending |
| 151 | FeralEqualeon | Pending |
| 152 | Raiju | Pending |
| 153 | Smudger | Pending |
| 154 | SpaceCroach | Pending |
| 155 | WoodDryad | Pending |
| 156 | EarthDryad | Pending |
| 157 | RiverDryad | Pending |
| 158 | FungalDryad | Pending |
| 159 | Trex | Pending |
| 160 | Utahraptor | Pending |
| 161 | Iliijiith | Pending |
| 162 | Pudding | Pending |
| 163 | SoulSprite | Pending |
| 164 | BlackWidow | Pending |

## Named unique range (170–196)

The user wants these named unique characters removed from the conversion.

| ID | Existing entry | New disposition |
|---:|---|---|
| 170 | Selicia | Remove |
| 171 | Vision | Remove |
| 172 | Ki | Remove |
| 173 | Scorch | Remove |
| 174 | Asura | Remove |
| 175 | DRACO | Remove |
| 176 | Zoey | Remove |
| 177 | Cierihaka | Remove |
| 178 | Zera | Remove |
| 179 | Auri | Remove |
| 180 | Erin | Remove |
| 181 | Salix | Remove |
| 182 | Bella | Remove |
| 183 | Abakhanskya | Remove |
| 184 | Singularity | Remove |
| 185 | Feit | Remove |
| 186 | Taraluxia | Remove |
| 187 | Xelhilde | Remove |
| 188 | Olivia | Remove |
| 189 | Skapa | Remove |
| 190 | Tatltuae | Remove |
| 191 | Seville | Remove |
| 192 | Nectar | Remove |
| 193 | Ryan | Remove |
| 194 | Konane | Remove |
| 195 | Cherub | Remove |
| 196 | Seraph | Remove |
