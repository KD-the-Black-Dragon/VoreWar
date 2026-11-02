# 14 — Persistent navigation, map, town services and roster

**Current revision:** [14B — centered draggable map and normal Options](14b-centered-map-and-options.md) implements the user’s feedback, replacing the initial right column and stretched layout. The following14Arecord describes the earlier pass; its presentation is not accepted as final.

2026-11-02. The user selected campaign interface/map presentation and supplied the specific navigation, information and rest requirements in [spec14](../specs/14-campaign-interface.md). Implementation is prepared for one combined UI review. Spec13 is already accepted with its lighter0.56slug budget; that tuning remains intact. The broad Help rewrite is deferred.

## Visible changes

- Persistent Company / Map / Town buttons at the top, with Map anchored to the actual screen center. Company and Town sit on either side. Main menu/Help are right-aligned. Setup, creator and checkpoint selection hide campaign navigation rather than offer access to a stale company. Ordinary campaign pages use these buttons instead of repeated Return/World map controls.
- The20x12map uses42pxcells instead of32pxsteps at the reference resolution. Existing terrain sprites remain; no tile art or procedural map generation changed. Discovered towns use the retained generic settlement sprite (StrategyMode.VillageSprites[6], also the original village renderer's generic fallback); T remains available if that reference is missing. C marks the company and ! marks accepted contract targets, including their exact tile under fog. Available adjacent moves have a green outline and the company has a gold outline. Rendering does not reveal undiscovered terrain or towns.
- Active contracts stay in the right sidebar with rating, gold and deadline; contract book, saving, settings and memorial remain accessible. Move costs, adjacency, battle preparation and event rolls remain in the same action path.
- Bottom status groups finances (gold, current roster's debt, upcoming weekly wages), company condition (member/active/reserve counts, sum of current/max HP and percent, number of non-old injuries), and calendar (day, actions, days until payday and Ironman). The same read-only status is visible across ordinary company/town pages. It refreshes with redraw after transactions and daily changes, not by advancing the game.
- The canvas uses Expand scaling to keep the reference layout in bounds. Navigation uses center/right anchors and the status/message/retry areas use bottom anchors. Source layout was reviewed for reserved bands: header0–72, main controls/content84–638, map legend640–660, status at the bottom138–66, and feedback/retry below it. This is not a graphical multi-resolution pass.
- Town overview and recruitment/merchant/contracts/treatment share a persistent left service column. Town content uses a narrower layout alongside that column, including recruit details, selling spare gear and contract enemy rosters. Opening a different service directly redraws its page. The existing Town entry/Visit path and configured costs remain, and were not turned into a new service economy. Current entry cost remains0.
- The roster shows HP, per-level XP, highlighted LEVEL UP READY, temporary/old injury counts, wages, debt and active/reserve status. It offers Level up directly, plus Manage, Pay and Injuries. Selecting a level-up stat uses the existing choices/stat system and redraws the roster. Five rows/page remain, with more row height and pagination below.
- Rest controls are Rest1day and a custom1–365day input; fixed3/7day shortcuts are removed. Resume/Cancel queued rest appear only when rest remains. Rest buttons and the clock reject starting rest after spending actions. End turn stays distinct and uses ordinary passive recovery. An interrupted saved rest day finishes using its stored intent; starting the next rest day again needs a full budget.

## State and navigation safeguards

Top navigation remains visible but disabled during forced road choices, deployment, battle result/revival and campaign-end routing. It does not bypass saved decisions or move the company during combat. Payday decisions allow Company access for individual debt payment and Map access; the map's End turn action becomes Review notice while that acknowledgement remains pending. Save failure keeps the existing root Retry checkpoint available and blocks further mutations. Help content strings are untouched; normal campaign Help uses the persistent tabs, while creator/setup/end contexts retain explicit destinations needed to preserve the draft or record.

No new serialized field or schema migration is needed. CanStartRest is a computed property; the clock checks the budget only when beginning a new rest day. Cumulative XP storage and the approved per-level display, event occurrences, injuries, contract rewards, species policy and battle-fate rules are unchanged.

## Exact source scope

| Source | Work |
| --- | --- |
| Assets/Scripts/Subwindows/CompanyCampaignView.cs | Shared header/footer/service column; larger map/markers; compact contract sidebar and town overview; action-aware rest controls and navigation routing. |
| Assets/Scripts/Subwindows/CompanyManagementUI.cs | Town content positioning; persistent navigation replaces repeated returns; richer roster and direct level-up buttons. |
| Assets/Scripts/Subwindows/CompanyMissionsUI.cs | Town offer/detail layout and persistent campaign navigation; preserves deployment/result/Help entry contexts. |
| Assets/Scripts/Subwindows/CompanyRecoveryUI.cs | Treatment uses the persistent service column; ordinary navigation uses the shared header. |
| Assets/Scripts/Core/CompanyCampaign.cs | Computed full-action rest eligibility and clock precondition; normal EndDay(false) recovery is unchanged. |

All changed files have dated conversion notices and an exact root CHANGES record; earlier legal/modification notices remain. No external code/assets/dependencies were introduced. Existing terrain/settlement references are reused without modifying their assets, attribution or license. Actual License.txt sections4–5 reviewed. Inherited asset provenance and distribution-method gates still apply before publication; this source review is not distribution certification.

## Performed checks

- Unity2018.3.12f1 final compile and read-only base scene inspection: Logs/ui14-responsive-compile-and-scene.log, return0. Earlier internal passes are ui14-final/handoff-compile-and-scene.log. Initial ui14-compile-and-scene.log failed on a roster lambda using the enclosing loop's variable name; corrected before the successful passes.
- Scene audit: all inspected references true,72retained terrain sprites. No Play-mode/visual/layout pass is inferred. The existing audit does not inspect runtime-created buttons or assert settlement sprite content.
- Existing32managed production-code regression checks passed, without new cases/framework: Logs/ui14-managed-diagnostics.json. They cover the earlier data/persistence/wage/rest/health/revival/species/summon boundaries, not the graphical navigation/layout or a new focused full-budget-rest test. The final later edits only adjusted UI text/anchors, followed by the final successful compile.
- Task-start snapshots and source diffs: Logs/ui14-start-hashes.json, ui14-<source>-before.txt/-diff.txt. Scope/GPL/asset correspondence recorded in ui14-source-review.json. No scene/prefab/art/packages/ProjectSettings changes, file deletion/move, new tests, current Windows player build, commit or publication.

## One combined player review

Use the current Unity project; the older private Windows build predates this milestone.

1. Switch between Company, Map and Town. Map should remain centered in the top navigation, Town should be unavailable off a town, and the bottom figures should remain readable. In town, switch directly between Recruitment, Merchant, Contracts (including enemy details) and Treatment using the left column. Note any clipping or overlap, especially at your normal Game-view size.
2. In the roster, check the richer rows and use Level up directly for a ready member; choosing the stat is the next separate step. Check current injury totals exclude old injuries and wages/debt/HP update after normal actions. No need to repeat the XP pacing or injury-popup tests.
3. At full daily actions, use Rest1 or a custom duration. After spending actions, rest should be unavailable; End turn should make a fresh full budget available and should grant only normal recovery. Existing pending payday/event/result flows should still route correctly; no forced grind for unusual battle outcomes or save failures.

Review this as one milestone. Report demonstrated UI problems together; no new event content, species, Help prose rewrite or release follows automatically.
