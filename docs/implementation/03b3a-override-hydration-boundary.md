# Packet 03B3A — override loading before definition retirement

**Status:** Implemented, 2026-10-30; fresh compilation, explicit scene loading and scope checks passed. Interactive load/persistence review is pending. The user accepted 03B2 startup/name reload and authorized the next step. No species approved.

## Trace and selected boundary

State.LoadEditedRaces deserializes RaceSettings and calls Sanitize; an exception falls back to a new settings object. Sanitize previously requested default mappings and clamped stats for every stored species, including the 90 permanently retired entries. Retiring prototypes first would therefore leave old personal settings coupled to those definitions even though the entries are hidden in the editor.

The first bounded data packet changes **only `Assets/Scripts/Utility/Stored/RaceSettings.cs`**:

- Skip permanently retired keys in both Sanitize passes. Keep their existing stored objects/values untouched; do not delete rows or hydrate them from definitions being retired.
- For new RaceSettingsItem objects, initialize LeaderRace from racePar.LeaderRace and MorphRace from racePar.MorphRace. Both previously copied ConversionRace. SpawnRace and ConversionRace already used their own fields and remain unchanged.

The retirement check uses IsRetired, not IsEnabled/IsCurated. Remaining uncurated metadata and supported existing player-disabled individuals continue through the existing validation. The FeralBats default, nonretired mapping repair/stat clamps, getters, reset methods, serialized field layout and State load/save code remain unchanged. Existing saved leader/morph overrides are not migrated to a guessed value; the constructor correction applies to newly created settings records.

This is preparation for removing definitions, not a claim that all retired data is inaccessible. RaceSettings.Get can still create legacy metadata on direct access; stat prototypes and the eager graphics registry still exist. Their remaining callers must be addressed in the next packets. No constructor-wide exception, generic playable replacement, enum renumbering or character/slot deletion was introduced.

## Remaining mapping evidence

RaceParameters currently has 154 enum cases and 155 RaceTraits prototypes including Default. Every one of the 90 retired entries resolves to a prototype. The explicit cross-species mappings found are:

| Owner | Owner status | Field → target |
|---|---|---|
| Youko | Retired | Spawn → Whisp; conversion → Foxes |
| MatronsMinions | Uncurated | Leader → Abakhanskya (retired) |
| Umbreon | Retired | Conversion → Eevee |
| Equaleon | Retired | Conversion → Eevee |
| Whisp | Retired | Spawn → Youko |
| FeralUmbreon | Uncurated | Conversion → FeralEevee (retired) |
| FeralEqualeon | Retired | Conversion → FeralEevee |
| Cherub | Retired | Spawn → SoulSprite |
| Seraph | Retired | Spawn → SoulSprite |

None of these links was changed or reclassified here. In particular, the two remaining owners must not silently gain a different species as a replacement. New creation/morph/spawn routes have 03A policy gates, but old Empire leader construction and other 4X providers still require 05 retirement. Existing restoration routes and serialized IDs remain stable.

The next definition packet must trace direct RaceSettings.Get callers as well as RaceParameters.GetTraitData/GetRaceTraits. RaceSettingsItem construction also calls Races.GetRace for appearance bounds; skipping a stat prototype alone does not remove the graphics dependency. Split the stats and graphics work, preserving required shared data structures and closing hydration/getter dependencies before retiring definitions.

## Verification

- Fresh Unity 2018.3.12f1 compilation passed without C# errors/import exceptions, exit 0: ignored `Logs/packet-03b3a-compile.log`.
- Subsequent explicit base.unity loading passed without parse errors/exceptions, exit 0: ignored `Logs/packet-03b3a-scene.log`. No agent Play-mode test, new player build or test framework.
- Source correspondence reconstructs the prior file by removing only the two retirement guards/loop adjustment, two corrected constructor assignments and notice. Everything else matches the packet snapshot.
- All 40 OdinSerialize fields/attributes retain their declarations and order. Reference snapshots confirm RaceParameters, State, RaceEditorPanel and SpeciesPolicy are unchanged. No save-schema or lookup policy change is inferred from compilation alone.

No personal settings contents were inspected or changed by the agent; no preset reset/migration was invoked. Runtime slot and mapping persistence still need interactive confirmation. Battle, unsupported legacy-world migration and nonempty curation checks remain deferred. The separate metadata/graphics retirements are not complete.

## GPL and scope

Reviewed License.txt sections 4 and 5. RaceSettings.cs has a dated conversion notice and exact-path CHANGES entry. Existing licensing/attribution remains, with no new third-party code, asset or dependency. No files deleted or moved. Scene bytes match the packet snapshot and License.txt's Git content hash matches the tracked license. Both known Unity-removed orphan metadata files were restored byte-for-byte. Game-file hashes confirm only RaceSettings.cs changed beyond the accepted 03B1/03B2 work; scenes/prefabs/manifests/bundles remain unchanged. Packet diff checks passed. This source-change review is not a full inherited-content or distribution compliance audit.

## User check and next packet

Enter Play mode, open the gated Race Editor and inspect a remaining species you have already viewed. Save without changing its mappings; stop/restart Play mode, reopen the same entry and confirm any explicit mapping choices remain and no new settings-load/Console errors appear. Existing Keep existing mapping behavior remains unchanged; no reset or undo feature was added.

After feedback, refine 03B3B's stat-definition/read-boundary retirement with its exact callers. Keep old graphics/eager-registry/shared clothing changes separate and carry actual strategic providers through 05. No test species or new campaign mechanics are needed for the current check.
