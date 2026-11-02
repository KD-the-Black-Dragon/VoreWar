# 13 — New road events and ambushes

**Status:** User-accepted on 2026-11-02 with a30% reduction in new slug encounter budgets (0.80 to0.56); further testing explicitly waived for that tuning. Earlier compile/scene/regression checks predate the constant-only adjustment. See [implementation](../implementation/13-road-events-and-xp.md) and historical [source preparation](../implementation/13-road-events-preparation.md).

**Phase:** Phase 6 — later campaign expansion

**Prerequisites:** Core loop reviewed in 12; reusable framework identified in 01/04. Timing may be advanced only by an explicit design decision.

## Purpose and scope

[Study 01D](../architecture/01d-campaign-persistence-and-identity.md) proposes campaign-owned pending decisions with explicit serialized progress. Future events should use that boundary and daily/action hooks rather than revive EventScreen's old StrategyMode regeneration, empire-turn dispatch, or custom 4X loader. Preserve zero campaign time in battles and interruption opportunities during multi-day rest. No new event content is introduced by this study.

Study 01B established that EventList/CustomEventList and EventScreen form the active legacy system; RandomEvent is an apparently unused text/action holder. EventScreen.OnDisable invokes StrategyMode.Regenerate, and custom effects assume Empire/Village data. Reuse presentation/choice concepts only after replacing those lifecycle/effect dependencies; do not port old event definitions or assume the unused RandomEvent type already provides a working framework. See [packet 01B](../architecture/01b-species-events-and-narrative.md).

- Design new mercenary-company event categories with the user: road happenings, choice events where appropriate, and sudden ambushes.
- Reuse event infrastructure only where it suits the company model; no old 4X event definitions, text, choices, or effects return.
- Define triggers, frequency, outcomes, eligible company/world state, campaign-time costs, save behavior, and rest interruption before implementing events.
- Route ambushes through the normal final deployment screen, battle results, member fate, and zero-campaign-time tactical rules.
- Keep authored event content separate from generic triggers/effects so new entries can be reviewed without reworking the framework.

## Completion and review criteria

- Every active event is approved new company content and has meaningful valid choices/outcomes.
- Events respect global species eligibility and do not duplicate on loading or repeating a callback.
- Ambushes give the final active/reserve review required for every battle and use normal outcome reconciliation.
- No event reintroduces empire simulation, unwanted species, or removed legacy narrative.

## Decisions, constraints, and risks

- The user approved a forced wild Spitter Slug encounter scaled to full company strength with extra leader weighting, plus the practice-duel event described in the implementation report. Provisional travel triggers/cooldown and no random-combat gold/reputation are approved. Future payment-to-avoid bandits and further content remain later design.
- The user approved two healthy non-leader mercenaries (active/reserve), exact half-level XP each on a shared50% success, otherwise half current HP lost with no injuries; declining has no material effect.
- Per-level XP display requested alongside this work preserves surplus and existing saves; cumulative storage is retained internally.
- Additional points of interest and later species releases can have their own specs after the core experience is reviewed.
- The user owns the replacement vore/digestion narrative; this spec does not implicitly delegate that writing.

## Deliverable

An approved new event catalog/framework and integrated road events/ambushes.

## Execution record

- Source trace and exact changes: [preparation](../implementation/13-road-events-preparation.md) and [implementation](../implementation/13-road-events-and-xp.md). Ten sources, including one new event source and its meta.
- Connected implementation user-accepted with lighter slug encounters; no additional review requested. Help rewrite remains deferred.
- Performed: Unity compilation/read-only scene audit and existing32managed regression checks; no new warnings or asset/scene changes. No new tests/framework/player build or live event/XP UI pass.

Before execution, refine this spec with the architecture study, list its small change batches, and resolve decisions needed for the next batch. Follow the review and file-operation rules in [the roadmap](../conversion-roadmap.md). Design intent comes from [the campaign plan](../mercenary-company-campaign-plan.md) and the three curation checklists.

