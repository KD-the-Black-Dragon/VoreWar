# Phase D injury validation and integrated handoff

2026-11-05. The user accepted Phase C and authorized Phase D of spec 38. The approved injury implementation passed the focused review. Changes in this packet add editor-side verification and documentation. Gameplay, tuning, player-facing wording and artwork were preserved.

## Completed native checks

The focused audit passed **671 assertions**, with actual Unity exit 0. Fifteen seeded frequency samples cover **170,000 trials**. The independent economy-boundary run passed **64 checks**, and the grouped integration run passed **963 checks**, both with actual exit 0.

| Area | Coverage |
| --- | --- |
| Temporary body injuries | All eight parts × Bruised/Battered/Mangled, exact names, effects and current 5/14/28-day durations |
| Combat effects | Four eye accuracies, hand melee/ranged/spell damage, four stat penalties, chest ceilings, fixed species AP and the one-AP minimum |
| Old injuries | All eight names and I/II/III effects, one family per part, upgrades capped at III, four additional complication families |
| Accumulated damage | Exact values above/at/below 75%, 50% and 25%, all four stat penalties, unchanged normal maximum HP |
| Healing | Separate lasting/temporary HP, immediate attrition removal/return, lasting healing crossing thresholds, explicit HP traits versus modified STR |
| Chest limits | Additive temporary/old ceilings, existing HP retained, normal HP denominator retained, one-HP floor and post-combat baseline |
| Injury occurrence | Below-10% lasting loss excluded, exact threshold/severity transitions, temporary-only loss excluded, summons excluded |
| Actual combat paths | A forced native weapon graze records its final result without body injury, while a native damaging spell can cause one |
| Armor | All eight component mappings, unrelated parts unprotected, chance boundaries and a seeded helmet comparison |
| Recovery | Exact 0/33/66% thresholds, treated 0/17/33%, occurrence roll retained, treatment never rerolls, last-day treatment and repeat-charge rejection |
| Daily complications | Exact 5%/10% boundaries, no roll at healthy/light condition, all eleven eligible families, knees excluded, seeded incidence/selection |
| Leader immunity | Both leader type and immunity flag prevent recovery, direct and daily permanent complications |
| Calendar and company | One injury-recovery day during ordinary/field/town recovery, HP rates 10/20/30%, minimum recovery, injured/reserve wages, healthy active/reserve sparring |
| Persistence | Real named battle save/resume, battle autosave reload, map autosave reload, exact injury ID/tier/treatment/roll and temporary HP preservation |
| Settlement | Temporary statuses/HP cleared, genuine injuries retained, reserve HP/injury calendar untouched, digested member and worn gear removed |
| Single Battle | Fresh characters, temporary Petrify expiry, battle-end effect cleanup, no inherited people/injuries/statuses in the next battle, no company save writes |
| Legacy and invalid data | Stable ID-derived missing rolls, old duplicate/tier normalization, all old-part identities serialized, invalid recovery rolls rejected |

The 15% post-combat HP baseline remains the existing provisional value. Specific injuries persist when HP has recovered. Rest improves general HP recovery while body injuries advance by one calendar day. Treatment removes two recovery days once, leaves at least one day, and charges ten gold per injured member. These checks confirm the later recovery changes from specs 35–36, superseding the historical 3/5/8-day values in the original injury implementation record.

## Probability evidence

Controlled rolls check both sides of each exact probability threshold. Seeded sampling is a separate frequency sanity check. Each nonzero sample uses a predeclared tolerance of six binomial standard deviations plus six occurrences. Zero-probability recovery must produce exactly zero old injuries. The approved probabilities were retained.

| Rule | Seed | Trials | Expected | Observed |
| --- | --- | ---: | ---: | ---: |
| Bruised recovery | 38110 | 10,000 | 0% | 0% |
| Treated Bruised recovery | 38111 | 10,000 | 0% | 0% |
| Battered recovery | 38112 | 10,000 | 33% | 33.30% |
| Treated Battered recovery | 38113 | 10,000 | 17% | 16.77% |
| Mangled recovery | 38114 | 10,000 | 66% | 67.11% |
| Treated Mangled recovery | 38115 | 10,000 | 33% | 32.64% |
| Moderate daily complication | 38122 | 20,000 | 5% | 4.95% |
| Severe daily complication | 38123 | 20,000 | 10% | 10.24% |
| Hit removing 10% normal HP | 38230 | 10,000 | 10% | 9.82% |
| Hit removing 25% normal HP | 38275 | 10,000 | 25% | 24.89% |
| Hit removing 50% normal HP | 38350 | 10,000 | 50% | 49.92% |
| Hit removing 90% normal HP | 38470 | 10,000 | 90% | 89.92% |
| Hit removing 100% normal HP | 38500 | 10,000 | 90% cap | 89.62% |
| Half-HP hit with helmet | 38501 | 10,000 | 43.75% overall | 44.27% |
| Animal half-HP hit | 38502 | 10,000 | 50% | 50.03% |

The helmet calculation averages two protected and six unprotected parts. All eleven daily complication families appeared and passed conditional frequency checks. Animal injuries remained confined to Head/Chest. Frequency samples verify implementation consistency, while human campaign enjoyment and long-term balance remain separate observations.

## Integration and save evidence

The injury fixture creates distinct survivor, casualty and reserve roles with explicitly supplied diagnostic funds. This setup is independent of the tested production starting budget. Damage, rolls and casualty flags are deliberately controlled. Native combat, save/resume, settlement and generation methods perform the resulting operations. These are reproducible scenarios, rather than a record of a manually played battle.

The real Save / Load path resumes the wounded participant with 135 lasting + 105 temporary HP, a treated Battered Eyes injury with stored roll 0.2 and an old arm injury. Settlement removes temporary HP/statuses, retains the injuries, and leaves reserve health/calendar untouched. Map and battle autosaves are reloaded and compared. Recovery before and after reload gives the same outcome for the stored treated roll. A separate controlled digested casualty exercises actual roster/equipment removal during settlement.

The focused economy checks reuse the existing exact boundaries for upkeep-first bills, partial allocation, grace-week payment, insolvency, interrupted saves, personal debts, wages, level-up previews, existing-save enrollment and quoted finances. It runs zero long campaign histories. The grouped suite also covers combat/armor/previews, traits, save integrity, world data and password-gated Battle Lab isolation. Synthetic deletion remains suppressed, including Ironman checks.

## Reproduction and retained artifacts

Use Unity 2018.3.12f1 in a separate batch editor with the regular editor closed. All diagnostics redirect saves into retained project Logs directories.

Fixtures assume the curated sapient pool is enabled, including Lizards for the leader. Global preferences are read during normal startup and are preserved.

- `InjuryValidationAudit.Run` with `-batchmode -nographics` runs the focused matrix. It creates a unique directory and updates `Logs/injury38d-latest.txt`.
- `CompanyBalanceAudit.Run` with `-balanceEconomy -balanceBoundariesOnly` runs the existing focused financial checks.
- `TraitCurationAudit.Run` with `-injuryReview` runs the grouped suite and preserves its result separately.

Final focused report is `Logs/injury38d-80f9cbb537304e1a8417db1a57fa3f02/report.json`, with `injured-battle.bin`, `injured-map.bin` and native save generations retained alongside it. The log and actual exit are `Logs/injury38d-final3.log` and `Logs/injury38d-final3-exit.txt`. The focused economy directory is recorded by `Logs/injury38d-economy-directory.txt`. Grouped results are in `Logs/injury38d-runtime-result.json` and `Logs/injury38d-regression1.log`.

Pilot 1 failed compilation on editor access to internal Unit members. The diagnostic was corrected to the existing reflection pattern. Pilot 2 passed 554 checks, pilot 3 passed 578, final 1 passed 650 and final 2 passed 651. The last run adds explicit AP checks for all four curated species and exact armor-protection checks. Earlier evidence is retained and excluded from the final counts.

## Handoff and limits

The accepted player remains `Builds/Warband-Review-20261105-512e67a8/Win64/VoreWar.exe`. All changes in this phase are under Assets/Editor and documentation. Native runs compile the new editor verification, and the existing accepted player is retained.

Headless scenarios establish numerical, state and persistence behavior. They do not establish rendered injury popups, physical input or subjective difficulty. Phase C's accepted graphical review remains the relevant presentation evidence. Optional later play feedback can assess how often the approved injuries occur and how they feel over a campaign. The broader Help rewrite, narrative work, ending scoreboard and time-based enemy escalation remain deferred.

The pre-existing developer race-settings startup warning recorded in Phase C remains outside this injury review. Its developer data was preserved.

## Source and licensing review

The new fork editor source/meta and two modified editor sources are listed in CHANGES with dated conversion notices. Earlier legal notices and attribution are preserved. The actual License sections 4–5 were reviewed. Existing Unity/.NET/Odin APIs are reused, with no outside code, assets or dependencies introduced. Source snapshots, hashes and the final integrity record are retained under `Logs/injury38d-*`.

The existing private player's license/notice package is retained for its unchanged game code. No publishing, agent file deletion/moving or personal company save access occurred. Existing public-distribution asset-provenance and Corresponding Source obligations remain separate open gates.
