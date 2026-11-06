# Phase B — Company economy and individual investment

Implemented, 2026-11-05. The user authorized implementation after the Flight retirement detour. This record distinguishes candidate tuning and completed evidence. No public release is declared.

## Implemented systems

The leader has zero personal wages and represents company upkeep in the roster. On payday, an outstanding upkeep debt from the preceding bill ends the company before any new bill, employee departure or payroll is processed. Available treasury cash does not silently repay that existing debt. The player must explicitly use Pay Upkeep before the deadline.

Otherwise the new upkeep bill uses available cash first, with only the unpaid shortfall becoming debt due before the next regular payday. Payroll then follows the established all-employees personal-debt rule when the company cannot settle it. Individual debts, warnings and departures remain separate. Warnings interrupt rest through the existing resumable daily stage, and an ended company cannot continue advancing days. The normal ending/autosave path handles Ironman invalidation with deletion intercepted in synthetic checks.

Accepted level-ups change future weekly wages at the next regular payday. They do not charge a midweek top-up or modify existing debt. Before the stat choice commits the level-up, the UI shows old/new individual wages and old/new company weekly totals alongside upkeep. The wage quote calculation is pure.

## Selected provisional values

| Parameter | Selected value |
| --- | --- |
| Starting gold | 260, retaining the existing opening purse |
| First weekly upkeep | 40 gold |
| Increase per later weekly bill | 10 gold, uncapped |
| Mercenary wage | BaseWage + 3n + 2n² + 2max(0,n−3)³, with n = level−1 |
| New contract reward | 240 × rating, with integer ±10% variation, rounded to tens |
| Full basic heavy/light armor | 69 / 68 gold |
| Full fine heavy/light armor | 630 / 710 gold |
| Full masterwork heavy/light armor | 2,450 / 2,750 gold |

A base-20 mercenary costs 20, 25, 34, 47, 66, 101, 164, 267, 422 and 641 gold at levels 1–10. Individual base quotes retain their existing 18–22 range. Gear/traits do not directly change wages.

The two-worst-quotes opening cost is 244. Three of even the cheapest current species cost at least 264, including when the player disables the other species. Candidate starting purses of 320 and 280 were rejected on that boundary. Two hires remain possible without guaranteeing an idle first payday. The first successful contract supplies operating capital, and a missed upkeep bill allows the confirmed grace week.

The lowest new rating-1 quote is 220. A full-week maximum-cost basic replacement is 122, while two maximum starting wages plus initial upkeep cost 84. That leaves 14 gold after a seven-day contract/recovery allocation and replacement. Mean payout leaves 34. Eight-to-ten-day recovery, accepted levels, higher weekly upkeep or restored armor investment narrow or remove this surplus, which is an intentional pressure to complete more work or take harder contracts. Prorated replacement advances cost less midweek.

At a mean four-day cadence, rating-1 income is about 420 per week. Two base-20 level-8 employees cost 534 before upkeep, making this work structurally unprofitable. At seven-day cadence the boundary is level 6. At two-day cadence it is level 9. These are break-even illustrations, with initial upkeep and no casualties, rather than bankruptcy dates. A company can coast on accumulated capital. Better/worse reward variation and actual travel/rest cadence change the boundary.

The fine and masterwork gear prices retain their pre-phase curve and measured combat benefits. Basic armor is cheaper. Purchase-horizon examples and replacement/day-of-week bounds are in Logs/economy38b-curves.json. They keep initial capital distinct and allocate recurring costs without charging recruitment advances twice.

## Save and repricing policy

Existing campaigns are enrolled in economy version 1 at their next scheduled payday, beginning with the initial upkeep amount. Campaign age does not produce historical bills. Existing money, accepted contract quotes, personal debt, advances and owned item identities/stats remain. Catalog price changes apply to new purchases, hiring gear and half-price resale of owned items. Enemy power, gear stats, AC/evasion, XP, contract difficulty and injury rules remain unchanged.

The save verification guard checks upkeep enrollment, bill count, debt/deadline, payday, money, pending decisions and ending state, plus each character's wage base and personal debt/departure fields. A pending daily stage resumes after the committed payroll stage without charging again.

## Calibration method and current evidence

Phase A's actual mitigation/outcome calibration and light-armor values are retained. Economic diagnostic histories use the existing CompanyBalanceAudit with a partial editor source, native campaign state and synthetic storage under Logs. No new test framework was added.

The first pilot passed 36 boundary/quote checks and completed a native 90-day balanced campaign. It experienced 45 battles, 36 completed contracts, two mercenary losses, replacements and 17 rest days, ending with 9,814 gold. This exploratory pilot used the first quadratic wage candidate, inexpensive-biased hires and single-contract outings. It is not a controlled final-candidate comparison. Evidence is Logs/economy38b-pilot-directory.txt and economy38b-pilot1.log, actual Unity exit 0.

Pure cash-flow comparisons in Logs/economy38b-curves.json examine alternative accelerating wage curves and opening upkeep/capital combinations. They are equations, without fabricated victories or inferred survival. At a four-day contract cadence, two base-20 employees under the selected veteran-tail candidate stop covering their wages plus initial upkeep from rating-1 work at level 8. Faster work changes that boundary. The maximum-income and idle/no-level policies are analyzed separately.

The discovery run completed four connected policies under the earlier 20+5 upkeep candidate and 320 opening purse. Balanced/conservative reached 180 days with 15,216/7,034 gold, risky was wiped out on day 49, and banking levels reached 180 days with 2,374 gold and four losses. These are exploratory observations under different initial funding and routing, rather than a paired comparison with the final tuning.

The final matrix completed all 14 connected histories under the selected production values, with actual Unity exit 0. It covered 1,299 elapsed campaign days and 602 native battles. Four core policies had a 180-day maximum and ten secondary policies had a 90-day maximum. Earlier losses retain their actual ending dates. All 14 cash ledgers reconciled exactly. The 64 focused checks plus those 14 ledger checks give 78 assertions in the final run. Evidence is Logs/economy38b-final-directory.txt, economy38b-final1.log and economy38b-policy-summary.json.

| Policy | Days | Native battles | Mercenary losses | Final gold | Outcome |
| --- | ---: | ---: | ---: | ---: | --- |
| balanced | 180 | 79 | 1 | 18,996 | Censored at horizon |
| basic-all | 90 | 46 | 0 | 14,418 | Censored at horizon |
| favorite-veteran | 90 | 48 | 2 | 6,849 | Censored at horizon |
| gear-first | 90 | 41 | 1 | 5,171 | Censored at horizon |
| grow | 90 | 52 | 5 | 14,270 | Censored at horizon |
| conservative | 90 | 34 | 0 | 809 | Censored at horizon |
| risky | 91 | 56 | 5 | 9,251 | Company wiped out |
| bank-levels | 180 | 36 | 4 | 852 | Censored at horizon |
| leader-only | 42 | 0 | 0 | 0 | Critical insolvency |
| slow-field | 43 | 33 | 2 | 8,308 | Company wiped out |
| tour | 90 | 52 | 1 | 11,227 | Censored at horizon |
| role-ranged | 90 | 46 | 0 | 8,466 | Censored at horizon |
| role-magic | 90 | 46 | 2 | 7,515 | Censored at horizon |
| role-melee | 43 | 33 | 2 | 8,308 | Company wiped out |

These policies share one final seed, 6101, and include correlated variants. The table is exploratory policy coverage, without an inferred independent-trial survival probability. Native AI controls combat. Routes use full terrain knowledge and minimum actions, making travel optimistic. Current terrain costs, road clearing, action limits, offer pools/refresh, contract deadlines, multi-objective outings, recruitment, actual casualties, destroyed gear, injuries, rest/treatment, reputation unlocks and wage choices still operate normally. Road encounters are real battles with no invented payout. Practice duels are declined. Initial money is kept separate from earned income, and recruitment advances are counted once.

The conservative leader-only policy only accepts power-eligible work. It found no suitable initial offers and ended for insolvency on day 42. This is evidence of the idle calendar pressure, rather than a test of an expert human winning solo battles. Separately, the analytical maximum-income bounds allow perfect success with no losses, zero employee wages and all town offers. Because those receipts are bounded while upkeep rises without a cap, even that excessive upper bound eventually becomes unprofitable. A desirable human long-run survival horizon still requires playtesting.

### Continued low-contract company

The level-banking company completed 26 contracts by day 180. It had lost four employees and 340 gold of worn equipment, and spent 618 on hires, 4,000 on upkeep and 1,080 on weekly wages. Its 6,290 earned gold plus the opening 260 left 852. Gold fell from 2,847 on day 90 to 852 on day 180.

The complete retained day-180 state was then continued without resetting money, injuries, roster, equipment, calendar, reputation or contract pools. Its 852 gold was recorded as carried capital, separately from new earnings. It completed one further native battle and reached critical insolvency on day 210 with 273 unpaid upkeep. The continuation passed 65 checks, including its separate cash ledger, with actual Unity exit 0. Evidence is Logs/economy38b-continuation-directory.txt and economy38b-continuation1.log. This demonstrates finite capital cushioning negative operations before insolvency for this case, without imposing an arbitrary 180-day bankruptcy requirement.

### Diagnostic corrections retained

Exploratory logs remain under Logs. An editor variable-shadowing compile error was corrected. A UI assertion initially included hidden labels awaiting deferred destruction and was corrected to examine the current labels. A batched-route policy initially skipped other accepted targets on its path. It was corrected to trigger any reached contract before rolling road encounters, matching CompanyCampaignView.Move. The affected preliminary runs were stopped or failed and are excluded from the final matrix. The 320-gold experiment was stopped after the opening-budget mismatch was established. These were diagnostic implementation corrections, with no production pathfinding change.

## Grouped regression and private build

TraitCurationAudit passed 963 assertions with actual Unity exit 0, including the existing trait/injury, gear, power, save and developer-isolation coverage. Evidence is Logs/economy38b-runtime-result.json and economy38b-regression1.log. Synthetic save deletion remained intercepted, and personal campaign saves were not accessed.

Private Windows build Builds/Warband-Review-20261105-9929d1bb/Win64/VoreWar.exe completed with actual Unity exit 0. Its 42 compiler warning kinds match the prior build after source-line normalization, with zero new warnings and zero C#/shader errors in final logs. Seven existing license/change/dependency notices are packaged with matching source/destination hashes in Logs/economy38b-notices.json. Root License.txt is unchanged. Source baselines, exact packet diff and current hashes are preserved in Logs/economy38b-before-directory.txt, economy38b-packet.diff and economy38b-source-manifest.json. The new editor-source .meta is identified explicitly in CHANGES. Rendered/interactive player acceptance remains pending. Suggested player review is one new campaign with hires and a payday, a level-up wage preview before confirming the stat choice, and save/load around an unpaid upkeep warning and repayment.

## Boundaries and pending work

The broad Help rewrite, narratives, post-battle events, display phase C, injury phase D and ending scoreboard remain deferred. No new time-based enemy escalation is introduced. Uncapped flat upkeep eventually exceeds finite maximum contract throughput, but that mathematical eventuality does not establish a desirable human-play survival horizon. Native AI is one tactical policy and its weaknesses are a limitation of economic forecasts.

Numerical selection, native/grouped checks and the private build are complete. The final policy matrix uses a new seed relative to the candidate runs, with the limited-seed caveat above. All existing attribution and license notices are retained. Actual License sections 4–5 have been reviewed for this batch. No outside assets, copied code or new dependencies are introduced. Existing provenance and public-distribution/Corresponding Source review gates remain open.
