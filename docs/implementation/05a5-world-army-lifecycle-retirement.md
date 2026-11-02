# 05A5 — World/Army campaign lifecycle retirement

**Date:** 2026-11-01. User accepted 05A4 screen checks. Three source files changed as one grouped lifecycle stage.

## Trace and completed changes

`Assets/Scripts/Core/World.cs`: Assets-wide search found one constructor caller, CreateTacticalGame's new World(false), and no true caller. Remove the unused map-editor branch and make the constructor parameterless while preserving its previous false-path statements and field defaults exactly. Keep configuration/item repository and empty supporting arrays; do not populate empire/village collections merely to satisfy old code. Update the sole caller in `Assets/Scripts/Subwindows/CreateTacticalGame.cs`.

Remove uncalled RefreshMonstersKeepingArmies, InitializeMonsters (the 52-entry hard-coded monster empire constructor list), RefreshEmpires, PopulateMonsterTurnOrders and RenameBunnyTownsAsPreyTowns. They had only self-references within this group. Keep UpdateBanditLimits and RefreshTurnOrder signatures as inert compatibility entries for retired map/world/empire settings callers. Preserve MonsterCount, monster/empire fields, all IDs and GetEmpireOfRace/GetEmpireOfSide. Remove unused LINQ import, without changing reusable terrain generation sources.

`Assets/Scripts/Entities/Army.cs`: Refresh callers are Empire, MonsterEmpire and the retired recruit-cheat control, not standalone combat. Its old turn pipeline healed/restored mana, adjusted appearance/growth, ticked strategic equipment, redistributed spell books, reset movement/cooldowns and applied village research/XP/traits. Retire that pipeline; retain inert Refresh signature. Retire direct civilian DevourHeal and Train mutation; report zero civilian devour capacity. GetTileHealRate now only clears stale InVillageIndex, including calls from the position setter and old map editor. Remove UpdateInVillage, ProcessInVillageOnTurn and SortSpells after their only callers are removed. Training price/XP query signatures remain for old UI compatibility, but cannot train/pay through Train.

No Pure Tactical Refresh call exists. Underlying battle army construction, units, ItemStock, roster capacity/size, tactical movement-mode helpers, naming/banner context and fields remain. This deliberately prevents future company code from accidentally inheriting the old daily simulation. It does not add the new campaign recovery, supplies, wages, sparring or procedural world model.

## Remaining provider inventory

| Retained owner | Current purpose / boundary | Remaining work |
| --- | --- | --- |
| World context | Item repository, config storage, IsNight and nullable empire lookup fields; used by standalone battle/snapshots | Explicit new campaign/battle/save context later; do not infer a running 4X world from the class name. |
| Army + lightweight Empire | Roster, equipment stock, deployment size, names/colors/race metadata and AI power context | Review compatibility constructor/size/identity/hostility before replacement; no blanket removal. |
| Army movement / prune | Grid/movement helpers and legacy Prune remain | Prune removes units and writes strategic stats; do not reuse it as a company redraw or survivor cleanup. Retired strategic callers still exist in source. |
| TacticalMode / TacticalUtilities | Village/garrison map option, team/race context, AI policies and normal battle loop | Pure Tactical remains separate; empire-aware retreat policies and context lookups need final boundary review before company integration. |
| TacticalMapGenerator / grid/path helpers | Terrain/layout/walls and reusable tile data | Strategic map rendering/path owners still need isolation; no terrain algorithm or tactical scenery change here. |
| Legacy menus / strategic classes | Some compatibility callers remain compiled behind retired routes | Residual cleanup report must distinguish inert entry points from retained data/helper code. |
| Persistence | Existing World-root tactical writer/snapshot remains; old load entry is retired | New-format/save ownership belongs to later reviewed integration; this packet does not restore save loading. |
| Species data | Eager monster empire registry no longer constructs discarded species | Remaining stat/graphics/override readers still gate prototype retirement; no species enabled. |

This is not complete spec 05 or final phase-2 review. Next consolidate the remaining battle identity/AI/hostility and strategic map-provider boundaries; do not reintroduce the old lifecycle as a new company time system.

## Verification

Unity 2018.3.12f1 compilation PASSED exit 0 without C# errors/import exceptions: Logs/packet-05a5-compile.log. No new compiler warnings versus 05A4. First scene launch stopped before loading with Package Manager startup cancelled, exit 1 (packet-05a5-scene.log); retained as failed evidence. A fresh retry opened base.unity and PASSED exit 0 without parse errors/exceptions: Logs/packet-05a5-scene-retry.log. Exact source correspondence/notice/scene-byte/license-hash/scoped diff checks passed; no Unity process remained. Ignored Logs/packet-05a5-* include source/scene snapshots, exact edit correspondence, per-source diffs and removed-method span inventory. No new tests/framework, agent Play-mode battle, player build or personal-data access. Actual ordinary/village/bulk combat tests await species curation; setup smoke does not establish combat correctness.

## GPL and scope

Read License.txt sections 4/5; content hash matches HEAD. All three sources have dated conversion notices, preserving earlier legal/attribution notices; CHANGES.md lists exact paths. No new third-party code/dependency/asset, deleted/moved paths or scene/prefab/serialized-field edits. Constructor source API changes are intentional and its only caller is updated. Distribution obligations require separate review.

## One combined user check

Open main menu and enter/leave Pure Tactical setup without new red errors. No settings/editor retest or sample species needed. Future battle checks after curation must cover ordinary and village maps, AI/bulk modes and equipment, since those paths cannot yet be exercised with the empty roster.

User confirmed the requested manual check successful and authorized the next stage (2026-11-01). Actual combat regressions remain pending species curation.
