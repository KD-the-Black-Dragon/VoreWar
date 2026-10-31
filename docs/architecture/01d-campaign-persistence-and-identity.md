# Architecture study 01D — campaign, clock, persistence, and identity

**Status:** Final study packet prepared for review, 2026-10-30. Source inspection and planning documentation only. No game source, scene, prefab, asset, dependency, or license changes; no Unity launch, compilation, save round-trip, or playtest in this packet.

## Main conclusions

The retained terrain, grid, tactical actors, and binary serializer are useful foundations. The existing campaign controller is not a suitable company scheduler: a strategic turn runs empire income, buildings, population, events, army recovery, recruitment, AI, and conquest checks. Battle return also calls strategic regeneration, which performs cleanup rather than merely refreshing the screen. Replace those owners before connecting the company loop.

Battle saving already exports actors, armies, containment-related state, turn/control state, AI, equipment-bearing units, and the log. It is worth adapting rather than discarding. However, loading restores the old World, settings, compatibility migrations, and strategic mode before resuming combat. Merely giving a new campaign no empires would classify it as Pure Tactical. A separate explicit campaign format/mode and load route are required.

The inspected Unit model has no persistent company-member identifier. Names, species, side, UnitType, list positions, and temporary copies are unsuitable identity keys. New member/candidate/target identities and once-only daily/result records must be owned by the new campaign, while the same canonical Unit data remains usable in battle.

The full [cleanup sequence and risk register](01d-removal-sequence-and-risks.md) reconciles packets [01A](01a-navigation-and-settings.md), [01B](01b-species-events-and-narrative.md), and [01C](01c-combat-equipment-and-results.md) with the [latest design decisions](../design-clarifications-2026-10-30.md).

## 1. Existing ownership and entry points

| Current owner | Evidence | Conversion implication |
|---|---|---|
| State | [State.cs](../../Assets/Scripts/Core/State.cs), static World/Rand/settings and Save/Load | Separate campaign data from application services; do not make the new root an accidental legacy World. |
| World | [World.cs](../../Assets/Scripts/Core/World.cs), fields and constructors | Stores terrain, empires/turn order, villages, config, item repository, stats, and optional TacticalData. Constructors also initialize empires and invoke strategic setup/turn logic. |
| StrategyMode | [EndTurn/BeginTurn](../../Assets/Scripts/Modes/StrategyMode.cs#L1829), ProcessGrowth/ProcessIncome | Owns empire scheduling, not a reusable one-day company update. |
| Empire/Army | [Empire.Regenerate](../../Assets/Scripts/Entities/Empire.cs#L380), [Army.Refresh](../../Assets/Scripts/Entities/Army.cs#L178) | Mix daily health/mana, movement, equipment, research, village, leader, and AI work. Extract selected rules; do not call the whole routine for company recovery. |
| GameManager | [SwitchToStrategyMode](../../Assets/Scripts/Core/GameManager.cs#L287), SwitchToTacticalOnLoadedGame | Mode routing and battle return need explicit company and skirmish branches. |
| SaveLoad UI | [SaveLoad.cs](../../Assets/Scripts/Subwindows/SaveLoad.cs) | Previews deserialize World and infer mode from MainEmpires; adapt listing/preview/manual/quick-save together with the format. |

World(false), used by [CreateTacticalGame](../../Assets/Scripts/Subwindows/CreateTacticalGame.cs#L426), creates a minimal legacy world with shared services. World's strategic constructor creates empires, villages, monsters, relationships, and starts StrategyMode. Neither is the intended new campaign constructor. Temporary compatibility data may expose terrain/items/night to retained combat, but must not start a hidden empire simulation.

## 2. Why the old strategic turn cannot become a day unchanged

StrategyMode.EndTurn advances through EmpireOrder. Only wrapping that order increments World.Turn and runs ProcessGrowth, AI events, diplomacy, constructible/claimable work, and other world maintenance. BeginTurn then processes the next empire's income/buildings and player events or strategic AI. Autosaves are embedded in this flow.

ProcessIncome can respond to negative gold by selecting low-experience units for departure and assigning zero HP. This conflicts directly with personal debt, player-selected repayment, the grace day, persistent former members, and digestion-only permanent loss. Do not reuse it as wages.

ProcessGrowth calls Village.NewTurn, Empire.Regenerate, town/mercenary-house stock refresh, and income calculation. Empire.Regenerate calls Army.Refresh and also maintains/generates leaders. Army.Refresh heals a percentage, restores mana, changes growth/body state, ticks equipment, sorts AI spell stock, resets movement/cooldowns, and runs village work. GetTileHealRate consults village recovery/research and UpdateInVillage can transfer item stock. A request to reuse normal army healing therefore means extracting its appropriate health-rate idea, not preserving these unrelated effects.

[StrategyMode.Regenerate](../../Assets/Scripts/Modes/StrategyMode.cs#L1150) also calls Empire.ArmyCleanup, recalculates income and auto-leveling, and rebuilds views. ArmyCleanup calls [Army.Prune](../../Assets/Scripts/Entities/Army.cs#L527), which removes summons and units whose IsDead is true and updates loss statistics. The legacy IsDead = HP below one does not represent the new downed state. Running this cleanup on canonical company members could turn a survivable downed state into a recorded loss.

Battle return, menu close, redraw, and load must rebuild presentation without processing a campaign day, healing, paying wages, generating candidates, pruning members, or applying contract results again.

## 3. Proposed company ownership

These are design proposals for specs 05–11, not new classes already implemented:

| Proposed record/service | Owns | Must not own |
|---|---|---|
| Campaign root | Format/schema version, campaign ID, mode/phase, creation rules, day/action state, treasury, world, company, offers/contracts, statistics, pending transitions | Unity scene objects or a running EmpireOrder. |
| Character registry | Stable identity and canonical Unit for each generated candidate, member, former member, target enemy; biography, debt, injury/history records as applicable | Identity inferred from name/species/side or battle-array position. |
| Company | Member IDs, leader ID, active/reserve flags, shared inventory, wage schedule/departure warnings | Separate copied Units for the same person in town, roster, and combat. |
| Region/town records | Actual terrain/doodads and dimensions, generation metadata, discovery, static towns, local offers/recruit pools/refresh dates | Population, territorial ownership, diplomacy, garrisons, empire economy. |
| Contract target | Contract/target IDs, location, generated enemy identities, persistent casualties/wounds, reward/deadline/completion state | Regeneration to the original power budget after failed attempts. |
| Battle session | Battle ID, target/context, deployed bindings, temporary summoned IDs, tactical snapshot, progress/result phase | Direct ownership of reserves or replacement of the canonical roster collection. |
| Daily updater/result reconciler | Ordered, resumable state changes with recorded progress | An implicit call from redraw, menu close, or deserialization. |

Creating a candidate is the point to give them an identity; hiring must not mint a replacement. A former member's town reappearance references the same identity and preserves debt/equipment/injuries. A new summoned double gets a separate temporary battle identity and cannot inherit the source's debts, memorial history, or persistent membership. Stable IDs are additional to serializer object references: neither alone proves that duplicated entries or replayed results cannot occur.

New member state can wrap the retained Unit rather than putting all campaign economics into tactical actors. Whether the identifier sits on Unit or a binding record is an implementation detail; the invariant is one canonical persistent character and unambiguous resolution after loading. Company/target/offer collections should reference that canonical identity. Reserves stay outside mutable deployed-army collections.

## 4. A daily transition that can stop for a decision

Use one campaign-owned day advancement path for End Day and each skipped rest day. Persist the day, remaining actions, payday/departure dates, remaining requested rest days, update progress, and any pending decision. Day numbering must distinguish elapsed days from a displayed calendar label so the first payday is exactly seven elapsed days after start.

Recommended sequencing, to refine before implementation:

1. Commit the player's request to end the current day; finish departure warnings whose opportunity to pay has actually elapsed. A new warning must not expire in this same transition.
2. Advance one day and process that day's registered systems once: wages/debt, deadlines, offer/recruit refresh, company and target recovery, injury progression, and healthy-member sparring. Use explicit due dates/progress markers, not repeated calls from the UI.
3. Establish the new day's action budget and pending warnings/choices. Return control when a response is needed, including the full grace-day opportunity to pay. Continue another rest day only after that decision is handled under the agreed rest policy.
4. Save a stable checkpoint at the chosen boundary. Reload resumes the recorded phase, not the entire day's effects.

Do not interpret this proposal as fixed numeric balance or a decision about every same-day tie. Exact payday/departure ordering and contract boundary comparisons must be documented in the implementing batch. The key guarantees are no duplicate charges/recovery, no skipped grace day, and no daily ticks in combat or the leader-revival trip.

Interaction costs remain configurable and normally zero; there is no need to decide each service now. Movement and action spending should have a single authoritative check. A screen opening must not accidentally charge again when refreshed or restored.

## 5. Terrain, paths, movement, and discovery

[WorldGenerator.GenerateWorld](../../Assets/Scripts/Modes/Strategic/WorldGenerator.cs#L52) combines terrain, village placement, and empire assignment. GenerateOnlyTerrain bypasses assignment, but takes no MapGenArgs and uses the instance's existing/default arguments. [StrategicTerrainGenerator](../../Assets/Scripts/Modes/Strategic/StrategicTerrainGenerator.cs) is a more direct terrain-generation candidate with explicit generation arguments. Preserve tile types and rendering where useful, then add company-specific static town/target placement and reachability checks. The old placement heuristics optimize farmland/empire spacing; even their bounded retry/fallback behavior does not prove a valid RPG region.

[Army.CheckMove](../../Assets/Scripts/Entities/Army.cs#L611) mixes action cost with claims, scouts, animations, undo history, occupancy, and hostile towns. An attack consumes all remaining movement in that implementation. Preserve the intended move-onto-target battle trigger without automatically inheriting that whole budget rule. [StrategyPathfinder](../../Assets/Scripts/AI/StrategyPathfinder.cs) provides useful grid search, but caches terrain and uses Empire/Army occupancy, village blocking, and trait-derived passability. Clear/rebuild caches on new campaign/load; adapt occupancy and terrain-cost providers. The eight-neighbor grid is existing behavior, not a new company travel design decision.

[FogSystem.UpdateFog](../../Assets/Scripts/Modes/Strategic/FogSystem.cs#L20) resets its grid to fogged on each update, then reveals around allied settlements/armies/buildings. It is live faction visibility, not a persisted record of exploration. New campaign discovery must survive leaving/reloading an area. Keep discovery/known towns, current visibility if used, and exact contract markers separate. Marking an objective through fog need not expose all surrounding terrain. Do not keep allied-town sight/research/building dependencies merely to draw fog.

Store the generated terrain/towns and discovery state in saves. A seed alone is insufficient to reproduce the region under the current mixed randomness or after generator/content changes. Town/target reachability, nearest revival town, and legal retreat placement should use the same campaign passability model; exact map size/radius/cost tuning remains open.

## 6. Existing persistence and its limits

[State.Save](../../Assets/Scripts/Core/State.cs#L538) stamps version 45D, exports TacticalData when tactical mode is active (otherwise clears it), serializes the World as Odin binary, and writes directly to the named file. It catches failures and displays a limited number of warnings; it returns no explicit success result. The inspected writer has no checkpoint rotation, validated staging, or completed-write acknowledgement for an Ironman coordinator.

[State.PreviewSave](../../Assets/Scripts/Core/State.cs#L577) deserializes the full World for menu previews. [State.Load](../../Assets/Scripts/Core/State.cs#L599) first clears strategic UI/state and changes mode, then deserializes into the live global World. It applies numeric legacy-version migrations, restores Config.World/building settings, recreates repositories, reloads traits, restores strategic mode if MainEmpires exists, and finally resumes TacticalData. A failed load can occur after changing live state. A new-format guard must run before these legacy mutations; old empire saves are unsupported, not silently interpreted as company or skirmish saves.

Use an explicit format marker/schema and mode discriminator, with campaign-specific preview metadata. Deserialize/validate the prospective save before installing it as live state. Rebind shared services and redraw only after validation. Keep legacy migration code out of the new company load route. No package/serializer upgrade is required by this recommendation.

The serializer can remain Odin binary initially. The new root must include all required data, not just the old World plus a few menu flags. Define version evolution for this fork independently of the old numeric 45D parsing. Distinguish unsupported format/content from corruption and from an empty eligible generation pool.

## 7. Tactical save feasibility and safe boundaries

[TacticalData](../../Assets/Scripts/Utility/Stored/TacticalData.cs) and [Export/Import](../../Assets/Scripts/Modes/TacticalMode.cs#L1788) contain actors, army references, terrain, active side/round, selected unit, AI objects, extra attacker/defender lists, fled-unit lists, log, buildings/effects, and other battlefield data. These lists are not the new company reserve roster. Actor_Unit serializes its Unit, surrendered/fled flags, SelfPrey, and other state; Prey links actors/predators/nested prey. This is evidence of an existing resumable representation, not proof of a round-trip under the new rules.

[LoadData](../../Assets/Scripts/Modes/TacticalMode.cs#L1552) imports the snapshot, reloads traits/leaders, refreshes prey, rebuilds presentation, and reconstructs RetreatedDigestors from fled actors carrying prey. It also can resume turbo AI. General setup resets review/dialog state. Animation/private transient state is not exhaustively represented; SurrenderedThisTurn is explicitly marked deliberately unsaved. Thus do not assume saving at any instruction or during a result dialog is safe.

Retain normal tactical saves by adapting this representation, with explicit stable action/turn boundaries. Add the requested lasting/temporary HP distinction, permanent downed state, disappearing summons, deployment/member bindings, target state, stalemate progress, and settlement phase. Existing TempBoosts/stat boosts or Barrier are not automatically the new temporary HP pool. Injury and actual digestion cause data must survive a battle save independently of randomized log prose.

For Ironman, a turn-boundary checkpoint after the prior side's actions/digestion/effects have settled and before the next AI action is a practical candidate. Current EndTurn switches sides, advances currentTurn only on one half of the exchange, calls NewTurn, and then processes effects/UI; adding a save at an arbitrary line could capture half an update. Stage/resume the transition explicitly or save only after its completion. The exact checkpoint must also work through TurboMode, friendly AI, and immediate leader-digestion game over.

Existing tactical autosave is at battle setup; quick saves are separately available. No turn-by-turn tactical autosave or Ironman control was found in these inspected paths. Normal in-battle saving is feasible in principle, but not verified for the conversion. If required coverage cannot be made reliable in the initial batch, report the agreed between-battle fallback and its exact Ironman restart behavior for review.

## 8. Checkpoints, results, and revival must resume once

Use explicit campaign/battle transition phases rather than serializing dialog callbacks. Recommended states cover deployment, active battle, result determined, result applied, summary pending, revival pending, and returned/ended. A stable battle/result ID plus recorded application progress prevents a loaded summary from paying again, memorializing twice, or reviving twice. Record actual final outcomes before destructive actor cleanup. If phases mutate in stages, progress must be serialized with those changes or reconciled transactionally; one boolean after a partly completed operation is insufficient.

Keep battle result label, target completion, and company survival distinct. A stalemate returns survivors as fled, and its fled enemies count defeated for contracts. Downed members facing standing enemies must remain in the actual simulation; no left-behind loss is inferred. Completed leader digestion triggers immediate ending when revival is disabled; enabled revival waits for settled results and any earned contract rewards, then the zero-day nearest-town cutscene. A downed leader is alive and needs no revival. Reserves can preserve the company.

The existing File.WriteAllBytes overwrite is not a corruption-backup strategy. For the new save service, return an explicit success/failure and retain several independently recoverable checkpoint generations. One implementation candidate is fixed rotating generation files with validation metadata: overwrite only the oldest slot after newer valid slots exist, and choose the newest fully valid generation on load. A partial write must not destroy the only recent valid checkpoint. Review any publication/rotation file operations against repository instructions; no files are moved, removed, or personal saves opened by this study. Backup count, checkpoint frequency details, and failure UI belong to the implementation batch.

## 9. Randomness and content/configuration ownership

[Rand](../../Assets/Scripts/Utility/Rand.cs) wraps a private System.Random initialized with no exposed seed/state API; State.Rand is static and is not a field of the saved World/TacticalData. World generation also uses UnityEngine.Random, and [OpenSimplex's default constructor](../../Assets/Scripts/Utility/OpenSimplex.cs#L79) seeds from the clock. StrategicTerrainGenerator uses random noise offsets. The inspected save path does not capture a unified random stream. Current reload therefore does not establish identical future random outcomes.

If reproducible continuation/generation is desired for debugging and reliable checkpoints, give the new campaign explicit random state or independent owned streams with a compatible implementation. Isolating cosmetic/narrative randomness is technically useful, but changes random sequences; make the substitution explicit before the relevant implementation. Normal reload permission is not a requirement for anti-reroll protection, and Ironman is not a security system.

World.ConfigStorage references legacy Config.World and load replaces the live object. RaceSettings/data slots are external application state, loaded separately; saves do not snapshot that global variable through a World field. Retained cosmetic/content preferences remain global, but code-locked balance values and approved species roles must not be restored from old player controls. Disabling a curated species blocks future individuals only: load/reappearance/rehire of an existing identity must not reject or replace them.

ItemRepository recreates code-defined items; legacy update paths remap some saved items by names through Unit.UpdateItems/SetItem. This can interact with direct accessory-stat mutation and slot sizing. New loadouts need stable item-definition identity and explicit per-instance uses/cooldowns without reapplying bonuses on every load. Review supported content/schema changes, Race Editor overrides, and stat cache rebuilding together; do not promise unchanged characters across arbitrary developer balance edits without a policy.

## 10. Readiness and remaining work

The architecture packets now establish a concrete implementation direction and [ordered retirement manifest](01d-removal-sequence-and-risks.md). This packet is ready for review; it does not approve cleanup, claim the entire dependency graph is proven, or mark baseline gameplay/save tests complete.

Bounded implementation preflights remain: resolve 01A's fourteen control bindings/inherited callbacks before their affected scene edits; trace any additional live caller before retiring it; prove asset ownership/repacking before any whole-file retirement; choose precise save/day transaction boundaries in the implementing batch; and exercise the earlier pending gameplay checks once curated species permit them. Numeric design tuning stays deferred. No new creative answers are required to understand this packet's proposed boundary.
