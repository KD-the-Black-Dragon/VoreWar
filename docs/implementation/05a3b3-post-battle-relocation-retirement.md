# 05A3B3 — post-battle relocation retirement

**Date:** 2026-10-31. User accepted 05A3B2. This packet retires old campaign relocation after battle without implementing the future company retreat policy.

## Source boundary

- `Assets/Scripts/Modes/TacticalMode.cs`: remove the two winning-side return-to-army/flee-to-town dialogs, FledReturn field/reset, and the conditions tied to that choice. Preserve the previous Default behavior: the winning side's collected surviving retreaters rejoin its battle force (or standalone village recruitables). Keep null guards. Remove three ProcessTravelingUnits dispatches while retaining dead-entry pruning and all other participant assignment.
- `Assets/Scripts/Modes/Tactical/TacticalUtilities.cs`: remove ProcessTravelingUnits, its private GenerateFleeingArmy helper and the uncalled GenerateAnotherArmy helper. These used strategic pathfinding/traveling armies, town ownership, monster empires and nearby strategic tiles. Remove the now-unused army/village/garrison fields and ResetData arguments; update its sole argument-taking caller in TacticalMode. Parameterless cleanup remains.

Search of Assets C# sources found only the three removed travel dispatches, one internal fleeing-army invocation, no GenerateAnotherArmy callers and no external FledReturn use. The separate StrategicUtilities.ProcessTravelingUnits is a different owner and is not changed. In normal standalone battles World.Villages is null, so the retired travel routine previously returned before doing any routing. The old choice dialog nevertheless appeared after a human victory with retreaters; it now no longer offers a meaningless town destination.

Tactical retreat controls, battle completion, digestion, revival/HP behavior, XP, deployment capacity, village battlefield/garrison data, bounties/loot, Wins counters, summaries and Pure Tactical return remain outside this edit. Losing-side retreat lists remain local battle bookkeeping; nothing is declared killed or left behind by this cleanup. New company survivors, stalemate and retreat policies remain specs 10/11 work. No species approved or generated for testing.

## Verification

Final Unity 2018.3.12f1 compilation passed exit 0 without C# errors/import exceptions: Logs/packet-05a3b3-compile-final.log. Initial compile also passed before context narrowing. Compiler warning set matches 05A3B2 after line-number normalization. Explicit base.unity opening passed exit 0 without parse errors/exceptions: Logs/packet-05a3b3-scene.log. Scene bytes match the packet snapshot; scoped diff check passed. No Unity process remained after checks. Ignored Logs/packet-05a3b3-* snapshots, diffs and source-review.json record exact correspondence outside enumerated edits/notices. No new tests/framework, agent Play-mode test, player build or personal-data access. Battle/retreat behavior cannot yet be playtested with the intentionally empty curated roster.

## GPL and scope

License.txt sections 4/5 reviewed; content hash matches HEAD. Both modified files have dated conversion notices, prior notices/attribution are retained and CHANGES.md names both paths. No external source, dependency or asset introduced. No files deleted/moved, serialized schemas changed or source publication performed. Distribution obligations remain a separate pre-release review.

## User check and next step

Confirm main menu and Pure Tactical setup open without new red errors. Existing settings/editor checks need not be repeated. After acceptance, review bounty/loot result handling as a separate bounded packet; other revival and old-world providers remain tracked before species prototype retirement.

User confirmed the manual startup check working and authorized the next packet (2026-10-31). Actual battle-level retreat checks remain pending approved species.
