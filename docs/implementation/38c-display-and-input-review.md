# Phase C display and input review

## Scope

The user accepted Phase B and authorized the grouped display/input milestone from spec 38. This review preserves the accepted blue/gold layout, existing wording, paper-doll rendering and gameplay. Developer Race Editor beautification and Phase D injury-mechanics validation are outside this packet.

## Review method and limits

A separate Unity 2018.3.12f1 editor runs the actual base scene and native UI on Direct3D 11. The editor-only DisplayInputAudit selects fixed Game View sizes, verifies actual Screen dimensions, renders the native canvases/world cameras to retained PNGs and records control geometry. Temporary render cameras and canvas presentation changes are restored in memory. The final driver also restores temporary Game View entries. No scene changes are part of this packet.

The five sizes are 1920×1080, 1280×720, 1024×768, 1280×1024 and 2560×1440. They form a review matrix rather than a promised new minimum resolution. A popup is also kept open while switching from 1080p to 1024×768 and back.

Physical Windows capture failed twice after a fresh game-window selection, with FrameArrived/window capture timeouts. Windows automation was stopped. The replacement captures are Unity-rendered fixtures. Native EventSystem raycasts and pointer/button handlers exercise hit testing and callbacks, but do not establish physical mouse/keyboard, desktop scaling, window dragging or player input timing. Those remain focused human observations.

Synthetic company/save data stays in a unique project Logs directory. The existing diagnostic save directory suppresses deletion. No personal company saves are loaded, deleted or changed. Existing user options/developer race settings are read by normal startup, and temporary slider values are restored without invoking Save and close.

## Demonstrated corrections

- Slider entry had a four-character limit despite offering 0.005 as a legal value. It now accepts sixteen characters. NaN and infinity are rejected before they can reach the slider. Finite values retain the existing clamping behavior.
- Hover positioning used 1920-based horizontal scaling and ignored the vertical screen edge. The panel now uses actual canvas/screen geometry with four-pixel edge clearance on both axes.
- A valid 100-character leader name with large debts clipped the roster HP/injury/debt summary. Row height is now measured from the rendered summary, with adaptive page size. Ordinary names retain the accepted five-row/82-unit layout and portrait positions. Only roster button labels use bounded fitting for unusually large debt amounts.
- Long save and leader names overflowed the list buttons. List captions now use an ellipsis when necessary. The original names and full campaign preview remain intact on hover and selection.
- With many traits, the tactical scroll area counted space covered by the compact combat log. Final traits and innate spells were hidden without sufficient scroll travel. The viewport now ends above the visible compact log, expands when it is hidden and retains its compact reservation while the full-screen log is open. Fonts and the accepted equipment/stat layout are preserved.

## Coverage record

| Area | Native rendered coverage |
| --- | --- |
| Main menu | All five sizes |
| Options | Top/bottom scroll positions, sliders, value entry and edge tooltip |
| Content Settings | Every visible page at each size |
| Single Battle setup | Both sides, paper dolls and open species dropdown |
| Leader creator | Existing controls and central portrait |
| Campaign | Map, footer, persistent navigation, roster, recruitment, merchant, contract lists/offers, records and Help |
| Save / Load | Ordinary and maximum-length captions/previews, full list/pagination |
| Tactical | Selected native actor, four-stat/equipment panel, command bar, compact/expanded log and Escape overlay |
| Single Battle outcome | Native summary strings with accepted title/side hierarchy |
| Stress | 100-character leader and 64-character save names, large finances, 13-member roster pagination, eight injury descriptions across pages, empty recruit/save lists, 19 traits, popup resize and edge placement |

Geometry is a diagnostic lead. Hidden legacy controls and world-space level labels can legitimately lie outside the screen. Raw preferred-size or bounds flags are not automatically product defects.

## Evidence and validation

The rendered baseline is `Logs/display38c-b15ecc0997134573a6cd48d626a0e93c/report.json`, with 145 frames. Baseline examples at 1024×768 show the roster clipping and overflowing save captions. Earlier preliminary captures that opened the legacy Content presentation or left the tactical actor unselected were corrected fixture errors. Validation 1 failed on an editor/internal-field access and validation 2 exposed a disabled tooltip ancestor in the fixture.

Validation 3 completed its matrix with 151 frames and 257 check/observation entries. Stress 1 demonstrated the tactical trait/log overlap. Stress 2 visibly confirmed the viewport correction, but its assertion mistakenly reset scrolling before checking the final position. Final 1 then reached 169 frames before an overly strict assertion rejected the taller 4:3 layout where every stressed trait already fits. Final 2 incorrectly required additional space on hiding the log even when the inherited inset already cleared it. These assertion errors were corrected without additional production changes. Earlier runs also passed custom-only indices to Unity's Game View cleanup API, which expects combined built-in/custom indices. The final driver uses the correct indices. Failed and preliminary logs remain retained and are excluded from final pass claims.

The full Final 3 run passed with actual exit 0, 187 rendered frames and 306 assertions. Its report is `Logs/display38c-aec2a8b4fe5d42e5b3f6af80cc4d9196/report.json`. Assertions cover actual resolutions, slider hit targeting, row labels outside sliders, focus, minimum-value precision, invalid/non-finite rejection, unchanged other sliders, clamping, modal blocking and one-Cancel dismissal. Navigation, full save previews, list pagination, roster summary fit, injury-page access and native trait scrolling also pass. Empty Save / Load is rendered at the last matrix size, and populated/paginated save lists are exercised at all five sizes.

Screenshot comparison then demonstrated stale extra trait-panel clearance immediately after switching aspect ratio, before any log action. The final production change refits after both canvases resize. The focused resize run passed with actual exit 0, 30 rendered frames and 24 assertions across all five sizes. It verifies that the initial resized viewport is already correct before any log action, and that final entries can be reached. Report `Logs/display38c-13c12834acfc46cea0268740edd1d0fe/report.json` and log `Logs/display38c-resize1.log` retain the evidence. This follow-up changes the log's resize refresh only. Earlier normal-screen findings and corrections remain covered by the full matrix.

Both grouped native regression runs passed 963 assertions with actual exit 0, including the final-source repeat in `Logs/display38c-regression2.log` and `Logs/display38c-runtime-result.json`.

The private Windows player built successfully with actual exit 0 at `Builds/Warband-Review-20261105-512e67a8/Win64/VoreWar.exe`. Compiler review found the same 42 warning kinds as the preceding economy build, zero new warnings after source-line normalization and zero C#/shader errors. `Logs/display38c-compiler-review.json` records the comparison. The full matrix and focused resize logs have no exception records or invalid Game View cleanup indices.

Visually inspected examples include the corrected 1024×768 roster/save preview and injury page, the 1080p edge tooltip and crowded trait-list bottom, and the resized 1024×768 tactical view. All matrix captures are retained for further inspection. The raw scenes contain pre-existing world-space sample labels/effects in these fixtures, so the native battlefield images are presentation checks rather than a recording of a played battle.

The remaining grouped player check is physical slider hover/right-click/manual entry and cancellation, tactical trait mouse-wheel scrolling with the log visible/hidden, and resizing the standalone window. Desktop capture failure prevents claiming these observations from native callbacks alone. Broad Help prose, narrative work and focused injury-mechanics validation remain deferred.

## Existing finding

Normal startup displayed a developer race-settings load warning in the baseline, before these production edits. This packet does not hide the warning or overwrite developer data. The cause has not been established. Race Editor metadata investigation remains separate if the warning also appears in the user's normal build.

## Licensing and handoff

Eight changed/added C# sources have dated conversion notices. CHANGES identifies the exact source paths and the editor driver/meta companion. Existing legal notices and License.txt are retained. Source notice coverage, earlier header retention and the unchanged License hash were checked against the packet snapshots. No outside code, images, libraries or dependencies are introduced. Final source hashes/diff are recorded in `Logs/display38c-source-manifest.json` and `Logs/display38c-packet.diff`. Nine preceding Phase B source hashes outside this packet are unchanged. No scene/prefab/package settings changes or file deletion/moves are part of this work. Existing public-distribution asset-provenance and Corresponding Source gates remain open.

Seven existing license/change/dependency notices are packaged beside the private player and verified in `Logs/display38c-notices.json`. This source/notice check does not establish every obligation for a future public distribution.
