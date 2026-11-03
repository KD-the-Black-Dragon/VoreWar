# 15D — Single Battle setup presentation

2026-11-02. Resumed at the user's request after their usage allowance recovered. Options, Content Settings and the main menu are already accepted. This group applies the shared blue styling to Single Battle setup (Create Tactical), preserving every existing field and both species previews. It does not redesign the live battle HUD or change battle generation/balance.

## Presentation

A centered1240×760frame uses the existing1280×800 Expand reference convention. Create World, Back and Content Settings are centered at the top; Tactical Size X/Y, Auto-Scale and Field Type follow immediately below. Two thin muted dividers separate these controls from the side panels.

Clearly labeled Attackers and Defenders occupy the left and right columns. Each keeps its Race choice, AI Enabled checkbox and species sample visible above an independent vertical scroll area. The scroll areas retain AI Type, Units, Level, Can Vore, Has Leader, % Ranged, % Heavy Weapons, % Spell Books and Max Book Level. Existing values, ranges, options, callbacks and disabled-state rules are reused. Labels use18points, side headings24, screen title26 and hover text19. Sliders and bright checkmarks use the shared UITheme helpers. No unrequested wording substitutions.

The original UIUnitSprite objects, Name references, Actor_Unit preview data and appearance/species update callbacks are retained. Sample names are positioned outside sprite scale and use readable foreground text; names hide when their original unavailable-species preview is hidden. Sprite images retain their original rendering and content preferences. Only presentation positions/scales change; no species or graphic is newly approved.

The watch/don't-watch AI dropdown is in the bottom section and retains its existing choices and enablement. Conditional bulk controls remain conditional: Runs per matchup, Include animals, Run current matchup, Run all matchups, and Run all matchups with the current Attacker. The original BulkPanel still follows WatchOptionChanged (both AI enabled and the original appropriate viewing choice). When shown, the side scroll areas become shorter to make room, keeping every field reachable. Original three bulk buttons are moved and styled with their existing callbacks. Bulk jobs were not run by the agent.

All29input controls are accounted for:11per side and7global/bulk; all6original action buttons remain, plus the two preview objects, six percentage readouts, current bulk value and tooltip. Legacy empty containers/labels are hidden after retained objects move out. No menu field is retired. Existing top-button captions remain Create World, Back and Content Settings.

## Wiring and source

- Modified Assets/Scripts/Subwindows/CreateTacticalGame.cs: build/refresh the presentation when opened/started and reapply its existing AI/Auto-Scale/percentage/bulk display handlers once for initial consistency. Two label lookups now include inactive children so the existing callbacks can still update their retained hidden legacy labels after layout replacement. All serialized/public declarations, battle generation, species selection/guard, validation, batching loops, combat entry, config/value semantics and handler bodies are otherwise preserved.
- Added Assets/Scripts/Subwindows/SingleBattleSetupView.cs and matching.meta for the runtime layout and retained-control styling. No scene/prefab/preview artwork/settings/package changes are intended. No external code, asset, font or dependency additions.
- Actual License.txt sections4–5 reviewed. Dated conversion notices on both C#sources, new source identified as a fork addition, exact source/meta paths in CHANGES; earlier notices/attribution/licensing retained. Existing provenance/distribution gates, including the earlier supplied-logo record, remain open. No publication or deletion/move.

## Checks and limits

Unity2018.3.12f1 compilation and read-only base-scene inspection passed, return0: Logs/battle15-compile-and-scene.log. No C#errors or new normalized compiler warnings versus the last main-menu logo compile. Inspected scene references and72terrain sprites remain intact. Unity was closed after the batch. Source snapshots: Logs/battle15-start-hashes.json, battle15-CreateTacticalGame.cs-before.txt/-diff.txt and serialized wiring battle15-scene-blocks.json.

Scoped review is recorded in Logs/battle15-source-review.json: only the intended existing CreateTacticalGame source changed, plus the new view/meta pair. No missing paths or other Assets/Packages/ProjectSettings changes. All29inputs and the original action/preview/readout bindings accounted for; public declarations unchanged. Exact source reconstruction confirms generation, species rules, validation, bulk loops and existing handlers outside the documented presentation additions/two inactive-label lookups are unchanged. Both dated notices and exact CHANGES paths present; License.txt hash and prior attribution preserved.

No new tests/framework, broad repeat of older32mechanics diagnostics, player build, Play-mode visual/battle/bulk-run check or agent personal-settings/save operation. Source coverage and compilation do not establish that native dropdowns, previews or field interactions have been visually tested.

## One focused player review

1. Open Single Battle: inspect top controls, left Attackers/right Defenders, both species samples and bottom watch choice. Scroll each column to its last field; change races and a percentage slider to inspect the live preview/readout. No repeated main-menu/Content Settings tour is needed.
2. Toggle Auto-Scale and AI Enabled to check their existing input/dropdown dependencies. Enable both AIs and inspect the conditional bulk controls under their appropriate watch setting; no need to run large bulk jobs.
3. If convenient, use small forces and Create World to confirm battle entry still works; there is no need to complete the battle. Back and Content Settings should retain their normal return behavior.

Wait for feedback before another UI group. Broad Help rewriting, final beautification and campaign/live tactical styling remain deferred.


## Caption-free 2x sample units (2026-11-02)

After standalone review, user identifies the small Unity Game view as a major contributor to visual confusion. Requested removing the temporary main-menu comparison entry and hiding species captions inside Single Battle sample cards, with sample dolls2×normal UI scale and centered. StartMode no longer calls PortraitComparisonPanel.Attach; retained tool/audit/shader sources are not deleted. SingleBattleSetupView retains the required UIUnitSprite.Name reference for the preview update callback but keeps its object inactive, including after refresh. Unit transform scale2, anchor84,144: the existing renderer adds30upwardpixels scaled to60, bringing image center to84 in the168square card. Species dropdowns remain labeled and functional. No other layout, appearance, sampling, company/recruitment alignment or gameplay change.

Fresh standalone build and scope review will be recorded at handoff; visual fit is pending. No new tests/framework or broad menu/combat retest requested. Dated notices and exact two source paths are in CHANGES; prior notices/licensing/attribution preserved, no external additions or publication.


## Larger preview cards (2026-11-02)

User accepts2×dolls and requests approximately15extra screen pixels in each box dimension at1080p. Cards increase168to180reference units (about16.2screen pixels with1080/800scaling). Lower-left remains fixed: x408/top-8/height180 retains bottom172. Dollcenter90,150 includes60renderer offset. Race/AIEnabled rows narrow16units; other fields' scroll viewport and scrollbar move16down. Column backgrounds widen12to contain the cards; scroll areas shorten correspondingly and retain every field. No sampling/artwork/gameplay changes. Fresh standalone build/checks recorded in the resume checkpoint; actual fit needs focused visual review.


Subsequent positioning feedback (2026-11-02): user requests moving the complete card down15screen pixels at1080p. Changed only top offset-8to3 (11referenceunits, approximately14.85screen pixels), retaining180square size and2xcentered doll. Other controls remain in their accepted positions. Fresh private build/check recorded in checkpoint.


Feet-clearance correction (2026-11-02): user observes whole-card nudge moves the child doll as well, leaving relative feet clearance unchanged. Keep card position/dimensions fixed and move only the doll upward11reference units, anchorY150to139 (~14.85screen pixels at1080p). Scale2andothercontrols unchanged. Build/check recorded at handoff; no graphical pass inferred.


## Final preview approval (2026-11-02)

User explicitly approves the corrected layout: “we got it exactly right.” Preserve the180square card at408,3,2xsample scale and child anchor90,139, with species caption hidden. This accepts the final visible fit/feet clearance in the standalone review; no repeated test or further adjustment needed. It does not imply unreported combat/performance coverage.
