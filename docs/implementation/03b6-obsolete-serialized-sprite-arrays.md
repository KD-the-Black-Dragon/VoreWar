# 03B6 — Obsolete serialized sprite arrays

Date: 2026-11-01. User accepted 03B5's runtime startup check, paused for usage recharge, then explicitly resumed. Do not repeat the accepted loader stage.

## Changes

- Added `Assets/Editor/SpeciesResourceAudit.cs` plus its `.meta`: a read-only Unity editor inspection command retained for resource work. It writes fixed report names under project Logs; does not save/import/rebuild assets, modify preferences or run gameplay. No test framework or new gameplay tests were added.
- Edited `Assets/Prefabs/Sprite Dictionary.prefab`: removed exactly 156 saved top-level field blocks corresponding to the 156 retired-only static bindings from 03B5. Every selected block was checked against the actual Unity SerializedObject before editing: static field true, serialized property absent. Removed 9,478 lines containing 9,181 raw GUID references. All other bytes, component/script/object identities and field blocks are preserved.
- No SpriteDictionary declaration, runtime loader, species stats/registry, base.unity, source artwork, metadata for existing assets, bundle or manifest changed. No path deleted or moved. No species approved.

## Engine evidence, not only text searches

The earlier 372-sheet prefab GUID inventory counted raw YAML text, not active runtime dependencies. Unity 2018.3.12f1 reports 49 serialized component fields, 650 object-reference records and 22 asset dependencies, including 20 PNG images. None of the 156 selected old arrays is a serialized property. Complete before/after report objects are identical, including all 413 reflected public field records (static and instance), property array sizes, object-reference paths/names and sorted dependencies.

The old GUID count therefore cannot justify deleting/repacking those sheets or claiming texture-memory savings. This packet removes obsolete serialized text only. The four mixed bundle loads and remaining loader bindings continue as recorded in 03B5. Other stale static blocks, generic/shared fields and rejected-looking filenames are intentionally outside this bounded change.

## Inspection command

Use the installed Unity editor with this project, batch mode and `-executeMethod SpeciesResourceAudit.CaptureBefore` or `SpeciesResourceAudit.CaptureAfter`. They write `Logs/packet-03b6-unity-resource-before.json` and `Logs/packet-03b6-unity-resource-after.json`. They intentionally inspect only the Sprite Dictionary prefab. Their before/after names are fixed; rerunning overwrites those local inspection reports, not assets. The command does not establish dependencies for arbitrary binary/Odin saves or bundled content.

## Cleared obsolete blocks

- `Mermen2`
- `NewimpVore`
- `NewimpGloves`
- `NewimpLegs`
- `NewimpUBottoms`
- `NewimpUTops`
- `NewimpOBottoms`
- `NewImpOTops`
- `NewimpOnePieces`
- `NewImpOverOnePieces`
- `NewimpHats`
- `Gobbo`
- `Gobbovore`
- `Gobbglove`
- `Gobleggo`
- `Gobbunderbottoms`
- `Gobundertops`
- `Gobboverbottoms`
- `Gobbovertops`
- `Gobbunderonepieces`
- `Gobboveronepieces`
- `Gobbohat`
- `HumenUniform1`
- `HumenUniform2`
- `HumenBigLoin`
- `HumenPants`
- `HumenSkirt`
- `AlrauneChristmas`
- `ImpGobHat`
- `SlimeQueen`
- `Lamia`
- `LamiaScales`
- `LamiaScalesBits`
- `Cows`
- `Driders`
- `Alraune`
- `Mermen`
- `BearsClothes`
- `Eevee`
- `Umbreon`
- `Umbreon2`
- `Umbreon3`
- `Puca`
- `DewSprite`
- `Vipers2`
- `Vipers3`
- `Vipers4`
- `Cockatrice1`
- `Hamsters`
- `RwuMercenaries1`
- `RwuMercenaries2`
- `RwuMercenaries3`
- `RwuMercenaries4`
- `RwuMercenaries5`
- `RwuMercenaries6`
- `RwuMercenaries7`
- `RwuMercenaries8a`
- `RwuMercenaries8b`
- `RenamonBodies`
- `RenamonClothes1`
- `RenamonClothes2`
- `RenamonClothes3`
- `RenamonVore`
- `OoviKatFemale`
- `OoviKatMale`
- `OoviKatCustomisation`
- `CowsSeliciaBelly`
- `YoukoTails`
- `Aabayx`
- `AabayxFacePaint`
- `CentaurTorsoAddOns`
- `MainlandElfParts`
- `MainlandElfClothes`
- `Ghosts1`
- `Ghosts2`
- `Ghosts3`
- `GhostsVoreSprites`
- `YordlesBodies`
- `YordlesVoreSkin`
- `YordlesVoreFur`
- `YordlesCustomization1`
- `YordleClothes1`
- `YordleClothes2`
- `Selicia`
- `Vision`
- `Ki`
- `Scorch`
- `AsuraHoliday`
- `DRACO`
- `Zoey`
- `ZoeyHoliday`
- `Cierihaka`
- `Zera240`
- `ZeraBelly`
- `Auri`
- `AuriHoliday`
- `AuriVore`
- `Erin`
- `Salix`
- `SalixVore`
- `SalixGen`
- `BellaBody1`
- `BellaArm1`
- `BellaHead1`
- `BellaBody2`
- `BellaArm2`
- `BellaHead2`
- `BellaRobe1`
- `BellaRobe2`
- `Singularity1`
- `Singularity2`
- `Feit`
- `Taraluxia`
- `TaraTailStrikes`
- `Xelhilde`
- `Olivia`
- `OliviaVoreSprites`
- `Skapa`
- `TatltuaeHackleVore`
- `Seville`
- `Nectar`
- `Ryan`
- `RyanVore`
- `Konane`
- `Cherub`
- `Seraph`
- `Abakhanskya`
- `Vagrants`
- `Vagrants2`
- `Vagrants3`
- `Compy`
- `Shark`
- `DarkSwallower`
- `Collector`
- `Voilin`
- `Frogs`
- `Fairy`
- `FairyExtraHair`
- `Fairy240`
- `Raptor`
- `Earthworms`
- `Schiwardez`
- `Whisp`
- `FeralOrcas`
- `BoomBunnies`
- `ViraeUltimae`
- `Viisels`
- `FeralEevee`
- `FeralEqualeon`
- `Iliijiith`
- `Pudding`
- `DryadSprites1`
- `DryadSprites2`
- `DryadSprites4`
- `Cake`
- `SoulSprite`

## Dependencies Unity actually reports (unchanged)

- `Assets/Prefabs/Sprite Dictionary.prefab`
- `Assets/Scripts/Races/Graphics/SpriteDictionary.cs`
- `Assets/Sprites/Bones2.png`
- `Assets/Sprites/Bones3.png`
- `Assets/Sprites/Scat.png`
- `Assets/Sprites/TacticalEffects/SpellIcons.png`
- `Assets/Sprites/TacticalEffects/SpellIcons_7.png`
- `Assets/Sprites/Units/Dryad/SmoothVoreSprites.png`
- `Assets/Sprites/Units/FurryGenitals.png`
- `Assets/Sprites/Units/NewClothes1.png`
- `Assets/Sprites/Units/SmoothBellies.png`
- `Assets/Sprites/Units/Sprites.png`
- `Assets/Sprites/Units/Vision.png`
- `Assets/Sprites/Units/black_top.png`
- `Assets/Sprites/Units/eyebrows.png`
- `Assets/Sprites/Units/privates.png`
- `Assets/Sprites/birdscat.png`
- `Assets/Sprites/bones1.png`
- `Assets/Sprites/clean_disposal.png`
- `Assets/Sprites/scat_v2.png`
- `Assets/Sprites/scat_viral_v2.png`
- `Assets/Sprites/surr_flag.png`

## Verification

Before and after batch compile/inspection runs PASSED exit 0 without C# errors/logged exceptions; no new compiler warnings versus 03B5. Logs: packet-03b6-audit-before.log and packet-03b6-audit-after.log. Complete JSON snapshots compared equal. Source-based exact-span reconstruction additionally proves all other prefab bytes preserved. Source/artifact snapshots and removed-span records are in ignored Logs/packet-03b6-*.

Explicit Assets/base.unity opening PASSED exit 0 (Logs/packet-03b6-scene.log), without errors/exceptions. Exact removed-span reconstruction proves all other prefab bytes unchanged; all 1247 other resource hashes from the prior inventory match. Scoped diff whitespace, new source notice/placement, unchanged License.txt hash and new editor metadata scope checks passed. No Unity process remains and previous uncommitted work is preserved. No player build or Play-mode battle; approved roster remains empty. No new manual test requested: this cleanup has identical engine-visible data/dependencies, and the user already accepted runtime startup for 03B5.

## Remaining work and next packet

Next grouped review should close remaining disconnected rejected definition source and species-specific effect/helper branches, accounting for shared clothing/base classes before retiring their source payloads. Preserve file paths/legal notices under the no-delete rule; do not remove/repack artwork simply because its old serialized block is gone. Shared resource provenance and actual bundle membership remain release concerns. This does not declare spec 03 or phase 2 complete or authorize new company/Downed/HP mechanics.

## GPL modification check

Read License.txt sections 4/5. New inspection source is marked as an addition by this fork dated 2026-11-01; CHANGES.md identifies it and the exact prefab/meta paths with associated non-commentable-format notice. Existing licensing/attribution preserved. No external code/dependency/asset introduced or copied. Final scope, notice/CHANGES coverage, prefab byte reconstruction, resource hashes and unchanged License.txt hash checks passed. Distribution obligations and unresolved artwork provenance require separate review before release; this inspection does not establish their license compatibility.
