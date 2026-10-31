# Total Conversion Roadmap

**Status:** Roadmap approved 2026-10-29; resumed 2026-10-30. The user accepted 03B2. [03B3A](implementation/03b3a-override-hydration-boundary.md) isolates retired override records during loading and corrects new leader/morph defaults; compilation/scene/scope checks passed, interactive load/persistence review pending. Stat/graphics/resource retirement and 05 providers remain. No species approved. Exact recovery state is linked from AGENTS.md.

## Direction and authority

Build the conversion in distinct reviewed stages: establish the original-game reference, study dependencies, retire the agreed unwanted features, then build the mercenary-company campaign. Keep Unity 2018.3.12f1 and the retained tactical mechanics/AI as the technical starting point.

The [campaign design record](mercenary-company-campaign-plan.md) is the source of gameplay intent. The [species](species-curation-checklist.md), [options](options-curation-checklist.md), and [screen](screen-curation-checklist.md) checklists supply the exact curation decisions. This roadmap assigns that work to specs; it does not replace those lists or approve unchosen species or balance formulas.

Specs are ordinary Markdown in this repository. No OpenSpec installation or external service is needed. Each spec has prerequisites, bounded scope, completion criteria, open decisions, and an execution record. The architecture study must refine the later specs with actual source/scene dependencies before their implementation.

## Phases and review points

| Phase | Specs, in recommended order | Result presented for review |
|---|---|---|
| 0 — Establish the reference | [00 — Editor and original-game baseline](specs/00-editor-baseline.md) | Known editor/import/compile/launch state and an original-game reference. |
| 1 — Comprehensive architecture study | [01 — Architecture, dependencies, and removal order](specs/01-architecture-study.md) | Dependency map, source-linked inventories, safe removal order, and refined implementation batches. |
| 2 — Retire unwanted features and content | [02 — Retire unwanted menus and lock balance settings](specs/02-settings-and-screen-retirement.md); [03 — Disable the legacy roster and retire rejected species](specs/03-species-retirement.md); [04 — Reset legacy prose and retire 4X events](specs/04-narrative-and-event-reset.md); [05 — Retire the 4X loop and preserve tactical combat](specs/05-strategic-retirement-and-combat-boundary.md) | A clean application shell and retained tactical boundary, with a manifest of retired content and any justified residual dependencies. |
| 3 — Build the first company campaign loop | [06 — Company campaign state, world, clock, and saving](specs/06-campaign-foundation.md); [07 — Curated starters, company members, recruitment, and wages](specs/07-leader-recruitment-and-wages.md); [08 — Expanded equipment and town commerce](specs/08-equipment-and-shops.md); [09 — Contracts, reputation, and procedural enemy power](specs/09-contracts-and-enemy-generation.md); [10 — Deployment, battle results, revival, and memorial](specs/10-battle-results-and-company-fate.md) | Create a leader, recruit, equip, accept work, travel, deploy, fight, receive rewards, manage wages, save, and continue or lose the company. |
| 4 — Complete the intended character experience | [11 — Persistent injuries, recovery, and downtime](specs/11-injuries-rest-and-progression.md) | Persistent injuries, treatment, daily recovery, downtime sparring, and multi-day rest. |
| 5 — Integration, balance, and help | [12 — Integrated campaign review, help, and first release candidate](specs/12-mvp-review-and-help.md) | Reviewed campaign proof of concept and Pure Tactical, save/Ironman coverage, help, known limitations, and next-work list. |
| 6 — Later campaign expansion | [13 — New road events and ambushes](specs/13-new-random-events.md) | Newly designed road events and ambushes. Timing remains open; legacy 4X events never return. |

The first playable loop in Phase 3 is an intermediate milestone. The intended injury/recovery experience is not complete until Phases 4–5. Avoid labeling intermediate screens or systems as finished simply because their navigation exists.

Within Phase 2, study findings may change the order of settings, species, and text retirement. Phase 3 foundations may also need small compatibility changes identified in Phase 1. Record those changes in the specs and explain material tradeoffs before implementation. Do not expand the work into an engine upgrade or a rewrite of retained combat.

## Phase 1 study packets

Work through these reviewable packets within spec 01 rather than producing a single unexplained architecture dump:

1. **Startup and screens:** mode transitions, base-scene wiring, settings persistence, Race Editor, and retired entry points.
2. **Species and content:** every species generation/filter route, numeric identities, external content, narrative selection, and legacy event triggers/choices/effects.
3. **Units and tactical combat:** stats, traits, inventory, AI, terrain, battle setup, surrender/downed, vore/digestion, retreat, outcomes, and results.
4. **Campaign and persistence:** old strategic turn ownership, world generation, saves, canonical member identity, proposed company boundary, and ordered daily updates.

For each important behavior, show the chain from player action to state update to display. Record the actual scripts, methods, scene/prefab references, settings, and runtime data involved.

Expected study deliverables are an architecture report, a keep/adapt/retire manifest, a risk/decision register, and source-based amendments to specs 02–13. Each manifest row identifies what is retired, how it is reached, what still depends on it, how it is disconnected/replaced, and how that change will be checked.

The study is complete when it explains the important migration paths and supports small implementation batches. It need not document every unrelated asset or every line of the original game.

## Meaning of a clean slate

Cleanup is functional retirement in the reviewed dependency order. Old player entry points, generation routes, callbacks, settings overrides, event triggers, and campaign simulation must cease to run where their removal is agreed. Hiding a button alone is insufficient.

Shared code may survive temporarily if retained tactical combat still needs it. Record the reason, owner, and later replacement point. Do not remove a shared data structure solely because it has an empire-era name.

Keep the underlying Trait system and approved runtime behavior while retiring its unwanted authoring screens. Removing a settings control does not automatically retire its mechanic. Defaults and persisted overrides require explicit treatment so hidden balance controls cannot keep influencing the game unexpectedly.

All existing species become unavailable before curation. During that intentional empty-roster stage, the editor/application/menu checks can pass while battles cannot start. Show a safe unavailable-state message; do not enable an unapproved species for convenience. Record battle checks as pending, then perform them as soon as the first approved species is curated in 07. Phase 2's cleanup review must disclose this limitation.

All old 4X event content is retired, though useful generic event machinery may be reused later. Existing vore/digestion prose is reset to neutral functional placeholders; the user will supply replacement narrative. Neither action authorizes changing retained combat mechanics.

### File-operation constraint

AGENTS.md prohibits deleting files or directories. These specs use edits, disconnection, and retirement from active code/content. If completing a later batch requires removing a whole file from active use, follow AGENTS.md and obtain the required specific direction then; a project Recycle Bin move is available when appropriate and authorized. Never use destructive cleanup, drop licenses/attribution, or automatically remove Unity-generated paths.

The user's request to plan removal is not permission to delete paths now. No file moves or deletions are included in this planning work.

## How a spec is executed

1. Confirm prerequisites and the exact source/scene trace; resolve only decisions needed for the next batch.
2. State that batch's scope and intended visible result. Keep changes small enough to review separately.
3. Implement the authorized batch and inspect its diff, including serialized Unity changes and preserved user work.
4. Perform only the checks authorized for that stage. Agree the relevant compile/build and in-game checks when starting it; the checklists here describe required evidence, not checks already run.
5. Report what changed, checks actually performed, remaining limitations, and a short player-facing checklist.
6. Wait for user feedback on that stage before beginning the next, following AGENTS.md.

Use Git checkpoints according to the agreed repository workflow. Do not silently commit unrelated changes. Record a spec as complete only when its criteria and pending checks are satisfied; record blocked checks explicitly.

## Decisions to settle when they become relevant

| Decision | Needed by | Why it matters |
|---|---|---|
| Any discrepancy between a listed settings-control removal and removal of its mechanic; final legacy Fog of War page disposition | 02/04 | Prevents menu cleanup from changing combat without a design decision. |
| Developer password for Race Editor | 02 | Completes the convenience gate; keep it out of reports. |
| First user-selected species and their sapient/animal classification | 07 | Enables real generation/battle checks after the initial empty roster. |
| Safe empty species pools and preserving existing individuals after disabling new generation | 03 design; 07/12 integration | Policy is settled: preserve every existing individual, including offered/former mercenaries and contract enemies. No retroactive removal. |
| Campaign map dimensions, action budget, terrain costs, feasible battle saves and turn-by-turn Ironman backups | 06 | Battle saves are desired with between-battle fallback acceptable; assess both campaign/tactical checkpoints. Service costs can remain zero and configurable. |
| Recruit refresh, wages/level increases, prorating, initial prepayment, and debt tolerance | 07 | Unaffordable payday recipient choice is settled: debts first, then explicit player-selected group or individual payment; never automatic prioritization. |
| Nine agreed equipment slots: compatibility, armor coverage, HP adjustment safety, and basic item prices | 08 | Helmet/torso/legs/arms/accessory/melee/ranged/two spells; visuals not required. Review symmetric maximum/current HP changes. |
| Power scale/weights, rating tolerance, deadlines, reward variation, offer refresh, and reputation progression | 09 | Turns the procedural contract idea into tunable rules; does not require exponential reputation growth. |
| Generous stalemate turn limit, retreat destination, nearest-town selection, insufficient revival funds, and ordinary loot | 10/11 | No abandonment state: simulate actual outcomes. Both sides inactive or a stalled battle trigger stalemate; survivors flee. Fled enemies count defeated even on a draw. Leader revival/game over is digestion-only. |
| Temporary HP caps/injury interaction, injury chances/penalties, recovery, healthy-member eligibility, treatment, and survivor HP baseline | 10/11 | Temporary HP absorbs damage first and clears after battle; vore heals lasting HP. Rest interrupts for warnings/required choices. Exact numeric tuning is deferred. |
| New event content, frequency, triggers, and priority | 13 | Retains the desired random-event capability without carrying over legacy 4X events. |

These are checkpoints for later work, not a request to answer them all now. Most numerical values should be developer-tunable and reviewed against the intended experience.

The [2026-10-30 clarification record](design-clarifications-2026-10-30.md) contains the full 24 answers and follow-up decisions, including permanent deprecation of resurrection/conversion effects, downed targeting/cleanup, temporary summons, and the resolved no-abandonment/stalemate/contract rules. Recording these decisions is documentation work, not implementation or completion of packet 01D.

## Evidence and present limits

The project records Unity 2018.3.12f1. Spec 00 has now verified import/compilation and a Windows player build with that editor, and the user confirmed successful main-menu startup. Broader gameplay checks remain pending; automated window capture still times out. See [the baseline report](unity-baseline-report.md).

Study packet 01A traced startup/navigation and settings. Packet 01B mapped all 90 rejected species names and traced content/generation/narrative routes. Packet 01C traced combat/equipment/results. Packet [01D](architecture/01d-campaign-persistence-and-identity.md) now covers campaign/day ownership, generation/discovery, identity, normal/tactical saves and Ironman feasibility. The [consolidated removal manifest, order and risk register](architecture/01d-removal-sequence-and-risks.md) reconciles the four packets and subsequent design answers. The study is prepared for review. 02C resolved the earlier unmatched settings bindings and relevant inherited callbacks; shared-resource ownership remains a preflight for later content retirement. This does not mark implementation, baseline gameplay testing, or all possible dependency paths complete.

Authoring the original roadmap changed documentation only. Spec 00 performed the recorded import/build checks. Packet 02A is the first source-change batch; no package upgrade or gameplay-rule change is included.

## Recommended next stage

Review [03B3A's editor-load and mapping-persistence check](implementation/03b3a-override-hydration-boundary.md). After feedback, refine **03B3B stat-definition/read-boundary retirement** with direct RaceSettings.Get and RaceParameters callers; retained getters and eager prototypes are not retired by the new Sanitize guards. Review the two remaining owners whose built-in mappings point at retired species without selecting substitute species. Keep graphics/shared-clothing work separate, numeric identity stable and supported existing disabled individuals intact; coordinate actual 4X callers with 05. No prototype removal, playable fallback, sample species or bundle rebuild has occurred in 03B3A. Recovery state is in [the interruption checkpoint](implementation/resume-checkpoint.md).
