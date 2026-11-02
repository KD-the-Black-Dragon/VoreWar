# 15A — Categorized normal Options

2026-11-02. User-authorized implementation based on the attached current Options screenshot: a restrained dark-to-moderate blue theme, smaller readable text, one centered scroll column, coherent categories and four explicit control retirements. Campaign and Maintenance were additionally approved in the user's follow-up. The user reviewed this layout as mostly okay and requested the slider/hover/terminology corrections recorded below; one focused follow-up remains. Beautification is deferred.

## Presentation and retained behavior

Normal Options now uses a centered960x760panel within a scalable1280x800reference canvas. It leaves outer padding at wide/4:3/custom window proportions rather than stretching two lists across the screen. One vertical viewport has a scrollbar, section headers and consistently spaced rows; each row contains one option with its label and aligned control. The long auto-advance caption is now a contained When only one side can move row, with the existing dropdown choices unchanged.

The blue foundation is in UITheme.cs: background#0C1A2E, panel#142B48, rows#193353, section#1E4269, controls#234870, border#41648C, light text#E7F0FB and accent#80B3E7. Main labels/captions use17logical points, slider values16, tooltips19(after feedback; initially15), category titles21 and the screen title28. Thin outlines distinguish panels/controls. This is a first reviewable style, not a claim that the user has approved every exact color/size. Existing builtin font/sprites are reused; no image assets or dependencies added.

| Category | Included settings |
| --- | --- |
| Move Delays | Player, friendly AI and enemy AI movement, AI attack and vore delays. |
| Audio | Master sound, combat/vore/digestion volume and continuous digestion sounds. Volume controls retain their sound-enabled dependency. |
| Graphics | Damage/level/side markers, extra information, existing simplified terrain/color choices, banner size and unit-preview/base-stat visibility. |
| Controls | Contained auto-advance dropdown, existing camera tracking and scrolling preferences, turn confirmation/review, AI turn completion and background running. |
| Campaign | Leader revival only when an editable active company/battle context exists. Hidden without a company, preserving14B's context guards and company checkpoint storage. |
| Maintenance | Existing namelist reload/reset, user-data reset and saved-preference reset, grouped last; existing confirmation callbacks retained. No maintenance operation was invoked by the agent. |

Removed from the visible Options page exactly as requested: Strategic Move Delay, Pop-up Notifications, Ignore battles that only involve monsters and AI-vs-AI battle viewing. Their serialized references and historical preference/config values are retained; this UI task does not reset stored values or independently alter tactical/legacy runtime semantics. Other controls not named for removal, including strategic camera tracking and simplified graphics preferences, remain. Further retirement requires separate direction.

Thirty-two retained original controls are reparented and styled, not replaced with separate copies of their settings. Slider ranges/values/step rules, dropdown options/ordinals, checkbox behavior and preference keys remain. Slider tracks/handles, dropdown captions/templates/items/scrollbars and checkbox sizes are normalized to their new rows. Full-row checkbox clicking is supported while keyboard focus remains on the original control. Original help tooltips are displayed in the footer. Save and close calls the existing normal Options save/return path.

The existing campaign checkbox can be added after an earlier main-menu opening; its category reflows without a stale hidden campaign. It continues using the company checkpoint/battle deployment fallback, never global PlayerPrefs. Campaign/town return behavior from14B remains in place.

## Exact source and legal record

- Modified Assets/Scripts/Subwindows/Options.cs for the presentation lifecycle, compact live-value refresh and dynamic campaign row integration. Existing GetValues, SetNewValues, LoadFromStored, CloseAndSave and maintenance callback bodies are preserved.
- Added Assets/Scripts/Subwindows/OptionsView.cs and matching.meta for the single-column presentation, retained control routing and categories.
- Added Assets/Scripts/UI/UITheme.cs and matching.meta for reusable blue colors, typography and control helpers. Other campaign/content/tactical screens are not recolored by this pass; the shared helpers establish a foundation for their later redesign.
- Each of the three C#sources has a dated conversion notice, and the exact paths/meta associations are in root CHANGES. Actual License.txt sections4–5 reviewed; earlier notices/licensing/attribution preserved. No external copied source, art/font files or dependencies. Inherited provenance and distribution-method reviews remain open before distribution.

## Performed checks and limits

Unity2018.3.12f1 compilation/read-only base-scene inspection passed, return0: Logs/options15-final-compile-and-scene.log (initial implementation also passed). Inspected scene references and72terrain sprites remain intact; no new normalized compiler warnings. No serialized scene/prefab/art/package/ProjectSettings change was needed.

The existing32managed production-code regression checks passed with project-local output redirected to Logs/options15-*. No new test cases/framework or project test files. These checks cover earlier data/persistence/mechanics, not the new Options presentation or actual preference UI interactions. Scope, control coverage, unchanged storage/callback bodies, notices and resource correspondence are recorded in Logs/options15-source-review.json. Task-start snapshots: options15-start-hashes.json and options15-Options.cs-before.txt.

No Play-mode visual, dropdown-over-scrollview, resolution or live preference-persistence pass was performed. No maintenance reset or personal preference write was invoked by the agent; existing synthetic probes stay under ignored Logs/options15-checkpoints. No new Windows build, path deletion/move, commit/push or publication. The older private executable predates this UI. Broader content/tactical redesign and Help prose remain later work.

## One focused player review

Use normal Options in the current Unity project.

1. Scroll the full column and check the blue theme, smaller text, headers/scrollbar and four removals. Inspect a dropdown, especially When only one side can move: choices should be readable and clickable without covering unrelated labels. If convenient, resize the window to check the centered column.
2. Change a delay slider, an ordinary checkbox and a dropdown; Save and close, reopen and check values persist. Check live slider values and sound-disabled volume controls. No need to test destructive Maintenance actions.
3. From an active company, check the Campaign revival row and normal return to the same campaign/town page; without an active company it should be absent. The accepted map/roster/event flows do not need another full review.

Return visual/interaction feedback together. Do not propagate the style to all other screens or begin the deferred Help rewrite until the next scope is chosen.


## Feedback corrections (2026-11-02)

The user reported that slider movement was limited to the middle of a separate, misaligned fill bar. Both retained slider prefabs have2xlocal scale on Background and Fill Area. The first redesign reset their anchors but missed these inherited scales, doubling the rendered rail relative to the handle.

OptionsView now normalizes scale/rotation/pivot on the background, fill container, fill, handle container and handle. Background/fill share the same8pixel-inset rail as handle-center movement; fill and handle initialize from the existing normalized value. Rectangle images use Simple mode so a filled-image fraction cannot compound the rect fraction. Numeric ranges, saved values and callbacks are unchanged. Hover text increases15to19logical points (+4).

Use vore for the general mechanic. AI vore actions and Vore volume replace the euphemistic labels; existing help, downed-unit tooltip and retreat-log substitutions were corrected too. AGENTS.md records the preference. Internal identifiers and historical notices/proper names are not blanket-renamed. No broader Help rewrite, balance change or beautification.

Final compile/read-only scene pass: Logs/options15b-compile-and-scene.log, return0; no new compiler warnings, inspected references/72terrain retained. Task-start snapshots/source review: Logs/options15b-start-hashes.json, options15b-<source>-before.txt/-diff.txt and options15b-source-review.json. No new tests/framework or repeat of the earlier32data checks for these geometry/text changes; those earlier passes remain historical. No native settings write or maintenance action, Play-mode visual check or player build by the agent.

Focused next check only: drag a delay and volume slider through both endpoints and check the handle/fill agree; hover a setting and check the larger text is readable. The remaining layout is broadly accepted; no full category/persistence/battle retest requested. Beautification and other screens remain later work.
