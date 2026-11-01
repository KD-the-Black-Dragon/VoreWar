# 05A3B2 — battle-result territory retirement

**Date:** 2026-10-31. User accepted 05A3B1. This bounded packet removes old campaign territory/statistics writes from battle finalization.

## Scope and evidence

- `Assets/Scripts/Modes/TacticalMode.cs`: remove two World.Stats.BattleResolution calls, one LostArmy call, StrategicUtilities.TryClaim and the CleanVillage callback.
- `Assets/Scripts/Modes/Tactical/TacticalUtilities.cs`: remove the now-uncalled 65-line CleanVillage helper. It pruned village hireables, changed ownership, consulted alliances/conquest settings, invoked strategic population devastation and altered monster movement/conquest state.

The only CleanVillage caller was VictoryCheck. Standalone tactical setup constructs World(false) and may supply a separate village battlefield/garrison object. That battlefield does not need a post-battle ownership or population simulation. Village setup, garrison/result identity and summaries are preserved. This intentionally removes the old village-population cleanup together with conquest, rather than replacing it with a new company policy.

Exact source correspondence against packet-start snapshots accounts only for those removals and two dated notices. Tactical win counters, battle completion calculations, survivor/retreat collection accounting, XP/healing, combat, loot/bounties, travel helpers, summary UI and Pure Tactical exit are unchanged. No new Downed, temporary HP, leader revival or company results implemented. Other resurrection and legacy provider paths remain unfinished.

## Verification

Unity 2018.3.12f1 compilation passed, exit 0, without C# errors/import exceptions: Logs/packet-05a3b2-compile.log. Explicit base.unity opening passed, exit 0, without scene parse errors/exceptions: Logs/packet-05a3b2-scene.log. Compiler warnings are identical to 05A3B1 after line-number normalization; no new warnings. Scene bytes match the packet snapshot; scoped diff check passed. No Unity process remained after checks. Evidence uses ignored Logs/packet-05a3b2-* snapshots, source-review.json and source diffs. No new tests/framework, agent Play-mode test, player build or personal-data access. Battle-result playtesting remains pending until species curation; startup checks cannot establish battle correctness.

## GPL and scope review

License.txt sections 4/5 reviewed. Both modified sources preserve prior notices and carry dated conversion notices; CHANGES.md lists exact paths and completed changes. Existing upstream narrative credit in TacticalUtilities remains. No external code, asset or dependency introduced. License content hash matches HEAD. No source/asset paths deleted or moved. This is the per-change workflow check, not a completed distribution review.

## User check / next packet

Confirm main menu and Pure Tactical setup still open without new red errors; the species roster remains intentionally empty. Existing settings/editor checks need not be repeated. After feedback, trace relocation/retreat choices and rewards in a separate bounded packet, preserving summaries and combat accounting.

User confirmed the manual startup check working and authorized the next packet (2026-10-31). Battle-result playtesting still awaits curated species.
