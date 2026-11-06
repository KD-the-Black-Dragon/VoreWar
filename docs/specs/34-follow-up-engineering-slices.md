# 34 — Follow-up engineering, one reviewed slice at a time

**Closed by the user, 2026-11-05.** The original eight-slice follow-up module is complete. Subsequent calibration, campaign economy and grouped display/injury review are a separate planning effort in [spec 38](38-calibration-economy-and-quality-review.md); its planning request does not authorize implementation. The dated scope and handoffs below are retained as history, including superseded map dimensions and earlier stage boundaries.

2026-11-05 follow-up: the user authorized cleanup, recovery, focused comparisons and the larger-world/roads work in one ordered batch, with stops only for useful design input. [Spec35](35-cleanup-recovery-and-world.md) records the approved160x120/six-town choice and current implementation. The earlier cadence below remains the historical plan, not a requirement to stop between those newly authorized items.

2026-11-04. User-approved directions organized into a plan; no implementation or testing performed by this document. The user monitors usage and explicitly rejects doing this whole program in one run. Complete only the next authorized slice, record an exact checkpoint, hand off and wait before starting another. Split further if a slice exceeds a reasonable session budget; do not replace the user's grouped review preference with tiny repetitive tests.

## Boundaries

- Unity2018.3.12f1; preserve retained map artwork and shared blue/gold UI.
- No new event narrative, Help rewrite or other prose/content-writing project. Necessary short mechanical controls can be added as part of tools/UI; obtain user direction for gear naming/content where needed.
- Current traits/injuries remain provisionally accepted. Focused injury playtesting is deferred; the developer lab should make future testing deliberate rather than requiring random combat outcomes.
- Crystaline/Fractured is known broken and extremely low priority. Add an explanatory source comment during slice1, preserving behavior; do not fix/retire/retune it or make it the basis of ordinary preview accuracy claims. A preview must not compound the cached damage multiplier merely by inspecting a target.
- Contract ratings continue to denote fixed enemy power, independent of company growth. Existing generated/saved forces must not silently regenerate when scoring changes.
- No filesystem deletion. Synthetic diagnostic saves only for automated runs; preserve them and suppress game deletion during checks. Keep notices/CHANGES per source batch; no engine/package upgrades or publishing.

## Slice1 — Correct AI targeting and damage previews

Scope: reproduce target-versus-itself GetMagicChance calls in TacticalAI, use the actual caster and audit equivalent branches. Make spell previews account for material outgoing/incoming damage modifiers, armor/resistance, flat trait/injury effects and relevant current state through a read-only calculation. Preserve intentional random variance; do not demand exact prediction of random hit/crit/damage outcomes. Do not spend mana, roll RNG, apply statuses or mutate cached boosts during previews. Keep actual action effects in resolution. Label/record unsupported edge cases rather than repairing unrelated obscure mechanics.

Add the requested known-broken/very-low-priority source comment at Crystaline/Fractured's compounding branch. Test preview queries for absence of state mutation and compare estimates to deterministic actual damage in supported cases; caster/target asymmetric INT/trait fixtures verify targeting. Compile and give one combined review/build when useful. Stop after handoff.

## Slice2 — Developer battle and recovery lab

Add a main-menu button adjacent to Race Editor, using the same existing214214214 convenience-password gate. Shared UI styling. Use an isolated synthetic scenario, not a hidden editor for the current campaign; never overwrite or invalidate personal saves.

Bounded first lab: choose approved species/gear/traits and tier, apply/remove specific temporary/old injuries, set lasting/temporary HP, trigger a controlled attack or spell, inspect current stats/AP/HP/healing ceiling and step recovery/treatment. Provide reusable saved fixtures or reproducible seeds where practical. Validate graze immunity, injury threshold, treatment rolls, temporary HP transitions and recovery deliberately. No full map/event editor, arbitrary scripting console or publicly available cheat controls. Human review focuses on whether the controls let the user deliberately reproduce an injury case. Stop after handoff.

## Slice3 — Power scoring that understands the new mechanics

Implementation: [34c power scoring](../implementation/34c-power-scoring.md). Pooled lasting/normal HP includes reserves; current-condition diagnostics do not additionally scale encounters. Values remain provisional; slice4 remains separate.

Replace near-uniform trait values with explicit tier/effect-aware estimates. Account for relevant stats, HP, AP, weapons/range/accuracy, spells, defenses, positive/negative traits, and composition/swarm effects where evidence supports them. Provide a developer breakdown so surprising scores can be explained and tuned. Use fixed fixtures and measured outcomes, not an opaque claim of perfect balance. Keep contract difficulty fixed and saved units unchanged.

Use the scorer for enemy generation and road encounters; record healthy capability separately from effective current condition. Use healthy capability as the reference with the approved capped health adjustment below; do not additionally weaken the encounter through an undocumented injured-power multiplier. Keep the previously reduced slug encounter pressure in mind. Revisit recruitment's power/availability mapping rather than assuming level alone equals talent. Stop after handoff.

## Slice4 — Equipment mechanics and merchant foundation

Implemented boundaries and evidence: [34d equipment foundation](../implementation/34d-equipment-foundation.md). User confirmed direct weapon/spell hits only (including area), after existing reductions and before barriers/temporary HP, minimum1 before barriers. Gear evasion affects melee/ranged/resistible spell accuracy; vore and guaranteed hits unaffected. Existing armor fixtures receive provisional AC/evasion; broader catalog/prices remain slice5.

Extend item definitions to express damage/accuracy and armor-class/evasion choices in a consistent, scoreable form. Armor has positive AC and/or evasion bonuses, without armor penalties. AC provides randomized deflection and covers magic as well as physical damage; follow the approved rule below. Preserve slot restrictions, base-STR HP rules, injury coverage and save-safe item identity. Add merchant category navigation (including Leg Armor and the other equipment slots) so catalog growth does not require paging through everything. Add concise mechanical comparisons, not item lore. Use a small set of fixtures/current items to verify the mechanism before broad catalog balancing. Stop after handoff.

## Slice5 — Gear progression, expensive investment and reputation stock

User confirmed whole-tier unlocks, identical eligible stock in all towns and unlimited quantities. Also approved equipment-inclusive hiring and higher starting gold. Implementation/provisional prices and thresholds: [34e gear progression](../implementation/34e-gear-progression.md). No stock/restock subsystem is needed for this version.

Using approved mechanical tradeoffs, expand the actual weapon/armor catalog and reputation-dependent availability. Higher-tier gear should require a serious gold investment, not trivial spare change. Preserve ability to buy lower-tier affordable gear. Confirm whether reputation reveals item tiers or individual stock and how regional/restock variation should work; do not invent a complex new resource or crafting system. Review gold prices against contract income and wages. User supplies creative naming/lore if desired; do not make prose generation part of this slice. Stop after handoff.

## Slice6 — Automated campaign and battle balance reports

Completed initial native matrix/report: [34f balance review](../implementation/34f-balance-review.md).51 battles across17 fixtures/3 seeds,150 cloned recovery comparisons and24 explicit-policy economy runs. No tuning applied; outliers and follow-up recommendations are separated from measured results. Editor-only runner; existing player build unchanged.

Run controlled synthetic scenarios across company compositions, contract ratings, gear tiers and injury conditions. Measure survival/downing/digestion, battle duration, healing downtime, wages/debt and net contract income. Include strengthened-leader/weak-company cases and concentrated ranged/swarm pressure. Preserve outputs/seeds and diagnostic save isolation. Summarize outliers and propose tuning separately; do not silently alter player-approved mechanics to achieve a target win rate. AI-driven simulations are measurements under that AI, not proof of optimal human play or final balance. Stop after a report and any specifically authorized tuning.

## Slice7 — Larger, more coherent world generation

**Complete and accepted, 2026-11-05.** The later authorized four-item batch and map feedback covered this scope through specs 35–37. Current accepted result: 120 × 80 region, six logical zones/one town each, bottom-center start, coherent reproducible terrain and closer town spacing. Seed/performance/save checks and retained artwork/virtualized map are recorded in the [world review](../implementation/35-cleanup-recovery-and-world.md), [compact-map review](../implementation/36-compact-zones-roads-and-fields.md) and [accepted journey review](../implementation/37-town-road-journeys.md). No required slice 7 implementation remains.

Original scope: agree dimensions/town distribution/geography, preserve the six-town limit, generate a reproducible coherent region with useful starting offers, check seeds/performance, preserve recorded campaign layouts and separate logical geography from artwork. The original 80 × 60 and intermediate 160 × 120 proposals were superseded by the accepted 120 × 80 layout. Existing road/farm normalization is documented separately; towns/maps are not regenerated on load.

## Slice8 — Roads, travel and map integration

**Complete and accepted, 2026-11-05.** The same batch also implemented this slice. The accepted road circuit gives each town two distinct exits to different towns; measured connections 28–34 moves and mean nearest-town distance 30.125 moves (~five six-action budgets). Roads are placed after terrain/towns, then cleared to one-action grassland. Original farm art, road overlays/fog, ordinary off-road travel, map dragging and save compatibility are included. See the reviews above and their native validation records. No required slice 8 implementation remains.

Original scope: connect towns with continuous visible routes, preserve unrestricted off-road travel and retained terrain artwork, settle movement costs, integrate fog/contracts/saves and validate map/window behavior. The later user decision settled road cost through actual grassland clearing. Further geography/travel extensions are separate future scope; deferred narrative/post-battle events remain deferred.

## Deferred — Post-battle trait events

Record only; the user explicitly does not want to tackle this now. Do not add hooks, event scaffolding, narrative or trait rewards during the slices above without later authorization.

Intent: after each battle, choose one post-battle event from noteworthy actual battle occurrences, unless the leader is the only remaining participant. Reserve members never qualify. An example extraordinary occurrence is a participant surviving being swallowed three or more times; unusual ranged activity is another candidate. Extraordinary cases should have greater selection priority/chance than mundane cases, with a mundane fallback ensuring an event when eligible. Outcomes may give positive or negative related traits/upgrades and support emergent character stories. Later work must define the occurrence catalog, eligibility/fate boundaries, extraordinary/mundane classification, selection weights and exactly-once saved event/reward resolution; the user authors the narrative.

## Approved clarifications — 2026-11-04

### Random encounter health adjustment

Encounters occur only when actually moving, not while resting in place. Being badly wounded does not exempt the company. Reduce the usual encounter strength by1% per2% of company HP missing, with the missing-health input capped at40% (maximum encounter reduction20%). Formula: encounter multiplier = 1 - 0.5 * min(missingHealthFraction,0.40). Examples: full health100% strength;80% health90% strength;60% health or below80% strength. This modifies random encounter budgets only, not fixed contract difficulty. Retain the established leader weighting in the normal power calculation. Determine the exact aggregate HP weighting when implementing slice3; avoid silently double-counting wounds through another effective-power reduction.

### Armor

For AC=N, roll an integer uniformly from0throughN, equivalent to1d(N+1)-1, and subtract it as armor deflection. AC10 therefore deflects0–10 damage per roll. It covers magic damage too; magic protection is not a mandatory separate property. Gear offers positive AC and/or evasion; no armor-induced evasion or other penalties. Successful damaging hits retain the already-approved minimum1damage. Future previews should reflect the resulting range/expected outcome without rolling or changing gameplay state. Before slice4, settle damage-source boundaries (digestion/status ticks vs direct hits), ordering with other mitigation, and evasion's covered attack types; do not silently invent these details.

### Roads and geography

Off-road travel is unrestricted. Generate terrain and towns first, roads last. Roads may curve/bend and be surrounded by forest/mountains, but following one should lead to a town and never abruptly terminate at a mountain or another terrain obstacle. Validate route continuity and town connections, not merely road-shaped visual decoration. Traversable passes/bridges and unusual geography need decisions only if that terrain exists in the planned generator. Exact world dimensions, town count and road movement costs remain open for slices7–8.

Other design questions should be asked just before their dependent slice, not as a large up-front interrogation. These clarifications update the plan only; no gameplay code or tests were changed.
