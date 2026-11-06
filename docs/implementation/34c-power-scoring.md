# Spec34 slice3 — Explainable power scoring

Implemented 2026-11-04 after the user accepted slice2. Scope: power estimates and existing consumers, plus a developer breakdown. Equipment/catalog, balance simulations, geography, roads and narrative remain later work.

## Scoring

CompanyPower replaces near-uniform trait allowances. Permanent modifiers are rebuilt locally without RNG, healing, status mutation or Crystaline's cached multiplier. Healthy capability ignores current HP and injuries. Current-condition estimates are separate diagnostics, never another encounter-budget reduction.

Battle Lab's Power page shows component points, total, action availability, current estimate and unsupported non-booster traits. Unsupported traits receive no fallback allowance. Some legacy booster fields and unusual abilities remain unmodelled; absence of a warning is not exhaustive valuation or proof of equal win rates.

Provisional weights live in CompanyPower.cs: stats0.30 each; normal HP0.20; species AP above2 gives3 points each; weapon damage/aim/INT critical value plus bounded range utility. Spells use tier/INT/range/AOE heuristics instead of invoking live callbacks; only affordable, non-retired spells count. Mana endurance is capped. Defense, evasion, injury protection, vore/escape modifiers and regeneration contribute separately. Baseline tiers use actual modifiers; special abilities have explicit weights. Juggernaut uses0.70 availability for skipped turns with persistent defenses.

Strongest weapon/spell modality counts fully; alternatives add25% flexibility, reflecting all-remaining-AP attacks. Formation pressure adds4% per member above3, capped20%, motivated by prior slug-swarm feedback and applied to both forces and company reference. These are scoring estimates, not new combat rules.

## Consumers and persistence

- Contracts retain fixed budget `45 + 30 × rating` and±8% band, independent of company. Composition includes formation pressure. Saved enemies/IDs/gear/casualties/stored power are not regenerated or regraded on load.
- Road encounters retain0.56 budget fraction and1.25 leader weighting. Health uses pooled lasting HP / normal MaxHP across members, including reserves. `1 - 0.5 × min(missing fraction,0.40)` gives100% strength at full health,90% at80% health and80% at60% health or below. Temporary HP, healing ceilings and injuries do not separately lower budgets. Travel-only triggers, cooldown, rewards/rest remain.
- Slugs use actual generated roster power nearest the budget rather than extrapolating from the first slug. One slug minimum; discrete units can prevent exact budget matching. Saved encounters remain intact.
- New recruits retain reputation-to-level/variety and wages, but must fit provisional ceiling `60 + 0.5 × reputation`. Bounded retries may try level1; no unapproved species fallback. Existing candidates/former mercenaries remain. Broader gear progression is later work.

## Validation and limits

Extended existing authorized isolated Unity audit, no new framework: non-mutating inspection, healthy/current separation, representative strict tier changes and all28 equipped family directions, pooled scaling/temporary HP exclusion, recruitment, seeded starter offers/higher ratings and new lab page. Synthetic files retained under Logs, deletion intercepted; no personal saves used.

Runtime1 narrow checks passed. Expanded runtime2 wrongly expected accuracy to affect non-resistible Power Bolt; runtime3 misinterpreted incoming-modifier direction; runtime4 expanded an array into multiple reflection arguments. Fixtures corrected (resistible Fireball, inverted incoming direction, roster passed as one argument); earlier logs retained. Final results recorded below at handoff. No rendered playtest or measured win-rate calibration claimed; simulations remain slice6.

## Licensing

New fork CompanyPower.cs/.meta; modified CompanyContracts, CompanyRoadEvents, CompanyEconomy, DeveloperBattleLab and existing TraitCurationAudit. Dated notices/exact CHANGES paths, inherited legal/attribution retained, License4–5 reviewed. No outside source/assets/dependencies or scene/prefab/package changes; no files moved/deleted. Public-distribution/provenance/Corresponding Source obligations remain separately tracked; private build does not clear them.

## Handoff

Final runtime5 passed693 checks (524 prior plus169 focused), actual editor exit0. Fresh standalone `Builds/Warband-Review-20261104-4326d84d/Win64/VoreWar.exe` built exit0, no C#/shader errors;41 distinct compiler warning messages match the preceding build. Evidence: Logs/power34c-runtime5-result.json, runtime5.log, build.log and power34c-result.json. Seven existing notices copied/hash-verified into the new build. Earlier builds/logs preserved.

User review: inspect Battle Lab > Power while changing a tier or HP; healthy power should respond to equipment/traits but not current wounds. In a fresh campaign inspect starter recruitment/contracts and ordinary combat composition. Balance and rendered readability remain human review; no repetitive menu-opening check requested. Stop here pending user feedback. Slice4 equipment requires its remaining design decisions before implementation.
