# 05A3B5 — post-battle survival-trait retirement

**Date:** 2026-10-31. User accepted 05A3B4. Changed only `Assets/Scripts/Modes/TacticalMode.cs`.

## Authority and scope

Design clarification 1 permanently deprecates alternate revival/replacement effects. The future company leader digestion-revival service is a distinct, unimplemented system. Earlier 05A3B1 removed reincarnation/saved-copy/replacement results; this packet removes the remaining two consecutive survival-trait revival branches in VictoryCheck.

The first branch revived dead non-summons through Eternal, LuckySurvival, Reformer, Revenant or DeathCheater at full HP. The second revived CloseCall casualties not killed by digestion at 1 HP. Both reset Surrendered, moved revived units into retreat/army/village collections, purged prey and removed the tactical actor. Those 58 lines are retired with no replacement rule. The surrounding actor loop, size/TempBoost reset, kill counter reset, ordinary surviving-unit assignment, battle completion/summary/exit and combat remain identical.

Trait IDs, race definitions, equipment/trait metadata and saved fields are retained. Other references are not declared closed: Actor_Unit corpse/CloseCall presentation, resurrection/reanimate spells and AI, utility resurrection, digestion rebirth/conversion and old strategic leader revival still need review. This is not a global trait filter and does not guarantee a deprecated trait cannot still be selected in developer data. No new Downed, temporary HP, permanent-loss or survivor baseline behavior introduced. No species approved. Removing revival chance rolls changes shared random consumption when those traits would have triggered; no legacy same-seed equivalence promise.

## Verification

Unity 2018.3.12f1 compilation passed exit 0 without C# errors/import exceptions: Logs/packet-05a3b5-compile.log. Warning set matches 05A3B4 after line-number normalization. Explicit base.unity opening passed exit 0 without parse errors/exceptions: Logs/packet-05a3b5-scene.log. Source correspondence, unchanged scene-byte and scoped diff checks passed; no Unity process remained. Ignored Logs/packet-05a3b5-* hold before snapshots, exact edit correspondence and source diff. No new tests/framework, agent Play-mode test, player build or personal-data access. Battle and revival tests await curated species; menu smoke is not verification of these branches.

## GPL and scope

Read License.txt sections 4/5; its content hash matches HEAD. TacticalMode retains prior notices and gains the dated conversion notice; CHANGES.md names its exact path and completed edit. No external code, dependency or asset introduced, no paths deleted/moved and no scene/prefab/schema edits. Attribution and licensing preserved; distribution obligations require later review.

## User check and next work

Confirm the main menu and Pure Tactical setup open without new red errors; existing settings/editor checks need not be repeated. After acceptance, trace on-death/spell resurrection and digestion conversion in bounded packets, preserving temporary summons. Combat provider isolation and species prototype closure remain unfinished.

User confirmed the manual startup check working and authorized the next packet (2026-10-31). Battle-level tests still await species curation.
