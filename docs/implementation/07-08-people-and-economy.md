# 07–08 — People and economy milestone

Implemented and combined runtime checklist user-confirmed, 2026-11-01. This is the next connected milestone after the user-accepted spec06 map preview. Contracts and company battles remain specs09–10, and injury/recovery progression remains11.

## Approved curation

The user selected exactly these existing species and authorized provisional tuning:

| Species | Kind | Initial direction |
|---|---|---|
| Avians | Sapient | Ranged; favored Dexterity, existing talents retained |
| Lizards | Sapient | Magic; favored Mind, NaturalCaster added to existing talents |
| Gnolls | Sapient | Melee; favored Strength, existing talents retained |
| SpitterSlugs | Animal | Weak ranged support, lower stats and natural-weapon damage |

All three sapients may choose any starting focus and use flexible shared leveling. Animals cannot be leaders/recruits. The global availability page exposes only these four; disabling generation preserves existing candidates, members and former members. The retired90 remain retired. Existing developer Race Editor override rows retain precedence; they are not silently reset by curation. Those personal rows may override the new defaults.

## Connected workflow

Campaign setup now offers real leader creation alongside the older map-only preview. The creator reuses the existing appearance controls with company-owned callbacks and an inline name field; it does not enter the retired recruitment mode. Confirming locks leader name/appearance, starts with the leader alone in a generated town, and creates varied named recruits with flavor biographies. The leader is free, always active, and uses an ordinary Unit with personal starting bonuses rather than the old empire Leader subclass/aura.

Recruitment, roster, nicknames for ordinary members, active/reserve selection, member details, level-up choices, nine-slot loadouts and a merchant are connected. No roster cap is introduced. Former members retain the same canonical record, Unit, level, equipment and debt. Their reappearance is rolled on visiting a different town, outside the ordinary pool; no travel is simulated. Departures/dismissals are not deaths. Refresh replaces only never-hired candidates.

The map now draws the original `StrategyMode.TileSprites` terrain using `StrategicTileInfo.GetTileType`. Army icons are not mistaken for terrain. Town/company markers remain simple letters; this is an interim interface, not final campaign presentation. Existing terrain sprites, bundles, scene and prefabs were not edited.

## Provisional tuning

| Setting | Current provisional value/policy |
|---|---|
| Starting gold | 50 |
| Initial recruit weekly wages | 18–22, quoted individually |
| Wage growth | +6 per level; new rate at the next payday, existing debt unchanged |
| Leader bonus | +3 to each of eight non-Leadership starting stats |
| First/recurring payday | Seven elapsed days after start, then every seven days; pays in advance |
| Hire charge | Ceiling of weekly wage × remaining days / 7, plus all retained debt |
| Ordinary recruit pool | 4, plus up to6 from reputation; seven-day refresh |
| Reputation hooks | More candidates and gradually higher generated levels/gear; reputation earning is09 |
| Unpaid grace | Weekly wage42+:1 day; 30–41:4 days; below30:10 days |
| Former reappearance | One-in-three roll per former member when visiting a different town |
| Shop | Basic weapons4–12, armor4–8, accessory4, spellbooks8; spare items sell for half |

These are review values rather than final balance. Wages alone cover the assumed daily needs; there is no separate food/upkeep ledger.

If a clean payday is affordable, it pays everyone. Otherwise each employed mercenary accrues their full new weekly wage as personal debt, with no automatic recipient selection. The decision shows gold, total debt, shortfall and imminent departures. It offers full payment of the threatened group only when affordable, company management for individual full repayment, or acknowledgment without payment. Old debts carry forward and another missed payday adds another full week.

Multi-day rest pauses at required decisions. The daily stage cursor and remaining rest days persist. Acknowledging finishes the interrupted day without automatically consuming later grace days; remaining rest can be resumed or cancelled explicitly. Low-paid members can remain through a second missed payday. No accelerated healing or sparring is pretended here: recovery systems remain11.

## Equipment and persistence boundary

Company members have helmet, torso, legs, arms, accessory, melee, ranged and two spell slots. Each item has a stable definition ID and per-instance ID. One canonical inventory contains owned spare and worn gear; a worn instance cannot be assigned twice or sold. The whitelist offers no potions, permanent-healing gear or retired ability grants. Armor has a protection input for the later injury system; its current stat bonuses are provisional and no appearance changes are promised.

Maximum-HP gear changes current HP by the same delta. A removal that would reduce current HP below1 is rejected and restored, preventing repeated clamp-to-one free healing. Company MaxHealth reads do not mutate health. Loading rebinds the item service without invoking SetItem or reapplying accessory bonuses. Existing melee/ranged selectors and natural fallback are reused; actual company attack/input/cooldown integration is still09–10.

The company flag preserves nine parallel item/use/cooldown slots through trait refresh. Resourceful does not shrink/expand those fixed slots; BookEater does not consume slotted company books. Standalone Pure Tactical retains its existing two/three-slot setup. Copy/temporary actor interactions with nine-slot combat equipment require the later battle milestone review.

The new company root, canonical people/items, cached level choices, debts/deadlines, pool refresh dates, daily cursor and rest remainder use the existing Odin checkpoint boundary. Level choices are checkpointed before display to avoid Ironman rerolls. Normal mode has a checkpoint browser/history; Ironman permits the latest valid checkpoint only. All immutable generations remain retained under the no-delete policy. Live battles still cannot be saved; between-battle saving remains the current supported boundary. Old map-only saves stay previews and do not acquire invented people or cash.

## Source trace and scope

- `Core/SpeciesPolicy.cs` and `Races/RaceParameters.cs`: approved roles, generation filters and provisional species defaults.
- New `Core/CompanyEconomy.cs`: creation, isolated seeded generation, hire/rehire, wages/debt, pool refresh and shared leveling.
- New `Core/CompanyEquipment.cs`: catalog, ownership, typed slots, safe equipment transactions and price/power/protection inputs.
- `Core/CompanyCampaign.cs` and `Core/CompanyCampaignSave.cs`: canonical economy state, validation, item-service rebinding and checkpoint navigation.
- `Entities/Units/Unit.cs`, `Entities/Items/Item.cs`, `Entities/Items/ItemRepository.cs`, `Utility/Rand.cs`: independent item context, nine-slot flag/IDs, weak slug weapon, seeded generation.
- `Entities/Units/Actor_Unit.cs`: portrait/best-weapon rendering uses the Unit item context rather than requiring the retired World.
- `Subwindows/CompanyCampaignView.cs`, new `Subwindows/CompanyManagementUI.cs`, `UI/Connectors/CustomizerPanel.cs`: map, creator and connected management workflow.
- `Assets/Editor/SpeciesResourceAudit.cs`: existing read-only scene-wiring inspection extended; does not enter Play mode or save assets.

All fifteen C# sources have dated fork notices. Three new runtime sources have matching metadata. Exact paths and licensing check are in root CHANGES.md. No scene/prefab/artwork/dependency upgrades, file deletion/move, commit or publication are included.

## Checks and limits

- Unity2018.3.12f1 compilation: passed, return0 (`Logs/people-economy-compile-handoff.log`). Earlier compile mistakes were corrected; the final log is the result to use.
- Explicit `Assets/base.unity` opening with final source: passed, return0 (`Logs/people-economy-scene-handoff.log`). The earlier `people-economy-scene.log` also passed before the final portrait-provider fix.
- Read-only scene-wiring inspection: passed, return0 (`Logs/people-economy-wiring.log`, JSON report). Creator, portrait/name/gender/pronoun controls, button panel/prefab, image prefab and72 terrain sprites exist. This proves references, not runtime layout or input behavior.
- Source notice/diff and resource checks: `Logs/people-economy-source-review.json` records hashes and warning delta. Existing scene bytes and1248 resource hashes match the previous accepted state, including the expected previously cleaned Sprite Dictionary prefab.
- The compile retains existing warnings and adds reserved-field CS0649 warnings for company Reputation and InBattle, both populated by the later contract/battle milestone. No warning-free claim.
- Automated window capture failed twice (`FrameArrived`/window-capture timeouts). No successful Play-mode, hiring/equipment/debt save round-trip, wage decision, or battle run is claimed. No new test framework/tests were introduced.

## One combined player review

1. Open the campaign, create/customize/name a leader from the approved sapients, choose any focus and confirm. Enter the starting town, inspect recruits/backstories and hire approximately two. Confirm ordinary-member nicknames/reserve status and the always-active free leader.
2. Buy/equip affordable gear, including separate melee/ranged weapons when funds allow. Change HP-granting gear and check matching current/max changes. Save, return to menu and load: names, portraits, roster, gold, gear and stats should remain identical without extra bonuses. Confirm original terrain artwork while traveling.
3. Rest through the first payday. With two hires, expect insufficient cash and personal debt. Confirm the choice dialog, company access, interruption/resume/cancel of remaining rest, and individual full repayment when affordable. Continue days to warnings/departures; reload once while interrupted. Departures must preserve debt without a death/memorial result.
4. Inspect normal checkpoint history and optionally make a separate Ironman company, whose UI exposes only its latest valid state. Optional: dismiss a member, visit different towns until they reappear, and check exact retained state plus debt in the rehire quote.
5. Now that curation permits battles, perform one separate Pure Tactical smoke battle using the approved roster, including weak ranged SpitterSlugs. This checks restored combat access; it is not a company mission or acceptance of the later downed/fate conversion.

A single report of these connected checks is the next gate. After acceptance, proceed to grouped09–10 contracts/enemy power generation and actual battle/results/fate integration; do not restart accepted cleanup or request another settings/menu tour.


User confirmation2026-11-01: all listed review items are confirmed working; proceed to grouped09–10. This is user-reported runtime evidence, separate from agent compile/scene checks.
