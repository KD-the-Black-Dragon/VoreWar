# 40 — Official balance authoring and campaign progression

Planning draft, 2026-11-06. The user requests an official, dependency-ordered module covering six further workstreams and an authoring workflow for shipping species balance changes. This authorizes design and documentation. Implementation awaits a subsequent instruction. Spec 38 remains closed and accepted.

## Goals

- Give the curator a reliable way to edit species balance visually, test a draft and promote approved data into future builds.
- Add useful travel planning, broader spell progression, earned post-battle traits, durable company history and eventual calendar pressure.
- Use existing mechanics, the Battle Lab and native campaign simulations. Narrative prose and the extensive Help rewrite remain separate work.
- Deliver substantial reviewable phases, with one grouped player handoff per meaningful workflow. Internal diagnostic batches can be smaller.

## Implementation order

| Phase | Scope | Dependency and reason |
| --- | --- | --- |
| [40A](40a-official-balance-authoring.md) | Developer settings reliability and official balance authoring | Establishes reproducible shipped data, draft isolation and revision identity for subsequent tuning. |
| [40B](40b-map-route-planning.md) | World-map route planning | Uses the resolved rules and existing per-tile travel. Completes the travel throughput needed for later campaign calibration. |
| [40C](40c-spellbook-progression.md) | Spellbook variety and progression | Uses the official-data workflow. Establishes combat and equipment choices before trait-event and pressure calibration. |
| [40D](40d-post-battle-trait-progression.md) | Post-battle evidence and trait progression | Uses the retained spell/trait vocabulary and revisioned rules. Establishes the company's additional growth channel. |
| [40E](40e-ending-scoreboard-and-history.md) | Ending scoreboard and durable campaign history | Establishes reliable terminal accounting before calendar pressure adds more end-to-end loss scenarios. |
| [40F](40f-calendar-difficulty.md) | Calendar-driven difficulty progression | Calibrates against final travel, spells, trait growth, wages and uncapped upkeep. |

The principal implementation dependencies are A before C, C before D, and the settled growth/travel systems before F. Route planning and history can be developed independently after their shared save conventions are established. The order above reduces repeated balance work and gives each phase a distinct player workflow.

## Shared requirements

1. Preserve the accepted four-stat system, AP and action economy, opportunity attacks, hit/graze/critical rules, temporary HP, injuries, digestion-only permanent losses, leader revival and separate Single Battle mode.
2. Preserve fixed contract-rating power meanings. Calendar progression must act through disclosed offer/encounter rules without silently changing an accepted force's rating or roster.
3. Keep curated species and permanent retirements authoritative. Importing a draft cannot restore retired species, OCs, traits or their artwork. Player species exclusions continue to block new generation while preserving existing individuals.
4. Version additions to company, battle and history data. Define old-save defaults and exactly-once settlement before adding persistent rewards. Preserve existing manual saves and autosave recovery.
5. Reuse Unity 2018.3.12f1 and existing dependencies. Avoid package upgrades and unrelated scene/asset changes.
6. Later implementation must include dated source notices, CHANGES.md and licensing/provenance review under AGENTS.md. No new artwork, dependency or external publication is part of this module.
7. Verification plans below are future work. Focused checks require implementation-stage authorization under the repository workflow. Fixtures must remain synthetic, retained and deletion-suppressed. Personal settings and saves are excluded from agent test inputs.

## Confirmed design answers

- Keep the password-protected Race Editor and add export/import for promoting approved defaults into future builds.
- Preserve existing characters when species defaults change. New defaults apply to newly generated individuals. This requires preserving relevant species-derived data as well as stored base stats. Global combat-formula changes need their own explicit compatibility decision.
- Produce one automatic post-battle scene recognizing the most significant qualifying experience and assigning a related trait. Existing traits influence the outcome.
- Balance positive and negative acquisitions by their current counts. Equal counts give equal chances. A difference of one gives a 75% chance toward the minority side. A difference of two gives a 100% chance toward the minority side. Larger differences should use that same 100% correction, subject to confirming the counting policy.
- Classify all traits internally as Good, Bad or Neutral. Count both species and acquired personal traits for the Good/Bad ratio. Mixed-effect traits are Neutral and contribute to neither side. The user explicitly classifies Featherweight as Neutral because it combines evasion with increased damage received. Old injuries remain separate.
- There is no hard trait-count limit. Favor eligible characters with fewer traits when choosing the subject of a post-battle event. Exact selection weights remain to be designed alongside event significance. Old injuries already add lasting penalties and have no positive counterpart.

## Decisions still open

The two trait-count/capacity questions are answered above. Phase 40D still needs the classification inventory, occurrence catalog and selection weights before implementation. Do not add injury-dependent polarity odds without approval.

Each phase also identifies decisions to settle immediately before its implementation. There is no need to choose every spell, event threshold or calendar curve to approve the module structure.

## Handoff and completion

For each phase, record changed behavior, actual checks, provisional numerical values, pending player review and the exact next action in the resume checkpoint. Request a grouped manual review when a new workflow is ready. Acceptance of this planning draft does not establish numerical tuning or resolve the open choices.

The module completes when all six workflows have their own implementation evidence and user acceptance. Public-release provenance, licensing and Corresponding Source review remain separately tracked release gates.
