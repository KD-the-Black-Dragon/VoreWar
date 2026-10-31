# 09 — Contracts, reputation, and procedural enemy power

**Status:** Proposed specification. Planning only; execution has not started.

**Phase:** Phase 3 — build the first company campaign loop

**Prerequisites:** 06–08; use the combat boundary from 05.

## Purpose and scope

Study [01C](../architecture/01c-combat-equipment-and-results.md) identified ArmyPower and UnitValue as existing approximations, not the new contract rating model. ArmyPower multiplies its sum by member count and includes leadership, level, weapon, scale, and race adjustment terms. Reusing it directly would import unintended headcount/leadership assumptions, especially for swarm forces. AI also uses it for retreat evaluation: review that coupling rather than silently replacing AI policy when introducing a contract formula. Account for gear bonuses already baked into stored stats so the rating model does not score them twice.

- Add separate replenishing town offer pools, several equal-priority active contracts, a persistent map-side list, and exact target markers even under fog.
- Implement only rout-enemy contracts initially with fixed generous deadlines, expiration/abandonment penalties, target removal at expiration, and several easy starting offers.
- Define a transparent numerical power model covering unit level, stats, traits/abilities, and equipment; generate enemy groups to a tight target band with varied composition/headcount, including approved swarm profiles.
- Use fixed difficulty independent of company strength. Bound generation attempts and provide a safe failure result when disabled species or available gear cannot satisfy a rating.
- Quote numeric difficulty and exact rating-based gold with modest variation rounded to tens. Reward gold only, automatically on success; no return trip.
- Implement visible company reputation: completion always increases it, only contract outcomes change it, difficulty scales failure loss, and reputation continuously widens eligible contract/recruit power.
- Record generated enemies/rewards with their contract state so loading or reopening an offer does not accidentally rebuild an accepted target.
- Failed attempts preserve casualties and surviving enemies' wounds. The stationary map army recovers HP at a typical daily rate; reuse applicable healing rules without restoring empire simulation, replacing casualties, or rebuilding the force to its original power budget. See [the clarification record](../design-clarifications-2026-10-30.md).
- Fleeing target enemies leave the game and count as defeated even on a stalemate/draw. When all target members have been defeated, grant the normal automatic contract reward/reputation once; do not preserve a retryable target merely because the battle result says draw. Retry persistence applies only where an actual surviving target remains.

### Preparation from study 01D

[The final study](../architecture/01d-campaign-persistence-and-identity.md) makes targets first-class saved records: target/contract/enemy IDs, actual surviving roster/wounds, location, deadline, and already quoted/generated reward. Loading or reopening a town must not reroll these. Daily target recovery uses selected health rules, not all of Army.Refresh/Empire.Regenerate. Separate contract completion from the battle's victory/draw label, and apply outcomes through the same once-only battle/result identity as 10.

## Completion and review criteria

- Targets and rewards match their quoted offers; the generator explains its computed power and remains within the chosen tolerance.
- Weak contracts remain weak for a strong company; reward does not scale with company size.
- Expiration/abandonment removes the correct target and applies one penalty; failed attempts can be retried before the deadline.
- Empty eligibility pools and unreachable targets are handled without an endless generation loop or unapproved fallback.

## Decisions, constraints, and risks

- Power weights, rating scale, tolerance, payout curve, offer refresh, deadline length, and reputation gain formula require tuning decisions.
- Computed power needs playtesting: arithmetic alone does not establish difficulty, especially with vore/digestion interactions.
- Define exact enemy daily recovery rate and ensure save/load retains the agreed casualty/wound state after failed attempts.

## Deliverable

Contracts/reputation screens, saved targets, and a bounded procedural force generator with inspectable power calculations.

## Execution record

- Source trace and exact change locations: pending.
- Implementation batches and user review: pending.
- Checks actually performed and remaining checks: none for this spec yet.

Before execution, refine this spec with the architecture study, list its small change batches, and resolve decisions needed for the next batch. Follow the review and file-operation rules in [the roadmap](../conversion-roadmap.md). Design intent comes from [the campaign plan](../mercenary-company-campaign-plan.md) and the three curation checklists.

