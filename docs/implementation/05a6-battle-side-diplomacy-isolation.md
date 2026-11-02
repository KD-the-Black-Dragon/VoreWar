# 05A6 — battle side/diplomacy isolation

**Date:** 2026-11-01. User accepted 05A5. Three sources changed in one grouped battle-provider stage.

## Trace and implementation

Standalone combat already used explicit attacker/defender sides, fixed loyalty, apparent disguise sides and per-side human/AI settings. Shared helper methods also contained alternate empire alliance/neutrality and LastHumanEmpire branches. Because the traditional campaign is retired, those alternatives are no longer the correct provider for a future company battle.

- `Assets/Scripts/Modes/Tactical/TacticalUtilities.cs`: retain former PureTactical paths for IsUnitControlledByPlayer, GetPreferredSide, TreatAsHostile and IsUnitControlledBySide. Preserve self/friendly-target exceptions, original fixed/apparent-side comparisons and existing developer-control overrides. PlayerCanSeeTrueSide and UnitCanSeeTrueSideOfTarget retain the former MainEmpires-null standalone behavior. Remove only the alternate empire/diplomacy paths and unused local variables; no default hostility/targeting rule invented. Existing method signatures remain, including side parameters no longer needed by GetPreferredSide.
- `Assets/Scripts/Modes/TacticalMode.cs`: remove InitRetreatConditions and its three callers. The method only assigned campaign policies when PureTactical was false; existing standalone AI constructors/default RetreatPlan are unchanged. Do not substitute a company retreat threshold here. Remove the world-empire AI-skip override and village empire-color block, both inactive for the standalone World context. Initial side/name/race compatibility metadata, manual retreat, normal AI/skip/UI flow and map/scenery remain.
- `Assets/Scripts/Entities/Units/Unit.cs`: remove four identical strategic AI CheatLevel XP multipliers from GiveExp/CalcGiveExp/GiveScaledExp/CalcScaledExp, and the non-PureTactical empire-trait reinjection branch in ReloadTraits. Keep ordinary flat/scaled XP, vore/trait modifiers, level-up choices and all other trait sources. The retained standalone World had no matching empire/StrategicAI, so its existing behavior is preserved.

No RelationsManager, GetEmpireOfSide, MainEmpires/AllActiveEmpires or StrategyMode provider reference remains in TacticalUtilities. TacticalMode retains only SwitchToStrategyMode as its existing mode-return dispatcher; that dispatcher already routes Pure Tactical to the menu. Unit has no StrategicAI/CheatLevel/EmpTraits provider left. Army/Empire lightweight race/name/color/capacity metadata still exists and has not been erased.

## Scope and remaining review

No new company mode, morale/retreat design, health/Downed/injury, hostility exception, species approval or save format. Temporary disguise apparent-side behavior and ordinary summons remain; this is provider isolation, not another ability retirement. Active skirmish numeric and team logic is preserved, while obsolete campaign alternatives are removed.

Remaining boundary work: strategic map/grid/rendering callers, save/context ownership, residual Army/Empire/Village data versus runtime code and species stat/graphics/override readers. Next consolidate that residual inventory/cleanup completion review before new company implementation. Do not treat class-name retention as proof a hidden empire scheduler is still running, or the empty roster as proof of battle correctness.

## Verification

Unity 2018.3.12f1 completion compilation PASSED exit 0 without C# errors/import exceptions: Logs/packet-05a6-compile-complete.log (initial compile log retained). No new warnings versus 05A5. Explicit base.unity opening PASSED exit 0 without parse errors/exceptions: Logs/packet-05a6-scene.log. Exact three-source correspondence, dated notices, unchanged scene bytes, license hash and scoped diff checks passed; no Unity process remained. Exact source edits and snapshots in ignored Logs/packet-05a6-*; final correspondence review must distinguish preserved PureTactical/MainEmpires-null expressions from removed legacy paths. No new tests/framework, agent Play-mode battle, player build or personal-data access. Actual ordinary/village/AI/bulk/disguise regression checks remain pending approved species.

## GPL and scope

License.txt sections 4/5 reviewed; preserve license/attribution and earlier notices. All three sources receive dated notices, with exact paths in CHANGES.md. No third-party source/dependencies/assets, file deletions/moves or scene/prefab/schema edits. Distribution review remains separate.

## One combined user check

Open main menu and enter/leave Pure Tactical setup without new red errors. No repeated settings/editor tests or sample species needed. Combat behavior still requires later regression after curation; startup checks verify only the available application shell.

User confirmed the requested screens opened without new red errors and authorized the next stage (2026-11-01). Actual combat regressions remain pending species curation.
