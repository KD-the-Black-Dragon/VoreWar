# 06 — Company campaign state, world, clock, and saving

**Status:** Proposed specification. Planning only; execution has not started.

**Phase:** Phase 3 — build the first company campaign loop

**Prerequisites:** Phase 2 review accepted; tactical boundary defined.

## Purpose and scope

### Preparation from study 01D

[The campaign/persistence study](../architecture/01d-campaign-persistence-and-identity.md) proposes an explicit new root/format/schema/mode, stable character/target/battle identities, persisted daily/decision progress, actual region/discovery state, and a separate load route. Current State.Load mutates live state before full validation, runs legacy migrations and installs saved configuration; MainEmpires-null means Pure Tactical. Validate a prospective company save before installing it and rebuild services/views without ticking a day or running those migrations.

Current fog is recalculated faction visibility, not exploration memory. World generation mixes terrain with empire assignment and multiple random sources; pathfinding caches old occupancy/passability. Reuse selected algorithms through explicit data providers, persist the generated region/discoveries, and validate reachability. A seed alone is insufficient under the current implementation. Army.Refresh is not a pure healing function.

Keep daily progression resumable through debt warnings/rest interruptions and establish safe checkpoint boundaries with explicit success/failure and multiple valid generations. Store elapsed-day semantics so first payday is seven days after start. Define random-state/content-version handling before promising reproducible continuation. These are proposed implementation boundaries, not completed features or approved numeric tuning.

### Agreed scope

- Introduce the canonical campaign/company state, stable member identity, treasury, day/action clock, map seed/state, towns, and extension points for contracts, debt, injuries, and memorial records.
- Generate a finite grid region with static towns and one moving company; provide the world-map screen, town-hub shell, exploration fog, and discovery state.
- Implement one day per player-ended turn, consistent daily action budget, terrain-dependent movement costs, and configurable interaction costs defaulting to zero. Zero actions prevents further action spending without automatically ending the day.
- Define one ordered daily update path for wages, deadlines, refreshes, recovery, and sparring. Later specs plug into this path without introducing their own clocks.
- Start a new campaign in a generated town; provide new-format save/load without importing empire saves. Persist identity and state without duplicate members after loading.
- Support normal in-battle saves if practical; between-battle saves are an acceptable initial fallback if technical problems prevent it. Aim for Ironman turn-by-turn saving with several corruption-recovery backups. Packet 01D must assess feasible campaign/tactical checkpoint boundaries, ongoing containment, downed state, lasting/temporary HP, and once-only result reconciliation; report any fallback. Store the Ironman selection at campaign creation and prevent later changes. See [the clarification record](../design-clarifications-2026-10-30.md).
- Rest must pause for missed-payday warnings and other decisions requiring a response; preserve the grace day rather than processing departures before control returns.
- Provide map/company/town navigation without exposing unfinished services as completed features.

## Completion and review criteria

- A generated region has usable towns and traversable starting placement; moving/revealing/exiting/reloading retains the same region and discoveries.
- Ending a day advances time once, refills actions once, and invokes each registered daily system once.
- Rest can use the same daily path later; tactical battles will not advance this clock.
- New campaign saves round-trip the implemented state; old campaign saves are identified without entering the retired 4X loop.

## Decisions, constraints, and risks

- Exact daily action count, terrain costs, map size, and reachability constraints are tuning decisions; do not treat the example 10 actions as approved final balance.
- Species-dependent campaign starts remain unavailable until 07.
- Final save coverage and Ironman behavior are exercised again in 12.

## Deliverable

Company campaign foundation, generated map/town navigation, daily update contract, and new save format.

## Execution record

- Source trace and exact change locations: pending.
- Implementation batches and user review: pending.
- Checks actually performed and remaining checks: none for this spec yet.

Before execution, refine this spec with the architecture study, list its small change batches, and resolve decisions needed for the next batch. Follow the review and file-operation rules in [the roadmap](../conversion-roadmap.md). Design intent comes from [the campaign plan](../mercenary-company-campaign-plan.md) and the three curation checklists.

