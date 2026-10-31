# Packet 03B — rejected-species dependency and ownership preflight

**Status:** Study complete, 2026-10-30, following the user's acceptance of 04B2. Subsequently accepted by the user; [03B1](03b1-retired-metadata-catalogs.md) implements the first packet, including the nested tag list found during tracing. 03B1 is accepted; [03B2](03b2-rejected-name-sources.md) implements rejected name sourcing with compile/scene checks passed and startup/reload review pending. Documentation only. No game source, scene, prefab, sprite, bundle, name list, personal file or license text was changed. This report proposes the next bounded implementation packets; it does not mark rejected code/assets physically removed or the full species-generation invariant complete.

## Result

All 90 rejected entries still match the explicit retirement policy and their existing numeric IDs, registry aliases and source classes. The remaining work is broader than taking entries out of a playable list: the game eagerly constructs definitions, imports names for the entire enum, builds shared clothing catalogs, permits developer metadata inspection, and carries old strategic generation providers. These must be disconnected before their definitions/resources can be retired safely.

Two current inventories accompany this report:

- [90-entry dependency matrix](03b-rejected-species-dependency-matrix.md): current registry/class/base mappings, lexical enum-reference and direct typed-field consumers, sprite sharing and seeded-name candidates.
- [Shared sprite, bundle and serialized-reference inventory](03b-shared-sprite-and-bundle-inventory.md): source/metadata availability for all four bundles, direct shared sprite consumers and Unity text-reference candidates.

These complement the historical [01B removal map](../architecture/01b-species-removal-map.md), rather than treating its pre-reset counts as current evidence. No remaining species is approved merely because it is absent from the rejected set.

## Definition and metadata dependencies

| Area | Current source finding | Required retirement boundary |
|---|---|---|
| Graphics/default-data registry | `Assets/Scripts/Races/Races.cs` declares 157 eagerly initialized fields and maps 154 enum cases. Three extra fields are BlankSlate and the two leader variants. Aliases include Taurus → Cows, Auri → Aurilika, Merfolk → Mermen and FeralSharks → Sharks. | Replace rejected registry construction/retrieval and direct typed consumers together. Preserve IDs and supported existing uncurated/disabled metadata; do not use a playable fallback or a blanket constructor exception. |
| Leader variants and inheritance | GetRace(Unit) selects SlimeQueen for a Slimes leader and AntQueen for an Ants leader. SlimeQueen inherits Slimes; Youko inherits Humans; Centaur inherits TaurHumanHalf, which inherits BlankSlate. | SlimeQueen follows the rejected Slimes dependency. AntQueen/Ants remain uncurated, not implicitly rejected. Keep shared bases until every dependent class is accounted for. |
| Stats/traits | `Assets/Scripts/Races/RaceParameters.cs` has a static constructor assigning species RaceTraits, including stats, innate spells, descriptions, transformation destinations and leader/spawn traits. GetRaceTraits uses a short metadata Unit and GetTraitData dispatches on its species. | Retire rejected parameter definitions/mappings in a separate code batch, after consumers have explicit unavailable handling. Preserve required shared stat structures and unrelated balance values. No new injury/downed rule belongs here. |
| Overrides | `Assets/Scripts/Utility/Stored/RaceSettings.cs` lazily creates RaceSettingsItem through Get; its constructor requests both RaceParameters and Races data. Sanitize revisits old mappings and injects a FeralBats default. | Changing the renderer registry alone can break metadata loading. Old slots/presets must not repopulate retired definitions; do not delete individuals or personal data as a shortcut. The reviewed 03A restoration rules still apply to existing supported individuals. |
| Developer catalogs | `Assets/Scripts/Subwindows/RaceEditorPanel.cs` builds species and four mapping dropdowns from the whole enum. Bulk trait/tag operations also traverse the whole enum. `Assets/Scripts/UI/HoveringRacePicture.cs` deliberately allows metadata preview in an authorized editor session. | Exclude permanently retired entries from catalogs, bulk edits and developer previews before dropping their data. Preserve all three tabs and inspection of remaining uncurated candidates; editor access must never grant gameplay approval. |
| Mapping persistence | Race Editor reads dropdown names with Enum.TryParse and searches options to display existing mappings. Removing an option may leave a previous selection displayed if its stored mapping has no match. | Handle a retired/missing mapping explicitly; do not silently rewrite it to the first remaining species on Save. Retain sentinel/absence semantics and review old-slot load/round-trip behavior. |

The distinction between permanently retired content and merely uncurated/player-disabled content is essential. A helper exposing permanent retirement is a reasonable next implementation seam. IsEnabled/IsCurated alone cannot serve as a metadata-retirement predicate: the curated registry is intentionally empty, and player disabling must preserve existing individuals.

## Graphics and clothing are shared infrastructure

The named rejected classes directly reference 204 distinct SpriteDictionary fields; 45 have lexical consumers outside those 90 definition files. These counts omit inherited and dynamic dependencies and therefore are not an exclusivity proof.

Examples traced in source:

- HumansBodySprites2 and HumansVoreSprites are also used by uncurated DemiDragons/Jackals and the TaurHumanHalf helper. Removing the Humans species does not establish that those sheets are removable.
- Cockatrice uses Avians3/Avians4 and Sharks3/Sharks5; those arrays also serve other uncurated species. The user's eventual favorites cannot be inferred from these technical dependencies.
- Cats, Dogs, Foxes, Wolves and Tigers use common/default rendering and scene arrays; lack of a named bundle assignment does not mean they have no art dependencies.
- `Assets/Scripts/Entities/Clothing/ClothingDiscards.cs` builds one shared catalog from generic clothing, RaceSpecificClothing, TaurusClothes/CruxClothing and many typed Races fields, including rejected Humans/Imps/Goblins. This occurs regardless of which discarded item is being rendered once the catalog is built. Merely making species registry access lazy would still allow this path to construct rejected definitions.
- `Assets/Scripts/Subwindows/UnitCustomizer.cs` compares selected data against typed SlimeQueen, Imps and Goblins instances for extra clothing controls. `Assets/Scripts/Modes/Tactical/TacticalGraphicalEffects.cs` calls Races.Slimes for an accent material. Other methods use enum-specific weapon sprites. These are separate consumers from the main GetRace switch.

The shared DefaultRaceData/BlankSlate classes, clothing types, palettes, renderer utilities and per-species self-references need explicit dispositions before leaf class bodies become inactive. Do not replace every rejected class with a broad generic renderer while leaving valid creation paths behind.

## Bundles, source assets and serialized references

`Assets/Scripts/Core/AssetBundleLoader.cs` opens all four bundles unconditionally and contains 362 matched named sprite-field loads. The four manifests list **383 source sheets**: empire 184, merc 50, monster 94, specialmerc 55. Every listed source file and metadata file is present, and every inspected assetBundleName matches its manifest group. This improves the evidence for a future controlled repack; no rebuild was attempted and binary contents/platform compatibility were not verified.

A scan of 201 readable Unity serialized text files found direct source GUID references for **372 of those sheets**. The Sprite Dictionary prefab accounts for all 372; base.unity references nine, and two tactical effect prefabs each reference one (counts overlap). A bundle-entry change therefore does not close the serialized-reference audit.

Some references may be obsolete serialization rather than live instance fields: the prefab still records arrays such as HumansBodySprites2 and Vision, while their current C# fields are static and loaded by AssetBundleLoader. Keep this distinction explicit. A GUID hit establishes a reference in the file; it does not prove that Unity currently deserializes or renders that field. Review inherited prefab state, actual component fields, GUID/fileID usage and startup behavior before clearing any arrays. Preserve still-used instance arrays such as Bodies.

The editor utility `Assets/Editor/AssetBundleHandler.cs` offers both a build and a hard rebuild. **The hard rebuild calls Directory.Delete recursively. Do not invoke it under AGENTS.md.** A later packing packet should use a verified, separate staging output and validate it before proposing any active-load switch. The ordinary builder also writes the current output and is not an appropriate read-only preflight operation. Existing bundle manifests report no bundle dependencies, which does not prove per-species exclusivity within them.

No ownership or license conclusion follows merely from a species name or a bundle manifest. Preserve artwork credits and per-asset terms when later changing packaging. This packet introduces no new art or dependency and does not authorize redistribution or whole-file moves/deletions.

## Names and other providers

`Assets/Scripts/Core/State.cs` declares 217 distinct seeded name-list basenames. Startup and reset paths copy missing shipped lists; ResetNamelists replaces existing personal lists using deletion and copying. That maintenance action was inspected, not invoked. No personal data was read or changed.

`Assets/Scripts/Utility/Texts/NameGenerator.cs` constructs hardcoded species town/monster name lists, reads generic personal lists, parses army names and loops across the entire Race enum for male/female/monster files. Permanently rejected names can therefore still be seeded and imported even though new playable generation is blocked. GetMonsterName also has rejected-specific fallback pools; GetTownName embeds old faction flavor and a species-label fallback.

Retire exact rejected name seed/read routes and their unused built-in pools together, retaining generic names and explicit data needed by uncurated candidates. Do not delete old user files or reset all names to suppress loading. Town naming will ultimately belong to the procedural company-region implementation in 06; cleaning rejected names does not implement that generator.

MercenaryHouse.UpdateStaticStock already stops named-unique creation. Its private CreateUniqueMercenary helper has no discovered call sites and still contains the old SpecialMercenary construction path. It is a good small retirement candidate; changing its caller guards alone is not physical source retirement. Existing named-special combat behavior and references elsewhere remain separate dependency work.

## Remaining 4X and combat providers

The retired event catalogs and narrative dummies no longer supply their old creation paths after 04A/04B. Reviewed tactical tower/encampment/copy/summon routes retain 03A2's policy gates; those old building providers still need 05 retirement rather than expanded company support.

World.InitializeMonsters still builds a fixed MonsterEmpire array, with rejected entries and index assumptions. Village production, GoblinAI, old recruitment and Empire.GenerateLeader have legacy construction paths. The gameplay editor/start guards and empty roster are not proof that those bodies are gone. The skirmish World(false) constructor avoids the strategic-generation constructor, but loaded legacy worlds and regeneration paths require the explicit 05 application/save boundary. Do not turn unsupported old empire saves into an implicit new-campaign migration.

Species-specific revival, conversion, permanent polymorph and leadership behavior is already scheduled for 05/10. Removing prose did not remove those mechanics. Keep their approved retirement coordinated across trait/spell/equipment providers; temporary summons/illusions are distinct. No new company rules or battle-balance changes are needed to finish this preflight.

## Recommended implementation packets

These packets follow dependencies rather than asking the user to classify 90 species individually. Each implementation needs its own dated notices, exact-path CHANGES entry, focused source review, Unity compilation/scene load, and appropriate user checks. Do not begin the next implementation packet before this staged review is accepted.

1. **03B1 — Retired metadata catalogs and named-unique factory.** Expose a read-only permanent-retirement predicate; apply it consistently to Race Editor catalogs/bulk operations and preview entry. Preserve the password and three tabs, uncurated inspection, mapping round-trip safety and empty gameplay approval. Retire the uncalled unique factory body while preserving existing individuals/data. Keep scene/resources unchanged. Check developer catalogs, normal settings and empty Pure Tactical.
2. **03B2 — Retired name sources.** Remove rejected seed/import routes and their exclusive built-in name pools, keeping generic names and remaining candidates. No user-file deletion/reset. Check startup and settings maintenance paths without invoking destructive resets; record any further maintenance adjustment as explicit scope.
3. **03B3 — Data/renderer registry and typed consumers.** Refine into separate stats and graphics subpackets. Retire eager rejected construction and rejected retrieval, replace direct mixed clothing/material/customizer dependencies, and account for leader variants and inheritance. Define safe unavailable metadata handling before altering Races/RaceParameters/RaceSettings. Preserve ID identity and restoration for supported disabled individuals; no uncurated gameplay fallback.
4. **03B4 — Retire disconnected leaf content.** In dependency groups, replace unused rejected species-specific code/parameters and exclusive helpers with inactive content/notices as appropriate, retaining attribution and paths under the no-delete rule. Keep shared generic resources. Retain necessary enum tombstones with documented reasons; a tombstone is not an approved species.
5. **03B5 — Shared resource/serialization and packing closure.** Reconcile field ownership with prefab/scene arrays and metadata; stage and verify new bundles if needed, then review the active-load change. No whole-file removal/move is implied. If old 4X consumers block a cleanup, carry that item through 05 and return to it before species curation.

03B3 and 03B4 must remain small reviewable groups rather than one sweeping rewrite. When an old-world dependency prevents a clean source retirement, record its exact owner and finish it with 05. The curation step stays later; no first species, sapient/animal assignment or balance content was selected here.

## Performed checks and limits

This is a source/asset-reference study. The 90-row mapping was checked against SpeciesPolicy, Races registry cases/classes and source paths; bundle source/metadata presence and labels were inspected; source consumers and serialized GUID candidates were inventoried. Details are retained in ignored `Logs/packet-03b-preflight-inventory.json`. These are source correspondences, not automated gameplay tests or an exhaustive reflection/binary/save audit.

All 992 recorded game-source/scene/prefab/manifest/bundle content hashes match the start of this preflight. The two known tracked orphan metadata files removed by Unity during the preceding user manual run were restored byte-for-byte from their tracked content; no user source changes were reverted. No Unity compilation, asset import, bundle rebuild, Play-mode test or new player build was needed or performed for documentation-only changes. The user's accepted 04B2 smoke result is recorded without extending it into nonempty battle testing.

CHANGES.md records this as documentation only. Existing source modification notices, License.txt, copyright and contribution notices remain intact; no headers were added to untouched source. Later actual source retirement still requires the AGENTS.md GPL change workflow and a separate distribution review where applicable.
