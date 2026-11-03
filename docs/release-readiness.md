# Release readiness

Updated 2026-11-02 for spec 12. **No release candidate, distribution approval or publication is declared.** The combined company player review and subsequent payday display are user-accepted. See the [implementation and coverage report](implementation/12-integrated-review-and-help.md).

## Player acceptance and follow-up (2026-11-02)

The user reports the combined review good with no material issues. This accepts that milestone, without establishing unreported specialist coverage or authorizing publication. Help needs a later concise player-facing rewrite; it is explicitly deferred. A world-map payday countdown was subsequently added in source only (diff reviewed, no new compile/build/playtest). The user subsequently confirmed the payday label looks good. The private Win64 artifact described below predates that label. The remaining gates still apply.

## Spec13 follow-up (2026-11-02)

Approved travel events and per-level XP display are implemented: [phase13 report](implementation/13-road-events-and-xp.md). Unity compile/read-only scene and existing32managed regression checks passed; the user subsequently reported the slice basically good and accepted it with new slug encounter budgets reduced30%. That constant-only follow-up was source-reviewed, without an additional compile/playtest as recorded in the phase report. The older Win64 artifact predates both payday and these events; no current player rebuild is claimed. No new dependencies/assets or release approval. The full help rewrite remains deferred.

## Spec14 interface follow-up (2026-11-02)

The user-defined campaign layout is implemented: [interface report](implementation/14-campaign-interface.md). Final editor compile/read-only scene and existing32managed regression checks passed; actual runtime layout/navigation and the new rest eligibility await one combined UI review. No current Windows player rebuild or multi-resolution visual pass. Help prose rewriting remains deferred; no distribution approval.

## Spec14 feedback revision (2026-11-02)

[14B](implementation/14b-centered-map-and-options.md) revises the unaccepted initial layout: bounded centered frame, top Contracts/Records, lower contract strip, normal Options and a pooled draggable map. User-specified new regions are80x60with six towns. Final compile/scene, existing32managed checks and actual map generation/save/resume inspection passed. The user subsequently accepted the functional layout with no material placement critique; this does not identify every individual drag/Options/aspect case they exercised. A common design for campaign/Options/content/tactical UI is now the planned precursor to deferred beautification. No new player build or publication approval.

## Options-first shared UI (2026-11-02)

[15A](implementation/15a-options-redesign.md) introduces a restrained blue, compact categorized normal Options presentation. Compile/read-only scene and existing32managed regression checks passed; the user subsequently found the layout mostly okay. Slider alignment, larger hover text and vore terminology corrections passed a later compile/scene check, with focused visual confirmation pending. No unreported preference/visual case is inferred as passed. Existing preference storage and maintenance callback bodies remain. Other screens and Help prose remain later work. No current player build or distribution approval.

## Evidence in this checkout

- Engine: Unity 2018.3.12f1 (`ProjectSettings/ProjectVersion.txt`). Current build settings enable `Assets/base.unity`. Packages and build configuration were not upgraded.
- Current editor compilation and read-only scene checks pass. Synthetic production-code integration checks cover company data/settlement/save behavior. Prior player confirmations cover the recorded staged workflows and spec 11 feedback. A current Win64 player build also passed the existing MultiBuild procedure, followed by a headless startup check. These do not prove graphical player gameplay or every tactical case.
- Root `License.txt`, original attribution, dependency notices and dated source modification notices are retained; copies of the root license/change record and inspected dependency notices also accompany the private Win64 review build; `CHANGES.md` identifies exact completed fork changes. No external code/assets/dependencies were introduced in spec 12.
- Current UI help explains the company loop, supported checkpoints, Ironman limits and project/license information. It does not replace delivering full license texts and other required notices with a future distribution.

## Known functional boundaries

- New company saves only; no legacy empire-save import. Early checkpoints affected by the earlier omitted-HP defect are not repaired, per user direction. Use fresh or confirmed healthy company state for review.
- Company tactical saving resumes the prebattle deployment checkpoint, not a live tactical turn. Results and revival transitions save separately. Ironman shares this boundary and resumes only the latest valid generation through the interface.
- Normal history and corruption recovery retain immutable checkpoint files under the no-delete workflow. Storage rotation/pruning is not implemented. A32 MiBpayload guard is explicit; long-run growth and high-roster behavior are not calibrated.
- Active/reserve deployment has no new fixed cap. The intended company is usually small; large forces/performance require live review.
- Two proof-of-concept travel events are user-accepted with lighter slug encounter tuning. Further events, extra points of interest, additional species and user-authored narrative remain later work. Only the currently curated species generate. Existing terrain artwork is retained.
- Numeric balance remains provisional. Specialist live combat and visual failure-recovery cases are listed as pending in the integration report, not passed by source inspection.

## Gates before any release

1. The combined player review is accepted. Resolve any newly demonstrated gameplay/save/UI regressions. Exercise the deferred fatal-fate, captive/retreat/draw, summon/copy, manual/turbo and skirmish-isolation paths before treating the combat integration as release-tested.
2. The Win64 build and headless startup check passed in this phase using the recorded Unity version and existing build procedure. Complete graphical startup/gameplay and packaged-data checks before release, including assets, fonts, plugins, platform-specific bundles and required data. Other target platforms are untested; this check does not authorize a public package.
3. Resolve the inherited asset/dependency provenance issues below and decide what is actually being conveyed. No file is assumed freely redistributable merely because Unity imported it.
4. Recheck the applicable actual GPL terms for that distribution. Preserve license/copyright/attribution/additional-term/warranty notices and prominent modification notices/dates. Review interactive Appropriate Legal Notices under 5(d), including its stated exception; do not assume a help paragraph alone settles it. For binaries, select and satisfy an applicable section 6 Corresponding Source method, required build/support material and Installation Information where applicable. No distribution method is selected here. The conversion currently includes uncommitted changes and untracked source additions: a future source delivery must contain the exact working tree used for the binary, not just a link to an older remote revision. No automatic commit or push was made.
5. Obtain the user's explicit authorization for publication/upload/distribution. No credential, remote, hosting or release service was changed by spec 12.

## Inherited notice inventory and unresolved provenance

| Material | Evidence inspected | Status before packaging |
| --- | --- | --- |
| Covered VoreWar fork source | Root License.txt (GNU GPLv3); README project attribution; CHANGES; per-file modification notices | Preserved. Distribution-specific obligations remain to be checked for the actual package/method. |
| Odin Serializer binaries/source context | `Assets/Plugins/Sirenix/Odin Serializer/License.txt` identifies Apache License 2.0; plugin folder includes JIT/AOT/EditorOnly variants | Notice retained, no plugin changes. Determine shipped variants and preserve applicable notices/terms in the chosen package. |
| Newtonsoft.Json 13.0.3 | `Assets/Plugins/Newtonsoft.Json.13.0.3/LICENSE.md` identifies MIT and James Newton-King attribution | Retained, no dependency changes; include relevant notice if conveyed. |
| Packaged Unity/GitHub tools | `Assets/package/LICENSE.md`, `Assets/package/Third Party Notices.md`, `Assets/package/com.unity.editor.tasks/LICENSE.md` | Existing notices retained. Determine whether these editor/tools components are conveyed; review all applicable third-party notices for that package. |
| Unity packages/platform runtime | Versions in `Packages/manifest.json`; installed engine/package contents | Not upgraded or newly introduced. Actual player/binary package terms and included notices need target-specific review. |
| Artwork, sprite sheets and bundles | Existing resource inventories/retirement ownership records; retained hashes and source graphics attribution | Asset-by-asset rights/notice mapping is incomplete. Source inclusion and a passing editor do not establish the right to redistribute every sheet/bundle. Resolve contributor provenance and any separate terms before release. |
| User-supplied Vore Warband menu logo | Assets/Resources/UI/VoreWarbandLogo.png, supplied by the user on2026-11-02; original PNG copied unchanged,2172×724RGBA, SHA256 a8fbea4f27b0e83912bab56aa34dcc4497e74607ad0d4b8433adde68ae91e3f9; no embedded author/license metadata | Integration authorized by the user. Creator/source, applicable redistribution terms and any required attribution remain unverified before packaging/publication. No GPL grant or copyright ownership inferred. |
| Existing TMP/font assets | Current text materials/font references preserved; installed TMP package inspected for API compatibility | No standalone font-license notice was found by the filename inventory in the inspected TMP asset folder. Verify the font origin and required notice supplied with the selected packaged font; absence from that inventory is not a conclusion that it is unlicensed. |

This is a concrete readiness record, not a claim of complete GPL compliance or a new license grant for separately licensed material. Inherited provenance remains unresolved; the newly supplied menu logo also requires its own provenance/terms record before distribution. No license grant is invented by its inclusion.

## Current local player artifact

`Builds/MVP-Review-20261102-e350dc13/Win64/VoreWar.exe` was built without overwriting earlier builds. `Logs/mvp-review-build-invocation.json` records the exact existing MultiBuild method/arguments; `Logs/mvp-review-build-result.json` records the executable hash, copied legal notices and headless startup result. Project settings remain identical to their pre-build snapshot. The hidden player test was stopped after checking its log; no visual capture or gameplay was attempted. Public source delivery, complete provenance and other distribution obligations remain unselected/unresolved, so the notice-bearing private build is not a distribution certification.


## Current standalone testing artifact (2026-11-02, after adaptive portraits)

User requested normal standalone play. Built current working source with existing MultiBuild.Builder.BuildCommandLine into unique `Builds/Warband-Review-20261102-63c65e66/Win64/VoreWar.exe`; prior artifact retained. Build completed return0, no logged C#/shader errors. Managed assembly/streaming bundle present; scoped hashes show no Assets/Packages/ProjectSettings changes. Logs/standalone-current-build.json, standalone-current-build.log and standalone-current-build-result.json record invocation/results/hashes. Engine executable stub hash may match earlier builds; the managed assembly carries the current gameplay/UI code.

Project-local Play Vore Warband (1600x900).lnk launches windowed1600×900 with the correct working folder. License.txt/CHANGES and retained dependency notices were copied into the new folder, with hashes recorded. No source change, publishing/upload, old-output overwrite or file deletion. No actual player launch/startup/gameplay/graphical validation performed by the agent; build success is not a visual pass or distribution certification. Existing source-correspondence, logo/artwork/font provenance and distribution-method gates remain. Comparison optionD raw-PNG loading is Editor-only; player reports missing raw layers, while normal rendering uses shipped bundles.


Latest local testing build: `Builds/Warband-Review-20261102-f5d988ca/Win64/VoreWar.exe`, with1600×900shortcut. Supersedes63c65e66forcurrentUI: removesPortraitComparisonentry andshowscentered2xSingleBattle samples withoutspeciescaptions. ExistingMultiBuildWindowsbuildcompletedreturn0/noC#orshadererrors; Logs/preview2x-source-build-review.json recordsassemblyhash/scope. Legalnoticescopied, oldbuildsretained, noagentplayerlaunch orvisualtest. Privateartifactonly; distribution/provenancegatesunchanged.


Current private review artifact: `Builds/Warband-Review-20261102-3679f353/Win64/VoreWar.exe`, enlarged Single Battle cards. Buildreturn0/noC#orshadererrors; scope/assemblyhash in Logs/preview-box-source-build-review.json. Existing notices copied; earlier artifacts retained. No actual player launch/visual acceptance or release certification.


Latest local testing build: `Builds/Warband-Review-20261102-15c68eeb/Win64/VoreWar.exe`, Single Battle sample cards nudged down. Buildreturn0/noC#orshadererrors; scope/assemblyhash in Logs/preview-nudge-source-build-review.json. Notices included, earlier builds retained. No agentplayerlaunch/visualacceptance or releasecertification.


Latest private review artifact: `Builds/Warband-Review-20261102-d70f6b12/Win64/VoreWar.exe`, moving the sample unit upward relative to its unchanged box. Buildreturn0/noC#orshadererrors; scope/assemblyhash in Logs/preview-feet-source-build-review.json. Notices included, prior builds retained. No agentplayerlaunch orvisualclearance validation; releasegatesunchanged.


## Tactical battle interface review build (2026-11-02)

[Spec 16](implementation/16-tactical-interface.md) implements the blue tactical HUD, distance-selected Attack on both 1/2 hotkeys and context menu, four weapon/spell readouts, a larger compact log and smaller traits. Core command/stat typography and accepted setup portraits remain. Clean Unity 2018.3.12f1 compilation/read-only scene and fresh Win64 build passed return 0, without logged C#/shader errors. Scope review confirms only the intended seven C# sources and new .meta association; no other game files/settings or missing paths. No player launch or visual/combat verification is claimed.

Latest private review artifact: `Builds/Warband-Review-20261102-67891352/Win64/VoreWar.exe`, with an optional 1600×900 shortcut. Earlier builds remain intact. Existing license/change/dependency notices included; source notices and CHANGES.md checked against the diff. No external code/assets/dependencies introduced. Public redistribution, provenance and source-delivery reviews remain separate open gates.


## Tactical feedback correction build (2026-11-02)

Latest private review artifact: `Builds/Warband-Review-20261102-256f9809/Win64/VoreWar.exe`. Four bordered/blank equipment slots, spaced primary/utility/AI command groups and corrected stretch-based log width replace the first tactical UI presentation. Existing build method completed exit0 without logged C#/shader errors; task scope contains exactly two intended C# source changes, no other game files/settings/additions/missing paths. Logs/tactical16b-build-result.json records the artifact/notices and scope.

Direct UI inspection was attempted: the launcher was accessible but screenshot capture timed out twice, and Play could not be clicked because input geometry was unavailable. The launcher was closed without entering a battle; visual/log behavior remains for player confirmation. No player data or option settings were changed. Dated source/CHANGES records and existing attribution/licensing retained; no external additions or release approval. Existing provenance/distribution gates remain open.


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


## HANDOFF 2026-11-03 — Spec17 shared popups and Esc overlay

HP accepted. Shared confirmations/messages/input/multi-choice boxes, hover/slider tooltips, generated save/load panels and auto-resolve options now use the blue palette. Dialog callbacks/content/font sizes retained; dimmer alpha and slider graphics preserved. Esc menu is a centered540x600blue panel over a dimmed game, with seven spaced native buttons; Race Editor entry hidden and Help renamed. Developer editor itself and gate code untouched. Original menu canvas contains no Options/SaveLoad/RaceEditor subtrees (scene ancestry checked), so their layouts are unaffected.

Fresh private Windows artifact: `Builds/Warband-Review-20261103-ee41877e/Win64/VoreWar.exe`. Unity compile/read-only base scene and Windows build both exited0 without logged C#/shader errors/exceptions. Logs/popup17-scene.log, popup17-build.log and popup17-result.json; task-start/post-build hashes show exactly nine intended existing sources changed, no other game files/settings/additions/missing paths. Source diffs and dated notice/changelog/prior-notice checks recorded. No GUI/actual interaction playtest claimed; previous screenshot helper failed, so no repeated attempt or new test framework.

Seven retained license/change/dependency notices included in the unique artifact; prior builds retained. Actual local License4–5 reviewed, exact source paths/dated notices and inherited licensing/attribution preserved. No external additions, scene/prefab edits, deletion/move or publication. Existing release provenance/distribution gates remain open.

One focused player review: Esc overlay/navigation/close and Help label/Race Editor absence; end-turn and Load saved confirmations blue; Options slider hover plus right-click number-entry popup readable; auto-resolve and ordinary messages use blue. Preserve accepted HP/traits/edges/portraits, no repeated full battle/menu tour. Help content rewrite remains deferred.


## HANDOFF 2026-11-03: slider hover / value-entry correction

Only SliderRevealer.cs and HoveringTooltip.cs changed: replace stale hover state with cached current UI raycast ownership plus slider rectangle check; right-button down edge opens one dialog only when ActiveInputfalse; tooltip graphics no longer intercept raycasts. Revealers are Handle Slide Area children, so event-only click handlers were discarded in source review in favor of a press-edge check compatible with Slider pointer handling. Preserve accepted popup/Esc colors and all other UI/gameplay.

Final private artifact: `Builds/Warband-Review-20261103-2171adf4/Win64/VoreWar.exe`. Logs/slider17b-review-build.log completed actual exit0 with no logged C#/shader errors or exceptions; player assembly verified to contain PointerInsideSlider. Two earlier internal artifacts preceded the final bounds/press-edge implementation and are NOT the handed-off build. Task-start hashes/source diffs/dated notices/CHANGES paths and inherited notices checked; only the two intended sources, no added/missing/other game files/settings. Logs/slider17b-scope.json/result.json record evidence. No actual GUI/playtest claimed and no new tests/framework.

Existing seven license/change/dependency notices packaged in the unique artifact, old builds retained. Actual License4–5 read; dated source notices and exact CHANGES paths reviewed, licensing/attribution preserved. No outside additions/deletion/move/publication; existing release provenance gates remain open. README points to this final build.

One focused player check in Options and Content Settings: outside sliders no slider-value tooltip; hover/right-click one slider opens only its dialog; cancel once dismisses it; holding right button does not stack entries; typing a value affects only that slider. No need to repeat wider accepted menu/battle/portrait checks.


## HANDOFF 2026-11-03: popup input contrast

Only InputBox.cs changed: after shared popup styling, text and caret are black, placeholder opaque#808080. Surrounding white dialog text/blue palette and all slider value-entry behavior are unchanged. Dated notice/CHANGES exact path and actual License4–5 reviewed, original notices/licensing/attribution preserved. No outside additions or release approval; existing provenance gates remain open.

Fresh private artifact: `Builds/Warband-Review-20261103-d62b53b1/Win64/VoreWar.exe`. Unity Windows build exited0 without logged C#/shader errors/exceptions. Source hashes/diff confirm only InputBox.cs changed, no other game files/settings/additions/missing paths. Seven existing license/change/dependency notices packaged; earlier artifacts retained. Logs/input17c-build.log, scope.json/result.json record evidence. No visual playtest claimed. Focused user check: empty field shows medium-grey Enter Text placeholder; typed numbers are black, with readable black caret. No repeated menu or slider targeting test needed.


## HANDOFF 2026-11-03 — Spec18 leader creator redesign

New blue one-screen creator: approved species dropdown, existing gender/five pronoun fields/quantification on left, existing focus buttons on right, all41native appearance rows distributed into compact adaptive left/right columns with unchanged labels/callbacks/visibility rules. Center paper doll is2x in a440x464clipped card compensating renderer offset. Name/confirm/cancel stay at center bottom, global Main menu/Help/Options footer remains uncovered. Legacy clone chrome/empty parents inactive; no original control files or art deleted. Only obsolete scroll instruction removed; no option wording changes. Optional company-only layout refresh runs after native customization updates; all original UnitCustomizer logic otherwise byte-identical after removing notice/hook. Source inventory confirmed all41CreateNewButton labels retained.

Fresh private review artifact: `Builds/Warband-Review-20261103-eda57af8/Win64/VoreWar.exe`. Clean Unity compile/read-only scene and final Windows build exited0 with no logged C#/shader errors or exceptions. Initial creator18-scene.log had the known new-source import-order CS0246 and is NOT counted as a clean pass; creator18-clean-scene.log is clean. Earlier internal build preceded final dropdown-template color styling; handed-off artifact is creator18-final-build.json/log. Player assembly includes CompanyLeaderCreatorView. Task-start/post-build scope: only CompanyManagementUI, UnitCustomizer, CustomizerPanel modified; CompanyLeaderCreatorView.cs/.meta added; no other game files/settings/additions/missing paths. Source diffs/inventory/scope Logs/creator18-*.

No actual visual/interaction playtest claimed; no new test framework or repeat failed Windows-capture attempt. One combined creator review remains: select each species/gender and focus; all available original controls fit without scrolling and reflow; paper doll contained/centered; dropdowns and pronouns/name editable; Cancel and Confirm function, chosen appearance/name/focus persist into campaign. No unrelated accepted UI/battle/portrait tests needed.

Actual local License4–5 read; all four C# dated notices, new-source attribution and exact CHANGES source/meta paths checked, inherited notices/licensing/attribution preserved. Seven existing license/change/dependency notices packaged in unique artifact; prior builds retained. No outside assets/dependencies, source art/import/scene/prefab changes, file deletion/move or publication. Existing release provenance/distribution gates remain open. README points to final artifact.


## HANDOFF 2026-11-03 — Spec19 named saves and exact battle restoration

Leader creator accepted. Save/load milestone implemented: unified blue named slot browser from map/main/battle; five-stat previews; manual overwrite and confirmed runtime deletion; map/day/committed-action autosaves, settled side-turn battle autosaves, latest+3 valid backups; exact native battle restore without NewTurn/species-trait regeneration. Ironman latest-valid/autosaves-only, normal and Ironman corruption fallback, durable fallen marker before removal on defeat. Original World serialization/public checkpoint list retired, serialized scene callbacks retained as forwarders. Single Battle remains nonpersistent, can load campaigns. Broad help rewrite deferred; obsolete saving guidance updated. User runtime file-deletion authorization recorded in AGENTS; agent filesystem deletion remains forbidden.

Final real Unity Play-mode synthetic audit passed34, exit0: Logs/save19-runtime-review.log, runtime-result.json. Canonical company/enemy actors, reserves/downed/containment, summons, AP/HP/tempHP/barrier/mana/cooldowns/temporary boosts, exact acquired traits, status/tile effects, log, RNGs, enemy-turn resume, slot-scoped/ironman corruption, manual/olderIronman rejection, retention/deletion plans and defeat marker validated. Deletion suppression preserved ALL probe files under unique Logs roots; no personal saves accessed. Earlier fixture-assumption failures retained, superseded by final34pass. Clean compile/read-only scene exit0 Logs/save19-compile3.log. No actual graphical/interaction playtest claimed.

Final private artifact: `Builds/Warband-Review-20261103-1710002a/Win64/VoreWar.exe`. Logs/save19-final-build.log actualexit0 with no logged C#/shader errors/exceptions; result.json records assembly markers and editor diagnostics absent from player. Seven existing notices packaged. Task-start/final hashes:17intendedexistingC#changed +4newC#/4meta, no other gamefiles/settings/scene/art/package changes or missing paths. 21source dated notices/exactCHANGESsource/metapaths and meaningful prior notices checked; actualLicense4–5 reviewed, inherited attribution/licensing retained, no outside additions. Existing distribution/provenance gates remain open. Earlier builds preserved; no agent deletion/move/publication. README points to final build.

NEXT: wait for one combined normal named-save/preview/disposable-slot-delete, exact midbattle resume/autosave and disposableIronmanUI/loss review. Do not start another phase or request repeated accepted menu/portrait tours. If interrupted, read spec19 and Logs/save19-result.json; do not repeat passed34checks unless code changes/failures justify it.


## Accepted — 2026-11-03

The user reports that manual test results look good. Spec19 Save / Load is accepted with no issues reported. This records user feedback, not a new agent playtest or a claim that every optional scenario was individually exercised. Preserve the accepted implementation and build. No source changes or new checks were made for this acknowledgment.

Acceptance does not close the inherited provenance or distribution gates.


## HANDOFF 2026-11-03 — Spec20 menu cleanup and shared campaign Help

New Campaign no longer creates Continue or Map-only preview buttons; their unused handlers and obsolete instruction retired. Load Game retained beside Create company; Ironman setup preserved. Main-menu load label and associated save guidance now Load Game. Routine default-race-settings message empty; actual load errors/fallback behavior unchanged.

HelpPanel is one shared blue campaign guide from main menu, company/setup/creator and tactical menu: 1160x744 centered reference frame, 15 compact chapter buttons, 18-point native TMP text, wrapped/clipped scroll area with visible scrollbar, Exit Help. Existing twelve campaign chapters and stats/traits/spells references retained; long references evaluated lazily. Underlying UI preserved; GM routes Escape only to Help and suspends tactical input/AI while open. Original help child objects retained inactive, no scene/prefab rewrite. Full editorial Help rewrite still separate; article text preserved except requested Load Game guidance label.

Private build: `Builds/Warband-Review-20261103-edb0c0f0/Win64/VoreWar.exe`. Clean Unity compile/read-only scene and Windows build exited 0 with no logged C#/shader errors/exceptions. Initial name-collision compile failure corrected; only menu20-clean-scene.log counts as clean compilation. Logs/menu20-result.json, scope.json, notice-review.json and source diffs record evidence. Scope: exactly eight intended existing C# files, no other game files/settings/art/scenes/prefabs/packages/additions/missing paths. No new test framework or repeat save audit; no actual visual/interaction playtest claimed.

Actual local License.txt sections 4–5 reviewed; eight dated notices/exact CHANGES paths/prior notices checked. Seven retained license/change/dependency notices packaged; no outside code/assets/dependencies. Existing provenance/distribution gates remain open. All earlier builds/user work preserved; no deletion/move/personal-save access/publication. README points to this build.

NEXT: one combined user review of main-menu Load Game/no routine settings message, New Campaign button cleanup, and shared Help layout/chapter scrolling/Exit Help/Escape return from menu/company/creator/battle. Wait for feedback before another phase; do not repeat accepted save/portrait/combat UI tours.


## Accepted — Spec20, 2026-11-03

The user explicitly accepts the menu cleanup and shared campaign Help: “Looks good. Accepted.” Preserve the accepted implementation and `Builds/Warband-Review-20261103-edb0c0f0/Win64/VoreWar.exe`. This is user acceptance, not an additional agent playtest. Documentation only for this acknowledgment; no source changes or new checks.
