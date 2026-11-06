# 40F — Calendar-driven difficulty progression

Planning draft, 2026-11-06. Final phase of [module 40](40-authoring-and-campaign-progression.md). Numerical curves and the specific pressure mechanism remain open.

## Confirmed creative direction

The user wants an open-ended roguelike in which expenses and enemy difficulty eventually outpace the company. Compulsory upkeep already starts positive and increases by a flat weekly amount without a cap. That accepted economy must be included in difficulty analysis. A permanent safe, cost-free leader-only campaign is outside the intended experience.

## Current boundary

`CompanyContracts.EnsureTown` generates contract difficulty from reputation, currently capped at rating 20, with fixed rating-based force budgets and quoted rewards. Calendar-driven enemy escalation is absent. `CompanyRoadEvents` scales encounters to company capability with extra leader weight and the accepted capped health reduction. Random road encounters occur during movement. Rest already advances wages, upkeep, deadlines, healing and refresh systems.

Do not change a rating's meaning or rescale an existing contract roster to match the company. Existing accepted targets, quotes, casualties and healing remain persistent. Calendar pressure must be a separate disclosed input into newly generated content.

## Design and calibration sequence

1. Measure the combined baseline after route planning, spell progression and earned traits. Record actual travel/recovery cycles, contracts per week, replacements, gear investment, elective level-ups, sparring, wages and upkeep.
2. Present a small set of pressure models for approval. Candidates include a rising range of available contract difficulties, an elapsed-time component to new road-encounter strength, or a combination. These are alternatives to evaluate, not approved rules.
3. Define early-game runway, visibility and which easier opportunities remain. Reputation must continue to matter for contract and recruit access. Decide explicitly how calendar pressure interacts with a company whose reputation or roster has fallen. Review the current rating ceiling and generator limits before proposing unbounded later pressure. Raising a ceiling must preserve each existing rating's power meaning.
4. Calculate candidate curves and test connected native campaign policies. Include ordinary growth, conservative play, low-rating farming, delayed leveling, leader-heavy parties, casualties/replacement, field/town recovery and inactivity. Use multiple seeds and report distributions rather than only a winning example.
5. Choose provisional values from those results and explain likely time-to-pressure and insolvency. Tune the pressure mechanism without silently changing established upkeep or armor rules.
6. Review readable calendar/offer information and one grouped campaign experience. Future numerical changes should use the official balance-authoring workflow where those parameters are supported.

## Persistence and fairness

Use elapsed campaign days as an explicit monotonic input. Travel automation, rest skips and loading must process the same boundaries once. Tactical turns consume zero campaign time. Preserve previously generated offers until their normal refresh/expiry rules act. Stamp generated content with the relevant rules revision and pressure state for diagnosis.

Decide how older campaigns enroll before implementation. Avoid silently applying a late-game difficulty jump on load. A conversion policy might use a grace interval or enroll at a defined epoch, but requires curator approval. Restore historical saves according to the shared compatibility policy rather than reinterpreting recorded enemy rosters.

## Planned grouped acceptance

Check day/week boundaries, multiday rest, route continuation, refreshes, deadlines, save/resume and older-campaign enrollment. Confirm fixed-rating power consistency, accepted-roster stability, reputation gates and new encounter bounds. Connected simulations must include both losses and recoveries and finish through the history pipeline. No infinite-survival guarantee or universal fixed defeat date is implied by a finite simulation sample.

## Implementation gate

Approve the pressure model, early-game runway and old-save enrollment after baseline results are available. No calendar curve is chosen by this planning draft.
