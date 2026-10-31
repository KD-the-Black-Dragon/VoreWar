# 12 — Integrated campaign review, help, and first release candidate

**Status:** Proposed specification. Planning only; execution has not started.

**Phase:** Phase 5 — integration and player review

**Prerequisites:** 06–11 complete; all deferred cleanup regression checks resolved.

## Purpose and scope

Study [01C](../architecture/01c-combat-equipment-and-results.md) and [the subsequent decisions](../design-clarifications-2026-10-30.md) add required regression coverage for normal versus skipped/turbo battles, enemies releasing captives before fleeing, all active members downed with reserves alive, draw handling, surrender-reset/round-limit safeguards, and leader digestion versus merely being downed. Review deprecated resurrection/conversion paths, disappearing summons, temporary HP removal and lasting vore recovery, and all nine equipment slots surviving trait refresh/cooldowns. Distinguish contract gold from any approved battle loot, avoid counting alive contained/downed members as dead, preserve skirmish isolation, and prove battle return does not advance campaign time. These are planned checks, not tests already run.

- Review the full new-campaign-to-contract-to-recovery-to-payday loop and campaign endings with the user.
- Exercise new saves across towns, offers/targets, equipment, debt warnings, former members, injuries, memorial, revival transitions, and supported tactical-save boundaries.
- Finish optional Ironman behavior using the agreed save/checkpoint policy; lock its selection at campaign creation and avoid promising protection from external save editing.
- Review content settings and global species disabling across both modes, new campaigns, existing saves, summons, and Race Editor overrides.
- Confirm species disabling preserves all existing individuals while blocking new generation; preserve former-member rehire and already offered candidates. Check rest warnings interrupt in time, and unaffordable paydays never automatically choose wage recipients.
- Review active members retreating while downed allies remain against standing enemies: actual simulation must continue, with no assumed deaths or left-behind memorial entries. Review both sides inactive and a stalled battle reaching the generous turn limit: survivors go home, while fleeing enemies count defeated and can complete/pay a contract once even though the battle is a draw.
- Calibrate starter affordability, wages, contract rewards/power, deadlines, recovery, and recruit access against the small-company target. Keep exact tuning visible in the decision record.
- Rewrite Help Screens late to match the implemented loop. Replace obsolete labels/tooltips, finish navigation/error feedback, and leave user-owned narrative as basic placeholders where desired.
- Perform the agreed narrow compile/build and player-facing regression checks; inventory any intentionally retained legacy internals and verify no retired path has resurfaced.

### Preparation from study 01D

The [persistence/identity report](../architecture/01d-campaign-persistence-and-identity.md) and [risk register](../architecture/01d-removal-sequence-and-risks.md) add planned checks for unsupported legacy saves, failed prospective loads, corruption/backup recovery, half-finished daily transitions, battle/AI checkpoints, nested prey references, repeated result/summary/revival loads, item bonus reapplication, discovery retention, and disabled-species existing identities. Keep exact supported save boundaries and any accepted fallback visible to the player. These checks remain unperformed until implementation and its authorized verification stage.

## Completion and review criteria

- The user can create a leader, choose among starters, take nearby work, deploy/fight, receive pay, recover, manage wages/equipment, save, and resume.
- Loss, retreat, revival, wage departure/rehire, and contract expiration work without entering retired systems.
- The specified run summary and memorial remain accessible at the intended stages.
- Pure Tactical remains an isolated configurable skirmish. The release candidate has explicit performed/pending checks and known limitations.

## Decisions, constraints, and risks

- This is an integration checkpoint, not permission for unrelated polishing or engine upgrades.
- Random road events, ambush content, additional points of interest, species expansion, and user-written narrative can remain later work.
- Publish/upload/release actions still require separate authorization.

## Deliverable

Reviewed campaign proof of concept, updated help, validation record, and an explicit next-work list.

## Execution record

- Source trace and exact change locations: pending.
- Implementation batches and user review: pending.
- Checks actually performed and remaining checks: none for this spec yet.

Before execution, refine this spec with the architecture study, list its small change batches, and resolve decisions needed for the next batch. Follow the review and file-operation rules in [the roadmap](../conversion-roadmap.md). Design intent comes from [the campaign plan](../mercenary-company-campaign-plan.md) and the three curation checklists.

