# 03B — current rejected-species dependency matrix

**Date:** 2026-10-30. Documentation-only preflight after accepted 04B2. All 90 rows match the explicit retired set in SpeciesPolicy and the existing enum-to-field/class registry mapping. No species is curated by exclusion from this table.

Counts are lexical source candidates, including comments and dormant paths. They do not prove live reachability or asset exclusivity. Sprite columns count direct SpriteDictionary tokens in the named class; inherited/generic/base-class and serialized references require the linked preflight review. Other-consumer counts mean any source outside the 90 named definition files, including shared helpers; they do not mean a currently playable species. No file removal is authorized.

| Species / saved ID | Registry field / source class | Direct base type | Race enum reference files | Typed field consumer files | Direct sprite fields / with other consumers | Bundle groups from those fields | Seeded name files matched by enum name |
|---|---|---|---:|---:|---:|---|---|
| Aabayx / 29 | `Aabayx` / [Assets/Scripts/Races/Graphics/MainRaces/Aabayx.cs](../../Assets/Scripts/Races/Graphics/MainRaces/Aabayx.cs) | `DefaultRaceData` | 9 | 1 | 5 / 3 | empire | femaleAabayx, maleAabayx |
| Abakhanskya / 183 | `Abakhanskya` / [Assets/Scripts/Races/Graphics/MainRaces/Abakhanskya.cs](../../Assets/Scripts/Races/Graphics/MainRaces/Abakhanskya.cs) | `BlankSlate` | 5 | 0 | 1 / 0 | specialmerc | none by this rule |
| Alraune / 20 | `Alraune` / [Assets/Scripts/Races/Graphics/MainRaces/Alraune.cs](../../Assets/Scripts/Races/Graphics/MainRaces/Alraune.cs) | `DefaultRaceData` | 8 | 0 | 3 / 1 | empire | femaleAlraune, maleAlraune |
| Asura / 174 | `Asura` / [Assets/Scripts/Races/Graphics/UniqueMercs/Asura.cs](../../Assets/Scripts/Races/Graphics/UniqueMercs/Asura.cs) | `BlankSlate` | 8 | 0 | 2 / 1 | specialmerc | none by this rule |
| Auri / 179 | `Aurilika` / [Assets/Scripts/Races/Graphics/UniqueMercs/Auri.cs](../../Assets/Scripts/Races/Graphics/UniqueMercs/Auri.cs) | `DefaultRaceData` | 5 | 1 | 4 / 1 | empire, specialmerc | none by this rule |
| Bears / 34 | `Bears` / [Assets/Scripts/Races/Graphics/MainRaces/Bears.cs](../../Assets/Scripts/Races/Graphics/MainRaces/Bears.cs) | `DefaultRaceData` | 7 | 0 | 5 / 3 | empire | femaleBears, maleBears |
| Bella / 182 | `Bella` / [Assets/Scripts/Races/Graphics/Monsters/Bella.cs](../../Assets/Scripts/Races/Graphics/Monsters/Bella.cs) | `BlankSlate` | 4 | 0 | 8 / 0 | specialmerc | none by this rule |
| BoomBunnies / 144 | `BoomBunnies` / [Assets/Scripts/Races/Graphics/Monsters/BoomBunnies.cs](../../Assets/Scripts/Races/Graphics/Monsters/BoomBunnies.cs) | `DefaultRaceData` | 6 | 0 | 1 / 0 | monster | femaleBoomBunnies, maleBoomBunnies |
| Cake / 108 | `Cake` / [Assets/Scripts/Races/Graphics/Monsters/Cake.cs](../../Assets/Scripts/Races/Graphics/Monsters/Cake.cs) | `BlankSlate` | 7 | 0 | 1 / 0 | monster | Cake |
| Cats / 0 | `Cats` / [Assets/Scripts/Races/Graphics/MainRaces/Cats.cs](../../Assets/Scripts/Races/Graphics/MainRaces/Cats.cs) | `DefaultRaceData` | 14 | 0 | 2 / 1 | generic/inherited/none | femaleCats, maleCats |
| Centaur / 65 | `Centaur` / [Assets/Scripts/Races/Graphics/MainRaces/Centaur.cs](../../Assets/Scripts/Races/Graphics/MainRaces/Centaur.cs) | `TaurHumanHalf` | 5 | 1 | 6 / 5 | empire, merc | femaleCentaur, maleCentaur |
| Cherub / 195 | `Cherub` / [Assets/Scripts/Races/Graphics/UniqueMercs/Cherub.cs](../../Assets/Scripts/Races/Graphics/UniqueMercs/Cherub.cs) | `BlankSlate` | 6 | 0 | 1 / 0 | specialmerc | none by this rule |
| Cierihaka / 177 | `Cierihaka` / [Assets/Scripts/Races/Graphics/UniqueMercs/Cierihaka.cs](../../Assets/Scripts/Races/Graphics/UniqueMercs/Cierihaka.cs) | `BlankSlate` | 5 | 0 | 1 / 0 | specialmerc | none by this rule |
| Cockatrice / 60 | `Cockatrice` / [Assets/Scripts/Races/Graphics/Mercs/Cockatrice.cs](../../Assets/Scripts/Races/Graphics/Mercs/Cockatrice.cs) | `DefaultRaceData` | 5 | 2 | 7 / 6 | empire, merc | femaleCockatrice, maleCockatrice |
| Collectors / 110 | `Collector` / [Assets/Scripts/Races/Graphics/Monsters/Collector.cs](../../Assets/Scripts/Races/Graphics/Monsters/Collector.cs) | `BlankSlate` | 8 | 0 | 1 / 0 | monster | Collectors |
| Compy / 104 | `Compy` / [Assets/Scripts/Races/Graphics/Monsters/Compy.cs](../../Assets/Scripts/Races/Graphics/Monsters/Compy.cs) | `BlankSlate` | 10 | 0 | 1 / 0 | monster | Compy |
| Crux / 15 | `Crux` / [Assets/Scripts/Races/Graphics/MainRaces/Crux.cs](../../Assets/Scripts/Races/Graphics/MainRaces/Crux.cs) | `DefaultRaceData` | 9 | 0 | 1 / 1 | empire | femaleCrux, maleCrux |
| Crypters / 11 | `Crypters` / [Assets/Scripts/Races/Graphics/MainRaces/Crypters.cs](../../Assets/Scripts/Races/Graphics/MainRaces/Crypters.cs) | `DefaultRaceData` | 10 | 0 | 2 / 2 | empire | femaleCrypters, maleCrypters |
| DarkSwallower / 107 | `DarkSwallower` / [Assets/Scripts/Races/Graphics/Monsters/DarkSwallower.cs](../../Assets/Scripts/Races/Graphics/Monsters/DarkSwallower.cs) | `BlankSlate` | 8 | 0 | 1 / 0 | monster | DarkSwallower |
| DewSprites / 56 | `DewSprite` / [Assets/Scripts/Races/Graphics/Mercs/DewSprite.cs](../../Assets/Scripts/Races/Graphics/Mercs/DewSprite.cs) | `BlankSlate` | 4 | 0 | 1 / 0 | merc | femaleDewSprites |
| Dogs / 1 | `Dogs` / [Assets/Scripts/Races/Graphics/MainRaces/Dogs.cs](../../Assets/Scripts/Races/Graphics/MainRaces/Dogs.cs) | `DefaultRaceData` | 8 | 0 | 2 / 1 | generic/inherited/none | femaleDogs, maleDogs |
| DRACO / 175 | `DRACO` / [Assets/Scripts/Races/Graphics/UniqueMercs/DRACO.cs](../../Assets/Scripts/Races/Graphics/UniqueMercs/DRACO.cs) | `BlankSlate` | 5 | 0 | 1 / 0 | specialmerc | none by this rule |
| Driders / 19 | `Driders` / [Assets/Scripts/Races/Graphics/MainRaces/Driders.cs](../../Assets/Scripts/Races/Graphics/MainRaces/Driders.cs) | `DefaultRaceData` | 7 | 0 | 1 / 0 | empire | femaleDriders, maleDriders |
| EarthDryad / 156 | `EarthDryad` / [Assets/Scripts/Races/Graphics/Monsters/EarthDryad.cs](../../Assets/Scripts/Races/Graphics/Monsters/EarthDryad.cs) | `DefaultRaceData` | 8 | 0 | 8 / 5 | empire, monster | EarthDryad |
| Earthworms / 131 | `Earthworms` / [Assets/Scripts/Races/Graphics/Monsters/Earthworms.cs](../../Assets/Scripts/Races/Graphics/Monsters/Earthworms.cs) | `BlankSlate` | 6 | 0 | 1 / 0 | monster | Earthworms |
| Eevee / 66 | `Eevee` / [Assets/Scripts/Races/Graphics/MainRaces/Eevee.cs](../../Assets/Scripts/Races/Graphics/MainRaces/Eevee.cs) | `DefaultRaceData` | 4 | 0 | 4 / 2 | empire, merc | femaleEevee, maleEevee |
| Equaleon / 67 | `Equaleon` / [Assets/Scripts/Races/Graphics/MainRaces/Equaleon.cs](../../Assets/Scripts/Races/Graphics/MainRaces/Equaleon.cs) | `DefaultRaceData` | 6 | 0 | 5 / 3 | empire, merc | femaleEqualeon, maleEqualeon |
| Erin / 180 | `Erin` / [Assets/Scripts/Races/Graphics/UniqueMercs/Erin.cs](../../Assets/Scripts/Races/Graphics/UniqueMercs/Erin.cs) | `DefaultRaceData` | 7 | 0 | 1 / 0 | specialmerc | none by this rule |
| Fairies / 117 | `Fairy` / [Assets/Scripts/Races/Graphics/Monsters/Fairy.cs](../../Assets/Scripts/Races/Graphics/Monsters/Fairy.cs) | `BlankSlate` | 10 | 0 | 3 / 0 | monster | femaleFairies, maleFairies |
| Feit / 185 | `Feit` / [Assets/Scripts/Races/Graphics/UniqueMercs/Feit.cs](../../Assets/Scripts/Races/Graphics/UniqueMercs/Feit.cs) | `BlankSlate` | 5 | 0 | 1 / 0 | specialmerc | none by this rule |
| FeralEevee / 149 | `FeralEevee` / [Assets/Scripts/Races/Graphics/Monsters/FeralEevee.cs](../../Assets/Scripts/Races/Graphics/Monsters/FeralEevee.cs) | `BlankSlate` | 6 | 0 | 1 / 0 | monster | femaleFeralEevee, maleFeralEevee |
| FeralEqualeon / 151 | `FeralEqualeon` / [Assets/Scripts/Races/Graphics/Monsters/FeraEqualeon.cs](../../Assets/Scripts/Races/Graphics/Monsters/FeraEqualeon.cs) | `BlankSlate` | 7 | 0 | 1 / 0 | monster | femaleFeralEqualeon, maleFeralEqualeon |
| FeralFrogs / 113 | `Frogs` / [Assets/Scripts/Races/Graphics/Monsters/Frogs.cs](../../Assets/Scripts/Races/Graphics/Monsters/Frogs.cs) | `BlankSlate` | 8 | 0 | 1 / 0 | monster | FeralFrogs |
| FeralOrcas / 143 | `FeralOrcas` / [Assets/Scripts/Races/Graphics/Monsters/FeralOrcas.cs](../../Assets/Scripts/Races/Graphics/Monsters/FeralOrcas.cs) | `BlankSlate` | 7 | 0 | 1 / 0 | monster | femaleFeralOrcas, maleFeralOrcas |
| FeralSharks / 105 | `Sharks` / [Assets/Scripts/Races/Graphics/Monsters/Sharks.cs](../../Assets/Scripts/Races/Graphics/Monsters/Sharks.cs) | `BlankSlate` | 9 | 0 | 1 / 0 | monster | FeralSharks |
| Foxes / 2 | `Foxes` / [Assets/Scripts/Races/Graphics/MainRaces/Foxes.cs](../../Assets/Scripts/Races/Graphics/MainRaces/Foxes.cs) | `DefaultRaceData` | 10 | 0 | 2 / 1 | generic/inherited/none | femaleFoxes, maleFoxes |
| FungalDryad / 158 | `FungalDryad` / [Assets/Scripts/Races/Graphics/Monsters/FungalDryad.cs](../../Assets/Scripts/Races/Graphics/Monsters/FungalDryad.cs) | `DefaultRaceData` | 7 | 0 | 7 / 3 | empire, monster | FungalDryad |
| Ghosts / 38 | `Ghosts` / [Assets/Scripts/Races/Graphics/MainRaces/Ghosts.cs](../../Assets/Scripts/Races/Graphics/MainRaces/Ghosts.cs) | `DefaultRaceData` | 7 | 0 | 5 / 1 | empire | femaleGhosts, maleGhosts |
| Goblins / 52 | `Goblins` / [Assets/Scripts/Races/Graphics/Mercs/Goblins.cs](../../Assets/Scripts/Races/Graphics/Mercs/Goblins.cs) | `DefaultRaceData` | 18 | 3 | 12 / 0 | empire, monster | femaleGoblins, maleGoblins |
| Hamsters / 63 | `Hamsters` / [Assets/Scripts/Races/Graphics/Mercs/Hamsters.cs](../../Assets/Scripts/Races/Graphics/Mercs/Hamsters.cs) | `BlankSlate` | 6 | 0 | 2 / 1 | merc | femaleHamsters, maleHamsters |
| Harpies / 8 | `Harpies` / [Assets/Scripts/Races/Graphics/MainRaces/Harpies.cs](../../Assets/Scripts/Races/Graphics/MainRaces/Harpies.cs) | `DefaultRaceData` | 11 | 0 | 1 / 1 | empire | femaleHarpies, maleHarpies |
| Humans / 10 | `Humans` / [Assets/Scripts/Races/Graphics/MainRaces/Humans.cs](../../Assets/Scripts/Races/Graphics/MainRaces/Humans.cs) | `DefaultRaceData` | 6 | 2 | 16 / 11 | empire | femaleHumans, maleHumans |
| Iliijiith / 161 | `Iliijiith` / [Assets/Scripts/Races/Graphics/Monsters/Iliijiith.cs](../../Assets/Scripts/Races/Graphics/Monsters/Iliijiith.cs) | `BlankSlate` | 8 | 0 | 1 / 0 | monster | Iliijiith |
| Imps / 9 | `Imps` / [Assets/Scripts/Races/Graphics/MainRaces/Imps.cs](../../Assets/Scripts/Races/Graphics/MainRaces/Imps.cs) | `DefaultRaceData` | 18 | 3 | 12 / 1 | empire | femaleImps, maleImps |
| Ki / 172 | `Ki` / [Assets/Scripts/Races/Graphics/UniqueMercs/Ki.cs](../../Assets/Scripts/Races/Graphics/UniqueMercs/Ki.cs) | `BlankSlate` | 7 | 0 | 1 / 0 | specialmerc | none by this rule |
| Konane / 194 | `Konane` / [Assets/Scripts/Races/Graphics/UniqueMercs/Konane.cs](../../Assets/Scripts/Races/Graphics/UniqueMercs/Konane.cs) | `BlankSlate` | 5 | 0 | 1 / 0 | specialmerc | none by this rule |
| Lamia / 12 | `Lamia` / [Assets/Scripts/Races/Graphics/MainRaces/Lamia.cs](../../Assets/Scripts/Races/Graphics/MainRaces/Lamia.cs) | `DefaultRaceData` | 12 | 0 | 9 / 6 | empire | femaleLamia, maleLamia |
| MainlandElves / 33 | `MainlandElves` / [Assets/Scripts/Races/Graphics/MainRaces/MainlandElves.cs](../../Assets/Scripts/Races/Graphics/MainRaces/MainlandElves.cs) | `DefaultRaceData` | 6 | 1 | 15 / 9 | empire | femaleMainlandElves, maleMainlandElves |
| Merfolk / 23 | `Mermen` / [Assets/Scripts/Races/Graphics/MainRaces/Mermen.cs](../../Assets/Scripts/Races/Graphics/MainRaces/Mermen.cs) | `DefaultRaceData` | 9 | 1 | 2 / 0 | empire | femaleMerfolk, maleMerfolk |
| Nectar / 192 | `Nectar` / [Assets/Scripts/Races/Graphics/UniqueMercs/Nectar.cs](../../Assets/Scripts/Races/Graphics/UniqueMercs/Nectar.cs) | `BlankSlate` | 5 | 0 | 1 / 0 | specialmerc | none by this rule |
| Olivia / 188 | `Olivia` / [Assets/Scripts/Races/Graphics/UniqueMercs/Olivia.cs](../../Assets/Scripts/Races/Graphics/UniqueMercs/Olivia.cs) | `DefaultRaceData` | 6 | 0 | 2 / 0 | specialmerc | none by this rule |
| OoviKat / 70 | `OoviKat` / [Assets/Scripts/Races/Graphics/Mercs/OoviKat.cs](../../Assets/Scripts/Races/Graphics/Mercs/OoviKat.cs) | `DefaultRaceData` | 5 | 1 | 17 / 9 | empire, merc | femaleOoviKat, maleOoviKat |
| Puca / 54 | `Puca` / [Assets/Scripts/Races/Graphics/Mercs/Puca.cs](../../Assets/Scripts/Races/Graphics/Mercs/Puca.cs) | `BlankSlate` | 5 | 0 | 2 / 1 | merc | femalePuca, malePuca |
| Pudding / 162 | `Pudding` / [Assets/Scripts/Races/Graphics/Monsters/Pudding.cs](../../Assets/Scripts/Races/Graphics/Monsters/Pudding.cs) | `BlankSlate` | 7 | 0 | 1 / 0 | monster | Pudding |
| Raptor / 128 | `Raptor` / [Assets/Scripts/Races/Graphics/Monsters/Raptor.cs](../../Assets/Scripts/Races/Graphics/Monsters/Raptor.cs) | `BlankSlate` | 6 | 0 | 1 / 0 | monster | Raptor |
| Renamon / 69 | `Renamon` / [Assets/Scripts/Races/Graphics/Mercs/Renamon.cs](../../Assets/Scripts/Races/Graphics/Mercs/Renamon.cs) | `DefaultRaceData` | 9 | 2 | 5 / 0 | merc | femaleRenamon, maleRenamon |
| RiverDryad / 157 | `RiverDryad` / [Assets/Scripts/Races/Graphics/Monsters/RiverDryad.cs](../../Assets/Scripts/Races/Graphics/Monsters/RiverDryad.cs) | `DefaultRaceData` | 8 | 0 | 8 / 5 | empire, monster | RiverDryad |
| RwuMercenaries / 64 | `RwuMercenaries` / [Assets/Scripts/Races/Graphics/Mercs/RwuMercenaries.cs](../../Assets/Scripts/Races/Graphics/Mercs/RwuMercenaries.cs) | `BlankSlate` | 8 | 0 | 9 / 0 | merc | femaleRwuMercenaries, maleRwuMercenaries |
| Ryan / 193 | `Ryan` / [Assets/Scripts/Races/Graphics/UniqueMercs/Ryan.cs](../../Assets/Scripts/Races/Graphics/UniqueMercs/Ryan.cs) | `BlankSlate` | 6 | 0 | 2 / 0 | specialmerc | none by this rule |
| Salix / 181 | `Salix` / [Assets/Scripts/Races/Graphics/UniqueMercs/Salix.cs](../../Assets/Scripts/Races/Graphics/UniqueMercs/Salix.cs) | `DefaultRaceData` | 4 | 1 | 3 / 0 | specialmerc | none by this rule |
| Schiwardez / 134 | `Schiwardez` / [Assets/Scripts/Races/Graphics/Monsters/Schiwardez.cs](../../Assets/Scripts/Races/Graphics/Monsters/Schiwardez.cs) | `BlankSlate` | 7 | 0 | 1 / 0 | monster | Schiwardez |
| Scorch / 173 | `Scorch` / [Assets/Scripts/Races/Graphics/UniqueMercs/Scorch.cs](../../Assets/Scripts/Races/Graphics/UniqueMercs/Scorch.cs) | `BlankSlate` | 4 | 0 | 1 / 0 | specialmerc | none by this rule |
| Scylla / 7 | `Scylla` / [Assets/Scripts/Races/Graphics/MainRaces/Scylla.cs](../../Assets/Scripts/Races/Graphics/MainRaces/Scylla.cs) | `DefaultRaceData` | 9 | 0 | 1 / 1 | empire | femaleScylla, maleScylla |
| Selicia / 170 | `Selicia` / [Assets/Scripts/Races/Graphics/UniqueMercs/Selicia.cs](../../Assets/Scripts/Races/Graphics/UniqueMercs/Selicia.cs) | `BlankSlate` | 146 | 0 | 1 / 0 | specialmerc | none by this rule |
| Seraph / 196 | `Seraph` / [Assets/Scripts/Races/Graphics/UniqueMercs/Seraph.cs](../../Assets/Scripts/Races/Graphics/UniqueMercs/Seraph.cs) | `BlankSlate` | 7 | 0 | 1 / 0 | specialmerc | none by this rule |
| Seville / 191 | `Seville` / [Assets/Scripts/Races/Graphics/UniqueMercs/Seville.cs](../../Assets/Scripts/Races/Graphics/UniqueMercs/Seville.cs) | `BlankSlate` | 7 | 0 | 1 / 0 | specialmerc | none by this rule |
| Singularity / 184 | `Singularity` / [Assets/Scripts/Races/Graphics/UniqueMercs/Singularity.cs](../../Assets/Scripts/Races/Graphics/UniqueMercs/Singularity.cs) | `BlankSlate` | 5 | 0 | 2 / 0 | specialmerc | none by this rule |
| Skapa / 189 | `Skapa` / [Assets/Scripts/Races/Graphics/UniqueMercs/Skapa.cs](../../Assets/Scripts/Races/Graphics/UniqueMercs/Skapa.cs) | `BlankSlate` | 5 | 0 | 1 / 0 | specialmerc | none by this rule |
| Slimes / 6 | `Slimes` / [Assets/Scripts/Races/Graphics/MainRaces/Slimes.cs](../../Assets/Scripts/Races/Graphics/MainRaces/Slimes.cs) | `DefaultRaceData` | 17 | 1 | 1 / 1 | empire | femaleSlimes, maleSlimes |
| SoulSprite / 163 | `SoulSprite` / [Assets/Scripts/Races/Graphics/Monsters/SoulSprite.cs](../../Assets/Scripts/Races/Graphics/Monsters/SoulSprite.cs) | `BlankSlate` | 9 | 0 | 1 / 0 | monster | none by this rule |
| Succubi / 50 | `Succubi` / [Assets/Scripts/Races/Graphics/Mercs/Succubi.cs](../../Assets/Scripts/Races/Graphics/Mercs/Succubi.cs) | `DefaultRaceData` | 17 | 0 | 2 / 2 | merc | femaleSuccubi, maleSuccubi |
| Taraluxia / 186 | `Taraluxia` / [Assets/Scripts/Races/Graphics/UniqueMercs/Taraluxia.cs](../../Assets/Scripts/Races/Graphics/UniqueMercs/Taraluxia.cs) | `BlankSlate` | 7 | 0 | 2 / 0 | specialmerc | none by this rule |
| Tatltuae / 190 | `Tatltuae` / [Assets/Scripts/Races/Graphics/UniqueMercs/Tatltuae.cs](../../Assets/Scripts/Races/Graphics/UniqueMercs/Tatltuae.cs) | `DefaultRaceData` | 11 | 0 | 2 / 1 | specialmerc | none by this rule |
| Taurus / 14 | `Cows` / [Assets/Scripts/Races/Graphics/MainRaces/Taurus.cs](../../Assets/Scripts/Races/Graphics/MainRaces/Taurus.cs) | `DefaultRaceData` | 9 | 0 | 4 / 2 | empire | femaleTaurus, maleTaurus |
| Tigers / 51 | `Tigers` / [Assets/Scripts/Races/Graphics/Mercs/Tigers.cs](../../Assets/Scripts/Races/Graphics/Mercs/Tigers.cs) | `DefaultRaceData` | 12 | 0 | 3 / 2 | generic/inherited/none | femaleTigers, maleTigers |
| Umbreon / 35 | `Umbreon` / [Assets/Scripts/Races/Graphics/MainRaces/Umbreon.cs](../../Assets/Scripts/Races/Graphics/MainRaces/Umbreon.cs) | `DefaultRaceData` | 10 | 1 | 5 / 2 | empire | femaleUmbreon, maleUmbreon |
| Vagrants / 100 | `Vagrants` / [Assets/Scripts/Races/Graphics/Monsters/Vagrants.cs](../../Assets/Scripts/Races/Graphics/Monsters/Vagrants.cs) | `BlankSlate` | 23 | 0 | 3 / 0 | monster | Vagrants |
| Vipers / 58 | `Vipers` / [Assets/Scripts/Races/Graphics/Mercs/Vipers.cs](../../Assets/Scripts/Races/Graphics/Mercs/Vipers.cs) | `DefaultRaceData` | 7 | 2 | 4 / 1 | merc | femaleVipers, maleVipers |
| Viisels / 148 | `Viisels` / [Assets/Scripts/Races/Graphics/Mercs/Viisels.cs](../../Assets/Scripts/Races/Graphics/Mercs/Viisels.cs) | `BlankSlate` | 7 | 0 | 1 / 0 | monster | femaleViisels, maleViisels |
| ViraeUltimae / 147 | `ViraeUltimae` / [Assets/Scripts/Races/Graphics/Monsters/ViraeUltimae.cs](../../Assets/Scripts/Races/Graphics/Monsters/ViraeUltimae.cs) | `DefaultRaceData` | 11 | 0 | 1 / 0 | monster | none by this rule |
| Vision / 171 | `Vision` / [Assets/Scripts/Races/Graphics/UniqueMercs/Vision.cs](../../Assets/Scripts/Races/Graphics/UniqueMercs/Vision.cs) | `BlankSlate` | 6 | 0 | 1 / 0 | specialmerc | none by this rule |
| Voilin / 111 | `Voilin` / [Assets/Scripts/Races/Graphics/Monsters/Voilin.cs](../../Assets/Scripts/Races/Graphics/Monsters/Voilin.cs) | `BlankSlate` | 6 | 0 | 1 / 0 | monster | Voilin |
| Whisp / 139 | `Whisp` / [Assets/Scripts/Races/Graphics/Monsters/Whisp.cs](../../Assets/Scripts/Races/Graphics/Monsters/Whisp.cs) | `BlankSlate` | 4 | 0 | 1 / 0 | monster | Whisp |
| Wolves / 3 | `Wolves` / [Assets/Scripts/Races/Graphics/MainRaces/Wolves.cs](../../Assets/Scripts/Races/Graphics/MainRaces/Wolves.cs) | `DefaultRaceData` | 8 | 0 | 2 / 1 | generic/inherited/none | femaleWolves, maleWolves |
| WoodDryad / 155 | `WoodDryad` / [Assets/Scripts/Races/Graphics/Monsters/WoodDryad.cs](../../Assets/Scripts/Races/Graphics/Monsters/WoodDryad.cs) | `DefaultRaceData` | 8 | 0 | 8 / 5 | empire, monster | WoodDryad |
| Xelhilde / 187 | `Xelhilde` / [Assets/Scripts/Races/Graphics/UniqueMercs/Xelhilde.cs](../../Assets/Scripts/Races/Graphics/UniqueMercs/Xelhilde.cs) | `DefaultRaceData` | 6 | 0 | 1 / 0 | specialmerc | none by this rule |
| Yordles / 40 | `Yordles` / [Assets/Scripts/Races/Graphics/MainRaces/Yordles.cs](../../Assets/Scripts/Races/Graphics/MainRaces/Yordles.cs) | `DefaultRaceData` | 6 | 2 | 10 / 4 | empire | femaleYordles, maleYordles |
| Youko / 62 | `Youko` / [Assets/Scripts/Races/Graphics/Mercs/Youko.cs](../../Assets/Scripts/Races/Graphics/Mercs/Youko.cs) | `Humans, IVoreRestrictions` | 7 | 1 | 9 / 6 | empire | femaleYouko, maleYouko |
| Zera / 178 | `Zera` / [Assets/Scripts/Races/Graphics/UniqueMercs/Zera.cs](../../Assets/Scripts/Races/Graphics/UniqueMercs/Zera.cs) | `BlankSlate` | 5 | 0 | 4 / 0 | specialmerc | none by this rule |
| Zoey / 176 | `Zoey` / [Assets/Scripts/Races/Graphics/UniqueMercs/Zoey.cs](../../Assets/Scripts/Races/Graphics/UniqueMercs/Zoey.cs) | `BlankSlate` | 5 | 1 | 2 / 0 | specialmerc | none by this rule |

## Typed registry field consumers

Self-references within a species class are listed because lazy initialization must still return the same instance. GetRace calls and serialized references are additional dependencies.

- `Aabayx`: [Assets/Scripts/Entities/Clothing/ClothingDiscards.cs](../../Assets/Scripts/Entities/Clothing/ClothingDiscards.cs).
- `Aurilika`: [Assets/Scripts/Races/Graphics/UniqueMercs/Auri.cs](../../Assets/Scripts/Races/Graphics/UniqueMercs/Auri.cs).
- `Centaur`: [Assets/Scripts/Races/Graphics/MainRaces/Centaur.cs](../../Assets/Scripts/Races/Graphics/MainRaces/Centaur.cs).
- `Cockatrice`: [Assets/Scripts/Entities/Clothing/ClothingDiscards.cs](../../Assets/Scripts/Entities/Clothing/ClothingDiscards.cs), [Assets/Scripts/Races/Graphics/Mercs/Cockatrice.cs](../../Assets/Scripts/Races/Graphics/Mercs/Cockatrice.cs).
- `Goblins`: [Assets/Scripts/Entities/Clothing/ClothingDiscards.cs](../../Assets/Scripts/Entities/Clothing/ClothingDiscards.cs), [Assets/Scripts/Races/Graphics/Mercs/Goblins.cs](../../Assets/Scripts/Races/Graphics/Mercs/Goblins.cs), [Assets/Scripts/Subwindows/UnitCustomizer.cs](../../Assets/Scripts/Subwindows/UnitCustomizer.cs).
- `Humans`: [Assets/Scripts/Entities/Clothing/ClothingDiscards.cs](../../Assets/Scripts/Entities/Clothing/ClothingDiscards.cs), [Assets/Scripts/Races/Graphics/MainRaces/Humans.cs](../../Assets/Scripts/Races/Graphics/MainRaces/Humans.cs).
- `Imps`: [Assets/Scripts/Entities/Clothing/ClothingDiscards.cs](../../Assets/Scripts/Entities/Clothing/ClothingDiscards.cs), [Assets/Scripts/Races/Graphics/MainRaces/Imps.cs](../../Assets/Scripts/Races/Graphics/MainRaces/Imps.cs), [Assets/Scripts/Subwindows/UnitCustomizer.cs](../../Assets/Scripts/Subwindows/UnitCustomizer.cs).
- `MainlandElves`: [Assets/Scripts/Races/Graphics/MainRaces/MainlandElves.cs](../../Assets/Scripts/Races/Graphics/MainRaces/MainlandElves.cs).
- `Mermen`: [Assets/Scripts/Entities/Clothing/ClothingDiscards.cs](../../Assets/Scripts/Entities/Clothing/ClothingDiscards.cs).
- `OoviKat`: [Assets/Scripts/Races/Graphics/Mercs/OoviKat.cs](../../Assets/Scripts/Races/Graphics/Mercs/OoviKat.cs).
- `Renamon`: [Assets/Scripts/Entities/Clothing/ClothingDiscards.cs](../../Assets/Scripts/Entities/Clothing/ClothingDiscards.cs), [Assets/Scripts/Races/Graphics/Mercs/Renamon.cs](../../Assets/Scripts/Races/Graphics/Mercs/Renamon.cs).
- `Salix`: [Assets/Scripts/Races/Graphics/UniqueMercs/Salix.cs](../../Assets/Scripts/Races/Graphics/UniqueMercs/Salix.cs).
- `Slimes`: [Assets/Scripts/Modes/Tactical/TacticalGraphicalEffects.cs](../../Assets/Scripts/Modes/Tactical/TacticalGraphicalEffects.cs).
- `Umbreon`: [Assets/Scripts/Races/Graphics/MainRaces/Umbreon.cs](../../Assets/Scripts/Races/Graphics/MainRaces/Umbreon.cs).
- `Vipers`: [Assets/Scripts/Entities/Clothing/ClothingDiscards.cs](../../Assets/Scripts/Entities/Clothing/ClothingDiscards.cs), [Assets/Scripts/Races/Graphics/Mercs/Vipers.cs](../../Assets/Scripts/Races/Graphics/Mercs/Vipers.cs).
- `Yordles`: [Assets/Scripts/Entities/Clothing/ClothingDiscards.cs](../../Assets/Scripts/Entities/Clothing/ClothingDiscards.cs), [Assets/Scripts/Races/Graphics/MainRaces/Yordles.cs](../../Assets/Scripts/Races/Graphics/MainRaces/Yordles.cs).
- `Youko`: [Assets/Scripts/Races/Graphics/Mercs/Youko.cs](../../Assets/Scripts/Races/Graphics/Mercs/Youko.cs).
- `Zoey`: [Assets/Scripts/Races/Graphics/UniqueMercs/Zoey.cs](../../Assets/Scripts/Races/Graphics/UniqueMercs/Zoey.cs).

## Variants and generic bases

- SlimeQueen is an extra registry field/class, selected for a Slimes leader and referenced by UnitCustomizer. It is covered by retirement of Slimes, not a 91st user-listed species.
- AntQueen belongs to the uncurated Ants entry; it is not approved or retired by analogy.
- Youko inherits Humans; SlimeQueen inherits Slimes; Centaur inherits TaurHumanHalf, which inherits BlankSlate. All other direct bases are shown above. BlankSlate is also a shared base for many uncurated animal definitions.
- The shared DefaultRaceData implementation, common sprite arrays, clothing tables and scene/prefab GUID references are outside the direct per-class token counts. See [the ownership preflight](03b-rejected-species-preflight.md).
