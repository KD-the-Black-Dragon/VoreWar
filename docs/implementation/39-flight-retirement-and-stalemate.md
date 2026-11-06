# Flight retirement and ranged stalemate follow-up

Implemented on 2026-11-05 following the user's request to remove Flying from every species.

## Observed root cause

The preserved ranged-none seed-1008 battle had three standing, full-health Avian archers with two AP, empty stomachs and sufficient capacity. Two downed enemies remained. Action readiness, statuses, retreat and containment did not prevent the archers from acting. StandardTacticalAI recognized the prey and repeatedly chose RunPred routes.

The round-50 trace shows actual backtracking. One archer moved from (8,7) to (7,8), discarded its route after spending its first AP, then planned a different flight route and moved back to (8,7) with its last AP. The others similarly returned to their starting squares. End-of-round snapshots therefore appeared stationary. Static terrain evidence shows (7,8) is nonwalkable while its planned landing at (8,9) is walkable.

Flight pathfinding permits transit through nonwalkable terrain and checks landing using the currently available movement. Ranged early replanning and subsequent-turn flight planning can cycle between partial routes without completing a feasible landing progression toward prey. This particular combination of terrain, two AP and an entirely flying ranged company exposed the loop. Ground routes remain on walkable tiles, and mixed forces can complete the cleanup through other units. That explains the rarity across the larger calibration matrix.

A diagnostic counterfactual retained the discarded route after round 50. It still reached the 200-round draw after 450 interventions. The issue therefore includes subsequent-turn flight replanning, beyond the first route cancellation. The initial diagnostic OpenTile label also included the mover itself in occupancy checks. It was corrected to occupiedOpen, with static terrain recorded independently. These unsuccessful/corrected diagnostics remain preserved and are not counted as successful fixes.

## Implemented retirement

Flight is globally retired through the existing trait policy. Existing saved or customized units cannot activate it, Help and active trait selectors omit it, and its power estimate is removed. Its numeric enum identity remains reserved for saved-data compatibility.

All thirteen baseline assignments were removed from Bees, Demibats, Avians, Draconians, Wyvern, WyvernMatron, Bats, Dragons, Dragonfly, Gryphons, EasternDragon, Dratopyr and Otachi. Wings and artwork are retained. RangedVore's tooltip no longer claims a Flight exception.

No new production pathfinding behavior was introduced. Retirement makes these units use the existing ground pathfinding rules.

## Native evidence

- Exact original initial-company.bin replay, preserving both armies, weapons, stats and battle seed, completed in 31 rounds after retirement. The original reached the 200-round stalemate. Actual Unity exit 0 and 446 focused checks passed. Report pointer Logs/flight39-replay-directory.txt.
- Related replay matrix completed all 24 ranged none/heavy/light fixtures over seeds 1001–1008, each in 22–33 rounds, with no case errors. All 24 recorded identity/species/level/stats/gear/HP/injury signatures match their discovery inputs. The original discovery JSON predates weapon-value reporting, so matching those JSON weapon fields is unavailable. The replay instead decodes the complete original binary state. Actual Unity exit 0. Report pointer Logs/flight39-related-directory.txt.
- First grouped audit failed while examining a pre-existing uninitialized inactive MatronsMinions metadata field. The audit was corrected to inspect every defined RaceTraits prototype, with separate non-null requirements for all curated species. Gameplay metadata was not changed for this unrelated inactive entry. The failed log remains Logs/flight39-regression1.log.

The corrected grouped TraitCurationAudit passed 963 assertions with actual Unity exit 0. Evidence is Logs/flight39-runtime-result.json and flight39-regression2.log.

Fresh private player Builds/Warband-Review-20261105-e4dcd957/Win64/VoreWar.exe built with actual Unity exit 0. All 42 compiler warning kinds match the prior build after source line-number normalization, with zero new warnings and zero C#/shader errors in final logs. Seven established license/change/dependency notices are packaged and hash-verified in Logs/flight39-notices.json.

These batch checks do not establish rendered/interactive player acceptance. Player review can confirm Flight is absent from Avian traits and run a normal Single Battle or company battle, then save/resume it.

## Source and licensing review

Six source paths are identified in the dated CHANGES entry and carry dated modification notices. Existing legal and attribution notices remain. Actual License sections 4–5 were reviewed. Existing project and Unity APIs are used, without outside assets, copied code or new dependencies. Before snapshots, exact packet diff and hashes are retained under Logs/flight39-before-directory.txt, flight39-packet.diff and flight39-source-manifest.json. Synthetic fixtures remain retained, with deletion intercepted. Personal saves were not accessed.

Public distribution, inherited provenance and Corresponding Source obligations remain in the release-readiness record. This private review build does not close those gates. Economy/upkeep Phase B remains subsequent work.
