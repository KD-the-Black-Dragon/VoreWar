# 05A7 — residual boundary review and direct metadata lookup

**Date:** 2026-11-01. User accepted 05A6. One source change plus a consolidated residual review. This step does not require another identical screen test; it adds no controls or gameplay rules.

## Current runtime boundary

| Area | Source evidence / disposition |
| --- | --- |
| Old campaign application | StartMode/CreateStrategicGame/GameManager creation/transition routes are blocked; StrategyMode setup/input/scheduler/regeneration are retired. Do not reopen the accepted packets. |
| Old map editor | Both StartMode.MapEditor and GameManager.SwitchToMapEditor are blocked. The former new World(true) route no longer exists; the sole constructor call is standalone CreateTacticalGame. |
| Old world simulation | Eager monster/empire registry and world-order upkeep are retired (05A5). World remains a battle/config/item/snapshot data shape, with nullable compatibility identity fields. |
| Old army lifecycle | Daily recovery, village inventory/XP/traits, training, civilian feeding and spell redistribution are retired (05A5). Grid movement, roster/equipment/capacity/identity helpers remain shared. Prune is still legacy/destructive and is not approved as company survivor/redraw logic. |
| Tactical entry/results | Pure Tactical guard owns current entry; empire traits, strategic buildings, campaign defection/travel/claims/rewards and approved ability alternatives are retired. Results/UI remain. |
| Tactical team/AI providers | Standalone fixed/apparent-side semantics no longer consult empire diplomacy; Unit XP/trait refresh no longer consult strategic AI or empire traits (05A6). Lightweight Army/Empire metadata still supplies names/race/size and does not mean its scheduler runs. |
| Tactical terrain | TacticalMapGenerator consumes a terrain enum and optional standalone village geometry, not StrategyMode/WorldGenerator/World map state. Keep ordinary/village maps, scenery and shared tile data. No reason to erase those reusable algorithms. |
| Strategic grid/render/path code | Retained source, behind retired campaign/editor routes; future company map still intends grid travel. Do not repurpose its old empire-owned UI/controller or delete shared terrain/path algorithms blindly. |
| Persistence | Old World load/preview/quickload/import application is blocked. Existing World-root tactical snapshot/writer remains for later integration; current save files are not a supported resume route. A new campaign format remains future work. |
| Species graphics/assets | Approval registry remains empty. The shared sprite/bundle/clothing provenance inventory still governs retirement; code searches alone do not authorize path deletion or rebuilding bundles with a deleting helper. |

This closes the review of why shared class names remain. It does not declare spec 05/phase 2 complete or claim actual combat has been regression-tested.

## Source change: direct parameter metadata

Modified only `Assets/Scripts/Races/RaceParameters.cs`.

GetRaceTraits(Race) previously cached a short Unit, initialized its random stats/HP/mana/stamina, changed its race and passed it to GetTraitData to select a fixed prototype. That metadata read should not instantiate gameplay state. It now performs the same Config.RaceTraitsEnabled check and exact enum switch directly. GetTraitData(Unit) preserves the old behavior of returning Default when racial traits are disabled (even for null Unit), otherwise delegates using unit.Race. The old null-Unit failure with enabled racial traits remains; no new fallback invented.

Removed only the tempUnit cache, dummy construction and outdated helper comment; retained both method signatures. Exact comparison proves the entire static-constructor payload and the complete switch/case/result block unchanged apart from switching on race instead of unit.Race. All 155 existing prototypes and 154 species mappings, including the 90 rejected definitions, remain pending retirement. Graphics, names, stats, eligibility, override fields and default/unknown-ID behavior are unchanged.

This deliberately removes incidental first-lookup RNG consumption. It does not preserve old seeded random-stream position; no same-seed equivalence claim. Other uses of short Unit(Race) remain outside this metadata helper and were not changed.

## Refreshed species/read blockers

The ignored source-only inventory Logs/packet-05a7-caller-inventory.json records 41 lexical stored-override Get call-site lines, 57 parameter-read lines, one World construction and two new Unit(race) lines elsewhere. Counts include comments/dormant paths and do not claim runtime reachability. Typed getters/internal override calls add further indirect routes.

The old 03B3B report's application/save/world blockers are now retired by 05A1–05A6. Its remaining lookup concern still applies: RaceSettings.Get both returns stored rows and lazily creates missing RaceSettingsItem; that constructor still reads parameter and graphics definitions. Shared consumers include existing Unit/NPC stats, appearance bounds, containment and gated Race Editor metadata. Sanitize skipping rejected rows is not enough to remove definitions, and neither global null nor silent Default substitution is acceptable.

Two nonretired owner mappings still reference rejected destinations (MatronsMinions leader -> Abakhanskya; FeralUmbreon conversion -> FeralEevee). Their old creation/conversion paths are retired, but raw metadata remains. No replacement species or hidden approval is inferred.

## Next grouped implementation

Return to spec 03 with a grouped stored-record versus creation boundary and parameter-registry retirement, using the refreshed caller inventory. Migrate callers that only need existing stored data to a non-instantiating lookup; retain explicit creation only for valid nonretired metadata/developer use. Preserve all nonretired stats and existing player-disabled individuals. Trace any remaining rejected read to its owner before removing matched prototype fields/initializers/cases. Keep enum IDs/raw stored data; no fallback species. Graphics registry and shared assets remain a separate reviewed group. This is the remaining required gate before rejected-stat removal, not a new feature/design question.

## Verification

Compilation PASSED (packet-05a7-compile.log) and explicit Assets/base.unity loading PASSED (packet-05a7-scene.log), both exit 0 with no C# errors or logged exceptions. No new compiler warnings versus 05A6. Exact edit correspondence, unchanged static initialization payload, unchanged scene bytes, scoped whitespace review and preserved License.txt hash passed. Ignored Logs/packet-05a7-* contain before source/scene snapshots and exact edit records/diff. Full static-constructor and dispatch comparisons passed. No new tests/framework, player build, agent Play-mode battle or personal-data access. Actual battle regressions remain pending curation. No additional manual screen check requested for this metadata-only simplification; future grouped behavior changes get a combined check.

## GPL and scope

Read License.txt sections 4/5; preserve its content and upstream attribution. Add actual dated source notice and exact-path CHANGES.md entry. No third-party source/dependency/asset additions or files deleted/moved. Review documentation is not recorded as completed gameplay migration. The 2026-11-01 source notice and CHANGES.md entry cover the actual one-file source diff; existing legal/attribution material is preserved. License.txt matches HEAD. A future distribution requires a separate compliance review.
