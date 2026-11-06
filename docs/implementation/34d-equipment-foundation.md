# Spec34 slice4 — Equipment mechanics and merchant foundation

Implemented 2026-11-04. The user authorized this slice and confirmed all three remaining boundaries: armor covers weapons/direct damaging spells including area hits, not digestion/ongoing effects; apply after existing reductions, before barriers/temporary HP; evasion affects melee/ranged/resistible spells, not vore or guaranteed hits.

## Mechanics

Equipment definitions now expose AC and evasion. AC sums across correctly worn armor slots. Each successful direct hit rolls uniformly from0throughAC and subtracts that amount after existing damage/elemental/mana reductions. The hit retains minimum1 before barriers; barriers may absorb all of it. Temporary HP then uses the existing damage pipeline. Graze injury immunity and lasting-HP injury thresholds remain unchanged. Armor's body-part injury protection is separate and preserved. Digestion and ongoing poison retain their original paths without AC deflection.

Existing four armor fixtures gain provisional defenses: Helmet AC1, Torso Armor AC2, Arm Armor AC1, Leg Armor5percentage-points evasion. Existing stat bonuses, prices, slot restrictions and injury coverage are retained. No armor penalties. The Insight charm remains a stat accessory. This is a foundation, not the expensive gear/progression catalog planned in slice5.

Existing weapons retain their damage/range/accuracy-rating tradeoffs: accuracy remains the inherited attack-rating multiplier, not a new flat final-odds modifier. Merchant summaries explicitly call it an accuracy rating. Power scoring now prices that existing multiplier and new AC/evasion. No weapon stat reroll, save migration or stored force regeneration.

Single Battle's original two-item inventory remains supported: named Helmet/Body Armor/Shoes/Gauntlet-or-Gloves map to the equivalent slot's defense values, just as injury protection already does. Company equipment uses stable definition IDs in their correct nine slots. Definitions supply new defenses to existing saved armor without re-equipping or applying another stat bonus; no save-format change.

## Resolution and previews

Direct-hit resolution reports the post-mitigation/pre-barrier damage to the existing combat log/counters. Spell hit counters are registered after resolution. The public general Damage entry remains compatible; an internal shared resolver returns the hit amount without a mutable last-hit cache.

Weapon/spell previews and ordinary AI target estimates use a non-rolling average after AC and barriers. The armor expectation is exact across its discrete roll range, including minimum damage and complete barrier absorption; display rounds the average. It does not execute equipment callbacks, consume mana/RNG or change injury/state. Ordinary critical/graze differences remain part of combat; opportunity risk estimates price each outcome separately. Existing exceptional legacy callbacks can still make preview results differ; this does not promise exhaustive simulation.

## Merchant and loadout

Nine persistent merchant filters: All, Helmet, Torso Armor, Leg Armor, Arm Armor, Accessory, Melee Weapon, Ranged Weapon, Spells. Four stock entries per page leave room for two filter rows. Buying retains the chosen category/page. Enemy-only slug weapons stay excluded. Existing persistent town navigation remains available.

Summaries show weapon damage/range/accuracy rating, armor AC/evasion/stat bonus, or spell effect/mana/range. The equipment picker shows the currently equipped item's mechanics for comparison. No new item lore, economy, crafting, regional stock or reputation unlock behavior.

## Validation

Existing authorized synthetic Unity audit extended, with files preserved under Logs and deletion intercepted; no personal saves. The 1326-case enumeration validates armor expectations/floors/barriers. Native checks cover armor stacking and0..AC rolls, weapon/magic/direct-spell paths, poison/attrition bypass, mana/barrier/temporary ordering, non-mutating previews, evasion coverage, stable serialization, original two-slot armor, all merchant filters, loadout comparison and full company save/reload. Prior693 checks retained.

Initial audit compilation exposed editor-only access to the internal RNG restoration helper; corrected using the audit's existing reflection approach. Runtime2 passed730 checks. Runtime3 adds the full damaging-spell pipeline check and compiles the final merchant explanation. Results/build recorded at handoff below. No rendered visual playtest or balance calibration claimed.

## Source and GPL workflow

Nine modified sources listed exactly in CHANGES, with dated modification notices and inherited legal/attribution notices retained. No external code, assets/dependencies, scene/prefab/package edits or agent file deletion/moving. License4–5 reviewed. Existing public-distribution/provenance/Corresponding Source review gates remain open; private build is not distribution clearance.

## Review and next boundary

Final native runtime3 passed731 checks (693 prior plus38 equipment/UI checks, including one exhaustive1326-case expectation check), actual exit0. Fresh Windows build `Builds/Warband-Review-20261104-9ae35c31/Win64/VoreWar.exe` exit0, no C#/shader errors or new compiler warning messages (41 unchanged distinct kinds). Final merchant explanation compiled and exercised in runtime3. Evidence: Logs/equipment34d-runtime3-result.json, runtime3.log, build.log and equipment34d-result.json. Seven existing notices packaged/hash-verified; earlier builds/logs preserved.

Inspect Town > Merchant filters/readability, buy armor and inspect Company > Manage > slot comparison, then use it in combat. Damage can vary by the AC roll; previews show rounded averages. Broader gear/reputation/prices await slice5. Before that slice, agree whether reputation exposes tiers or individual stock and whether towns differ/restock; do not add complexity without direction. Help/narrative and post-battle events remain deferred.
