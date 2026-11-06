# 40C — Spellbook variety and progression

Planning draft, 2026-11-06. Part of [module 40](40-authoring-and-campaign-progression.md). Depends on 40A's authoring and effective-data boundary.

## Current foundation

`Assets/Scripts/Core/CompanyEquipment.cs` provides explicit equipment IDs, two spell slots, merchant categories, whole-tier reputation gates, common unlimited stock and gear-inclusive recruitment quotes. Its current books provide Power Bolt, Valor and Shield. `CompanyPower.cs`, tactical AI, spell previews and native damage resolution must all understand additions.

The existing weapon/armor economy and accepted calibration are already implemented. This phase expands spell choices and their progression.

## Design work and bounded implementation

1. Inventory retained spells by practical role, targeting, AP/mana costs, duration, friendly fire, AI support, retirement status and existing bugs. Present a compact mechanical shortlist for curator approval.
2. Select a small first catalog covering useful distinctions such as direct damage, area damage, protection and control. These are proposed roles, not approved spell selections or a requirement to add new effects.
3. Assign approved books to existing reputation tiers with provisional prices and power values, using 40A data where supported. Preserve stable inventory IDs and existing owned items.
4. Check equipped books, innate spells, NaturalCaster and class/focus behavior together. Avoid duplicate spell offerings, free gear value exploits and abilities that bypass the accepted action economy.
5. Check actual AI use, target selection, previews and temporary-effect cleanup. Permanent spell healing, resurrection, conversion, polymorph and other retired mechanics remain excluded.
6. Measure meaningful tradeoffs in native battles and connected economic scenarios. Report provisional costs and the situations where each book helps before requesting one grouped player review.

## Save and grant boundaries

Existing books retain their IDs and saved state. Import validation rejects unknown or retired spell references. New catalogs do not reroll existing recruits, accepted forces or owned items. Any proposed rebalance to an existing definition follows the compatibility policy chosen in 40A. Newly generated enemy forces must be scored for their actual spells and mana capability within the fixed rating budget.

## Planned grouped acceptance

Cover both book slots, equipment swaps, duplicate spell sources, mana/AP limits, damage previews, area targeting, AI cast decisions, temporary statuses, save/resume, pricing and gear-inclusive hiring. Reuse native Battle Lab and simulation infrastructure with synthetic fixtures. Player review covers choosing and using the approved catalog, without a narrative-writing requirement.

## Decisions before implementation

Approve the mechanical shortlist and any genuinely new spell behavior. Confirm acceptable control effects and durations where the inventory identifies ambiguity. Derive provisional prices and scoring through calculation and simulation, following the user's existing tuning preference.
