# Rejected species: source and content map

**Packet:** 01B; 2026-10-30. Planning evidence only; no species disabled or removed.

**Subsequent work:** 03A/04 are accepted. [03B preflight](../implementation/03b-rejected-species-preflight.md) now rechecks the 90 mappings and provides a [current dependency matrix](../implementation/03b-rejected-species-dependency-matrix.md) and [shared resource inventory](../implementation/03b-shared-sprite-and-bundle-inventory.md). Original counts below are historical, not current closure evidence. No resource deletion/relocation is authorized.

Every supplied removal name is matched against the Race enum and its registry return/class. Code-file counts are lexical Race.Name references, not proof of runtime use or exclusive ownership. Bundle columns follow SpriteDictionary fields mentioned in the registry class to AssetBundleLoader assignments; a class may use shared clothing/graphics. Paths are manifest matches by normalized asset name, not inspection of binary bundle contents. Preserve numeric identities and audit shared dependencies before retiring files.

| User entry | Enum ID | Registry field / class source | Code files with Race.Name | Loader bundle groups | Manifest paths matched via loader |
|---|---:|---|---:|---|---|
| Aabayx | 29 | Aabayx / [Aabayx](../../Assets/Scripts/Races/Graphics/MainRaces/Aabayx.cs#L5) | 10 | empire | 5 |
| Abakhanskya | 183 | Abakhanskya / [Abakhanskya](../../Assets/Scripts/Races/Graphics/MainRaces/Abakhanskya.cs#L4) | 5 | specialmerc | 1 |
| Alraune | 20 | Alraune / [Alraune](../../Assets/Scripts/Races/Graphics/MainRaces/Alraune.cs#L6) | 8 | empire | 2 |
| Asura | 174 | Asura / [Asura](../../Assets/Scripts/Races/Graphics/UniqueMercs/Asura.cs#L5) | 8 | specialmerc | 2 |
| Auri | 179 | Aurilika / [Auri](../../Assets/Scripts/Races/Graphics/UniqueMercs/Auri.cs#L5) | 5 | empire, specialmerc | 4 |
| Bears | 34 | Bears / [Bears](../../Assets/Scripts/Races/Graphics/MainRaces/Bears.cs#L6) | 6 | empire | 2 |
| Bella | 182 | Bella / [Bella](../../Assets/Scripts/Races/Graphics/Monsters/Bella.cs#L8) | 3 | specialmerc | 8 |
| BoomBunnies | 144 | BoomBunnies / [BoomBunnies](../../Assets/Scripts/Races/Graphics/Monsters/BoomBunnies.cs#L4) | 7 | monster | 1 |
| Cake | 108 | Cake / [Cake](../../Assets/Scripts/Races/Graphics/Monsters/Cake.cs#L4) | 9 | monster | 1 |
| Cats | 0 | Cats / [Cats](../../Assets/Scripts/Races/Graphics/MainRaces/Cats.cs#L3) | 16 | no loader match | 0 |
| Centaur | 65 | Centaur / [Centaur](../../Assets/Scripts/Races/Graphics/MainRaces/Centaur.cs#L8) | 4 | empire, merc | 6 |
| Cherub | 195 | Cherub / [Cherub](../../Assets/Scripts/Races/Graphics/UniqueMercs/Cherub.cs#L4) | 7 | specialmerc | 1 |
| Cierihaka | 177 | Cierihaka / [Cierihaka](../../Assets/Scripts/Races/Graphics/UniqueMercs/Cierihaka.cs#L4) | 5 | specialmerc | 1 |
| Cockatrice | 60 | Cockatrice / [Cockatrice](../../Assets/Scripts/Races/Graphics/Mercs/Cockatrice.cs#L5) | 6 | empire, merc | 7 |
| Collectors | 110 | Collector / [Collector](../../Assets/Scripts/Races/Graphics/Monsters/Collector.cs#L4) | 8 | monster | 1 |
| Compy | 104 | Compy / [Compy](../../Assets/Scripts/Races/Graphics/Monsters/Compy.cs#L4) | 12 | monster | 1 |
| Crux | 15 | Crux / [Crux](../../Assets/Scripts/Races/Graphics/MainRaces/Crux.cs#L6) | 9 | empire | 1 |
| Crypters | 11 | Crypters / [Crypters](../../Assets/Scripts/Races/Graphics/MainRaces/Crypters.cs#L4) | 10 | empire | 1 |
| DarkSwallower | 107 | DarkSwallower / [DarkSwallower](../../Assets/Scripts/Races/Graphics/Monsters/DarkSwallower.cs#L4) | 8 | monster | 1 |
| DewSprites | 56 | DewSprite / [DewSprite](../../Assets/Scripts/Races/Graphics/Mercs/DewSprite.cs#L4) | 4 | merc | 1 |
| Dogs | 1 | Dogs / [Dogs](../../Assets/Scripts/Races/Graphics/MainRaces/Dogs.cs#L3) | 9 | no loader match | 0 |
| DRACO | 175 | DRACO / [DRACO](../../Assets/Scripts/Races/Graphics/UniqueMercs/DRACO.cs#L4) | 5 | specialmerc | 1 |
| Driders | 19 | Driders / [Driders](../../Assets/Scripts/Races/Graphics/MainRaces/Driders.cs#L6) | 7 | empire | 1 |
| EarthDryad | 156 | EarthDryad / [EarthDryad](../../Assets/Scripts/Races/Graphics/Monsters/EarthDryad.cs#L5) | 7 | empire, monster | 8 |
| Earthworms | 131 | Earthworms / [Earthworms](../../Assets/Scripts/Races/Graphics/Monsters/Earthworms.cs#L8) | 7 | monster | 1 |
| Eevee | 66 | Eevee / [Eevee](../../Assets/Scripts/Races/Graphics/MainRaces/Eevee.cs#L5) | 4 | empire, merc | 3 |
| Equaleon | 67 | Equaleon / [Equaleon](../../Assets/Scripts/Races/Graphics/MainRaces/Equaleon.cs#L5) | 6 | empire, merc | 4 |
| Erin | 180 | Erin / [Erin](../../Assets/Scripts/Races/Graphics/UniqueMercs/Erin.cs#L4) | 7 | specialmerc | 1 |
| Fairies | 117 | Fairy / [Fairy](../../Assets/Scripts/Races/Graphics/Monsters/Fairy.cs#L53) | 11 | monster | 3 |
| Feit | 185 | Feit / [Feit](../../Assets/Scripts/Races/Graphics/UniqueMercs/Feit.cs#L8) | 6 | specialmerc | 1 |
| FeralEevee | 149 | FeralEevee / [FeralEevee](../../Assets/Scripts/Races/Graphics/Monsters/FeralEevee.cs#L4) | 7 | monster | 1 |
| FeralEqualeon | 151 | FeralEqualeon / [FeralEqualeon](../../Assets/Scripts/Races/Graphics/Monsters/FeraEqualeon.cs#L4) | 7 | monster | 1 |
| FeralFrogs | 113 | Frogs / [Frogs](../../Assets/Scripts/Races/Graphics/Monsters/Frogs.cs#L4) | 9 | monster | 1 |
| FeralOrcas | 143 | FeralOrcas / [FeralOrcas](../../Assets/Scripts/Races/Graphics/Monsters/FeralOrcas.cs#L8) | 9 | monster | 1 |
| FeralSharks | 105 | Sharks / [Sharks](../../Assets/Scripts/Races/Graphics/Monsters/Sharks.cs#L7) | 10 | monster | 1 |
| Foxes | 2 | Foxes / [Foxes](../../Assets/Scripts/Races/Graphics/MainRaces/Foxes.cs#L3) | 11 | no loader match | 0 |
| FungalDryad | 158 | FungalDryad / [FungalDryad](../../Assets/Scripts/Races/Graphics/Monsters/FungalDryad.cs#L5) | 6 | empire, monster | 6 |
| Ghosts | 38 | Ghosts / [Ghosts](../../Assets/Scripts/Races/Graphics/MainRaces/Ghosts.cs#L5) | 6 | empire | 5 |
| Goblins | 52 | Goblins / [Goblins](../../Assets/Scripts/Races/Graphics/Mercs/Goblins.cs#L9) | 18 | empire, monster | 11 |
| Hamsters | 63 | Hamsters / [Hamsters](../../Assets/Scripts/Races/Graphics/Mercs/Hamsters.cs#L5) | 5 | merc | 1 |
| Harpies | 8 | Harpies / [Harpies](../../Assets/Scripts/Races/Graphics/MainRaces/Harpies.cs#L4) | 11 | empire | 1 |
| Humans | 10 | Humans / [Humans](../../Assets/Scripts/Races/Graphics/MainRaces/Humans.cs#L5) | 9 | empire | 15 |
| Iliijiith | 161 | Iliijiith / [Iliijiith](../../Assets/Scripts/Races/Graphics/Monsters/Iliijiith.cs#L4) | 12 | monster | 1 |
| Imps | 9 | Imps / [Imps](../../Assets/Scripts/Races/Graphics/MainRaces/Imps.cs#L9) | 19 | empire | 11 |
| Ki | 172 | Ki / [Ki](../../Assets/Scripts/Races/Graphics/UniqueMercs/Ki.cs#L5) | 7 | specialmerc | 1 |
| Konane | 194 | Konane / [Konane](../../Assets/Scripts/Races/Graphics/UniqueMercs/Konane.cs#L7) | 4 | specialmerc | 1 |
| Lamia | 12 | Lamia / [Lamia](../../Assets/Scripts/Races/Graphics/MainRaces/Lamia.cs#L5) | 14 | empire | 4 |
| MainlandElves | 33 | MainlandElves / [MainlandElves](../../Assets/Scripts/Races/Graphics/MainRaces/MainlandElves.cs#L5) | 6 | empire | 15 |
| Merfolk | 23 | Mermen / [Mermen](../../Assets/Scripts/Races/Graphics/MainRaces/Mermen.cs#L5) | 9 | empire | 2 |
| Nectar | 192 | Nectar / [Nectar](../../Assets/Scripts/Races/Graphics/UniqueMercs/Nectar.cs#L8) | 4 | specialmerc | 1 |
| Olivia | 188 | Olivia / [Olivia](../../Assets/Scripts/Races/Graphics/UniqueMercs/Olivia.cs#L5) | 5 | specialmerc | 2 |
| OoviKat | 70 | OoviKat / [OoviKat](../../Assets/Scripts/Races/Graphics/Mercs/OoviKat.cs#L5) | 4 | empire, merc | 17 |
| Puca | 54 | Puca / [Puca](../../Assets/Scripts/Races/Graphics/Mercs/Puca.cs#L5) | 5 | merc | 1 |
| Pudding | 162 | Pudding / [Pudding](../../Assets/Scripts/Races/Graphics/Monsters/Pudding.cs#L4) | 7 | monster | 1 |
| Raptor | 128 | Raptor / [Raptor](../../Assets/Scripts/Races/Graphics/Monsters/Raptor.cs#L4) | 6 | monster | 1 |
| Renamon | 69 | Renamon / [Renamon](../../Assets/Scripts/Races/Graphics/Mercs/Renamon.cs#L5) | 8 | merc | 5 |
| RiverDryad | 157 | RiverDryad / [RiverDryad](../../Assets/Scripts/Races/Graphics/Monsters/RiverDryad.cs#L5) | 7 | empire, monster | 8 |
| RwuMercenaries | 64 | RwuMercenaries / [RwuMercenaries](../../Assets/Scripts/Races/Graphics/Mercs/RwuMercenaries.cs#L4) | 8 | merc | 9 |
| Ryan | 193 | Ryan / [Ryan](../../Assets/Scripts/Races/Graphics/UniqueMercs/Ryan.cs#L4) | 6 | specialmerc | 2 |
| Salix | 181 | Salix / [Salix](../../Assets/Scripts/Races/Graphics/UniqueMercs/Salix.cs#L8) | 4 | specialmerc | 3 |
| Schiwardez | 134 | Schiwardez / [Schiwardez](../../Assets/Scripts/Races/Graphics/Monsters/Schiwardez.cs#L4) | 8 | monster | 1 |
| Scorch | 173 | Scorch / [Scorch](../../Assets/Scripts/Races/Graphics/UniqueMercs/Scorch.cs#L4) | 4 | specialmerc | 1 |
| Scylla | 7 | Scylla / [Scylla](../../Assets/Scripts/Races/Graphics/MainRaces/Scylla.cs#L4) | 9 | empire | 1 |
| Selicia | 170 | Selicia / [Selicia](../../Assets/Scripts/Races/Graphics/UniqueMercs/Selicia.cs#L4) | 151 | specialmerc | 1 |
| Seraph | 196 | Seraph / [Seraph](../../Assets/Scripts/Races/Graphics/UniqueMercs/Seraph.cs#L4) | 8 | specialmerc | 1 |
| Seville | 191 | Seville / [Seville](../../Assets/Scripts/Races/Graphics/UniqueMercs/Seville.cs#L4) | 8 | specialmerc | 1 |
| Singularity | 184 | Singularity / [Singularity](../../Assets/Scripts/Races/Graphics/UniqueMercs/Singularity.cs#L9) | 4 | specialmerc | 2 |
| Skapa | 189 | Skapa / [Skapa](../../Assets/Scripts/Races/Graphics/UniqueMercs/Skapa.cs#L8) | 4 | specialmerc | 1 |
| Slimes | 6 | Slimes / [Slimes](../../Assets/Scripts/Races/Graphics/MainRaces/Slimes.cs#L4) | 18 | empire | 1 |
| SoulSprite | 163 | SoulSprite / [SoulSprite](../../Assets/Scripts/Races/Graphics/Monsters/SoulSprite.cs#L4) | 10 | monster | 1 |
| Succubi | 50 | Succubi / [Succubi](../../Assets/Scripts/Races/Graphics/Mercs/Succubi.cs#L5) | 20 | merc | 1 |
| Taraluxia | 186 | Taraluxia / [Taraluxia](../../Assets/Scripts/Races/Graphics/UniqueMercs/Taraluxia.cs#L8) | 7 | specialmerc | 2 |
| Tatltuae | 190 | Tatltuae / [Tatltuae](../../Assets/Scripts/Races/Graphics/UniqueMercs/Tatltuae.cs#L5) | 14 | specialmerc | 2 |
| Taurus | 14 | Cows / [Taurus](../../Assets/Scripts/Races/Graphics/MainRaces/Taurus.cs#L6) | 10 | empire | 1 |
| Tigers | 51 | Tigers / [Tigers](../../Assets/Scripts/Races/Graphics/Mercs/Tigers.cs#L3) | 14 | no loader match | 0 |
| Umbreon | 35 | Umbreon / [Umbreon](../../Assets/Scripts/Races/Graphics/MainRaces/Umbreon.cs#L5) | 10 | empire | 4 |
| Vagrants | 100 | Vagrants / [Vagrants](../../Assets/Scripts/Races/Graphics/Monsters/Vagrants.cs#L4) | 26 | monster | 3 |
| Vipers | 58 | Vipers / [Vipers](../../Assets/Scripts/Races/Graphics/Mercs/Vipers.cs#L5) | 7 | merc | 4 |
| Viisels | 148 | Viisels / [Viisels](../../Assets/Scripts/Races/Graphics/Mercs/Viisels.cs#L4) | 7 | monster | 1 |
| ViraeUltimae | 147 | ViraeUltimae / [ViraeUltimae](../../Assets/Scripts/Races/Graphics/Monsters/ViraeUltimae.cs#L4) | 12 | monster | 1 |
| Vision | 171 | Vision / [Vision](../../Assets/Scripts/Races/Graphics/UniqueMercs/Vision.cs#L4) | 7 | specialmerc | 1 |
| Voilin | 111 | Voilin / [Voilin](../../Assets/Scripts/Races/Graphics/Monsters/Voilin.cs#L4) | 7 | monster | 1 |
| Whisp | 139 | Whisp / [Whisp](../../Assets/Scripts/Races/Graphics/Monsters/Whisp.cs#L4) | 5 | monster | 1 |
| Wolves | 3 | Wolves / [Wolves](../../Assets/Scripts/Races/Graphics/MainRaces/Wolves.cs#L3) | 9 | no loader match | 0 |
| WoodDryad | 155 | WoodDryad / [WoodDryad](../../Assets/Scripts/Races/Graphics/Monsters/WoodDryad.cs#L5) | 8 | empire, monster | 8 |
| Xelhilde | 187 | Xelhilde / [Xelhilde](../../Assets/Scripts/Races/Graphics/UniqueMercs/Xelhilde.cs#L4) | 6 | specialmerc | 1 |
| Yordles | 40 | Yordles / [Yordles](../../Assets/Scripts/Races/Graphics/MainRaces/Yordles.cs#L6) | 5 | empire | 9 |
| Youko | 62 | Youko / [Youko](../../Assets/Scripts/Races/Graphics/Mercs/Youko.cs#L5) | 11 | empire | 9 |
| Zera | 178 | Zera / [Zera](../../Assets/Scripts/Races/Graphics/UniqueMercs/Zera.cs#L5) | 6 | specialmerc | 4 |
| Zoey | 176 | Zoey / [Zoey](../../Assets/Scripts/Races/Graphics/UniqueMercs/Zoey.cs#L6) | 6 | specialmerc | 2 |

## Interpretation and additional dependencies

An enum ID, named class, or bundle reference does not define the new sapient/animal classification. Remaining entries are unapproved until individually curated. Named unique entries are retired as a category as already agreed. No replacement species have been selected.

Races.GetRace(Unit) also returns SlimeQueen and AntQueen variants for leaders; deleting only the base species class would miss this relationship. Races.GetRace(Race) returns BlankSlate for unknown IDs, which must not silently replace an ineligible species in the conversion. The bundle groups are mixed containers, not per-species packages.

## Manifest paths reached by rejected class loader fields

These are candidate dependency paths; they are not an approved deletion list and can include assets shared with retained species.

### Aabayx
- Assets/Sprites/Units/Aabayx.png
- Assets/Sprites/Units/AabayxFacePaint.png
- Assets/Sprites/Units/AabayxHighPriest.png
- Assets/Sprites/Units/humanrework/humansBodySprites4.png
- Assets/Sprites/Units/humanrework/humansVoreSprites.png

### Abakhanskya
- Assets/Sprites/Units/Abakhanskya.png

### Alraune
- Assets/Sprites/Units/Alraune.png
- Assets/Sprites/Units/alraunechristmas.png

### Asura
- Assets/Sprites/Units/Asura.png
- Assets/Sprites/Units/AsuraHoliday.png

### Auri
- Assets/Sprites/Units/Auri.png
- Assets/Sprites/Units/AuriVore.png
- Assets/Sprites/Units/AuriXmasAddSheet.png
- Assets/Sprites/Units/Panther/panther hats.png

### Bears
- Assets/Sprites/Units/Bears.png
- Assets/Sprites/Units/BearsClothes.png

### Bella
- Assets/Sprites/Units/Bella/BellaArm1.png
- Assets/Sprites/Units/Bella/BellaArm2.png
- Assets/Sprites/Units/Bella/BellaBody1.png
- Assets/Sprites/Units/Bella/BellaBody2.png
- Assets/Sprites/Units/Bella/BellaHead1.png
- Assets/Sprites/Units/Bella/BellaHead2.png
- Assets/Sprites/Units/Bella/BellaRobes1.png
- Assets/Sprites/Units/Bella/BellaRobes2.png

### BoomBunnies
- Assets/Sprites/Units/Boom_Bunnies.png

### Cake
- Assets/Sprites/Units/Cake.png

### Centaur
- Assets/Sprites/Units/CentaurParts.png
- Assets/Sprites/Units/CentaurTorsoAddOns.png
- Assets/Sprites/Units/TaurTorso.png
- Assets/Sprites/Units/humanrework/hu_men_F_undertops.png
- Assets/Sprites/Units/humanrework/hu_men_M_undertops.png
- Assets/Sprites/Units/humanrework/humansVoreSprites.png

### Cherub
- Assets/Sprites/Units/Cherub/Cherub.png

### Cierihaka
- Assets/Sprites/Units/Cierihaka.png

### Cockatrice
- Assets/Sprites/Units/Avians3.png
- Assets/Sprites/Units/Avians4.png
- Assets/Sprites/Units/Cockatrice1.png
- Assets/Sprites/Units/Cockatrice2.png
- Assets/Sprites/Units/Cockatrice3.png
- Assets/Sprites/Units/Sharks3.png
- Assets/Sprites/Units/Sharks5.png

### Collectors
- Assets/Sprites/Units/Collector.png

### Compy
- Assets/Sprites/Units/Compy.png

### Crux
- Assets/Sprites/Units/CruxBodySprites1.png

### Crypters
- Assets/Sprites/Units/crypters.png

### DarkSwallower
- Assets/Sprites/Units/DarkSwallower.png

### DewSprites
- Assets/Sprites/Units/dew.png

### DRACO
- Assets/Sprites/Units/DRACO.png

### Driders
- Assets/Sprites/Units/Drider.png

### EarthDryad
- Assets/Sprites/Units/Dryad/Dryad1.png
- Assets/Sprites/Units/Dryad/Dryad2.png
- Assets/Sprites/Units/Dryad/Dryad3.png
- Assets/Sprites/Units/Dryad/Dryad4.png
- Assets/Sprites/Units/Dryad/Dryad5.png
- Assets/Sprites/Units/humanrework/humansBodySprites2.png
- Assets/Sprites/Units/humanrework/humansBodySprites3.png
- Assets/Sprites/Units/humanrework/humansVoreSprites.png

### Earthworms
- Assets/Sprites/Units/Earthworms.png

### Eevee
- Assets/Sprites/Units/Eeveelution/AnthroEevee.png
- Assets/Sprites/Units/Eeveelution/AnthroUmbreon2.png
- Assets/Sprites/Units/humanrework/humansVoreSprites.png

### Equaleon
- Assets/Sprites/Units/Eeveelution/AnthroEevee.png
- Assets/Sprites/Units/Eeveelution/AnthroEqualeon.png
- Assets/Sprites/Units/Eeveelution/AnthroUmbreon2.png
- Assets/Sprites/Units/humanrework/humansVoreSprites.png

### Erin
- Assets/Sprites/Units/Erin.png

### Fairies
- Assets/Sprites/Units/fairy240.png
- Assets/Sprites/Units/fairySmall.png
- Assets/Sprites/Units/fairy_addsheet.png

### Feit
- Assets/Sprites/Units/Feit.png

### FeralEevee
- Assets/Sprites/Units/Eeveelution/FeralEevee.png

### FeralEqualeon
- Assets/Sprites/Units/Eeveelution/FeralEqualeon.png

### FeralFrogs
- Assets/Sprites/Units/Frog.png

### FeralOrcas
- Assets/Sprites/Units/Feral_Orcas.png

### FeralSharks
- Assets/Sprites/Units/SkyShark.png

### FungalDryad
- Assets/Sprites/Units/Dryad/Dryad1.png
- Assets/Sprites/Units/Dryad/Dryad2.png
- Assets/Sprites/Units/Dryad/Dryad3.png
- Assets/Sprites/Units/Dryad/Dryad4.png
- Assets/Sprites/Units/Dryad/Dryad5.png
- Assets/Sprites/Units/humanrework/humansBodySprites3.png

### Ghosts
- Assets/Sprites/Units/ghosts1.png
- Assets/Sprites/Units/ghosts2.png
- Assets/Sprites/Units/ghosts3.png
- Assets/Sprites/Units/ghostvorebits.png
- Assets/Sprites/Units/humanrework/humansBodySprites4.png

### Goblins
- Assets/Sprites/Units/goblins/gobbglove.png
- Assets/Sprites/Units/goblins/gobbo.png
- Assets/Sprites/Units/goblins/gobbohat.png
- Assets/Sprites/Units/goblins/gobboverbottoms.png
- Assets/Sprites/Units/goblins/gobboveronepieces.png
- Assets/Sprites/Units/goblins/gobbovertops.png
- Assets/Sprites/Units/goblins/gobbovore.png
- Assets/Sprites/Units/goblins/gobbunderbottoms.png
- Assets/Sprites/Units/goblins/gobbunderonepieces.png
- Assets/Sprites/Units/goblins/gobleggo.png
- Assets/Sprites/Units/goblins/gobundertops.png

### Hamsters
- Assets/Sprites/Units/Hamsters.png

### Harpies
- Assets/Sprites/Units/harpies.png

### Humans
- Assets/Sprites/Units/humanrework/Hu_men_uniform_1.png
- Assets/Sprites/Units/humanrework/HumanRags.png
- Assets/Sprites/Units/humanrework/hu_men_F_undertops.png
- Assets/Sprites/Units/humanrework/hu_men_M_undertops.png
- Assets/Sprites/Units/humanrework/hu_men_bigloin.png
- Assets/Sprites/Units/humanrework/hu_men_one_pieces.png
- Assets/Sprites/Units/humanrework/hu_men_pants.png
- Assets/Sprites/Units/humanrework/hu_men_skirt.png
- Assets/Sprites/Units/humanrework/hu_men_underbottoms.png
- Assets/Sprites/Units/humanrework/hu_men_uniform_2.png
- Assets/Sprites/Units/humanrework/humansBodySprites1.png
- Assets/Sprites/Units/humanrework/humansBodySprites2.png
- Assets/Sprites/Units/humanrework/humansBodySprites3.png
- Assets/Sprites/Units/humanrework/humansBodySprites4.png
- Assets/Sprites/Units/humanrework/humansVoreSprites.png

### Iliijiith
- Assets/Sprites/Units/Iliijiith.png

### Imps
- Assets/Sprites/Units/imps/Impfinished.png
- Assets/Sprites/Units/imps/impfinishedvore.png
- Assets/Sprites/Units/imps/impgloves.png
- Assets/Sprites/Units/imps/imphood.png
- Assets/Sprites/Units/imps/implegg.png
- Assets/Sprites/Units/imps/impoverbottoms.png
- Assets/Sprites/Units/imps/impoveronepiece.png
- Assets/Sprites/Units/imps/impovertops.png
- Assets/Sprites/Units/imps/impunderbottoms.png
- Assets/Sprites/Units/imps/impunderonepieces.png
- Assets/Sprites/Units/imps/impundertops.png

### Ki
- Assets/Sprites/Units/Ki.png

### Konane
- Assets/Sprites/Units/Konane.png

### Lamia
- Assets/Sprites/Units/Lamia.png
- Assets/Sprites/Units/LamiaNaga.png
- Assets/Sprites/Units/LamiaNagaBits.png
- Assets/Sprites/Units/Scylla.png

### MainlandElves
- Assets/Sprites/Units/Elves/elven_armor.png
- Assets/Sprites/Units/MainlandElves.png
- Assets/Sprites/Units/humanrework/Hu_men_uniform_1.png
- Assets/Sprites/Units/humanrework/hu_men_F_undertops.png
- Assets/Sprites/Units/humanrework/hu_men_M_undertops.png
- Assets/Sprites/Units/humanrework/hu_men_bigloin.png
- Assets/Sprites/Units/humanrework/hu_men_one_pieces.png
- Assets/Sprites/Units/humanrework/hu_men_pants.png
- Assets/Sprites/Units/humanrework/hu_men_skirt.png
- Assets/Sprites/Units/humanrework/hu_men_underbottoms.png
- Assets/Sprites/Units/humanrework/humansBodySprites1.png
- Assets/Sprites/Units/humanrework/humansBodySprites2.png
- Assets/Sprites/Units/humanrework/humansBodySprites3.png
- Assets/Sprites/Units/humanrework/humansBodySprites4.png
- Assets/Sprites/Units/humanrework/humansVoreSprites.png

### Merfolk
- Assets/Sprites/Units/Mermen.png
- Assets/Sprites/Units/Mermen2.png

### Nectar
- Assets/Sprites/Units/Nectar.png

### Olivia
- Assets/Sprites/Units/Olivia.png
- Assets/Sprites/Units/OliviaVoreSprites.png

### OoviKat
- Assets/Sprites/Units/OoviKatCustom.png
- Assets/Sprites/Units/OoviKatFemale.png
- Assets/Sprites/Units/OoviKatMale.png
- Assets/Sprites/Units/humanrework/Hu_men_uniform_1.png
- Assets/Sprites/Units/humanrework/hu_men_F_undertops.png
- Assets/Sprites/Units/humanrework/hu_men_M_undertops.png
- Assets/Sprites/Units/humanrework/hu_men_bigloin.png
- Assets/Sprites/Units/humanrework/hu_men_one_pieces.png
- Assets/Sprites/Units/humanrework/hu_men_pants.png
- Assets/Sprites/Units/humanrework/hu_men_skirt.png
- Assets/Sprites/Units/humanrework/hu_men_underbottoms.png
- Assets/Sprites/Units/humanrework/hu_men_uniform_2.png
- Assets/Sprites/Units/humanrework/humansBodySprites1.png
- Assets/Sprites/Units/humanrework/humansBodySprites2.png
- Assets/Sprites/Units/humanrework/humansBodySprites3.png
- Assets/Sprites/Units/humanrework/humansBodySprites4.png
- Assets/Sprites/Units/humanrework/humansVoreSprites.png

### Puca
- Assets/Sprites/Units/puca.png

### Pudding
- Assets/Sprites/Units/Pudding.png

### Raptor
- Assets/Sprites/Units/RaptorSprites.png

### Renamon
- Assets/Sprites/Units/RenamonBodies.png
- Assets/Sprites/Units/RenamonClothes1.png
- Assets/Sprites/Units/RenamonClothes2.png
- Assets/Sprites/Units/RenamonClothes3.png
- Assets/Sprites/Units/RenamonVore.png

### RiverDryad
- Assets/Sprites/Units/Dryad/Dryad1.png
- Assets/Sprites/Units/Dryad/Dryad2.png
- Assets/Sprites/Units/Dryad/Dryad3.png
- Assets/Sprites/Units/Dryad/Dryad4.png
- Assets/Sprites/Units/Dryad/Dryad5.png
- Assets/Sprites/Units/humanrework/humansBodySprites2.png
- Assets/Sprites/Units/humanrework/humansBodySprites3.png
- Assets/Sprites/Units/humanrework/humansVoreSprites.png

### RwuMercenaries
- Assets/Sprites/Units/RwuMercenaries/RwuMercenariesLizard.png
- Assets/Sprites/Units/RwuMercenaries/RwuMercenariesLizardArmor1.png
- Assets/Sprites/Units/RwuMercenaries/RwuMercenariesMice.png
- Assets/Sprites/Units/RwuMercenaries/RwuMercenariesMiceArmor1.png
- Assets/Sprites/Units/RwuMercenaries/RwuMercenariesShark.png
- Assets/Sprites/Units/RwuMercenaries/RwuMercenariesSharkArmor1.png
- Assets/Sprites/Units/RwuMercenaries/RwuMercenariesSharkArmor2.png
- Assets/Sprites/Units/RwuMercenaries/RwuMercenariesStandard.png
- Assets/Sprites/Units/RwuMercenaries/RwuMercenariesStandardArmor1.png

### Ryan
- Assets/Sprites/Units/Ryan.png
- Assets/Sprites/Units/RyanVore.png

### Salix
- Assets/Sprites/Units/Salix.png
- Assets/Sprites/Units/SalixGen.png
- Assets/Sprites/Units/SalixVore.png

### Schiwardez
- Assets/Sprites/Units/Schiwardez.png

### Scorch
- Assets/Sprites/Units/Scorch.png

### Scylla
- Assets/Sprites/Units/Scylla.png

### Selicia
- Assets/Sprites/Units/Selicia.png

### Seraph
- Assets/Sprites/Units/Seraph/Seraph.png

### Seville
- Assets/Sprites/Units/Seville.png

### Singularity
- Assets/Sprites/Units/Singularity1.png
- Assets/Sprites/Units/Singularity2.png

### Skapa
- Assets/Sprites/Units/Skapa.png

### Slimes
- Assets/Sprites/Units/Slimes.png

### SoulSprite
- Assets/Sprites/Units/SoulSprite/SoulSprite.png

### Succubi
- Assets/Sprites/Units/succubus.png

### Taraluxia
- Assets/Sprites/Units/TaraTailStrikes.png
- Assets/Sprites/Units/Taraluxia.png

### Tatltuae
- Assets/Sprites/Units/Tatltuae.png
- Assets/Sprites/Units/TatltuaeHackleVore.png

### Taurus
- Assets/Sprites/Units/cows.png

### Umbreon
- Assets/Sprites/Units/Eeveelution/AnthroUmbreon.png
- Assets/Sprites/Units/Eeveelution/AnthroUmbreon2.png
- Assets/Sprites/Units/Eeveelution/AnthroUmbreon3.png
- Assets/Sprites/Units/humanrework/humansVoreSprites.png

### Vagrants
- Assets/Sprites/Units/Vagrants.png
- Assets/Sprites/Units/vagrants2.png
- Assets/Sprites/Units/vagrants3.png

### Vipers
- Assets/Sprites/Units/Vipers1.png
- Assets/Sprites/Units/Vipers2.png
- Assets/Sprites/Units/Vipers3.png
- Assets/Sprites/Units/Vipers4.png

### Viisels
- Assets/Sprites/Units/Viisels.png

### ViraeUltimae
- Assets/Sprites/Units/ViraeUltimae.png

### Vision
- Assets/Sprites/Units/Vision.png

### Voilin
- Assets/Sprites/Units/Voilin.png

### Whisp
- Assets/Sprites/Units/Whisp.png

### WoodDryad
- Assets/Sprites/Units/Dryad/Dryad1.png
- Assets/Sprites/Units/Dryad/Dryad2.png
- Assets/Sprites/Units/Dryad/Dryad3.png
- Assets/Sprites/Units/Dryad/Dryad4.png
- Assets/Sprites/Units/Dryad/Dryad5.png
- Assets/Sprites/Units/humanrework/humansBodySprites2.png
- Assets/Sprites/Units/humanrework/humansBodySprites3.png
- Assets/Sprites/Units/humanrework/humansVoreSprites.png

### Xelhilde
- Assets/Sprites/Units/Xelhilde1.png

### Yordles
- Assets/Sprites/Units/Avians3.png
- Assets/Sprites/Units/Avians4.png
- Assets/Sprites/Units/Yordles/YordleBodies.png
- Assets/Sprites/Units/Yordles/YordleClothes1.png
- Assets/Sprites/Units/Yordles/YordleClothes2.png
- Assets/Sprites/Units/Yordles/YordleCustomization1.png
- Assets/Sprites/Units/Yordles/YordleCustomization2.png
- Assets/Sprites/Units/Yordles/YordleVoreFur.png
- Assets/Sprites/Units/Yordles/YordleVoreSkin.png

### Youko
- Assets/Sprites/Units/YoukoTails.png
- Assets/Sprites/Units/humanrework/Hu_men_uniform_1.png
- Assets/Sprites/Units/humanrework/hu_men_F_undertops.png
- Assets/Sprites/Units/humanrework/hu_men_M_undertops.png
- Assets/Sprites/Units/humanrework/hu_men_one_pieces.png
- Assets/Sprites/Units/humanrework/hu_men_uniform_2.png
- Assets/Sprites/Units/humanrework/humansBodySprites2.png
- Assets/Sprites/Units/humanrework/humansBodySprites3.png
- Assets/Sprites/Units/humanrework/humansVoreSprites.png

### Zera
- Assets/Sprites/Units/zera/1x1_bols.png
- Assets/Sprites/Units/zera/Front Belly 2x1.png
- Assets/Sprites/Units/zera/zera240.png
- Assets/Sprites/Units/zera/zerabel.png

### Zoey
- Assets/Sprites/Units/zoey.png
- Assets/Sprites/Units/zoey_addsheet.png
