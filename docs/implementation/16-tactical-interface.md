# 16 — Tactical battle interface

Date: 2026-11-02. Authorized scope: live tactical battle HUD; the accepted Single Battle setup and its 2x portraits are unchanged.

## Implemented

The existing dark/moderate blue palette now covers tactical panels, controls and dynamically created ability/context buttons. Team-color information, artwork, health/mana/AP information and command sizes remain. The redundant bottom attacker/defender species/controller labels are hidden; their references remain intact.

Attack replaces the separate main melee/ranged commands. Both native 1/2 bindings select it. Distance 1 uses the existing best melee weapon (including natural/unarmed attacks); greater distance requires the existing best ranged weapon within its range. An invalid target spends no action. One shared range choice drives target percentages, hover damage and the right-click Attack entry. Native Actor_Unit.Attack still handles cost, damage, traits, animation and special failures; a valid attempt still performs the native action cleanup, including a failed Dazzle roll. Explicit AI and special melee/ranged paths remain unchanged. Downed units cannot be directly attacked.

The tactical inspector shows Melee, Ranged, Spell 1 and Spell 2. Company units read their dedicated equipment slots; single-battle units classify their legacy inventory. Four bordered boxes in a two-by-two layout have headings outside them; empty slots show no item text or natural-weapon fallback. The original inactive equipment hierarchy remains for existing inspection references; new labels use the existing item-name tooltip lookup. Extra rows consume 82 reference units of the scrollable details region, preserving core statistics. Only the trait block is reduced to 85%; all other detail text stays at its existing size.

Attack, Move, Vore and End Turn share a single padded row; End Turn is twice its original width (160). AP/Undo and the equal-width Auto-resolve/AI pair occupy separate columns, with gaps between groups. Existing command fonts remain.

Compact log height 160→260 and font 28→22 provide room for at least 8 ordinary lines. Full-size, collapse/restore and filters are retained. Long entries can wrap and occupy several lines. Labels are Auto-resolve Battle and AI: Finish Turn; their established confirmation/delegation behavior remains.

## Validation and limits

- Clean Unity 2018.3.12f1 compile and read-only base.unity load: return 0, no logged C# errors/exceptions (`Logs/tactical16-network-compile-and-scene.log`). Initial compilation encountered import ordering for the new source; a later restricted attempt waited on licensing. Neither is counted as a pass. The clean network-enabled run is the result above.
- Fresh private Win64 build completed with return 0 and no logged C#/shader errors (`Logs/tactical16-build.log`). Artifact: `Builds/Warband-Review-20261102-256f9809/Win64/VoreWar.exe`. The player assembly contains the new HUD/Attack methods.
- Scoped hashes confirm exactly the six intended existing sources changed and only TacticalHudView.cs/.meta were added. No missing paths or other Assets/Packages/ProjectSettings changes. Evidence: `Logs/tactical16-scope-review.json` and per-file source diffs. Prior user work is preserved. Existing license and dependency notices are included with the local build.
- No new tests/framework, scene/prefab rewrites, source-art edits, package upgrades or player launch. Graphical fit and actual combat behavior require the user's combined review.

## Combined player review

1. Start one battle with a unit equipped for both melee and ranged attacks. Press 1 and 2: each selects Attack. Adjacent targets use melee; distant targets use ranged within reach. Out-of-range/downed targets cannot be attacked. Confirm previews and right-click Attack agree.
2. Check the dark blue panels, weapon/two-spell readout, smaller traits and log with 8+ visible lines once enough messages exist. HP/AP, stats, names/species and command text should retain their previous size. Expand and restore the log.
3. Check AI: Finish Turn hands back control next turn, and Auto-resolve Battle opens the existing options/confirmation. The two redundant bottom controller labels should be absent.

## Source-change notices

Actual local License.txt sections4–5 reviewed. All7modified/added C# sources carry dated conversion notices; CHANGES.md records their exact paths and the new .meta association. Existing attribution and license grants remain. No external code, artwork or dependencies added. Existing asset/font/logo provenance and redistribution reviews in release-readiness.md remain unresolved release gates; private build checks do not certify distribution compliance.


## Player feedback correction — 2026-11-02

The first build's item names were plain text and empty em-dash placeholders were misread as multiple characters. The readout now uses four boxes, truly blank unequipped entries and source encoding marked with a UTF-8 BOM. Slot headings stay outside each box.

The first log resizing added 1920 to a width already supplied by stretch anchors. That produced a 3840 reference-unit panel and moved the right-edge Expand/Reduce controls offscreen. Compact layout now uses horizontal stretch with sizeDelta.x 0, height 260 and bottom 110; fullscreen uses full stretch/zero offsets. All return paths share the compact helper, and resize/filter changes request the existing next-frame latest-message scroll. No log events/filter defaults were removed or reset.

Clean C# compilation and read-only scene load completed with no logged errors and Unity's final return-code 0 message (`Logs/tactical16b-compile-and-scene.log`). The PowerShell Start-Process -Wait wrapper continued waiting after Unity exited and was later interrupted; its shell exit is not reported as 0. The subsequent standalone build used WaitForExit, completed actual exit 0, and has no logged C#/shader errors (`Logs/tactical16b-build.log`). Only TacticalHudView.cs and MessageLogPanel.cs changed in this follow-up; scoped hashes show no added/missing/other game files or settings.

An actual UI review was attempted with the Computer Use skill. The 1920×1080 launcher was readable through accessibility, but screenshot capture failed twice (“FrameArrived timed out” / “window capture timed out”), and clicking Play failed with “coordinate input geometry is unavailable”. No battle was entered; the review launcher was closed. This is not a visual or functional log test pass. No new test framework or temporary test sources were added.

Focused player review for this correction: four bordered slots/empty boxes; the spaced primary row and utility/AI groups; visible log messages plus Expand → Reduce → hide → Open Log. No need to repeat the already accepted setup portraits or unrelated menus. GPL dated notices/CHANGES checked for both sources; existing licensing/attribution and release gates remain unchanged.


2026-11-02 alignment correction: selector pivot now matches the top-left command origins; equipment boxes/details moved60reference units down to clear stats. Only TacticalHudView.cs changed; dated notice/CHANGES and retained attribution reviewed. Fresh build exited0 without logged C#/shader errors; no visual test claimed. New private artifact: `Builds/Warband-Review-20261102-df20ed48/Win64/VoreWar.exe`. Existing notices packaged; prior builds retained. Please confirm outline alignment for Attack/Move/Vore and equipment headings clear VOR/STM. Existing release provenance gates remain open.


## 2026-11-03: six resumed tactical UI corrections ready

Restored red HP fill and excluded sliders from generic control styling. Equipment headings/boxes moved halfway back up (318), with matching details clearance112. Tactical traits and innate spells use70%text; core stats/commands unchanged. Expanded-log toggles use the existing bright vector checkmarks with their original geometry. Compact log height257/bottom100 joins the bottom bar with no gap.

Fresh private build: `Builds/Warband-Review-20261103-1db4b742/Win64/VoreWar.exe`. Unity build exited0, no logged C#/shader errors/exceptions. Scoped hashes confirm exactly TacticalHudView.cs, MessageLogPanel.cs and InfoPanel.cs changed; no other game files/settings/additions/missing paths. No actual visual playtest claimed. Please confirm red HP bar, equipment placement/text readability, bright log checkmarks and clean log/bar join in one focused review. Previous accepted controls/setup portraits retained.

Dated notices and exact CHANGES paths checked, actual License4–5 reviewed, inherited attribution/licensing retained; existing7license/dependency notices copied to the unique artifact. No external assets/dependencies, deletion/move or publication. Existing release provenance gates remain open.


## 2026-11-03: solid HP, equal trait spacing and edge coverage

HP image now explicitly uses Simple rendering/filled center and clears sprite overrides, rather than inheriting sliced rendering. The trait block places its final newline before closing the70%size span so the last trait retains the same line metrics. Left inspector moves one reference pixel left, right commands one right; bottom bar gains two reference pixels, one per edge. No other layout, font sizes, artwork or gameplay changed.

Fresh private build: `Builds/Warband-Review-20261103-0551cd32/Win64/VoreWar.exe`. Unity build exited0 without logged C#/shader errors or exceptions. Scoped hashes show only TacticalHudView.cs and InfoPanel.cs changed; no other game files/settings/additions/missing paths. Dated notices and CHANGES exact paths reviewed against the diff, actual License4–5 read, inherited notices/licensing/attribution retained; seven existing license/change/dependency notices included. No outside additions, file deletion/move, publication or release approval. Existing provenance gates remain open.

No visual playtest claimed. Please check a filled red HP bar (including partial HP), equal trait spacing through the final trait, and covered left/bottom/right screen-edge seams. All prior accepted menus/portraits need no repeat review.


## 2026-11-03: HP fill occluded by barrier background — corrected

User confirms trait spacing and edge seams fixed; preserve them. Actual HP cause identified in prefab: BarrierBar overlays HealthBar at the same rectangle, with a transparent Background (alpha0). Generic background theme recoloring made that background opaque, masking the HP fill. TacticalHudView now skips slider-descendant images in that pass, preserving native status-bar transparency. This is the only source change, plus its dated notice; no scene/prefab changes or outside additions.

New private build: `Builds/Warband-Review-20261103-470fab48/Win64/VoreWar.exe`. Build exited0 with no logged C#/shader errors/exceptions; scoped hashes confirm only TacticalHudView.cs changed, no other game files/settings/additions/missing paths. Logs/tactical16f-build.log/scope.json/source diff record evidence. No actual visual playtest claimed. Only player check: full HP shows a solid red bar; taking damage shortens it normally, with barrier behavior retained. Previously accepted traits/edges do not need another review.

Actual local License4–5 reviewed, dated source notice and exact CHANGES path checked, inherited notices/licensing/attribution preserved; seven existing license/change/dependency notices included with unique artifact. Earlier builds retained, no deletion/move/publication; existing release provenance gates remain open.
