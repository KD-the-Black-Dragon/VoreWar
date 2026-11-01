# 05A3B12 — unused reanimation/Bind helper retirement

**Date:** 2026-10-31. User accepted 05A3B11. Five sources changed.

## Boundary and trace

After Reanimate and Bind entry retirement, source references to their execution helpers existed only within their own obsolete utility/picker/AI cluster. The removed names were not found in serialized asset references. Remove that cluster without changing independent summon creation or identity schemas.

- `Assets/Scripts/Modes/Tactical/TacticalUtilities.cs`: remove the 79-line FindUnitToReanimate/CreateReanimationPanel span and 34-line Reanimate span. Remove the private UnitPickerUI field/assignment and unused UnityEngine.UI import. Shared tile selection/target qualifiers, actor-location context and other utilities remain unchanged.
- `Assets/Scripts/Modes/TacticalMode.cs`: remove the 12-line HandleReanimationSideEffects helper, which removed a casualty from army/garrison/extra collections and changed allegiance. Retain its serialized UnitPickerUI reference and scene object; no scene edit or file retirement needed.
- `Assets/Scripts/AI/TacticalAI.cs`: remove the consecutive 230-line TryReanimate/CheckReanimate/RunBind/CheckBind method span and the two three-line action-economy branches. Generic spell filters and other AI priorities remain verbatim.
- `Assets/Scripts/AI/StandardTacticalAI.cs`, `Assets/Scripts/AI/HedonistTacticalAI.cs`: remove TryReanimate and RunBind calls only, plus dated notices.

Post-edit source search finds no retired helper/action names. BoundUnit serialized storage and other references, spell/trait/item IDs and catalog entries remain pending provider/schema/catalog closure. This closes the original Reanimate/Bind execution/helper cluster; it does not claim all charm/surrender/allegiance/revival behavior elsewhere has been retired. Ordinary animal/clone summon creation, temporary disguises, combat, survivor rules, results and other AI actions are untouched. No new Downed/health/leader-revival rules or species approval.

## Verification

Unity 2018.3.12f1 compilation passed exit 0 without C# errors/import exceptions: Logs/packet-05a3b12-compile.log. Warning set matches 05A3B11 after line-number normalization. Explicit base.unity opening passed exit 0 without parse errors/exceptions: Logs/packet-05a3b12-scene.log. Exact source correspondence, scene-byte preservation and scoped diff checks passed; no Unity process remained. Ignored Logs/packet-05a3b12-* contain source/scene snapshots, exact source-edit correspondence and diffs. No new tests/framework, agent Play-mode battle, player build or personal-data access. Actual AI/summon regressions await curated species; menu smoke is not battle verification.

## GPL and scope

Read License.txt sections 4/5; content hash matches HEAD. All five sources retain previous notices and gain dated conversion notices; upstream utility narrative credit remains. CHANGES.md lists exact paths. No external code/dependency/asset introduced, paths deleted/moved or scene/prefab/schema changed. Existing license/attribution preserved; distribution obligations require separate review.

## User check / next work

Confirm main menu and Pure Tactical setup open without new red errors. Existing settings/editor tests need not be repeated. Next review remaining allegiance switching/charm/surrender/trait callbacks in bounded packets; preserve independent temporary summons and disguises. Old strategic revival/providers, catalogs and species read closure remain unfinished.

User confirmed the manual startup check working and authorized the next packet (2026-10-31). AI/combat verification still awaits curated species.
