# 07–08 — People/economy milestone implementation preparation

Date: 2026-11-01. User accepted06 mechanics preview, reconfirmed reuse of original terrain artwork, and authorized the next milestone. This turn changes documentation only; no species or balancing values inferred.

## Required user decisions requested together

1. First existing sapient species, or the small preferred melee/ranged/magic group. Never infer approval from the 64 remaining definitions or resurrect a rejected species.
2. Any initial animal species for enemies/summons, or explicit deferral of that curation.
3. Whether initial numeric tuning (cash/wages/prices/leader bonuses/refresh/grace) may be provisional, or must be specified before implementation. No final values chosen by this review.

Current status: decisions were supplied and the grouped milestone is implemented, awaiting combined runtime review. Use [07–08 implementation](07-08-people-and-economy.md) for current behavior/checks. The preparation below records the earlier pre-implementation trace.

## Source trace and integration work

- CompanyCampaign character registry/ID-member references already provide the ownership boundary. Extend this root for candidate pools, former members, debt/grace dates, shared inventory and explicit pending choices; do not duplicate Units when moving between pools/roster/former members.
- Unit's full constructor reads species settings/graphics, initializes traits and appearance, creates Items but hardcodes ItemUses/ItemCooldowns to3, runs SetGear, random skills/uniforms and reincarnation checks. SetGear, GetBestMelee/Ranged, book paths and various legacy actions obtain definitions through State.World.ItemRepository. Provide an explicit company item context; do not construct World simply to supply a catalog because its constructor changes global settings/State.World.
- Existing Leader adds large fixed stats, Leadership10, naming prefixes and customizations. These are not automatically the company leader's desired bonuses. Reuse the shared leveling/Unit framework with approved or provisional personal bonuses, no aura, immutable creation appearance/name and free wages.
- UnitCustomizer plus UI/Connectors/CustomizerPanel can supply appearance controls, but CustomizerPanel callbacks currently reach old UnitEditor/Recruit_Mode. Creation must own these callbacks and disable renaming/customization afterward. Ordinary roster nicknames remain distinct.
- Nine-slot agreement: helmet, torso, legs, arms, accessory, melee, ranged, spell1,spell2. Unit.SetMaxItems currently shrinks to2/3 and is called by trait refresh/copies/forms. Migrate item/use/cooldown storage and all active UI/AI consumers together. Resourceful and BookEater assumptions require explicit review rather than silent item loss.
- GetBestMelee already scans weapons with range<=1 and GetBestRanged range>1; preserve input1/2 and these selection semantics with legal dedicated slots. Natural weapons remain fallback. Do not add weapon-switching chores.
- Repository's legacy armor-named items are generic stat accessories (helmet+agility, torso+endurance, gauntlet+strength, gloves+dexterity, shoes+agility), not an existing full armor/body-part protection system. Introduce stable company item-definition IDs/slot eligibility and protection hooks; do not claim legacy names already implement injury prevention. Existing equipment hooks can perform lasting healing; the separate temporary-HP policy must be respected before exposing those items in shops.
- Unit.SetItem mutates accessory stats/current HP, may consume spellbooks through BookEater and updates cooldown arrays. Loading/rebinding must not re-equip in a way that compounds stat bonuses; retain symmetric HP changes and safe near-zero behavior.
- Daily wages plug into CompanyClock Wages stage and explicit PendingDecision. At unaffordable payday everyone accrues debt; group payment only by user choice. Preserve remaining rest days and the full grace day after returning control. Rehire preserves identity/equipment/injuries/debt, and adds debt to the prorated advance quote. No automatic low-experience dismissal, supplies ledger or roster cap.
- Preview snapshots remain preview data; actual campaigns need creator validation, approved species and tested nonempty Unit/item round trips. Loading must not resurrect the retired World format or overwrite global content settings.
- Map renderer must reuse retained StrategicTileDictionary assets (StrategyMode.TileDictionary reference) as read-only artwork, consuming company terrain/discovery. Do not reactivate the old StrategyMode scheduling just to draw tiles. Glyph preview is not final art direction.

## Grouping and validation

Implement the connected milestone, not one-species/one-control micro-handoffs: approved curation and independent Unit creation; roster/candidate/former-member/debt state; nine-slot catalog/equip/stat integration; creator/recruitment/company/shop screens and saved state. Keep internal compile/source/GPL checks; one integrated manual workflow when stable. Source-specific battle regressions deferred by cleanup should run as soon as curated skirmish units can launch, with genuine blockers reported instead of pretending campaign combat integration is already complete.

No source/runtime/scene/asset changes in this preparation turn. No build or repeated manual test needed. Existing source notices, license and attribution untouched. Next action: incorporate requested user decisions, then implement the grouped milestone; do not infer selections/tuning from this report.
