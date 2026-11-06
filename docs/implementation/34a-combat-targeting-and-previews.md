# 34A — Caster-aware spell targeting and read-only damage previews

2026-11-04. Implements slice1 only from spec34. Developer lab, power scoring, gear, simulations, world/roads and post-battle events are not started.

## Changes

- Correct all four TacticalAI.GetListOfPotentialSpellTargets branches to compare the actual caster against each target. Non-resistible spells use1.0 hit chance; the single-target HUD now does likewise.
- Shared Actor_Unit.ScaleSpellDamage computes current outgoing/incoming magic multipliers, tag modifiers and retained scaling additions. Actual hits and previews share it, preserving actual hit order.
- PreviewSpellDamage predicts HP damage on a successful hit: sneak multiplier, current damage modifiers, ManaBurn's affordability outcome, flat trait/injury damage adjustments, elemental resistance, ManaBarrier with shadow mana, minimum damaging hit1 and existing barrier absorption.
- Extracted read-only elemental and mana-barrier arithmetic from CalculateDamageWithResistance. Actual resolution alone spends mana; the existing Unit mana-cost formula is shared with preview affordability checks. ManaBurn's spend precedes mana-barrier availability in both paths.
- Both map percentage/damage labels and hover/AOE health-bar projections use the same preview. No preview RNG draw, resource spend, equipment callback, applied status or cached resistance mutation.
- Added requested KNOWN BROKEN/extremely-low-priority comment to Crystaline/Fractured's actual cached multiplication branch. Its compounding behavior remains unchanged. Preview models the next hit locally instead of mutating the cache merely by inspecting a target.

## Limits

Preview is HP damage conditional on a hit, not expected damage multiplied by hit probability. It does not simulate custom equipment action callbacks, random on-hit events or future state changes caused by earlier targets in an AOE sequence. Self-hit AOE can also see resource changes caused by casting itself. Ordinary current-state comparisons should match; unsupported edge cases are not a promise of exact prediction. Existing combat-log pre-resistance values are not redesigned here; focused validation compares actual HP loss.

## Verification

Extended existing TraitCurationAudit with10 controlled spell scenarios, repeated previews, RNG-state/resource/cache invariance and native hit comparisons, plus asymmetric caster/target AI checks. Scenarios cover generic/incoming/outgoing multipliers, elemental resistance, tier traits and hand injuries, ManaBarrier, ManaBurn with/without mana, the two combined, existing barrier, SpellForce mana costs and the deferred Crystaline cache. Native checks retain isolated synthetic game/save state and deletion suppression; no personal save exercised. Initial diagnostic compile failed because the test assumed System.Random rather than the game's Rand wrapper; corrected to use its existing Capture API. Final results recorded below when complete. No graphical playtest claimed.

## Handoff boundary

Stop after this slice and await user feedback/direction. Developer testing screen is slice2, not an extension of this task. Source notices/CHANGES identify the original fork edits; licensing/attribution preserved and no new third-party code/assets/packages introduced. Existing release/provenance gates remain open.


## Completed verification — 2026-11-04

Native runtime2 passed499 checks (457regression+42focused) with editor exit0. All10scenario predictions matched native HP loss;20repeated previews per scenario preserved RNG/resources/resistance cache. AI asymmetric INT and non-resistible targeting checks passed. Fresh standalone Warband-Review-20261104-38ca6196/Win64/VoreWar.exe built exit0 with no C#/shader errors. Compiler warning messages unchanged against prior fresh-compilation logs. Earlier diagnostic compile failure is retained, not counted as passed.

Evidence: Logs/combat34a-runtime2-result.json, combat34a-runtime2.log, combat34a-build.log and combat34a-result.json. Existing notices packaged; source dates/CHANGES reviewed. One optional focused player review: repeated spell-target hover should not change resources/estimates; compare the predicted HP loss with an ordinary spell hit. No new injury-focused manual testing requested. Stop here pending direction for slice2.
