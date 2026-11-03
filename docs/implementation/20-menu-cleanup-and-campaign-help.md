# 20 — Menu cleanup and campaign Help

Authorized 2026-11-03: remove Continue and Map-only Preview from New Campaign; suppress the normal default-race-settings message; use Load Game on the main menu; make main-menu Help lead to the company campaign guide; unify Help colors/layout and reduce its oversized text.

Implemented source: eight files listed in CHANGES.md. New Campaign retains Create company, Load Game and Ironman. Only routine default-settings feedback is silenced; actual race-setting errors remain. Map-preview creation handlers retired, without deleting files or changing company creation's internal Preview state or save schemas.

One shared Help overlay now serves main-menu, company/setup/leader creator and tactical menu entry points. It uses the shared blue palette, a centered 1160x744 reference frame, fifteen compact chapter buttons at left, 18-point wrapped text at right, a visible scrollbar and Exit Help at bottom right. CanvasScaler Expand targets 1280x800 so bounded layout fits wide/4:3 windows. Native TMP font preserved. Existing twelve campaign chapters plus character stats, traits and spells retained; long generated references evaluated only when selected. Selection scrolls back to top. The underlying screen is left intact and Escape closes only Help; tactical input/AI is suspended while reading. Original scene child objects are retained inactive; no scene/prefab edits. Existing article wording remains except the requested Load Game caption in save guidance; full editorial rewrite is not part of this layout cleanup.

Verification: clean Unity compilation/read-only base-scene check and Windows build both exited 0, with no logged C#/shader errors or exceptions (`Logs/menu20-clean-scene.log`, `Logs/menu20-build.log`). An initial compile detected a campaign-property/global-type name collision; it was corrected and `menu20-scene.log` is not counted as a clean pass. Scope hashes prove exactly eight intended existing C# files changed, no added/missing game files or scene/prefab/art/package/settings changes. Eight dated source notices, exact CHANGES paths and all earlier header notices reviewed; `git diff --check` clean for the changed sources. Player assembly contains the shared chapter browser and no retired preview handlers. No new tests/framework or actual visual playtest claimed. Earlier save audit and accepted creator/combat UI were not retested. Task-start hashes/source diffs/scope/notice review and complete result: `Logs/menu20-*`.

One combined player review: main menu has Load Game and no routine default-race message; New Campaign shows neither Continue nor Map-only preview; Help opens the campaign guide with readable blue layout and scrollable long references; Exit Help/Escape returns to the original main menu, company/creator or tactical menu without changing the campaign.

Actual License.txt sections 4–5 reviewed; eight dated source notices/exact CHANGES paths and prior notices checked against the final source diff. No outside additions, file deletion/move or publication. Existing provenance/distribution gates remain open.

Private review build: `Builds/Warband-Review-20261103-edb0c0f0/Win64/VoreWar.exe`. Seven existing license/change/dependency notices packaged with it, earlier builds preserved. The user accepted the combined review on 2026-11-03; see acceptance below.


## Accepted — Spec20, 2026-11-03

The user explicitly accepts the menu cleanup and shared campaign Help: “Looks good. Accepted.” Preserve the accepted implementation and `Builds/Warband-Review-20261103-edb0c0f0/Win64/VoreWar.exe`. This is user acceptance, not an additional agent playtest. Documentation only for this acknowledgment; no source changes or new checks.
