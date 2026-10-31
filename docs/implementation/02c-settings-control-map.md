# 02C — Settings control map

**Date:** 2026-10-30. Implementation companion to [the packet record](02c-settings-retirement-and-balance-locking.md). This records the pre-edit scene bindings and the retained/retired decision for each component field. Objects remain in the scene; retired fields are disconnected from ContentSettings.

The 215 field bindings below include 59 actual preferences and two navigation/tooltip references. The component also retains its separately recorded five-element SettingsPanels array. Seven full pages and the specified controls within retained pages are inactive. Dynamic mercenary/spawner construction and BuildingSettings load/save hooks have been removed from the active component.

| Field | Scene reference ID | Original page | Policy |
|---|---:|---|---|
| `FemaleFraction` | 1525541078 | Gender Settings | Retain |
| `HermFraction` | 573222328 | Gender Settings | Retain |
| `HermNameFraction` | 1908071396 | Gender Settings | Retain |
| `ClothedFraction` | 785092071 | Appearance Settings | Retain |
| `WeightGain` | 1292339692 | Post-Vore | Retire binding |
| `WeightLoss` | 147877280 | Post-Vore | Retire binding |
| `WeightLossFractionBreasts` | 2071348941 | Post-Vore | Retire binding |
| `WeightLossFractionBody` | 1869126405 | Post-Vore | Retire binding |
| `WeightLossFractionDick` | 140925664 | Post-Vore | Retire binding |
| `GrowthCap` | 1513995869 | Vore Misc | Retire binding |
| `GrowthMod` | 921526691 | Vore Misc | Retire binding |
| `GrowthDecayOffset` | 507133295 | Vore Misc | Retire binding |
| `GrowthDecayIncreaseRate` | 1703547162 | Vore Misc | Retire binding |
| `FurryHandsAndFeet` | 848864218 | Appearance Settings | Retain |
| `FurryFluff` | 1110098543 | Appearance Settings | Retain |
| `FurryFraction` | 741408869 | Appearance Settings | Retain |
| `FriendlyRegurgitation` | 233947648 | Vore Main | Retain |
| `FogDistance` | 177954454 | Fog of War | Retire binding |
| `HairMatchesFur` | 463186972 | Appearance Settings | Retain |
| `MaleHairForFemales` | 1421965789 | Gender Settings | Retain |
| `FemaleHairForMales` | 1578395544 | Gender Settings | Retain |
| `HermsOnlyUseFemaleHair` | 649376349 | Gender Settings | Retain |
| `HideBreasts` | 1972273043 | Gender Settings | Retain |
| `HideCocks` | 1830567076 | Gender Settings | Retain |
| `RaceTraitsEnabled` | 1252198715 | Tactical Gameplay | Retire binding |
| `NoAIRetreat` | 1764062564 | Tactical Gameplay | Retire binding |
| `AICanHireSpecialMercs` | 1177995292 | Strategic Gameplay | Retire binding |
| `AICanCheatSpecialMercs` | 1398117330 | Strategic Gameplay | Retire binding |
| `MultiRaceVillages` | 57444836 | Strategic Gameplay | Retire binding |
| `RagsForSlaves` | 36260560 | Appearance Settings | Retain |
| `VisibleCorpses` | 1728097075 | Appearance Settings | Retain |
| `EdibleCorpses` | 1726700025 | Vore Main | Retire binding |
| `RaceSizeLimitsWeightGain` | 1880189265 | Post-Vore | Retire binding |
| `RaceSpecificVoreGraphicsDisabled` | 549375265 | Vore Misc | Retire binding |
| `GoblinCaravans` | 67960565 | Strategic Gameplay | Retire binding |
| `FogOfWar` | 893696548 | Fog of War | Retire binding |
| `LeadersUseCustomizations` | 781304764 | Appearance Settings | Retain |
| `HermsCanUB` | 1955337139 | Gender Settings | Retain |
| `Unbirth` | 1699856498 | Vore Main | Retain |
| `CockVore` | 646475173 | Vore Main | Retain |
| `BreastVore` | 656888968 | Vore Main | Retire binding |
| `AnalVore` | 1183252556 | Vore Main | Retain |
| `TailVore` | 577005153 | Vore Main | Retain |
| `BladderVore` | 754038438 | Vore Main | Retire binding |
| `AltVoreOralGain` | 2145308027 | Post-Vore | Retire binding |
| `MultiRaceFlip` | 1242634386 | Strategic Gameplay | Retire binding |
| `AdventurersDisabled` | 2114724213 | Strategic Gameplay | Retire binding |
| `MercenariesDisabled` | 1235771299 | Strategic Gameplay | Retire binding |
| `TroopScatter` | 854433089 | Strategic Gameplay | Retire binding |
| `AlwaysRandomizeConverted` | 1524173646 | Vore Misc | Retire binding |
| `SpecialMercsCanConvert` | 1077318397 | Vore Misc | Retire binding |
| `CanUseStomachRubOnEnemies` | 102359187 | Vore Main | Retain |
| `LeadersRerandomizeOnDeath` | 36206444 | Appearance Settings | Retain |
| `DayNightEnabled` | 158303118 | Fog of War | Retire binding |
| `DayNightCosmetic` | 757382567 | Fog of War | Retire binding |
| `DayNightSchedule` | 1326292999 | Fog of War | Retire binding |
| `DayNightRandom` | 1374162161 | Fog of War | Retire binding |
| `NightMonsters` | 912299540 | Fog of War | Retire binding |
| `NightMoveMonsters` | 1784294730 | Fog of War | Retire binding |
| `NightRounds` | 487207703 | Fog of War | Retire binding |
| `BaseNightChance` | 1341527176 | Fog of War | Retire binding |
| `NightChanceIncrease` | 239713250 | Fog of War | Retire binding |
| `DefualtTacticalSightRange` | 1716098350 | Fog of War | Retire binding |
| `NightStrategicSightReduction` | 351122062 | Fog of War | Retire binding |
| `RevealTurn` | 2018508211 | Fog of War | Retire binding |
| `DayNightMonsterMovemnt` | 0 | Helper/navigation | Retire binding |
| `CombatComplicationsEnabled` | 781998191 | Tactical Misc | Retire binding |
| `StatCrit` | 623379187 | Tactical Misc | Retire binding |
| `StatGraze` | 450287863 | Tactical Misc | Retire binding |
| `BaseCritChance` | 1933880229 | Tactical Misc | Retire binding |
| `CritDamageMod` | 864322583 | Tactical Misc | Retire binding |
| `BaseGrazeChance` | 830601718 | Tactical Misc | Retire binding |
| `GrazeDamageMod` | 1865939972 | Tactical Misc | Retire binding |
| `AllowHugeBreasts` | 1522103123 | Gender Settings | Retain |
| `AllowHugeDicks` | 1866807718 | Gender Settings | Retain |
| `AllowTopless` | 1766638033 | Gender Settings | Retain |
| `BreastSizeModifier` | 1466495845 | Gender Settings | Retain |
| `HermBreastSizeModifier` | 1375574103 | Gender Settings | Retain |
| `CockSizeModifier` | 240110989 | Gender Settings | Retain |
| `DefaultStartingWeight` | 1412613924 | Appearance Settings | Retain |
| `AutoSurrenderChance` | 1761995971 | Tactical Gameplay | Retire binding |
| `AutoSurrenderDefectChance` | 616185040 | Tactical Gameplay | Retire binding |
| `OverallMonsterCapModifier` | 1840167080 | Merc / Monster Spawning | Retire binding |
| `OverallMonsterSpawnRateModifier` | 1438536270 | Merc / Monster Spawning | Retire binding |
| `Diplomacy` | 554869516 | Strategic Gameplay | Retire binding |
| `CockVoreHidesClothes` | 1870168312 | Gender Settings | Retain |
| `KuroTenkoEnabled` | 87866254 | Vore Misc | Retire binding |
| `OverhealEXP` | 223212389 | Vore Misc | Retire binding |
| `TransferAllowed` | 988977748 | Vore Misc | Retire binding |
| `CumGestation` | 1616186847 | Vore Misc | Retire binding |
| `NoScatForDeadTransfers` | 1260973974 | Vore Misc | Retire binding |
| `LewdDialog` | 1904059939 | Gender Settings | Retain |
| `HardVoreDialog` | 804720834 | Vore Main | Retire binding |
| `FemalesLike` | 1943900338 | Gender Settings | Retain |
| `MalesLike` | 191768573 | Gender Settings | Retain |
| `FairyBVType` | 1238431040 | Vore Main | Retire binding |
| `FeedingType` | 298593923 | Vore Misc | Retire binding |
| `FourthWallBreakType` | 1392764945 | Vore Misc | Retire binding |
| `UBConversion` | 1943584428 | Vore Misc | Retire binding |
| `GoddessMercy` | 385362845 | Tactical Gameplay | Retire binding |
| `SucklingPermission` | 962737353 | Vore Misc | Retire binding |
| `WinterStuff` | 753645372 | Appearance Settings | Retain |
| `DiplomacyScale` | 1170718036 | Strategic Gameplay | Retire binding |
| `MaxSpellLevelDrop` | 367717048 | Strategic Gameplay | Retire binding |
| `MaxEquipmentLevelDrop` | 2096857786 | Strategic Gameplay | Retire binding |
| `TacticalWaterValue` | 1989631291 | Tactical Gameplay | Retire binding |
| `TacticalTerrainFrequency` | 1090316346 | Tactical Gameplay | Retire binding |
| `OralWeight` | 285058624 | Vore Main | Retain |
| `AnalWeight` | 1200720789 | Vore Main | Retain |
| `BreastWeight` | 735085829 | Vore Main | Retire binding |
| `CockWeight` | 1297482125 | Vore Main | Retain |
| `UnbirthWeight` | 1829940451 | Vore Main | Retain |
| `TailWeight` | 1808842099 | Vore Main | Retain |
| `BladderWeight` | 547105777 | Vore Main | Retire binding |
| `FurryGenitals` | 1499292700 | Gender Settings | Retain |
| `LamiaUseTailAsSecondBelly` | 1909699424 | Appearance Settings | Retain |
| `HideViperSlit` | 1567235775 | Gender Settings | Retain |
| `FlatExperience` | 1057482391 | Strategic Gameplay | Retire binding |
| `MonstersDropSpells` | 1381830676 | Merc / Monster Spawning | Retire binding |
| `AnimatedBellies` | 190871664 | Vore Main | Retain |
| `DigestionSkulls` | 1850843178 | Post-Vore | Retain |
| `BellyRubHands` | 612716400 | Post-Vore | Retain |
| `SurrenderFlag` | 1712182859 | Tactical Gameplay | Retain |
| `ShowUnitSides` | 266546773 | Tactical Gameplay | Retain |
| `BurpFraction` | 385150883 | Post-Vore | Retain |
| `BurpOnDigest` | 185299223 | Post-Vore | Retain |
| `FartFraction` | 674948497 | Post-Vore | Retire binding |
| `WeightGainFraction` | 578095566 | Post-Vore | Retire binding |
| `FartOnAbsorb` | 1792704643 | Post-Vore | Retire binding |
| `Bones` | 1653951734 | Post-Vore | Retain |
| `CleanDisposal` | 228225885 | Post-Vore | Retire binding |
| `Scat` | 1460094685 | Post-Vore | Retire binding |
| `ScatV2` | 1634190043 | Post-Vore | Retire binding |
| `BirdScat` | 331291549 | Post-Vore | Retire binding |
| `ScatBones` | 920346904 | Post-Vore | Retire binding |
| `ClothingDiscards` | 930263821 | Appearance Settings | Retain |
| `ErectionsFromVore` | 1235097676 | Gender Settings | Retain |
| `ErectionsFromCockVore` | 1719885973 | Gender Settings | Retain |
| `AutoSurrender` | 1725633519 | Tactical Gameplay | Retire binding |
| `EatSurrenderedAllies` | 1765087147 | Tactical Gameplay | Retire binding |
| `BoostedAccuracy` | 433267493 | Tactical Gameplay | Retire binding |
| `DisorientedPrey` | 957755762 | Vore Main | Retire binding |
| `ExtraHairColors` | 628893112 | Appearance Settings | Retain |
| `LizardsHaveNoBreasts` | 1125854218 | Gender Settings | Retain |
| `AllowInfighting` | 500985551 | Tactical Gameplay | Retire binding |
| `SurrenderedCanConvert` | 434188391 | Tactical Gameplay | Retire binding |
| `LockedAIRelations` | 937686628 | Strategic Gameplay | Retire binding |
| `Defections` | 1336352107 | Tactical Gameplay | Retire binding |
| `Cumstains` | 884551023 | Post-Vore | Retain |
| `CondomsForCV` | 1262146702 | Post-Vore | Retain |
| `MonstersCanReform` | 2015875831 | Merc / Monster Spawning | Retire binding |
| `TooltipText` | 504747422 | Helper/navigation | Retain |
| `PageSelector` | 5537626153803511361 | Helper/navigation | Retain |
| `AllMercs` | 1658623700 | Merc / Monster Spawning | Retire binding |
| `MercenaryToggleFolder` | 989498051 | Merc / Monster Spawning | Retire binding |
| `TogglePrefab` | 1203441227673672 | Helper/navigation | Retire binding |
| `MonsterSpawnerFolder` | 1498791141 | Merc / Monster Spawning | Retire binding |
| `MonsterSpawnerPrefab` | 793185541971709009 | Helper/navigation | Retire binding |
| `ArmyMP` | 1794804451 | Strategic Gameplay | Retire binding |
| `ScoutMP` | 1830919382 | Strategic Gameplay | Retire binding |
| `ScoutMax` | 1635318951 | Strategic Gameplay | Retire binding |
| `ArmyCreationMPMod` | 1050907281 | Strategic Gameplay | Retire binding |
| `ArmyCreationMPCurve` | 1271644442 | Strategic Gameplay | Retire binding |
| `CustomEventFrequency` | 600554831 | Strategic Gameplay | Retire binding |
| `MonsterConquest` | 2144333868 | Merc / Monster Spawning | Retire binding |
| `VoreRate` | 2130014495 | Vore Main | Retire binding |
| `EscapeRate` | 2144044925 | Vore Main | Retire binding |
| `RandomEventRate` | 1765713681 | Strategic Gameplay | Retire binding |
| `RandomAIEventRate` | 478361900 | Strategic Gameplay | Retire binding |
| `EventsRepeat` | 2125519888 | Strategic Gameplay | Retire binding |
| `MercSortMethod` | 1710195080 | Merc / Monster Spawning | Retire binding |
| `MercSortDirection` | 1043706866 | Merc / Monster Spawning | Retire binding |
| `MonsterConquestTurns` | 1423329119 | Merc / Monster Spawning | Retire binding |
| `LeaderTraits` | 288819545 | Tactical Gameplay | Retire binding |
| `MaleTraits` | 1819493723 | Tactical Gameplay | Retire binding |
| `FemaleTraits` | 1599151775 | Tactical Gameplay | Retire binding |
| `HermTraits` | 1481282733 | Tactical Gameplay | Retire binding |
| `SpawnTraits` | 1686667196 | Tactical Gameplay | Retire binding |
| `MaxArmies` | 939000862 | Strategic Gameplay | Retire binding |
| `StatBoostsAffectMaxHP` | 2021297459 | Vore Misc | Retire binding |
| `OverfeedingDamage` | 123860790 | Vore Misc | Retire binding |
| `PotionSystemEnabled` | 208530011 | Strategic Gameplay | Retire binding |
| `DigestionSpeedMult` | 1073760087 | Vore Advanced | Retire binding |
| `AbsorbSpeedMult` | 1780442903 | Vore Advanced | Retire binding |
| `BellyRubEffMult` | 1820426994 | Vore Advanced | Retire binding |
| `BellyRubsPerTurn` | 549604152 | Vore Advanced | Retire binding |
| `DigestionRamp` | 1790241371 | Vore Advanced | Retire binding |
| `DigestionRampTurn` | 271518371 | Vore Advanced | Retire binding |
| `DigestionRampCap` | 1992919352 | Vore Advanced | Retire binding |
| `DigestionRampLoss` | 202995342 | Vore Advanced | Retire binding |
| `AbsorbRamp` | 1671421717 | Vore Advanced | Retire binding |
| `AbsorbResourceMod` | 565439092 | Vore Advanced | Retire binding |
| `AbsorbLoss` | 639539580 | Vore Advanced | Retire binding |
| `AbsorbBoostDeadOnly` | 48525758 | Vore Advanced | Retire binding |
| `AbsorbResourceModBoost` | 882786177 | Vore Advanced | Retire binding |
| `DigestionCap` | 1992919352 | Vore Advanced | Retire binding |
| `DigestionFlatDmg` | 664543559 | Vore Advanced | Retire binding |
| `DigestionGraceTurns` | 1214084563 | Vore Advanced | Retire binding |
| `DigestionDamageDivision` | 1948655783 | Vore Advanced | Retire binding |
| `AbsorbRateDivision` | 923297057 | Vore Advanced | Retire binding |
| `SurrenderedPredEscapeMult` | 33179335 | Vore Main | Retire binding |
| `SurrenderedPredAutoRegur` | 2057479351 | Vore Main | Retire binding |
| `TacticalMovementSoftCap` | 1582728229 | Tactical Misc | Retire binding |
| `TacticalMovementHardCap` | 1849221532 | Tactical Misc | Retire binding |
| `SizeAccuracyMod` | 704132810 | Tactical Misc | Retire binding |
| `SizeAccuracyLowerBound` | 341319346 | Tactical Misc | Retire binding |
| `SizeAccuracyInterval` | 1315243191 | Tactical Misc | Retire binding |
| `SizeAccuracyInverse` | 950705041 | Tactical Misc | Retire binding |
| `SizeAccuracyCap` | 1831149279 | Tactical Misc | Retire binding |
| `SizeDamageMod` | 1240121290 | Tactical Misc | Retire binding |
| `SizeDamageLowerBound` | 1300780221 | Tactical Misc | Retire binding |
| `SizeDamageInterval` | 903512380 | Tactical Misc | Retire binding |
| `SizeDamageInverse` | 519975991 | Tactical Misc | Retire binding |
| `SizeDamageCap` | 941797734 | Tactical Misc | Retire binding |
| `buildingSettings` | 1859552281 | Building Options | Retire binding |

## Inactive scene roots

IDs identify the scene blocks changed for page/widget activity. For prefab instances, the root source GameObject was checked against its source prefab before adding/updating the active override. No shared prefab was changed.

| Kind | Scene block ID | Source root ID | Source prefab GUID |
|---|---:|---:|---|
| go | 24594958 | — | — |
| go | 70401225 | — | — |
| go | 417709632 | — | — |
| go | 578095563 | — | — |
| go | 674948494 | — | — |
| go | 901646815 | — | — |
| go | 913145415 | — | — |
| go | 1732272875 | — | — |
| go | 1746464486 | — | — |
| inst | 33179333 | 1522731415904372 | de238d9f746b63947b58c5a18189ebf9 |
| inst | 147877278 | 1203441227673672 | 85fd49726cb8b6b4e84b5a705fe74ce2 |
| inst | 228225881 | 1203441227673672 | 85fd49726cb8b6b4e84b5a705fe74ce2 |
| inst | 265613602 | 1203441227673672 | 85fd49726cb8b6b4e84b5a705fe74ce2 |
| inst | 288819544 | 1320550614093716 | 89bebbadda021fa4888a74173e78312f |
| inst | 331291547 | 1203441227673672 | 85fd49726cb8b6b4e84b5a705fe74ce2 |
| inst | 352977798 | 1518062312798294 | b1eb41b4645bf4c4e9e7c8eb8802e45e |
| inst | 402141450 | 1518062312798294 | b1eb41b4645bf4c4e9e7c8eb8802e45e |
| inst | 433267491 | 1203441227673672 | 85fd49726cb8b6b4e84b5a705fe74ce2 |
| inst | 434188389 | 1203441227673672 | 85fd49726cb8b6b4e84b5a705fe74ce2 |
| inst | 500985549 | 1203441227673672 | 85fd49726cb8b6b4e84b5a705fe74ce2 |
| inst | 656888965 | 1203441227673672 | 85fd49726cb8b6b4e84b5a705fe74ce2 |
| inst | 716650687 | 1518062312798294 | b1eb41b4645bf4c4e9e7c8eb8802e45e |
| inst | 754038435 | 1203441227673672 | 85fd49726cb8b6b4e84b5a705fe74ce2 |
| inst | 804720832 | 1203441227673672 | 85fd49726cb8b6b4e84b5a705fe74ce2 |
| inst | 920346901 | 1203441227673672 | 85fd49726cb8b6b4e84b5a705fe74ce2 |
| inst | 957755758 | 1203441227673672 | 85fd49726cb8b6b4e84b5a705fe74ce2 |
| inst | 1090316342 | 1522731415904372 | de238d9f746b63947b58c5a18189ebf9 |
| inst | 1252198712 | 1203441227673672 | 85fd49726cb8b6b4e84b5a705fe74ce2 |
| inst | 1336352105 | 1203441227673672 | 85fd49726cb8b6b4e84b5a705fe74ce2 |
| inst | 1368994955 | 1518062312798294 | b1eb41b4645bf4c4e9e7c8eb8802e45e |
| inst | 1460094682 | 1203441227673672 | 85fd49726cb8b6b4e84b5a705fe74ce2 |
| inst | 1481282731 | 1320550614093716 | 89bebbadda021fa4888a74173e78312f |
| inst | 1522245814 | 1518062312798294 | b1eb41b4645bf4c4e9e7c8eb8802e45e |
| inst | 1599151773 | 1320550614093716 | 89bebbadda021fa4888a74173e78312f |
| inst | 1634190041 | 1203441227673672 | 85fd49726cb8b6b4e84b5a705fe74ce2 |
| inst | 1663814752 | 1518062312798294 | b1eb41b4645bf4c4e9e7c8eb8802e45e |
| inst | 1686667194 | 1320550614093716 | 89bebbadda021fa4888a74173e78312f |
| inst | 1725633515 | 1203441227673672 | 85fd49726cb8b6b4e84b5a705fe74ce2 |
| inst | 1726700022 | 1203441227673672 | 85fd49726cb8b6b4e84b5a705fe74ce2 |
| inst | 1764062562 | 1203441227673672 | 85fd49726cb8b6b4e84b5a705fe74ce2 |
| inst | 1765087138 | 1203441227673672 | 85fd49726cb8b6b4e84b5a705fe74ce2 |
| inst | 1792704641 | 1203441227673672 | 85fd49726cb8b6b4e84b5a705fe74ce2 |
| inst | 1819493721 | 1320550614093716 | 89bebbadda021fa4888a74173e78312f |
| inst | 1859552278 | 1518062312798294 | b1eb41b4645bf4c4e9e7c8eb8802e45e |
| inst | 1880189263 | 1203441227673672 | 85fd49726cb8b6b4e84b5a705fe74ce2 |
| inst | 1989631287 | 1522731415904372 | de238d9f746b63947b58c5a18189ebf9 |
| inst | 2057479350 | 1522731415904372 | de238d9f746b63947b58c5a18189ebf9 |
| inst | 2145308023 | 1203441227673672 | 85fd49726cb8b6b4e84b5a705fe74ce2 |

## Retired persistent callbacks

All 77 callbacks below target the ContentSettings component and have call state zero. Where the method/state was inherited, it was resolved from the source prefab and disabled with an instance override. Retained ActivatePage, VoreTypesChanged, reload/apply/save callbacks keep their existing routes.

| Scene block ID | Method |
|---:|---|
| 33179333 | `RefreshSliderText` |
| 36206440 | `CorpsesChanged` |
| 48525756 | `WeightLossChanged` |
| 87866250 | `KuroTenkoChanged` |
| 140925662 | `RefreshSliderText` |
| 147877278 | `WeightLossChanged` |
| 158303115 | `DayNightCycleChanged` |
| 185299221 | `CorpsesChanged` |
| 190871661 | `CorpsesChanged` |
| 202995340 | `RefreshSliderText` |
| 223212385 | `WeightGainChanged` |
| 228225881 | `DiaperChanged` |
| 265613602 | `WeightGainChanged` |
| 266546771 | `SurrenderChanged` |
| 271518367 | `RefreshSliderText` |
| 331291547 | `DiaperChanged` |
| 549604150 | `RefreshSliderText` |
| 554869514 | `DiplomacyChanged` |
| 565439090 | `RefreshSliderText` |
| 600554829 | `RefreshSliderText` |
| 612716397 | `CorpsesChanged` |
| 628893109 | `CorpsesChanged` |
| 639539578 | `WeightLossChanged` |
| 664543557 | `RefreshSliderText` |
| 781304762 | `CorpsesChanged` |
| 830601715 | `RefreshSliderText` |
| 864322580 | `RefreshSliderText` |
| 884551021 | `ScatChanged` |
| 920346901 | `CorpsesChanged` |
| 923297053 | `WeightLossChanged` |
| 930263819 | `CorpsesChanged` |
| 937686626 | `DiplomacyChanged` |
| 939000861 | `RefreshSliderText` |
| 988977744 | `WeightGainChanged` |
| 1043706865 | `MercSortDirectionChanged` |
| 1050907277 | `RefreshSliderText` |
| 1073760083 | `RefreshSliderText` |
| 1077318395 | `WeightGainChanged` |
| 1090316342 | `RefreshSliderText` |
| 1125854216 | `CorpsesChanged` |
| 1214084561 | `RefreshSliderText` |
| 1260973972 | `WeightGainChanged` |
| 1262146700 | `ScatChanged` |
| 1271644438 | `RefreshSliderText` |
| 1303696420 | `RefreshSliderText` |
| 1326292996 | `DayNightCycleChanged` |
| 1374162158 | `DayNightCycleChanged` |
| 1460094682 | `ScatChanged` |
| 1524173644 | `WeightGainChanged` |
| 1567235771 | `CorpsesChanged` |
| 1616186843 | `WeightGainChanged` |
| 1634190041 | `ScatChanged` |
| 1635318950 | `RefreshSliderText` |
| 1653951732 | `ScatChanged` |
| 1671421715 | `RefreshSliderText` |
| 1710195079 | `MercSortChanged` |
| 1712182857 | `SurrenderChanged` |
| 1725633515 | `SurrenderChanged` |
| 1728097072 | `CorpsesChanged` |
| 1780442899 | `RefreshSliderText` |
| 1790241369 | `RefreshSliderText` |
| 1792704641 | `CorpsesChanged` |
| 1794804450 | `RefreshSliderText` |
| 1820426992 | `RefreshSliderText` |
| 1830919380 | `RefreshSliderText` |
| 1850843175 | `CorpsesChanged` |
| 1865939969 | `RefreshSliderText` |
| 1869126403 | `RefreshSliderText` |
| 1909699422 | `CorpsesChanged` |
| 1933880226 | `RefreshSliderText` |
| 1948655781 | `WeightLossChanged` |
| 1989631287 | `RefreshSliderText` |
| 1992919350 | `DigestionRampChanged` |
| 2057479350 | `RefreshSliderText` |
| 2071348940 | `RefreshSliderText` |
| 2090889183 | `AllMercsCheckedChanged` |
| 2144333868 | `MonsterDropdownChanged` |
