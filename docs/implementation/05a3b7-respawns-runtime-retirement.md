# 05A3B7 — Respawns runtime retirement

**Date:** 2026-10-31. User accepted 05A3B6. Three source files changed.

## Trace and source changes

The Respawns status caused resurrection after weapon/spell kills in Actor_Unit and completed digestion in PredatorComponent. Grants came from Respawner/RespawnerIII during CurrentMaxMovement and Goddess Pendant's tactical turn callback. The user permanently deprecated alternate resurrection; all three execution blocks and these grants are retired.

- `Assets/Scripts/Entities/Units/Actor_Unit.cs`: remove both kill-overload respawn blocks (full health, random relocation, effects/logging and status consumption) and the two initial trait grants. Remove Respawns' exclusion from the existing automatic-surrender condition; stale status no longer prevents ordinary surrender. Existing surrender chance, HP behavior and other conditions remain unchanged. Remove CloseCall's special corpse hiding and fake fled message, left after 05A3B5 removed its result revival. Existing corpse visibility/species exceptions remain.
- `Assets/Scripts/Entities/Units/PredatorComponent.cs`: remove digestion-death respawn restoration/release/early return. Completed digestion now continues through existing fresh-kill callbacks, corpse handling and normal cleanup. Digestion conversion/rebirth branches are unchanged and still require retirement review.
- `Assets/Scripts/Entities/Items/ItemRepository.cs`: remove only Goddess Pendant's OnTacticalTurnStart respawn callback and its description sentence. Keep battle-start Bolstered callback, cost/tier/cooldown fields and item ID/order. Broader equipment balance/catalog cleanup is later work.

Search found no other runtime Respawns grants or consumers outside the retained Unit status helpers/type handling after these edits. Unit.AddRespawns/RemoveRespawns and enum/status/trait data remain stored compatibility definitions; no source files are deleted. No temporary summon, clone, new Downed, health pool, company survivor baseline or leader revival mechanic changed. Other original-character resurrection utility/UI/AI helpers remain to retire; Reanimate and digestion conversion are separate concerns. Removing chance/relocation/presentation RNG draws changes shared random consumption; no legacy same-seed equivalence promise.

## Verification

Unity 2018.3.12f1 compilation passed exit 0 without C# errors/import exceptions: Logs/packet-05a3b7-compile.log. Compiler warning set matches 05A3B6 after line-number normalization. Explicit base.unity opening passed exit 0 without scene parse errors/exceptions: Logs/packet-05a3b7-scene.log. Exact source correspondence, scene-byte preservation and scoped diff checks passed; no Unity process remained. Ignored Logs/packet-05a3b7-* record source/scene snapshots, exact edit correspondence and diffs. No new tests/framework, agent Play-mode battle, player build or personal-data access. Actual combat/respawn/surrender checks await curated species; menu smoke does not verify these outcomes.

## GPL and scope

Read License.txt sections 4/5; Git content hash matches HEAD. All three sources carry dated conversion notices; existing notices/attribution, including PredatorComponent's upstream credit, are retained. CHANGES.md names all exact source paths. No external code/dependency/asset introduced, no files deleted/moved and no scene/prefab/serialized-schema edit. Existing licensing preserved; distribution review remains separate.

## User check and next work

Confirm main menu and Pure Tactical setup open without new red errors. Existing settings/editor tests need not be repeated. After acceptance, review digestion revival/conversion and the now-unused original-character resurrection utility/UI/AI paths. Distinguish Reanimate from approved temporary summons before choosing its retirement boundary. Old strategic revival/providers and item/trait catalogs remain unfinished before species data closure.

User confirmed the manual startup check working and authorized the next packet (2026-10-31). Actual combat checks still await curated species.
