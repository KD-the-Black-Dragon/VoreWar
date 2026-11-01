# 05A3B11 — Bind/Resummon retirement

**Date:** 2026-10-31. User accepted 05A3B10. Two sources changed.

## Trace and implemented boundary

CastBind stole or captured an existing summon, stored its Actor_Unit in Unit.BoundUnit and changed fixed/current allegiance. A tied binding contest could instead turn that same summon into a permanent Adventurer with a fresh side and permanent traits. SummonBound restored the stored character after death via Reanimate or inserted that same Unit into a later battle with full health and spent level-ups. These paths conflict with the permanent-loss/no-spontaneous-allegiance design and temporary-only summons.

- `Assets/Scripts/Entities/Spells/SpellList.cs`: remove Bind's OnExecute/OnExecuteTile callbacks, mark its preserved description retired, and add Bind to IsRetired. The existing end-of-UpdateSpells filter and early rejection in both TryCast overloads apply without changing Unit.cs. Static object/dictionary entry, enum identity and target/range/tier metadata remain readable.
- `Assets/Scripts/Entities/Units/Actor_Unit.cs`: remove Binder's five-line single-use grant and the 177-line span containing CastBind/SummonBound. Search found only the two removed spell callbacks called these methods; AI RunBind uses guarded Spell.TryCast. No CastBind/SummonBound or BoundUnit reference remains in Actor_Unit after this edit.

Unit.BoundUnit serialized storage and its other references remain for later schema/provider review, along with item/trait catalog metadata. This packet does not erase stored individuals. Reanimate's picker/runtime/side-effect helper and obsolete AI planning/actions remain next cleanup; the picker can still call its own helper, so do not claim every internal revival helper is removed. Independent temporary creature/clone summons, disguises, other spell grants, ordinary combat/retreat and shared actor trait code are unchanged. Other charm/surrender/allegiance paths still require retirement. No new company fate/Downed/health/leader-revival rule or species approval. Removal of binding-contest/appearance/RNG operations changes random consumption in retired paths; no legacy seed-equivalence promise.

## Verification

Unity 2018.3.12f1 compilation passed exit 0 without C# errors/import exceptions: Logs/packet-05a3b11-compile.log. Warning set matches 05A3B10 after line-number normalization. Explicit base.unity opening passed exit 0 without parse errors/exceptions: Logs/packet-05a3b11-scene.log. Exact source correspondence, scene-byte preservation and scoped diff checks passed; no Unity process remained. Ignored Logs/packet-05a3b11-* contain exact source edits, snapshots and diffs including base.unity. No new tests/framework, agent Play-mode battle, player build or personal-data access. Actual summon/AI/combat checks await curated species; menu smoke cannot verify these paths.

## GPL and scope

Read License.txt sections 4/5; Git content hash matches HEAD. Both source notices updated with actual date while preserving earlier attribution/notices. CHANGES.md records exact paths. No external code/dependency/asset introduced, files deleted/moved or scene/prefab/schema changed. Existing license/attribution preserved; distribution review remains separate.

## User check / next packet

Confirm main menu and Pure Tactical setup open without new red errors. Existing settings/editor tests need not be repeated. After acceptance retire obsolete Reanimate/Bind utility/UI/AI routines, preserving separate temporary summons. Remaining allegiance callbacks, strategic providers/revival, catalogs and species read boundaries stay unfinished.

User confirmed the manual startup check working and authorized the next packet (2026-10-31). Battle-level checks still await curated species.
