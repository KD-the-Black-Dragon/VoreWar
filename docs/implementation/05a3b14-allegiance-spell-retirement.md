# 05A3B14 — allegiance spell retirement

**Date:** 2026-10-31. User accepted 05A3B13. Two sources modified.

## Trace and boundary

Capture Net directly switches a weak target's allegiance; Charm makes a target fight for the caster; Hypnotic Gas changes targets into servants of the caster's side; Whispers applies charm alongside other effects. These conflict with design clarification 12. Retire whole spells without inventing replacements or rebalancing their secondary effects.

- `Assets/Scripts/Entities/Spells/SpellList.cs`: remove all execution callbacks on those four objects, including both actor and tile HypnoGas callbacks; mark descriptions retired; add their types to Spell.IsRetired. Preserve their static objects, enum/dictionary identities, status metadata, range/tier/target/effect definitions. Existing UpdateSpells filtering and both TryCast guards cover ordinary player/AI casting.
- `Assets/Scripts/Entities/Units/Actor_Unit.cs`: remove the Charmer and HypnoticGas trait automatic grant blocks; preserve other grant behavior. Add early false return in CastStatusSpell for retired spells before spending mana or invoking OnSpellCast. The Whispers trait in Traits.cs calls CastStatusSpell directly during digestion, bypassing Spell.TryCast; this guard retires that execution path without modifying the trait class yet.

No stored grants/items/traits/status IDs are rewritten. Developer/item catalogs can still show retired entries until catalog curation. Traits.cs may still advertise a Whispers grant, but usable-list filtering and direct-status guard prevent its execution. Other direct ApplyStatusEffect/defection/containment/trait paths remain review work; this packet does not globally reject charm status or remove generic side APIs. Ordinary spells, fear/pain surrender, temporary summons and disguises remain unchanged. No new Downed/HP/leader revival or species approval. Removed retired callbacks cease their RNG/resource/effect behavior; no legacy same-seed equivalence promise.

## Verification

Unity 2018.3.12f1 compilation passed exit 0 without C# errors/import exceptions: Logs/packet-05a3b14-compile.log. Warning set matches 05A3B13 after line-number normalization. Explicit base.unity opening passed exit 0 without parse errors/exceptions: Logs/packet-05a3b14-scene.log. Exact source correspondence, scene-byte preservation and scoped diff checks passed; no Unity process remained. Ignored Logs/packet-05a3b14-* contain packet source/scene snapshots, exact edit records and diffs. No new tests/framework, agent Play-mode battle, player build or personal-data access. Actual spell/AI/trait regressions await curated species; startup smoke is not combat verification.

## GPL and scope

Read License.txt sections 4/5; content hash matches HEAD. Both sources retain prior notices and gain dated conversion notices; CHANGES.md records exact paths. No external code/dependency/asset introduced, files deleted/moved or scene/prefab/schema changed. Licensing/attribution preserved; distribution review remains separate.

## User check / next work

Confirm main menu and Pure Tactical setup open without new red errors. Existing settings/editor tests need not be repeated. Next trace remaining direct trait/status/containment allegiance effects and callbacks, preserving fear/pain surrender and temporary disguises/summons. Providers/catalogs/species read closure remain unfinished.

User confirmed the manual startup check working and authorized the next packet (2026-11-01). Combat verification still awaits curated species.
