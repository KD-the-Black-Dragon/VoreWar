# 05A3A — campaign world and battle entry retirement

**Date:** 2026-10-31. User accepted 05A2 and authorized this packet. Implemented; final compilation and explicit scene loading passed exit 0 without C# errors/import exceptions/scene errors. User smoke review pending. No species approved.

## Three changed sources

- `Assets/Scripts/Core/World.cs`: retire the uncalled World(StrategicCreationArgs, Map) constructor, including territorial population/empire creation and its setup chain. Keep the file and all other constructors, fields, enums and helpers. Source search before/after shows the only current World construction is CreateTacticalGame's World(false).
- `Assets/Scripts/Core/GameManager.cs`: retire delayed, queued and direct campaign tactical activation plus loaded-world tactical transition bodies. Each reports the existing unavailable message before former effects. Remove four obsolete queue payload fields and the automatic battle-dispatch line from camera-transition completion; preserve camera animation itself. Retain read compatibility fields explicitly initialized to their previous defaults (queuedTactical false, CurrentPreviewSkip Watch).
- `Assets/Scripts/Modes/TacticalMode.cs`: reject direct Begin calls unless PureTactical is set, before any setup mutations. Everything after this guard is unchanged, including combat, results, village sandbox support and AI.

ActivatePureTacticalMode is unchanged and sets PureTactical before calling Begin. The sole remaining source caller of Begin is that method. Standalone village battle setup in CreateTacticalGame remains supported; a blanket village-null restriction was deliberately avoided. No new company battle context or save system is introduced.

## Source review and checks

The retained World source matches its snapshot after removing exactly the constructor and adding its notice. TacticalMode matches in full after stripping the new guard/notice. Twenty-two unaffected GameManager void-method bodies match snapshots, including ActivatePureTacticalMode and the previously preserved return branch. Other GameManager changes are the four entry bodies, obsolete queue dispatch/state removal and explicit previous defaults.

Fresh final compilation: ignored Logs/packet-05a3a-compile-final.log; initial unsuffixed compilation predates explicit compatibility initializers. Scene evidence: Logs/packet-05a3a-scene.log. Completed results are recorded at handoff. No agent Play-mode test, new player build, personal data access or new tests/framework. Real combat entry/results/return tests remain deferred with the empty roster.

This packet does not retire all World/Empire/Village methods, monster registries or battle-result providers. Tactical results still contain campaign-specific branches, rebirth/replacement, travel and bounty logic; these require separately reviewed retirement with retained skirmish behavior identified. The campaign gate prevents supported entry into that old flow, but does not make every helper safe to remove. Stat/graphics prototypes remain and no generic replacement species was added.

## GPL and scope

Reviewed License.txt sections 4/5. All three modified C# files carry dated notices and exact-path CHANGES coverage, retaining earlier notices/attribution. No third-party source/dependency/assets introduced. No paths deleted/moved, no scene/prefab/resource changes. Source comparison and license/scene byte checks supplement compile checks; no distribution compliance certification is claimed.

## User review and next packet

Open Play mode and Pure Tactical setup: the expected empty-approved-roster explanation should remain, with no new errors. Main menu/settings and gated Race Editor should remain accessible. No sample species or legacy save is needed.

After feedback, audit a bounded 05A3B World/monster-provider and campaign-results cleanup, prioritizing remaining creation paths before stat definition retirement. Keep combat rules, temporary summons, identity and shared map/item services; do not implement company battle results or new health mechanics during retirement.

Final scope check: scene bytes match packet snapshot, license Git hash matches tracked content, and packet source diff check passes. No obsolete orphan metadata recreated.

The user confirmed the requested 05A3A checks passed and authorized the next results cleanup.
