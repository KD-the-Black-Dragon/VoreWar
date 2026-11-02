# 05A3B16 — metamorphosis-family retirement

**Date:** 2026-11-01. User accepted 05A3B15. Modified only `Assets/Scripts/Races/Traits.cs`.

## Trace and scope

Metamorphosis.OnFinishDigestion restored full health, granted XP and changed the consumed character's race, then returned false to interrupt ordinary digestion. MetamorphicConversion additionally changed allegiance and invoked that revival. ForcedMetamorphosis granted one of those traits to the predator, overwrote its spawn species and blocked escape. Retire the family under design clarifications 1/11/12.

Keep all three concrete class names and registry entries, with retired descriptions and IsPredTrait false. Remove their processing-priority overrides, custom callbacks, no-auto-escape interfaces and escape suppression. The inherited VoreTrait callbacks return true, permitting ordinary processing instead of revival. ForcedMetamorphosis changes from VoreTraitBooster to VoreTrait; Unit's booster dispatch invokes Boost without a null guard, so leaving an empty booster object would be unsafe. No replacement boost or identity mechanic added.

Post-edit trait-ID references outside registry remain only species metadata, tag/tooltip metadata and the hidden-tag list; no other explicit runtime grant/check was found. Those definitions/IDs are preserved pending catalog/species work. Existing upstream author instructions/credits remain. Possession/SpiritPossession and Friendosoma/containment result code remain separate review. Other trait classes, temporary disguises, summons and shared callback pipeline are unchanged. No new Downed, HP, survivor baseline, leader revival or species approval. Former revival cases now proceed through ordinary digestion; actual fate-system redesign belongs to later combat specs.

## Verification

Unity 2018.3.12f1 compilation passed exit 0 without C# errors/import exceptions: Logs/packet-05a3b16-compile.log. No new warnings versus the prior full 05A3B14 compile after line-number normalization. Explicit base.unity opening passed exit 0 without parse errors/exceptions: Logs/packet-05a3b16-scene.log. Exact source correspondence, scene-byte preservation and scoped diff checks passed; no Unity process remained. Ignored Logs/packet-05a3b16-* contain source/scene snapshots, exact edit records and diff. No new tests/framework, agent Play-mode battle, player build or personal-data access. Actual trait/digestion regressions await curated species; startup smoke cannot verify combat outcomes.

## GPL and scope

Read License.txt sections 4/5; content hash matches HEAD. Added dated conversion notice and exact-path CHANGES.md entry; existing source comments/attribution and licensing retained. No external source/dependency/asset introduced, paths deleted/moved or scene/prefab/schema changed. A future distribution requires separate review.

## User check / next work

Confirm main menu and Pure Tactical setup open without new red errors; existing settings/editor tests need not be repeated. Next trace possession/SpiritPossession and containment allegiance callbacks, including Friendosoma result handling, before retiring them. Preserve temporary disguises/summons. Providers/catalog/species read closure remain unfinished.

User confirmed the manual check working on 2026-11-01 and requested that all remaining approved ability retirement be completed as one consolidated effort before the next manual test.
