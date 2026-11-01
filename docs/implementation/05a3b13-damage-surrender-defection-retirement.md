# 05A3B13 — damage/surrender defection retirement

**Date:** 2026-10-31. User accepted 05A3B12. Modified only `Assets/Scripts/Entities/Units/Actor_Unit.cs`.

## Scope and decisions

Design clarification 12 forbids spontaneous combat allegiance switching, and clarification 8 excludes ordinary permanent healing via automatic full recovery. Actor_Unit.Damage still implemented both. Remove the five-line TurnCoat damage-defection branch and the 24-line automatic-surrender conversion/Goddess Mercy recovery span. The latter switched sides, set AIAvoidEat, emitted conversion text and conditionally healed loyal or defecting units to full HP.

Retain the existing shared damage/barrier/HP handling, low-health random check and Cowardly surrender, MutualBiology/Berserk/teleport effects, automatic-surrender trigger/chance, 1 HP result, zero movement, configured prey release and ordinary casualty presentation. No new default values, guaranteed 0-HP Downed behavior, injury checks, post-battle baseline, lasting/temporary HP or leader revival introduced. The state still uses Surrendered until the dedicated combat change; do not claim the new Downed system is implemented.

TurnCoat/settings/enum metadata and other direct allegiance calls elsewhere remain. Spell charm, hypnosis, consumption-related conversions and trait callbacks need separate review. This is not global allegiance or healing closure. Removing the defection chance roll changes shared random consumption when surrender conversion would have been eligible; no legacy same-seed equivalence promise. No species approval or temporary summon/disguise changes.

## Verification

Unity 2018.3.12f1 compilation passed exit 0 without C# errors/import exceptions: Logs/packet-05a3b13-compile.log. Warning set matches 05A3B12 after line-number normalization. Explicit base.unity opening passed exit 0 without parse errors/exceptions: Logs/packet-05a3b13-scene.log. Exact source correspondence, scene-byte preservation and scoped diff checks passed; no Unity process remained. Ignored Logs/packet-05a3b13-* contain source/scene snapshots, exact source-edit correspondence and diff. No new tests/framework, agent Play-mode battle, player build or personal-data access. Actual damage/surrender regression checks await curated species; startup smoke cannot verify those outcomes.

## GPL and scope

Read License.txt sections 4/5; content hash matches HEAD. Actor_Unit retains earlier notices and gains a dated conversion notice; CHANGES.md lists the exact source path. No external code/dependency/asset introduced, paths deleted/moved or scene/prefab/schema changed. Licensing/attribution preserved; distribution review remains separate.

## User check / next work

Confirm main menu and Pure Tactical setup open without new red errors. Existing settings/editor tests need not be repeated. After acceptance review remaining charm/allegiance spells and trait/containment callbacks in bounded packets. Preserve legitimate fear/pain surrender and temporary summons/disguises. Providers/catalog/species boundaries remain unfinished.

User confirmed the manual startup check working and authorized the next packet (2026-10-31). Actual damage/surrender verification still awaits curated species.
