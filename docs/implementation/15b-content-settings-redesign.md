# 15B — Content Settings and visible checkmarks

2026-11-02. The user accepted the corrected Options layout apart from low-contrast checkmarks, authorized Content Settings next, and requested the existing top page dropdown with no additional subcategories. Broad beautification, tactical/campaign styling and the Help rewrite remain deferred.

## Implemented scope

Content Settings now uses the same dark-to-moderate blue palette and centered 960×760 panel as Options, inside a scalable 1280×800 reference canvas. Each of the six existing pages has one scrollable column, smaller 17-point labels, aligned controls and live numeric slider values. Tooltips use 19 points. The page dropdown stays above the viewport, with per-page scroll positions retained during the session. No extra category headers or navigation layers were added.

The existing page order is Tactical Gameplay, Gender Settings, Appearance Settings, Vore Main, Post-Vore, Species Availability. All 50 retained serialized controls plus the curated species toggles are reused, preserving their values, ranges, callbacks and remaining preference keys. Ownership is captured before reparenting because some vore-weight sliders are nested under toggles. Old panels and labels are hidden only after the retained controls have new active parents. The existing Load saved confirmation, Apply to this game and Apply now and as global default paths remain available; none was invoked by the agent. Vore-type-dependent weight sliders remain disabled when their type is disabled.

The species page continues offering only curated species, with disabling affecting future generation and preserving existing characters. No species were approved, reclassified or newly enabled by this work. Global preference scope is unchanged.

Removed controls:

- Lewd combat log text.
- Males and Females attractedness dropdowns.
- Randomize leader appearance on death.
- Leaders use existing customizations.
- Lamias use tail as second stomach.
- Visible corpses.
- Winter holiday mode.
- Subjugated units wear rags by default.

As with earlier retired content controls, their existing code defaults now apply and their old player overrides are no longer read or written. Stored preference keys are not deleted. This does not invent new defaults: six toggles retain their existing ToggleDefaults entries; the two orientation values and seasonal mode retain their previous zero defaults. Removing Visible corpses from the menu does not mean turning the default off. Other retained preferences and mechanics are unchanged.

Options and Content Settings now use a shared bright, thick vector checkmark instead of tinting the legacy black sprite. It is controlled by the original Toggle state/fade, including dropdown selection marks. No external sprite/font/dependency was added. The previously corrected slider geometry is shared unchanged across both menus.

## Source and legal record

Modified Assets/Scripts/Subwindows/ContentSettings.cs for presentation lifecycle and nine retirements; Assets/Scripts/Core/ContentSettingsData.cs for the corresponding existing-default preference policy; Assets/Scripts/Subwindows/OptionsView.cs to use shared styling; Assets/Scripts/UI/UITheme.cs for shared slider/checkmark helpers. Added Assets/Scripts/Subwindows/ContentSettingsView.cs and Assets/Scripts/UI/SettingsCheckmark.cs with their matching .meta files. No serialized scene/prefab/art/package edits are intended.

Actual License.txt sections 4–5 reviewed. All six affected C# sources have dated conversion notices; the two new sources identify this fork's additions, and CHANGES.md records exact source/meta paths. Earlier attribution, notices and licensing remain. No copied external code, new assets or dependencies. Inherited provenance and distribution-method reviews remain open; this source-change check does not declare a release compliant.

## Verification and next review

Final Unity 2018.3.12f1 compilation and read-only base-scene inspection passed, return 0: Logs/content15-final-compile-and-scene.log. No C# errors or new normalized compiler warnings versus the previous Options correction. Inspected scene references and 72 terrain sprites remain intact. The initial compilation also passed.

Source/notice/control-coverage review is recorded in Logs/content15-source-review.json: exactly four existing sources changed, two new source/meta pairs, no other asset/package/ProjectSettings changes and no missing paths. All 50 retained controls are accounted for; existing defaults and shared accepted slider geometry are unchanged. License.txt hash remains unchanged.

No new tests/framework or repeat of older mechanics diagnostics for this focused UI milestone. No player build, live preference write, reset/maintenance action or Play-mode visual check was performed. Runtime layout and persistence still require the focused player review below. Unity was closed after the batch checks.

One focused review in the current Unity project:

1. Check visible checked/unchecked states in Options and Content Settings.
2. Select each Content Settings page and scroll longer pages; confirm retained controls appear, the nine removals are absent, and dropdown selection/scrollbar/tooltips are readable.
3. Change one retained slider and checkbox, Save and close, then reopen. Confirm persistence and that the species page still lists only curated species. No destructive reset testing or old campaign/battle tour is needed.

Wait for feedback before the next screen group.


## Terminology feedback (2026-11-02)

The user prefers herm/herms and prohibits unrequested word substitutions during UI changes. Five expanded labels were restored, and the corresponding tooltip uses Herm. Corrected the reported diegesting typo plus the outright spelling/word-use errors vales, occuring and effect starting Exp in DefaultTooltips. The preference is recorded in AGENTS.md. No mechanics, layout or saved data changed; no new compilation/playtest/manual test round for these text edits. Earlier 15B checks remain historical. Broader UI feedback and deferred Help work are unchanged.


## Apply-button feedback (2026-11-02)

User requested explicit scope labels. “Apply to this game” remains bound to ContentSettings.Exit: SetValues, then close. “Apply to all games” remains bound to ExitAndSave: SetValues, SavePreferences, then close. SavePreferences writes global defaults for subsequent games/reloads; it does not rewrite other saved game files. The original callbacks and preference behavior are unchanged, including the existing species-choice scope. Source tracing/diff only; no new compile or Play-mode check for these labels. Normal Options keeps its existing Save and close button.


Latest label correction (2026-11-02): the user replaced “Apply to all games” with “Apply now and as global default”. Applied verbatim, retaining ExitAndSave and the existing button geometry. “Apply to this game” remains unchanged. No new compile/tests or Play-mode run for this text-only correction.


Accepted by the user on2026-11-02 (“Content Options are good”). No repeated Content Settings test requested. Next authorized UI group: main menu, tracked in15C.
