# 38A — Combat calibration

Implementation record, 2026-11-05. Phase A is implemented and native validation and the private Windows build passed. Player review is pending. The user requested detailed interruption checkpoints. The creative direction includes uncapped flat weekly upkeep and eventual defeat as expenses and enemy challenge outpace company growth. The ending scoreboard is deferred. Numerical upkeep and new enemy escalation rules belong to later work.

## Baseline and method

Existing CompanyBalanceAudit now supports configurable calibration seeds, separate exact calculation output, armor-only alternatives, and optional stronger-hit/accuracy diagnostic fixtures. Each case preserves an initial campaign, native battle log and settled campaign under a unique project Logs directory. The runner processes actual AI, opportunity attacks, attacks/spells, vore/digestion and canonical battle settlement. All files are synthetic, deletion is intercepted, and evidence is retained.

The primary contexts cover starter, level-5 mixed, level-10 mixed, five-member companies, all-melee/ranged/magic companies, a wounded company and a strong leader with inexperienced staff. Each compares empty armor slots with full heavy and full light alternatives. Two additional fixtures compare individual torso pieces. Weapons stay fixed within each context. Tier-II torso alternatives cost 230/240 and full tier-I sets cost 160/165, providing closely priced comparisons. Other tiers retain their actual prices for later economic calibration.

All battles use a 16×16 grass field. This controls terrain during comparison. Both sides use the existing AI. Seed blocks and fixed-roster comparisons must be distinguished from independent human play or arbitrary battlefield layouts. Calibration omits the older runner's recovery and economy branches. A driver error fails the run while preserving completed cases.

## Completed discovery evidence

- Exact enumeration passed 392 comparisons against production ExpectedAfterArmor arithmetic across damage, AC, barriers and temporary HP. It also records 42 hit/graze/critical probability combinations, including KeenEye and the 51% boundary. Current native configuration uses 0.30 graze and 1.50 critical damage multipliers.
- Pilot2 completed three starter alternatives. Its waiting wrapper was interrupted after the editor had exited, so its shell exit is unverified. Pilot1 failed to access activation in the restricted environment and was stopped. The subsequent licensed runs use explicit process handles and retain actual exit codes.
- Discovery1 completed all 232 cases across seeds 1001–1008 with actual Unity exit 0. Its root is recorded in Logs/calibration38-discovery-directory.txt. The exact calculations and native cases completed without driver errors.
- Candidate1 completed all 232 cases with actual Unity exit 0. Its root is recorded in Logs/calibration38-candidate-directory.txt. It tested full light-armor totals of 16/28/40 percentage points in memory against the then-current 12/20/30. Finish restored definitions. This discovery batch preceded the production change.
- Roster comparison identified six changed generated forces, all elite/five-member branches at seed 1008. These are excluded from controlled discovery comparisons. The remaining 226 pairs match their recorded race, level, stats and loadout. Later candidate evaluation must use the saved baseline enemy rosters to prevent generation changes from confounding armor comparisons.
- Logs/calibration38-discovery-comparison.json records matching pairs and exclusions. Logs/calibration38-theory.json records clean weapon damage comparisons across tiers, accuracy and damage sizes. Theoretical weapon tables exclude flat additions/statuses, which remain part of actual native battles.

The baseline has three 200-round stalemates in the all-ranged seed-1008 branches. These are listed separately from direct clears and follow the approved stalemate settlement. A focused final trace reproduced the issue and is described below.

## Initial numerical interpretation

Uniform AC 5 reduces a four-damage hit to 2 on average, and a twenty-damage hit to 17.5. AC 10 reduces an eight-damage hit to about 3.55. AC 15 reduces a forty-damage hit to 32.5. These include the minimum-damage floor before barriers. Evasion depends on hit odds and the graze threshold. Guaranteed damaging spells still benefit from armor deflection, while their accuracy bypasses evasion.

On matching discovery seeds, mixed level-5 light armor reduces mean logged incoming direct damage from 31.75 to 22.5 with the candidate. Five-member light-armored companies reduce it from about 155.43 to 118.57 across seven matching seeds. Wounded light-armored companies change from 5/8 direct clears and seven permanent losses to 6/8 and six losses. These are preliminary paired measurements with substantial casualty uncertainty. Logged direct damage excludes digestion and represents damage before ordinary barrier absorption.

These discovery measurements informed the candidate. The independent evaluation below determined the final selection.

## Independent evaluation checkpoint

The current-value batch completed all 304 cases across independent seeds 2001–2008 with actual Unity exit 0. Each seed includes the earlier contexts plus three-member rating-8, high-weapon-damage and high-weapon-accuracy fixtures. The high-hit fixture adds 12 base weapon damage, and the accuracy fixture adds 0.75 weapon accuracy rating to the generated enemies. Their actual modified equipment and power are recorded. These deliberate stress conditions are separate from ordinary generated contract strength.

The frozen candidate evaluation reads the exact enemy characters from those initial campaign saves. Existing stress modifications are retained once. Inputs are restricted to preserved synthetic project Logs data. The first attempt failed to compile direct access to internal Unit fields and was corrected to the existing reflection helpers. The next attempt correctly refused the reference due to a diagnostic error-reader bug, since JsonUtility represents null strings as empty strings. That reader now checks for nonempty errors. Both failed attempts and exit codes remain preserved.

The third attempt completed all 304 cases with actual Unity exit 0 and matched all 304 reference rosters, including recorded weapon values. It passed 392 exact mitigation comparisons and 12 native roll-frequency checks from four 10,000-roll samples. Accuracy 50% produced zero grazes. Accuracy 51.01% produced 2,558 grazes out of 10,000 against an expected fraction about 24.99%. Criticals occur on initial hits. The checks restore the original RNG object and target modifier before battle simulation.

The four principal batches comprise 1,072 native battle runs across 16 seed blocks and multiple conditions. Additional pilot and final diagnostic runs are separate. Logs/calibration38-evaluation-comparison.json records individual fixtures and paired seed-block bootstrap intervals with 20,000 resamples using seed 38038. Individual-fixture clear-rate intervals use Wilson 95%. Grouped rates are descriptive because contexts within one seed share inputs.

| Independent light-armored fixtures | Previous | Candidate |
| --- | ---: | ---: |
| Regular fixtures | 80 | 80 |
| Mean logged incoming direct damage | 63.20 | 57.18 |
| Direct clears | 77 | 76 |
| Permanent mercenary losses | 27 | 26 |
| Stress fixtures | 24 | 24 |
| Mean logged stress direct damage | 122.21 | 106.88 |
| Stress direct clears | 16 | 18 |
| Stress permanent mercenary losses | 22 | 18 |

For regular fixtures, paired mean direct damage changes by -6.025 with a seed-block 95% interval of approximately -10.79 to -1.50. Casualty differences have intervals spanning zero. The stress-damage interval also spans zero. The selected increase has measured mitigation support, while its survival advantage remains uncertain. Heavy armor retains its small-hit advantage. In the baseline high-hit fixtures heavy/light means are 145.0/143.38, whereas high-accuracy means are 27.0/52.5. The candidate high-hit light mean is 95.0 across eight seeds, with substantial variation.

## Selected values and power correction

Full light sets now provide 16%, 28% and 40% evasion. The per-piece values are flat percentage points deducted from applicable incoming accuracy.

| Tier | Helmet | Torso | Legs | Arms | Total |
| --- | ---: | ---: | ---: | ---: | ---: |
| I | 3 | 4 | 6 | 3 | 16 |
| II | 6 | 8 | 8 | 6 | 28 |
| III | 8 | 10 | 14 | 8 | 40 |

These catalog bonuses also apply to existing worn pieces through stable definition IDs. Existing serialized items retain their stored weapon/stat values. Full heavy armor still provides AC 5/10/15. Numerical price and wage recommendations follow in Phase B.

Healthy weapon power now includes the expected damage of rescued misses and outgoing critical/graze damage multipliers. KeenEye's guarantee is represented through that probability model, replacing its flat three-point estimate. The pure helper uses the existing estimated reference accuracy, initial-hit critical probability and graze threshold. The rest of the scorer remains a heuristic for target-independent healthy capability. It cannot predict exact matchup win rates, arbitrary status combinations or optimal human tactics. Stored contracts and enemy identities remain preserved. Newly generated forces use the revised score, which passed the existing fixed-band and recruit-ceiling checks.

## Final validation and build

- Final frozen native trace passed 434 exact mitigation/outcome-helper comparisons plus 12 native frequency checks, giving 446 checks with actual Unity exit 0. Its report pointer is Logs/calibration38-final-trace-directory.txt.
- Grouped TraitCurationAudit passed 893 checks with actual Unity exit 0. Evidence is Logs/calibration38-runtime-result.json and calibration38-regression1.log. It covers traits, injuries, evasion, damage previews, equipment serialization, developer isolation, starter recruitment and contract power bands through rating 20. It writes a separate result file, preserving earlier road-review evidence.
- The fresh private Windows build passed with actual Unity exit 0. Open Builds/Warband-Review-20261105-15117801/Win64/VoreWar.exe with its full neighboring folder. Compiler review found zero new warnings relative to the previous build and zero C#/shader errors in the final logs. Logs/calibration38-compiler-review.json records the comparison.
- Source baselines, final hashes and the exact packet diff are preserved in Logs/calibration38-before-directory.txt, calibration38-source-manifest.json and calibration38-packet.diff. The driver records actual light-armor totals for future batches. Historical candidate reruns require the corresponding source/content settings and preserved fixtures.
- Logs/calibration38-result.json reconciles all four completed batches, the matched evaluation pairs, final checks, source hashes, compiler review, build and seven hash-verified notices. Logs/calibration38-notices.json records the notice copies. The root license hash matches the pre-phase value.

The ranged-1008 trace originally reached the existing 200-round stalemate. Follow-up traces established repeated backtracking by two-AP flying archers on partial flight routes. The user requested global Flight retirement. The exact preserved battle then completed in 31 rounds, and all 24 related ranged replays completed. See [Flight retirement and root-cause evidence](39-flight-retirement-and-stalemate.md). This follow-up is separately recorded from the calibration results above.

Player review can focus on inspecting the new light-armor bonuses in the merchant or Battle Lab, then playing an ordinary battle and resuming its save. Rendered/interactive playtesting remains pending. Injury-focused player testing and the wider display review retain their later phases.

## Source notices and provenance

Modified sources are Assets/Scripts/Core/CompanyEquipment.cs, Assets/Scripts/Core/CompanyPower.cs, Assets/Editor/CompanyBalanceAudit.cs and Assets/Editor/TraitCurationAudit.cs. Each has a dated conversion notice and an exact-path CHANGES entry. Actual License sections 4–5 were reviewed, existing attribution/legal notices remain, and the new code uses existing project/Unity APIs. The build carries the seven established license/change/dependency notices, with source/destination hashes recorded at handoff. Synthetic files are retained with deletion interception. Existing asset provenance and public-distribution/Corresponding Source obligations remain in the release-readiness record.
