# 14 — Campaign interface and map presentation

**Status:** Functional layout user-accepted on2026-11-02 after the eight-item feedback revision. Placement has no material critique. Beautification is deferred until a unified design across campaign/Options/content/tactical screens; see [design brief15](15-unified-interface-design.md). See [current14Breport](../implementation/14b-centered-map-and-options.md). Earlier14Alayout below is historical where superseded. See [implementation and checks](../implementation/14-campaign-interface.md).

**Prerequisites:** Accepted company loop and spec13 event proof of concept. The user chose interface/map presentation as the next milestone, then supplied the layout requirements below before source changes.

## User-defined scope

- Map is the primary view and occupies most of the main content area, retaining original terrain artwork. Company, Map and Town buttons persist across campaign screens; Map is at the top center, with Company and Town on either side. Town is available only at a town, subject to existing decision/save/action safeguards.
- End turn is separate from rest and does not grant resting recovery. Offer Rest 1 day and a custom-day input. Starting rest requires the full daily action budget.
- Group critical information along the bottom: gold, current-member debt and upcoming wages; member count, aggregate company HP and current injury count excluding old injuries; day, remaining actions and days until payday.
- Town services form a persistent left column so recruitment, merchant, contracts and treatment can be switched directly. Global navigation replaces generic Return/World map buttons. Local confirmations and task-specific deployment/loadout destinations remain where needed.
- Roster shows more member information, prominently identifies level-up readiness and provides a direct Level up action. Company > Level up initiates improvement selection without first opening member details.
- Preserve gameplay, saves, random-event tuning, progression rates and retained combat. The full Help content rewrite remains deferred. No additional species, authored narrative, art import, engine upgrade or release work is included.

## Implementation and review boundaries

Shared navigation stays visible but cannot skip forced deployment, event choices, results, revival or campaign end. Payday choices retain Company access for individual repayment; Map exposes Review notice to acknowledge a pending daily decision. Setup, character creation and checkpoint browsing retain their separate entry context.

Full-action rest is enforced in the campaign clock as well as buttons. An already-started interrupted day can finish under its saved intent. Continuing queued rest after spending actions requires ending the ordinary turn first. Normal passive daily recovery remains; End turn does not set the resting flag.

## Completion criteria

1. Company/Map/Town positions are stable while switching screens and town services remain available alongside their content.
2. Map > adjacent tile moves normally; Town > service and Company > Level up each initiate the task in two clicks.
3. Bottom totals include active and reserve members, omit old injuries from the current count, and update after purchases, repayment, healing and level-up changes.
4. Rest is unavailable after spending actions, becomes available on a fresh turn and retains wage/deadline interruptions. End turn remains ordinary recovery.
5. Existing compile/read-only scene and managed regression checks pass. One connected player review confirms layout and interaction; no per-control/menu-opening microtests.

## Execution record

Five existing C# files changed; no new source/assets/scene/prefab/package changes. Initial compile found a local lambda-variable shadowing error, corrected. Final Unity2018.3.12f1 compile/read-only scene inspection passed;72terrain sprites and inspected references intact. Existing32managed checks passed without new test cases/framework. Actual graphical UI and the two-click workflows await player review. No Windows player rebuild or publication.


## Feedback revision14B (2026-11-02)

Current direction supersedes the initial right-column and stretched layout: top Contracts far left/Records far right; bounded centered map/interface; active contracts in the lower bar; no terrain-cost description/right column; leader revival in normal Options. Add large draggable maps with left/middle input and no movement on drag release. The user specified80x60tiles and explicitly declined any increase from six towns. Existing saves keep their map. See14Breport for exact implementation, final compile/data checks and remaining live review. Help content rewrite remains deferred.


## Functional acceptance and deferred visual finish (2026-11-02)

The user describes14B as acceptable and functional, with no real placement critique. Accept this layout milestone and do not request the same review again by default. It still looks prototypical; thin borders/subtle shading are desired examples, but the user explicitly does not want that fixed immediately. Options, content menus and the tactical view should first receive a unified design addressing sky-blue backgrounds, gigantic text and haphazard placement. No exact palette or tactical layout has been chosen. This records feedback only; no new tests/visual inspection or source edits.
