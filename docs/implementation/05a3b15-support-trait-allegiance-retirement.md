# 05A3B15 — support-trait allegiance retirement

**Date:** 2026-11-01. User accepted 05A3B14 and closed Unity for verification. Modified only `Assets/Scripts/Entities/Units/Actor_Unit.cs`.

## Scope

SeductiveTouch let a support action change the target's current/fixed allegiance and mark the new allegiance hidden. The same randomized branch could instead paralyze the target as a distraction. Under design clarification 12, retire this complete 38-line trait branch rather than inventing a weaker replacement. Remove its separate enemy-support permission exception; Config.CanUseStomachRubOnEnemies and the existing mind-control permission are untouched pending their own review.

The underlying support action, target/fullness/received-support checks, action costs, digestion calls and other support traits remain unchanged. IDs, trait metadata, sprites and animations remain. No new Downed/HP/fate or species approval. Removed stat-clash/presentation RNG draws change consumption in retired paths; no legacy same-seed equivalence promise.

Read review also found MetamorphicConversion/Metamorphosis revival and possession callbacks in Traits.cs, plus Friendosoma's results conversion. They are not edited here. They alter identity/survival or result cleanup and need their own bounded review before retirement; do not declare global allegiance closure or substitute an unreviewed casualty outcome. Other temporary disguises/summons remain unchanged.

## Verification

Unity 2018.3.12f1 final compilation passed exit 0 without C# errors/import exceptions: Logs/packet-05a3b15-compile-final.log. Initial compile log is also retained; combined warning set introduces no new warnings versus 05A3B14 after line-number normalization. Explicit base.unity opening passed exit 0 without parse errors/exceptions: Logs/packet-05a3b15-scene.log. Exact source correspondence, scene-byte preservation and scoped diff checks passed; no Unity process remained. Ignored Logs/packet-05a3b15-* contain source/scene snapshots, exact edits and diff. No new tests/framework, agent Play-mode battle, player build or personal-data access. Combat-level support regression checks await curated species; startup smoke does not verify these outcomes.

## GPL and scope

Read License.txt sections 4/5; content hash matches HEAD. Dated source notice and exact-path CHANGES.md entry added; prior notices/license/attribution retained. No external code/dependency/asset introduced, files deleted/moved or scene/prefab/schema changed. Distribution review remains separate.

## User check / next work

Confirm main menu and Pure Tactical setup open without new red errors; existing settings/editor tests need not be repeated. Next review possession/metamorphosis and containment allegiance callbacks, including Friendosoma result handling, while preserving temporary disguises/summons. Providers/catalog/species read closure remain unfinished.

User confirmed the manual startup check working and authorized the next packet (2026-11-01). Combat verification still awaits curated species.
