# 40D — Post-battle evidence and earned trait progression

Planning draft, 2026-11-06. Part of [module 40](40-authoring-and-campaign-progression.md). This reopens the previously deferred mechanical event system for planning. Narrative prose remains the curator's later work.

## Confirmed direction

The user wants a post-battle event when eligible non-leader participants remain. Reserve units cannot qualify. Leader-only remaining participation is exempt. Events recognize actual battle experiences and can grant positive or negative related traits. Extraordinary experiences should be favored over mundane ones, with a mundane fallback so eligible battles still produce an event.

Examples already supplied by the user are unusually high ranged activity and surviving being swallowed at least three times. These guide the occurrence catalog. The user confirms one automatic scene based on the most significant qualifying experience, assigning a related trait in light of the unit's existing traits. Exact thresholds, outcome lists and significance weights await design approval.

## Positive and negative balance

The user specifies the following polarity rule before an eligible acquired outcome is granted.

| Current count difference | Outcome polarity |
| --- | --- |
| Equal | 50% positive, 50% negative |
| One | 75% toward the minority side, 25% toward the majority |
| Two | 100% toward the minority side |
| Greater than two | Proposed defensive extension is 100% toward the minority side |

Do not substitute uniform random quirks or continue adding to a majority at a difference of two. The desired polarity and the significant occurrence must both have a valid outcome. Design candidate coverage and fallback handling before implementing selection. Do not silently reverse the desired polarity when a family is capped or an outcome conflicts.

The user requires internal Good, Bad and Neutral classifications for all traits. Both innate species traits and acquired personal traits count toward the Good/Bad ratio. Neutral traits have mixed benefits and drawbacks and contribute to neither side. Featherweight is explicitly Neutral because it grants evasion while increasing damage received. Classify the remaining retained traits in a reviewable inventory before enabling grants. Proposed family counting uses one currently held family regardless of tier, with upgrades replacing that family's previous tier. This detail remains part of the selection-rule review.

Old injuries are separate from traits and from the Good/Bad ratio. Their burden must be visible in outcome review and simulations. Do not secretly change the specified polarity probabilities to compensate. Whether injuries affect candidate eligibility or severity requires a separate approved rule.

## Event-subject preference and growth

The user confirms there is no hard trait-count limit. Prefer eligible characters with fewer traits when selecting the event subject. Keep significant battle experiences influential, with extraordinary occurrences favored over mundane ones. This preference must be a weighting factor rather than a hard eligibility cutoff, so an experienced character can still be recognized for an exceptional battle.

Choose and review the exact weighting together with occurrence significance. Proposed total-count weighting includes currently held Good, Bad and Neutral traits from species and personal sources, while old injuries remain separate. Show the effects using comparable candidates and long-lived veteran simulations before selecting provisional weights. No count-based trait replacement system is required by this design. Existing mutually exclusive tier upgrades still replace the previous tier within their family.

## Evidence foundation

`Assets/Scripts/Modes/Tactical/TacticalStats.cs` mainly records side totals. Trait events require a per-participant ledger linked to stable company character and battle IDs. Extend battle snapshots and settlement alongside `CompanyBattle`, `CompanyBattleSnapshot` and company saves.

Record actual resolved outcomes, including relevant attack counts, hits, grazes, criticals, meaningful damage, swallowing, escapes, digestion, downing and survival. Final ledger fields should follow the approved occurrence catalog. Previews, AI scoring and cancelled actions must never count as accomplishments. Distinguish ordinary attacks, opportunity attacks and summons where attribution matters.

## Selection and reward transaction

1. Snapshot deployed participants and their identities at battle start. Reserves and temporary summons are ineligible for lasting grants.
2. Settle the battle's actual fates and identify eligible surviving participants. Do not grant traits to digested characters or create rewards for a company that has ended.
3. Build candidates from approved occurrence rules. Classify extraordinary and mundane candidates, favor significant experiences and apply the preference for subjects with fewer traits. Determine outcome polarity from all held Good/Bad traits, then select an appropriate related outcome with approved fallback coverage.
4. Persist the chosen event and automatic outcome before presenting them. Persist the grant and its applied state so reload cannot reroll or duplicate a reward.
5. Apply through the existing exclusive I–III trait-family rules. Define capped-tier and conflicting-trait alternatives so selection cannot produce an unusable reward. Keep innate species traits protected according to the chosen policy.
6. Use concise factual placeholders showing the participant, qualifying experience and mechanical outcome. The curator can replace prose later without changing the evidence pipeline.

Manual combat, AI Finish Turn and Auto-resolve must use the same evidence and settlement logic. Single Battle may produce diagnostic evidence but cannot progress a campaign or permanent character history.

## Decisions before implementation

- Complete the Good/Bad/Neutral classification inventory and confirm family-count treatment for tier upgrades. Featherweight's Neutral classification is already confirmed.
- Exact weighting for fewer-trait subjects alongside occurrence significance. Trait-count capacity is uncapped as confirmed by the user.
- First occurrence catalog, eligibility thresholds and extraordinary/mundane significance weights. Decide how repetition and trivial farming are handled and ensure valid polarity outcomes exist.
- Whether the leader can receive a selected outcome when other eligible mercenaries remain, and how trait exclusions interact with leader permanent-injury immunity.
- Event placement relative to results, rewards and the leader-revival cutscene. Preserve the established reward-before-revival rule.
- Behavior for older saves resumed mid-battle with no complete ledger. Do not fabricate earlier accomplishments. A documented fallback is needed.

## Planned grouped acceptance

Use controlled battles to cover extraordinary selection, mundane fallback, reserves, summons, leader-only and company-ended cases, all relevant fates, maximum tiers and opposed traits. Check equal/one/two count boundaries, the 50%/75% distribution, guaranteed minority correction and outcome exhaustion. Verify species traits count, Neutral traits affect neither polarity count, and old injuries remain separate. Compare otherwise similar candidates to confirm the fewer-trait preference, while retaining a veteran's chance for exceptional experiences. Run long-lived histories with uncapped trait counts and old injuries to expose excessive penalty accumulation. Check manual/automatic combat parity, interruption before/after selection and grant, mid-battle saves, no rerolls and no duplicate settlement. One player handoff covers factual event presentation and automatic trait outcomes. Existing injury probabilities and accepted combat rules remain independent.
