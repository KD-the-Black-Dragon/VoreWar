# Four-stat consolidation and combat revamp

Recorded 2026-11-03. The user subsequently authorized implementation of the stat, graze and critical revamp. Earlier planning decisions are retained below; current implementation details and verification status are recorded at the end. Leadership remains excluded/deprecated.

## User's agreed stat roles

| Stat | Requested effects |
| --- | --- |
| Strength (STR) | Melee accuracy, melee damage, HP, resistance to digestion damage while swallowed |
| Agility (AGI) | Ranged weapon accuracy, ranged damage, evasion, vore evasion |
| Intelligence (INT) | Magic accuracy, magic damage, mana, escape chance while swallowed, magic resistance, non-damage spell strength, critical-hit influence (formula unresolved) |
| Voracity (VOR) | Swallow chance, stomach capacity, digestion speed, keeping swallowed prey from escaping |

These roles include the user's follow-up decisions on 2026-11-03. Implementation is now authorized; numerical tuning is provisional as recorded below.

## Fixed tactical AP and positioning

AP is based on species specialty, not AGI or a character's chosen build:

| Species specialty | AP |
| --- | --- |
| Melee | 3 |
| Ranged | 2 |
| Mage | 2 |
| Voracity | 3 |

The user wants melee to be able to charge down archers, ranged units explicitly unable to kite, and low mobility so positioning matters. These are acceptance goals, not merely a request to reduce displayed AP. Campaign daily actions remain separate.

Source implications: ordinary attacks and spell casts currently use all remaining AP, with special multiattack branches retaining AP through fractional costs. Walking pays the destination tile cost. Existing Charge, speed bonuses/multipliers, minimum speed, movement penalties and terrain costs require review against the new small fixed allowances. Do not silently retain bonuses that defeat the positioning goal, or silently remove species abilities and injuries. No changed costs, modifier policy, engagement system or AI behavior has yet been agreed. In particular, test/review moving away then shooting, multiattack, Flight/leap and terrain when implementation is authorized; a 3-versus-2 base allowance alone is not proof that kiting is impossible.

Follow-up confirmation: most attacks and abilities continue to consume all remaining AP as the general rule. Trait exceptions are not implicitly approved for restoration; discuss their action economy if/when the user reintroduces them.

## Escape design requirement

Preserve random escape as a meaningful safeguard against a lucky swallow. A high-INT, high-HP unit swallowed by a low-VOR predator should have an appreciable escape chance; succeeding at a low-probability swallow must not itself imply inevitable digestion. Exact percentages, HP contribution and timing remain to be workshopped, without promising escape or survival.

The user does not require preservation of hidden stamina or the Endosoma drain. Any retained internal mechanic must serve the INT-versus-VOR escape goal, rather than quietly defeating it. Do not add another player-facing resource or require a stamina-system decision before proceeding with formula design. Existing Downed no-escape rules are not changed by this discussion.

## Critical hits and grazes

User approves re-enabling critical hits and tying them to INT, rationalized as insight and exploiting opportunities. Exact critical chance, caps and any critical-resistance formula remain unresolved.

The latest direction supersedes the earlier idea of INT-based grazes. Grazes become a stat-independent rescue of a miss:

1. Resolve the ordinary accuracy roll.
2. A successful hit is never downgraded into a graze.
3. If the attack misses and its accuracy is at least 51%, make one additional independent silent random roll for a graze, at that same accuracy percentage.
4. Success upgrades that miss into a reduced-damage graze; failure leaves it a miss. Below 51% accuracy, a miss gets no graze roll.

There is no separate INT or other stat bonus to this additional roll. The ordinary accuracy threshold can still depend on the normal attack stats. The user explicitly chose the same displayed percentage for the additional roll: a 51% attack retries at 51%, a 75% attack at 75%. Spell applicability and on-hit effects still need design before implementation.

Graze damage uses the existing Vore War calculation, explicitly confirmed by the user on 2026-11-03: multiply the ordinary attack damage multiplier by `Config.GrazeDamageMod` (default 0.30), the attacker's outgoing and defender's incoming `GrazeDamageMult` trait factors, and the applicable Unit Tag graze-damage factor; retain the ordinary downstream damage calculation, rounding and minimums. This preserves the damage calculation, not the old downgrade-on-hit rule or its cancellation with criticals. Existing modifier support does not itself authorize reintroducing retired traits or content.

On a successful initial hit, roll for an INT-based critical; failure leaves normal hit damage. A rescued miss is a graze, not a critical-hit opportunity. Show only the final resolved result (miss, graze, normal hit or critical), with applicable damage; do not show intermediate failed rolls, a transient miss before a graze, or dice values. The user's die numbers illustrate outcome order, not an exact integer success-boundary algorithm; preserve the stated probabilities rather than inferring an inverted probability from roll-high examples.

For accuracy p at or above 0.51, first-roll hits have probability p (normal/critical split unresolved), grazes have probability (1-p)*p, and final misses have probability (1-p)^2. At 51% accuracy these are 51%, 24.99% and 24.01%. Below 51%, use the ordinary hit/miss roll without a graze retry. The displayed accuracy remains the initial full-hit chance; it must not be silently relabeled as combined hit-or-graze probability.

Source review confirms the old optional system does the opposite: `Actor_Unit.Attack` rolls graze before `Defend` resolves accuracy, multiplying damage by the configured graze fraction (default 0.30). If the attack then hits, it deals reduced damage. A separate critical roll can cancel the graze back to normal. Thus the old system can downgrade a hit and cannot rescue a miss. It remains disabled in current source; the new design is unimplemented.

## Source audit and follow-up allocations

| Effect | Current source | Follow-up decision |
| --- | --- | --- |
| Tactical movement/AP allowance | Agility; `Actor_Unit.MaxMovement` and `CurrentMaxMovement` | Fixed species-specialty allowances above |
| Resisting resistable spells, including hostile status spells | Will; `Actor_Unit.GetMagicChance` | INT |
| Resistance to digestion damage | Endurance and Will; `PredatorComponent.CalculateDigestionDamage` | STR |
| Keeping swallowed prey from escaping | Stomach, Voracity and capacity; `Prey.UpdateEscapeRate` calculation | VOR |
| Non-damage spell potency and duration | Mind in many `SpellList` effects; Will in some abilities such as Amplify Magic | INT for strength; duration mapping not explicitly confirmed |

Movement here means tactical allowance, not the company's daily campaign action budget. Magic resistance affects success odds; it is not a universal reduction of damage after a spell hits. Digestion resistance is separate from escape chance, maximum HP and the predator's digestion power. The source confirms Endurance's ordinary damage-resistance role here is digestion, not a universal armor bonus.

## Conditional and specialized effects

- Stamina maximum currently uses Strength and Endurance (`Unit.MaxStamina`). Escape rate is multiplied by remaining stamina percentage. The explicit stamina drain found is the special Endosoma containment branch in `PredatorComponent`; ordinary digestion damages HP. This is not a general attack-fatigue system. Follow-up direction prioritizes escape behavior above preservation of this internal resource.
- Stat-based critical hits and grazes exist in `Actor_Unit.CritCheck`/`GrazeCheck`: Strength/Dexterity contribute to offense, Endurance/Will to critical defense, and Agility to grazing. These branches are gated by combat-complication and stat-crit/graze settings, which are false in `ContentSettingsData` and the world defaults. Planned replacements are INT-based critical hits and the stat-independent miss-rescue graze rule above, both unimplemented.
- Traits, items and special actions have individual old-stat dependencies: e.g. Dexterous Defense blocking (Dexterity), Dazzle checks and Stunning Strike duration (Will), Swift Strike (Agility), Multifaceted highest-stat branches, Bramble Band retaliation (Endurance), and prey stealing/transfers (Strength/Voracity/Will). Each needs a deliberate migration or retirement decision rather than automatic substitution. This is a dependency category, not an exhaustive per-ability migration inventory.
- Hybrid weapons and magical weapons have special scaling beyond ordinary melee/ranged formulas. Review their intended role when migrating equipment and spells.

## Important redistribution

Current vore defense uses Strength and Will; the proposal explicitly assigns it to AGI. Current escape uses Strength, Dexterity, Endurance and especially Will; the proposal assigns it to INT. Stomach's functions merge into VOR. These are intentional balance changes, not simple label replacements. Existing stat values must not simply be summed into the four new stats without revisiting formulas.

AGI carries ranged offense and both physical/vore evasion, but no longer grants base AP under the agreed direction. INT combines magical offense/defense, escape and critical influence; tune coefficients with attention to effects on non-mage builds rather than assuming all stat contributions should be equal. Grazes do not add another INT benefit.

## Later implementation scope, only after design approval

Inventory stat reads and numeric indices, species defaults/developer overrides, growth/level-up, gear and spells, traits and Unit Tags, injuries/status effects, enemy power ratings/AI, UI/tooltips/help, and save data. Agree on save compatibility and numerical conversion rather than assuming old eight-stat arrays can be relabeled. Preserve unrelated accepted behavior and apply the normal source-notice/GPL workflow when implementation is authorized.

This record is documentation only. No game source, assets, settings, saves or build changed; no tests or build were run for the planning discussion.

## Implemented milestone — 2026-11-03

The implementation section supersedes earlier planning-only/unimplemented status statements above. No retired species, abilities or content have been re-enabled.

- Four authoritative stat slots retain original numeric IDs STR0, AGI3, INT5, VOR2. Old DEX/WILL/END/STM identifiers resolve to those four for compatible traits/items/tags; Mind is a serialized compatibility alias for Intelligence. No extra independent player stats. Explicit active-stat lists prevent alias duplication in level-up, stat totals and power budgets.
- One-time old-unit conversion: STR=round((oldSTR+2oldEND)/3), AGI=round((oldDEX+oldAGI)/2), INT=round((oldMIND+2oldWILL)/3), VOR=round((oldVOR+oldSTM)/2), minimum1. Remove old equipped accessory stat contributions before averaging and restore their mapped bonuses afterward. Apply analogous min/max-range conversion to species data; developer controls expose four ranges and mirror compatibility fields on save. Fresh units roll only the four converted ranges. No personal saves/settings are rewritten by the agent.
- HP uses3baseSTR (or effectiveSTR when the existing code setting enables stat-boost HP), with inherited health multipliers/explicit equipment health effects. Mana uses3baseINT and its mana multiplier. Strength growth/equipment changes adjust current and maximum HP together. Growth grants the existing +1 general gain once per active stat and +4 to the selected improvement; four choices are available, subject to existing non-predator/brainless restrictions. Company power uses four stats at0.36 each instead of eight at0.18.
- Species base AP is2 for Avian/Lizard/ranged Spitter Slug and3 for Gnoll/current default melee/vore specialty. Chosen character focus and gear do not alter species AP. Existing injury/fullness penalties still reduce available AP, minimum1. Positive speed bonuses/multipliers no longer increase this base; Featherweight retains its defensive effects and loses its contradictory +1AP claim/boost. Ordinary attacks/spells still spend all remaining AP; no retired multiattack traits restored. Supporting one-third-AP action and AI estimates clamp cost to1, avoiding free actions at2AP.
- Swallow defense uses AGI; retention and capacity use VOR; digestion power uses VOR against STR resistance. Escape uses INT versus VOR with the inherited HP/fullness/time/trait/tag/config factors, without hidden stamina scaling or the hostile Endosoma zero-stamina escape block. Downed/sleeping and friendly containment restrictions remain. These are probabilistic safeguards, not guarantees.
- Weapon outcomes: first roll at actual accuracy; on miss with p>=0.51 retry once at p; on successful initial hit roll critical. Graze damage retains the prior pre-damage multiplier pipeline, default0.30 with outgoing/incoming trait and Unit Tag multipliers and ordinary downstream rounding/minimums. Critical chance provisionally clamp(0.05+0.0025*effectiveINT,0.05,0.35), then existing critical chance trait/tag modifiers; inherited critical damage default1.5x and damage modifiers. No stat-graze modifier and no graze/crit cancellation. Logs distinguish graze/critical/normal hit, while intermediate rolls/misses stay silent. Marked accuracy is included in both preview and actual hit chance. This deliberately covers ordinary weapon attacks, matching the scope of the old critical/graze system; spells and special status/vore abilities retain their own checks.
- Company binding upgrades all stored characters including former members and enemies, clears old cached choices, and clamps old battle AP on conversion only. Subsequent saves preserve current AP and unit values. Original empire saves remain unsupported. Old source aliases/config schemas remain compatibility data, not new gameplay options.

### Verification and remaining review

Source baseline: `Logs/stats29-before.json`; packet-specific diff and notice review are recorded separately. Native clean compilation `Logs/stats29-clean-compile.log` exited0 without C# errors after first import. Initial non-native attempt stalled on license access; its process was stopped. Initial native import contained stale missing-new-helper diagnostics and is not clean evidence. Final standalone build after all refinements passed, exit0: `Logs/stats29-review-build.log`. The native build compiled player scripts and loaded the build scene without scanned C# errors or runtime/import exceptions. Existing upstream warnings remain. Review player: `Builds/Warband-Review-20261103-2818594a/Win64/VoreWar.exe`. No new test framework or automated tests added; no graphical playtest claimed.

One combined player review should cover: new campaign four-stat details/level-up/gear HP; Single Battle species AP and no AGI-based AP growth; final-only normal/critical/graze/miss feedback; STR digestion resistance and INT escape versus VOR; map and mid-battle save/load preserving stats/HP/AP. Include the two accepted Spec28 border/roster-padding corrections in the same review build. Do not repeat broad unrelated screen tests.

Mapped trait bonuses are applied once per trait using the strongest absolute contribution among that stat's old source aliases; paired legacy stat multipliers use the stronger deviation from1. This prevents a generic all-stat bonus from being counted twice. Exact migration, critical and power coefficients remain provisional tuning, not a claim of final balance or proof that all terrain/ability matchups prevent kiting.

Final scope/notice review: `Logs/stats29-source.diff` and `Logs/stats29-notice-review.json` cover26 modified C# sources plus the new original helper. No source paths removed; no external assets/dependencies added. The serialized scene/art/package files remain unchanged by this milestone. Seven existing legal/change/dependency notices packaged in the local player. No graphical playtest, new tests or repeat synthetic save suite run. Prior Spec28 border and roster-padding corrections are included. Await one combined player review before further changes.

## Player feedback — compact HUD and hidden hover, 2026-11-03

User accepts most of29 but reports large stat gaps and obsolete tooltips in blank spaces. The inherited FlexibleGridLayoutGroup stretches the remaining active rows into the old180-unit stat block. Geometry-based hover checks ignore object activation and still inspect hidden old labels. Three-source UI-only correction:30-unit stat rows,60/90-unit block depending on visible counters; inventory positioned8units after that block, two existing56-unit slot rows, traits viewport immediately below. Keep existing fonts/portrait/bars/counters and recover vertical space. IsMousingOver now rejects inactive/null controls; live item-label checks use the same guard. InfoPanel updates compact layout after counter visibility decisions. No scenes/prefabs or gameplay/save changes. Snapshot/diff evidence `Logs/stats29-ui-feedback-before.json` and `Logs/stats29-ui-feedback-source.diff`. Build/visual status to follow; no new tests/framework.

Feedback correction build passed, exit0 with no scanned C# errors/exceptions or new compiler warnings: `Logs/stats29-ui-feedback-build.log`. New local player: `Builds/Warband-Review-20261103-4b5894ed/Win64/VoreWar.exe`. Three source notices/CHANGES reviewed, scene/prefab/license hashes unchanged from feedback baseline; seven legal/change notices packaged. No graphical playtest or automated tests run. Review only compact stats/inventory/traits placement and absence of obsolete tooltips while visible stat/item tooltips still work.
