# Technical opportunities after the trait/injury milestone

## Current follow-up — 2026-11-06

The original follow-up and spec 38 are closed. The newly requested [module 40](specs/40-authoring-and-campaign-progression.md) now plans developer settings/official balance authoring, route planning, spellbook progression, earned trait scenes, ending history and calendar pressure. Its specifications supersede earlier deferrals for the planning of those features. The older findings below remain historical source-review context.

2026-11-04. Read-only source review in response to the user's request for worthwhile Codex-led work excluding narrative/text creation. This is an advisory backlog, not authorization to implement or a runtime test report. Spec33 remains provisionally accepted; focused injury playtesting and the Help rewrite stay deferred.

## Concrete combat follow-up

- Actor_Unit.DefendDamageSpell multiplies cached TraitBoosts.Incoming.MagicDamage by0.75 on each unfractured Crystaline spell hit and by1.50 when Fractured, allowing repeated-hit compounding instead of a local per-hit modifier. Fixing this should preserve intended single-hit behavior and avoid adding an invented balance rule.
- TacticalAI.GetListOfPotentialSpellTargets has paths calling target.GetMagicChance(target,spell), rather than target.GetMagicChance(actualCaster,spell). This scores target versus itself. Audit similar targeting branches and add focused functional checks.
- TacticalMode.SpellDamageMod and ShowMagicPercentages do not share the complete damage pipeline used by Actor_Unit.DefendDamageSpell. The new flat trait/injury adjustments are present, but existing incoming/outgoing multipliers, resistance and other effects can diverge. A shared read-only prediction path should not spend mana, roll random outcomes, mutate cached boosts or apply statuses. Actual resolution can perform the required effects separately.

No observed runtime defect is claimed from this source review; these are concrete source findings requiring focused reproduction/corrections.

## Larger worthwhile work

1. Developer-only battle scenario controls behind the established developer access: deterministic fixtures, HP/temporary HP and injury selection, trait tiers, armor, recovery/treatment and day stepping. Build automated reproducible checks from the same scenarios. Useful because the user cannot conveniently induce particular injuries during normal play. Do not expose developer balance controls to players or access personal saves.
2. Recalibrate CompanyContracts.Power. Current score uses raw stats, maximum HP, weapon damage/range, spell count, simple armor protection and mostly2points per trait (5for Featherweight/NaturalCaster). It ignores tier strength and many distinct effects, current wounds and HP. Contract rating must remain fixed enemy power, independent of the player's company. The same score feeds road encounters. Design decision needed: should ambush sizing use healthy capability or current injured strength? Use automated controlled battle/economy simulations to measure rather than assume balance.
3. Trait acquisition/progression. Spec32 provides definitions and exclusive grant/upgrading mechanics, not a deliberate new campaign schedule for recruits gaining quirks/veterans upgrading tiers. CompanyEconomy currently generates level/focus/gear by species/reputation without a new baseline-quirk assignment policy. Need user decisions on starting quirks, player choice vs earned/random upgrades, and limits/opposing traits. Existing species traits remain; no narrative is required.
4. Broader equipment mechanics/progression. The explicit company shop catalog currently has four ordinary weapons, four armor pieces, one accessory and three spells (plus enemy-only slug weapon). Nine equipment slots exist, but variety/progression is still narrow. User can define meaningful tradeoffs before coding; do not invent item names/lore or new armor mitigation rules. New scoring should understand resulting equipment.
5. Campaign generation robustness/pacing. CompanyCampaign.Generate places six towns by random distinct tiles, without minimum separation. Preserve the requested low town count; consider spacing and early-contract availability across many seeds. Terrain is already noise-based; do not falsely describe it as independent random tiles. All terrain is currently traversable, so no existing unreachable-town bug is established.
6. Resolution/layout regression and input polish. Check supported smaller windows/4:3/widescreen for overflow, portrait clipping, focus, dropdowns and tooltips. No current clipping defect is established. This can be one grouped quality pass instead of repetitive screen-by-screen user tests.

Recommended sequence: concrete combat corrections first, then developer scenarios and power calibration, then user-directed trait acquisition/equipment expansion. No source edits, new tests, builds, file deletions or moves were performed for this review.


## User direction — 2026-11-04

Superseded by the bounded plan in docs/specs/34-follow-up-engineering-slices.md. User approves targeting/preview fixes; Crystaline/Fractured only gets a known-broken low-priority comment. Developer lab adjacent to Race Editor uses the same214214214 password. Power scoring, expensive reputation-gated gear/category merchant navigation, simulation reports and larger coherent road-connected worlds are approved directions. Event-based trait progression is deferred, with a mandatory eligible post-battle event based on extraordinary/mundane battle occurrences; no reserves, leader-only remaining participation exempt. Plan only this turn; do not implement all directions in one session.
