# 03B7 — Disconnected definition source disposition review

Date: 2026-11-01. Documentation-only source review following completed 03B6; no new gameplay or asset edits.

The 91 graphics classes disconnected by 03B4 remain as source payloads: 90 rejected species plus SlimeQueen. The registry has no construction/lookup entries for them. Current source inventory found no external `new`, `typeof`, `is`, `as` or direct class-inheritance candidate referring to these types outside the 91 definition files. Internal inheritance/static imports were recorded separately. This is lexical evidence, not proof against arbitrary reflection, casts, generic signatures, serialized type names or external assemblies.

Shared helpers remain a separate ownership boundary: DefaultRaceData, BlankSlate, TaurHumanHalf, generic clothing, TaurusClothes/CruxClothing, sprite declarations and palettes must not be retired simply because rejected definitions use them. Species-specific projectile branches still exist in TacticalGraphicalEffects and can retain sprite bindings even without constructing species objects. The 03B5 shared-reader table remains the conservative source for those resources; do not clear them automatically from this class inventory.

## Definition paths requiring source-payload disposition

| Class | Retained path |
|---|---|
| Aabayx | `Assets/Scripts/Races/Graphics/MainRaces/Aabayx.cs` |
| Abakhanskya | `Assets/Scripts/Races/Graphics/MainRaces/Abakhanskya.cs` |
| Alraune | `Assets/Scripts/Races/Graphics/MainRaces/Alraune.cs` |
| Asura | `Assets/Scripts/Races/Graphics/UniqueMercs/Asura.cs` |
| Auri | `Assets/Scripts/Races/Graphics/UniqueMercs/Auri.cs` |
| Bears | `Assets/Scripts/Races/Graphics/MainRaces/Bears.cs` |
| Bella | `Assets/Scripts/Races/Graphics/Monsters/Bella.cs` |
| BoomBunnies | `Assets/Scripts/Races/Graphics/Monsters/BoomBunnies.cs` |
| Cake | `Assets/Scripts/Races/Graphics/Monsters/Cake.cs` |
| Cats | `Assets/Scripts/Races/Graphics/MainRaces/Cats.cs` |
| Centaur | `Assets/Scripts/Races/Graphics/MainRaces/Centaur.cs` |
| Cherub | `Assets/Scripts/Races/Graphics/UniqueMercs/Cherub.cs` |
| Cierihaka | `Assets/Scripts/Races/Graphics/UniqueMercs/Cierihaka.cs` |
| Cockatrice | `Assets/Scripts/Races/Graphics/Mercs/Cockatrice.cs` |
| Collector | `Assets/Scripts/Races/Graphics/Monsters/Collector.cs` |
| Compy | `Assets/Scripts/Races/Graphics/Monsters/Compy.cs` |
| Crux | `Assets/Scripts/Races/Graphics/MainRaces/Crux.cs` |
| Crypters | `Assets/Scripts/Races/Graphics/MainRaces/Crypters.cs` |
| DRACO | `Assets/Scripts/Races/Graphics/UniqueMercs/DRACO.cs` |
| DarkSwallower | `Assets/Scripts/Races/Graphics/Monsters/DarkSwallower.cs` |
| DewSprite | `Assets/Scripts/Races/Graphics/Mercs/DewSprite.cs` |
| Dogs | `Assets/Scripts/Races/Graphics/MainRaces/Dogs.cs` |
| Driders | `Assets/Scripts/Races/Graphics/MainRaces/Driders.cs` |
| EarthDryad | `Assets/Scripts/Races/Graphics/Monsters/EarthDryad.cs` |
| Earthworms | `Assets/Scripts/Races/Graphics/Monsters/Earthworms.cs` |
| Eevee | `Assets/Scripts/Races/Graphics/MainRaces/Eevee.cs` |
| Equaleon | `Assets/Scripts/Races/Graphics/MainRaces/Equaleon.cs` |
| Erin | `Assets/Scripts/Races/Graphics/UniqueMercs/Erin.cs` |
| Fairy | `Assets/Scripts/Races/Graphics/Monsters/Fairy.cs` |
| Feit | `Assets/Scripts/Races/Graphics/UniqueMercs/Feit.cs` |
| FeralEevee | `Assets/Scripts/Races/Graphics/Monsters/FeralEevee.cs` |
| FeralEqualeon | `Assets/Scripts/Races/Graphics/Monsters/FeraEqualeon.cs` |
| FeralOrcas | `Assets/Scripts/Races/Graphics/Monsters/FeralOrcas.cs` |
| Foxes | `Assets/Scripts/Races/Graphics/MainRaces/Foxes.cs` |
| Frogs | `Assets/Scripts/Races/Graphics/Monsters/Frogs.cs` |
| FungalDryad | `Assets/Scripts/Races/Graphics/Monsters/FungalDryad.cs` |
| Ghosts | `Assets/Scripts/Races/Graphics/MainRaces/Ghosts.cs` |
| Goblins | `Assets/Scripts/Races/Graphics/Mercs/Goblins.cs` |
| Hamsters | `Assets/Scripts/Races/Graphics/Mercs/Hamsters.cs` |
| Harpies | `Assets/Scripts/Races/Graphics/MainRaces/Harpies.cs` |
| Humans | `Assets/Scripts/Races/Graphics/MainRaces/Humans.cs` |
| Iliijiith | `Assets/Scripts/Races/Graphics/Monsters/Iliijiith.cs` |
| Imps | `Assets/Scripts/Races/Graphics/MainRaces/Imps.cs` |
| Ki | `Assets/Scripts/Races/Graphics/UniqueMercs/Ki.cs` |
| Konane | `Assets/Scripts/Races/Graphics/UniqueMercs/Konane.cs` |
| Lamia | `Assets/Scripts/Races/Graphics/MainRaces/Lamia.cs` |
| MainlandElves | `Assets/Scripts/Races/Graphics/MainRaces/MainlandElves.cs` |
| Mermen | `Assets/Scripts/Races/Graphics/MainRaces/Mermen.cs` |
| Nectar | `Assets/Scripts/Races/Graphics/UniqueMercs/Nectar.cs` |
| Olivia | `Assets/Scripts/Races/Graphics/UniqueMercs/Olivia.cs` |
| OoviKat | `Assets/Scripts/Races/Graphics/Mercs/OoviKat.cs` |
| Puca | `Assets/Scripts/Races/Graphics/Mercs/Puca.cs` |
| Pudding | `Assets/Scripts/Races/Graphics/Monsters/Pudding.cs` |
| Raptor | `Assets/Scripts/Races/Graphics/Monsters/Raptor.cs` |
| Renamon | `Assets/Scripts/Races/Graphics/Mercs/Renamon.cs` |
| RiverDryad | `Assets/Scripts/Races/Graphics/Monsters/RiverDryad.cs` |
| RwuMercenaries | `Assets/Scripts/Races/Graphics/Mercs/RwuMercenaries.cs` |
| Ryan | `Assets/Scripts/Races/Graphics/UniqueMercs/Ryan.cs` |
| Salix | `Assets/Scripts/Races/Graphics/UniqueMercs/Salix.cs` |
| Schiwardez | `Assets/Scripts/Races/Graphics/Monsters/Schiwardez.cs` |
| Scorch | `Assets/Scripts/Races/Graphics/UniqueMercs/Scorch.cs` |
| Scylla | `Assets/Scripts/Races/Graphics/MainRaces/Scylla.cs` |
| Selicia | `Assets/Scripts/Races/Graphics/UniqueMercs/Selicia.cs` |
| Seraph | `Assets/Scripts/Races/Graphics/UniqueMercs/Seraph.cs` |
| Seville | `Assets/Scripts/Races/Graphics/UniqueMercs/Seville.cs` |
| Sharks | `Assets/Scripts/Races/Graphics/Monsters/Sharks.cs` |
| Singularity | `Assets/Scripts/Races/Graphics/UniqueMercs/Singularity.cs` |
| Skapa | `Assets/Scripts/Races/Graphics/UniqueMercs/Skapa.cs` |
| SlimeQueen | `Assets/Scripts/Races/Graphics/MainRaces/SlimeQueen.cs` |
| Slimes | `Assets/Scripts/Races/Graphics/MainRaces/Slimes.cs` |
| SoulSprite | `Assets/Scripts/Races/Graphics/Monsters/SoulSprite.cs` |
| Succubi | `Assets/Scripts/Races/Graphics/Mercs/Succubi.cs` |
| Taraluxia | `Assets/Scripts/Races/Graphics/UniqueMercs/Taraluxia.cs` |
| Tatltuae | `Assets/Scripts/Races/Graphics/UniqueMercs/Tatltuae.cs` |
| Taurus | `Assets/Scripts/Races/Graphics/MainRaces/Taurus.cs` |
| Tigers | `Assets/Scripts/Races/Graphics/Mercs/Tigers.cs` |
| Umbreon | `Assets/Scripts/Races/Graphics/MainRaces/Umbreon.cs` |
| Vagrants | `Assets/Scripts/Races/Graphics/Monsters/Vagrants.cs` |
| Viisels | `Assets/Scripts/Races/Graphics/Mercs/Viisels.cs` |
| Vipers | `Assets/Scripts/Races/Graphics/Mercs/Vipers.cs` |
| ViraeUltimae | `Assets/Scripts/Races/Graphics/Monsters/ViraeUltimae.cs` |
| Vision | `Assets/Scripts/Races/Graphics/UniqueMercs/Vision.cs` |
| Voilin | `Assets/Scripts/Races/Graphics/Monsters/Voilin.cs` |
| Whisp | `Assets/Scripts/Races/Graphics/Monsters/Whisp.cs` |
| Wolves | `Assets/Scripts/Races/Graphics/MainRaces/Wolves.cs` |
| WoodDryad | `Assets/Scripts/Races/Graphics/Monsters/WoodDryad.cs` |
| Xelhilde | `Assets/Scripts/Races/Graphics/UniqueMercs/Xelhilde.cs` |
| Yordles | `Assets/Scripts/Races/Graphics/MainRaces/Yordles.cs` |
| Youko | `Assets/Scripts/Races/Graphics/Mercs/Youko.cs` |
| Zera | `Assets/Scripts/Races/Graphics/UniqueMercs/Zera.cs` |
| Zoey | `Assets/Scripts/Races/Graphics/UniqueMercs/Zoey.cs` |

## Next implementation packet

Retire the disconnected class payloads as one reviewed group while retaining file paths and legal/modification notices. First scan remaining cast/generic/type-member and serialized type references, including nested types; distinguish class names from enum/GameObject names. Preserve all shared base/helper classes. Then consolidate rejected-only projectile setup/selection branches and orphan helpers, preserving all remaining species effects exactly. Compile and scene checks follow the whole group; no per-species tests. Do not delete/move source paths or sprites, modify bundles, or approve species. If a genuinely shared type is found, retain its exact dependency and document why rather than inventing replacement content.

## Evidence and limits

Inventory: ignored Logs/packet-03b7-source-disposition.json records all paths, external operation candidates and internal inheritance/imports. No source, prefab, scene, package or artwork changed in this review. No compilation or manual test requested/performed for documentation-only work. Existing license/attribution and source notices are untouched; no new source notices apply. This is not phase-2/spec03 completion or release clearance.
