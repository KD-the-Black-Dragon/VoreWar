# 05A2 — strategic scheduler retirement

**Date:** 2026-10-31. Implemented following user acceptance of all 05A1 checks. Final compilation and explicit scene loading passed, exit 0, without C# errors/import exceptions/scene parse errors. Interactive smoke check pending.

## Changed sources

- `Assets/Scripts/Modes/StrategyMode.cs`: retire Setup, Regenerate, ButtonCallback, BeginTurn and ReceiveInput bodies, retaining callback signatures for existing scene/external bindings. Remove private AI, EndTurn, ProcessIncome, ProcessGrowth, VictoryCheck and CheckIfLastHumanPlayerEliminated methods after their callers were retired. Remove the now-unused AI timer and write-only subWindowUp/buildingRangeOn fields/assignments.
- `Assets/Scripts/Core/GameManager.cs`: prevent strategic scene activation. Preserve the existing PureTactical return-to-main-menu branch verbatim; other attempts report the shared campaign-unavailable message without switching scenes.

This stops the old turn driver, strategic AI dispatch, income/auto-dismissal, research/growth scheduling, turn autosave and territorial victory progression at their owning scheduler. Stale regeneration/menu callbacks cannot run their former pruning/income/leveling effects. UI and keyboard strategy entry bodies no longer dispatch old actions.

Map/terrain, rendering/helper methods, cleanup, units/stats, tactical AI, tactical turn handling and snapshots are retained. Files and scene/prefab bindings remain in place. No new company clock, income, saving, health/downed rule or species approval is introduced.

## Evidence and limits

The initial source comparison matched 85 unaffected method bodies and the complete PureTactical return branch to packet snapshots. Final correspondence permits only removal of obsolete subWindowUp/buildingRangeOn assignments from those methods; all other content is preserved. No EndTurn/AI/economy/victory scheduler calls remain in StrategyMode. Source search found no remaining explicit strategy activation/scene assignment in Assets/Scripts.

Fresh final compilation: ignored Logs/packet-05a2-compile-confirm.log. Earlier unsuffixed compile predates unused-state removal. Explicit scene-load evidence: Logs/packet-05a2-scene.log. Completed results are recorded at handoff. Existing unrelated upstream compiler warnings are not claimed fixed. No agent Play-mode test, new player build or new tests/framework; real tactical exit testing awaits approved species.

This is scheduler retirement, not complete deletion of strategic helpers/providers. Standalone map-editor revival, movement/building UI helpers, World/Empire/Village provider internals and campaign-specific battle result logic remain for separate review. New campaign result ownership must not be inferred from the preserved skirmish exit. Legacy save resume remains unavailable after 05A1. Retired stat/graphics prototypes remain until read dependencies are closed.

## GPL and scope

Reviewed License.txt sections 4/5. Both C# sources have dated conversion notices, preserving prior notices/attribution and exact-path CHANGES coverage. No external code, assets/dependencies or license changes. No files deleted or moved. Scene bytes and license content are compared against packet/tracked content; obsolete orphan metadata is not recreated. This is not a full release/distribution compliance audit.

## User checks / next work

Open Play mode: main menu, retained Content Settings and gated Race Editor should still open normally. Pure Tactical should retain its empty-roster explanation. Old campaign creation/load should keep 05A1's unavailable messages. No valid battle is required, and no sample species is enabled.

After feedback, audit the remaining World/Empire/Village creation and tactical provider/result boundary as a bounded 05A3 packet before stat prototype removal. Preserve reusable map/combat helpers and numeric identity. Do not replace rejected species with generic playable data or introduce the new campaign during cleanup.

The user confirmed all requested 05A2 manual checks passed and authorized 05A3A.
