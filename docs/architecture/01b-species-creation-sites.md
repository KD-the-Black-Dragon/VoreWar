# Unit creation and species-write candidates

**Packet:** 01B; 2026-10-30. Lexical inventory, not a complete call graph.

These sites include gameplay factories, previews, narration dummies, definitions, comments, and repeated writes. Serialization, clones, default enum values, reflection, and indirect mutations require separate inspection. Do not treat a constructor-only gate as sufficient. No source excerpts or narrative prose are reproduced.

## Explicit unit construction

| Source | Constructor |
|---|---|
| [Assets/Scripts/AI/GoblinAI.cs](../../Assets/Scripts/AI/GoblinAI.cs#L135) | NPC_unit |
| [Assets/Scripts/Entities/Empire.cs](../../Assets/Scripts/Entities/Empire.cs#L433) | Leader |
| [Assets/Scripts/Entities/MercenaryHouse.cs](../../Assets/Scripts/Entities/MercenaryHouse.cs#L107) | Unit |
| [Assets/Scripts/Entities/MercenaryHouse.cs](../../Assets/Scripts/Entities/MercenaryHouse.cs#L167) | Unit |
| [Assets/Scripts/Entities/Village.cs](../../Assets/Scripts/Entities/Village.cs#L1008) | Unit |
| [Assets/Scripts/Entities/Village.cs](../../Assets/Scripts/Entities/Village.cs#L1108) | Unit |
| [Assets/Scripts/Entities/Village.cs](../../Assets/Scripts/Entities/Village.cs#L1141) | Unit |
| [Assets/Scripts/Entities/Village.cs](../../Assets/Scripts/Entities/Village.cs#L1167) | Unit |
| [Assets/Scripts/Entities/Village.cs](../../Assets/Scripts/Entities/Village.cs#L1271) | Unit |
| [Assets/Scripts/Entities/Village.cs](../../Assets/Scripts/Entities/Village.cs#L1428) | Unit |
| [Assets/Scripts/Entities/Village.cs](../../Assets/Scripts/Entities/Village.cs#L1468) | Unit |
| [Assets/Scripts/Modes/Recruit_Mode.cs](../../Assets/Scripts/Modes/Recruit_Mode.cs#L674) | Unit |
| [Assets/Scripts/Modes/Recruit_Mode.cs](../../Assets/Scripts/Modes/Recruit_Mode.cs#L1908) | Unit |
| [Assets/Scripts/Modes/Recruit_Mode.cs](../../Assets/Scripts/Modes/Recruit_Mode.cs#L1917) | Unit |
| [Assets/Scripts/Modes/Recruit_Mode.cs](../../Assets/Scripts/Modes/Recruit_Mode.cs#L1968) | Unit |
| [Assets/Scripts/Modes/StrategyMode.cs](../../Assets/Scripts/Modes/StrategyMode.cs#L1506) | Unit |
| [Assets/Scripts/Modes/TacticalMode.cs](../../Assets/Scripts/Modes/TacticalMode.cs#L756) | NPC_unit |
| [Assets/Scripts/Modes/TacticalMode.cs](../../Assets/Scripts/Modes/TacticalMode.cs#L938) | NPC_unit |
| [Assets/Scripts/Modes/TacticalMode.cs](../../Assets/Scripts/Modes/TacticalMode.cs#L1075) | NPC_unit |
| [Assets/Scripts/Modes/TacticalMode.cs](../../Assets/Scripts/Modes/TacticalMode.cs#L1335) | NPC_unit |
| [Assets/Scripts/Modes/TacticalMode.cs](../../Assets/Scripts/Modes/TacticalMode.cs#L1354) | NPC_unit |
| [Assets/Scripts/Modes/TacticalMode.cs](../../Assets/Scripts/Modes/TacticalMode.cs#L1413) | NPC_unit |
| [Assets/Scripts/Modes/TacticalMode.cs](../../Assets/Scripts/Modes/TacticalMode.cs#L1436) | NPC_unit |
| [Assets/Scripts/Races/RaceParameters.cs](../../Assets/Scripts/Races/RaceParameters.cs#L173) | Unit |
| [Assets/Scripts/Subwindows/CreateStrategicGame.cs](../../Assets/Scripts/Subwindows/CreateStrategicGame.cs#L741) | Unit |
| [Assets/Scripts/Subwindows/CreateTacticalGame.cs](../../Assets/Scripts/Subwindows/CreateTacticalGame.cs#L66) | Unit |
| [Assets/Scripts/Subwindows/CreateTacticalGame.cs](../../Assets/Scripts/Subwindows/CreateTacticalGame.cs#L90) | Unit |
| [Assets/Scripts/Subwindows/CreateTacticalGame.cs](../../Assets/Scripts/Subwindows/CreateTacticalGame.cs#L464) | NPC_unit |
| [Assets/Scripts/Subwindows/CreateTacticalGame.cs](../../Assets/Scripts/Subwindows/CreateTacticalGame.cs#L471) | NPC_unit |
| [Assets/Scripts/Subwindows/CreateTacticalGame.cs](../../Assets/Scripts/Subwindows/CreateTacticalGame.cs#L480) | NPC_unit |
| [Assets/Scripts/Subwindows/CreateTacticalGame.cs](../../Assets/Scripts/Subwindows/CreateTacticalGame.cs#L488) | NPC_unit |
| [Assets/Scripts/Subwindows/CreateTacticalGame.cs](../../Assets/Scripts/Subwindows/CreateTacticalGame.cs#L499) | NPC_unit |
| [Assets/Scripts/Subwindows/CreateTacticalGame.cs](../../Assets/Scripts/Subwindows/CreateTacticalGame.cs#L507) | NPC_unit |
| [Assets/Scripts/Subwindows/DiplomacyScreen.cs](../../Assets/Scripts/Subwindows/DiplomacyScreen.cs#L32) | Unit |
| [Assets/Scripts/UI/CustomizationLoaderWindow.cs](../../Assets/Scripts/UI/CustomizationLoaderWindow.cs#L40) | Unit |
| [Assets/Scripts/UI/HoveringRacePicture.cs](../../Assets/Scripts/UI/HoveringRacePicture.cs#L31) | Unit |
| [Assets/Scripts/UI/UniformLoaderWindow.cs](../../Assets/Scripts/UI/UniformLoaderWindow.cs#L93) | Unit |
| [Assets/Scripts/Utility/EventList.cs](../../Assets/Scripts/Utility/EventList.cs#L122) | Unit |
| [Assets/Scripts/Utility/EventList.cs](../../Assets/Scripts/Utility/EventList.cs#L619) | Unit |
| [Assets/Scripts/Utility/EventList.cs](../../Assets/Scripts/Utility/EventList.cs#L771) | Unit |
| [Assets/Scripts/Utility/EventList.cs](../../Assets/Scripts/Utility/EventList.cs#L789) | Unit |
| [Assets/Scripts/Utility/EventList.cs](../../Assets/Scripts/Utility/EventList.cs#L809) | Unit |
| [Assets/Scripts/Utility/EventList.cs](../../Assets/Scripts/Utility/EventList.cs#L831) | Unit |
| [Assets/Scripts/Utility/EventList.cs](../../Assets/Scripts/Utility/EventList.cs#L864) | Unit |
| [Assets/Scripts/Utility/EventList.cs](../../Assets/Scripts/Utility/EventList.cs#L930) | Unit |
| [Assets/Scripts/Utility/EventList.cs](../../Assets/Scripts/Utility/EventList.cs#L952) | Unit |
| [Assets/Scripts/Utility/EventList.cs](../../Assets/Scripts/Utility/EventList.cs#L1034) | Unit |
| [Assets/Scripts/Utility/EventList.cs](../../Assets/Scripts/Utility/EventList.cs#L1157) | Unit |
| [Assets/Scripts/Utility/EventList.cs](../../Assets/Scripts/Utility/EventList.cs#L1268) | Unit |
| [Assets/Scripts/Utility/EventList.cs](../../Assets/Scripts/Utility/EventList.cs#L1300) | Unit |
| [Assets/Scripts/Utility/EventList.cs](../../Assets/Scripts/Utility/EventList.cs#L1321) | Unit |
| [Assets/Scripts/Utility/EventList.cs](../../Assets/Scripts/Utility/EventList.cs#L1553) | Unit |
| [Assets/Scripts/Utility/EventList.cs](../../Assets/Scripts/Utility/EventList.cs#L1608) | Unit |
| [Assets/Scripts/Utility/EventList.cs](../../Assets/Scripts/Utility/EventList.cs#L1651) | Unit |
| [Assets/Scripts/Utility/EventList.cs](../../Assets/Scripts/Utility/EventList.cs#L1653) | Unit |
| [Assets/Scripts/Utility/EventList.cs](../../Assets/Scripts/Utility/EventList.cs#L1678) | Unit |
| [Assets/Scripts/Utility/EventList.cs](../../Assets/Scripts/Utility/EventList.cs#L1680) | Unit |
| [Assets/Scripts/Utility/EventList.cs](../../Assets/Scripts/Utility/EventList.cs#L1701) | Unit |
| [Assets/Scripts/Utility/EventList.cs](../../Assets/Scripts/Utility/EventList.cs#L1702) | Unit |
| [Assets/Scripts/Utility/EventList.cs](../../Assets/Scripts/Utility/EventList.cs#L1737) | Unit |
| [Assets/Scripts/Utility/EventList.cs](../../Assets/Scripts/Utility/EventList.cs#L1785) | Unit |
| [Assets/Scripts/Utility/EventList.cs](../../Assets/Scripts/Utility/EventList.cs#L2107) | Unit |
| [Assets/Scripts/Utility/EventList.cs](../../Assets/Scripts/Utility/EventList.cs#L2422) | Unit |
| [Assets/Scripts/Utility/EventList.cs](../../Assets/Scripts/Utility/EventList.cs#L2495) | Unit |
| [Assets/Scripts/Modes/Tactical/TacticalMessageLog.cs](../../Assets/Scripts/Modes/Tactical/TacticalMessageLog.cs#L29) | Unit |
| [Assets/Scripts/Modes/Tactical/Log/LogUtilities.cs](../../Assets/Scripts/Modes/Tactical/Log/LogUtilities.cs#L195) | Unit |
| [Assets/Scripts/Entities/Spells/SpellList.cs](../../Assets/Scripts/Entities/Spells/SpellList.cs#L946) | Unit |
| [Assets/Scripts/Entities/Spells/SpellList.cs](../../Assets/Scripts/Entities/Spells/SpellList.cs#L977) | Unit |
| [Assets/Scripts/Entities/Spells/SpellList.cs](../../Assets/Scripts/Entities/Spells/SpellList.cs#L1021) | Unit |
| [Assets/Scripts/Entities/Units/PredatorComponent.cs](../../Assets/Scripts/Entities/Units/PredatorComponent.cs#L1469) | Spawn |
| [Assets/Scripts/Entities/Units/PredatorComponent.cs](../../Assets/Scripts/Entities/Units/PredatorComponent.cs#L2082) | Unit |
| [Assets/Scripts/Entities/Units/Unit.cs](../../Assets/Scripts/Entities/Units/Unit.cs#L3291) | Unit |
| [Assets/Scripts/AI/Legacy/legacyStrategicAI.cs](../../Assets/Scripts/AI/Legacy/legacyStrategicAI.cs#L360) | Unit |
| [Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs](../../Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs#L140) | Leader |
| [Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs](../../Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs#L143) | Unit |
| [Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs](../../Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs#L150) | Unit |
| [Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs](../../Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs#L159) | Unit |
| [Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs](../../Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs#L161) | Unit |
| [Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs](../../Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs#L169) | Unit |
| [Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs](../../Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs#L171) | Unit |
| [Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs](../../Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs#L176) | Leader |
| [Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs](../../Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs#L179) | Unit |
| [Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs](../../Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs#L197) | Unit |
| [Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs](../../Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs#L199) | Unit |
| [Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs](../../Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs#L201) | Unit |
| [Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs](../../Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs#L203) | Unit |
| [Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs](../../Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs#L210) | Unit |
| [Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs](../../Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs#L212) | Unit |
| [Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs](../../Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs#L220) | Unit |
| [Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs](../../Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs#L222) | Unit |
| [Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs](../../Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs#L227) | Leader |
| [Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs](../../Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs#L230) | Unit |
| [Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs](../../Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs#L238) | Unit |
| [Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs](../../Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs#L240) | Unit |
| [Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs](../../Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs#L253) | Unit |
| [Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs](../../Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs#L255) | Unit |
| [Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs](../../Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs#L257) | Unit |
| [Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs](../../Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs#L259) | Unit |
| [Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs](../../Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs#L265) | Leader |
| [Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs](../../Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs#L268) | Unit |
| [Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs](../../Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs#L275) | Unit |
| [Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs](../../Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs#L283) | Unit |
| [Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs](../../Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs#L355) | Unit |

## Race writes and change/hide entry points

| Source | Matched operation |
|---|---|
| [Assets/Scripts/Entities/Empire.cs](../../Assets/Scripts/Entities/Empire.cs#L185) | Race = |
| [Assets/Scripts/Entities/RacePop.cs](../../Assets/Scripts/Entities/RacePop.cs#L15) | Race = |
| [Assets/Scripts/Entities/RacePop.cs](../../Assets/Scripts/Entities/RacePop.cs#L22) | Race = |
| [Assets/Scripts/Entities/Village.cs](../../Assets/Scripts/Entities/Village.cs#L107) | Race = |
| [Assets/Scripts/Entities/Village.cs](../../Assets/Scripts/Entities/Village.cs#L458) | Race = |
| [Assets/Scripts/Entities/Village.cs](../../Assets/Scripts/Entities/Village.cs#L606) | Race = |
| [Assets/Scripts/Entities/Village.cs](../../Assets/Scripts/Entities/Village.cs#L612) | Race = |
| [Assets/Scripts/Modes/MapEditor.cs](../../Assets/Scripts/Modes/MapEditor.cs#L25) | Race = |
| [Assets/Scripts/Modes/Recruit_Mode.cs](../../Assets/Scripts/Modes/Recruit_Mode.cs#L1751) | village.Race = |
| [Assets/Scripts/Modes/Recruit_Mode.cs](../../Assets/Scripts/Modes/Recruit_Mode.cs#L1753) | village.Race = |
| [Assets/Scripts/Races/RaceParameters.cs](../../Assets/Scripts/Races/RaceParameters.cs#L174) | tempUnit.Race = |
| [Assets/Scripts/Races/Traits.cs](../../Assets/Scripts/Races/Traits.cs#L655) | ChangeRace( |
| [Assets/Scripts/Subwindows/CheatMenu.cs](../../Assets/Scripts/Subwindows/CheatMenu.cs#L150) | village.Race = |
| [Assets/Scripts/Subwindows/CheatMenu.cs](../../Assets/Scripts/Subwindows/CheatMenu.cs#L160) | Leader.Race = |
| [Assets/Scripts/Subwindows/CreateTacticalGame.cs](../../Assets/Scripts/Subwindows/CreateTacticalGame.cs#L79) | Unit.Race = |
| [Assets/Scripts/Subwindows/CreateTacticalGame.cs](../../Assets/Scripts/Subwindows/CreateTacticalGame.cs#L102) | Unit.Race = |
| [Assets/Scripts/UI/HoveringRacePicture.cs](../../Assets/Scripts/UI/HoveringRacePicture.cs#L34) | Unit.Race = |
| [Assets/Scripts/UI/UnitEditorPanel.cs](../../Assets/Scripts/UI/UnitEditorPanel.cs#L313) | ChangeRace( |
| [Assets/Scripts/UI/UnitEditorPanel.cs](../../Assets/Scripts/UI/UnitEditorPanel.cs#L321) | Unit.Race = |
| [Assets/Scripts/Utility/EventList.cs](../../Assets/Scripts/Utility/EventList.cs#L386) | Race = |
| [Assets/Scripts/Utility/Stored/CustomizerData.cs](../../Assets/Scripts/Utility/Stored/CustomizerData.cs#L109) | Race = |
| [Assets/Scripts/Utility/Stored/CustomizerData.cs](../../Assets/Scripts/Utility/Stored/CustomizerData.cs#L165) | Race = |
| [Assets/Scripts/Utility/Stored/UniformData.cs](../../Assets/Scripts/Utility/Stored/UniformData.cs#L49) | Race = |
| [Assets/Scripts/Modes/Strategic/MonsterSpawnerLocation.cs](../../Assets/Scripts/Modes/Strategic/MonsterSpawnerLocation.cs#L10) | Race = |
| [Assets/Scripts/Modes/Strategic/WorldGenerator.cs](../../Assets/Scripts/Modes/Strategic/WorldGenerator.cs#L323) | Race = |
| [Assets/Scripts/Entities/Units/Actor_Unit.cs](../../Assets/Scripts/Entities/Units/Actor_Unit.cs#L3974) | HideRace( |
| [Assets/Scripts/Entities/Units/Actor_Unit.cs](../../Assets/Scripts/Entities/Units/Actor_Unit.cs#L4032) | UnhideRace( |
| [Assets/Scripts/Entities/Units/PredatorComponent.cs](../../Assets/Scripts/Entities/Units/PredatorComponent.cs#L1743) | ChangeRace( |
| [Assets/Scripts/Entities/Units/PredatorComponent.cs](../../Assets/Scripts/Entities/Units/PredatorComponent.cs#L1960) | ChangeRace( |
| [Assets/Scripts/Entities/Units/Prey.cs](../../Assets/Scripts/Entities/Units/Prey.cs#L174) | ChangeRace( |
| [Assets/Scripts/Entities/Units/Prey.cs](../../Assets/Scripts/Entities/Units/Prey.cs#L185) | ChangeRace( |
| [Assets/Scripts/Entities/Units/Reincarnator.cs](../../Assets/Scripts/Entities/Units/Reincarnator.cs#L25) | Race = |
| [Assets/Scripts/Entities/Units/Unit.cs](../../Assets/Scripts/Entities/Units/Unit.cs#L711) | Race = |
| [Assets/Scripts/Entities/Units/Unit.cs](../../Assets/Scripts/Entities/Units/Unit.cs#L730) | Race = |
| [Assets/Scripts/Entities/Units/Unit.cs](../../Assets/Scripts/Entities/Units/Unit.cs#L2544) | ChangeRace( |
| [Assets/Scripts/Entities/Units/Unit.cs](../../Assets/Scripts/Entities/Units/Unit.cs#L2546) | Race = |
| [Assets/Scripts/Entities/Units/Unit.cs](../../Assets/Scripts/Entities/Units/Unit.cs#L2550) | HideRace( |
| [Assets/Scripts/Entities/Units/Unit.cs](../../Assets/Scripts/Entities/Units/Unit.cs#L2553) | Race = |
| [Assets/Scripts/Entities/Units/Unit.cs](../../Assets/Scripts/Entities/Units/Unit.cs#L2565) | UnhideRace( |
| [Assets/Scripts/Entities/Units/Unit.cs](../../Assets/Scripts/Entities/Units/Unit.cs#L2567) | Race = |
| [Assets/Scripts/Entities/Units/Unit.cs](../../Assets/Scripts/Entities/Units/Unit.cs#L3579) | Race = |
| [Assets/Scripts/Entities/Units/Unit.cs](../../Assets/Scripts/Entities/Units/Unit.cs#L3588) | Race = |
| [Assets/Scripts/Entities/Units/Unit.cs](../../Assets/Scripts/Entities/Units/Unit.cs#L3609) | Race = |
| [Assets/Scripts/Entities/Units/Unit.cs](../../Assets/Scripts/Entities/Units/Unit.cs#L3664) | Race = |
| [Assets/Scripts/Entities/Units/Unit.cs](../../Assets/Scripts/Entities/Units/Unit.cs#L3965) | Race = |
