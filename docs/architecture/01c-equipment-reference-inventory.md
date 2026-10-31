# Equipment migration reference candidates

**Packet:** 01C; 2026-10-30. Static lexical inventory; no implementation or runtime tests.

Matches identify locations to inspect, not automatically defective code. Comments and legacy paths may be included. A dynamic Items loop can still fail if ItemUses/ItemCooldowns remain short; serialized/UI references and item identity also need contextual review. The slot-sizing and accessory-mutation behavior is explained in the main report. No item descriptions or flavor prose are copied here.

| Source | Reference category |
|---|---|
| [Assets/Scripts/AI/GoblinAI.cs:139](../../Assets/Scripts/AI/GoblinAI.cs#L139) | fixed slot/index, item mutation |
| [Assets/Scripts/AI/GoblinAI.cs:141](../../Assets/Scripts/AI/GoblinAI.cs#L141) | fixed slot/index, item mutation |
| [Assets/Scripts/AI/GoblinAI.cs:143](../../Assets/Scripts/AI/GoblinAI.cs#L143) | fixed slot/index, item mutation |
| [Assets/Scripts/AI/GoblinAI.cs:145](../../Assets/Scripts/AI/GoblinAI.cs#L145) | fixed slot/index, item mutation |
| [Assets/Scripts/AI/StrategicArmyCommander.cs:659](../../Assets/Scripts/AI/StrategicArmyCommander.cs#L659) | fixed slot/index |
| [Assets/Scripts/AI/StrategicArmyCommander.cs:760](../../Assets/Scripts/AI/StrategicArmyCommander.cs#L760) | potion paths |
| [Assets/Scripts/AI/TacticalAI.cs:1708](../../Assets/Scripts/AI/TacticalAI.cs#L1708) | potion paths |
| [Assets/Scripts/AI/TacticalAI.cs:1711](../../Assets/Scripts/AI/TacticalAI.cs#L1711) | potion paths |
| [Assets/Scripts/Core/Config.cs:48](../../Assets/Scripts/Core/Config.cs#L48) | slot sizing |
| [Assets/Scripts/Core/Config.cs:412](../../Assets/Scripts/Core/Config.cs#L412) | potion paths |
| [Assets/Scripts/Core/State.cs:888](../../Assets/Scripts/Core/State.cs#L888) | fixed slot/index |
| [Assets/Scripts/Core/WorldConfig.cs:516](../../Assets/Scripts/Core/WorldConfig.cs#L516) | potion paths |
| [Assets/Scripts/Entities/Army.cs:202](../../Assets/Scripts/Entities/Army.cs#L202) | equipment lifecycle |
| [Assets/Scripts/Entities/Army.cs:203](../../Assets/Scripts/Entities/Army.cs#L203) | equipment lifecycle |
| [Assets/Scripts/Entities/Army.cs:878](../../Assets/Scripts/Entities/Army.cs#L878) | fixed slot/index |
| [Assets/Scripts/Entities/Army.cs:882](../../Assets/Scripts/Entities/Army.cs#L882) | fixed slot/index, item mutation |
| [Assets/Scripts/Entities/Army.cs:886](../../Assets/Scripts/Entities/Army.cs#L886) | fixed slot/index |
| [Assets/Scripts/Entities/Army.cs:890](../../Assets/Scripts/Entities/Army.cs#L890) | fixed slot/index |
| [Assets/Scripts/Entities/Army.cs:891](../../Assets/Scripts/Entities/Army.cs#L891) | fixed slot/index, item mutation |
| [Assets/Scripts/Entities/Army.cs:897](../../Assets/Scripts/Entities/Army.cs#L897) | fixed slot/index |
| [Assets/Scripts/Entities/Army.cs:904](../../Assets/Scripts/Entities/Army.cs#L904) | fixed slot/index, item mutation |
| [Assets/Scripts/Entities/Empire.cs:396](../../Assets/Scripts/Entities/Empire.cs#L396) | fixed slot/index |
| [Assets/Scripts/Entities/Empire.cs:403](../../Assets/Scripts/Entities/Empire.cs#L403) | fixed slot/index |
| [Assets/Scripts/Entities/Empire.cs:413](../../Assets/Scripts/Entities/Empire.cs#L413) | fixed slot/index |
| [Assets/Scripts/Entities/Empire.cs:415](../../Assets/Scripts/Entities/Empire.cs#L415) | fixed slot/index |
| [Assets/Scripts/Entities/MercenaryHouse.cs:110](../../Assets/Scripts/Entities/MercenaryHouse.cs#L110) | fixed slot/index |
| [Assets/Scripts/Entities/MercenaryHouse.cs:113](../../Assets/Scripts/Entities/MercenaryHouse.cs#L113) | fixed slot/index |
| [Assets/Scripts/Entities/MercenaryHouse.cs:115](../../Assets/Scripts/Entities/MercenaryHouse.cs#L115) | fixed slot/index |
| [Assets/Scripts/Entities/MercenaryHouse.cs:122](../../Assets/Scripts/Entities/MercenaryHouse.cs#L122) | fixed slot/index, item mutation |
| [Assets/Scripts/Entities/MercenaryHouse.cs:126](../../Assets/Scripts/Entities/MercenaryHouse.cs#L126) | fixed slot/index, item mutation |
| [Assets/Scripts/Entities/MercenaryHouse.cs:130](../../Assets/Scripts/Entities/MercenaryHouse.cs#L130) | fixed slot/index, item mutation |
| [Assets/Scripts/Entities/MercenaryHouse.cs:135](../../Assets/Scripts/Entities/MercenaryHouse.cs#L135) | fixed slot/index, item mutation |
| [Assets/Scripts/Entities/MercenaryHouse.cs:137](../../Assets/Scripts/Entities/MercenaryHouse.cs#L137) | fixed slot/index, item mutation |
| [Assets/Scripts/Entities/MercenaryHouse.cs:145](../../Assets/Scripts/Entities/MercenaryHouse.cs#L145) | fixed slot/index, item mutation |
| [Assets/Scripts/Entities/Village.cs:980](../../Assets/Scripts/Entities/Village.cs#L980) | fixed slot/index |
| [Assets/Scripts/Entities/Village.cs:1052](../../Assets/Scripts/Entities/Village.cs#L1052) | fixed slot/index |
| [Assets/Scripts/Entities/Village.cs:1054](../../Assets/Scripts/Entities/Village.cs#L1054) | fixed slot/index |
| [Assets/Scripts/Entities/Village.cs:1057](../../Assets/Scripts/Entities/Village.cs#L1057) | fixed slot/index |
| [Assets/Scripts/Entities/Village.cs:1059](../../Assets/Scripts/Entities/Village.cs#L1059) | fixed slot/index |
| [Assets/Scripts/Entities/Village.cs:1061](../../Assets/Scripts/Entities/Village.cs#L1061) | fixed slot/index |
| [Assets/Scripts/Entities/Village.cs:1122](../../Assets/Scripts/Entities/Village.cs#L1122) | fixed slot/index |
| [Assets/Scripts/Entities/Village.cs:1431](../../Assets/Scripts/Entities/Village.cs#L1431) | fixed slot/index |
| [Assets/Scripts/Entities/Village.cs:1434](../../Assets/Scripts/Entities/Village.cs#L1434) | fixed slot/index |
| [Assets/Scripts/Entities/Village.cs:1436](../../Assets/Scripts/Entities/Village.cs#L1436) | fixed slot/index |
| [Assets/Scripts/Entities/Village.cs:1443](../../Assets/Scripts/Entities/Village.cs#L1443) | fixed slot/index, item mutation |
| [Assets/Scripts/Entities/Village.cs:1471](../../Assets/Scripts/Entities/Village.cs#L1471) | fixed slot/index |
| [Assets/Scripts/Entities/Village.cs:1474](../../Assets/Scripts/Entities/Village.cs#L1474) | fixed slot/index |
| [Assets/Scripts/Entities/Village.cs:1476](../../Assets/Scripts/Entities/Village.cs#L1476) | fixed slot/index |
| [Assets/Scripts/Entities/Village.cs:1483](../../Assets/Scripts/Entities/Village.cs#L1483) | fixed slot/index, item mutation |
| [Assets/Scripts/Entities/Village.cs:1487](../../Assets/Scripts/Entities/Village.cs#L1487) | fixed slot/index, item mutation |
| [Assets/Scripts/Entities/Village.cs:1491](../../Assets/Scripts/Entities/Village.cs#L1491) | fixed slot/index, item mutation |
| [Assets/Scripts/Entities/Village.cs:1496](../../Assets/Scripts/Entities/Village.cs#L1496) | fixed slot/index, item mutation |
| [Assets/Scripts/Entities/Village.cs:1498](../../Assets/Scripts/Entities/Village.cs#L1498) | fixed slot/index, item mutation |
| [Assets/Scripts/Entities/Village.cs:1508](../../Assets/Scripts/Entities/Village.cs#L1508) | fixed slot/index, item mutation |
| [Assets/Scripts/Entities/Village.cs:1516](../../Assets/Scripts/Entities/Village.cs#L1516) | fixed slot/index, item mutation |
| [Assets/Scripts/Modes/Recruit_Mode.cs:231](../../Assets/Scripts/Modes/Recruit_Mode.cs#L231) | potion paths |
| [Assets/Scripts/Modes/Recruit_Mode.cs:949](../../Assets/Scripts/Modes/Recruit_Mode.cs#L949) | fixed slot/index |
| [Assets/Scripts/Modes/Recruit_Mode.cs:969](../../Assets/Scripts/Modes/Recruit_Mode.cs#L969) | fixed slot/index |
| [Assets/Scripts/Modes/Recruit_Mode.cs:993](../../Assets/Scripts/Modes/Recruit_Mode.cs#L993) | fixed slot/index |
| [Assets/Scripts/Modes/Recruit_Mode.cs:1027](../../Assets/Scripts/Modes/Recruit_Mode.cs#L1027) | fixed slot/index |
| [Assets/Scripts/Modes/Recruit_Mode.cs:1129](../../Assets/Scripts/Modes/Recruit_Mode.cs#L1129) | item mutation |
| [Assets/Scripts/Modes/TacticalMode.cs:536](../../Assets/Scripts/Modes/TacticalMode.cs#L536) | equipment lifecycle |
| [Assets/Scripts/Modes/TacticalMode.cs:537](../../Assets/Scripts/Modes/TacticalMode.cs#L537) | equipment lifecycle |
| [Assets/Scripts/Modes/TacticalMode.cs:946](../../Assets/Scripts/Modes/TacticalMode.cs#L946) | fixed slot/index |
| [Assets/Scripts/Modes/TacticalMode.cs:949](../../Assets/Scripts/Modes/TacticalMode.cs#L949) | fixed slot/index |
| [Assets/Scripts/Modes/TacticalMode.cs:952](../../Assets/Scripts/Modes/TacticalMode.cs#L952) | fixed slot/index |
| [Assets/Scripts/Modes/TacticalMode.cs:955](../../Assets/Scripts/Modes/TacticalMode.cs#L955) | fixed slot/index |
| [Assets/Scripts/Modes/TacticalMode.cs:960](../../Assets/Scripts/Modes/TacticalMode.cs#L960) | fixed slot/index |
| [Assets/Scripts/Modes/TacticalMode.cs:964](../../Assets/Scripts/Modes/TacticalMode.cs#L964) | fixed slot/index |
| [Assets/Scripts/Modes/TacticalMode.cs:4819](../../Assets/Scripts/Modes/TacticalMode.cs#L4819) | equipment lifecycle |
| [Assets/Scripts/Modes/TacticalMode.cs:4822](../../Assets/Scripts/Modes/TacticalMode.cs#L4822) | potion paths |
| [Assets/Scripts/Modes/TacticalMode.cs:4828](../../Assets/Scripts/Modes/TacticalMode.cs#L4828) | potion paths |
| [Assets/Scripts/Modes/TacticalMode.cs:4831](../../Assets/Scripts/Modes/TacticalMode.cs#L4831) | potion paths |
| [Assets/Scripts/Subwindows/ContentSettings.cs:257](../../Assets/Scripts/Subwindows/ContentSettings.cs#L257) | potion paths |
| [Assets/Scripts/Subwindows/ContentSettings.cs:419](../../Assets/Scripts/Subwindows/ContentSettings.cs#L419) | potion paths |
| [Assets/Scripts/Subwindows/CreateStrategicGame.cs:601](../../Assets/Scripts/Subwindows/CreateStrategicGame.cs#L601) | slot sizing |
| [Assets/Scripts/Subwindows/CreateTacticalGame.cs:416](../../Assets/Scripts/Subwindows/CreateTacticalGame.cs#L416) | slot sizing |
| [Assets/Scripts/Subwindows/CreateTacticalGame.cs:467](../../Assets/Scripts/Subwindows/CreateTacticalGame.cs#L467) | fixed slot/index, item mutation |
| [Assets/Scripts/Subwindows/CreateTacticalGame.cs:474](../../Assets/Scripts/Subwindows/CreateTacticalGame.cs#L474) | fixed slot/index, item mutation |
| [Assets/Scripts/Subwindows/CreateTacticalGame.cs:484](../../Assets/Scripts/Subwindows/CreateTacticalGame.cs#L484) | fixed slot/index, item mutation |
| [Assets/Scripts/Subwindows/CreateTacticalGame.cs:492](../../Assets/Scripts/Subwindows/CreateTacticalGame.cs#L492) | fixed slot/index, item mutation |
| [Assets/Scripts/Subwindows/CreateTacticalGame.cs:503](../../Assets/Scripts/Subwindows/CreateTacticalGame.cs#L503) | fixed slot/index, item mutation |
| [Assets/Scripts/Subwindows/CreateTacticalGame.cs:511](../../Assets/Scripts/Subwindows/CreateTacticalGame.cs#L511) | fixed slot/index, item mutation |
| [Assets/Scripts/Subwindows/InfoPanel.cs:883](../../Assets/Scripts/Subwindows/InfoPanel.cs#L883) | potion paths |
| [Assets/Scripts/Subwindows/InfoPanel.cs:885](../../Assets/Scripts/Subwindows/InfoPanel.cs#L885) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:25](../../Assets/Scripts/Subwindows/PotionShop.cs#L25) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:27](../../Assets/Scripts/Subwindows/PotionShop.cs#L27) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:30](../../Assets/Scripts/Subwindows/PotionShop.cs#L30) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:32](../../Assets/Scripts/Subwindows/PotionShop.cs#L32) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:33](../../Assets/Scripts/Subwindows/PotionShop.cs#L33) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:34](../../Assets/Scripts/Subwindows/PotionShop.cs#L34) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:35](../../Assets/Scripts/Subwindows/PotionShop.cs#L35) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:36](../../Assets/Scripts/Subwindows/PotionShop.cs#L36) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:63](../../Assets/Scripts/Subwindows/PotionShop.cs#L63) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:65](../../Assets/Scripts/Subwindows/PotionShop.cs#L65) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:66](../../Assets/Scripts/Subwindows/PotionShop.cs#L66) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:67](../../Assets/Scripts/Subwindows/PotionShop.cs#L67) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:73](../../Assets/Scripts/Subwindows/PotionShop.cs#L73) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:79](../../Assets/Scripts/Subwindows/PotionShop.cs#L79) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:94](../../Assets/Scripts/Subwindows/PotionShop.cs#L94) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:100](../../Assets/Scripts/Subwindows/PotionShop.cs#L100) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:106](../../Assets/Scripts/Subwindows/PotionShop.cs#L106) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:122](../../Assets/Scripts/Subwindows/PotionShop.cs#L122) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:128](../../Assets/Scripts/Subwindows/PotionShop.cs#L128) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:130](../../Assets/Scripts/Subwindows/PotionShop.cs#L130) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:131](../../Assets/Scripts/Subwindows/PotionShop.cs#L131) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:132](../../Assets/Scripts/Subwindows/PotionShop.cs#L132) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:134](../../Assets/Scripts/Subwindows/PotionShop.cs#L134) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:140](../../Assets/Scripts/Subwindows/PotionShop.cs#L140) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:163](../../Assets/Scripts/Subwindows/PotionShop.cs#L163) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:164](../../Assets/Scripts/Subwindows/PotionShop.cs#L164) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:165](../../Assets/Scripts/Subwindows/PotionShop.cs#L165) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:216](../../Assets/Scripts/Subwindows/PotionShop.cs#L216) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:217](../../Assets/Scripts/Subwindows/PotionShop.cs#L217) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:218](../../Assets/Scripts/Subwindows/PotionShop.cs#L218) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:220](../../Assets/Scripts/Subwindows/PotionShop.cs#L220) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:229](../../Assets/Scripts/Subwindows/PotionShop.cs#L229) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:232](../../Assets/Scripts/Subwindows/PotionShop.cs#L232) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:235](../../Assets/Scripts/Subwindows/PotionShop.cs#L235) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:271](../../Assets/Scripts/Subwindows/PotionShop.cs#L271) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:289](../../Assets/Scripts/Subwindows/PotionShop.cs#L289) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:291](../../Assets/Scripts/Subwindows/PotionShop.cs#L291) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:293](../../Assets/Scripts/Subwindows/PotionShop.cs#L293) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:297](../../Assets/Scripts/Subwindows/PotionShop.cs#L297) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:299](../../Assets/Scripts/Subwindows/PotionShop.cs#L299) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:301](../../Assets/Scripts/Subwindows/PotionShop.cs#L301) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:303](../../Assets/Scripts/Subwindows/PotionShop.cs#L303) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:305](../../Assets/Scripts/Subwindows/PotionShop.cs#L305) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:307](../../Assets/Scripts/Subwindows/PotionShop.cs#L307) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:311](../../Assets/Scripts/Subwindows/PotionShop.cs#L311) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:313](../../Assets/Scripts/Subwindows/PotionShop.cs#L313) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:315](../../Assets/Scripts/Subwindows/PotionShop.cs#L315) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:316](../../Assets/Scripts/Subwindows/PotionShop.cs#L316) | potion paths |
| [Assets/Scripts/Subwindows/PotionShop.cs:317](../../Assets/Scripts/Subwindows/PotionShop.cs#L317) | potion paths |
| [Assets/Scripts/Subwindows/Shop.cs:63](../../Assets/Scripts/Subwindows/Shop.cs#L63) | item mutation |
| [Assets/Scripts/Subwindows/Shop.cs:82](../../Assets/Scripts/Subwindows/Shop.cs#L82) | item mutation |
| [Assets/Scripts/Subwindows/Shop.cs:107](../../Assets/Scripts/Subwindows/Shop.cs#L107) | item mutation |
| [Assets/Scripts/Subwindows/Shop.cs:138](../../Assets/Scripts/Subwindows/Shop.cs#L138) | item mutation |
| [Assets/Scripts/Subwindows/UnitEditor.cs:404](../../Assets/Scripts/Subwindows/UnitEditor.cs#L404) | item mutation |
| [Assets/Scripts/Subwindows/WorldSettings.cs:188](../../Assets/Scripts/Subwindows/WorldSettings.cs#L188) | slot sizing |
| [Assets/Scripts/UI/UnitEditorPanel.cs:454](../../Assets/Scripts/UI/UnitEditorPanel.cs#L454) | slot sizing |
| [Assets/Scripts/UI/UnitEditorPanel.cs:481](../../Assets/Scripts/UI/UnitEditorPanel.cs#L481) | slot sizing |
| [Assets/Scripts/UI/UnitEditorPanel.cs:517](../../Assets/Scripts/UI/UnitEditorPanel.cs#L517) | slot sizing |
| [Assets/Scripts/UI/UnitEditorPanel.cs:555](../../Assets/Scripts/UI/UnitEditorPanel.cs#L555) | slot sizing |
| [Assets/Scripts/UI/UnitEditorPanel.cs:578](../../Assets/Scripts/UI/UnitEditorPanel.cs#L578) | slot sizing |
| [Assets/Scripts/UI/UnitEditorPanel.cs:606](../../Assets/Scripts/UI/UnitEditorPanel.cs#L606) | slot sizing |
| [Assets/Scripts/Utility/EventList.cs:1652](../../Assets/Scripts/Utility/EventList.cs#L1652) | fixed slot/index |
| [Assets/Scripts/Utility/EventList.cs:1679](../../Assets/Scripts/Utility/EventList.cs#L1679) | fixed slot/index |
| [Assets/Scripts/Utility/EventList.cs:1811](../../Assets/Scripts/Utility/EventList.cs#L1811) | fixed slot/index |
| [Assets/Scripts/Utility/EventList.cs:1818](../../Assets/Scripts/Utility/EventList.cs#L1818) | fixed slot/index |
| [Assets/Scripts/Utility/EventList.cs:2424](../../Assets/Scripts/Utility/EventList.cs#L2424) | fixed slot/index |
| [Assets/Scripts/Utility/EventList.cs:2426](../../Assets/Scripts/Utility/EventList.cs#L2426) | fixed slot/index |
| [Assets/Scripts/Utility/EventList.cs:2497](../../Assets/Scripts/Utility/EventList.cs#L2497) | fixed slot/index |
| [Assets/Scripts/Utility/EventList.cs:2499](../../Assets/Scripts/Utility/EventList.cs#L2499) | fixed slot/index |
| [Assets/Scripts/Utility/Texts/HoveringTooltip.cs:89](../../Assets/Scripts/Utility/Texts/HoveringTooltip.cs#L89) | potion paths |
| [Assets/Scripts/UI/Connectors/AdvancedUnitCommands.cs:60](../../Assets/Scripts/UI/Connectors/AdvancedUnitCommands.cs#L60) | potion paths |
| [Assets/Scripts/UI/Connectors/ItemPotionPanel.cs:9](../../Assets/Scripts/UI/Connectors/ItemPotionPanel.cs#L9) | potion paths |
| [Assets/Scripts/UI/Connectors/RecruitCheatsPanel.cs:101](../../Assets/Scripts/UI/Connectors/RecruitCheatsPanel.cs#L101) | slot sizing |
| [Assets/Scripts/UI/Connectors/RecruitCheatsPanel.cs:123](../../Assets/Scripts/UI/Connectors/RecruitCheatsPanel.cs#L123) | slot sizing |
| [Assets/Scripts/UI/Connectors/RecruitCheatsPanel.cs:140](../../Assets/Scripts/UI/Connectors/RecruitCheatsPanel.cs#L140) | slot sizing |
| [Assets/Scripts/Races/Graphics/MainRaces/Crux.cs:131](../../Assets/Scripts/Races/Graphics/MainRaces/Crux.cs#L131) | fixed slot/index |
| [Assets/Scripts/Races/Graphics/MainRaces/Crux.cs:132](../../Assets/Scripts/Races/Graphics/MainRaces/Crux.cs#L132) | fixed slot/index |
| [Assets/Scripts/Races/Graphics/MainRaces/Crux.cs:213](../../Assets/Scripts/Races/Graphics/MainRaces/Crux.cs#L213) | fixed slot/index |
| [Assets/Scripts/Races/Graphics/MainRaces/Crux.cs:214](../../Assets/Scripts/Races/Graphics/MainRaces/Crux.cs#L214) | fixed slot/index |
| [Assets/Scripts/Races/Graphics/MainRaces/Kangaroos.cs:247](../../Assets/Scripts/Races/Graphics/MainRaces/Kangaroos.cs#L247) | fixed slot/index |
| [Assets/Scripts/Races/Graphics/MainRaces/Kangaroos.cs:248](../../Assets/Scripts/Races/Graphics/MainRaces/Kangaroos.cs#L248) | fixed slot/index |
| [Assets/Scripts/Races/Graphics/MainRaces/Kangaroos.cs:257](../../Assets/Scripts/Races/Graphics/MainRaces/Kangaroos.cs#L257) | fixed slot/index |
| [Assets/Scripts/Races/Graphics/MainRaces/Kangaroos.cs:258](../../Assets/Scripts/Races/Graphics/MainRaces/Kangaroos.cs#L258) | fixed slot/index |
| [Assets/Scripts/Races/Graphics/MainRaces/Kangaroos.cs:267](../../Assets/Scripts/Races/Graphics/MainRaces/Kangaroos.cs#L267) | fixed slot/index |
| [Assets/Scripts/Races/Graphics/MainRaces/Kangaroos.cs:268](../../Assets/Scripts/Races/Graphics/MainRaces/Kangaroos.cs#L268) | fixed slot/index |
| [Assets/Scripts/Races/Graphics/MainRaces/Umbreon.cs:358](../../Assets/Scripts/Races/Graphics/MainRaces/Umbreon.cs#L358) | fixed slot/index |
| [Assets/Scripts/Races/Graphics/MainRaces/Umbreon.cs:360](../../Assets/Scripts/Races/Graphics/MainRaces/Umbreon.cs#L360) | fixed slot/index |
| [Assets/Scripts/Races/Graphics/MainRaces/Umbreon.cs:373](../../Assets/Scripts/Races/Graphics/MainRaces/Umbreon.cs#L373) | fixed slot/index |
| [Assets/Scripts/Races/Graphics/MainRaces/Umbreon.cs:375](../../Assets/Scripts/Races/Graphics/MainRaces/Umbreon.cs#L375) | fixed slot/index |
| [Assets/Scripts/Races/Graphics/MainRaces/Umbreon.cs:389](../../Assets/Scripts/Races/Graphics/MainRaces/Umbreon.cs#L389) | fixed slot/index |
| [Assets/Scripts/Races/Graphics/MainRaces/Umbreon.cs:391](../../Assets/Scripts/Races/Graphics/MainRaces/Umbreon.cs#L391) | fixed slot/index |
| [Assets/Scripts/Races/Graphics/Mercs/Alligators.cs:158](../../Assets/Scripts/Races/Graphics/Mercs/Alligators.cs#L158) | fixed slot/index |
| [Assets/Scripts/Races/Graphics/Mercs/Alligators.cs:159](../../Assets/Scripts/Races/Graphics/Mercs/Alligators.cs#L159) | fixed slot/index |
| [Assets/Scripts/Races/Graphics/Mercs/Alligators.cs:168](../../Assets/Scripts/Races/Graphics/Mercs/Alligators.cs#L168) | fixed slot/index |
| [Assets/Scripts/Races/Graphics/Mercs/Alligators.cs:169](../../Assets/Scripts/Races/Graphics/Mercs/Alligators.cs#L169) | fixed slot/index |
| [Assets/Scripts/Races/Graphics/Mercs/Alligators.cs:178](../../Assets/Scripts/Races/Graphics/Mercs/Alligators.cs#L178) | fixed slot/index |
| [Assets/Scripts/Races/Graphics/Mercs/Alligators.cs:179](../../Assets/Scripts/Races/Graphics/Mercs/Alligators.cs#L179) | fixed slot/index |
| [Assets/Scripts/Modes/Strategic/StrategicUtilities.cs:545](../../Assets/Scripts/Modes/Strategic/StrategicUtilities.cs#L545) | fixed slot/index |
| [Assets/Scripts/Modes/Strategic/StrategicUtilities.cs:577](../../Assets/Scripts/Modes/Strategic/StrategicUtilities.cs#L577) | fixed slot/index |
| [Assets/Scripts/Modes/Strategic/StrategicUtilities.cs:646](../../Assets/Scripts/Modes/Strategic/StrategicUtilities.cs#L646) | fixed slot/index |
| [Assets/Scripts/Modes/Strategic/StrategicUtilities.cs:655](../../Assets/Scripts/Modes/Strategic/StrategicUtilities.cs#L655) | fixed slot/index |
| [Assets/Scripts/Entities/Items/Equipment.cs:38](../../Assets/Scripts/Entities/Items/Equipment.cs#L38) | uses/cooldown storage |
| [Assets/Scripts/Entities/Items/Equipment.cs:59](../../Assets/Scripts/Entities/Items/Equipment.cs#L59) | uses/cooldown storage |
| [Assets/Scripts/Entities/Items/Equipment.cs:72](../../Assets/Scripts/Entities/Items/Equipment.cs#L72) | uses/cooldown storage |
| [Assets/Scripts/Entities/Items/Equipment.cs:79](../../Assets/Scripts/Entities/Items/Equipment.cs#L79) | uses/cooldown storage |
| [Assets/Scripts/Entities/Items/Equipment.cs:90](../../Assets/Scripts/Entities/Items/Equipment.cs#L90) | uses/cooldown storage |
| [Assets/Scripts/Entities/Items/Equipment.cs:93](../../Assets/Scripts/Entities/Items/Equipment.cs#L93) | uses/cooldown storage |
| [Assets/Scripts/Entities/Items/Equipment.cs:95](../../Assets/Scripts/Entities/Items/Equipment.cs#L95) | uses/cooldown storage |
| [Assets/Scripts/Entities/Items/Equipment.cs:100](../../Assets/Scripts/Entities/Items/Equipment.cs#L100) | uses/cooldown storage |
| [Assets/Scripts/Entities/Items/Equipment.cs:102](../../Assets/Scripts/Entities/Items/Equipment.cs#L102) | uses/cooldown storage |
| [Assets/Scripts/Entities/Items/Equipment.cs:107](../../Assets/Scripts/Entities/Items/Equipment.cs#L107) | uses/cooldown storage |
| [Assets/Scripts/Entities/Items/Equipment.cs:109](../../Assets/Scripts/Entities/Items/Equipment.cs#L109) | item mutation |
| [Assets/Scripts/Entities/Items/EquipmentFunctions.cs:19](../../Assets/Scripts/Entities/Items/EquipmentFunctions.cs#L19) | equipment lifecycle |
| [Assets/Scripts/Entities/Items/EquipmentFunctions.cs:29](../../Assets/Scripts/Entities/Items/EquipmentFunctions.cs#L29) | uses/cooldown storage |
| [Assets/Scripts/Entities/Items/EquipmentFunctions.cs:31](../../Assets/Scripts/Entities/Items/EquipmentFunctions.cs#L31) | uses/cooldown storage |
| [Assets/Scripts/Entities/Items/EquipmentFunctions.cs:33](../../Assets/Scripts/Entities/Items/EquipmentFunctions.cs#L33) | uses/cooldown storage |
| [Assets/Scripts/Entities/Items/EquipmentFunctions.cs:34](../../Assets/Scripts/Entities/Items/EquipmentFunctions.cs#L34) | uses/cooldown storage |
| [Assets/Scripts/Entities/Items/EquipmentFunctions.cs:36](../../Assets/Scripts/Entities/Items/EquipmentFunctions.cs#L36) | uses/cooldown storage |
| [Assets/Scripts/Entities/Items/EquipmentFunctions.cs:38](../../Assets/Scripts/Entities/Items/EquipmentFunctions.cs#L38) | uses/cooldown storage |
| [Assets/Scripts/Entities/Items/EquipmentFunctions.cs:58](../../Assets/Scripts/Entities/Items/EquipmentFunctions.cs#L58) | potion paths |
| [Assets/Scripts/Entities/Items/EquipmentFunctions.cs:68](../../Assets/Scripts/Entities/Items/EquipmentFunctions.cs#L68) | potion paths |
| [Assets/Scripts/Entities/Items/ItemRepository.cs:156](../../Assets/Scripts/Entities/Items/ItemRepository.cs#L156) | equipment lifecycle |
| [Assets/Scripts/Entities/Items/ItemRepository.cs:160](../../Assets/Scripts/Entities/Items/ItemRepository.cs#L160) | equipment lifecycle |
| [Assets/Scripts/Entities/Items/ItemRepository.cs:161](../../Assets/Scripts/Entities/Items/ItemRepository.cs#L161) | equipment lifecycle |
| [Assets/Scripts/Entities/Items/ItemRepository.cs:165](../../Assets/Scripts/Entities/Items/ItemRepository.cs#L165) | equipment lifecycle |
| [Assets/Scripts/Entities/Items/ItemRepository.cs:166](../../Assets/Scripts/Entities/Items/ItemRepository.cs#L166) | equipment lifecycle |
| [Assets/Scripts/Entities/Items/ItemRepository.cs:170](../../Assets/Scripts/Entities/Items/ItemRepository.cs#L170) | equipment lifecycle |
| [Assets/Scripts/Entities/Items/ItemRepository.cs:171](../../Assets/Scripts/Entities/Items/ItemRepository.cs#L171) | equipment lifecycle |
| [Assets/Scripts/Entities/Items/ItemRepository.cs:175](../../Assets/Scripts/Entities/Items/ItemRepository.cs#L175) | equipment lifecycle |
| [Assets/Scripts/Entities/Items/ItemRepository.cs:179](../../Assets/Scripts/Entities/Items/ItemRepository.cs#L179) | equipment lifecycle |
| [Assets/Scripts/Entities/Items/ItemRepository.cs:183](../../Assets/Scripts/Entities/Items/ItemRepository.cs#L183) | equipment lifecycle |
| [Assets/Scripts/Entities/Items/ItemRepository.cs:187](../../Assets/Scripts/Entities/Items/ItemRepository.cs#L187) | equipment lifecycle |
| [Assets/Scripts/Entities/Items/ItemRepository.cs:188](../../Assets/Scripts/Entities/Items/ItemRepository.cs#L188) | equipment lifecycle |
| [Assets/Scripts/Entities/Items/Potion.cs:30](../../Assets/Scripts/Entities/Items/Potion.cs#L30) | potion paths |
| [Assets/Scripts/Entities/Items/Potion.cs:31](../../Assets/Scripts/Entities/Items/Potion.cs#L31) | potion paths |
| [Assets/Scripts/Entities/Spells/SpellList.cs:988](../../Assets/Scripts/Entities/Spells/SpellList.cs#L988) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Actor_Unit.cs:1896](../../Assets/Scripts/Entities/Units/Actor_Unit.cs#L1896) | equipment lifecycle |
| [Assets/Scripts/Entities/Units/Actor_Unit.cs:1903](../../Assets/Scripts/Entities/Units/Actor_Unit.cs#L1903) | equipment lifecycle |
| [Assets/Scripts/Entities/Units/Actor_Unit.cs:1963](../../Assets/Scripts/Entities/Units/Actor_Unit.cs#L1963) | equipment lifecycle |
| [Assets/Scripts/Entities/Units/Actor_Unit.cs:2004](../../Assets/Scripts/Entities/Units/Actor_Unit.cs#L2004) | equipment lifecycle |
| [Assets/Scripts/Entities/Units/Actor_Unit.cs:2030](../../Assets/Scripts/Entities/Units/Actor_Unit.cs#L2030) | equipment lifecycle |
| [Assets/Scripts/Entities/Units/Actor_Unit.cs:2116](../../Assets/Scripts/Entities/Units/Actor_Unit.cs#L2116) | equipment lifecycle |
| [Assets/Scripts/Entities/Units/Actor_Unit.cs:2303](../../Assets/Scripts/Entities/Units/Actor_Unit.cs#L2303) | equipment lifecycle |
| [Assets/Scripts/Entities/Units/Actor_Unit.cs:2327](../../Assets/Scripts/Entities/Units/Actor_Unit.cs#L2327) | equipment lifecycle |
| [Assets/Scripts/Entities/Units/Actor_Unit.cs:2363](../../Assets/Scripts/Entities/Units/Actor_Unit.cs#L2363) | equipment lifecycle |
| [Assets/Scripts/Entities/Units/Actor_Unit.cs:2389](../../Assets/Scripts/Entities/Units/Actor_Unit.cs#L2389) | equipment lifecycle |
| [Assets/Scripts/Entities/Units/Actor_Unit.cs:2403](../../Assets/Scripts/Entities/Units/Actor_Unit.cs#L2403) | equipment lifecycle |
| [Assets/Scripts/Entities/Units/Actor_Unit.cs:2459](../../Assets/Scripts/Entities/Units/Actor_Unit.cs#L2459) | equipment lifecycle |
| [Assets/Scripts/Entities/Units/Actor_Unit.cs:2467](../../Assets/Scripts/Entities/Units/Actor_Unit.cs#L2467) | equipment lifecycle |
| [Assets/Scripts/Entities/Units/Actor_Unit.cs:2494](../../Assets/Scripts/Entities/Units/Actor_Unit.cs#L2494) | equipment lifecycle |
| [Assets/Scripts/Entities/Units/Actor_Unit.cs:2513](../../Assets/Scripts/Entities/Units/Actor_Unit.cs#L2513) | equipment lifecycle |
| [Assets/Scripts/Entities/Units/Actor_Unit.cs:2958](../../Assets/Scripts/Entities/Units/Actor_Unit.cs#L2958) | equipment lifecycle |
| [Assets/Scripts/Entities/Units/Actor_Unit.cs:2959](../../Assets/Scripts/Entities/Units/Actor_Unit.cs#L2959) | equipment lifecycle |
| [Assets/Scripts/Entities/Units/Actor_Unit.cs:3233](../../Assets/Scripts/Entities/Units/Actor_Unit.cs#L3233) | equipment lifecycle |
| [Assets/Scripts/Entities/Units/Actor_Unit.cs:3999](../../Assets/Scripts/Entities/Units/Actor_Unit.cs#L3999) | equipment lifecycle |
| [Assets/Scripts/Entities/Units/NPC_unit.cs:54](../../Assets/Scripts/Entities/Units/NPC_unit.cs#L54) | fixed slot/index |
| [Assets/Scripts/Entities/Units/NPC_unit.cs:56](../../Assets/Scripts/Entities/Units/NPC_unit.cs#L56) | fixed slot/index |
| [Assets/Scripts/Entities/Units/NPC_unit.cs:75](../../Assets/Scripts/Entities/Units/NPC_unit.cs#L75) | fixed slot/index |
| [Assets/Scripts/Entities/Units/NPC_unit.cs:77](../../Assets/Scripts/Entities/Units/NPC_unit.cs#L77) | fixed slot/index |
| [Assets/Scripts/Entities/Units/NPC_unit.cs:97](../../Assets/Scripts/Entities/Units/NPC_unit.cs#L97) | fixed slot/index |
| [Assets/Scripts/Entities/Units/NPC_unit.cs:99](../../Assets/Scripts/Entities/Units/NPC_unit.cs#L99) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:270](../../Assets/Scripts/Entities/Units/Unit.cs#L270) | uses/cooldown storage |
| [Assets/Scripts/Entities/Units/Unit.cs:272](../../Assets/Scripts/Entities/Units/Unit.cs#L272) | uses/cooldown storage |
| [Assets/Scripts/Entities/Units/Unit.cs:274](../../Assets/Scripts/Entities/Units/Unit.cs#L274) | potion paths |
| [Assets/Scripts/Entities/Units/Unit.cs:750](../../Assets/Scripts/Entities/Units/Unit.cs#L750) | slot sizing |
| [Assets/Scripts/Entities/Units/Unit.cs:751](../../Assets/Scripts/Entities/Units/Unit.cs#L751) | uses/cooldown storage |
| [Assets/Scripts/Entities/Units/Unit.cs:752](../../Assets/Scripts/Entities/Units/Unit.cs#L752) | uses/cooldown storage |
| [Assets/Scripts/Entities/Units/Unit.cs:753](../../Assets/Scripts/Entities/Units/Unit.cs#L753) | potion paths |
| [Assets/Scripts/Entities/Units/Unit.cs:765](../../Assets/Scripts/Entities/Units/Unit.cs#L765) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:906](../../Assets/Scripts/Entities/Units/Unit.cs#L906) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:911](../../Assets/Scripts/Entities/Units/Unit.cs#L911) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:916](../../Assets/Scripts/Entities/Units/Unit.cs#L916) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:921](../../Assets/Scripts/Entities/Units/Unit.cs#L921) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:926](../../Assets/Scripts/Entities/Units/Unit.cs#L926) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:930](../../Assets/Scripts/Entities/Units/Unit.cs#L930) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:934](../../Assets/Scripts/Entities/Units/Unit.cs#L934) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:938](../../Assets/Scripts/Entities/Units/Unit.cs#L938) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:943](../../Assets/Scripts/Entities/Units/Unit.cs#L943) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:948](../../Assets/Scripts/Entities/Units/Unit.cs#L948) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:953](../../Assets/Scripts/Entities/Units/Unit.cs#L953) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:958](../../Assets/Scripts/Entities/Units/Unit.cs#L958) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:963](../../Assets/Scripts/Entities/Units/Unit.cs#L963) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:968](../../Assets/Scripts/Entities/Units/Unit.cs#L968) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:973](../../Assets/Scripts/Entities/Units/Unit.cs#L973) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:974](../../Assets/Scripts/Entities/Units/Unit.cs#L974) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:981](../../Assets/Scripts/Entities/Units/Unit.cs#L981) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:982](../../Assets/Scripts/Entities/Units/Unit.cs#L982) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:989](../../Assets/Scripts/Entities/Units/Unit.cs#L989) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:996](../../Assets/Scripts/Entities/Units/Unit.cs#L996) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:997](../../Assets/Scripts/Entities/Units/Unit.cs#L997) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:1006](../../Assets/Scripts/Entities/Units/Unit.cs#L1006) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:1007](../../Assets/Scripts/Entities/Units/Unit.cs#L1007) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:1016](../../Assets/Scripts/Entities/Units/Unit.cs#L1016) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:1023](../../Assets/Scripts/Entities/Units/Unit.cs#L1023) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:1028](../../Assets/Scripts/Entities/Units/Unit.cs#L1028) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:1033](../../Assets/Scripts/Entities/Units/Unit.cs#L1033) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:1038](../../Assets/Scripts/Entities/Units/Unit.cs#L1038) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:1045](../../Assets/Scripts/Entities/Units/Unit.cs#L1045) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:1046](../../Assets/Scripts/Entities/Units/Unit.cs#L1046) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:1053](../../Assets/Scripts/Entities/Units/Unit.cs#L1053) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:1058](../../Assets/Scripts/Entities/Units/Unit.cs#L1058) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:1063](../../Assets/Scripts/Entities/Units/Unit.cs#L1063) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:1070](../../Assets/Scripts/Entities/Units/Unit.cs#L1070) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:1079](../../Assets/Scripts/Entities/Units/Unit.cs#L1079) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:1086](../../Assets/Scripts/Entities/Units/Unit.cs#L1086) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:1196](../../Assets/Scripts/Entities/Units/Unit.cs#L1196) | item mutation |
| [Assets/Scripts/Entities/Units/Unit.cs:1923](../../Assets/Scripts/Entities/Units/Unit.cs#L1923) | equipment lifecycle |
| [Assets/Scripts/Entities/Units/Unit.cs:1942](../../Assets/Scripts/Entities/Units/Unit.cs#L1942) | equipment lifecycle |
| [Assets/Scripts/Entities/Units/Unit.cs:1960](../../Assets/Scripts/Entities/Units/Unit.cs#L1960) | equipment lifecycle |
| [Assets/Scripts/Entities/Units/Unit.cs:2090](../../Assets/Scripts/Entities/Units/Unit.cs#L2090) | slot sizing |
| [Assets/Scripts/Entities/Units/Unit.cs:2213](../../Assets/Scripts/Entities/Units/Unit.cs#L2213) | slot sizing |
| [Assets/Scripts/Entities/Units/Unit.cs:2219](../../Assets/Scripts/Entities/Units/Unit.cs#L2219) | fixed slot/index, item mutation |
| [Assets/Scripts/Entities/Units/Unit.cs:2224](../../Assets/Scripts/Entities/Units/Unit.cs#L2224) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:2229](../../Assets/Scripts/Entities/Units/Unit.cs#L2229) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:2230](../../Assets/Scripts/Entities/Units/Unit.cs#L2230) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:2242](../../Assets/Scripts/Entities/Units/Unit.cs#L2242) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:2243](../../Assets/Scripts/Entities/Units/Unit.cs#L2243) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:2249](../../Assets/Scripts/Entities/Units/Unit.cs#L2249) | fixed slot/index |
| [Assets/Scripts/Entities/Units/Unit.cs:2519](../../Assets/Scripts/Entities/Units/Unit.cs#L2519) | slot sizing |
| [Assets/Scripts/Entities/Units/Unit.cs:2520](../../Assets/Scripts/Entities/Units/Unit.cs#L2520) | potion paths |
| [Assets/Scripts/Entities/Units/Unit.cs:2521](../../Assets/Scripts/Entities/Units/Unit.cs#L2521) | potion paths |
| [Assets/Scripts/Entities/Units/Unit.cs:3085](../../Assets/Scripts/Entities/Units/Unit.cs#L3085) | item mutation |
| [Assets/Scripts/Entities/Units/Unit.cs:3089](../../Assets/Scripts/Entities/Units/Unit.cs#L3089) | item mutation |
| [Assets/Scripts/Entities/Units/Unit.cs:3096](../../Assets/Scripts/Entities/Units/Unit.cs#L3096) | item mutation |
| [Assets/Scripts/Entities/Units/Unit.cs:3107](../../Assets/Scripts/Entities/Units/Unit.cs#L3107) | item mutation |
| [Assets/Scripts/Entities/Units/Unit.cs:3130](../../Assets/Scripts/Entities/Units/Unit.cs#L3130) | item mutation |
| [Assets/Scripts/Entities/Units/Unit.cs:3134](../../Assets/Scripts/Entities/Units/Unit.cs#L3134) | uses/cooldown storage |
| [Assets/Scripts/Entities/Units/Unit.cs:3135](../../Assets/Scripts/Entities/Units/Unit.cs#L3135) | uses/cooldown storage |
| [Assets/Scripts/Entities/Units/Unit.cs:3143](../../Assets/Scripts/Entities/Units/Unit.cs#L3143) | item mutation |
| [Assets/Scripts/Entities/Units/Unit.cs:3148](../../Assets/Scripts/Entities/Units/Unit.cs#L3148) | uses/cooldown storage |
| [Assets/Scripts/Entities/Units/Unit.cs:3597](../../Assets/Scripts/Entities/Units/Unit.cs#L3597) | slot sizing |
| [Assets/Scripts/Entities/Units/Unit.cs:3622](../../Assets/Scripts/Entities/Units/Unit.cs#L3622) | slot sizing |
| [Assets/Scripts/Entities/Units/Unit.cs:3658](../../Assets/Scripts/Entities/Units/Unit.cs#L3658) | slot sizing |
| [Assets/Scripts/Entities/Units/Unit.cs:3680](../../Assets/Scripts/Entities/Units/Unit.cs#L3680) | slot sizing |
| [Assets/Scripts/Entities/Units/Unit.cs:3683](../../Assets/Scripts/Entities/Units/Unit.cs#L3683) | uses/cooldown storage |
| [Assets/Scripts/Entities/Units/Unit.cs:3684](../../Assets/Scripts/Entities/Units/Unit.cs#L3684) | uses/cooldown storage |
| [Assets/Scripts/Entities/Units/Unit.cs:3685](../../Assets/Scripts/Entities/Units/Unit.cs#L3685) | potion paths |
| [Assets/Scripts/AI/Legacy/legacyStrategicAI.cs:365](../../Assets/Scripts/AI/Legacy/legacyStrategicAI.cs#L365) | fixed slot/index, item mutation |
| [Assets/Scripts/AI/Legacy/legacyStrategicAI.cs:373](../../Assets/Scripts/AI/Legacy/legacyStrategicAI.cs#L373) | fixed slot/index, item mutation |
| [Assets/Scripts/AI/Legacy/legacyStrategicAI.cs:398](../../Assets/Scripts/AI/Legacy/legacyStrategicAI.cs#L398) | fixed slot/index, item mutation |
| [Assets/Scripts/AI/Legacy/legacyStrategicAI.cs:402](../../Assets/Scripts/AI/Legacy/legacyStrategicAI.cs#L402) | fixed slot/index, item mutation |
| [Assets/Scripts/AI/Legacy/legacyStrategicAI.cs:411](../../Assets/Scripts/AI/Legacy/legacyStrategicAI.cs#L411) | fixed slot/index, item mutation |
| [Assets/Scripts/AI/Legacy/legacyStrategicAI.cs:448](../../Assets/Scripts/AI/Legacy/legacyStrategicAI.cs#L448) | fixed slot/index, item mutation |
| [Assets/Scripts/AI/Legacy/legacyStrategicAI.cs:452](../../Assets/Scripts/AI/Legacy/legacyStrategicAI.cs#L452) | fixed slot/index, item mutation |
| [Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs:181](../../Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs#L181) | fixed slot/index |
| [Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs:183](../../Assets/Scripts/AI/Monsters/MonsterStrategicAI.cs#L183) | fixed slot/index |
