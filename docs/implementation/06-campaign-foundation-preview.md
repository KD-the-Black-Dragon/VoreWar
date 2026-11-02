# 06 — Campaign foundation preview

Date: 2026-11-01. Larger connected milestone authorized after functional cleanup. Implementation includes new campaign data/world/clock/checkpoint/navigation together; runtime review pending. No species selected, no placeholder soldiers, no playable leader/company campaign claimed.

## Entry and separation

StartMode.CreateStrategic now opens CompanyCampaignView through the existing main-menu campaign button, without CreateStrategicGame, World, Empire, Army.Refresh, State.Load or strategic scheduling. Existing scene/prefab/button bindings stay unchanged. StartMode input yields while the overlay is open and ReturnToStart closes it. A new runtime overlay Canvas uses existing Unity UI and built-in Arial; no assets/packages/fonts copied. Pure Tactical, content preferences and Race Editor stay on their earlier paths. The old Load Game menu still rejects old saves; the new preview owns its separate Load latest preview button.

Actual character-dependent starts remain unavailable until07. The preview is explicitly labeled on setup/map/town/company pages, creates zero Units, and saves Preview=true so it cannot silently become a real company. Future creator integration must build a validated leader registry and transition through an explicit migration/creation path.

## State and map

CompanyCampaign owns schema/ID/creation mode, seed and persisted xorshift RNG, actual terrain/discovery arrays, position, day/actions, treasury/payday date, town records, character registry, member-ID references/leaderID and interrupted day/rest cursor. CompanyCharacter wraps canonical retained Unit plus ID/biography/debt; actual Unit serialization and identity integration need07's nonempty roster checks. Validation rejects duplicate character IDs/Unit references, duplicate/missing members, retired species, invalid dimensions/ranges/towns and missing real leader. Player enablement is intentionally not a load validator for existing nonretired individuals.

The region uses the existing seeded OpenSimplex noise utility through an independent grid provider: old WorldGenerator mixes global configuration, village/empire placement and random sources, so it is not called. Persist actual arrays rather than regenerate from a seed on load. Provisional review values:20x12 grid,6 distinct numbered towns, radius2 discovery,10actions/day, grass/desert1cost and forest/hills2cost. All current preview terrain is traversable, guaranteeing town connectivity. These are configurable source/model values, not final balance/species decisions. No enemy targets/encounters, water-blocking/pathfinder or auto-route feature is claimed.

Click adjacent cells for movement; visibility remains discovered after leaving. One moving company marker, static town markers and persistent empty-contract area. Only the current town can be entered. Costed actions check affordability; zero actions does not end a day. Existing zero-cost navigation remains accessible. TownEntryCost is a source-owned default0 hook. No service purchase/acceptance costs are hardcoded as future design decisions.

## Clock and rest

One CompanyClock path advances elapsed days, with fixed stage order: wages, deadlines, refresh, recovery, sparring. Register future ICompanyDailySystem implementations by stage; none is installed for this foundation, so no wages/healing/contracts are simulated. First payday date is elapsed7 (display starts Day1 at elapsed0).

DailyStage/UpdatingDay/PendingDecision/RestDaysRemaining persist. A stage cursor advances only after its campaign-state transaction succeeds; on exception the root is restored from a serialized snapshot. Systems must mutate only the campaign root, not perform irreversible external side effects or retain stale object references after rollback. Pending decisions interrupt before later stages/rest days; future service-owned decision UI must resolve them before Resume. Explicit resume continues a started day without incrementing it again. Load/redraw/navigation never ticks a day.

Rest uses EndDay repeatedly and checkpoints each completed day, stopping on decisions/failure. Active interrupted-rest UI/policies will be integrated when11 supplies actual recovery/services; the preview's1/3/7day rest controls exercise only time. Battle time remains disconnected from this clock.

## Checkpoints

Separate application persistentDataPath/CompanyCampaigns/<companyID>/ immutable *.company generations. Header magic/schema, size bound, SHA256 and validated deserialization before live preview installation. Existing Odin binary serializer reused; no old World migrations/config install. Persist generated map, discoveries, RNG and clock rather than seed alone. No promise of equivalence across changed content/generator/schema versions; unsupported schema rejected.

Each write uses a new name/CreateNew, flushes to disk and rereads/validates before reporting success. Incomplete/corrupt newest generations are skipped within the selected company; no silent switch to another campaign on recovery. All previous generations retained under the no-delete rule (not pruned to a fixed count); no backup rotation deletion. Timestamps plus GUID suffix name generations; newest normal/ironman selection is immutable filename order. Storage use grows until an authorized retention policy is added.

The preview exposes latest-preview loading only, not named save slots or a full campaign browser. Normal saves can be made during travel; every EndDay saves both modes. Ironman is chosen on creation, has no midrun toggle/manual midturn checkpoint, and loads the latest valid generation. Save failure blocks further travel/day advancement until successful retry; earlier files preserved. No tactical checkpoint is implemented: between-battle save boundary is the current intended fallback until10/12 integrates combat. Save/load of actual Units, nonempty rosters, future service decisions and full Ironman regressions remain explicitly pending their milestones.

## Exact source and metadata paths

- Assets/Scripts/Core/CompanyCampaign.cs (+.meta)
- Assets/Scripts/Core/CompanyCampaignSave.cs (+.meta)
- Assets/Scripts/Subwindows/CompanyCampaignView.cs (+.meta)
- Assets/Scripts/Modes/StartMode.cs

## Verification

Initial compile exposed multiline string literal generation errors; corrected. Subsequent compile passes recorded in Logs/spec06-compile-final.log and compile-complete.log. Final checkpoint-selection safeguard compile PASSED exit0: spec06-compile-handoff.log, no C# errors or logged exceptions. Ten new CS0649 warnings describe currently unassigned reserved character/economy/decision fields (Logs/spec06-warning-delta.json); no claim of warning-free compilation. Source hashes/StartMode before snapshot under Logs/spec06-*. No new tests/framework introduced. No agent Play-mode/visual inspection or save round-trip claimed. Runtime movement/rest/map layout/checkpoint/recovery checks require the combined manual workflow below; compilation alone does not prove them. Explicit base.unity load PASSED exit0 (spec06-scene.log), without errors/exceptions. Source notice/encoding/whitespace/dependency and scene/prefab byte checks passed; License.txt hash matches HEAD. New source hashes refreshed after the final selection safeguard. No Unity process remains; previous user work preserved.

## Combined manual workflow

1. Main menu campaign button opens the labeled preview. Generate a normal map; check company marker, nearby discovered town/tiles, unknown tiles elsewhere, empty contract area.
2. Move between adjacent tiles, confirm costs and persistent discovery. Spend down actions; no free movement or automatic day advance. End Day refills exactly once.
3. Enter a town at its tile; visit Company and return to the map without changing day/actions except configured entry cost. No fictitious roster/services.
4. Save, note location/day/actions/discovery, return to main menu and Load latest preview. Confirm the values/map stay the same. Quit/reopen and load again if convenient.
5. Rest3days advances exactly3 and creates checkpoints. Generate a separate Ironman preview; mode stays fixed, manual midturn save disabled, EndDay saves. No test of old-world import requested.
6. Recovery can be tested later with agent-assisted non-destructive file edits; do not delete personal saves or ask the user to corrupt files manually. Save-failure/resumable service and actual-character cases remain pending meaningful automation/integration, not assumed passed.

## GPL and scope

New source files marked additions by this fork with actual2026-11-01 date; StartMode retains prior notices and adds current change. CHANGES identifies exact source/meta paths. No introduced/copied external code/assets/dependencies; existing noise/serializer reused without relabeling their terms. License.txt/attribution preserved; newmeta associated notice recorded. Distribution/asset-provenance review remains separate. No scene/prefab edits, path deletion/move or changes to earlier cleanup work intended.


## User acceptance and presentation requirement

User confirmed the preview operates correctly and approved it. The lettered grid is mechanics-only presentation. Preserve and reuse the existing 4X terrain artwork for the final company world map; the user explicitly reconfirmed this requirement. StrategicTileDictionary and its serialized terrain sprites remain intact. Rendering must consume the new campaign state without reactivating old empire turns/ownership. No Ironman corruption recovery, actual-character serialization or battle checks are inferred from general preview acceptance.
