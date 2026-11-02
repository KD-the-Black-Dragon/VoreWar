# 03B3C — Stored override reads versus intentional edits

Date: 2026-11-01. User accepted the preceding screen checks and authorized the next stage. This consolidates the complete direct settings-call group rather than another one-caller packet.

## Completed scope

- `Assets/Scripts/Utility/Stored/RaceSettings.cs`
- `Assets/Scripts/Entities/Units/Unit.cs`
- `Assets/Scripts/Races/Graphics/DefaultRaceData.cs`
- `Assets/Scripts/Entities/Units/PredatorComponent.cs`
- `Assets/Scripts/Entities/MercenaryHouse.cs`
- `Assets/Scripts/Entities/Village.cs`
- `Assets/Scripts/Modes/Strategic/StrategicUtilities.cs`
- `Assets/Scripts/AI/RaceServantTacticalAI.cs`
- `Assets/Scripts/Subwindows/RaceEditorPanel.cs`
- `Assets/Scripts/Subwindows/RandomizerTraitEditor.cs`
- `Assets/Scripts/Subwindows/TaggedTraitEditor.cs`

RaceSettings.Get was a read/write hybrid. It is replaced by GetExisting (returns stored row or null, does not hydrate defaults or graphics) and GetOrCreate (intentional editing, preserves existing rows). All direct callers and internal typed readers now declare which operation they need; the old ambiguous API is removed so compilation exposes missed callers.

- Typed override readers keep their existing record-presence checks and parameter/default fallback. Serialized fields and dictionary identity are unchanged.
- Unit gender/name and graphics fur initialization use existing overrides when present, otherwise the same global gender/fur defaults. Other graphics and growth limits remain guarded by their existing override flags, guaranteeing a stored row before dereference.
- Nine power-read sites across AI, army valuation and old recruitment now share GetPowerAdjustment. Preserve stored multiplier, zero-means-prototype fallback, racial-traits-disabled metadata behavior and calculation formulas; no full settings/graphics record is constructed for valuation. Old dormant callers are migrated too so they cannot reintroduce the hybrid API later.
- Race Editor and tagged-trait editing intentionally create rows. Existing password/session and species selection filters are unchanged. Existing empire banner reads stay non-creating.
- Both SynchronizedEvolution assimilation mutation sites explicitly create a mutable override. Assimilation itself remains; this is not ability retirement.
- Randomizer-trait removal previously constructed a record for every enum value, including rejected and sentinel IDs. It now removes references only from existing records, including retained legacy rows; it does not create records simply to remove an absent custom trait.
- RaceSettingsItem rejects new construction for permanently retired species before parameter or graphics lookup. Existing retired rows remain readable, survive Sanitize, and GetOrCreate returns an existing row without rebuilding it. This uses permanent retirement, not player enablement or curation: existing player-disabled individuals and development access to nonretired species are preserved.

Removing incidental record creation means later typed reads continue using prototype defaults until an intentional edit creates a record, rather than freezing a full copy (including empty override-only trait lists) on an unrelated cosmetic/power read. Existing records, overrides, stats and mappings are not rewritten. This is the intended read boundary, not a promise of identical old incidental hydration state.

## Remaining work

All 90 rejected parameter definitions remain in RaceParameters for now. Direct parameter readers and typed-reader fallback still need a separate explicit unavailable-metadata boundary before removing the matched fields/initializers/cases. Do not claim this packet retires those definitions. Preserve nonretired prototypes, raw mapping IDs and unknown-ID semantics; review the two nonretired mappings to rejected IDs recorded in 05A7. Graphics registries/shared assets require their own ownership pass. No species approved, no substitute species, no new campaign/Downed/HP mechanics.

## Verification and review

Compilation PASSED exit 0 (packet-03b3c-compile.log), with no C# errors/logged exceptions or new compiler warnings versus 05A7. Explicit Assets/base.unity opening PASSED exit 0 (packet-03b3c-scene.log), without logged exceptions/errors. All eleven final source files match the reviewed expected text; BOM/line-ending styles preserved, old direct settings API absent, scene bytes unchanged, and scoped diff whitespace checks passed. Earlier uncommitted work remains intact. Ignored Logs/packet-03b3c-* contain per-file before snapshots, final expected source and diffs. No new tests/framework. Actual battle checks remain blocked by intentionally empty curated roster.

One focused manual check after agent verification: in Race Editor select an available development species, change a harmless cosmetic override, save and revisit it to confirm persistence; restore the preference if desired. Confirm the tabs still open without new red errors. No repeat tour of unrelated settings pages is needed.

## GPL and scope

Read local License.txt sections 4/5. Each of the eleven changed sources has a 2026-11-01 conversion notice and is listed in CHANGES.md; earlier notices preserved. No new external code, assets or dependencies, and no file deletions/moves. Dated notice coverage matches the eleven-file source diff, CHANGES.md lists exact source paths, prior attribution is preserved, and License.txt matches HEAD. No scene changes or unrelated new file changes found. This is an ongoing modification-notice check, not full binary/source distribution clearance; distribution obligations and unresolved shared-asset provenance still require review before release.


User accepted the focused Race Editor checks. [03B3D](03b3d-rejected-stat-definition-retirement.md) then retires all 90 rejected stat prototypes with a central explicit-unavailable metadata boundary; this report's remaining-work section describes its earlier checkpoint.
