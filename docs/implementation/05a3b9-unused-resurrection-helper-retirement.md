# 05A3B9 — unused Resurrection helper retirement

**Date:** 2026-10-31. User accepted 05A3B8. Five source files modified; no scene edits.

## Scope and caller trace

05A3B6 retired Resurrection casting; 05A3B7 removed Respawns callers. Assets C# search then found only the target finder referenced by two obsolete AI routines, the uncalled picker referencing Resurrect, and no GetRandomTileForActor caller. No serialized asset references to the removed picker/placement names were found. These internal static helpers are not retained as a future leader-revival implementation.

- `Assets/Scripts/Modes/Tactical/TacticalUtilities.cs`: remove FindUnitToResurrect (6-line span), CreateResurrectionPanel (78-line span), and the 29-line span containing GetRandomTileForActor plus Resurrect. Preserve FindUnitToReanimate, CreateReanimationPanel, Reanimate and shared UI/context dependencies.
- `Assets/Scripts/AI/TacticalAI.cs`: remove TryResurrect (34-line span), CheckResurrect (35-line span) and its three-line action-economy branch. Preserve surrounding priority/order and reanimation logic.
- `Assets/Scripts/AI/StandardTacticalAI.cs`, `Assets/Scripts/AI/NonCombatantTacticalAI.cs`, `Assets/Scripts/AI/HedonistTacticalAI.cs`: remove one TryResurrect call each; NonCombatant also loses its three-line CheckResurrect branch. No derived overrides of the removed methods remain.

Post-edit source search has no removed helper/call names. Stored Resurrection metadata/enum IDs, item books, trait/status definitions and separate AI spell exclusions remain. No new combat mechanics, company revival, Downed/health pools or species approval. Reanimation and approved temporary summon behavior are untouched; Reanimate's original-casualty mutation still requires review against permanent-loss/allegiance decisions. This packet closes unused Resurrection helper paths, not every old revival/conversion/provider dependency.

## Verification

Unity 2018.3.12f1 compilation passed exit 0 without C# errors/import exceptions: Logs/packet-05a3b9-compile.log. Warning set matches 05A3B8 after line-number normalization. Explicit base.unity opening passed exit 0 without parse errors/exceptions: Logs/packet-05a3b9-scene.log. Exact source correspondence, scene-byte preservation and scoped diff checks passed; no Unity process remained. Ignored Logs/packet-05a3b9-* contain packet snapshots, exact edit correspondence and per-source diffs; original line endings/BOMs preserved. No new tests/framework, agent Play-mode battle, player build or personal-data access. Actual AI/battle regressions await curated species; startup smoke does not establish combat correctness.

## GPL and scope

Read License.txt sections 4/5; content hash matches HEAD. All five sources have dated conversion notices, preserving prior notices/attribution; CHANGES.md lists exact paths. No external code/dependency/asset introduced, paths deleted/moved or scene/prefab/schema changed. Licensing/attribution preserved; a distribution review remains separate.

## User check and next work

Confirm main menu and Pure Tactical setup open without new red errors. Existing settings/editor tests need not be repeated. Next review Reanimate's casualty mutation and remaining allegiance callbacks, preserving approved temporary clones/animals and disguises. Old strategic revival/providers, item/trait catalogs and species read closure remain unfinished.

User confirmed the manual startup check working and authorized the next packet (2026-10-31). AI/combat verification still awaits curated species.
