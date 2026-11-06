# Auto-resolve save overhead — 2026-11-04

The player reported a 3v3 company battle taking about seven seconds to auto-resolve, followed by the inherited slow-simulation warning in a later battle. This is separate from balance tuning and the slice6 cleanup-stall finding.

## Cause and correction

`TurboMode` runs native combat synchronously, but each `EndTurn` called `SaveCompanyTurn`. That captured the complete tactical/campaign graph, serialized it, decoded it for validation, flushed a generation to disk, reread it for verification and reviewed saved generations for backup retention. Both sides' turn boundaries performed this work. A24-round battle wrote47 intermediate saves; a26-round battle wrote51.

`SaveCompanyTurn` now returns during turbo simulation. The existing battle recovery save remains available while the synchronous calculation runs. Ordinary played/watched turn saves still run; campaign return still saves the settled result. No alternate battle-resolution formula, changed rolls, AI rules, digestion acceleration, capacity change or new victory condition was introduced.

The legacy once-per-session slow-simulation modal was created after the simulation loop and could outlive its battle context. Slow runs still produce a diagnostic log warning, without adding that modal to the player interface.

## Focused evidence

The existing editor measurement runner now has an optional `-balanceTurbo` mode that calls the actual synchronous method. The slice6 matrix used a stepped driver and deliberately suppressed turn saves; it did not measure the overhead reported here. That earlier report remains a battle/economy measurement, not a user-click latency measurement. Current stepped runs use the production save guard rather than changing the session's finished flag.

Before run: `Logs/autoresolve-before3.log`, synthetic root `Logs/balance34f-4dfd21afdbc64a5ea587dc0065d10055`. Actual Unity exit0; both cases completed without case errors. After run: `Logs/autoresolve-after1.log`; exact paths/results in `Logs/autoresolve-comparison.json` and `Logs/autoresolve-after-directory.txt`. Actual Unity exit0; both cases completed without case errors.

| Fixture, seed101 | Rounds | Intermediate saves before / after | Measured simulation before / after |
| --- | ---: | ---: | ---: |
| Starter mixed roles,3v3 |24|47 /0|28.003s /0.225s|
| Starter melee,3v3 |26|51 /0|31.619s /0.217s|

Both final actor snapshots and final random-generator states match exactly before/after. Ordinary turn saving created a verified save in both after cases.

**Timing limitation:** diagnostic checks retain all generated save files and intercept deletion. Backup scanning consequently grows more expensive than a normal game, so the before timings are not estimates of normal standalone latency. These headless Unity2018 runs use16x16grass fixtures and exclude setup/result-save time. The absence of intermediate writes and the identical combat results are the relevant conclusions; the player's own hardware/build remains the graphical performance check.

Final save-handoff run `Logs/autoresolve-handoff1.log` exited0; root `Logs/balance34f-edd5a6bbe81f4b34af0b89d487c9166a`. Both cases completed, ordinary battle autosaves loaded through `LoadForResume`, and the actual campaign-view return wrote exactly one result generation that loaded as a settled Summary. Simulation times0.207/0.231s, result-return/save times0.135/0.110s. All18 compared combat metrics per case, final RNG, actor snapshots and canonical settled snapshots match the before run (`Logs/autoresolve-final-comparison.json`). Synchronous-mode transient downing counts are not instrumented and are excluded from that comparison; the stepped runner remains the source for downing counts.

The seed202 starter-melee outlier was rerun with the actual turbo loop: `Logs/autoresolve-stall1.log`, exact root in `Logs/autoresolve-stall-directory.txt`. It still reaches the approved200-round draw and completes in0.487s, with zero intermediate saves and a valid result save (0.108s). Actual Unity exit0 and no case error. This confirms the save-performance correction works even for long battles; it does **not** diagnose or fix that fixture's cleanup/stalemate cause.

No personal saves have been accessed. Initial restricted-license startup, an editor-only internal API access error and a synthetic-folder counting error are preserved in earlier logs; they are not passed comparisons. All synthetic files are retained. No new test framework was added.

Fresh private player build `Builds/Warband-Review-20261104-300bc8bb/Win64/VoreWar.exe`: actual Unity exit0, no C#/shader errors,41 inherited compiler-warning kinds and zero new warning messages. Seven existing license/attribution notices packaged and hash-verified. No graphical playtest claimed. Player check: auto-resolve two consecutive small battles, confirm no stale warning, and load the resulting campaign save. Crash recovery during the synchronous calculation uses the existing pre-simulation turn save.

## Licensing and scope

Only `Assets/Scripts/Modes/TacticalMode.cs` and the existing fork editor source `Assets/Editor/CompanyBalanceAudit.cs` changed in this correction. Dated notices and CHANGES identify the modifications; License4–5 reviewed and prior notices preserved. No outside code, assets or dependencies were introduced. Existing provenance/public-distribution/Corresponding Source gates remain open. No files were deleted or moved. No narrative work, balance changes or slice7 implementation.
