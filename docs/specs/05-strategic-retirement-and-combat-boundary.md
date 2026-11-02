# 05 — Retire the 4X loop and preserve tactical combat

**Status:** Functional retirement milestone complete, 2026-11-01, with final agent checks passed as recorded in the [closure report](../implementation/phase-2-cleanup-closure.md). Earlier packet records remain historical. Combat/log scenarios await approved species; distribution/shared-asset review remains a release gate.

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

- 05A1: five-source application/save boundary implemented; exact scope and limits in the report.
- Implementation batches and user review: pending.
- 05A1 fresh compilation and explicit scene load passed. Menu checks pending; no nonempty combat or new save-format tests.

Before execution, refine this spec with the architecture study, list its small change batches, and resolve decisions needed for the next batch. Follow the review and file-operation rules in [the roadmap](../conversion-roadmap.md). Design intent comes from [the campaign plan](../mercenary-company-campaign-plan.md) and the three curation checklists.


05A1 manual checks accepted. [05A2](../implementation/05a2-strategic-scheduler-retirement.md) retires the strategic scheduler/input/regeneration boundary in two sources, preserving Pure Tactical return and shared helpers. Final checks and user review are recorded in its report; remaining provider/result internals are not complete.

05A2 manual checks accepted. [05A3A](../implementation/05a3a-campaign-battle-entry-retirement.md) retires the unused campaign World constructor and campaign battle transitions; direct tactical setup is gated to Pure Tactical. Retained battle/result logic is unchanged. Remaining providers/results and actual battle verification are separate unfinished work.

05A3A accepted. [05A3B1](../implementation/05a3b1-post-battle-replacement-retirement.md) retires the post-battle replacement/reincarnation/village-copy result paths in TacticalMode only. Other fate, revival, travel/reward and provider paths remain unfinished.

05A3B1 manual smoke check accepted. [05A3B2](../implementation/05a3b2-battle-result-territory-retirement.md) removes campaign statistics/claims and the village-conquest result callback/helper. Standalone village battles and summaries remain. Relocation/rewards, other revival paths and providers still require separate packets.

05A3B2 manual check accepted. [05A3B3](../implementation/05a3b3-post-battle-relocation-retirement.md) retires town-retreat choices and traveling/reforming-army result helpers. Existing default winner rejoin and participant accounting are retained; new company retreat/fate rules remain future work. Rewards and other provider/revival paths still need closure.

05A3B3 manual check accepted. [05A3B4](../implementation/05a3b4-battle-reward-retirement.md) retires empire bounty payment and army/village loot transfers from battle results, preserving tactical counters and standard summaries. Company contract payout remains future work. Review remaining revival effects and combat/provider dependencies before claiming full result or species-data isolation.

05A3B4 manual check accepted. [05A3B5](../implementation/05a3b5-post-battle-survival-trait-retirement.md) retires the two remaining post-battle survival-trait revival branches (Eternal/LuckySurvival/Reformer/Revenant/DeathCheater and CloseCall). Other spell, on-death and digestion/conversion paths remain separate work; trait metadata/IDs remain.

05A3B5 manual check accepted. [05A3B6](../implementation/05a3b6-resurrection-spell-retirement.md) retires Resurrection spell execution and filters it from assembled usable spells, preserving ID/metadata and stored grants. Reanimate and other on-death/digestion resurrection/conversion require separate review; temporary summons remain outside this packet.

05A3B6 manual check accepted. [05A3B7](../implementation/05a3b7-respawns-runtime-retirement.md) retires Respawns grants and weapon/spell/digestion resurrection triggers, plus CloseCall casualty-presentation remnants. Ordinary death/surrender/digestion paths remain; no new fate rules. Reanimate, digestion conversion and old provider/catalog paths remain review work.

05A3B7 manual check accepted. [05A3B8](../implementation/05a3b8-digestion-conversion-retirement.md) retires four digestion/absorption allegiance conversion and species rebirth branches, retaining ordinary digestion/absorption effects. Other allegiance/callback/provider paths and Reanimate remain separate review; this is not global conversion closure.

05A3B8 manual check accepted. [05A3B9](../implementation/05a3b9-unused-resurrection-helper-retirement.md) removes unused original-character Resurrection utility/picker routines and AI planning/action calls following spell/Respawns retirement. Reanimation and other spells/summons remain unchanged; broader allegiance/provider/catalog closure remains unfinished.

05A3B9 manual check accepted. [05A3B10](../implementation/05a3b10-reanimate-spell-retirement.md) retires Reanimate casting and its Reanimator grant because it mutates the original casualty rather than creating a separate temporary clone. Bind and shared reanimation helpers remain separate review; other temporary summons are unchanged.

05A3B10 manual check accepted. [05A3B11](../implementation/05a3b11-bind-resummon-retirement.md) retires Bind/Resummon spell entry points, Binder grant and actor capture/re-summoning routines. Stored identities remain; separate temporary summons are unchanged. Shared reanimation utility/UI/AI are now next cleanup; other allegiance/provider/catalog paths remain.

05A3B11 manual check accepted. [05A3B12](../implementation/05a3b12-unused-reanimation-bind-helper-retirement.md) removes obsolete Reanimate/Bind utility/picker/AI and roster-mutation helper code. Serialized picker/BoundUnit identities remain; other summons are unchanged. Remaining allegiance callbacks/providers/catalogs still need closure.

05A3B12 manual check accepted. [05A3B13](../implementation/05a3b13-damage-surrender-defection-retirement.md) retires damage-triggered TurnCoat and automatic-surrender defection/full-health recovery. Existing surrender trigger and 1 HP result remain; dedicated Downed/HP work is not part of this packet. Other charm/allegiance/callback paths remain.

05A3B13 manual check accepted. [05A3B14](../implementation/05a3b14-allegiance-spell-retirement.md) retires Capture Net/Charm/Hypnotic Gas/Whispers casting and automatic charm/hypnosis grants, including a retired direct-status-cast guard for trait callbacks. Status/trait/containment allegiance paths remain separate review.

05A3B14 manual check accepted. [05A3B15](../implementation/05a3b15-support-trait-allegiance-retirement.md) retires SeductiveTouch support-action conversion/distraction and its permission exception. Underlying support remains. Possession/metamorphosis/containment allegiance callbacks require separate packets.

05A3B15 manual check accepted. [05A3B16](../implementation/05a3b16-metamorphosis-family-retirement.md) retires the three metamorphosis-family callbacks/escape restrictions while retaining class/registry IDs. Possession/containment callbacks remain separate review.

05A3B16 accepted. User requested consolidation. [05A3B17](../implementation/05a3b17-consolidated-ability-retirement.md) completes the approved ability-retirement pass with shared activation guards, possession/containment/status/legacy revival closure and Leadership removal. One combined manual check follows agent verification; return to provider/species boundary review afterward. New Downed/HP/company systems remain separate.

05A3B17 combined manual check accepted. [05A4](../implementation/05a4-campaign-combat-provider-retirement.md) retires empire-trait, strategic-building combat/results and campaign race-defection providers as one grouped stage. Scenery/village skirmish and ordinary summons remain; shared battle/AI/map/save compatibility providers still require residual review.

05A4 manual check accepted. [05A5](../implementation/05a5-world-army-lifecycle-retirement.md) retires unused world/monster construction and Army daily village/recovery/training/feeding pipelines as one group. The report inventories retained battle/map/save providers; compatibility fields and tactical helpers remain. Final provider/species boundary review is still required.

05A5 manual check accepted. [05A6](../implementation/05a6-battle-side-diplomacy-isolation.md) retains standalone battle side/hostility/visibility semantics without empire diplomacy, removes campaign-only retreat/skip/color providers and shared Unit strategic XP cheats/trait injection. Residual map/save/context/species boundaries remain before phase-2 completion.

05A6 manual check accepted. [05A7](../implementation/05a7-residual-boundary-and-metadata-review.md) consolidates residual active/retired/shared provider dispositions and removes dummy Unit creation from metadata reads. Return to spec 03 stat/override closure; this does not mark phase 2 complete, remove reusable map code or restore legacy save loading.


## Consolidated cleanup milestone closure (2026-11-01)

See [phase-2 cleanup closure](../implementation/phase-2-cleanup-closure.md) for current accepted work, exact retained services and deferred combat/release gates. Earlier packet status lines are historical. No further per-packet menu tests are required; final milestone verification status is in that report. New campaign/character mechanics remain subsequent specs.
