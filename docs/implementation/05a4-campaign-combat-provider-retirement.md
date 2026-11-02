# 05A4 — campaign combat-provider retirement

**Date:** 2026-11-01. User accepted the complete 05A3B17 screen checklist. Grouped source cleanup; modified only `Assets/Scripts/Modes/TacticalMode.cs`.

## Completed scope

Remove the empire-trait injection block (28 lines), strategic-building attacker/defender startup effects (587 lines), building result bookkeeping (32 lines) and activation wait coroutines (15 lines), plus their private fields/counters/report fragments and unused collection import. This closes BlackMagicTower afflictions/soul collection, BarrierTower core prompts, CasterTower temporary mages/spell charges and DefenseEncampment reinforcement/return hooks. These startup effects were gated off in Pure Tactical; their fields had no live assignment after 05A3A. Results no longer need those nullable building collections. The generic pause field/control remains because tactical pause still uses it.

Disconnect DefectProcessor construction/checks/reporting, all gated campaign defection setup, and its extra-army/stat-counter dependency. Initialize extraAttackers/extraDefenders as the same empty lists provided by that constructor for Pure Tactical; use actual initial army/garrison counts. The uncalled DefectProcessor source is retained without deletion. Empty-side messages now say no attacking/defending units remain rather than claiming everyone defected. Keep race compatibility metadata, with a null-safe optional village dereference for the defender race.

Exact source reconstruction covers all removals/initialization changes, two functional labels, the null guard and dated notice. No ConstructibleBuilding, strategic tower/camp/BuildConfig, EmpTraits or DefectProcessor reference remains in TacticalMode. Remaining Reinforcement enum references are condition predicates, not a live generator. No serialized field removed: the retired building collections were internal runtime-only fields; tactical map/scenery Buildings and public scene bindings remain.

## Preserved behavior and remaining boundaries

Keep TacticalBuildings scenery/walls, procedural tactical terrain, village/garrison skirmish setup, ordinary creature/trait summons, equipment battle-start callbacks, combat, tactical pause, AI retreat policy, summaries, bulk Wins and Pure Tactical return. No new campaign, deployment, Downed, temporary HP, injury or survivor rules. No species approved or placeholder supplied.

Army/World/Empire/Village types still provide compatible battle identity, colors/names/race context, AI policy and inventory. Empire-aware retreat settings, shared hostility/power/terrain services, old recruitment/equipment/shop/potion/strategic-map callers and save context still require residual-boundary review. Retired building class definitions/World fields are retained; source-file deletion is not authorized. This packet is not completion of all spec 05 or species prototype retirement.

## Verification

Unity 2018.3.12f1 compile passed exit 0 without C# errors/import exceptions: Logs/packet-05a4-compile.log. Explicit base.unity opening PASSED exit 0 without parse errors/exceptions: Logs/packet-05a4-scene.log. No new compiler warnings; the two existing unassigned strategic-building-field warnings are removed with those fields. Exact source correspondence, scene-byte preservation, dated notice/license hash and scoped diff checks passed; no Unity process remained. Ignored Logs/packet-05a4-* preserve before source/scene, exact edits and diff. No new tests/framework, agent Play-mode battle, player build or personal-data access. Actual village/ordinary/bulk combat tests remain blocked by the empty curated roster.

## GPL and scope

Read License.txt sections 4/5; content hash matches HEAD. Preserve earlier source notices/attribution and add actual dated notice; CHANGES.md lists the exact source path. No external additions, files deleted/moved or scene/prefab/schema edits. This check does not replace a distribution review.

## One combined user check / next work

Open main menu and enter/leave Pure Tactical setup without new red errors. No settings/editor retest or sample species needed. Then proceed to the residual Army/World/Empire/Village and map-provider inventory to choose the next grouped retirement/isolation stage; do not reopen the accepted ability stage. Future battle regression must include ordinary and village maps, normal/turbo AI and summons after curation.

User confirmed the requested screen checks passed without new red errors (2026-11-01). The grouped campaign-combat provider stage is accepted. Actual combat regressions remain pending curation.
