# Vore Warband UI sampler

Open [index.html](index.html) in Edge, Chrome or another modern browser. It runs directly from disk, offline; no server, installation or Unity build is needed.

This is a standalone design study. It does not modify the game's UI, settings or saves. Layouts and sample values illustrate controls rather than propose new gameplay or a new screen arrangement.

## Explore

- Compare **Current campaign grey** and **Current menu blue** with **Blue & aged gold**, **Midnight & bronze** and **Warm slate**.
- Pick a scheme, then adjust gradient contrast, border emphasis, border accent and whether panels have borders. Choosing another scheme loads that scheme's starting treatment; Reset treatment restores it.
- Switch among Campaign, Tactical controls and Options. Open Sample dialog to compare popup surfaces; Escape dismisses it.
- Hover and press the buttons. Active/reserve and tactical command selections demonstrate selected states. The unavailable controls deliberately show their disabled appearance.
- Hold the comparison button with the pointer, Space or Enter to temporarily show the existing campaign or menu palette. Release to return to the chosen treatment.
- Use Copy preference to copy the selected scheme/settings. If clipboard access is unavailable for a local file, the summary is selected so Ctrl+C can copy it.

Sampler choices are remembered only in this browser's local storage. No data is sent anywhere. Test controls do not affect the game. The tactical field is deliberately omitted so the surrounding interface can be compared without introducing new artwork.

## Implementation and checks

Added by this fork on 2026-11-03 (spec 27), under the applicable terms in [License.txt](../../License.txt); the modification record is in [CHANGES.md](../../CHANGES.md). The HTML is self-contained and uses system fonts, CSS and JavaScript. No external code, libraries, fonts, images or game assets were introduced or copied into it. Existing game palette values were consulted for the two reference schemes.

Inspected in isolated headless Edge: 14 focused interaction/layout checks passed, covering scheme selection, gradient/accent controls, dialog focus and Escape, tactical command selection, keyboard comparison/restoration, reset and no horizontal overflow at 1440, 1024 and 360 pixels. Campaign/tactical/options screenshots were rendered and visually inspected. Final JavaScript syntax check passed after formatting and a minor small-screen value-label adjustment. Evidence is under ignored `Logs/ui27-*`; no test framework or Unity test sources were added. No Unity compile, player build or in-game playtest was run or needed for this standalone sampler.

Browser rendering is a design reference, not proof of identical Unity rendering. Once a direction is selected, it needs a separate implementation in the existing Unity UI and an in-game review.

## Selected treatment — 2026-11-03

The user selected:

> Warband UI preference: Current menu blue · gradient 100% · border 100% · Aged gold · panel borders

Use the **Current menu blue** base palette, not the Blue & aged gold preset. Apply the sampler's Aged gold accent override, maximum sampler gradient contrast and border emphasis, and borders on panels as well as buttons. “100% gradient” means the sampler's existing bounded treatment, not a solid white-to-black ramp: control top is a16% mix toward white, bottom14% toward black, with gentler panel/row gradients. Gold control edge is #917a51 at full opacity; highlight/selected accent #c4a574; panel edge uses70% opacity at full emphasis. Typography/layout were specimens, not approved redesigns.

This chosen treatment is now implemented in the player-facing Unity UI. See [the implementation report](../implementation/28-ui-beautification.md) for the current review build and verification limits.
