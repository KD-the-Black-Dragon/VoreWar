# 01 — Architecture, dependencies, and removal order

**Status:** Packets 01A–01D and the consolidated removal sequence are prepared for review, 2026-10-30. The user authorized resuming 01D after the combat-design follow-ups. Implementation preflights and baseline gameplay limits remain explicit; no gameplay implementation has started.

**Phase:** Phase 1 — comprehensive study

**Prerequisites:** 00 baseline, or a documented blocker that still permits source inspection.

## Purpose and scope

- Trace startup and screen navigation from the base scene and GameManager through both current modes. Inventory scene/prefab references, button callbacks, static state, and every entry point to retired screens.
- Trace World, Empire, Army, Village, Unit, traits, items, appearance, species generation, tactical AI, battle setup, terrain selection, retreat, surrender, vore/digestion outcomes, battle results, and return to the strategic screen.
- Trace settings defaults, saved overrides, Race Editor overrides, and global species availability. Identify every generation route, including summons, fallback species, Pure Tactical setup, and bulk matchup generation.
- Trace serialization and identity: normal/tactical saves, Odin data, enum IDs, scene GUIDs, external content, resource loading, addressables, asset bundles, and race-data slots where present.
- Inventory narrative selection and display, all legacy event definitions/choices/effects/triggers, and any custom-data loading paths that could restore retired content.
- Produce a keep/adapt/retire table with source locations, callers, serialized references, shared dependencies, intended owner, removal order, and required checks for each item in the three existing curation checklists.
- Propose the company model and a narrow interface to tactical battles. Specify how canonical mercenaries survive battle transfer, how results return once, and which old structures need temporary compatibility code.
- Return findings in small review packets: navigation/settings; species/content; combat/results; campaign/save boundaries. Update later specs with evidence before implementation.

## Completion and review criteria

- The study follows important behavior from player action through state change to visible result; a list of class names alone is insufficient.
- Each proposed retirement has its dependent code/assets and replacement or disconnection point identified.
- An evidence-based dependency diagram, removal manifest, risk register, and recommended implementation batches are ready for user review.
- Unresolved gameplay decisions are separated from technical questions the study can answer. No gameplay, scene, or asset implementation occurs in this spec.

## Decisions, constraints, and risks

- Starting points verified in this checkout: Assets/base.unity; Core/GameManager.cs, State.cs, World.cs; Entities/Army.cs, Empire.cs, Village.cs, Units/Unit.cs; Modes/TacticalMode.cs and Tactical/; AI/; Races/Races.cs; Enums/Race.cs (all script paths under Assets/Scripts/).
- Content starting points: Subwindows/ContentSettings.cs and RaceEditorPanel.cs; Utility/EventList.cs, CustomEventList.cs, RandomEvent.cs; UI/EventScreen.cs; Modes/Tactical/Log/StoredLogTexts.cs and related text helpers.
- These are entry points for the study, not a claim that the full dependency trace is already complete.

## Deliverable

Architecture report, source-linked inventories, migration boundaries, and revisions to the implementation specs.

## Execution record

- 01A: [startup/navigation/settings report](../architecture/01a-navigation-and-settings.md), [scene inventory](../architecture/01a-scene-inventory.md), and [Content Settings bindings](../architecture/01a-content-bindings.md) prepared. The user authorized the next packet; this does not approve implementation or new gameplay decisions.
- Inspected startup, menu/mode entry/exit, global and saved configuration, editor persistence, reflection/unit-editor entry points, positional settings pages, and selected prefab overrides. Fourteen control bindings and inherited callback details remain explicitly unresolved in the appendix.
- Specs 02, 03, and 05 received source-based preparation notes; their implementation remains pending completion/review of the necessary study packets.
- 01B: [species/events/narrative report](../architecture/01b-species-events-and-narrative.md), [90-name source/content map](../architecture/01b-species-removal-map.md), [construction/race-write candidates](../architecture/01b-species-creation-sites.md), [narrative groups](../architecture/01b-narrative-inventory.md), and [event cases/import routes](../architecture/01b-event-inventory.md) prepared for review. Specs 03/04/07/13 received preparation notes.
- 01C: [combat/equipment/results report](../architecture/01c-combat-equipment-and-results.md), [equipment reference candidates](../architecture/01c-equipment-reference-inventory.md), and [damage/outcome reference candidates](../architecture/01c-damage-outcome-inventory.md) prepared for review. Specs 05/07/08/09/10/11/12 received preparation notes. Important findings include shared Unit references, fixed-slot refresh behavior, nontrivial surrender/anti-stall rules, transient status cleanup, and monolithic strategic battle finalization.
- 01D: [campaign/persistence/identity report](../architecture/01d-campaign-persistence-and-identity.md) and [consolidated removal manifest, sequence and risk register](../architecture/01d-removal-sequence-and-risks.md) prepared. Traced World/State construction/load, strategic days and recovery, terrain/path/fog ownership, tactical snapshot/checkpoint feasibility, random-state limitations, and canonical identity/result reconciliation. The proposed adapters and new data model remain unimplemented.
- Recorded [the user's 24 design answers and follow-ups](../design-clarifications-2026-10-30.md), with revisions to the campaign plan, roadmap, checklists, and affected specs. Packet 01D must incorporate lasting/temporary HP, disappearing summons, digestion-only leader loss, preserved existing disabled-species individuals, persistent damaged enemy targets, rest interruptions, and feasible battle/Ironman checkpoints. No-abandonment/stalemate/contract interactions are resolved: preserve actual battle outcomes, return stalemate survivors as fled, and count fled enemies defeated even on a draw.
- Validation performed: source/serialization inspection and document checks only. No new Unity build, gameplay test, or implementation was performed for this packet. Earlier baseline gameplay limitations remain recorded in spec 00.
- Review gate: accept or refine the consolidated boundary/order before cleanup. The recommended first implementation packet is 02A (defaults/configuration ownership without widget removal). Fourteen earlier scene bindings/inherited callbacks and shared-resource ownership remain bounded preflights before their affected edits, not evidence of completed removals.

Review each study packet before proceeding to the next. Reconcile the dependency/removal findings across all packets before starting cleanup. Follow the review and file-operation rules in [the roadmap](../conversion-roadmap.md). Design intent comes from [the campaign plan](../mercenary-company-campaign-plan.md) and the three curation checklists.

