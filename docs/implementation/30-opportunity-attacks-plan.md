# Opportunity attacks — feasibility and proposed implementation

Recorded 2026-11-03. **Planning only; no implementation authorized.** The user accepted the Spec29 tactical spacing/hover corrections before raising this feature.

## Requested behavior

- Movement by a unit next to an enemy provokes an instant, free ordinary melee attack from that enemy. Apply symmetrically to player and AI, in company battles and Single Battle.
- No per-turn reaction limit or AP cost. Spending normal AP must not stop a unit from reacting later.
- Ordinary melee numerical rules: equipped melee weapon or existing unarmed/natural fallback, normal accuracy/damage/evasion, the Spec29 miss-rescue graze and INT critical rules. Do not substitute a flat damage amount or automatic hit.
- A reaction commits the action: Undo must not allow repeatedly attempting movement until the attack misses. Missing still commits it.
- Only ordinary move commands provoke, including ordinary flying movement. Pounces/leaps, teleports, edge Retreat, forced displacement and non-action relocations do not provoke.
- Opportunity attacks cannot cause automatic knockback. Normal attacks retain their knockback behavior.
- The purpose is to give frontline melee presence, punish passing enemies to reach vulnerable backliners, and prevent ranged units from freely disengaging to shoot.

Interpretation to preserve: test adjacency at the start of each movement step. This includes moving around an enemy while still adjacent, not just leaving its reach. Re-entering then moving again can provoke again; every eligible adjacent enemy reacts, with no artificial reaction cap. Merely approaching from outside adjacency does not itself provoke under this interpretation. Existing ordinary melee adjacency is eight neighboring tiles, including diagonals (`Vec2i.GetNumberOfMovesDistance`); exclude overlapping/same-tile actors.

## Feasibility findings

This is feasible in the current engine. It is a moderate combat integration task, not a new battle system, but is broader than adding one movement callback.

| Area | Current wiring | Consequence |
| --- | --- | --- |
| Ordinary movement | `Actor_Unit.MoveTo`, around2584, validates tile/AP then sets position and deducts tile cost | Shared hook for player, enemy AI and simulated battle movement |
| Player paths | `TacticalMode` queued-path update, around2945; `OrderSelectedUnitToMoveTo`, around3657 | Resolve each actual step, not one reaction per click/path; stop queued movement after incapacitation/displacement |
| Enemy/auto-resolve movement | `TacticalAI` path execution, around250–315, calls `MoveTo` | Shared combat rule applies; interrupt handling must stop stale path actions rather than invoking a planned attack after a failed move |
| Normal melee | `Actor_Unit.Attack`, around1641; `ResolveWeaponHit` and `ResultDamageMultiplier`, around3190 | Reuse these rules; currently rejects0AP and spends AP, so needs an explicit reaction execution context |
| Undo | `TacticalMode.PlaceUndoMarker`/`UndoMovement`, around360–377; button state around3272 | Undo rewinds position/AP only. Guard the method, button, hotkeys and implicit undo paths; hiding/disabling the button alone is insufficient |
| Special movement | `MeleePounce`/`VorePounce` call `SetPos`; Dimension Shift and Curse of Phasing also relocate directly | These are intentional exceptions under the user's move-command-only rule; keep them separate |
| Forced displacement | `TacticalUtilities.KnockBack`/`SpellKnockBack` call `Actor_Unit.Move`, which delegates to `MoveTo` | Blindly applying all movement hooks includes knockback and can recursively trigger more attacks |
| Pathfinding | `TacticalPathfinder` scores steps/terrain; `TacticalAI` chooses targets/routes | No opportunity-attack risk model exists; avoid treating damage risk as extra AP cost |
| Save/load | `TacticalMode.CanSaveCompanyBattle` already excludes queued movement but has no reaction-resolution state | Do not save midway through a reaction batch; autosaves and normal saves must see settled state |

`SetPos` is also used for setup, containment release, copies, swaps, restoration and Undo. Do not attach reactions indiscriminately to this low-level setter. Keep setup/restoration separate from movement actions.

## Proposed execution

1. Validate the requested movement destination and normal movement cost before rolling any attacks. Invalid/no-op movement must not create free attacks or spend random rolls.
2. At the old tile, collect eligible adjacent hostiles in deterministic order (stable saved actor-list order is sufficient initially). Use existing combat hostility rules rather than a new alliance interpretation. A unit that merely spent all its AP remains eligible; downed, dead, fled, contained or incapacitated units cannot make reactions. Visibility alone is not a new reaction cap.
3. Invalidate existing Undo **before** the first reaction roll, even if all attacks miss. After the movement/reaction transaction settles, establish a fresh baseline at the resulting position/AP, matching the ordinary attack/spell commit behavior. Do not permit automatic Undo to rewind a reaction through selection changes, action-mode changes, right-click or occupied-tile correction. This is not a blanket prohibition for the rest of the unit's turn; later safe movement follows ordinary Undo rules but cannot cross the committed reaction.
4. Resolve one normal melee attack per eligible enemy, before the mover leaves its tile. Use explicit free-reaction handling in the shared attack pipeline; do not temporarily grant AP and blindly restore it afterward. Avoid the player-command wrapper, which changes action mode/selection/Undo. The reaction must preserve the attacker's turn/AP budget without undoing legitimate damage/incapacitation effects.
5. Recheck participants after each reaction. Stop if the mover is downed, swallowed, removed, displaced or otherwise unable to continue; do not let remaining enemies directly attack a downed target, per existing rules. Stop or revalidate the planned movement if an on-hit effect changes positions/status. Do not restore AP to an attacker made downed by retaliation.
6. If movement remains valid, perform the step and its normal AP cost, then continue any queued path. An attack that merely hits need not stop movement. An interruption must be distinguishable from an ordinary blocked step so the AI does not invoke an obsolete destination action.
7. Sequence reaction animations and the walk so attacks visibly happen before departure, including multiple adjacent enemies. The numeric result must match auto-resolve, where the animation delay can be skipped. Log the final hit/miss/graze/critical as an opportunity attack without extra failed-roll messages.

Normal weapon effects, armor, injuries, on-hit effects and XP should use the ordinary attack behavior by default, consistent with the user's request for an ordinary attack. No vore action is substituted for the reaction. Audit exceptional attack callbacks (Dazzle, force-feed, sweep, knockback, retaliation) for re-entry and movement cancellation. This is not authorization to restore retired traits.

Explicit reaction exception: disable automatic knockback for opportunity attacks, including any secondary attack invoked in their execution context. `ForcefulBlow` currently has both a damage-stage `CheckKnockBack` call (which can add blocked-knockback damage) and the on-hit `KnockBack` call. Gate the knockback behavior consistently rather than suppressing only the visible displacement while leaving a collision bonus from an attempt that cannot happen. Keep ordinary attacks unchanged. Forced movement must also be identified at shared movement entry points so ordinary knockback does not accidentally become a provoking move command.

## User decisions — 2026-11-03

1. **Knockback:** opportunity attacks must not cause it; automatic-knockback chances/effects are zero for reactions. Forced movement is outside the move-command trigger rule.
2. **Exceptional movement:** the user rejected provoking on pounces/leaps and similar exceptional abilities. Restrict triggers to move commands. Teleports and edge Retreat likewise are not ordinary move commands; no universal relocation hook. Ordinary flying steps still count as move commands.
3. **Undo:** invalidate existing Undo when an incoming opportunity attack occurs, matching how attacking/casting already commits movement. Do not impose the previously suggested full-turn lock. Implementation must commit through the reaction and resulting step, not just establish a baseline before rolling and then allow that same step to be undone. A later safe move can be undone only back to the post-reaction baseline.

These decisions supersede the initial recommendations concerning exceptional movement and a full-turn Undo lock. The user agrees with the remaining proposed approach and AI risk awareness, with the additional offensive-chase policy below. This remains planning approval, not a request to implement. No remaining creative decision is being treated as a blocker to a later implementation request; collision/interruption details must preserve these rules.

## AI and UI scope

- Teach route/target selection about threatened departures, so the AI can choose to fight an adjacent unit rather than repeatedly walk through lethal reactions to reach the weakest backliner. Risk can influence route choice without changing displayed AP or terrain cost. Allow deliberate risky charges when worthwhile; do not make threatened tiles impassable. The precise user-directed chase policy follows below.
- Preserve actual rule enforcement during enemy AI, AI: Finish Turn and Auto-resolve Battle. Do not resolve opportunities only in mouse-input code.
- Include lightweight warning/path feedback when a chosen route provokes, instead of a confirmation popup for every step; the user agreed with this proposed direction. No broad UI redesign is implied.
- Handle interrupted movement on transiently occupied tiles used by Flight/pass-through. Current implicit Undo corrects such states, but it cannot rewind committed random combat. Choose a valid stopping/collision policy before relying on those paths.

### Approved AI chase policy

Use three independent checks for tactical target selection:

- **Mage:** INT > STR.
- **Archer:** AGI > STR.
- **Vulnerable sapient:** a sapient with no actually equipped melee weapon. This is an additional equipment-based reason to rush the unit, regardless of the stat checks and without requiring a bow or spell. Its unarmed/natural fallback is not an equipped melee weapon for this check. Animals do not qualify for this equipment-only vulnerability merely because they have no manufactured weapon.

The two stat checks are strict and independent: if both INT and AGI individually exceed STR, the unit is both a mage and an archer. Equality does not satisfy that stat check. The equipment check is additional, not a replacement for either stat check and not a new requirement for them. A unit can satisfy any combination of the three. Do not implement these as mutually exclusive categories or require the larger of INT/AGI to win.

Use combat-relevant stats and actual equipped melee-weapon presence, not a generated name/archetype or the current AI's `IsRanged` classification. These checks guide charge priorities and do not change species roles, AP or attack availability. The no-melee-weapon check identifies vulnerability without automatically relabeling that unit a mage or archer.

- The AI may accept reactions if a worthwhile target is reachable **and attackable this turn**, with enough AP remaining for a legal attack. The user's example is taking one reaction to reach and attack an archer/mage. Check the actual legal route and attack endpoint, not just geometric proximity.
- This is an option to evaluate, not a compulsory charge whenever the target is close. Compare the likely immediate attack benefit with incoming damage and the risk of becoming downed before arrival. Avoid fake high-value plans whose attack cannot actually occur.
- Do not accept opportunity attacks merely to chase a distant offensive target that cannot be attacked this turn. Prefer fighting the engaged frontliner, another actionable target or a safer route. Safe movement toward distant targets remains allowed; this is not a general prohibition on pursuing them or on tactical retreat.
- Count all expected reaction opportunities along the route: one enemy can react on several departure steps, and several enemies can react on one step. Spending normal AP does not remove a reactor. Do not count only distinct enemies, and do not mistake the user's one-reaction example for a universal cap of one.
- Keep risk separate from AP/terrain costs and reassess when reactions alter HP, positions or the legality of the planned attack. Use the same trigger/eligibility rules as actual execution. Exact utility weights are implementation tuning rather than a new player-facing mechanic.

## Suggested implementation grouping after approval

One feature milestone: shared reaction resolution/free melee context and interruption handling; all movement entry points and Undo guards; bounded AI risk awareness and clear feedback; save boundaries; then one combined player review. Internal batches may be used for diagnosis, but do not request repetitive manual tests after every file change.

Future validation should cover: zero-AP reactors; one/multiple enemies; diagonal and still-adjacent steps; repeated steps with no cap; invalid movement; normal hit/miss/graze/critical parity and weapons/unarmed; downed/contained/incapacitated exclusion; mover/reactor incapacitation and displaced targets; approved special/forced movement; player/AI/turbo equivalence; no recursive movement chains; Undo button/hotkeys/implicit paths and selection/save-load; occupied-tile interruption; and pending reactions blocking save/turn transitions. No test framework or new automated checks are authorized by this planning request.

No source, scenes, prefabs, assets, saves or settings changed during this feasibility review. No build, tests or graphical playtest run. The accepted Spec29 review build remains `Builds/Warband-Review-20261103-4b5894ed/Win64/VoreWar.exe`.
