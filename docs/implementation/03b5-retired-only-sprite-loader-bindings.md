# 03B5 — Retired-only sprite-loader bindings

Date: 2026-11-01. User accepted 03B4 startup/setup/Race Editor checks and authorized the next packet.

## Completed source scope

Only `Assets/Scripts/Core/AssetBundleLoader.cs` changed. Its 355 direct sprite-array loads were reconciled against C# consumers and the 91 disconnected graphics classes (90 rejected species plus SlimeQueen). Of these, 156 bindings have code readers exclusively in those retired definition files. Replace those exact load expressions with explicit null assignments. The fields remain declared so disconnected legacy source compiles; no empty sprite arrays, alternate species or stale previous bindings are substituted. Explicit null clearing handles repeat invocation as well as fresh startup.

The other 199 direct load expressions are unchanged, including the 33 bindings shared between rejected source and any other code. All four mixed AssetBundle.LoadFromFile calls remain identical. Unknown/shared/other consumers are conservatively kept even if their owning UI or tactical branch is dormant. SpriteDictionary field declarations and all artwork, manifests, bundles, .meta files, scenes and prefabs are untouched.

This is a code-reader boundary, not asset exclusivity. A retained field can request the same sheet, a prefab can still refer to its GUID, or a mixed bundle can contain it. No promise of memory/build-size reduction, no physical content removal and no bundle repacking. The earlier 383-sheet manifest inventory counts assets, while this packet's 355 count is loader field assignments; they are different measures.

## Reader and shared-resource review

- The 91 retired file paths were reconciled from the exact registry types removed by 03B4. No nonretired subclass or remaining direct construction of those graphics types was found there; their instantiated registry path is closed.
- Scan all compiled C# sources for SpriteDictionary member consumers, excluding the declaration file and loader writes when classifying readers. No SpriteDictionary using-static/type alias or named reflection lookup was found. Additional bare-symbol/static-type searches identified ordinary enum/class names, disabled Bestiary GameObject fields and flavor strings, not an additional sprite-binding reader. This is source evidence, not proof against arbitrary reflection or all binary/Odin payloads.
- All 156 selected targets are public static Sprite arrays, so they are not Unity-serialized instance fields. Their retained retired-class readers are unreachable through the normal species registry; direct fabricated retired objects remain unsupported. Existing nonretired/player-disabled individuals keep their shared/retained bindings.
- Shared generic clothing helpers, tactical effects, retained species and serialized resources remain possible sheet consumers. Keep these conservatively. Source searches do not establish sheet ownership or artwork redistribution rights.

## Cleared bindings

| SpriteDictionary field | Mixed bundle | Asset request no longer made by this binding |
|---|---|---|
| Mermen2 | empire | `Mermen2` |
| NewimpVore | empire | `impfinishedvore` |
| NewimpGloves | empire | `impgloves` |
| NewimpLegs | empire | `implegg` |
| NewimpUBottoms | empire | `impunderbottoms` |
| NewimpUTops | empire | `impundertops` |
| NewimpOBottoms | empire | `impoverbottoms` |
| NewImpOTops | empire | `impovertops` |
| NewimpOnePieces | empire | `impunderonepieces` |
| NewImpOverOnePieces | empire | `impoveronepiece` |
| NewimpHats | empire | `imphood` |
| Gobbo | monster | `Gobbo` |
| Gobbovore | monster | `Gobbovore` |
| Gobbglove | monster | `Gobbglove` |
| Gobleggo | monster | `Gobleggo` |
| Gobbunderbottoms | monster | `Gobbunderbottoms` |
| Gobundertops | monster | `Gobundertops` |
| Gobboverbottoms | monster | `Gobboverbottoms` |
| Gobbovertops | monster | `Gobbovertops` |
| Gobbunderonepieces | monster | `Gobbunderonepieces` |
| Gobboveronepieces | monster | `Gobboveronepieces` |
| Gobbohat | monster | `Gobbohat` |
| HumenUniform1 | empire | `Hu_men_uniform_1` |
| HumenUniform2 | empire | `Hu_men_uniform_2` |
| HumenBigLoin | empire | `hu_men_bigloin` |
| HumenPants | empire | `hu_men_pants` |
| HumenSkirt | empire | `hu_men_skirt` |
| AlrauneChristmas | empire | `alraunechristmas` |
| ImpGobHat | empire | `ImpGobHat` |
| SlimeQueen | empire | `SlimeQueen` |
| Lamia | empire | `Lamia` |
| LamiaScales | empire | `LamiaNaga` |
| LamiaScalesBits | empire | `LamiaNagaBits` |
| Cows | empire | `Cows` |
| Driders | empire | `Drider` |
| Alraune | empire | `Alraune` |
| Mermen | empire | `Mermen` |
| BearsClothes | empire | `BearsClothes` |
| Eevee | merc | `AnthroEevee` |
| Umbreon | empire | `AnthroUmbreon` |
| Umbreon2 | empire | `AnthroUmbreon2` |
| Umbreon3 | empire | `AnthroUmbreon3` |
| Puca | merc | `Puca` |
| DewSprite | merc | `dew` |
| Vipers2 | merc | `Vipers2` |
| Vipers3 | merc | `Vipers3` |
| Vipers4 | merc | `Vipers4` |
| Cockatrice1 | merc | `Cockatrice1` |
| Hamsters | merc | `Hamsters` |
| RwuMercenaries1 | merc | `RwuMercenariesStandard` |
| RwuMercenaries2 | merc | `RwuMercenariesStandardArmor1` |
| RwuMercenaries3 | merc | `RwuMercenariesLizard` |
| RwuMercenaries4 | merc | `RwuMercenariesLizardArmor1` |
| RwuMercenaries5 | merc | `RwuMercenariesMice` |
| RwuMercenaries6 | merc | `RwuMercenariesMiceArmor1` |
| RwuMercenaries7 | merc | `RwuMercenariesShark` |
| RwuMercenaries8a | merc | `RwuMercenariesSharkArmor1` |
| RwuMercenaries8b | merc | `RwuMercenariesSharkArmor2` |
| RenamonBodies | merc | `RenamonBodies` |
| RenamonClothes1 | merc | `RenamonClothes1` |
| RenamonClothes2 | merc | `RenamonClothes2` |
| RenamonClothes3 | merc | `RenamonClothes3` |
| RenamonVore | merc | `RenamonVore` |
| OoviKatFemale | merc | `OoviKatFemale` |
| OoviKatMale | merc | `OoviKatMale` |
| OoviKatCustomisation | merc | `OoviKatCustom` |
| CowsSeliciaBelly | empire | `CowsSeliciaBelly` |
| YoukoTails | empire | `YoukoTails` |
| Aabayx | empire | `Aabayx` |
| AabayxFacePaint | empire | `AabayxFacePaint` |
| CentaurTorsoAddOns | merc | `CentaurTorsoAddOns` |
| MainlandElfParts | empire | `MainlandElves` |
| MainlandElfClothes | empire | `elven_armor` |
| Ghosts1 | empire | `ghosts1` |
| Ghosts2 | empire | `ghosts2` |
| Ghosts3 | empire | `ghosts3` |
| GhostsVoreSprites | empire | `ghostvorebits` |
| YordlesBodies | empire | `YordleBodies` |
| YordlesVoreSkin | empire | `YordleVoreSkin` |
| YordlesVoreFur | empire | `YordleVoreFur` |
| YordlesCustomization1 | empire | `YordleCustomization1` |
| YordleClothes1 | empire | `YordleClothes1` |
| YordleClothes2 | empire | `YordleClothes2` |
| Selicia | specialmerc | `Selicia` |
| Vision | specialmerc | `Vision` |
| Ki | specialmerc | `Ki` |
| Scorch | specialmerc | `Scorch` |
| AsuraHoliday | specialmerc | `AsuraHoliday` |
| DRACO | specialmerc | `DRACO` |
| Zoey | specialmerc | `Zoey` |
| ZoeyHoliday | specialmerc | `zoey_addsheet` |
| Cierihaka | specialmerc | `Cierihaka` |
| Zera240 | specialmerc | `Zera240` |
| ZeraBelly | specialmerc | `zerabel` |
| Auri | specialmerc | `Auri` |
| AuriHoliday | specialmerc | `AuriXmasAddSheet` |
| AuriVore | specialmerc | `AuriVore` |
| Erin | specialmerc | `Erin` |
| Salix | specialmerc | `Salix` |
| SalixVore | specialmerc | `SalixVore` |
| SalixGen | specialmerc | `SalixGen` |
| BellaBody1 | specialmerc | `BellaBody1` |
| BellaArm1 | specialmerc | `BellaArm1` |
| BellaHead1 | specialmerc | `BellaHead1` |
| BellaBody2 | specialmerc | `BellaBody2` |
| BellaArm2 | specialmerc | `BellaArm2` |
| BellaHead2 | specialmerc | `BellaHead2` |
| BellaRobe1 | specialmerc | `BellaRobes1` |
| BellaRobe2 | specialmerc | `BellaRobes2` |
| Singularity1 | specialmerc | `Singularity1` |
| Singularity2 | specialmerc | `Singularity2` |
| Feit | specialmerc | `Feit` |
| Taraluxia | specialmerc | `Taraluxia` |
| TaraTailStrikes | specialmerc | `TaraTailStrikes` |
| Xelhilde | specialmerc | `Xelhilde1` |
| Olivia | specialmerc | `Olivia` |
| OliviaVoreSprites | specialmerc | `OliviaVoreSprites` |
| Skapa | specialmerc | `Skapa` |
| TatltuaeHackleVore | specialmerc | `TatltuaeHackleVore` |
| Seville | specialmerc | `Seville` |
| Nectar | specialmerc | `Nectar` |
| Ryan | specialmerc | `Ryan` |
| RyanVore | specialmerc | `RyanVore` |
| Konane | specialmerc | `Konane` |
| Cherub | specialmerc | `Cherub` |
| Seraph | specialmerc | `Seraph` |
| Abakhanskya | specialmerc | `Abakhanskya` |
| Vagrants | monster | `Vagrants` |
| Vagrants2 | monster | `Vagrants2` |
| Vagrants3 | monster | `Vagrants3` |
| Compy | monster | `Compy` |
| Shark | monster | `SkyShark` |
| DarkSwallower | monster | `DarkSwallower` |
| Collector | monster | `Collector` |
| Voilin | monster | `Voilin` |
| Frogs | monster | `Frog` |
| Fairy | monster | `fairySmall` |
| FairyExtraHair | monster | `fairy_addsheet` |
| Fairy240 | monster | `fairy240` |
| Raptor | monster | `RaptorSprites` |
| Earthworms | monster | `Earthworms` |
| Schiwardez | monster | `Schiwardez` |
| Whisp | monster | `Whisp` |
| FeralOrcas | monster | `Feral_Orcas` |
| BoomBunnies | monster | `Boom_Bunnies` |
| ViraeUltimae | monster | `ViraeUltimae` |
| Viisels | monster | `Viisels` |
| FeralEevee | monster | `FeralEevee` |
| FeralEqualeon | monster | `FeralEqualeon` |
| Iliijiith | monster | `Iliijiith` |
| Pudding | monster | `Pudding` |
| DryadSprites1 | monster | `Dryad1` |
| DryadSprites2 | monster | `Dryad2` |
| DryadSprites4 | monster | `Dryad4` |
| Cake | monster | `Cake` |
| SoulSprite | monster | `SoulSprite` |

## Shared bindings preserved

These bindings have rejected-definition readers AND other source readers. This table does not assert that every listed other reader is active; it is intentionally conservative.

| Field | Other source readers retained |
|---|---|
| HumanRags | `Assets/Scripts/Races/Graphics/MainRaces/DemiDragons.cs` |
| PantherHats | `Assets/Scripts/Races/Graphics/MainRaces/Panthers.cs` |
| NewimpBase | `Assets/Scripts/Modes/Tactical/TacticalGraphicalEffects.cs` |
| HumenFundertops | `Assets/Scripts/Races/Graphics/MainRaces/DemiDragons.cs`, `Assets/Scripts/Races/Graphics/MainRaces/Jackals.cs` |
| HumenMundertops | `Assets/Scripts/Races/Graphics/MainRaces/DemiDragons.cs`, `Assets/Scripts/Races/Graphics/MainRaces/Jackals.cs` |
| HumenFOnePieces | `Assets/Scripts/Races/Graphics/MainRaces/Jackals.cs` |
| HumenUnderbottoms | `Assets/Scripts/Races/Graphics/MainRaces/DemiDragons.cs`, `Assets/Scripts/Races/Graphics/MainRaces/Jackals.cs` |
| Slimes | `Assets/Scripts/Modes/Tactical/TacticalGraphicalEffects.cs` |
| Scylla | `Assets/Scripts/Modes/Tactical/TacticalGraphicalEffects.cs` |
| Harpies | `Assets/Scripts/Modes/Tactical/TacticalGraphicalEffects.cs` |
| Crypters | `Assets/Scripts/Modes/Tactical/TacticalGraphicalEffects.cs` |
| Avians3 | `Assets/Scripts/Races/Graphics/MainRaces/Avians.cs`, `Assets/Scripts/Races/Graphics/MainRaces/Bees.cs`, `Assets/Scripts/Races/Graphics/MainRaces/Deer.cs`, `Assets/Scripts/Races/Graphics/MainRaces/Demiants.cs`, `Assets/Scripts/Races/Graphics/MainRaces/Demibats.cs`, `Assets/Scripts/Races/Graphics/MainRaces/DemiDragons.cs`, `Assets/Scripts/Races/Graphics/MainRaces/Demifrogs.cs`, `Assets/Scripts/Races/Graphics/MainRaces/Demisharks.cs`, `Assets/Scripts/Races/Graphics/MainRaces/Gnolls.cs`, `Assets/Scripts/Races/Graphics/MainRaces/Lupine.cs`, `Assets/Scripts/Races/Graphics/MainRaces/Mice.cs` |
| Avians4 | `Assets/Scripts/Races/Graphics/MainRaces/Avians.cs`, `Assets/Scripts/Races/Graphics/MainRaces/Bees.cs`, `Assets/Scripts/Races/Graphics/MainRaces/Deer.cs`, `Assets/Scripts/Races/Graphics/MainRaces/Demiants.cs`, `Assets/Scripts/Races/Graphics/MainRaces/Demibats.cs`, `Assets/Scripts/Races/Graphics/MainRaces/DemiDragons.cs`, `Assets/Scripts/Races/Graphics/MainRaces/Demifrogs.cs`, `Assets/Scripts/Races/Graphics/MainRaces/Demisharks.cs`, `Assets/Scripts/Races/Graphics/MainRaces/Gnolls.cs`, `Assets/Scripts/Races/Graphics/MainRaces/Lupine.cs`, `Assets/Scripts/Races/Graphics/MainRaces/Mice.cs` |
| Sharks3 | `Assets/Scripts/Races/Graphics/MainRaces/Demisharks.cs` |
| Sharks5 | `Assets/Scripts/Races/Graphics/MainRaces/Deer.cs`, `Assets/Scripts/Races/Graphics/MainRaces/Demisharks.cs`, `Assets/Scripts/Races/Graphics/MainRaces/Mice.cs` |
| Bears | `Assets/Scripts/Modes/Tactical/TacticalGraphicalEffects.cs` |
| Vipers1 | `Assets/Scripts/Modes/Tactical/TacticalGraphicalEffects.cs` |
| Cockatrice2 | `Assets/Scripts/Races/Graphics/MainRaces/Deer.cs` |
| Cockatrice3 | `Assets/Scripts/Races/Graphics/MainRaces/Deer.cs`, `Assets/Scripts/Races/Graphics/MainRaces/Gnolls.cs`, `Assets/Scripts/Races/Graphics/MainRaces/Mice.cs` |
| Equaleon | `Assets/Scripts/Modes/Tactical/TacticalGraphicalEffects.cs` |
| HumansBodySprites1 | `Assets/Scripts/Races/Graphics/MainRaces/DemiDragons.cs`, `Assets/Scripts/Races/Graphics/MainRaces/Jackals.cs` |
| HumansBodySprites2 | `Assets/Scripts/Races/Graphics/TaurHumanHalf.cs`, `Assets/Scripts/Races/Graphics/MainRaces/DemiDragons.cs`, `Assets/Scripts/Races/Graphics/MainRaces/Jackals.cs` |
| HumansBodySprites3 | `Assets/Scripts/Races/Graphics/TaurHumanHalf.cs`, `Assets/Scripts/Races/Graphics/MainRaces/DemiDragons.cs`, `Assets/Scripts/Races/Graphics/MainRaces/Jackals.cs`, `Assets/Scripts/Races/Graphics/MainRaces/Mice.cs` |
| HumansBodySprites4 | `Assets/Scripts/Races/Graphics/MainRaces/DemiDragons.cs`, `Assets/Scripts/Races/Graphics/MainRaces/Jackals.cs` |
| HumansVoreSprites | `Assets/Scripts/Races/Graphics/TaurHumanHalf.cs`, `Assets/Scripts/Races/Graphics/MainRaces/DemiDragons.cs`, `Assets/Scripts/Races/Graphics/MainRaces/Jackals.cs` |
| AabayxLeader | `Assets/Scripts/Entities/Clothing/RaceSpecificClothing.cs` |
| CentaurParts | `Assets/Scripts/Races/Graphics/TaurHumanHalf.cs` |
| TaurTorso | `Assets/Scripts/Races/Graphics/TaurHumanHalf.cs` |
| YordlesCustomization2 | `Assets/Scripts/Modes/Tactical/TacticalGraphicalEffects.cs` |
| Asura | `Assets/Scripts/Races/Graphics/MainRaces/Sergal.cs`, `Assets/Scripts/Entities/Clothing/MainAccessories.cs` |
| Tatltuae | `Assets/Scripts/Entities/Animations/EntropicChaos.cs` |
| DryadSprites3 | `Assets/Scripts/Modes/Tactical/TacticalGraphicalEffects.cs` |
| DryadSprites5 | `Assets/Scripts/Modes/Tactical/TacticalGraphicalEffects.cs` |

## Verification

Unity compilation PASSED exit 0 (Logs/packet-03b5-compile.log), no C# errors/logged exceptions or new compiler warnings versus 03B4. Explicit Assets/base.unity loading PASSED exit 0 (Logs/packet-03b5-scene.log), without errors/exceptions. Exact final-source reconstruction from 156 edits, untouched 199 requests/four bundle opens, source BOM/line endings, unchanged scene bytes, 1248 unchanged resource/serialized input hashes and scoped diff whitespace checks passed. No Unity process remains; previous uncommitted work is preserved. Ignored Logs/packet-03b5-loader-inventory.json records the full reader classification and 91 file paths; source-review.json records exact edits/expected final source, before/diff and base.unity snapshots retained. No new tests/framework or player build. Actual battles/rendering of curated units remain blocked by the deliberately empty approved registry.

One focused manual check after agent verification: startup reaches the main menu without new red errors. No repeated setup/editor/settings tour is requested for this loader-only change. A successful editor compile/scene load does not exercise runtime bundle extraction; user startup remains the runtime smoke check.

## Remaining work

Next review serialized artwork references, residual shared helper/effect dependencies and disconnected leaf source disposition as a group before declaring spec 03/phase 2 complete. The old 372-sheet Sprite Dictionary prefab reference inventory requires refresh against the current static/instance declarations and actual Unity dependency behavior; do not erase serialized blocks or repack bundles from this source-only list. No path deletion/move is authorized. New campaign/Downed/HP mechanics and species curation remain later work.

## GPL modification check

Read local License.txt sections 4/5. Add a 2026-11-01 conversion notice to the modified loader and an exact-path dated CHANGES.md entry; preserve prior attribution/license material. No new/copied external source, dependency or asset. Dated notice and exact CHANGES path coverage, scope/resource hashes and License.txt matching HEAD verified; existing attribution and license material preserved. This check is not full distribution clearance; shared-asset provenance and applicable release obligations remain a separate review.


User accepted 03B5 startup before a usage-recharge pause. [03B6](03b6-obsolete-serialized-sprite-arrays.md) resumes with actual Unity serialized-field/dependency evidence and removes only the 156 matching obsolete prefab blocks; shared resources remain.
