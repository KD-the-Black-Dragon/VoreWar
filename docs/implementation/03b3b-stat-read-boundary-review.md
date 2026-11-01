# 03B3B — stat read-boundary review

**Date:** 2026-10-31. Documentation-only review after user acceptance of 03B3A. No prototypes or game code changed.

## Finding and order adjustment

Removing the 90 retired RaceParameters prototypes now would leave live metadata and legacy-world consumers. RaceParameters.GetTraitData falls back to Default for unknown cases; merely removing switch cases would silently give rejected IDs generic stats. Throwing or returning null in RaceSettings.Get would instead break callers that immediately dereference the result. Neither is the intended retirement behavior.

The 03B3A Sanitize guards protect that loading pass only. RaceSettings.Get still returns an old row or lazily constructs RaceSettingsItem. That constructor requests both parameter and graphics data. GetRaceTraits also uses a temporary short Unit; the full Unit constructor asks for graphics, name/appearance and traits. NPC stats and regular Unit stats use GetRaceStats. Retiring the stat registry alone does not isolate these dependencies.

## Direct Get caller inventory

These are lexical call-site lines, not an exhaustive runtime/reflection graph. Internal Get calls within RaceSettings itself and indirect typed getters are additional routes.

| Source | Current line |
|---|---:|
| [Assets/Scripts/AI/RaceServantTacticalAI.cs](../../Assets/Scripts/AI/RaceServantTacticalAI.cs) | 38 |
| [Assets/Scripts/Core/State.cs](../../Assets/Scripts/Core/State.cs) | 1184 |
| [Assets/Scripts/Core/State.cs](../../Assets/Scripts/Core/State.cs) | 1185 |
| [Assets/Scripts/Core/State.cs](../../Assets/Scripts/Core/State.cs) | 1195 |
| [Assets/Scripts/Core/State.cs](../../Assets/Scripts/Core/State.cs) | 1196 |
| [Assets/Scripts/Core/State.cs](../../Assets/Scripts/Core/State.cs) | 1231 |
| [Assets/Scripts/Core/World.cs](../../Assets/Scripts/Core/World.cs) | 77 |
| [Assets/Scripts/Entities/MercenaryHouse.cs](../../Assets/Scripts/Entities/MercenaryHouse.cs) | 123 |
| [Assets/Scripts/Entities/Village.cs](../../Assets/Scripts/Entities/Village.cs) | 1448 |
| [Assets/Scripts/Entities/Village.cs](../../Assets/Scripts/Entities/Village.cs) | 1520 |
| [Assets/Scripts/Subwindows/RaceEditorPanel.cs](../../Assets/Scripts/Subwindows/RaceEditorPanel.cs) | 311 |
| [Assets/Scripts/Subwindows/RaceEditorPanel.cs](../../Assets/Scripts/Subwindows/RaceEditorPanel.cs) | 320 |
| [Assets/Scripts/Subwindows/RaceEditorPanel.cs](../../Assets/Scripts/Subwindows/RaceEditorPanel.cs) | 329 |
| [Assets/Scripts/Subwindows/RaceEditorPanel.cs](../../Assets/Scripts/Subwindows/RaceEditorPanel.cs) | 338 |
| [Assets/Scripts/Subwindows/RaceEditorPanel.cs](../../Assets/Scripts/Subwindows/RaceEditorPanel.cs) | 374 |
| [Assets/Scripts/Subwindows/RaceEditorPanel.cs](../../Assets/Scripts/Subwindows/RaceEditorPanel.cs) | 383 |
| [Assets/Scripts/Subwindows/RaceEditorPanel.cs](../../Assets/Scripts/Subwindows/RaceEditorPanel.cs) | 392 |
| [Assets/Scripts/Subwindows/RaceEditorPanel.cs](../../Assets/Scripts/Subwindows/RaceEditorPanel.cs) | 401 |
| [Assets/Scripts/Subwindows/RaceEditorPanel.cs](../../Assets/Scripts/Subwindows/RaceEditorPanel.cs) | 425 |
| [Assets/Scripts/Subwindows/RaceEditorPanel.cs](../../Assets/Scripts/Subwindows/RaceEditorPanel.cs) | 448 |
| [Assets/Scripts/Subwindows/RaceEditorPanel.cs](../../Assets/Scripts/Subwindows/RaceEditorPanel.cs) | 463 |
| [Assets/Scripts/Subwindows/RaceEditorPanel.cs](../../Assets/Scripts/Subwindows/RaceEditorPanel.cs) | 678 |
| [Assets/Scripts/Subwindows/RaceEditorPanel.cs](../../Assets/Scripts/Subwindows/RaceEditorPanel.cs) | 975 |
| [Assets/Scripts/Subwindows/RandomizerTraitEditor.cs](../../Assets/Scripts/Subwindows/RandomizerTraitEditor.cs) | 177 |
| [Assets/Scripts/Subwindows/TaggedTraitEditor.cs](../../Assets/Scripts/Subwindows/TaggedTraitEditor.cs) | 30 |
| [Assets/Scripts/Races/Graphics/DefaultRaceData.cs](../../Assets/Scripts/Races/Graphics/DefaultRaceData.cs) | 237 |
| [Assets/Scripts/Races/Graphics/DefaultRaceData.cs](../../Assets/Scripts/Races/Graphics/DefaultRaceData.cs) | 238 |
| [Assets/Scripts/Races/Graphics/DefaultRaceData.cs](../../Assets/Scripts/Races/Graphics/DefaultRaceData.cs) | 369 |
| [Assets/Scripts/Races/Graphics/DefaultRaceData.cs](../../Assets/Scripts/Races/Graphics/DefaultRaceData.cs) | 498 |
| [Assets/Scripts/Races/Graphics/DefaultRaceData.cs](../../Assets/Scripts/Races/Graphics/DefaultRaceData.cs) | 515 |
| [Assets/Scripts/Races/Graphics/DefaultRaceData.cs](../../Assets/Scripts/Races/Graphics/DefaultRaceData.cs) | 516 |
| [Assets/Scripts/Races/Graphics/DefaultRaceData.cs](../../Assets/Scripts/Races/Graphics/DefaultRaceData.cs) | 528 |
| [Assets/Scripts/Races/Graphics/DefaultRaceData.cs](../../Assets/Scripts/Races/Graphics/DefaultRaceData.cs) | 529 |
| [Assets/Scripts/Modes/Strategic/StrategicUtilities.cs](../../Assets/Scripts/Modes/Strategic/StrategicUtilities.cs) | 699 |
| [Assets/Scripts/Modes/Strategic/StrategicUtilities.cs](../../Assets/Scripts/Modes/Strategic/StrategicUtilities.cs) | 728 |
| [Assets/Scripts/Modes/Strategic/StrategicUtilities.cs](../../Assets/Scripts/Modes/Strategic/StrategicUtilities.cs) | 755 |
| [Assets/Scripts/Modes/Strategic/StrategicUtilities.cs](../../Assets/Scripts/Modes/Strategic/StrategicUtilities.cs) | 1251 |
| [Assets/Scripts/Modes/Strategic/StrategicUtilities.cs](../../Assets/Scripts/Modes/Strategic/StrategicUtilities.cs) | 1298 |
| [Assets/Scripts/Entities/Units/PredatorComponent.cs](../../Assets/Scripts/Entities/Units/PredatorComponent.cs) | 1575 |
| [Assets/Scripts/Entities/Units/PredatorComponent.cs](../../Assets/Scripts/Entities/Units/PredatorComponent.cs) | 1584 |
| [Assets/Scripts/Entities/Units/PredatorComponent.cs](../../Assets/Scripts/Entities/Units/PredatorComponent.cs) | 1595 |
| [Assets/Scripts/Entities/Units/PredatorComponent.cs](../../Assets/Scripts/Entities/Units/PredatorComponent.cs) | 1605 |
| [Assets/Scripts/Entities/Units/PredatorComponent.cs](../../Assets/Scripts/Entities/Units/PredatorComponent.cs) | 1613 |
| [Assets/Scripts/Entities/Units/PredatorComponent.cs](../../Assets/Scripts/Entities/Units/PredatorComponent.cs) | 1638 |
| [Assets/Scripts/Entities/Units/PredatorComponent.cs](../../Assets/Scripts/Entities/Units/PredatorComponent.cs) | 2179 |
| [Assets/Scripts/Entities/Units/Unit.cs](../../Assets/Scripts/Entities/Units/Unit.cs) | 1316 |
| [Assets/Scripts/Entities/Units/Unit.cs](../../Assets/Scripts/Entities/Units/Unit.cs) | 1393 |

## Caller dispositions

| Group | Evidence / disposition |
|---|---|
| Retained combat and appearance | Unit.cs, NPC_unit.cs, PredatorComponent.cs, DefaultRaceData.cs, RaceServantTacticalAI.cs and StrategicUtilities power helpers consume data synchronously. Preserve valid existing nonretired/player-disabled units. A global null/exception replacement is unsafe. |
| Gated metadata | RaceEditorPanel species loads/bulk loops are filtered by 03B1, but TaggedTraitEditor and shared helpers still use direct Get. Preserve tabs and saved raw mapping semantics. |
| Legacy campaign providers | World map-editor construction, Empire.GenerateLeader, Village and old recruitment/power flows need disconnection through spec 05. Do not supply a replacement species to keep these retired systems running. |
| Legacy save migrations | State old-version migration (including version <= 42) reads overrides for strategic and tactical units. Current old-save loading must be explicitly closed before missing definitions can be treated as impossible. Preserve personal files; do not attempt an implicit import into the new campaign. |

## Required next implementation: 05A1 application/save entry boundary

Bring this narrow part of spec 05 forward before prototype removal. Trace StartMode/GameManager load and campaign-create paths, load dialogs, hotkeys/quickload and direct State.Load entry, and any preview that deserializes old World data. Close unsupported empire/tactical legacy-save application routes with a clear message before live state mutation; keep files and retained skirmish setup/menu paths. Do not build new company saving yet, and do not remove tactical snapshot data required by later combat work. Define concrete scope from source before editing; keep scheduler retirement in a subsequent packet.

After that boundary and strategic creation providers are closed, revisit 03B3 stat read closure with source-based evidence. Distinguish a non-instantiating stored-record lookup from lazy creation if callers still require both; migrate actual callers rather than adding an unused helper. Only then retire prototype fields/initializers/cases in matched groups, preserving all 64 remaining definitions and enum IDs. Graphics registry/shared clothing needs a separate packet.

Two nonretired prototype owners retain rejected destinations: MatronsMinions.LeaderRace -> Abakhanskya and FeralUmbreon.ConversionRace -> FeralEevee. No target replacement or species approval is inferred. Resolve those links with the owning creation/conversion retirement, not generic fallback.

## Verification and limits

Source inspection only. No Unity build/import, Play-mode test, personal-data read, new tests/framework or source edits. The user accepted the preceding compile/scene/manual results; this review does not extend those results to real battles or old-save safety. Source files remain unchanged during this packet. Existing license/attribution and all source notices are preserved; no new source notices apply to untouched files. No files deleted or moved.
