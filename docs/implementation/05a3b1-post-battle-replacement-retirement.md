# 05A3B1 — post-battle replacement retirement

**Date:** 2026-10-31. User accepted 05A3A; this is the first bounded campaign-results cleanup. Implemented in one source; compilation and explicit scene loading passed exit 0 without errors/exceptions; startup smoke review pending.

## Exact source and effect

Modified `Assets/Scripts/Modes/TacticalMode.cs`:

- Remove the ProcessReplaceable call and private method/local replacement routine. Fallen Replaceable soldiers no longer have experience reduced, identity/appearance rerolled, counters reset and health restored by that post-battle path.
- Remove the consecutive Transmigration, Reincarnation and SavedCopy result branches. Battle finalization no longer queues those campaign reincarnations/reports or returns village-bound copies through this path.

The user permanently deprecated these alternate resurrection/replacement systems. The future leader-only revival cutscene is not implemented here. Serialized trait IDs, unit fields, Reincarnator data types and World fields are retained until their other dependencies are retired; no saved individuals or user files are deleted.

## Preservation and limits

Whole-file source comparison matches the packet snapshot after exactly the 68 result-branch lines, 49 replacement-method lines, single call and modification notice are accounted for. Victory calculation/ending conditions, ordinary cleanup, XP/healing, CloseCall and other surviving trait behavior, temporary summons, unit assignment/travel, village/loot/bounty handling, summaries and Pure Tactical return remain unchanged. This is not completion of all resurrection retirement or of the new Downed/HP/fate rules.

Remaining campaign travel/territorial/reward behavior and World/monster/Empire/Village providers need separate closure. No stat/graphics definitions or species approval changed. No new company results, injury rules, names, save format or RNG architecture introduced. Removing replacement identity randomization changes shared random draws when an unsupported old trait would have triggered; no same-seed equivalence promise.

## Verification

Fresh Unity 2018.3.12f1 compilation passed exit 0 without C# errors/import exceptions: ignored Logs/packet-05a3b1-compile.log. TacticalMode's existing unreachable-code and unassigned building-range warnings also appear in the prior 05A3A final log; they were not introduced or fixed here. Explicit scene log: Logs/packet-05a3b1-scene.log; completed result recorded before handoff.

Source correspondence and unchanged scene-byte comparison passed. No new tests/framework, agent Play-mode test, personal-data access or player build. Actual replacement/fate/battle-result tests remain pending until approved species exist; menu smoke checks cannot verify them.

## GPL and scope

License.txt sections 4/5 reviewed. TacticalMode.cs retains prior notices and gains a dated conversion notice; CHANGES.md records the exact path. No new external code, dependency or asset; license/attribution preserved. No paths deleted or moved; schemas/scene/prefabs unchanged. This does not establish complete distribution compliance.

## User check and next work

Startup smoke check only: enter Play mode and confirm main menu/Pure Tactical setup open without new red errors. The roster remains intentionally empty. Existing settings/editor checks are already accepted; no reset or sample species is needed.

After feedback, trace a bounded 05A3B2 campaign-result relocation/territory/reward cleanup, with standalone village skirmish support explicitly distinguished. Keep summaries and retained combat outcomes. Other revival spell/trait/equipment paths and old-world monster providers remain tracked work before final stat-definition closure.

Final scene byte/license hash checks passed; packet diff check passed. No obsolete metadata paths recreated.

User confirmed the manual check working and authorized the next packet (2026-10-31). Actual battle verification remains blocked by the intentionally empty approved roster.
