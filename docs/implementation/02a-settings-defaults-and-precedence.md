# Packet 02A — content defaults and preference ownership

**Status:** 02A implemented and compiled, 2026-10-30. The user confirmed Show Unit Sides persisted between exiting and coming back and authorized 02B. First source-change packet, limited to extracting existing data loading. Screens, settings eligibility, gameplay formulas, save format, and existing preference precedence are retained by 02A; later changes are recorded separately.

## What changed

- Added [ContentSettingsData](../../Assets/Scripts/Core/ContentSettingsData.cs), a static data loader taking an explicit WorldConfig. It owns the 105 existing boolean preference defaults, 89 scalar assignments, legacy mercenary defaults and spawner preference loading without constructing or reading menu controls.
- [ContentSettings](../../Assets/Scripts/Subwindows/ContentSettings.cs) keeps its UI bindings and delegates loading to the service. ToggleObject is now a widget/key binding; it no longer owns a default value. Dynamic mercenary controls retain the same keys, sorting and persistence. The legacy spawner subset is shared between UI construction and loading, rather than inferred from instantiated panels.
- No WorldConfig fields or serialization names changed. No existing options/pages, species or mechanics were retired. BuildingSettings, menu application/saving, State.Load, cheats and Race Editor data slots remain on their existing paths until the relevant packets.

## Current precedence retained by this packet

| Entry/context | Current authority | What 02A does |
|---|---|---|
| New WorldConfig before preferences | Field initializers; raw toggle GetValue fallback/ResetDictionary | Leaves these unchanged; they are not all the menu defaults. |
| StartMode.Start and ReturnToStart | ContentSettings.Refresh → building hard load, control setup, ContentSettingsData.LoadSaved | Same entry points and preference keys; data loading itself no longer needs widgets. |
| Missing saved content preference | Loader's literal default; spawner values sometimes fall back to that world's current spawner data | Preserves exactly the former Refresh behavior. |
| Existing PlayerPrefs key | Stored value overrides that content-setting fallback | Preserved; no preference deletion/reset/migration. |
| Opening Content Settings | Current Config.World displayed in widgets | No global reload merely from opening. |
| Exit / Escape | Widget values applied to running Config.World; building soft save | Preserved; this is not Cancel. |
| Exit and Save | Widget values applied and saved as global defaults; building hard/soft save | Preserved. |
| Load Saved confirmation | Explicit Refresh reloads global defaults into current configuration, then displays them | Preserved; automatic save loading does not silently call this. |
| State.Load | Saved World.ConfigStorage replaces Config.World; old migration/options/cheat paths still apply | Preserved pending explicit legacy/new-format and lock changes. |
| Race Editor preset switch | External RaceSettings data slot | Not part of this extraction; no change to species/stat definitions. |

**Balance locking is not active yet.** This packet establishes the data boundary and baseline needed for later retirement; it does not claim old PlayerPrefs, save data, or still-visible controls are already prevented from changing balance. Packet 02C must enforce the approved code-owned values on every applicable read/apply/load route while preserving allowed global content/cosmetic preferences. Packet 02B closes retired tools and gates Race Editor first. New campaign saves later use the explicit format/ownership boundary in spec 06.

## Existing discrepancies intentionally preserved

- DayNightEnabled has loader default **false**, but the raw WorldConfig.ResetDictionary fallback is **true**. Neither is silently reconciled here.
- Thirty-five menu toggle keys are absent from the raw fallback dictionary; GetValue normally creates missing keys as false except its ClothingDiscards special case. Some loader defaults are true. The table below makes the distinction explicit.
- Some numeric controls have SaveValues writes without a matching old Refresh read. Their existing WorldConfig initializer/other load behavior remains unchanged. The 89 assignments below describe exactly the extracted block, not all WorldConfig fields.
- Spawner day/night preference loading retains the old conquest-type guard. Correcting unrelated legacy behavior belongs in a separately scoped change.
- Legacy species defaults still allow nonnegative Merc enum entries and use the old spawner subset. Curated eligibility and zero-species behavior belong to 03, not this packet.

## Extracted toggle defaults

“Raw fallback absent” means ResetDictionary does not contain that key; it does not mean a default was invented in 02A.

| Preference key | Loader default | Raw ResetDictionary entry |
|---|---|---|
| `WeightGain` | `true` | `true` |
| `WeightLoss` | `true` | absent |
| `FurryHandsAndFeet` | `true` | `true` |
| `FurryFluff` | `true` | `true` |
| `FriendlyRegurgitation` | `true` | `true` |
| `HairMatchesFur` | `false` | `false` |
| `MaleHairForFemales` | `true` | `true` |
| `FemaleHairForMales` | `true` | `true` |
| `HermsOnlyUseFemaleHair` | `false` | absent |
| `HideBreasts` | `false` | `false` |
| `HideCocks` | `false` | `false` |
| `RaceTraitsEnabled` | `true` | `true` |
| `RagsForSlaves` | `true` | `true` |
| `VisibleCorpses` | `true` | `true` |
| `EdibleCorpses` | `false` | `false` |
| `FogOfWar` | `false` | `false` |
| `LeadersUseCustomizations` | `false` | `false` |
| `HermsCanUB` | `false` | `false` |
| `Unbirth` | `false` | `false` |
| `CockVore` | `false` | `false` |
| `BreastVore` | `false` | `false` |
| `AnalVore` | `false` | absent |
| `TailVore` | `false` | `false` |
| `BladderVore` | `false` | `false` |
| `CockVoreHidesClothes` | `false` | `false` |
| `AltVoreOralGain` | `false` | absent |
| `AllowHugeBreasts` | `false` | `false` |
| `AllowHugeDicks` | `false` | `false` |
| `AllowTopless` | `false` | `false` |
| `FurryGenitals` | `false` | `false` |
| `LamiaUseTailAsSecondBelly` | `false` | `false` |
| `AnimatedBellies` | `true` | `true` |
| `DigestionSkulls` | `true` | `true` |
| `BellyRubHands` | `true` | `true` |
| `SurrenderFlag` | `true` | `true` |
| `ShowUnitSides` | `true` | `true` |
| `Bones` | `true` | `true` |
| `CleanDisposal` | `false` | `false` |
| `Scat` | `false` | `false` |
| `ScatV2` | `false` | absent |
| `BirdScat` | `false` | `false` |
| `ScatBones` | `false` | `false` |
| `CondomsForCV` | `false` | `false` |
| `ErectionsFromVore` | `false` | `false` |
| `ErectionsFromCockVore` | `false` | `false` |
| `AutoSurrender` | `false` | `false` |
| `EatSurrenderedAllies` | `false` | `false` |
| `FlatExperience` | `false` | `false` |
| `BoostedAccuracy` | `false` | `false` |
| `ClothingDiscards` | `true` | `true` |
| `ExtraRandomHairColors` | `false` | absent |
| `GoblinCaravans` | `true` | absent |
| `Diplomacy` | `false` | absent |
| `LizardsHaveNoBreasts` | `false` | absent |
| `LewdDialog` | `false` | absent |
| `KuroTenkoEnabled` | `false` | `false` |
| `OverhealEXP` | `true` | `true` |
| `TransferAllowed` | `true` | `true` |
| `CumGestation` | `true` | `true` |
| `HardVoreDialog` | `false` | absent |
| `NoAIRetreat` | `false` | absent |
| `AICanHireSpecialMercs` | `false` | absent |
| `AICanCheatSpecialMercs` | `false` | absent |
| `DisorientedPrey` | `true` | absent |
| `MonstersDropSpells` | `true` | absent |
| `AllowInfighting` | `false` | absent |
| `EventsRepeat` | `false` | absent |
| `SurrenderedCanConvert` | `false` | absent |
| `LockedAIRelations` | `false` | absent |
| `Defections` | `true` | absent |
| `RaceSizeLimitsWeightGain` | `false` | absent |
| `MultiRaceVillages` | `false` | absent |
| `RaceSpecificVoreGraphicsDisabled` | `false` | absent |
| `MonstersCanReform` | `true` | absent |
| `MultiRaceFlip` | `false` | absent |
| `AdventurersDisabled` | `false` | absent |
| `MercenariesDisabled` | `false` | absent |
| `TroopScatter` | `false` | absent |
| `AlwaysRandomizeConverted` | `false` | absent |
| `SpecialMercsCanConvert` | `false` | absent |
| `LeadersRerandomizeOnDeath` | `false` | absent |
| `NoScatForDeadTransfers` | `false` | absent |
| `HideViperSlit` | `false` | `false` |
| `Cumstains` | `true` | absent |
| `BurpOnDigest` | `false` | `false` |
| `FartOnAbsorb` | `false` | `false` |
| `CanUseStomachRubOnEnemies` | `false` | absent |
| `DayNightEnabled` | `false` | `true` |
| `DayNightCosmetic` | `false` | `false` |
| `DayNightSchedule` | `true` | `true` |
| `DayNightRandom` | `true` | `true` |
| `NightMonsters` | `false` | `false` |
| `NightMoveMonsters` | `false` | `false` |
| `CombatComplicationsEnabled` | `false` | `false` |
| `StatCrit` | `false` | `false` |
| `StatGraze` | `false` | `false` |
| `StatBoostsAffectMaxHP` | `false` | `false` |
| `OverfeedingDamage` | `false` | `false` |
| `DigestionDamageDivision` | `false` | `false` |
| `AbsorbRateDivision` | `false` | `false` |
| `AbsorbLoss` | `false` | `false` |
| `AbsorbBoostDeadOnly` | `false` | `false` |
| `SizeAccuracyInverse` | `true` | `true` |
| `SizeDamageInverse` | `true` | `true` |
| `PotionSystemEnabled` | `false` | `false` |

## Extracted scalar assignments

Values, casts, spelling, preference keys and missing-key defaults are retained from Refresh. These statements show the new explicit target object; the old target was Config.World. Trait text still uses RaceEditorPanel's static parsing helper, which requires no editor instance or widget.

```csharp
world.MaleFraction = PlayerPrefs.GetFloat("MaleFraction", .5f);
world.HermFraction = PlayerPrefs.GetFloat("HermFraction", 0);
world.HermNameFraction = PlayerPrefs.GetFloat("HermNameFraction", .66f);
world.ClothedFraction = PlayerPrefs.GetFloat("ClothedFraction", .85f);
world.FurryFraction = PlayerPrefs.GetFloat("FurryFraction", .5f);
world.WeightLossFractionBreasts = PlayerPrefs.GetFloat("WeightLossFractionBreasts", .2f);
world.WeightLossFractionBody = PlayerPrefs.GetFloat("WeightLossFractionBody", .2f);
world.WeightLossFractionDick = PlayerPrefs.GetFloat("WeightLossFractionDick", .2f);
world.GrowthCap = PlayerPrefs.GetFloat("GrowthCap", 5f);
world.GrowthMod = PlayerPrefs.GetFloat("GrowthMod", 1f);
world.GrowthDecayOffset = PlayerPrefs.GetFloat("GrowthDecayOffset", 0);
world.GrowthDecayIncreaseRate = PlayerPrefs.GetFloat("GrowthDecayIncreaseRate", 0.04f);
world.TacticalTerrainFrequency = PlayerPrefs.GetFloat("TacticalTerrainFrequency", 10f);
world.TacticalWaterValue = PlayerPrefs.GetFloat("TacticalWaterValue", .29f);
world.BreastSizeModifier = PlayerPrefs.GetInt("BreastSizeModifier", 0);
world.CockSizeModifier = PlayerPrefs.GetInt("CockSizeModifier", 0);
world.DefaultStartingWeight = PlayerPrefs.GetInt("StartingWeight", 2);
world.AutoSurrenderChance = PlayerPrefs.GetFloat("AutoSurrenderChance", 1);
world.AutoSurrenderDefectChance = PlayerPrefs.GetFloat("AutoSurrenderDefectChance", 0.25f);
world.OralWeight = PlayerPrefs.GetInt("OralWeight", 40);
world.BreastWeight = PlayerPrefs.GetInt("BreastWeight", 40);
world.AnalWeight = PlayerPrefs.GetInt("AnalWeight", 40);
world.UnbirthWeight = PlayerPrefs.GetInt("UnbirthWeight", 40);
world.CockWeight = PlayerPrefs.GetInt("CockWeight", 40);
world.TailWeight = PlayerPrefs.GetInt("TailWeight", 40);
world.BladderWeight = PlayerPrefs.GetInt("BladderWeight", 40);
world.MonsterConquest = (Config.MonsterConquestType)PlayerPrefs.GetInt("MonsterConquest", 0);
world.BurpFraction = PlayerPrefs.GetFloat("BurpFraction", .1f);
world.FartFraction = PlayerPrefs.GetFloat("FartFraction", .1f);
world.WeightGainFraction = PlayerPrefs.GetFloat("WeightGainFraction", .5f);
world.DigestionSpeedMult = PlayerPrefs.GetFloat("DigestionSpeedMult", 1);
world.AbsorbSpeedMult = PlayerPrefs.GetFloat("AbsorbSpeedMult", 1);
world.AbsorbResourceMod = PlayerPrefs.GetFloat("AbsorbResourceMod", 1);
world.BellyRubEffMult = PlayerPrefs.GetFloat("BellyRubEffMult", 1);
world.BellyRubsPerTurn = PlayerPrefs.GetInt("BellyRubsPerTurn", 1);
world.DigestionRamp = PlayerPrefs.GetFloat("DigestionRamp", 0);
world.DigestionRampTurn = PlayerPrefs.GetInt("DigestionRampTurn", 1);
world.DigestionRampCap = PlayerPrefs.GetInt("DigestionRampCap", -1);
world.DigestionRampLoss = PlayerPrefs.GetFloat("DigestionRampLoss", 1);
world.AbsorbResourceModBoost = PlayerPrefs.GetInt("AbsorbResourceModBoost", 0);
world.AbsorbRamp = PlayerPrefs.GetFloat("AbsorbRamp", 0);
world.DigestionCap = PlayerPrefs.GetFloat("DigestionCap", 0);
world.DigestionFlatDmg = PlayerPrefs.GetFloat("DigestionFlatDmg", 0);
world.DigestionGraceTurns = PlayerPrefs.GetInt("DigestionGraceTurns", 0);
world.SurrenderedPredEscapeMult = PlayerPrefs.GetFloat("SurrenderedPredEscapeMult", 1);
world.SurrenderedPredAutoRegur = PlayerPrefs.GetFloat("SurrenderedPredAutoRegur", 0);
world.ArmyMP = PlayerPrefs.GetInt("ArmyMP", 3);
world.ScoutMP = PlayerPrefs.GetInt("ScoutMP", 3);
world.ScoutMax = PlayerPrefs.GetInt("ScoutMax", 4);
world.ArmyCreationMPMod = PlayerPrefs.GetFloat("ArmyCreationMPMod", 0);
world.ArmyCreationMPCurve = PlayerPrefs.GetFloat("ArmyCreationMPCurve", 1f);
world.CustomEventFrequency = PlayerPrefs.GetFloat("CustomEventFrequency", .25f);
world.MaxArmies = PlayerPrefs.GetInt("MaxArmies", 32);
world.MonsterConquestTurns = PlayerPrefs.GetInt("MonsterConquestTurns", 1);
world.MalesLike = (Orientation)PlayerPrefs.GetInt("MalesLike", 0);
world.FemalesLike = (Orientation)PlayerPrefs.GetInt("FemalesLike", 0);
world.WinterStuff = (Config.SeasonalType)PlayerPrefs.GetInt("WinterStuff", 0);
world.VoreRate = PlayerPrefs.GetInt("VoreRate", 0);
world.FairyBVType = (FairyBVType)PlayerPrefs.GetInt("FairyBVType", 0);
world.FeedingType = (FeedingType)PlayerPrefs.GetInt("FeedingType", 0);
world.FourthWallBreakType = (FourthWallBreakType)PlayerPrefs.GetInt("FourthWallBreakType", 0);
world.UBConversion = (UBConversion)PlayerPrefs.GetInt("UBConversion", 0);
world.GoddessMercy = (GoddessMercy)PlayerPrefs.GetInt("GoddessMercy", 0);
world.SucklingPermission = (SucklingPermission)PlayerPrefs.GetInt("SucklingPermission", 0);
world.EscapeRate = PlayerPrefs.GetInt("EscapeRate", 0);
world.FogDistance = PlayerPrefs.GetInt("FogDistance", 0);
world.RandomEventRate = PlayerPrefs.GetInt("RandomEventRate", 0);
world.RandomAIEventRate = PlayerPrefs.GetInt("RandomAIEventRate", 0);
world.DiplomacyScale = (DiplomacyScale)PlayerPrefs.GetInt("DiplomacyScale", 0);
world.MaxSpellLevelDrop = PlayerPrefs.GetInt("MaxSpellLevelDrop", 4);
world.MaxEquipmentLevelDrop = PlayerPrefs.GetInt("MaxEquipmentLevelDrop", 4);
world.LeaderTraits = RaceEditorPanel.TextToTraitList(PlayerPrefs.GetString("LeaderTraits", ""));
world.MaleTraits = RaceEditorPanel.TextToTraitList(PlayerPrefs.GetString("MaleTraits", ""));
world.FemaleTraits = RaceEditorPanel.TextToTraitList(PlayerPrefs.GetString("FemaleTraits", ""));
world.HermTraits = RaceEditorPanel.TextToTraitList(PlayerPrefs.GetString("HermTraits", ""));
world.SpawnTraits = RaceEditorPanel.TextToTraitList(PlayerPrefs.GetString("SpawnTraits", ""));
world.OverallMonsterCapModifier = PlayerPrefs.GetFloat("OverallMonsterCapModifier", 1);
world.OverallMonsterSpawnRateModifier = PlayerPrefs.GetFloat("OverallMonsterSpawnRateModifier", 1);
world.RevealTurn = PlayerPrefs.GetInt("RevealTurn", 50);
world.TacticalMovementSoftCap = PlayerPrefs.GetInt("TacticalMovementSoftCap", -1);
world.TacticalMovementHardCap = PlayerPrefs.GetInt("TacticalMovementHardCap", -1);
world.SizeAccuracyMod = PlayerPrefs.GetFloat("SizeAccuracyMod", 0.01f);
world.SizeAccuracyLowerBound = PlayerPrefs.GetFloat("SizeAccuracyLowerBound", 10);
world.SizeAccuracyInterval = PlayerPrefs.GetInt("SizeAccuracyInterval", 5);
world.SizeAccuracyCap = PlayerPrefs.GetFloat("SizeAccuracyCap", -1);
world.SizeDamageMod = PlayerPrefs.GetFloat("SizeDamageMod", 0.01f);
world.SizeDamageLowerBound = PlayerPrefs.GetFloat("SizeDamageLowerBound", 10);
world.SizeDamageInterval = PlayerPrefs.GetInt("SizeDamageInterval", 5);
world.SizeDamageCap = PlayerPrefs.GetFloat("SizeDamageCap", -1);
```

## Spawner and mercenary defaults

Each nonnegative Race keeps the `Merc {race}` preference key and missing-key value true. This is the existing setup behavior, not approved future curation.

For each species in the shared legacy spawner subset, loading retains: Enabled false; Team 900 + numeric race; Add-On true; Monster Scout MP false; Conquest Type 0; D/N Move Type 0. Max Armies, Spawn Rate, Scale Factor, Attempts, Confidence, Min/Max Army Size and Turn Order fall back to the world's existing GetSpawner result, just as before. WorldConfig.SpecifySpawnerDefaults remains responsible for the species-specific baseline. No live armies are removed by the extracted loader; the old UI apply side effects remain pending their separate retirement.

## Validation and remaining player checks

- Source-move review: all 105 literal boolean values, all 89 scalar statements and all toggle binding counts match the original source. Preference keys and spawner construction/guards were reviewed against the original method.
- Unity 2018.3.12f1 compilation passed. The restricted launch stopped at licensing; the normal-access import initially attempted compilation before the new script was registered, then compiled successfully. A fresh confirmation launch compiled Assembly-CSharp and Assembly-CSharp-Editor with no C# errors and ended with return code 0. Evidence: ignored `Logs/packet-02a-compile-confirm.log`; earlier attempt logs remain under the same directory.
- After each import, restored the two Unity-removed orphan metadata files byte-for-byte (`Assets/Scripts/Core/obj.meta` and `Assets/Scripts/Core/bin/Debug/net10.0.meta`). Final asset scope is the modified component plus the added data loader and its metadata; no scene, prefab, package or project-settings diff.
- No automated test framework or tests added. No new player build, settings playtest, save/load round-trip or preference reset performed.
- User confirmed Show Unit Sides persisted between exiting and coming back, then explicitly authorized proceeding to 02B. Broader settings/save coverage remains unverified; no reset of options/user data was performed by the agent.

## GPL/source-change check

- `Assets/Scripts/Subwindows/ContentSettings.cs` has a dated fork modification notice; its pre-existing source had no copyright/license header to remove.
- `Assets/Scripts/Core/ContentSettingsData.cs` is marked as a fork addition with the actual date and GPL license/change-record references. Its moved logic is identified as extracted upstream logic, not claimed as wholly original authorship.
- `Assets/Scripts/Core/ContentSettingsData.cs.meta` is new Unity metadata; its exact path and date are associated in root CHANGES.md rather than inserting invalid comment syntax.
- License.txt and upstream attribution/notices remain unchanged. No dependency, copied external code, or new asset license was introduced. This is the per-change check, not a full audit of inherited third-party content or permission to distribute.
