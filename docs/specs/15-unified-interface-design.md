# 15 — Unified interface design

**Status:** User resumed work after usage recovery. Options, Content Settings and the main menu are accepted. [15D Single Battle setup](../implementation/15d-single-battle-setup.md) is implemented and has passed compilation/read-only scene inspection; one focused graphical review remains. Broader campaign/live tactical styling and final beautification remain future work.

## User direction

The campaign interface is functional and placement is acceptable, but looks spartan/prototypical. Thin borders and subtle shading are examples of desired eventual polish. Do not apply a campaign-only cosmetic patch now. Options, content menus and the tactical view need a similar redesign: the user dislikes their sky-blue appearance, oversized text and inconsistent placement.

Establish one coherent design across campaign, Options, content menus and tactical screens before the final beautification pass. The existing campaign colors are not an approved final palette. The Help prose rewrite remains a separate deferred task.

## Next design work, when requested

- Review typography hierarchy, readable sizes, spacing/padding, panel/button states, color/contrast and treatment of borders/shading with the user. Avoid an unrequested detailed art direction or replacement asset set.
- Map which elements share a common style and which information each screen must prioritize. Tactical HUD placement/interaction still needs user input; do not infer it from the campaign layout.
- Prepare reviewable layouts for Options, content settings and tactical controls using the same conventions. Retain the accepted campaign navigation, bounded presentation, town shortcuts and status grouping unless the user asks otherwise.
- Trace legacy scene/prefab/runtime UI ownership before implementation. Preserve content-setting persistence and restrictions, normal Options behavior, tactical controls/visibility, text notifications and gameplay/save rules. This brief performs no such source audit yet.
- After that direction is settled, implement coherent screen groups with one meaningful review per milestone. Thin borders, subtle shading and other finish work should follow the common design rather than establish separate incompatible screen styles.

## Scope limits and acceptance

No source, assets, scenes, settings, tests or build changes in this recording task. This is future design scope, not a claim of a completed UI system. It does not authorize extra species/events, a Help rewrite, gameplay rebalance, new dependencies or publication. No file deletion is authorized. User feedback on14B does not establish every unreported specialist combat/aspect-ratio/save case as tested.


## Options-first decisions and execution (2026-11-02)

User provided the current Options screenshot and requested smaller text, one centered scroll column, coherent categories and less grey/eye-searing cyan. Dark-to-moderate blue is the chosen direction. Explicit removals: Strategic Move Delay, Pop-up Notifications, Ignore monster-only battles and AI-vs-AI viewing dropdown. Categories Move Delays/Audio/Graphics/Controls are required; Campaign/Maintenance additionally approved. Preserve other settings and existing save/return behavior; keep the long single-side auto-advance setting within one row.

15A adds the shared UITheme foundation and OptionsView, modifies Options lifecycle, and preserves existing preference/storage callbacks and serialized controls. Compile/scene and existing32checks passed; actual new UI review pending. User-owned tactical/content layout direction, full style propagation and Help rewriting are not included in this implementation.


## Options feedback (2026-11-02)

The user finds the Options layout mostly okay. Correct inherited slider scale/rail alignment, enlarge hover text by several points and restore vore wherever general player-facing wording had been changed to consumption. Applied15to19point hover text and the focused terminology correction, preserving mechanics/values. Beautification stays deferred; no broad Help rewrite. Compilation/scene checks passed; only focused slider/hover visual confirmation remains for this correction. The terminology preference is recorded in AGENTS.md for future work.


## Content Settings scope (2026-11-02)

The user accepts the Options slider/hover correction and authorizes Content Settings next, with one remaining Options tweak: clearly visible checkmarks. Retain the existing top page dropdown, without any subcategories. Use the same restrained blue panel, smaller text, aligned controls and scroll column. Preserve the retained settings, their global persistence and curated-species restriction.

Remove Lewd combat log text; Males and Females attractedness dropdowns; Randomize leader appearance on death; Leaders use existing customizations; Lamias use tail as second stomach; Visible corpses; Winter holiday mode; Subjugated units wear rags by default. Follow the existing code-default policy for retired controls, preserving the existing defaults and stored key data. See [15B implementation](../implementation/15b-content-settings-redesign.md). Options and Content Settings receive shared bright checkmarks; broader beautification, tactical/campaign styling and Help rewriting remain deferred.


## Main-menu scope (2026-11-02)

Use the shared blue styling, remove the three visible presets and routine race-settings success text, and display version1.0. Move the four central buttons up beneath the retained title, ordered New Campaign, Single Battle, Load Saved Game, Options. Credits/Help/Exit retain their positions. Reuse existing controls/routes and the existing company checkpoint browser for the load entry; no new loading policy, release declaration or broad Help rewrite. See 15C for exact implementation and pending focused review.


## Next brief: Single Battle setup (15D; recorded 2026-11-02)

The user approves the main menu and explicitly pauses work until they report recovery of the five-hour usage allowance. Record this brief only; do not begin implementation or further study during the pause.

On resumption, apply the established dark-to-moderate blue styling to Single Battle setup, formerly Create Tactical. Replace its eye-searing blue background and confusing layout. Preserve every existing field, its behavior and the species sample images; no control retirement is authorized in this screen group.

- Center the core controls at the top: Create World, Back, Content Settings, Tactical Size X/Y, Auto-Scale, and the Field Type dropdown.
- Left column: clearly labeled Attackers and all of their information/options, including their species preview image.
- Right column: clearly labeled Defenders and all of their information/options, including their species preview image.
- Bottom: the watch/don't-watch AI battles option.
- Use two thin, subtle dividing bars to separate the simple core options from the combatant sides.

Preserve wording except these requested section labels or outright typos; retain herm/herms and vore terminology. Do not infer extra gameplay/balance changes, remove controls because they look extraneous, or begin a battle HUD redesign. Trace CreateTacticalGame and its scene/prefab/runtime ownership before choosing the implementation. Main-menu approval does not establish any unreported tactical/battle coverage. Continue with one meaningful screen-group review after implementation and appropriate existing checks; no repeated accepted-screen tours.


### 15D execution after user resume (2026-11-02)

The user reports usage recovered and requests resume, ending the earlier pause. Source/scene ownership was traced and the recorded Single Battle layout implemented using the original fields, buttons and UIUnitSprite previews. Conditional bulk controls also remain; individual combatant columns scroll to preserve every field in a bounded window. See the15Dreport for exact scope/checks and one pending focused review. Main-menu approval stands; do not repeat its checklist. No live battle HUD redesign or broad Help rewrite is included.
