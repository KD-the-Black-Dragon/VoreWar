# Spec34 slice6 — Native balance measurements

2026-11-04. Implemented the editor-only `CompanyBalanceAudit` runner and completed51 native battles (17 fixtures ×3 seeds),150 recovery counterfactuals and24 economy comparisons. This is a small diagnostic sample, not a win-rate estimate or final balance certification. **No gameplay mechanics, prices, AI, injury odds or scoring weights were changed.** The approved slice5 standalone remains current; this slice needs no new player build.

## Evidence and reproducibility

- Full run: `Logs/balance34f-533e071f082f41b991f52c0cb7112a11/report.json`.
- Derived timing/canonical settlement: `Logs/balance34f-533e071f082f41b991f52c0cb7112a11/enriched-report.json`.
- Each case folder retains `initial-company.bin`, `battle-log.bin` and `settled-company.bin`; normal initial synthetic autosaves are also retained.257 files/~29.4MiB after enrichment. No personal saves or actual deletion; the existing diagnostic deletion interception is active.
- Pilot: two cases in `Logs/balance34f-b76c3a85fe6a4747b3e8fc1672d825d5/report.json`.
- Editor logs: `Logs/balance34f-pilot1.log`, `Logs/balance34f-matrix1.log`, `Logs/balance34f-analysis1.log`, `Logs/balance34f-analysis2.log`. All four processes exited0; all51 full-run cases completed without driver errors/timeouts. The final analysis compiled the path guards/team-label enrichment and removed an unused diagnostic local. No new compiler warning messages or C#/shader errors. The two pilot cases matched the full run on all20 compared battle metrics (duration, outcomes, fates, rewards and power).
- Seeds101/202/303; native battle seed = fixture seed +9000, enemy contract generator seed = fixture seed +7000. Paired contract fixtures share enemy generation and battlefield seeds. Source runner, initial binary and report retain the actual loadout/health inputs. Exact reruns require matching species/content settings and source; snapshot `id` is the unit's display name, while canonical character IDs remain in the binary fixture.

Run in a separate Unity2018 editor process with `-batchmode -nographics -projectPath <project> -executeMethod CompanyBalanceAudit.Run`. Do not add `-quit`: the runner exits after its Play-mode work settles. `-balanceCount 2` runs the pilot battle count plus economy checks. `-balanceAnalyze <report.json>` derives timing/settled snapshots from preserved outputs without rerunning battles. Inputs stay under project Logs; the report directory and per-case folders identify preserved synthetic data. `Logs/balance34f-latest.txt` points to the latest run. Repeated runs create new directories.

## Method and limits

The actual company adapter, contract/road force generation, combat AI, opportunity attacks, damage/vore/digestion, victory/stalemate settlement, revival, wages, daily recovery, treatment and debt/departure rules execute. Both sides use the existing AI on16×16 grass maps. The driver follows native turbo stepping with a frame time budget,30,000-action/45-second technical cap and the game's200-round stalemate rule. Technical failures would be reported separately, never counted as defeats or draws.

Per-turn autosaves are suppressed only around native EndTurn using the session's existing `finished` guard, with turbo preventing its nested victory callback. The guard is restored immediately; native victory and settlement run explicitly with the real company session installed. Initial autosaves/raw fixtures are retained. The game manager/view update loops and headless renderers are paused, keeping input/return screens from racing the driver. Production code is untouched.

Role comparisons combine species and role: Gnoll melee, Avian ranged, Lizard magic. Premium/levelled fixtures receive explicit synthetic capital; this tests capability, not whether a real company could afford that loadout. New contracts use nominal rating×60 rewards rather than random payout margins. Fixed five/seven-slug forces are labelled stress fixtures; ordinary road fixtures use production event generation.

Camp/town/treatment recovery branches clone the same settled company and RNG seed. Town relocation is free **in this counterfactual only**; travel costs/days are excluded. Readiness means remaining members reach their healing ceiling with no temporary injuries. Recovery pays affordable full individual debts in roster order; departure counts prevent silently treating lost staff as healed. The economy comparison explicitly contrasts that payment policy against ignoring debt. This is a benchmark policy, not a new game feature.

Raw `final` actor snapshots precede detached campaign settlement and can show living downed units at0HP. `settled` snapshots come from the canonical post-result save, including recovery-floor healing/revival. Negative HP on digested actors is a retained terminal state, not a surviving roster entry. No graphical playtest is inferred from these measurements.

## Battle findings

All figures below average3 seeds. Direct clears exclude the one stalemate, even though the approved stalemate/flee rule correctly paid its contract. Losses count permanently lost non-leader mercenaries. Camp recovery averages exclude the wiped-out company.

| Fixture | Direct clears | Mean rounds | Mean incoming hit damage | Permanent mercenary losses | Mean camp days |
| --- | ---: | ---: | ---: | ---: | ---: |
| Starter mixed, rating1 | 3/3 | 29.7 | 22.7 | 0 | 4.3 |
| Starter melee, rating1 | 2/3 +1 draw | 87.7 | 50.3 | 2 | 4.0 |
| Starter ranged, rating1 | 3/3 | 32.7 | 23.0 | 0 | 6.7 |
| Starter magic, rating1 | 3/3 | 29.0 | 35.0 | 1 | 3.3 |
| Starter mixed, rating2 | 3/3 | 33.0 | 54.7 | 0 | 4.7 |
| Healthy level3 mixed, rating2 | 3/3 | 29.7 | 29.3 | 0 | 2.0 |
| 45% HP +Battered Eyes, level3 mixed, rating2 | 2/3 | 35.3 | 43.7 | 3 | 5.0 |
| Level5 heavy I, rating4 | 3/3 | 28.7 | 41.7 | 0 | 1.7 |
| Level5 heavy II, rating4 | 3/3 | 25.7 | 20.0 | 0 | 0.7 |
| Level5 heavy III, rating4 | 3/3 | 25.7 | 15.7 | 0 | 0.0 |
| Level5 evasive II, rating4 | 3/3 | 26.7 | 50.3 | 0 | 2.7 |
| Native road, starter mixed | 3/3 | 25.0 | 16.0 | 0 | 0.7 |
| Native road, balanced level3 | 3/3 | 26.7 | 14.0 | 0 | 0.3 |
| Native road, level8 leader +level1 mercs | 3/3 | 27.7 | 25.3 | 0 | 3.3 |
| Native road, level3 at60% HP | 3/3 | 25.3 | 15.7 | 0 | 5.0 |
| Fixed five slugs | 3/3 | 29.3 | 21.7 | 0 | 1.7 |
| Fixed seven slugs | 3/3 | 35.7 | 50.7 | 0 | 4.3 |

Incoming hit damage is logged enemy weapon/spell damage after mitigation but before barrier absorption; it excludes digestion. It is not total HP lost across every mechanic. These fixtures produced8 distinct company downings,7 company digestions (including one leader),6 permanent mercenary losses and one full company loss. Characters can be swallowed/digested before reaching0HP, so downing is not a proxy for survival.

### Outliers and interpretation

1. **Long quiet tails deserve an AI/cleanup investigation first.** Among the50 non-stalemates, median time after the final logged hit/miss/spell/swallow/escape was15 rounds. This tail can include movement, support actions and digestion; it is not proof of inactivity. One all-melee starter case (seed202) reached200 rounds, with its last tracked action at21:179 later rounds. Two downed company survivors and three live enemies ultimately fled under the existing stalemate rule; one mercenary had already been digested. The contract paid60 as required by the user's flee-on-draw rule. Inspect capacity/pathfinding/cleanup decisions before proposing damage or digestion tuning. Do not "fix" the approved draw reward rule or assume units were left behind.
2. **Wounds matter.** The45%-HP/injured fixture lost three mercenaries across its three runs, including one company wipe; its healthy match lost none. Sample size and coupled HP+injury prevent assigning the effect to one penalty independently.
3. **The reduced slug pressure is supported by this small sample.** Seven fixed slugs caused50.7 versus21.7 incoming damage for five, and longer recovery. Native balanced-level3 encounters generated five slugs, strong-leader/weak-staff six, and wounded-level3 four. The strong-leader company had more incoming damage and recovery than the balanced company. This is evidence to retain current caution, not a calibrated new encounter coefficient.
4. **Heavy armor performed better than the evasive set here.** Tier-II heavy reduced incoming damage to20 versus50.3 for evasive II and shortened camp recovery. This small set of enemies/AI/maps does not establish global superiority; high-damage opponents, stronger accuracy traits and more seeds are needed before changing evasion or AC. Neither armor behavior nor values were retuned.

## Recovery and treatment

Across50 surviving companies, mean readiness time was2.92 camp days versus2.08 town days. Twelve paid treatments cost120 total. Treated town recovery had the same mean2.08days: in these cases HP recovery, rather than temporary-injury days, remained the readiness bottleneck. Old complications recorded during these branches were5 in camp,1 in town and0 with town treatment. These are paired counterfactuals, not150 independent campaigns or a reliable probability estimate.

Do not make treatment an instant HP heal merely because time-to-full-readiness did not improve here; reduced lasting-complication risk is already a benefit and the approved design limits treatment to temporary injuries. Camp recovery paid403 wages across branches, town recovery none because all town recoveries finished before the first payday. One camp branch still owed21; no recovery branch lost staff to nonpayment. Travel to town was excluded, so the town result is optimistic.

For starter mixed rating1, mean contract gold after revival/recovery wages was46.7 with camp rest and60 with town rest before any travel or equipment cost. Starter ranged averaged32.3 after camp wages. These are conditional successful-run ledgers, not daily-profit forecasts or replacement-cost accounting.

## Investment and income pressure

The matched level5 companies wore gear worth740 /2510 /9730 for heavy tiers I/II/III; evasive II cost2750. At nominal rating4 payouts of240, those equal3.1 /10.5 /40.5 gross rewards for the whole company's gear, before wages. Some fixtures retained unequipped initial gear in inventory, so their setup spending is higher than their worn value. Premium funding was synthetic and is not an income claim. The steep high-tier price curve therefore needs an intended outfitting-time target before price recommendations; "serious investment" is the approved direction.

The28-day income exercise assumes guaranteed60-gold payouts every4/7/10days or no payouts, without simulating the corresponding travel/battles/injuries. Actual wage/debt/departure systems process every day. Starter wages total40–42 per week; initial hire costs leave28–30 gold in the mixed company.

| Income cadence | Mean income | Mean paid wages, attentive policy | Mean final debt | Departures across3 runs |
| --- | ---: | ---: | ---: | ---: |
| Every4days | 420 | 164 | 0 | 0 |
| Every7days | 240 | 164 | 0 | 0 |
| Every10days | 120 | 144.3 | 19.7 | 0 |
| None | 0 | 21.3 | 82 | 6 |

Ignoring debt with ten-day income lost both mercenaries in all three runs despite accumulated cash: this reflects deliberately not paying, not an automatic insolvency result. Paying affordable full debts avoided those departures but left some debt at the28-day cutoff. A longer window includes the next payout, so do not infer inevitable long-run insolvency from this boundary. Four/seven-day income is an optimistic upper bound: actual failed contracts, rest and travel can worsen it.

## Recommended next decisions

1. Investigate the seed202 cleanup/stalemate outlier and long action tails; retain the approved vore/digestion/fate rules while diagnosing.
2. Expand paired AC/evasion and fragile-company cases across more seeds/opponents before any balance adjustment. Three seeds do not justify trait/species nerfs.
3. Define the desired time to buy one good item versus fully outfit three/five mercenaries, then extend multi-week travel/injury/replacement ledgers. The current report exposes prices and costs rather than silently choosing that target.

These are follow-up recommendations, not implemented tuning. Slice7 geography/road work is separate and still needs its own dimensions/town-spacing decisions. Help/narrative/post-battle events remain deferred.

## GPL and scope

Added fork source `Assets/Editor/CompanyBalanceAudit.cs` and its `.meta`, with dated notice and exact-path CHANGES record. Existing production source/assets/dependencies and attribution remain untouched by this slice. License4–5 reviewed; no outside code/assets/dependencies introduced, no files moved/deleted, no public distribution. Existing distribution/provenance/Corresponding Source obligations remain open.
