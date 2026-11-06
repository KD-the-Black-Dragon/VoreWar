# Injury and old-complication revamp

2026-11-04. Implements the user's approved injury design alongside spec32 baseline traits and the final spec31 pruning batch. Verification results are recorded below when complete; no rendered playtest is implied by runtime checks.

## Health and accumulated damage

Normal maximum HP uses natural base STR, plus modifiers explicitly targeting HP. Legacy equipped accessory STR bonuses are stored in the raw stat array, so they are excluded from the HP base; equipping/removing STR accessories no longer grants/removes HP. Combat stat multipliers and STR penalties never change normal maximum HP. HP remains at least1.

Current total HP, including temporary HP, determines a derived condition: below75% Light Damage (-10% all four combat stats), below50% Moderate Damage (-20%), below25% Severe Damage (-30%). Exact75/50/25% uses the less severe tier. Healing changes the condition immediately; expiring temporary HP can restore it. Normal maximum HP, not the chest healing ceiling, is the denominator. This derived condition is displayed in tactical Status, company roster and recovery summaries rather than stored as a duplicate injury record.

## Body-part injuries and armor

| Part | Effect | Bruised / Battered / Mangled | Old injury |
| --- | --- | --- | --- |
| Head | INT | -20 / 40 / 60% | Frequent Headaches |
| Eyes | Melee/ranged/magic/vore final accuracy | -10 / 20 / 30 percentage points | Blurred Vision |
| Arm | STR | -20 / 40 / 60% | Stiff Arm |
| Hand | Melee/ranged/spell damage, excluding digestion | -1 / 2 / 4 | Weak Grip |
| Chest | Reduction in healing ceiling | -10 / 20 / 30% of normal maximum HP | Shortened Breath |
| Abdomen | VOR | -20 / 40 / 60% | Poor Digestion |
| Leg | AGI | -20 / 40 / 60% | Leg Pains |
| Knee | AP per turn, minimum1 | -1 / 2 / 3 | Weak Knees |

There is no shoulder part/protection. Helmet protects Head/Eyes; arm armor Arm/Hand; torso armor Chest/Abdomen; leg armor Leg/Knee. Armor reduces occurrence risk, not already-applied injury penalties. Retained animal targeting remains Head/Chest only; sapients use all eight parts.

Approved provisional tuning: lasting HP loss below10% of normal maximum cannot cause a body-part injury. At/above10%, injury chance equals the hit fraction (capped90%), multiplied by1/(1+protection). The hit chooses a part before armor risk is applied. Below25% hit fraction produces Bruised,25–below50% Battered,50%+ Mangled. Recovery days3/5/8. These values are code-level tuning, not balance options. Each qualifying hit is its own injury record; simultaneous temporary injuries add their penalties. Damage to temporary HP alone never qualifies. Grazes explicitly suppress body-part injury rolls, but their HP loss still contributes to accumulated-damage conditions. The previous extra near-death injury roll is retired.

## Recovery and old injuries

A hidden recovery roll is generated and serialized at injury occurrence. Bruised leaves no old injury. Battered has33% risk, or17% if treated. Mangled has66%, or33% if treated. Treatment changes the threshold without rerolling. Recovery removes the temporary record and may add/upgrade the corresponding old family. The leader never receives permanent complications. Old families upgrade I/II/III and cap atIII; no duplicate records per family. Legacy duplicate old records consolidate with capped summed tiers; old numeric body-part/grade IDs are preserved. Old records missing a stored roll get a stable ID-derived value, never a fresh load-time roll.

Old stat penalties are10/15/20%; eye accuracy penalties5/10/15pp; Weak Grip damage1/2/3; Shortened Breath healing ceiling reductions5/10/15%; Weak Knees AP1/2/3 (minimum1). Temporary and old penalties add. Damaging hits remain at least1 under existing hit rules.

Chest effects are healing ceilings, not destructive HP changes. Example: Bruised Chest plus Shortened Breath I allows healing to85% of normal maximum HP. Existing HP above that stays intact. Healing cannot increase HP above the ceiling; temporary-to-lasting conversion also respects the lasting ceiling. The ceiling floors at1HP. Post-combat recovery respects the ceiling without discarding already-existing lasting HP. The chest ceiling does not change damage-condition thresholds.

## Daily complications and systemic old injuries

Before that day's healing, Moderate Damage has5% chance of a complication and Severe Damage10%. The uniform pool contains Head/Eyes/Arm/Hand/Chest/Abdomen/Leg old injuries plus the four families below; knees are excluded. The leader remains immune. Rolls are daily campaign recovery only, never tactical turns.

- Poor Focus: maximum mana -5/10/15%, after explicit mana traits; minimum0. Current mana is clamped when necessary.
- Dulled Instincts: final critical chance -5/10/15 percentage points, including opportunity risk evaluation.
- Sluggish Recovery: daily HP healing -1/2/3, minimum1 before the chest healing ceiling.
- Sensitive Stomach: swallowed hostile prey's final escape chance +5/10/15pp, capped100%. Normal escape-disabled states remain disabled.

Ordinary/company recovery remains3% HP daily, field rest10%, town rest15%; enemy contract survivors2%. HP healing rounds up before Sluggish Recovery. Recovery progress1/2/3 days respectively. Injuries finish recovering before that day's HP healing. Mana recovery remains60% daily. Town treatment retains10gold/member,2recovery days removed, at least1remaining. Day-one injuries can be treated for reduced old-injury risk even when their remaining duration cannot be shortened. Old injuries cannot be treated. Sparring health uses full lasting HP at the healing ceiling and no temporary injury.

## Persistence and validation

CompanyInjury adds Tier, Complication kind and RecoveryRoll. CompanyInjuries.Same compares them during the existing save round-trip guard; validation checks enums, tiers, duration, finite roll range and leader restrictions. Derived damage conditions use serialized HP/temporary HP and need no separate persisted state. No new serializer/dependency/save foundation is introduced. Legacy fields retained for compatibility are not revived as mechanics.

## Verification and player review

The existing authorized native Unity audit is extended using synthetic characters and isolated diagnostic save roots under Logs; deletion remains suppressed. No personal saves are exercised. Runtime1 imported the new tier helper after an initial transient missing-type compile pass, then passed the earlier suite. Runtime2 found mana percentage truncation; corrected with stable floor tolerance. Runtime3 passed415 checks. Runtime4 additionally covers all temporary effects, eight armor mappings and STR equipment HP; passed451 checks. Final runtime5/build results are pending at this checkpoint. Headless audit disables drawing only inside the diagnostic session, not in game code.

One combined player review after a fresh build: check categorized Traits and injury Help; battle damage conditions/eye-hand-knee penalties and injury popup names; town treatment, rest and old-injury/healing-ceiling presentation. Tuning remains provisional. Existing source/asset provenance and public distribution obligations remain separate review gates.


## Final verification — 2026-11-04

Runtime5 passed455 checks. Source review restored the incomplete-preview HP guard and kept Submissive prey escape-disabled under Sensitive Stomach; runtime6 passed457 with actual native editor exit0. Final handoff build Warband-Review-20261104-1b8b2f8d/Win64/VoreWar.exe compiled exit0 with no C#/shader errors or new compiler warnings. A final Help-only wording correction clarifies sparring at the healing ceiling and is included in the final build. All prior failed/import/rounding attempts remain in Logs; only final clean results count as completed verification.

Seven existing license/notice files accompany the local build. Source notices and CHANGES coverage reviewed, no new dependencies/assets or changed legal grants. Public redistribution/provenance gates remain open. Graphical readability and actual player-driven combat remain for one combined review; source/runtime diagnostics do not claim visual testing.


## Provisional user acceptance — 2026-11-04

The user provisionally accepted the combined trait/injury milestone. Help content is acceptable for now. Focused manual injury testing is deferred because the probabilistic mechanics are difficult to exercise deliberately; this is not a manual gameplay pass. The existing457 automated checks and clean build remain the performed verification.

Deferred editorial work: rewrite Help as concise, task-oriented player guidance rather than exhaustive design notes. Keep detailed formulas, edge cases and implementation rationale in developer documentation; Help should prioritize what players need to do and understand. Do not start this rewrite or demand injury testing without further user direction.

## Later recovery changes and focused verification — 2026-11-05

The later approved recovery work changed body-injury durations to 5/14/28 calendar days, with one day of progress per day, and ordinary/field-rest/town-rest HP healing to 10/20/30%. These supersede the historical timing values above. Phase D was subsequently authorized and passed 671 focused assertions and 170,000 seeded trials, including exact probability boundaries, real saves/resume, reserves, wages and Single Battle isolation. See [the current verification record](38d-injury-validation-and-integrated-handoff.md). The approved gameplay rules passed with their existing tuning. Broad Help editorial work remains deferred.
