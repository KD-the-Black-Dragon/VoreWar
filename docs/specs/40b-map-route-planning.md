# 40B — World-map route planning

Planning draft, 2026-11-06. Part of [module 40](40-authoring-and-campaign-progression.md).

## Goal and current wiring

Let the player plan travel on the existing map while retaining per-tile actions, meaningful terrain costs and manual End Turn. `CompanyCampaign.Move` currently accepts one adjacent tile. `CompanyCampaignView.Move` handles contract interception, movement, discovery, road-event checks, validation and saving. New routing must use this same entry path for each actual step.

The accepted region, six towns, grassland roads, farms, two road exits per town, six daily actions and approximately five-day nearest-town journeys stay in place. World regeneration is outside this phase.

## Proposed workflow

1. Select a destination to preview a route, action cost and an estimated number of daily movement budgets. Preview does not move the company or roll an event.
2. Confirm travel along that route. Spend actions one adjacent step at a time through native movement. Stop at zero actions, arrival, a battle/event, invalidated route or player cancellation.
3. Preserve the selected destination across a day boundary and save/load. The player still explicitly ends the day. Resuming travel is deliberate.
4. Recompute when newly discovered terrain or changed objectives invalidate the route. A blocked/unreachable destination produces a useful status without spending actions.

Use weighted pathfinding over actual action costs. Roads remain ordinary one-action terrain. Off-road movement remains freely available. Route previews must not reveal undiscovered towns, terrain or enemy data through hidden-cost estimates.

## Decisions before implementation

- Choose click behavior that preserves convenient adjacent movement while distinguishing preview from commitment. Recommendation is a destination preview with an explicit Travel control, with an adjacent-tile shortcut if the user wants it.
- Choose the unknown-terrain rule. Recommendation is routing through discovered tiles and toward a frontier destination, with exact contract markers selectable under the existing fog rules. Routes into unknown terrain need explicitly uncertain costs if supported.
- Confirm whether a post-event continuation requires one explicit Resume action. Recommend stopping after any interrupt so new information can be reviewed.

## Persistence and interruption safety

Persist destination and necessary route intent, with a schema default of no route for older saves. Derived path segments can be recomputed. Native position and action spending remain authoritative. Save event/battle transitions exactly once. A load cannot repeat an encounter roll for a step already committed or advance a tile twice. Ordinary travel must not become a chain of per-frame full saves beyond the existing intended checkpoint cadence.

## Planned grouped acceptance

Check road and rough-terrain costs, fog, exact objective markers, zero actions, manual End Turn, cancellation, changed objectives, travel-triggered events, battle deployment, save/load at interruptions and large-map responsiveness. Use generated synthetic regions and existing native movement. Calibrate actual journey time before 40F. One player handoff covers route preview, multiday continuation and interrupted travel.
