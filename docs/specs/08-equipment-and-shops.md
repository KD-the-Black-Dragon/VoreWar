# 08 — Expanded equipment and town commerce

**Status:** Implemented with provisional tuning; combined runtime review user-accepted, 2026-11-01. See [milestone implementation and limits](../implementation/07-08-people-and-economy.md). Battle/injury integration remains09–11.

**Phase:** Phase 3 — build the first company campaign loop

**Prerequisites:** 07 roster and curated species; 06 towns/saving.

## Purpose and scope

### Preparation from study 01C

Use [the combat/equipment study](../architecture/01c-combat-equipment-and-results.md) and [reference inventory](../architecture/01c-equipment-reference-inventory.md). Raising NewItemSlots alone is insufficient: SetMaxItems forces inventories back to two/three, while uses and cooldown arrays have separate fixed sizes. Trait refresh, copies, and morphs can call these paths. Equipment loops over Items.Length but indexes the parallel arrays.

SetItem adds/removes Accessory bonuses directly in stored Stats and alters current HP for Endurance accessories. Define canonical loadout/stat ownership before changing equip/restore behavior; check for duplicated bonuses, HP churn, and shared copy/shifter arrays. Best-weapon selection and old slot-zero/fixed-gear shop checks need review with the new hand/slot rules. Body armor coverage is new data, not cosmetic clothing by itself.

Potion retirement includes both inventory/stock/refill and equipment health/mana consumable callbacks. Preserve retained equipment lifecycle hooks while replacing their Army/strategic-day arguments as needed. Test full loadouts through trait refresh, cooldowns, save/load, and any retained morph/copy/revert mechanic when implementation is authorized.

### Agreed scope

- Implement nine typed slots: helmet, torso armor, leg armor, arm armor, accessory (ring/amulet), melee weapon, ranged weapon, and two spells. Armor coverage representation and species compatibility need refinement before the relevant batch.
- Expand item/stat aggregation, equip/unequip behavior, inventory, item display, and saving for those slots. Visible armor changes are not a current goal.
- Keep attack keys 1 (melee) and 2 (ranged); use each corresponding equipped weapon automatically, with natural/unarmed melee fallback as appropriate. No additional weapon-selection interface is requested.
- Maximum-HP equipment changes current and maximum HP in tandem on equip and removal. Preserve symmetric adjustments without repeated free heals; define safe near-zero unequip/clamping behavior before implementation. See [the clarification record](../design-clarifications-2026-10-30.md).
- Allow equipment changes outside battle; remove potion gameplay without removing shared item/stat behavior needed by retained gear.
- Provide town buying/selling and basic starter weapons; no separate food, ration, supply, or upkeep ledger.
- Make gear contributions accessible to the enemy/recruit power model and armor protection accessible to the injury system.
- Apply eligibility/compatibility rules to enemy equipment as well as company members.

### Preparation from study 01D

[01D](../architecture/01d-campaign-persistence-and-identity.md) traces legacy item repository reconstruction and name-based remapping through Unit.UpdateItems/SetItem. New saves need stable item-definition identity, nine-slot/per-instance uses/cooldowns, and refresh that does not reapply accessory bonuses. Cover candidates, members, former members, target enemies and temporary clone loadouts; canonical ownership cannot depend on the current repository object's reference or a mutable display name alone.

## Completion and review criteria

- Complete legal loadouts display correct combined stats before/after changes and save/load.
- Invalid equipment combinations are explained and cannot duplicate or lose items.
- Equipment remains unchanged by attempts to alter it during tactical battle.
- Shops support basic company preparation with clear gold changes; removed potions are not offered or generated.

## Decisions, constraints, and risks

- Prices, inventory capacity, item variety, and armor/slot compatibility remain design/tuning decisions. The nine-slot set is agreed.
- Battlefield gear loot remains undecided. The user leans toward equipment being destroyed with a digested member; do not implement guaranteed drops or salvage from that preference alone.
- Do not invent durability, repair bills, ammunition tracking, or new supply chores.
- Audit AI/stat consumers that assumed the legacy two-item limit.

## Deliverable

Expanded loadouts, item compatibility, town commerce, and item power/armor inputs.

## Execution record

- Source trace and exact changes: [07–08 implementation](../implementation/07-08-people-and-economy.md).
- One grouped people/economy implementation; user selected Avians/ranged, Lizards/magic, Gnolls/melee and animal SpitterSlugs, and authorized provisional tuning.
- Unity compilation and scene opening passed; read-only wiring/source/resource checks performed. Actual creation, hiring, debt, equipment and nonempty save round-trips await the combined runtime review. No Play-mode or battle pass claimed.

For subsequent work, refine this spec from actual runtime findings and resolve decisions needed for the next milestone. Follow the review and file-operation rules in [the roadmap](../conversion-roadmap.md). Design intent comes from [the campaign plan](../mercenary-company-campaign-plan.md) and the three curation checklists.



## Grouped milestone preparation (2026-11-01)

User authorized grouped07–08 following accepted06 preview. [Source/dependency preparation](../implementation/07-08-people-economy-preparation.md) records item/Unit/customizer/clock integration and required species/tuning decisions. Those decisions have now been supplied explicitly and implemented; the preparation report is historical, and the implementation report above is current. One connected review milestone, not per-species/control tests.
