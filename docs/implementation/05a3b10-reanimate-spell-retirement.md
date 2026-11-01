# 05A3B10 — Reanimate spell retirement

**Date:** 2026-10-31. User accepted 05A3B9. Two modified sources.

## Finding and decision

Reanimate passes the original dead Actor_Unit to TacticalUtilities.Reanimate. That routine restores health, changes the same Unit.Type to Summon and changes its fixed side through reanimation handling. It does not create a separate temporary copy of the casualty. Retire this spell under the existing decisions excluding alternate revival and mid-combat allegiance conversion. Approved temporary clones/illusions and animal summons remain unaffected.

- `Assets/Scripts/Entities/Spells/SpellList.cs`: remove Reanimate's OnExecuteTile callback; retain static object, dictionary/enum identity and tier/range/target metadata with retired description. Add Reanimate to Spell.IsRetired alongside Resurrection. Existing Unit.UpdateSpells filtering removes it after all assembled grant sources, and both TryCast overloads reject it before resources/effects. No Unit.cs edit needed in this packet.
- `Assets/Scripts/Entities/Units/Actor_Unit.cs`: remove the five-line Reanimator trait automatic single-use grant block. Other trait spell grants retain their exact behavior. Stored grant entries and item books/IDs remain readable; book/catalog presentation still needs later curation.

Bind/SummonBound is a separate path that can still invoke the shared Reanimate routine or add the stored BoundUnit back to battle. It remains explicit unfinished work; this packet is spell-only retirement, not global reanimation closure. Reanimation utility/picker/AI, HandleReanimationSideEffects and BoundUnit storage remain until Bind is retired. Other allegiance switching/charm/surrender callbacks also need review. No new Downed, health pools, company membership policy, leader revival, summon creation or species approval.

## Verification

Unity 2018.3.12f1 compilation passed exit 0 without C# errors/import exceptions: Logs/packet-05a3b10-compile.log. Warning set matches 05A3B9 after line-number normalization. Explicit base.unity opening passed exit 0 without parse errors/exceptions: Logs/packet-05a3b10-scene.log. Exact source correspondence, scene-byte preservation and scoped diff checks passed; no Unity process remained. Ignored Logs/packet-05a3b10-* contain packet-start source/scene snapshots, exact source-edit correspondence and diffs. No new tests/framework, agent Play-mode battle, player build or personal-data access. Actual spell/AI/result checks await curated species; startup smoke does not verify combat paths.

## GPL and scope

Read License.txt sections 4/5; Git content hash matches HEAD. Both modified sources retain previous notices and carry dated conversion notices; CHANGES.md lists exact paths. No external source/dependency/asset introduced, files deleted/moved or scene/prefab/schema changed. Licensing/attribution preserved; distribution obligations require their own review.

## User check and next work

Confirm main menu and Pure Tactical setup open without new red errors. No repeated settings/editor tests required. After acceptance, retire Bind's separate allegiance/casualty re-summoning path, then remove shared reanimation utility/UI/AI. Preserve independent temporary clones/animal summons and temporary disguises. Old strategic/provider/catalog and species read boundaries remain unfinished.

User confirmed the manual startup check working and authorized the next packet (2026-10-31). Spell/AI battle checks still await curated species.
