# 05 — Retire the 4X loop and preserve tactical combat

**Status:** Proposed specification. Planning only; execution has not started.

**Phase:** Phase 2 — retire unwanted systems

**Prerequisites:** 01 boundary approved; 02–04 completed or explicitly coordinated.

## Purpose and scope

Study 01A identified navigation dependencies to replace before retiring StrategyMode: GameManager.SwitchToMainMenu cleans strategic windows; GameManager.CloseMenu touches strategic controls; GameMenu.MainMenu/CloseMenu clear or regenerate strategic state; GameManager.SwitchToStrategyMode owns both campaign battle return and the separate Pure Tactical return-to-main-menu branch. See [packet 01A](../architecture/01a-navigation-and-settings.md). The combat/result audit in 01C must trace the callers before choosing the replacement boundary.

Study [01C](../architecture/01c-combat-equipment-and-results.md) traced campaign entry through diplomacy/building/Empire/Village setup and battle finalization through VictoryCheck, traveling armies, territorial claims, world statistics, bounty/loot, and StatScreenPanel return callbacks. Replace these owners together; keeping only the tactical classes is not isolation. Actor_Unit wraps the supplied Unit references, so a temporary battle Army must have its own deployed-member collection rather than the canonical roster. AI still requires side/hostility, night, retreat, race-policy, and power-estimate context; preserve those through reviewed providers. Packet 01D must reconcile the proposed battle/result boundary with saves and stable identity.

- Disconnect traditional empire-campaign creation and retire strategic AI turns, ownership, diplomacy, research, buildings, population/economy, multiple player armies, and conquest results from the active game.
- Separate battle initialization and results from empire buildings, village/garrison effects, strategic reports, income, and empire victory checks.
- Retain reusable grid/terrain generation, units, stats, trait runtime, combat logic, presentation, and tactical AI. Use a small compatibility layer where justified by the study rather than rewriting them.
- Preserve Pure Tactical setup, AI-versus-AI, and bulk matchup tools with curated species filtering; provide clear unavailable-state feedback while the roster is empty.
- Retire old recruitment/training/potion entry points and systems in the reviewed dependency order. Shared item/stat infrastructure remains available for later replacement.
- Inventory residual legacy structures required by tactical combat. Remove unnecessary behavior; schedule remaining internal dependency work explicitly instead of calling a hidden legacy simulation 'removed'.
- Carry [the clarified combat decisions](../design-clarifications-2026-10-30.md) into the cleanup dependency order: permanently deprecated resurrection/replacement, permanent animal polymorph, excluded conversion and spontaneous allegiance changes must not be revived by retained hooks. Temporary clone/illusion summons remain eligible. Downed/temporary-HP/result changes belong to the reviewed combat batches in 10/11; define their shared health/save boundary before the first batch that depends on it.

### Preparation from study 01D

Use [the final boundary study](../architecture/01d-campaign-persistence-and-identity.md) and [ordered manifest](../architecture/01d-removal-sequence-and-risks.md). StrategyMode.Regenerate performs army pruning/income/auto-level work; Army.Refresh includes movement, equipment, village and mana effects; ProcessIncome can auto-dismiss units and zero HP. Do not retain those as harmless redraw/recovery/wage helpers. State.Load and GameManager infer skirmish from MainEmpires being null and can restore strategic mode before tactical data; replace mode/load/return ownership together. A compatibility World/Army may supply reviewed terrain/item/battle context, but cannot be a hidden empire loop.

## Completion and review criteria

- No active menu starts a traditional 4X campaign or runs the retired empire simulation.
- Tactical entry/exit no longer requires live strategic ownership or a village economy.
- The cleaned project opens and compiles with a clear temporary state for the future campaign.
- Retained combat regression scenarios use 00 as reference; any cases blocked by an empty roster remain visible and must be completed after 07.

## Decisions, constraints, and risks

- A clean slate means retired features cannot run; it does not require erasing every shared class name.
- Do not create the new company economy or campaign rules inside this cleanup spec.
- Cleanup completion includes a residual-dependency report, not just hidden buttons.

## Deliverable

A clean playable application shell/Pure Tactical path, a tactical integration boundary, and the Phase 2 cleanup review.

## Execution record

- Source trace and exact change locations: pending.
- Implementation batches and user review: pending.
- Checks actually performed and remaining checks: none for this spec yet.

Before execution, refine this spec with the architecture study, list its small change batches, and resolve decisions needed for the next batch. Follow the review and file-operation rules in [the roadmap](../conversion-roadmap.md). Design intent comes from [the campaign plan](../mercenary-company-campaign-plan.md) and the three curation checklists.

