# 15C — Main menu presentation

2026-11-02. User accepted Content Settings and requested the main menu next: restrained blue buttons, no Preset 1/2/3 or routine race-settings success message, displayed version 1.0, and an elevated central stack ordered New Campaign, Single Battle, Load Saved Game, Options. Credits, Help and Exit retain their positions. The title/logo is retained. No wider campaign/tactical styling or Help prose rewrite is included.

## Implemented behavior

StartMode restyles the existing main-menu background and direct button graphics with UITheme colors, light 24-point captions and thin borders. It moves only the four central buttons, retaining 360×60 dimensions and 12-point gaps at bottom offsets 400, 328, 256 and 184 on the existing 1920×1080 reference canvas. The canvas uses Expand to keep a minimum reference height and space below the title in short/wide windows. Credits, Help, Exit and developer access keep their original transforms, labels and callbacks. The title's text, material and transform remain untouched.

Preset controls are set inactive and their startup click listeners/private selection helper are retired. Serialized fields and the public visibility hook remain; the selected stored race-edit slot and its data are preserved. This does not bypass Race Editor's password or change its contents. Successful race-settings loading now clears the routine status text; missing-data/failure notices remain. Race loading/sanitization and defaults are unchanged.

The menu displays Version: 1.0. This is presentation for the conversion, not a change to legacy State.Version, company rule/schema versions, save validation or a declaration of release readiness.

New Campaign retains the existing company setup route; Single Battle retains tactical setup; Options retains normal Options. The existing Load Saved Game button was found to still enter the retired legacy save screen. It now opens the existing company checkpoint browser through a small CompanyCampaignView entry method. Existing list/history/empty-state, load validation, Ironman rules and checkpoint format are reused. Opening the list does not create a new company or advance time. No automatic load/write is added. Two legacy guidance messages now name the correct main-menu buttons.

## Source and legal record

Modified Assets/Scripts/Modes/StartMode.cs for styling/layout/labels, preset retirement, display version and company load entry; Assets/Scripts/Core/State.cs for the success status and two menu-guidance messages; Assets/Scripts/Subwindows/CompanyCampaignView.cs for the existing browser entry. No new source/assets/dependencies or serialized scene/prefab changes are intended. Dated notices preserve earlier notices, and exact source paths are recorded in CHANGES.md.

Actual License.txt sections 4–5 reviewed. Earlier licensing, attribution and warranty notices remain. No third-party additions or distribution. Inherited provenance and distribution-method reviews remain open; this menu work does not declare a release compliant.

## Verification and review

Final Unity2018.3.12f1 compilation/read-only base-scene inspection passed, return0: Logs/menu15-complete-compile-and-scene.log. No C# errors or new normalized compiler warnings versus the last Content Settings compile. Inspected scene references and72terrain sprites remain intact; earlier intermediate compiles also passed. Unity was closed after the batch checks.

Scoped source/notice/wiring review is recorded in Logs/menu15-source-review.json. Only the three intended C#sources changed under Assets/Packages/ProjectSettings; no added or missing paths. Existing scene object names/callbacks match the runtime lookups; title/peripheral transforms are untouched by the new styling. License.txt hash remains unchanged. Snapshots/diffs are under Logs/menu15-*. These are source/compile checks, not a live visual or loading pass.

No new tests/framework or repeat of older32mechanics diagnostics for this menu milestone. No preference reset, agent load of personal campaign files, Play-mode visual test or new player build was performed.

One focused review in the current Unity project:

1. Main menu: blue buttons, requested central order/labels, no presets or success message, Version: 1.0, comfortable clearance below the retained title; Credits/Help/Exit remain in their positions.
2. New Campaign opens company setup, Single Battle opens tactical setup, Load Saved Game opens saved companies (or its existing empty-state message), and Options opens normal Options. Return to the main menu to confirm the layout persists. No need to start a battle, repeat older content tests or activate Exit/destructive tools.

Await feedback before the next screen group. Content Settings is accepted at the user-reported scope. Main-menu review does not imply a campaign save or battle was tested.


## Size, spacing and supplied-logo feedback (2026-11-02)

The user found the first pass too small and requested padding between Credits/Help plus replacement of the old title text with the supplied Vore Warband banner. Main buttons are now460×76 with32-point labels, up from360×60/24points; central bottom offsets420/328/236/144 retain the requested order with16-point gaps. Corner buttons become400×68 with32-point labels. Help/Exit retain their bottom positions120/0; Credits moves to204, leaving16points above Help. Their right-edge anchors and actions remain. Version text increases24to28points; other status text is unchanged.

The supplied PNG was inspected and copied byte-for-byte from the user-provided Downloads path into Assets/Resources/UI/VoreWarbandLogo.png. Original dimensions2172×724, RGBA with transparency, SHA256 a8fbea4f27b0e83912bab56aa34dcc4497e74607ad0d4b8433adde68ae91e3f9. No crop, recoloring or regeneration. Added Assets/Resources/UI.meta and the PNG.meta for a single UI sprite, alpha transparency, bilinear filtering, no mipmaps and4096maximum dimension. StartMode loads the resource into a noninteractive Image preserving aspect ratio in a1200×400centered area at vertical offset260. The former title object is hidden after the sprite loads, preserving its source assets. With the existing Expand canvas, nominal clearance above the central stack is104reference points at minimum1080canvas height. Actual visual fit is pending player feedback.

Only StartMode.cs is modified in this follow-up; menu order, wording, callbacks, checkpoint loading, presets, displayed version and settings are otherwise preserved. Dated source notice and exact asset/meta associations are recorded in CHANGES.md. The supplied image contains no embedded author/license metadata; the user's request authorizes this integration, but no authorship or redistribution grant is inferred. Its provenance/terms are explicitly pending in release-readiness.md before packaging/publication. No new dependency or automatic GPL relabeling of the artwork.

Unity2018.3.12f1 compilation, logo import and read-only base-scene check passed, return0: Logs/menu15-logo-compile-and-scene.log. No C# errors or new normalized compiler warnings; inspected references/72terrain sprites retained. Imported metadata confirms single sprite, alpha transparency, no mipmaps and4096maximum size. No live Resources.Load or Play-mode visual pass was performed.

Scope/notice/asset review: Logs/menu15-logo-source-review.json. Only StartMode.cs changed, and exactly the PNG plus its two associated metadata paths were added. No missing paths or other asset/scene/prefab/package/ProjectSettings changes. PNG remains byte-identical to the user source; prior licensing/attribution and License.txt hash remain. Unity closed after the checks. No new tests/framework, older mechanics-check repeat, personal settings/save operations or player build.

One focused visual review only: larger buttons/text; separate Credits/Help; logo transparency, proportions and clearance. No need to repeat unchanged routes or play a battle. Wait for feedback before another screen group.


## Accepted and paused (2026-11-02)

The user explicitly approves the main menu, including its larger controls, Credits/Help gap and supplied logo. No repeat manual test is needed for this group; acceptance does not imply unreported specialist coverage. The user requests a pause until their five-hour usage allowance recovers and will tell us when to resume. The next Single Battle setup brief is recorded in spec15; no implementation/source audit of that group has begun. This acceptance/brief update changes documentation only, with no compile/tests/source/assets or gameplay CHANGES entry.
