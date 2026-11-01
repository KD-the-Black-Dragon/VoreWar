# 05A3B8 — digestion conversion retirement

**Date:** 2026-10-31. User accepted 05A3B7. Modified only `Assets/Scripts/Entities/Units/PredatorComponent.cs`.

## Trace and boundary

Design clarifications 1, 11 and 12 exclude alternate revival, permanent prey conversion and spontaneous allegiance changes. Four branches inside digestion processing violated those decisions:

- At completion of digestion, KuroTenko/PredConverter restored half HP, changed side, released prey and returned early (15 lines).
- DigestionConversion restored half HP and changed side, while DigestionRebirth also selected a replacement species (29 consecutive lines covering both branches).
- At full absorption, KuroTenko/PredRebirther restored half HP, changed side/species and released/removed prey instead of completing the ordinary absorption path (31-line conditional including its ordinary log fallback).

Remove those branches and retain the ordinary RegisterAbsorb call unconditionally at the former fallback location. Remove the 11-line uncalled, comment-only HandleShapeshifterRebirth helper. Exact source correspondence accounts for those edits and the dated notice only. No direct prey ChangeSide or prey ChangeRace call remains in this source; that is not a claim that all allegiance mechanics elsewhere are retired.

Normal digestion/absorption logging, completed-death flags/statistics, healing/mana, XP, callbacks, trait gains, sub-prey movement and final cleanup are unchanged. Former conversion cases now continue through that ordinary path. Temporary predator disguises/template changes, prey-location handling, summons and generic identity APIs are preserved. Stored trait/settings/enum IDs remain. Other callbacks, allegiance effects and Reanimate's casualty mutation need separate review. No new Downed, HP, leader revival or contract outcome mechanics, and no species approval. Removed chance/species-selection draws change shared RNG consumption; no legacy same-seed equivalence promise.

## Verification

Unity 2018.3.12f1 compilation passed exit 0 without C# errors/import exceptions: Logs/packet-05a3b8-compile.log. Compiler warning set matches 05A3B7 after line-number normalization. Explicit base.unity opening passed exit 0 without parse errors/exceptions: Logs/packet-05a3b8-scene.log. Exact source correspondence, scene-byte preservation and scoped diff checks passed; no Unity process remained. Ignored Logs/packet-05a3b8-* contain exact edit correspondence, source diff and packet-start source/scene snapshots. No new tests/framework, agent Play-mode battle, player build or personal-data access. Actual digestion/absorption/conversion checks await curated species; menu smoke is not battle verification.

## GPL and scope

Read License.txt sections 4/5; its Git content hash matches HEAD. The source retains previous notices and upstream narrative-contribution credit, with a new dated conversion notice; CHANGES.md names the exact source path. No external source/dependency/asset introduced, paths deleted/moved or scene/prefab/schema changed. Licensing/attribution preserved; distribution obligations remain a separate review.

## User check and next work

Confirm main menu and Pure Tactical setup open without new red errors. Existing settings/editor tests need not be repeated. After acceptance, retire unused original-character resurrection utility/UI/AI paths, then review remaining allegiance callbacks and Reanimate. Preserve approved temporary clone/animal summons and temporary disguises. Old strategic providers/revival and item/trait catalogs still require closure before species prototype retirement.

User confirmed the manual startup check working and authorized the next packet (2026-10-31). Battle-level checks still await curated species.
