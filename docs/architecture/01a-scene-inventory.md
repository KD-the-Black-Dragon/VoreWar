# Navigation and settings scene inventory

**Packet:** 01A; static source/serialization inspection, 2026-10-30. No scene edits or runtime input checks.

This records the original scene. [02C](../implementation/02c-settings-retirement-and-balance-locking.md) subsequently resolved unmatched settings bindings/inherited callbacks and changed 100 settings-related blocks; its control map records the current bindings.  [Packet 02B](../implementation/02b-tool-retirement-and-race-editor-gate.md) now changes twelve specific prefab-instance blocks; the recorded original callback states and line anchors below are historical, with stable instance IDs used for current change tracking.

## Scope and limits

Selected script components and their GameObjects were resolved using script GUIDs, scene object IDs, and source prefab references. Both direct persistent calls and prefab-instance method/target overrides were inspected. This is a targeted inventory, not a complete Unity prefab deserializer: inherited callback methods, inactive ancestors, arbitrary GameObject activation routes, and dynamically added listeners require source review. A serialized reference alone does not prove runtime reachability. Call state 0 means disabled; 2 means enabled in editor/runtime.

## Primary components

| Script | Scene component ID | Owner |
|---|---:|---|
| [GameManager](../../Assets/Scripts/Core/GameManager.cs) | 65019991 | GameManager |
| [UnitTagEditor](../../Assets/Scripts/Subwindows/UnitTagEditor.cs) | 135671443 | Game Menu/Unit Tags Menu |
| [CheatMenu](../../Assets/Scripts/Subwindows/CheatMenu.cs) | 430755319 | Game Menu/Cheat Screen |
| [RaceEditorPanel](../../Assets/Scripts/Subwindows/RaceEditorPanel.cs) | 451951670 | Game Menu/Race Settings |
| [StartMode](../../Assets/Scripts/Modes/StartMode.cs) | 1052564618 | Start Mode |
| [WorldSettings](../../Assets/Scripts/Subwindows/WorldSettings.cs) | 1122947798 | Game Menu/World Settings |
| [GameMenu](../../Assets/Scripts/Subwindows/GameMenu.cs) | 1219520519 | Game Menu |
| [Options](../../Assets/Scripts/Subwindows/Options.cs) | 1493388513 | Game Menu/Options Screen |
| [ContentSettings](../../Assets/Scripts/Subwindows/ContentSettings.cs) | 1620494162 | Game Menu/Content Settings |
| [BestiaryPanel](../../Assets/Scripts/UI/BestiaryPanel.cs) | 1966731973 | Game Menu/Bestiary Screen |

## Selected serialized routes

Scene links for overrides point to the start of their PrefabInstance block. Preserve disabled/stale callbacks as evidence; do not count them as active entry points. Runtime listeners are described separately in the study report.

| Scene source | Owner/label | Target | Component ID | Callback state |
|---|---|---|---:|---|
| [base.unity:108327](../../Assets/base.unity#L108327) | Recruit Mode/Army UI/Button Back | GameManager.SwitchToStrategyMode | 65019991 | direct; inspect source |
| [base.unity:325650](../../Assets/base.unity#L325650) | Recruit Mode/Army UI/Army Sections/Canvas/Army Buttons Row/Button Back | GameManager.SwitchToStrategyMode | 65019991 | direct; inspect source |
| [base.unity:8774](../../Assets/base.unity#L8774) | Game Menu/Race Settings/Trait Options/Options/Traits Column/Trait Search | RaceEditorPanel.ActivateTaggedTraitsPanel | 451951670 | 2 |
| [base.unity:27048](../../Assets/base.unity#L27048) | Start Mode/Start UI/Preset 2 | GameMenu.OpenRaceEditor | 1219520519 | 0 |
| [base.unity:36612](../../Assets/base.unity#L36612) | Game Menu/Menu UI/Exit Game | GameMenu.ExitGame | 1219520519 | 2 |
| [base.unity:36705](../../Assets/base.unity#L36705) | Game Menu/Content Settings/Save settings for this game and exit | ContentSettings.Exit | 1620494162 | 2 |
| [base.unity:42727](../../Assets/base.unity#L42727) | Game Menu/Menu UI/Options | GameMenu.OpenOptions | 1219520519 | 2 |
| [base.unity:47629](../../Assets/base.unity#L47629) | Game Menu/Menu UI/Main Menu | GameMenu.MainMenu | 1219520519 | 2 |
| [base.unity:53402](../../Assets/base.unity#L53402) | Game Menu/Race Settings/Cancel Changes | RaceEditorPanel.CloseWithoutSave | 451951670 | 2 |
| [base.unity:67910](../../Assets/base.unity#L67910) | Game Menu/Menu UI/Save / Load Game | GameMenu.OpenSaveLoadMenu | 1219520519 | 2 |
| [base.unity:68390](../../Assets/base.unity#L68390) | Start Mode/Start UI/Help | GameMenu.LoadHelp | 1219520519 | 2 |
| [base.unity:86540](../../Assets/base.unity#L86540) | Start Mode/Start UI/Race Editor | GameMenu.OpenRaceEditor | 1219520519 | 2 |
| [base.unity:99039](../../Assets/base.unity#L99039) | Game Menu/Bestiary Screen/Button Panel/Sky Sharks | BestiaryPanel.ShowShark | 1966731973 | 2 |
| [base.unity:101959](../../Assets/base.unity#L101959) | Start Mode/Start UI/Create Pure Tactical | StartMode.CreateTactical | 1052564618 | 2 |
| [base.unity:102933](../../Assets/base.unity#L102933) | Start Mode/Start UI/Modify Custom Traits | StartMode.ModifyCustomTraits | 1052564618 | 2 |
| [base.unity:106143](../../Assets/base.unity#L106143) | Start Mode/Start UI/Preset 1 | GameMenu.OpenRaceEditor | 1219520519 | 0 |
| [base.unity:110865](../../Assets/base.unity#L110865) | Strategic/Strategic UI/Battle Report Panel/Skip Battle - Show Stats | GameManager. | 65019991 | 2 |
| [base.unity:117350](../../Assets/base.unity#L117350) | Start Mode/Create Strategic UI/Side Panel/Back | StartMode.ReturnToStart | 1052564618 | 2 |
| [base.unity:118400](../../Assets/base.unity#L118400) | Game Menu/Bestiary Screen/Button Panel/Harvesters | BestiaryPanel.ShowHarvester | 1966731973 | 2 |
| [base.unity:139178](../../Assets/base.unity#L139178) | Game Menu/Options Screen/Clear All Saved Options/Content Settings | Options.AskClearAllSettings | 1493388513 | 2 |
| [base.unity:141189](../../Assets/base.unity#L141189) | Game Menu/Unit Tags Menu/Close | UnitTagEditor.Close | 135671443 | 2 |
| [base.unity:143293](../../Assets/base.unity#L143293) | Start Mode/Start UI/Map Editor | StartMode.MapEditor | 1052564618 | 2 |
| [base.unity:145425](../../Assets/base.unity#L145425) | Strategic/Strategic UI/Battle Report Panel/Fight Battle | GameManager.ActivateQueuedTacticalMode | 65019991 | 2 |
| [base.unity:146457](../../Assets/base.unity#L146457) | Game Menu/Variable Editor/Discard Changes | VariableEditor.Close | 1990661836 | 2 |
| [base.unity:146716](../../Assets/base.unity#L146716) | Game Menu/Menu UI/Close Menu | GameManager.CloseMenu | 65019991 | 2 |
| [base.unity:155093](../../Assets/base.unity#L155093) | Game Menu/Bestiary Screen/Button Panel/Abakhanskya | BestiaryPanel.ShowAbakhanskya | 1966731973 | 2 |
| [base.unity:156342](../../Assets/base.unity#L156342) | Game Menu/Menu UI/Empire Traits | GameMenu.OpenEmpireTraits | 1219520519 | 2 |
| [base.unity:158164](../../Assets/base.unity#L158164) | Game Menu/Options Screen/Reload Namelists | Options.UpdateNamelists | 1493388513 | 2 |
| [base.unity:162344](../../Assets/base.unity#L162344) | Start Mode/Start UI/Customize Random Traits | StartMode.ChangeRandomizableTraits | 1052564618 | 2 |
| [base.unity:176385](../../Assets/base.unity#L176385) | Game Menu/Menu UI/Open Map Editor | GameManager.SwitchToMapEditor | 65019991 | 2 |
| [base.unity:177766](../../Assets/base.unity#L177766) | Stat Screen/Canvas/Exit Stats Screen | GameManager.CloseStatsScreen | 65019991 | 2 |
| [base.unity:180156](../../Assets/base.unity#L180156) | Game Menu/Menu UI/Edit World Settings | GameMenu.ShowGameSettings | 1219520519 | 2 |
| [base.unity:188502](../../Assets/base.unity#L188502) | Game Menu/Content Settings/Save settings for this game and make settings the new default for created | ContentSettings.ExitAndSave | 1620494162 | 2 |
| [base.unity:190524](../../Assets/base.unity#L190524) | Game Menu/Bestiary Screen/Button Panel/Compy | BestiaryPanel.ShowCompy | 1966731973 | 2 |
| [base.unity:198649](../../Assets/base.unity#L198649) | Game Menu/Variable Editor/Save and Close | VariableEditor.SaveAndClose | 1990661836 | 2 |
| [base.unity:213761](../../Assets/base.unity#L213761) | Start Mode/Create Strategic UI/Side Panel/Content Settings | ContentSettings.Open | 1620494162 | 2 |
| [base.unity:214008](../../Assets/base.unity#L214008) | Game Menu/Bestiary Screen/Button Panel/Foreword | BestiaryPanel.ShowForeward | 1966731973 | 2 |
| [base.unity:222164](../../Assets/base.unity#L222164) | Game Menu/Menu UI/Help Screens | GameMenu.LoadHelp | 1219520519 | 2 |
| [base.unity:229771](../../Assets/base.unity#L229771) | Start Mode/Start UI/Exit Game | GameMenu.ExitGame | 1219520519 | 2 |
| [base.unity:230109](../../Assets/base.unity#L230109) | Game Menu/Bestiary Screen/Button Panel/Boom Bunnies | BestiaryPanel.ShowBoomBunny | 1966731973 | 2 |
| [base.unity:230844](../../Assets/base.unity#L230844) | Start Mode/Create Tactical UI/Content Settings | ContentSettings.Open | 1620494162 | 2 |
| [base.unity:241751](../../Assets/base.unity#L241751) | Game Menu/Options Screen/Reset Namelists | Options.ResetNamelists | 1493388513 | 2 |
| [base.unity:252860](../../Assets/base.unity#L252860) | Start Mode/Start UI/Create Strategic | StartMode.CreateStrategic | 1052564618 | 2 |
| [base.unity:254728](../../Assets/base.unity#L254728) | Game Menu/Race Settings/Trait Options | RaceEditorPanel.ActivateTraits | 451951670 | 2 |
| [base.unity:256289](../../Assets/base.unity#L256289) | Start Mode/Start UI/Warning! Untagged Traits! | GameMenu.OpenRaceEditor | 1219520519 | 2 |
| [base.unity:264310](../../Assets/base.unity#L264310) | Game Menu/Menu UI/Bestiary | Game Menu/Bestiary Screen.SetActive | 1966731968 | 2 |
| [base.unity:274921](../../Assets/base.unity#L274921) | Game Menu/Menu UI/Content Settings | ContentSettings.Open | 1620494162 | 2 |
| [base.unity:279947](../../Assets/base.unity#L279947) | Strategic/Strategic UI/Battle Report Panel/Watch Battle | GameManager.ActivateQueuedTacticalMode | 65019991 | 2 |
| [base.unity:282074](../../Assets/base.unity#L282074) | Start Mode/Start UI/Options Menu | GameMenu.OpenOptions | 1219520519 | 2 |
| [base.unity:291403](../../Assets/base.unity#L291403) | End Mode/End UI/Scroll View/Viewport/Return to Main Menu | GameManager.SwitchToMainMenu | 65019991 | 2 |
| [base.unity:291908](../../Assets/base.unity#L291908) | Game Menu/Race Settings/Unit Tags Options | RaceEditorPanel.ActivateTags | 451951670 | 2 |
| [base.unity:306099](../../Assets/base.unity#L306099) | Game Menu/Content Settings/Load saved settings for this game | ContentSettings.ConfirmRefresh | 1620494162 | 2 |
| [base.unity:309144](../../Assets/base.unity#L309144) | Game Menu/Race Settings/General Options | RaceEditorPanel.ActivateGeneral | 451951670 | 2 |
| [base.unity:315047](../../Assets/base.unity#L315047) | Game Menu/Menu UI/Cheat Menu | GameMenu.OpenCheats | 1219520519 | 2 |
| [base.unity:317990](../../Assets/base.unity#L317990) | Start Mode/Start UI/Gameplay Tutorial | StartMode.TutorialMode | 1052564618 | 2 |
| [base.unity:324731](../../Assets/base.unity#L324731) | Game Menu/Bestiary Screen/Button Panel/Aabayx | BestiaryPanel.ShowAabayx | 1966731973 | 2 |
| [base.unity:335468](../../Assets/base.unity#L335468) | Start Mode/Create Tactical UI/Back | StartMode.ReturnToStart | 1052564618 | 2 |
| [base.unity:350713](../../Assets/base.unity#L350713) | Game Menu/Bestiary Screen/Button Panel/Exit | Game Menu/Bestiary Screen.SetActive | 1966731968 | 2 |
| [base.unity:354787](../../Assets/base.unity#L354787) | Game Menu/Menu UI/Race Editor | GameMenu.OpenRaceEditor | 1219520519 | 2 |
| [base.unity:363512](../../Assets/base.unity#L363512) | Start Mode/Start UI/Load Saved Game | StartMode.LoadGame | 1052564618 | 2 |
| [base.unity:368008](../../Assets/base.unity#L368008) | Strategic/Strategic UI/Battle Report Panel/Skip Battle - No Stats | GameManager.ActivateQueuedTacticalMode | 65019991 | 2 |
| [base.unity:369151](../../Assets/base.unity#L369151) | Start Mode/Start UI/Change Assimilable Traits | StartMode.ChangeAssimilableTraits | 1052564618 | 2 |
| [base.unity:374643](../../Assets/base.unity#L374643) | Start Mode/Start UI/"Modify Conditional Traits\r" | StartMode.ModifyCondTraits | 1052564618 | 2 |
| [base.unity:380078](../../Assets/base.unity#L380078) | Game Menu/Race Settings/Tag Options/Unit Tags Editor | RaceEditorPanel.ModifyUnitTags | 451951670 | 2 |
| [base.unity:380226](../../Assets/base.unity#L380226) | Game Menu/Options Screen/Reset/Wipe Userdata | Options.ResetUserdata | 1493388513 | 2 |
| [base.unity:383475](../../Assets/base.unity#L383475) | Start Mode/Start UI/Preset 3 | GameMenu.OpenRaceEditor | 1219520519 | 0 |
| [base.unity:401474](../../Assets/base.unity#L401474) | Game Menu/Race Settings/Save Changes | RaceEditorPanel.CloseAndSave | 451951670 | 2 |
| [base.unity:412527](../../Assets/base.unity#L412527) | Game Menu/Options Screen/Back To Menu | Options.CloseAndSave | 1493388513 | 2 |

## Content page index mapping

ContentSettings.SettingsPanels[i] and PageSelector.options[i] must remain aligned.

| Index | Panel GameObject ID | Panel path |
|---:|---:|---|
| 0 | 688019743 | Game Menu/Content Settings/Strategic Gameplay |
| 1 | 1820698785 | Game Menu/Content Settings/Tactical Gameplay |
| 2 | 716650689 | Game Menu/Content Settings/Tactical Misc |
| 3 | 806433022 | Game Menu/Content Settings/Merc / Monster Spawning |
| 4 | 1277277714 | Game Menu/Content Settings/Gender |
| 5 | 426437537 | Game Menu/Content Settings/Appearance Settings |
| 6 | 402141452 | Game Menu/Content Settings/Fog of War settings |
| 7 | 1438320094 | Game Menu/Content Settings/Vore Main |
| 8 | 352977800 | Game Menu/Content Settings/Vore Advanced |
| 9 | 1522245816 | Game Menu/Content Settings/Vore Misc |
| 10 | 1391225976 | Game Menu/Content Settings/Post-Vore |
| 11 | 1859552280 | Game Menu/Content Settings/Building Options |
