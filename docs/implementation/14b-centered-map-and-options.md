# 14B — Centered campaign frame, draggable region and normal Options

2026-11-02. Implements the user's eight feedback items on the initial interface. This replaces the right-column presentation and stretched anchors from14A; it is not acceptance of14A's graphical layout. The user subsequently accepted the functional layout on2026-11-02 with no real placement critique. The review checklist below is historical; do not repeat it by default. Help prose rewriting stays deferred.

## User direction and implemented behavior

1. Contracts is now the far-left top navigation button; Records is far right. Company / Map / Town remain between them, with Map centered. Context guards still prevent skipping forced event/deployment/result/revival decisions.
2. Campaign leader revival is a campaign-specific checkbox in the existing normal Options screen. The separate campaign Settings page/button is retired. Ordinary preferences remain global; leader revival still saves into the company checkpoint (or the supported deployment checkpoint when changed through battle Options), not PlayerPrefs. The control is absent without an editable active company. It cannot edit a stale hidden company from setup/leader creation. Options closes back to the same underlying company/town page, preserving the town column; Escape also closes/saves this native Options view.
3. The entire campaign UI uses a fixed1280x800frame centered within the scalable canvas. Extra widescreen/4:3 space becomes outer padding; header/status elements no longer pull toward the screen edges. The map viewport is centered in that frame with80logical pixels of left/right padding, and the finance/condition/calendar groups stay bounded with it. Main menu, Help and normal Options are utilities below the status bar.
4. The right column is gone. Active-contract count, reputation and the first three rating/reward/deadline/location summaries appear in a lower map strip; further contracts remain one click away in Contracts. Terrain/movement-cost descriptions and the former right-side location text are removed. Movement costs themselves remain unchanged. Saving stays with map controls, alongside Center company.
5. New campaigns and map previews generate80x60tiles and exactlySIXtowns, as explicitly directed after the size question. Existing checkpoints keep their own saved dimensions, towns and discovery. Company loading now accepts the domain's validated4–100tile dimensions rather than requiring the former20x12view size. No legacy empire-save import is introduced.
6. Left- or middle-button dragging pans the map, matching the original Camera_Controller's pointer choices. The original controller was traced, but is not invoked: it assumes the old world/camera and scene modes. The company implementation uses a clipped ScrollRect with the same pointer inputs. Wheel input pans through the ordinary ScrollRect; no new zoom/edge-scroll/keyboard subsystem was added. Center company returns to the company; successful movement/load/position change centers it automatically. Panning otherwise survives tab switching during that UI session; it is not new saved camera state.
7. Only348tile controls (29columns x12rows, including a margin) are pooled for the1120x420viewport. Changing the viewport's first visible cell rebinds those controls to world coordinates. It does not create4800buttons, re-run generation, reveal fog or consume actions/RNG. Ordinary adjacent-tile clicks still use the existing movement/contract/event route. Unity's drag threshold and explicit drag/release click suppression prevent panning from becoming a movement click. Unseen accepted targets retain exact ! markers; undiscovered terrain/towns remain hidden.

Rest1/custom controls, the full-action rest requirement, direct roster level-up and the persistent town service column from14A remain. No injury/progression/wage/contract-reward/species/event-budget changes are part of this pass; the accepted0.56random-combat budget remains intact.

## Exact source scope

| Source | Change |
| --- | --- |
| Assets/Scripts/Subwindows/CompanyCampaignView.cs | Bounded centered frame, navigation/footer reorganization, clipped pooled map rendering, center/pan lifecycle, normal Options handoff and return context. |
| Assets/Scripts/UI/CompanyMapScroll.cs (new, with matching .meta) | Left/middle ScrollRect forwarding and explicit suppression of tile clicks on a drag release. No campaign mutations. |
| Assets/Scripts/Subwindows/Options.cs | Reuses the existing Toggle/grid style for the active company's revival preference, separate from global stored preferences. Existing inherited callbacks are cleared on the cloned control. |
| Assets/Scripts/Subwindows/CompanyMissionsUI.cs | Removes the separate campaign settings page; shared contracts/records still provide their existing functions. |
| Assets/Scripts/Core/CompanyCampaign.cs | Generate overrides new region dimensions to80x60; the six-town loop and saved fields are retained. |
| Assets/Scripts/Core/CompanyCampaignSave.cs | Resume relies on existing map validation rather than fixed20x12presentation assumptions; company-folder/Ironman/live-battle restrictions remain. |

No scenes/prefabs/art/packages/ProjectSettings were edited; the pooled view reuses retained terrain/settlement assets. No copied external source/dependency or license change. New source is identified as this fork's addition; all6affected sources have dated notices and exact CHANGES coverage, with the meta associated there. Actual License.txt sections4–5 reviewed; attribution and prior notices preserved. Inherited provenance/distribution obligations remain open before publication.

## Performed checks

- Unity2018.3.12f1 compile/read-only base scene check passed, return0: Logs/ui14b-final-compile-and-scene.log (initial implementation also passed ui14b-compile-and-scene.log). Unity was observed closed before exclusive checks; no agent termination of the user's editor. No new normalized compiler warnings; inspected scene references intact and72terrain sprites retained.
- Existing32managed production-code regression checks passed with output redirected to Logs/ui14b-*; no new cases/framework or project test sources. This includes accepted former20x12fixture save/resume behavior.
- Inspected actual CompanyCampaign.Generate output and its immutable save/LoadForResume roundtrip using the existing diagnostic helpers:80width,60height,4800terrain tiles and6towns before/after. The inspected region is a map-only generated state without a roster; this is not a complete generated-company/playthrough. Values: Logs/ui14b-generated-map-values.json.
- Task-start asset/source snapshot: Logs/ui14b-start-hashes.json. Before/diffs: ui14b-<source>-before.txt/-diff.txt. Final scope/notices/asset/license review: ui14b-source-review.json.
- No Play-mode visual drag, resolution, native Options-return or performance test was performed; no current player rebuild. The older Win64 artifact predates this pass. No file deletion/move, commit/push or publication.

## One focused player review

Use the current Unity project.

1. Check the centered frame at your normal window size and, if convenient, a4:3or narrower window. Contracts/Records should stay at the top ends, with no right column or spread-out status groups. Accepted work should appear in the lower map strip.
2. Create a new region to see80x60with six towns. Drag with left/middle mouse; looking around must not spend actions, move the company or uncover terrain. Click Center company, then make an ordinary adjacent move. An older checkpoint should retain its old map and still load.
3. Open normal Options from a company/town screen. The leader-revival checkbox should work and persist; closing Options should restore that same page/left service column. From the main menu without an active company, Options should not expose a stale company setting.

These checks are one UI feedback pass. No repeat old ability/menu tours, XP-pacing tests or forced event/injury grinding. Report any clipping, unexpected movement or Options routing issue together.


## Acceptance and next design direction (2026-11-02)

User: acceptable/functional, no real critique about placement. Record14B accepted at that scope; no new source/build/test is implied and unreported specialist cases remain unreported. The remaining visual issue is a spartan prototype look. Defer borders/shading/beautification until a shared UI design covers campaign, normal Options, content menus and tactical view. The user specifically dislikes the older screens' sky-blue look, oversized text and inconsistent placement. [Brief15](../specs/15-unified-interface-design.md) records the next design work; its palette, typography and tactical hierarchy require user direction before implementation. Help prose remains separately deferred.
