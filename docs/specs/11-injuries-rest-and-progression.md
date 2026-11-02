# 11 — Persistent injuries, recovery, and downtime

**Status:** Initial grouped implementation complete; reported feedback corrections accepted. Broader integrated review and tuning remain spec12. See the [implementation report](../implementation/11-injuries-rest-and-progression.md) for tuning and verification.

**Phase:** Phase 4 — complete the intended character experience

**Prerequisites:** 08 armor; 10 battle outcomes; daily clock and wage loop from 06/07.

## Purpose and scope

Study [01C](../architecture/01c-combat-equipment-and-results.md) found that battle finalization clears StatusEffects and resets temporary boosts, while some legacy recovery paths grant full heals. Store persistent injuries separately. Small injury chances may apply across attacks, digestion, poison, spells, and environment damage. Distinguish mitigation, lasting/temporary HP damage, healing, costs, and initialization; do not roll twice for one hit through Damage and SubtractHealth. Account for stat/MaxHealth getter side effects and accessory changes. Exact injury probabilities and how temporary HP protects against injury need refinement with the HP model. See [the clarification record](../design-clarifications-2026-10-30.md).

- Add minor, serious, and old-injury state separate from transient combat effects. Check injury chances on damaging hits and deep attrition; do not guarantee serious injury at 1 or 0 HP.
- Replace spell/trait full recovery with temporary combat healing where retained; use separate lasting/temporary pools and spend temporary HP first. Temporary HP is visibly distinct, does not decay over time, and disappears after battle. Vore healing raises lasting HP; ordinary lasting recovery is daily/rest-based. Remove automatic battle-end full heals. Do not reset to a lowest-total-HP record that erases lasting healing. Caps and injury/deep-attrition interactions need refinement.
- Critically injured surviving members receive a modest post-battle HP baseline after temporary HP is removed; the exact percentage is deferred (10%/20% are examples). This is not full healing or resurrection. Leader revival remains its separate full-health exception.
- Use mostly combat-stat penalties with random body parts initially. Armor reduces injury risk; covered-part weighting can follow later.
- Minor injuries clear on recovery; serious injuries have stronger temporary penalties and become stackable permanent old injuries with weaker penalties. Leaders can have temporary injuries but never old injuries.
- Add small daily HP recovery and sparring for all healthy members, active or reserve; preserve the existing level-up system.
- Rest anywhere for chosen days, with better town recovery; process every skipped day's wages, deadlines, pool refreshes, healing, and sparring through the single daily update path.
- Pause multi-day rest for payday warnings and other decisions requiring a response, returning control in time to pay during the grace day.
- Provide paid treatment per injured member to reduce temporary recovery days without curing permanent old injuries.
- Retain within-battle injuries/downed behavior in Pure Tactical but discard all state after its single battle.

### Preparation from study 01D

[01D](../architecture/01d-campaign-persistence-and-identity.md) separates current army percentage healing from Army.Refresh's unrelated movement/mana/equipment/village effects. Integrate company and target recovery, injury transitions and sparring into the single resumable daily path; never process a day on battle return, menu refresh or load. Persist lasting/temporary HP, injuries and interrupted rest/warning progress. Confirm which actual damage feeds injury/attrition rolls after temporary HP absorbs damage. No existing TempBoosts/Barrier field should be treated as the requested HP pool without a reviewed model.

## Completion and review criteria

- Damage, armor, injury stacking, recovery transitions, temporary/permanent penalties, and leader exemption have clear inspection scenarios.
- Resting multiple days matches daily updates for the same elapsed days and cannot skip the opportunity to respond to a departure warning.
- Treatment saves correctly and never cures an old injury; injured/reserve members still draw wages.
- Combat and save/load use the same canonical member state without double-applying stat penalties.

## Decisions, constraints, and risks

- The user authorized provisional tuning. Initial injury chances, thresholds, penalties, healthy-member criteria, recovery and treatment values are documented in the implementation report; refine them during12. The accepted15% post-battle baseline and separate HP pools remain in use. Rest interruption for warnings/required choices is agreed.
- Descriptions stay plain and non-graphic; avoid amputation framing.
- Damage-type and armor-location refinements can follow after the initial random-part model.

## Deliverable

Injuries, treatment/rest UI, daily recovery/sparring, and complete persistent character consequences.

## Execution record

- One grouped milestone implements injury data/penalties, armor risk, daily recovery, explicit chosen-day rest, treatment and healthy-member sparring. User authorized provisional tuning; no species additions or new assets.
- Source trace, tuning, actual checks and the single connected manual workflow are in the [implementation report](../implementation/11-injuries-rest-and-progression.md). Player review remains pending.

Use milestone-sized review batches, as requested by the user. The initial numerical decisions are provisional; refine them through the integrated balance review. Follow the review and file-operation rules in [the roadmap](../conversion-roadmap.md). Design intent comes from [the campaign plan](../mercenary-company-campaign-plan.md) and the three curation checklists.


###2026-11-01 feedback

Company-owned exponential XP thresholds, challenge scaling and starter contract participation rewards replace the inherited cap sentinel; existing chosen level-up improvements remain. New injuries receive a battle-log entry and queued tactical text. Departures have a separate OK-only notice; simultaneous genuine wage warnings follow separately without consuming the grace day. See the [feedback report](../implementation/11-feedback-progression-and-notices.md). Player confirmation pending.
