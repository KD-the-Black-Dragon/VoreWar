# 04 — Reset legacy prose and retire 4X events

**Status:** Functional retirement milestone complete, 2026-11-01, with final agent checks passed as recorded in the [closure report](../implementation/phase-2-cleanup-closure.md). Earlier packet records remain historical. Combat/log scenarios await approved species; distribution/shared-asset review remains a release gate.

**Phase:** Phase 2 — retire unwanted systems

**Prerequisites:** 01 narrative/event inventory; coordinate with 03 species-specific references.

## Purpose and scope

### Preparation from study 01B

Use [the content study](../architecture/01b-species-events-and-narrative.md), [narrative registry inventory](../architecture/01b-narrative-inventory.md), and [legacy event inventory](../architecture/01b-event-inventory.md).

1. Retire both empire-turn player-event and strategic-round AI-event triggers. Cover 36 player cases, 32 AI event cases, custom dispatch, choice effects, and stale listeners. Do not count the separate frequency-settings switch as event content.
2. Disconnect startup/reset seeding and reading of the legacy events.txt schema; changing only StreamingAssets does not stop an existing UserData copy. Do not delete personal files to stop loading them.
3. Replace EventScreen.OnDisable's StrategyMode regeneration before reusing the panel. RandomEvent has no discovered callers and is not the active controller.
4. Provide neutral coverage for typed event formatting, the 22 StoredLogTexts lists (2,492 lexical entries), inline Generate/SimpleText branches, descriptor/synonym helpers, and free-form combat callbacks. Counts estimate source coverage, not tested runtime entries.
5. Replace Humans dummy contexts without inventing another rejected-species fallback. Preserve mechanical side effects in mixed callbacks while changing their prose.
6. Explain and choose the handling of narrative RNG before implementation: text selection shares State.Rand with gameplay and also uses UnityEngine.Random. Prose replacement may change later same-seed outcomes without changing formulas; do not promise identical random sequences or introduce a separate RNG silently.

### Agreed scope

[Study 01D](../architecture/01d-campaign-persistence-and-identity.md) confirms that State.Rand is static and the inspected World/TacticalData save path does not preserve its state. Terrain also uses Unity randomness and clock-seeded noise. Keep the shared narrative-RNG decision explicit; unchanged formulas do not imply identical future outcomes after prose changes or reload. The [consolidated sequence](../architecture/01d-removal-sequence-and-risks.md) places trigger/loader retirement before framework reuse, and placeholder/dummy-context work before rejected-species reference retirement.

- Trace narrative event keys, selection predicates, names/pronouns, markup, helper dictionaries, logs, imported text, and display routes before replacing prose.
- Replace the existing vore/digestion narrative with minimal neutral functional placeholders. Preserve event identity and mechanical side effects; the user will author replacement prose later.
- Remove text associated with confirmed retired types where practical; placeholders may remain indefinitely for retained types.
- Retire all legacy 4X event definitions, narrative, response choices, effects, and triggers from the converted game, including custom-content routes that would restore them.
- Document any reusable event framework separately from retired event content. Do not add replacement road events or ambushes during cleanup.
- Distinguish option removal from type removal; request a design decision only where the existing record does not establish whether the underlying mechanic is retired.

## Completion and review criteria

- Every narrative route in the inventory has a placeholder, a documented retirement, or an explicitly identified outstanding dependency.
- Remaining gameplay messages preserve usable actor/target information and do not contain legacy flavor prose.
- No legacy 4X event fires or is offered; removing event content does not change unrelated combat behavior.
- Review includes missing keys, empty selections, formatting errors, and unexpected fallback prose.

## Decisions, constraints, and risks

- This spec covers functional text reset, not authoring new narrative.
- Preserve attribution and license notices even in files whose narrative contents change.
- The planned event replacement belongs to 13.

## Deliverable

Narrative inventory with replacement coverage, neutral placeholders, and a disabled legacy event catalog.

## Execution record

- 04A: removed State startup/reset event construction and seeding, both StrategyMode trigger families/Empire dispatcher, executable EventList/CustomEventList contents and shipped catalog payload; EventScreen is inert and no longer regenerates strategy on disable. See [the exact source/validation record](../implementation/04a-legacy-event-retirement.md).
- Source closure spans all Assets C# sources. Scene/layout, saved event-history fields, personal event files and tactical prose are preserved. Generic future event presentation needs a new company-owned controller in 13.
- The user accepted 04A. 04B is split into 04B1 (central registry, typed generators, missing-target context) and 04B2 (mixed free-form callbacks and remaining descriptors/synonyms). 04B1 covers 22 registry categories and 46 typed event values; preserves event schemas/registrations and UI filters; documents deterministic placeholders and future multi-entry State.Rand selection without a new RNG system.
- [04B1 source/validation record](../implementation/04b1-central-combat-text-reset.md), [04B2 caller inventory/disposition](../implementation/04b2-miscellaneous-call-sites.md) and [04B2 source/verification record](../implementation/04b2-callback-and-descriptor-reset.md) cover the inventoried registry, typed formatting, callback, descriptor and event routes. Reviewed new miscellaneous payloads are functional; old stored legacy payloads are outside this reset. Help/species/mechanical descriptions remain separately scoped. Source replacement is complete for these routes, while real battle-log verification remains pending with the empty roster. No tests/framework or new player build added.

Before execution, refine this spec with the architecture study, list its small change batches, and resolve decisions needed for the next batch. Follow the review and file-operation rules in [the roadmap](../conversion-roadmap.md). Design intent comes from [the campaign plan](../mercenary-company-campaign-plan.md) and the three curation checklists.



## Consolidated cleanup milestone closure (2026-11-01)

See [phase-2 cleanup closure](../implementation/phase-2-cleanup-closure.md) for current accepted work, exact retained services and deferred combat/release gates. Earlier packet status lines are historical. No further per-packet menu tests are required; final milestone verification status is in that report. New campaign/character mechanics remain subsequent specs.
