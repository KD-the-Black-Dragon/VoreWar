# 03B4 — Rejected graphics registry retirement

Date: 2026-11-01. User accepted 03B3D and authorized the next packet. Full related registry/typed-consumer group handled together.

## Source scope

- `Assets/Scripts/Races/Races.cs`
- `Assets/Scripts/Subwindows/UnitCustomizer.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Centaur.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Humans.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Imps.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/MainlandElves.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Umbreon.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Yordles.cs`
- `Assets/Scripts/Races/Graphics/Mercs/Cockatrice.cs`
- `Assets/Scripts/Races/Graphics/Mercs/Goblins.cs`
- `Assets/Scripts/Races/Graphics/Mercs/OoviKat.cs`
- `Assets/Scripts/Races/Graphics/Mercs/Renamon.cs`
- `Assets/Scripts/Races/Graphics/Mercs/Vipers.cs`
- `Assets/Scripts/Races/Graphics/Mercs/Youko.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Auri.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Salix.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Zoey.cs`
- `Assets/Scripts/Modes/Tactical/TacticalGraphicalEffects.cs`
- `Assets/Scripts/Entities/Clothing/ClothingDiscards.cs`

## Changes

- Races.cs no longer eagerly constructs 91 rejected instances: all 90 species plus SlimeQueen. Removed all 90 corresponding enum dispatch arms and the Slimes leader override. Retained all 66 other instance declarations and all 64 nonretired species mappings exactly, including AntQueen's UnitType.Leader override and BlankSlate for unrelated unknown IDs. Races.GetRace(Race) rejects permanent-retirement IDs explicitly before the unknown-ID BlankSlate fallback. No player enablement/curation filter is used for rendering existing nonretired individuals.
- ClothingDiscards drops 28 catalog additions that depended on rejected registry instances. Keep all other additions in their original order, generic ClothingTypes/RaceSpecificClothing and existing TaurusClothes/CruxClothing shared helper catalogs. This is not a wholesale clothing/art purge. Retired-specific discard routes cannot justify instantiating a rejected species. Potential shared cloth/sheet ownership remains a later boundary; no asset was removed.
- UnitCustomizer removes only rejected SlimeQueen/Imps/Goblins comparisons for two extra color buttons. Panthers retains both buttons. The separate customized leader creator remains later work.
- TacticalGraphicalEffects removes the Slimes-specific projectile branch that required the retired singleton material getter. Other species/projectile branches and shared palette services remain unchanged. Residual rejected enum/sprite branches do not instantiate species; sprite-loader closure remains open.
- Fifteen retained-but-retired graphics source files contain nested clothing callbacks referring to their own old registry instance. Those member reads now resolve the drawing actor's graphics and cast to the original class; no replacement global singleton or lazy rejected construction was introduced. This is compile-time dependency closure for preserved source, not re-enabling that species. Races.GetRace still rejects actual retired species. No nonretired class inherits from a rejected graphics class in the source inventory; direct rejected constructor expressions are absent after this packet. A manually fabricated retired object is unsupported, not an existing player-disabled nonretired individual.

Before registry: 157 eager instances. After: 66. All nonretired lookup arms/instance declarations are exact matches to packet start. The retired types and artwork remain on disk; this packet does not claim their source payloads have been erased or their textures unloaded. No files deleted or moved.

## Exact retired instances

| Removed registry field | Graphics class |
|---|---|
| Aabayx | Aabayx |
| Abakhanskya | Abakhanskya |
| Alraune | Alraune |
| Asura | Asura |
| Aurilika | Auri |
| Bears | Bears |
| Bella | Bella |
| BoomBunnies | BoomBunnies |
| Cake | Cake |
| Cats | Cats |
| Centaur | Centaur |
| Cherub | Cherub |
| Cierihaka | Cierihaka |
| Cockatrice | Cockatrice |
| Collector | Collector |
| Compy | Compy |
| Cows | Taurus |
| Crux | Crux |
| Crypters | Crypters |
| DRACO | DRACO |
| DarkSwallower | DarkSwallower |
| DewSprite | DewSprite |
| Dogs | Dogs |
| Driders | Driders |
| EarthDryad | EarthDryad |
| Earthworms | Earthworms |
| Eevee | Eevee |
| Equaleon | Equaleon |
| Erin | Erin |
| Fairy | Fairy |
| Feit | Feit |
| FeralEevee | FeralEevee |
| FeralEqualeon | FeralEqualeon |
| FeralOrcas | FeralOrcas |
| Foxes | Foxes |
| Frogs | Frogs |
| FungalDryad | FungalDryad |
| Ghosts | Ghosts |
| Goblins | Goblins |
| Hamsters | Hamsters |
| Harpies | Harpies |
| Humans | Humans |
| Iliijiith | Iliijiith |
| Imps | Imps |
| Ki | Ki |
| Konane | Konane |
| Lamia | Lamia |
| MainlandElves | MainlandElves |
| Mermen | Mermen |
| Nectar | Nectar |
| Olivia | Olivia |
| OoviKat | OoviKat |
| Puca | Puca |
| Pudding | Pudding |
| Raptor | Raptor |
| Renamon | Renamon |
| RiverDryad | RiverDryad |
| RwuMercenaries | RwuMercenaries |
| Ryan | Ryan |
| Salix | Salix |
| Schiwardez | Schiwardez |
| Scorch | Scorch |
| Scylla | Scylla |
| Selicia | Selicia |
| Seraph | Seraph |
| Seville | Seville |
| Sharks | Sharks |
| Singularity | Singularity |
| Skapa | Skapa |
| SlimeQueen | SlimeQueen |
| Slimes | Slimes |
| SoulSprite | SoulSprite |
| Succubi | Succubi |
| Taraluxia | Taraluxia |
| Tatltuae | Tatltuae |
| Tigers | Tigers |
| Umbreon | Umbreon |
| Vagrants | Vagrants |
| Viisels | Viisels |
| Vipers | Vipers |
| ViraeUltimae | ViraeUltimae |
| Vision | Vision |
| Voilin | Voilin |
| Whisp | Whisp |
| Wolves | Wolves |
| WoodDryad | WoodDryad |
| Xelhilde | Xelhilde |
| Yordles | Yordles |
| Youko | Youko |
| Zera | Zera |
| Zoey | Zoey |

## Shared resources and remaining work

The earlier 03B dependency matrix and shared-sprite/bundle inventory remain historical candidate maps. Four mixed bundle loads and numerous SpriteDictionary entries are still present; removing a registry instance does not prove a sheet is exclusive or remove it from a bundle. Generic body parts/clothing and tactical effects share sheets associated with rejected species. No bundle or sprite-loader changes are included here. The code search is lexical source evidence, not proof against arbitrary reflection or all possible serialized references.

Next recommended grouped packet: refresh residual sprite-loader/typed helper/shared-sheet ownership against this new registry. Identify live shared consumers before retiring loader bindings or class payloads. No file move/delete is authorized by this packet; comply with the file-operation policy for any later retirement of entire paths. Phase 2/spec 03 remain open, and no species is approved.

## Verification

Unity compilation PASSED exit 0 (Logs/packet-03b4-compile.log), with no C# errors/logged exceptions or new compiler warnings versus 03B3D. Explicit Assets/base.unity loading PASSED exit 0 (Logs/packet-03b4-scene.log), without errors/exceptions. All 19 source files match the reviewed expected text and retain their BOM/line-ending styles; dated notices cover the actual diff. Exact retained registry declarations/mappings, no rejected typed fields/direct construction, unchanged scene bytes, preserved License.txt hash and scoped diff whitespace checks passed. No Unity process remains; prior uncommitted work is preserved. Evidence: ignored Logs/packet-03b4-source-review.json contains per-file expected text, before paths, exact 91 removed/66 retained declarations and 64 retained mappings; typed-consumers.json records the pre-change 18 consumer files. Per-file diffs/before snapshots and base.unity snapshot retained. Source assertions passed for exact retained declarations/mappings, absent rejected typed-field references across all C# sources, and no remaining direct rejected graphics construction.

No new test framework/tests or player build; actual battles await approved species. One combined manual check after agent verification: startup/Main Menu, Pure Tactical setup, and Race Editor opening/loading a few remaining development species without new red errors. No content-settings tour required.

## GPL modification check

Read local License.txt sections 4/5. All 19 modified sources have a dated 2026-11-01 conversion notice; earlier notices retained, exact paths listed in CHANGES.md. No introduced/copied external code, dependency or asset. Final scope/notice/CHANGES path coverage, unchanged license hash and scene-byte checks passed; existing attribution preserved. This is not complete release/distribution clearance; shared-asset provenance and distribution requirements still need review before release.


User accepted the combined 03B4 checks. [03B5](03b5-retired-only-sprite-loader-bindings.md) follows with 156 retired-only loader bindings; shared bindings, files, bundles and serialized assets are preserved.
