# Spec34 slice5 — Gear progression and recruitment costs

Implemented 2026-11-04 after user acceptance of slice4. User confirmed whole-tier reputation unlocks, the same stock in every town, unlimited quantities, and charging recruits' equipment alongside advance wages. User explicitly requested increased new-campaign starting funds to compensate. Values below are provisional tuning, not measured win-rate/economy calibration.

## Catalog and availability

Three tiers unlock at reputation0/10/25. Unlocks reveal the whole tier and retain lower tiers. The merchant filters/paging use the same eligible-stock function as the purchase command; direct calls cannot bypass reputation. Owned equipment remains usable/sellable at lower reputation. No town inventory, restock clock, limited quantities, crafting or regional system was added. Each purchase creates a distinct stable item instance.

Weapons offer accuracy-rating versus damage alternatives at every tier, plus the retained longer-range Compound bow. Existing accuracy mechanics remain rating multipliers; no silent change to flat final-odds accuracy. Each armor slot has AC and evasion alternatives at every tier, with positive defenses and no armor penalties. Tiered charms cover the four stats. Existing Power Bolt/Valor/Shield books remain; no unreviewed spells introduced. Names are descriptive placeholders using existing generic weapon artwork; no narrative/new art work.

Existing definition IDs, serialized damage/range/accuracy/stat bonuses and ownership are preserved. Existing armor definitions keep slice4's AC/evasion. New item instances use the new catalog, while old item values stored in saves are not rewritten. Transactions and hiring quotes use current catalog prices; sale proceeds remain half the catalog price. This can revalue prototype-era items but does not grant treasury adjustments on load.

## Provisional investment scale

| Category | Tier I | Tier II | Tier III |
| --- | ---: | ---: | ---: |
| Melee weapons | 70–100 | 180–200 | 750–900 |
| Ranged weapons | 90–120 | 230–260 | 850–1000 |
| Armor pieces | 30–60 | 120–240 | 450–950 |
| Stat charms | 60 | 220 | 800 |

Books cost30/60/60. Contract rewards remain rating×60 with the existing modest rounded variation: rating1 roughly60, rating4 roughly240, rating9 roughly540. A basic melee weapon therefore exceeds one typical rating1 reward; a masterwork weapon is several gross rewards even from stronger offers. Fully equipping multiple mercenaries competes with wages and treatment. No contract gold, reputation gain, wage amounts or payday rules were altered to fund gear.

Full heavy sets provide AC5/10/15. Full evasive sets provide12/20/30percentage-points evasion. Heavy armor retains the modest existing stat bonuses; light pieces generally trade those for evasion, with leg armor's existing STR bonus retained. These sets need balance review, especially combined with traits. No new cap or penalty was invented. Controlled balance simulations remain slice6.

## Recruitment and generated loadouts

Hire quote = current equipment value + prorated advance wages + full personal debt. This applies to first hires and former mercenaries returning with retained gear. Hiring pays the quoted total once, clears debt and transfers gear into canonical company inventory. Dismissing someone leaves their worn equipment with them as before. Stripping/reselling cannot produce profit because the full gear cost was paid while resale returns half. The UI itemizes gear/advance/debt on recruitment rows and detail pages.

New-campaign starting gold increases50→260. Starter gear remains Mace70, Simple bow90, and mage Mace70 + Power Bolt30, with weekly wage18–22. A full-week starter hire costs88–122. Any two cost at most244, while the cheapest three cost at least264: two comfortable starter choices, with a remaining budget depending on composition. The leader's initial gear remains part of character creation; it is not deducted. Existing campaigns receive no top-up, and debt/wages remain unchanged.

New generated higher-level sapients draw role-appropriate weapons/armor from tier I at levels1–4, II at5–9, III at10+. Level1 retains its guaranteed basic role weapon; armor still starts at level3 with a second piece at6, preserving the original loadout cadence. The scorer prices actual stats/gear, so fixed contract budgets remain independent of player reputation/treasury and do not use item prices. Recruitment still respects its power ceiling. Existing saved candidate/roster/enemy loadouts are never rerolled; leveling a current member does not automatically upgrade equipment.

## Validation and scope

Extend the existing authorized isolated Unity audit only. Synthetic saves stay under Logs and are preserved; deletion is intercepted, no personal saves. Check all catalog IDs/slots/prices, reputation boundaries, lower-tier retention, shared town stock, unlimited unique purchases, core purchase rejection, AC/evasion alternatives, merchant visibility/paging, starter affordability, itemized prorated hiring/debt, resale exploit prevention, former gear valuation and save/reload without treasury top-up. Prior trait/injury/combat/equipment checks remain.

Source paths/notices are recorded in CHANGES. Original fork code and existing generic artwork only; no dependencies/assets/imports/scenes/prefabs added or files moved/deleted. License4–5 reviewed, prior legal notices/attribution preserved. Existing distribution/provenance/Corresponding Source gates remain open. Build/check results recorded at handoff below.

Stop after slice5. Next slice6 is controlled balance reporting, not automatic tuning or new narrative. Help rewrite/post-battle events remain deferred.

## Handoff

Native runtime1 passed883 checks (731 prior plus152 progression checks), actual editor exit0. Catalog report has52 shop items:19/17/16 in tiers I/II/III, plus one enemy-only slug weapon excluded from shops. No C#/shader errors or new compiler warning messages; fresh Windows build `Builds/Warband-Review-20261104-15ad5c3f/Win64/VoreWar.exe` exit0,41 unchanged distinct compiler warnings. Seven existing notices packaged/hash-verified. No graphical playtest or balance calibration claimed.

Evidence: Logs/progression34e-runtime1-result.json, runtime1.log, progression34e-catalog.json, build.log and progression34e-result.json. Source dates/CHANGES reviewed, README points to the fresh build; earlier builds/logs remain. User review: fresh campaign treasury260, recruit quote/affordability, merchant categories/tier notices and purchases. Existing campaigns retain their treasury and existing gear. No request to grind25 contracts merely to retest automated unlock boundaries. Next work awaits user direction on slice6.
