# 07 — Curated starters, company members, recruitment, and wages

**Status:** Implemented with provisional tuning; combined runtime review user-accepted, 2026-11-01. See [milestone implementation and limits](../implementation/07-08-people-and-economy.md). Battle/injury integration remains09–11.

**Phase:** Phase 3 — build the first company campaign loop

**Prerequisites:** 06; user selects first species for curation. Battle-dependent checks from 03/05 resume here.

## Purpose and scope

Study 01B found that NPC_unit uses old numeric species ranges to choose monster generation, and RaceSettings can remap spawn/conversion/leader/morph destinations. Curating a species therefore includes reviewing those assumptions and ability destinations, not only enabling its dropdown. Generic creature summoning is animal-only, but temporary sapient clones/doubles/illusions are allowed. No spontaneous mid-combat allegiance changes; future rescue/hiring events require salary information and player consent. See [packet 01B](../architecture/01b-species-events-and-narrative.md) and [the clarifications](../design-clarifications-2026-10-30.md).

Study [01C](../architecture/01c-combat-equipment-and-results.md) found that Unit.GetStat includes GetLeaderBonus and battle setup also heals by that bonus. Removing the aura requires addressing both paths and associated leadership growth/power assumptions. Keep company-member/leader identity independent of temporary side, species, name, and copy/morph state so debt and personal history do not follow an impersonation or replacement unit accidentally.

- Curate user-selected existing species one at a time into sapient or animal roles; aim for a small eventual melee/ranged/magic core without guessing the user's choices.
- Enable leader creation first: species, appearance, name, and starting focus; lock name/appearance afterward and preserve species talents. Deprecate Leadership, its aura and automatic growth. Make the leader personally tougher through starting stats and/or extra growth within the shared leveling framework; exact bonuses remain tuning work.
- Create company management available outside battle, ordinary-member nicknames, active/reserve status, existing level-up behavior, quoted level-dependent wages, and flavor backstories.
- Create separate small town recruitment pools with refresh, varied inexpensive starters, and enough starting cash for roughly two hires. Preserve former members outside ordinary pool limits and roll reappearance on town entry.
- Implement weekly advance wages, first payday seven days after start, prorated midweek hiring, per-member debt, full individual repayment, warnings, one-day veteran grace, departures, and rehire debt payment.
- On an unaffordable payday, assign unpaid wages to each member's debt without automatically selecting recipients. Show current gold, total debt, shortfall, and imminent departures. Offer an affordable-only button paying all threatening members' full debts, View Company for individual full payment, and OK to return without payment. Multi-day rest pauses for this decision and preserves the grace day.
- Keep former members' complete identity, level, injuries, equipment, and debt; level-growth wage changes use an explicitly chosen effective-date policy.
- Expose reputation eligibility hooks for 09; injured/reserve members still require wages, while the leader is free.

### Preparation from study 01D

[01D](../architecture/01d-campaign-persistence-and-identity.md) found no persistent company-member ID in the inspected Unit model. Give generated candidates stable identities before hiring; town pools, roster and former-member reappearances must resolve to one canonical Unit/record after load. Keep reserves outside mutable deployment collections. Do not use Army.Prune/Unit.IsDead as a permanent-loss rule, Empire.Regenerate as leader maintenance, or ProcessIncome as wages: those paths can remove/replace characters or automatically choose departures. Persist debt/warning due dates and interrupted day/rest progress.

## Completion and review criteria

- An approved leader and a couple of affordable varied recruits can form a company; uncurated/disabled entries remain unavailable.
- Midweek hire, regular payday, two missed paydays, individual full repayment, warning/departure, dismissal, and rehire are covered by review scenarios.
- Former members retain state after leaving and after save/load; quit/dismiss is not recorded as a death.
- No automatic battle-size or roster cap is introduced; the leader always belongs to the eventual deployment.

## Decisions, constraints, and risks

- Set recruitment refresh pace, wage formula, lower-paid member tolerance, and prorating/rounding before the relevant batch. Insufficient-treasury recipient choice is settled: the player decides, including through the explicit group-payment button; never automatically prioritize recipients.
- Resolve day-zero advance pay semantics so the initial hiring cost and day-seven payday do not charge twice for the same period.
- Choose the first species only when ready to curate; this does not block Phase 0–2.

## Deliverable

A created leader, curated starting roster, hiring/company screens, and persistent wage/debt loop.

## Execution record

- Source trace and exact changes: [07–08 implementation](../implementation/07-08-people-and-economy.md).
- One grouped people/economy implementation; user selected Avians/ranged, Lizards/magic, Gnolls/melee and animal SpitterSlugs, and authorized provisional tuning.
- Unity compilation and scene opening passed; read-only wiring/source/resource checks performed. Actual creation, hiring, debt, equipment and nonempty save round-trips await the combined runtime review. No Play-mode or battle pass claimed.

For subsequent work, refine this spec from actual runtime findings and resolve decisions needed for the next milestone. Follow the review and file-operation rules in [the roadmap](../conversion-roadmap.md). Design intent comes from [the campaign plan](../mercenary-company-campaign-plan.md) and the three curation checklists.



## Grouped milestone preparation (2026-11-01)

User authorized grouped07–08 following accepted06 preview. [Source/dependency preparation](../implementation/07-08-people-economy-preparation.md) records item/Unit/customizer/clock integration and required species/tuning decisions. Those decisions have now been supplied explicitly and implemented; the preparation report is historical, and the implementation report above is current. One connected review milestone, not per-species/control tests.
