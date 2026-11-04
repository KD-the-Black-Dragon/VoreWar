# Spec28 — Blue-and-gold UI beautification

2026-11-03. User approved application of the selected standalone sampler treatment to player-facing UI: **Current menu blue · gradient100% · border100% · Aged gold · panel borders**.

## Implementation

Keep shared Background/Panel/Row/Section/Control/Foreground palette values. Border now#917a51, selected/slider accent#c4a574. New locally authored UIThemeSurface draws two gradient bands and an inset physical-pixel border using standard Unity UI geometry, with no new textures/materials/shaders. Its configuration fields are serialized so Unity dropdown-template clones retain the treatment. Control top blends16%toward white, middle retains the base, bottom14%toward black; panel/row/section depths are55/35/65%of control depth. Panel border70%alpha; control borderfullalpha. White manual-entry inputs retain black text/readability and gain gold framing. Text, layout, user actions and gameplay are preserved.

Shared UITheme.Surface covers current menu/options/content/setup/creator/SaveLoad/Help/Credits/Esc/dialog/tooltip chrome. Explicit remaining grey/direct-color campaign, tactical and dropdown paths were migrated. Context-menu and species-hover card backgrounds now share the treatment. No developer Race Editor page redesign.

## Player-facing coverage

- Main menu; New Campaign setup and leader creator; shared Help and credits.
- Map frame, campaign navigation/status/contracts bar, town hub/service column, company/recruitment, loadout/progression/level-up, merchant/inventory, injuries/treatment and nickname screens.
- Contract offers/details, deployment, battle results/revival, road events, payday/departures, records/memorial: the shared management surface/actions cover these existing pages.
- Save/Load browser, Options, Content Settings, dropdown item/templates, sliders/checkmarks, confirmation/input dialogs and hover tooltips.
- Single Battle setup, tactical sidebars/log/equipment boxes, commands/selected command, context menus, auto-resolve popup and Esc overlay.

## Preservation boundaries

Only UI-only, sprite-free simple surfaces use the new mesh effect. Map tile buttons explicitly bypass shared chrome; tile art/fog/selection wiring unchanged. Portrait descendants excluded from generic surface sweeps. Status Sliders (including transparent barrier backgrounds), HP/mana/XP fills and gameplay/status colors preserved. The map border is a separate non-raycasting overlay; backplates stay beneath all roster portraits, with no new portrait masks. No text size, sprite filtering/magnification, accepted positioning or action logic changes. No source entities/save algorithms changed; no scenes/prefabs/art/resources/packages/project settings changed in this task.

## Checks actually performed

Task baseline sources/allgamepath hashes preserved in Logs/ui28-before.json. Final scope:11existing C# files changed, newUIThemeSurface.cs/meta; no other game files changed/missing relative to baseline. Source/diff/earlier-notice coverage reviewed. Native Windows build exited0, no scanned compiler errors/exceptions/missing scripts (Logs/ui28-review-build.log). Earlier ui28-compile-scene.log initially referenced a stale source list before import refreshed the new file; its exit0 is not represented as a clean compilation. No new test framework/game test sources, no unrelated repeat save audit. No graphical playtest claimed: render fidelity, clipping edges and interaction states need the combined user review below.

## Combined player review

1. Main menu, Options and Content Settings: compare blue/gold surfaces, dropdown items, bright checkmarks and slider/manual-entry behavior. Help/Credits/SaveLoad/Esc should share the same borders and depth.
2. Campaign: inspect map/status/navigation, drag the map, open Company and a town service. Confirm thin borders/frame contrast, unchanged paper dolls/terrain, and clickable controls. Sample a contract/deployment/result or event screen as convenient; no need to repeat every accepted gameplay test.
3. Single Battle: check tactical panels, solid HP/mana/XP bars, the aligned gold Attack/Move/Vore selector, log and context menus. Core fonts and positions should remain as previously accepted.

Use this as one consolidated visual review; no permission to redesign layouts/typography is inferred from the palette choice. No automatic subsequent phase until feedback.

## License and artifact

All12new/modified C# sources have dated fork notices; earlier notices preserved. CHANGES lists exact paths, including new metadata. No external code/assets/dependencies or file moves/deletion. Existing license/attribution/provenance and source/binary distribution gates remain applicable. The next result record Logs/ui28-result.json identifies the packaged private build and notices; previous builds remain untouched.

## Delivered review artifact

`Builds/Warband-Review-20261103-0691b41b/Win64/VoreWar.exe` includes the selected styling, cloned-dropdown configuration and pending credits wording. Final build exit0/no scanned C# errors/exceptions/missing scripts/shader errors. Earlier b65a4d7c/8ff1ad52 builds remain preserved intermediates; the delivered package also keeps the new setup backplate below its existing title. Seven existing license/change/dependency notices packaged. No graphical playtest is claimed.

## User acceptance and two follow-ups — 2026-11-03

User explicitly accepts all styling except duplicate inner left combat-log border and roster row buttons reaching their enclosing border. Source fixes remove only inset left edges (not top/right/bottom or the outer log frame) and modestly reduce only five member-row button types: height38→34, widthminus6 centered horizontally, matching label rectangles with original fonts. Other accepted styling preserved. Static log hierarchy confirms text lies inside Viewport and Scroll View, whose runtime theme surfaces are covered by the ancestor-only correction. No C# compilation, new player build or graphical playtest for this follow-up; include in next requested build. No repeated broad UI review required. Source snapshots/diff Logs/ui28-feedback-before.json and ui28-feedback-source.diff.
