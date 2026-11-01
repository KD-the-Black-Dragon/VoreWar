# 05A3B6 — Resurrection spell retirement

**Date:** 2026-10-31. User accepted 05A3B5. Two modified sources; no scene edits.

## Trace and implementation

SpellList.Resurrection restored an existing dead unit through TacticalUtilities.Resurrect, optionally opening the player target picker. Reanimate instead mutates a casualty into a summon with side effects; it requires separate review and is untouched here. The user's permanent deprecation of alternate character revival authorizes retiring Resurrection.

- `Assets/Scripts/Entities/Spells/SpellList.cs`: retain Resurrection metadata, enum/dictionary identity, range/tier/target structure and the static object, label its description retired, remove its OnExecuteTile revive callback. Add computed Spell.IsRetired (currently only Resurrection) and early false returns to both TryCast overloads. Rejection precedes actor lookup, mana/movement reads, execution, single-use removal and spell refresh; direct cached/static spell casts therefore do not execute or spend resources through these entry points.
- `Assets/Scripts/Entities/Units/Unit.cs`: remove retired spells after all UpdateSpells grant sources are combined: individual innate, single/multi-use, trait/racial data, configured innate and equipped spell books. This keeps normal UI/AI lists clear without rewriting source grants, book items or stored IDs. Search found these list additions owned by UpdateSpells; both player spell controls and AI use TryCast. No direct Resurrection callback invocation remains.

Old Resurrection book/catalog metadata can still appear in developer/item-generation contexts; it provides no usable Resurrection spell. This packet does not finish item catalog curation. Retained AI target search/planner helpers, utility resurrection panel/Resurrect and other trait/digestion callers remain tracked work rather than being declared globally retired. Other spells, reanimation, creature/clone summons, spell costs and cast logic retain their previous behavior. No new Downed/temporary HP/survivor recovery/leader revival rule or species approval.

## Verification

Unity 2018.3.12f1 compilation passed exit 0 without C# errors/import exceptions: Logs/packet-05a3b6-compile.log. Compiler warning set matches 05A3B5 after line-number normalization. Explicit base.unity opening passed exit 0 without parse errors/exceptions: Logs/packet-05a3b6-scene.log. Exact source correspondence, scene-byte and scoped diff checks passed; no Unity process remained. Ignored Logs/packet-05a3b6-* contain source and scene snapshots, exact edit records and source diffs. Source line endings are preserved per file. No new tests/framework, agent Play-mode battle, player build or personal-data access. Actual spell/AI checks await curated species; startup smoke cannot verify execution paths.

## GPL and scope

Read License.txt sections 4/5; its Git content hash matches HEAD. Both changed files retain prior notices and have dated conversion notices. CHANGES.md names their exact paths. No external code/dependency/asset introduced, files deleted/moved, stored schemas changed or publication performed. Existing license/attribution preserved; a future distribution still needs its own review.

## User check and next work

Confirm main menu and Pure Tactical setup open without new red errors. Existing settings/editor checks need not be repeated. Next trace Actor_Unit on-death and CloseCall presentation plus digestion revival/conversion in bounded packets. Reanimate's mutation of a persistent casualty into a summon needs explicit review against the no-conversion/permanent-loss design; preserve approved clone/animal temporary summons. Old strategic revival and combat/provider dependencies remain unfinished.

User confirmed the manual startup check working and authorized the next packet (2026-10-31). Spell/AI battle checks still await species curation.
