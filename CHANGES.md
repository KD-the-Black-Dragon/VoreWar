# VoreWar fork changes

This checkout is developing a mercenary-company total conversion of VoreWar. The original project is described in [README.md](README.md) as a continuation of darkevilme's Vore War. This record distinguishes this fork's work from that earlier work; original copyright, attribution, license, and warranty notices remain applicable.

The covered derivative work continues under the applicable GNU GPL version 3 terms in [License.txt](License.txt). This record does not change existing license grants or the separate terms of third-party code, dependencies, or assets.

## Recording changes

Record completed source changes by actual date, affected paths, and a short description, with a related spec where useful. Modified source files also receive dated notices under the workflow in [AGENTS.md](AGENTS.md). Mark additions as this fork's additions. Preserve previous entries and do not describe planned removals or features as implemented.

## 2026-11-04 — Spec30 feedback: AI move then melee with the last AP

- Modified `Assets/Scripts/AI/TacticalAI.cs`: allow melee planning when the target-tile distance equals the available AP, since the actor stops on an adjacent tile. Both the planning estimate and the executable melee plan reserve one AP using the actual terrain/flight route cost. A completed path executes its planned action before the one-AP ranged replanning shortcut, including actors carrying both weapon types.
- The short-charge candidate search now gives the pathfinder an inclusive target-distance bound; its existing exact route-cost/payoff/risk checks still reserve the attack AP. This fixes the same boundary for a2-AP actor moving once and charging, without changing opportunity costs, limits, stats or target classifications.
- Dated per-file notice, exact source-path record and inherited notices preserved; no external code, assets or dependencies added, no files moved/deleted. GPL License4–5 reviewed; existing provenance/public-distribution review gates remain open. Related spec: `docs/implementation/30-opportunity-attacks-plan.md`.

## 2026-11-04 — Spec30 opportunity attacks and AI risk awareness

- Added `Assets/Scripts/Modes/Tactical/OpportunityAttacks.cs` and its `.meta`: battle-local, deterministic adjacent-enemy reactions before each valid move-command step, sequenced presentation, ordinary melee results and no reaction AP budget or per-turn cap. Pause commands/saving during the transaction; cancel movement on incapacitation or displacement. Strict independent INT>STR mage, AGI>STR archer, and sapient-without-equipped-melee vulnerability checks; deterministic expected-damage estimates do not roll combat RNG.
- Modified `Assets/Scripts/Entities/Units/Actor_Unit.cs`: explicit free melee reaction context in the existing attack pipeline, including existing effects/XP/criticals/grazes and secondary attacks; no AP grant/restore trick. Ordinary movement starts/resumes the shared reaction transaction. Validate before rolling; prohibit threatened occupied-tile transit that could strand a downed mover. Exceptional direct relocations remain exempt.
- Modified `Assets/Scripts/Modes/Tactical/TacticalUtilities.cs`: exclude forced knockback moves from reactions; suppress reaction-context knockback and blocked-knockback bonuses, including nested attacks. Normal knockback remains available.
- Modified `Assets/Scripts/Modes/TacticalMode.cs`: sequence reactions while continuing visual updates; invalidate Undo before reactions and establish the new baseline only after the step/cancellation settles; guard implicit Undo/input/turn/save paths. Preserve pending path steps, reset pending state when replacing battles, and show orange threatened path steps plus a reaction count.
- Modified `Assets/Scripts/AI/TacticalAI.cs`, `Assets/Scripts/AI/AIPlottedPath.cs` and `Assets/Scripts/AI/TacticalPathfinder.cs`: safe routes separate from AP/terrain costs, per-step risk checks, immediate worthwhile charges with AP reserved for a legal attack, no risky distant offensive pursuit or aimless kiting. Standard battle AI can prefer a vulnerable reachable backliner over an engaged frontliner. Stop stale destination actions after interrupted/blocked movement and re-evaluate routes. Existing tactical retreat remains possible.
- Modified `Assets/Scripts/AI/Legacy/legacyTacticalAI.cs`: preserve pending reaction steps, apply the shared safe-route/same-turn charge checks, and prevent aimless moves while engaged in the retained alternate AI.
- Modified `Assets/Scripts/Modes/Tactical/TacticalMessageLog.cs`: persist an opportunity-result marker and label final ordinary hit/miss/graze/critical/block messages; older saved log entries default to ordinary events.
- GPL workflow: dated notices in all nine affected/new C# sources, existing legal notices/attribution preserved, exact-path record including new Unity metadata. Original fork code only; no external code, art or dependencies introduced. No files moved/deleted. Existing asset provenance and public-distribution obligations remain separately tracked in `docs/release-readiness.md`; this does not declare distribution clearance. Related spec: `docs/implementation/30-opportunity-attacks-plan.md`.

## 2026-11-03 — Spec29 tactical stat layout and hidden hover correction

- Modified `Assets/Scripts/UI/TacticalHudView.cs`: fix visible stat rows at30 reference units rather than stretching them over the old eight-stat area. Place four equipment boxes below the compact stats and optional battle counters, and move/enlarge the traits/status viewport to reclaim the space. Preserve existing font sizes, health/mana/XP bars, portrait, item slots and gameplay.
- Modified `Assets/Scripts/Subwindows/InfoPanel.cs`: refresh the compact tactical layout after determining whether battle counters are visible, including when switching units.
- Modified `Assets/Scripts/UI/UnitInfoPanel.cs`: exclude inactive objects from geometry-based hover checks, including hidden legacy stats/equipment; retain tooltips on visible stats and the four live item slots. New internal rectangle references are UI state only, not game/save data.
- Dated per-source notices and exact-path change record added; inherited legal/attribution notices retained. No external source, assets or dependencies introduced, no scene/prefab changes, no files moved/deleted. Existing distribution/provenance review gates remain open. Related record: `docs/implementation/29-four-stat-planning.md`.

## 2026-11-03 — Spec29 four-stat, AP and weapon-result revamp

- Implement four active stats: STR melee/HP/digestion resistance, AGI ranged/evasion/vore evasion, INT magic/mana/resistance/escape/criticals, VOR swallowing/capacity/digestion/retention. Preserve historical numeric IDs and compatibility aliases; old units and species ranges convert once using documented weighted averages, never summing the old stats. Count active stats once in growth, choices and power ratings. Preserve equipped stat bonuses during conversion.
- Fixed species AP: Gnoll/melee and future-default melee/vore 3; Avian/ranged, Lizard/mage and ranged Spitter Slug 2. Remove stat/positive speed growth from AP, retain injury/fullness penalties, ordinary all-remaining-AP action rules and un-restored retired trait status. Fractional supporting-action costs have a minimum of 1 in execution and AI estimates.
- Ordinary weapon attacks roll accuracy first; a miss at accuracy >=51% gets one independent retry at the same chance for a graze, using the inherited graze damage pipeline (default30% plus existing modifiers). Initial hits can critically hit using provisional INT-based chance (5% +0.25 percentage points per effective INT, capped35%) and inherited critical damage (default1.5x). No crit on a rescued graze; logs/popups report final result only. Spells/vore/status abilities retain their separate resolution rules.
- Update company/inspection/level-up/help/tooltips and developer stat-range/choice controls. Preserve old saves via per-unit/per-range markers and non-destructive load conversion; old pending level-up choices refresh and old mid-battle AP clamps only on upgrade. Existing Spec28 feedback fixes are preserved and included in subsequent builds.
- Modified source paths:
  - `Assets/Scripts/AI/TacticalAI.cs`
  - `Assets/Scripts/Core/CompanyContracts.cs`
  - `Assets/Scripts/Core/CompanyEconomy.cs`
  - `Assets/Scripts/Core/CompanyEquipment.cs`
  - `Assets/Scripts/Core/CompanyInjuries.cs`
  - `Assets/Scripts/Core/ContentSettingsData.cs`
  - `Assets/Scripts/Core/WorldConfig.cs`
  - `Assets/Scripts/Entities/Units/Actor_Unit.cs`
  - `Assets/Scripts/Entities/Units/Leader.cs`
  - `Assets/Scripts/Entities/Units/NPC_unit.cs`
  - `Assets/Scripts/Entities/Units/PredatorComponent.cs`
  - `Assets/Scripts/Entities/Units/Prey.cs`
  - `Assets/Scripts/Entities/Units/Spawn.cs`
  - `Assets/Scripts/Entities/Units/Unit.cs`
  - `Assets/Scripts/Enums/Stat.cs`
  - `Assets/Scripts/Modes/Tactical/TacticalMessageLog.cs`
  - `Assets/Scripts/Races/RaceParameters.cs`
  - `Assets/Scripts/Races/Traits.cs`
  - `Assets/Scripts/Subwindows/CompanyManagementUI.cs`
  - `Assets/Scripts/Subwindows/InfoPanel.cs`
  - `Assets/Scripts/Subwindows/RaceEditorPanel.cs`
  - `Assets/Scripts/UI/Connectors/EditStatButton.cs`
  - `Assets/Scripts/UI/UnitEditorPanel.cs`
  - `Assets/Scripts/Utility/Stored/RaceSettings.cs`
  - `Assets/Scripts/Utility/Texts/HelpText.cs`
  - `Assets/Scripts/Utility/Texts/HoveringTooltip.cs`
- Added `Assets/Scripts/Core/WarbandStats.cs` and its `Assets/Scripts/Core/WarbandStats.cs.meta` companion. The helper is original fork code; no external code/assets/dependencies introduced. The exact meta path is associated here because Unity metadata cannot carry C# comments.
- Related design/implementation record: `docs/implementation/29-four-stat-planning.md`. Every changed source has a dated modification/addition notice; earlier attribution retained, actual License.txt sections4–5 reviewed. Existing distribution/provenance obligations remain unresolved as previously recorded; this is not a release or blanket compliance claim.

## 2026-11-03 — Spec28 accepted styling: two padding/border corrections

- User accepts the UI beautification except two specific details. Modified `Assets/Scripts/UI/TacticalHudView.cs` and `Assets/Scripts/UI/UIThemeSurface.cs`: omit only the duplicate left border of the combat log's nested scroll/viewport surfaces, keeping the leftmost outer frame, other edges, shading, text and layout. The per-instance left-edge setting is serialized for normal Unity cloning; all other surfaces default to retaining it.
- Modified `Assets/Scripts/Subwindows/CompanyManagementUI.cs`: member-row actions only use34-point reference height instead of38, six units less width centered within the previous bounds, and matching label rectangles. Keep fonts/text/callbacks, row borders, portraits, pagination and other screens unchanged.
- GPL workflow: actual License.txt sections4–5 reviewed; three source files carry dated notices, meaningful earlier notices/attribution retained, exact paths recorded here. No new source/assets/dependencies, serialized scene changes, file moves/deletion or personal-save access. Existing provenance/distribution gates unchanged. Scoped source/scene-reference and diff review only; no Unity compile, new build or graphical playtest run for these small follow-ups. Latest0691b41b executable predates the corrections.

## 2026-11-03 — Spec28: selected blue-and-gold UI beautification

- Implement the user's selected sampler treatment: Current menu blue base colors, gradient100%, border100%, Aged gold accent and panel borders. Preserve text, accepted sizes/positions, interactions, gameplay and save formats. Base palette is not the Blue & aged gold alternative preset.
- Added fork source `Assets/Scripts/UI/UIThemeSurface.cs` and `Assets/Scripts/UI/UIThemeSurface.cs.meta`: procedural three-stop UI-only gradients and inside-one-pixel aged-gold borders; full control depth (16%white top/14%black bottom), gentler panel/row/section depth,70%panel-edge opacity/full control opacity. Separate border-only overlay for map/command selection; retain clipping, alpha and ordinary UI materials. Serialize the renderer settings so cloned dropdown templates keep the configured treatment. Reject artwork/fill graphics at the renderer boundary. No imported textures/shaders/dependencies or artwork edits.
- Modified `Assets/Scripts/UI/UITheme.cs`: gold border/selected accents, shared gradient surfaces, neutral hover/pressed tinting, framed dropdown items and border-only frames. Existing callers cover main menu, Save/Load, Help/Credits, Esc, popups, input dialogs, tooltips and the existing settings/creator/setup controls.
- Modified `Assets/Scripts/Subwindows/CompanyCampaignView.cs`, `Assets/Scripts/Subwindows/CompanyManagementUI.cs`: replace grey campaign chrome with the shared palette; frame setup/town/management/status areas and roster/recruitment rows; route buttons and inputs through shared styling. Keep terrain/fog tile buttons out of the new effect; place a non-intercepting border over the unchanged map viewport. Row backplates stay behind all portraits so overlapping paper-doll parts are not clipped or obscured. Existing locations, actions, wording, costs and timing unchanged.
- Modified `Assets/Scripts/Subwindows/OptionsView.cs`, `Assets/Scripts/Subwindows/ContentSettingsView.cs`, `Assets/Scripts/Subwindows/SingleBattleSetupView.cs`, `Assets/Scripts/Subwindows/CompanyLeaderCreatorView.cs`: theme dropdown items/templates without painting over checkmark/arrow or paper-doll graphics.
- Modified `Assets/Scripts/UI/TacticalHudView.cs`: shared tactical panel/command treatment, bright-gold action-selection outline at its accepted position; exclude portraits and Slider status fills/backgrounds. Modified `Assets/Scripts/UI/RightClickMenu.cs` and `Assets/Scripts/UI/HoveringRacePicture.cs`: theme remaining context-menu/hover-card chrome. Modified `Assets/Scripts/UI/InputBox.cs`: gold-frame retained light input with black entered text and neutral hover colors. Developer Race Editor page redesign remains outside this player-facing pass.
- GPL workflow: actual License.txt sections4–5 reviewed; eleven modified C# sources plus the new fork source have dated notices and preserve meaningful previous notices/attribution. Exact new metadata path recorded here; no serialized game asset/project/package changes, personal-save access or file deletion/moves. Seven existing license/change/dependency notices packaged with the review build. No new external code/assets/dependencies. Existing provenance/distribution gates remain open; a successful build is not full distribution clearance.
- Native review build exit0 with no logged C# errors/exceptions/missing scripts; the initial scene/import run refreshed a stale compiler list before recognizing the newly added source and is not counted as clean compilation evidence. Source/hash/notice/diff scope reviewed. No new test framework/tests or repeated unrelated save suite; no graphical playtest claimed. See `docs/implementation/28-ui-beautification.md` for coverage/limits and the combined player checklist.

## 2026-11-03 — Spec27: standalone UI palette sampler

- Added fork source `docs/ui-sampler/index.html` and documentation `docs/ui-sampler/README.md`. Offline, self-contained browser design sampler: current campaign grey/menu blue references plus Blue & aged gold, Midnight & bronze and Warm slate alternatives. Adjustable gradient contrast/border emphasis/accent/panel edges; campaign, tactical-control and options specimens; sample dialog, control states, temporary reference comparison and copyable preference summary. Local browser-only choice persistence. No integration into or modification of Unity UI/gameplay/settings/saves.
- GPL workflow: actual License.txt sections 4–5 reviewed; dated fork-addition notice at top of new HTML and an exact-path change record here, with local license link. New locally authored HTML/CSS/JavaScript, system fonts only; no external code/assets/dependencies or copied artwork. Existing upstream/dependency attribution and provenance/distribution gates remain untouched. This design sampler does not certify release readiness of the game.
- Inspection: isolated headless Edge, 14 focused interaction/layout checks passed; rendered campaign/tactical/options specimens inspected at 1440/1024/360 pixel widths. Final JavaScript syntax check passed. No Unity build, new test framework or game test sources. See sampler README for exact limits and usage. Existing live UI and all prior user work preserved; no files deleted or moved.

## 2026-11-03 — Credits character-rights wording follow-up

- Modified `Assets/StreamingAssets/Credits.txt` and the matching credits text in `Assets/base.unity`: add the exact user-requested statement “All OCs used in Vore War have been removed from Vore Warband.” beneath CHARACTER RIGHTS with blank lines before/after. Remove the complete parenthetical “Feels weird, I know. But credit where its due.” line. Preserve the remaining Pokémon attribution, credit wording and layout.
- Content-only change; exact serialized/text paths recorded here as the associated modification notice. Actual License.txt sections 4–5 reviewed, remaining license/attribution preserved, no new code/assets/dependencies. Existing provenance/distribution gates unchanged. Scoped content review only; no Unity invocation, new build or playtest.

## 2026-11-03 — Requested Windows review build for Specs23–26

- Built the pending credits text/link update, Bestiary retirement, default name-list cleanup and dark-blue/smaller credits styling into a fresh private Win64 player. Final clean build exited0 with no logged C# errors, exceptions or missing-script messages. Initial build refreshed a stale BestiaryPanel compiler-cache entry; it is preserved separately and is not the clean-log evidence.
- Existing License.txt sections4–5 and source modification notices/CHANGES coverage reviewed; seven existing license/change/dependency notices packaged with the build. No new code, art or dependencies in this build-only step; existing provenance/distribution gates remain open. No graphical playtest or repeated save suite claimed.
- Unity import recognized the user's intentional removal of empty retired-art folders and removed their orphan folder metadata: `Assets/Sprites/Units/Bella.meta`, `Cherub.meta`, `Elves.meta`, `RwuMercenaries.meta`, `Seraph.meta`, `SoulSprite.meta`, `Yordles.meta`, `goblins.meta`, `imps.meta`, `zera.meta` (all under the same Units directory). These were not restored or treated as corruption. No agent deletion command, personal-save access or changes to prior builds.

## 2026-11-03 — Spec26: credits color and typography unification

- Modified `Assets/Scripts/UI/CreditsLink.cs`: apply the existing UITheme dark-blue background/panel colors, blue Exit button and scrollbar, and light text when the credits page opens. Reduce body text from 30 to 24 points, render the Warband heading at 30 and upstream heading at 26, and reduce Exit label typography to 24. Restrict styling to the existing credits Canvas hierarchy; preserve layout, scroll behavior, wording, attribution and Patreon click handling. No serialized scene/content changes in this packet.
- GPL workflow: actual License.txt sections 4–5 reviewed; dated per-file modification notice added after the existing fork-addition notice, exact source path recorded here, existing licensing/attribution preserved. No external assets/code/dependencies introduced. Existing provenance/distribution gates remain open. Scoped source/scene-hierarchy review only; no Unity compile, player build or graphical playtest run.

## 2026-11-03 — Spec25: retired species name-list cleanup

- Modified `Assets/StreamingAssets/armyNames.txt`: remove exactly 22 retired-species paragraphs while preserving all remaining paragraphs and their spacing byte-for-byte, including uncurated/future species. Removed headings: Humans, Dogs, Wolves, Cats, Imps, Taurus, Foxes, Slimes, Crypters, Harpies, Lamia, Driders, Aabayx, Merfolk, Crux, MainlandElves, Bears, Umbreon, Scylla, Alraune, Ghosts, Yordles. Kept Bunnies and the historical DemiDragons candidate section; no additional species retirement inferred.
- Directly moved the following six text/metadata paths to ignored project Recycle Bin; hashes match, nothing deleted:

  - `Assets/StreamingAssets/femaleCentaurs.txt` -> `Recycle Bin/Retired Name Lists/Assets/StreamingAssets/femaleCentaurs.txt`
  - `Assets/StreamingAssets/femaleCentaurs.txt.meta` -> `Recycle Bin/Retired Name Lists/Assets/StreamingAssets/femaleCentaurs.txt.meta`
  - `Assets/StreamingAssets/maleCentaurs.txt` -> `Recycle Bin/Retired Name Lists/Assets/StreamingAssets/maleCentaurs.txt`
  - `Assets/StreamingAssets/maleCentaurs.txt.meta` -> `Recycle Bin/Retired Name Lists/Assets/StreamingAssets/maleCentaurs.txt.meta`
  - `Assets/StreamingAssets/GhostVillageNames.txt` -> `Recycle Bin/Retired Name Lists/Assets/StreamingAssets/GhostVillageNames.txt`
  - `Assets/StreamingAssets/GhostVillageNames.txt.meta` -> `Recycle Bin/Retired Name Lists/Assets/StreamingAssets/GhostVillageNames.txt.meta`

- Data-only retirement: no C# changes, personal name/settings files, other text lists, artwork or prior builds touched. Exact non-commentable name-list modification/relocation notice is this entry; no header inserted that could be mistaken for an army name. Actual License.txt sections 4–5 reviewed, existing license/attribution retained; no outside code/assets/dependencies introduced. Existing provenance/distribution gates remain open. Scoped data-diff and move/hash review only; no Unity compile, player build or playtest run.

## 2026-11-03 — Spec24: physical Bestiary retirement

- Modified `Assets/base.unity`: remove the deprecated Bestiary Screen subtree, inactive Bestiary menu-button instance, dedicated controller component and their callbacks. Remove only the retired transforms from parent child lists; preserve all unrelated scene content, including the pending Spec23 credits changes. Exact retired local IDs recorded in Logs/bestiary24-before.json; no remaining local-ID/GUID consumers found during source/content review.
- Directly moved the remaining `Assets/Bestiary/` folder and `Assets/Bestiary.meta`, plus `Assets/Scripts/UI/BestiaryPanel.cs` and its metadata, into ignored `Recycle Bin/Retired Bestiary/` with the original Assets hierarchy. 15 files, including six image/metadata pairs, folder metadata and controller/metadata; hashes verified after moves. Nothing deleted, shared assets untouched. Exact paths follow:

  - `Assets/Bestiary/BoomBunny.jpg` -> `Recycle Bin/Retired Bestiary/Assets/Bestiary/BoomBunny.jpg`
  - `Assets/Bestiary/BoomBunny.jpg.meta` -> `Recycle Bin/Retired Bestiary/Assets/Bestiary/BoomBunny.jpg.meta`
  - `Assets/Bestiary/HarvesterBio.jpg` -> `Recycle Bin/Retired Bestiary/Assets/Bestiary/HarvesterBio.jpg`
  - `Assets/Bestiary/HarvesterBio.jpg.meta` -> `Recycle Bin/Retired Bestiary/Assets/Bestiary/HarvesterBio.jpg.meta`
  - `Assets/Bestiary/HS010.png` -> `Recycle Bin/Retired Bestiary/Assets/Bestiary/HS010.png`
  - `Assets/Bestiary/HS010.png.meta` -> `Recycle Bin/Retired Bestiary/Assets/Bestiary/HS010.png.meta`
  - `Assets/Bestiary/HS011.png` -> `Recycle Bin/Retired Bestiary/Assets/Bestiary/HS011.png`
  - `Assets/Bestiary/HS011.png.meta` -> `Recycle Bin/Retired Bestiary/Assets/Bestiary/HS011.png.meta`
  - `Assets/Bestiary/HS012.png` -> `Recycle Bin/Retired Bestiary/Assets/Bestiary/HS012.png`
  - `Assets/Bestiary/HS012.png.meta` -> `Recycle Bin/Retired Bestiary/Assets/Bestiary/HS012.png.meta`
  - `Assets/Bestiary/HS013.png` -> `Recycle Bin/Retired Bestiary/Assets/Bestiary/HS013.png`
  - `Assets/Bestiary/HS013.png.meta` -> `Recycle Bin/Retired Bestiary/Assets/Bestiary/HS013.png.meta`
  - `Assets/Bestiary.meta` -> `Recycle Bin/Retired Bestiary/Assets/Bestiary.meta`
  - `Assets/Scripts/UI/BestiaryPanel.cs` -> `Recycle Bin/Retired Bestiary/Assets/Scripts/UI/BestiaryPanel.cs`
  - `Assets/Scripts/UI/BestiaryPanel.cs.meta` -> `Recycle Bin/Retired Bestiary/Assets/Scripts/UI/BestiaryPanel.cs.meta`

- GPL workflow: actual License.txt sections 4–5 reviewed; the archived controller retains earlier notices and has a dated retirement notice. Exact serialized/moved paths recorded here; inherited licensing/attribution preserved. No outside code/art/dependencies introduced. Existing provenance/distribution gates remain open. Source/scene-reference and archive-hash review only; no Unity compile, new player build or manual playtest run for this packet.

## 2026-11-03 — Spec23: Warband and upstream credits separation

- Modified `Assets/StreamingAssets/Credits.txt` and the existing credits text in `Assets/base.unity`: add a separate centered Vore Warband credit for K.D. — Curator of Voracious Beasts, the user-supplied Patreon URL and requested GPL-3.0 principles statement. Label the remaining credits as Vore War 45D. Remove the obsolete OC character-rights entries through Renamon and the closing Note from Ignis. Preserve the CHARACTER RIGHTS heading, complete GAME FREAK/Creatures Inc./Nintendo note and all other contributor/art/code credits unchanged.
- Added fork source `Assets/Scripts/UI/CreditsLink.cs` and `Assets/Scripts/UI/CreditsLink.cs.meta`; attach the component only to the existing credits text in `Assets/base.unity`. A left-click on the supplied Patreon link opens that exact URL. No other menu layout, artwork or gameplay changes.
- GPL workflow: actual License.txt sections 4–5 reviewed; new source has an addition/date notice, exact text/serialized/source/meta paths recorded here, existing contributor/legal notices retained outside the explicitly requested obsolete character-rights block. No outside code/assets/dependencies introduced. The displayed principles statement is the user's requested credit wording; existing provenance/distribution checks remain applicable. Source/content review performed; no Unity compile, new player build or graphical/manual link test run for this packet.

## 2026-11-03 — Spec22: trait metadata import and retained developer Unit Tags

- Modified `Assets/Scripts/Utility/ExternalTraitHandler.cs`: skip undefined retired trait names per entry, keep valid local category/rarity edits, fill missing entries from the shipped catalog and replace metadata without deleting the existing file first. Malformed entries cannot abort later valid rows. Update the Unit Tags loader per file, report invalid/duplicate definitions, normalize missing lists and track source filenames so legacy space-prefixed files and renamed tags save to their own existing paths. New tag filenames include stable IDs. No personal settings or saves accessed during agent checks.
- Modified `Assets/StreamingAssets/taggedTraits.json`: remove exactly TheMatron, QueenOfFrost and DestroyingAngel category rows; all other rows and their ordering/values retained.
- Modified `Assets/Scripts/Subwindows/RaceEditorPanel.cs`, `Assets/Scripts/Subwindows/UnitTagEditor.cs`, `Assets/Scripts/UI/Connectors/UnitTagMenu.cs`: tolerate empty tag lists/missing definitions; assign stable IDs rather than dropdown positions; refresh species tag choices after tag editing; correct loaded condition dropdown offsets. Retain the existing password/session gate and require it for SaveTag. No built-in Unit Tags or player balance controls added.
- Modified `Assets/Scripts/Utility/TagConditionChecker.cs`: correct legacy numeric species-category comparisons, retain trait-derived tags when species tag overrides are absent and guard missing tactical actors in fullness/digestion conditions. These legacy category conditions remain developer-only and do not replace the curated sapient/animal policy.
- Modified `Assets/Scripts/Modes/StartMode.cs`: suppress the player-facing developer trait-category warning on the main menu; developer tools remain available through the password-protected Race Editor.
- Includes the preceding `Assets/Scripts/UI/Connectors/RaceHoverObjectAuto.cs` fix in the next player build. No additional art changes or content restoration.
- GPL workflow: actual License.txt sections 4–5 reviewed; seven changed C# paths have dated modification notices, inherited notices/attribution preserved; exact non-commentable metadata path recorded above. No outside code/assets/dependencies introduced. Seven license/change/dependency notices accompany the private build; existing provenance/distribution gates remain open. See `docs/implementation/22-unit-tags-correction.md` for actual verification and manual-review limits.

## 2026-11-03 — Race Editor dropdown hover preview correction

- Modified `Assets/Scripts/UI/Connectors/RaceHoverObjectAuto.cs`: use the shared species-name parser for singular/spaced dropdown labels instead of parsing display text as a raw enum name. Retain the existing preview eligibility gate, including rejection of retired species. No artwork, bundles, scenes or species availability changed.
- GPL check: actual License.txt sections 4–5 reviewed; dated conversion notice added, exact source path recorded here, existing licensing/attribution retained. No copied code, assets or dependencies introduced; existing release-provenance gates remain open. Source/diff review only; no new Unity compile, player build or graphical playtest run for this correction.

## 2026-11-03 — Spec21: approved physical content retirement

- Removed the 90 permanently rejected species/character identities from the active Race catalog, exclusive rendering implementations, spawn/parameter entries and OC equipment/trait branches. Surviving numeric species IDs are unchanged. Modified and relocated source files carry dated conversion notices; meaningful previous notices and attribution are retained.
- Directly moved 808 source/asset/bundle/metadata paths into ignored `Recycle Bin/Retired Content/`, preserving original hierarchy and verified hashes. Nothing was deleted. Exact modified, added and source-to-archive paths are recorded in [the executed path record](docs/implementation/21-executed-source-paths.md), which is part of this change notice. The former bundle payloads were replaced rather than left carrying rejected artwork.
- Preserved generic/shared artwork and future non-rejected species. Extracted original pixels for Sergal mask/discard, generic slash and Diamond Storm effects into `Assets/Sprites/Units/SharedMaskParts.png`, `Assets/Sprites/TacticalEffects/SharedSlashFrames.png`, and `Assets/Sprites/TacticalEffects/SharedDiamondProjectile.png`; original artwork attribution/terms continue to apply. Added fork editor utility `Assets/Editor/RetiredContentBuild.cs` and associated metadata. Updated `Assets/Prefabs/Tactical/Specific Effects/Slash Effect.prefab`, `Assets/Prefabs/Tactical/Specific Effects/DiamondStorm.prefab`, sprite catalog/loader bindings and exact archived GUID references in `Assets/base.unity`, `Assets/Prefabs/Sprite Dictionary.prefab` and associated metadata. Replacement empire/merc/monster bundles and manifests are under `Assets/StreamingAssets/AssetBundles/`; the exclusive specialmerc group is archived.
- Broad swallowed-Selicia sprite sheets are intact. The [artwork inspection index](docs/implementation/21-selicia-artwork-review.md) identifies dedicated archived candidates and historical mixed-sheet frame references for user review. It does not authorize trimming those sheets.
- GPL workflow: actual License.txt sections 4–5 reviewed; source notice/date/path coverage checked, existing license/attribution preserved, exact relocations recorded. No outside code/art/dependencies introduced. Retained original/derived asset provenance and binary-distribution obligations remain open release gates; these checks are not a claim of complete distribution compliance. Verification and manual-review limits are recorded in the [execution report](docs/implementation/21-retired-content-execution.md).

## 2026-11-03 — Spec20: menu cleanup and shared campaign Help

- Modified `Assets/Scripts/Subwindows/CompanyCampaignView.cs`: remove Continue and Map-only preview creation buttons and their unused handlers; remove the obsolete preview instruction; move the retained Load Game beside Create company. Existing campaign/leader creation, Ironman setup and save loading remain intact.
- Modified `Assets/Scripts/Modes/StartMode.cs`, `Assets/Scripts/Core/State.cs`, `Assets/Scripts/Utility/Texts/HelpText.cs`: rename the main-menu caption and corresponding save guidance to Load Game; suppress the routine default-race-settings message while preserving actual error reporting and race-setting fallback behavior.
- Modified `Assets/Scripts/UI/HelpPanel.cs`, `Assets/Scripts/Subwindows/GameMenu.cs`, `Assets/Scripts/Subwindows/CompanyMissionsUI.cs`, `Assets/Scripts/Core/GameManager.cs`: use one shared campaign Help presentation from main menu, company/creator and tactical menu. Restrained blue surfaces/borders, compact chapter list, 18-point reading text, permanent visible scrollbar, bounded centered layout and Escape/Exit Help. Opening the guide preserves its underlying screen and stops tactical input/AI until closed. All existing campaign chapters and character-stat/trait/spell references remain available, with long references generated only on selection. Original scene objects/bindings are retained inactive; no scene/prefab rewrite or asset deletion. Broader help prose rewrite remains separate.
- GPL workflow: actual License.txt sections 4–5 reviewed; dated conversion notices on all eight changed source files, exact paths recorded here, existing notices/licensing/attribution preserved. No new source files or outside code/assets/dependencies. Existing provenance and distribution gates remain open. Verification and player-review status are recorded in `docs/implementation/20-menu-cleanup-and-campaign-help.md`.

## 2026-11-03 — Spec19: named Save / Load and exact battle persistence

- Added fork sources `Assets/Scripts/Core/CompanySaveSlots.cs`, `Assets/Scripts/Core/CompanyBattleSnapshot.cs`, `Assets/Scripts/UI/CompanySaveMenu.cs` and their associated `.cs.meta` files: named logical slots, map/battle autosaves, selected/hovered company summaries, overwrite/load/delete confirmations, verified latest-plus-three-backup retention and complete battle graph guards. Runtime deletion is confined to owned company saves; Ironman defeat first publishes a durable invalidation marker. Original source and assets are retained.
- Modified `Assets/Scripts/Core/CompanyCampaign.cs`, `Assets/Scripts/Core/CompanyCampaignSave.cs`, `Assets/Scripts/Core/CompanyBattle.cs`, `Assets/Scripts/Utility/Rand.cs`: slot metadata; checked immutable generations and corruption fallback; canonical company/actor identity; resume HP/AP/containment/turns/camera and the pinned Mono/Unity random state. Results/revival remain transactional. No old empire saves are imported.
- Modified `Assets/Scripts/Entities/Units/Unit.cs`, `Assets/Scripts/Entities/Units/Actor_Unit.cs`, `Assets/Scripts/Entities/Units/TempBoosts.cs`, `Assets/Scripts/Modes/Tactical/TileEffect.cs`, `Assets/Scripts/Modes/TacticalMode.cs`: persist attrition latch, temporary combat boosts, animation/stance and tile effects; reuse native tactical reconstruction without starting another turn; settled side-turn autosaves and safe player-turn manual saving. Company item services remain bound; summons, effects and reserves keep their distinct identities.
- Modified `Assets/Scripts/Core/GameManager.cs`, `Assets/Scripts/Core/State.cs`, `Assets/Scripts/Modes/StartMode.cs`, `Assets/Scripts/Subwindows/CompanyCampaignView.cs`, `Assets/Scripts/Subwindows/CompanyManagementUI.cs`, `Assets/Scripts/Subwindows/GameMenu.cs`, `Assets/Scripts/Subwindows/SaveLoad.cs`: route main menu, map, battle menu and quicksave/load through the unified blue browser; retire original World serialization and public checkpoint browsing. Old serialized scene bindings are retained as forwarding callbacks, without scene/prefab rewrites.
- Modified `Assets/Scripts/Utility/Texts/HelpText.cs`: update saving/controls guidance for this feature only; broader help rewrite remains deferred.
- Added fork editor check `Assets/Editor/CompanySavePersistenceAudit.cs` and `Assets/Editor/CompanySavePersistenceAudit.cs.meta`, explicitly authorized by the user: synthetic map/native-battle restore, canonical containment/summons/resources/effects/RNG, autosaves, corruption recovery, Ironman restrictions and retention/deletion targeting. Editor-only redirection stays under project Logs and suppresses every actual removal, preserving all probes and never accessing personal saves. No new framework or external dependencies.
- Associated new serialized metadata paths: `Assets/Editor/CompanySavePersistenceAudit.cs.meta`, `Assets/Scripts/Core/CompanyBattleSnapshot.cs.meta`, `Assets/Scripts/Core/CompanySaveSlots.cs.meta`, `Assets/Scripts/UI/CompanySaveMenu.cs.meta`. These metadata files are recorded here because their format has no source-comment header.
- GPL workflow check: actual License.txt sections 4–5 reviewed, dated source modification/addition notices and exact paths recorded; prior notices, licensing and attribution preserved. Associated metadata notices are covered by this record. No outside code/art/assets introduced. Existing release provenance and binary/source distribution gates remain open; checks do not declare complete distribution compliance. See `docs/implementation/19-save-load-and-battle-persistence.md` for actual verification and remaining player review.

## 2026-11-03 — Spec18: one-screen company leader creator

- Modified `Assets/Scripts/Subwindows/CompanyManagementUI.cs`: replace species buttons with a dropdown host, use compact top controls, remove the old scroll viewport and its obsolete scroll instruction, retain focus/name/confirm/cancel wording and behavior, and host the new presentation. Cover unused campaign heading area only while creating; global bottom navigation remains available.
- Added fork source `Assets/Scripts/Subwindows/CompanyLeaderCreatorView.cs` and associated `Assets/Scripts/Subwindows/CompanyLeaderCreatorView.cs.meta`: blue single-screen layout, all native appearance rows distributed between two compact columns; existing gender/pronoun/quantification controls retained; species dropdown uses the approved list and native template. Centered2x paper doll in a clipped dedicated card; original art/rendering unchanged. No original option labels renamed.
- Modified `Assets/Scripts/UI/Connectors/CustomizerPanel.cs` and `Assets/Scripts/Subwindows/UnitCustomizer.cs`: optional nonserialized company-only refresh callback keeps layout synchronized with native gender/species conditional visibility and appearance updates. Other customizer users unchanged.
- Actual License.txt sections4–5 reviewed; dated notices/exact source/meta paths recorded, new source identified as this fork's addition, inherited licensing/attribution retained. No outside assets/dependencies introduced; existing release provenance gates remain open. Build/scope/manual review evidence in spec18 implementation report.

## 2026-11-03 — Popup input contrast

- Modified `Assets/Scripts/UI/InputBox.cs`: after shared dialog styling, set entered text/caret black and empty-input placeholder opaque medium grey (#808080). Light input background and surrounding blue dialog/foreground labels remain unchanged; no value-entry behavior changed.
- Actual License.txt sections4–5 reviewed; dated notice/exact path recorded and inherited notices/licensing/attribution preserved. No outside additions; existing release provenance gates remain open. Visual confirmation pending.

## 2026-11-03 — Slider hover and single-dialog value entry

- Modified `Assets/Scripts/UI/Connectors/SliderRevealer.cs`: replace persistent hover flags and held-button polling with current topmost UI raycast ownership and right-button press-edge detection. Cache raycast results once per frame; only the targeted slider with the pointer inside its rectangle may display its tooltip or open an entry dialog, and ActiveInput blocks further requests until closure. Existing parsing/range behavior retained.
- Modified `Assets/Scripts/Utility/Texts/HoveringTooltip.cs`: shared tooltip graphics are non-raycast targets, preventing the displayed tooltip from capturing pointer events from underlying controls.
- Actual License.txt sections4–5 reviewed, dated notices/exact paths recorded and inherited attribution/licensing preserved. No outside additions; existing release provenance gates remain open. Visual interaction review pending.

## 2026-11-03 — Spec 17: shared blue popups and compact Esc menu

- Modified `Assets/Scripts/UI/UITheme.cs`, `Assets/Scripts/Subwindows/GameMenu.cs`, `Assets/Scripts/UI/DialogBox.cs`, `Assets/Scripts/UI/MessageBox.cs`, `Assets/Scripts/UI/InputBox.cs`, `Assets/Scripts/UI/OptionsBox.cs`, `Assets/Scripts/Utility/Texts/HoveringTooltip.cs`, `Assets/Scripts/Subwindows/SaveLoad.cs`, `Assets/Scripts/UI/TacticalHudView.cs`: share popup palette across confirmations, messages, input/multi-choice boxes, hover/slider tooltips, save/load and auto-resolve options. Dialog actions, content and font sizes retained.
- Esc menu becomes a centered blue overlay with dim background and spaced buttons; remove its Race Editor entry and rename Help Screens to Help. Developer editor itself and its gate code are unchanged.
- Actual License.txt sections4–5 reviewed; dated notices/exact source paths recorded and earlier attribution/licensing preserved. No outside code/assets/dependencies added; existing release provenance gates remain open. Build/visual review status recorded in spec17 report.

## 2026-11-03 — Preserve transparency of the barrier overlay above HP

- Modified `Assets/Scripts/UI/TacticalHudView.cs`: exclude images belonging to sliders from general panel recoloring. The barrier slider occupies the same rectangle above HP and its Background has original alpha0; recoloring it opaque concealed the HP fill. Native bar backgrounds/fills now keep their original transparency. Accepted trait spacing/edge layout unchanged.
- Actual License.txt sections4–5 reviewed; dated notice/exact source path recorded and prior notices/licensing/attribution retained. No outside additions; existing release provenance gates remain open. Visual confirmation pending.

## 2026-11-03 — Solid HP fill, uniform trait spacing and screen-edge seams

- Modified `Assets/Scripts/UI/TacticalHudView.cs`: explicitly use Simple/filled-center HP image and clear sprite overrides; move left inspector one reference pixel left and right command sidebar one right, widen bottom bar by two. Core controls/fonts remain unchanged.
- Modified `Assets/Scripts/Subwindows/InfoPanel.cs`: put the final trait's newline inside the reduced-size span, preventing restored larger font metrics from changing that line's spacing. Non-tactical trait output remains unchanged.
- Actual License.txt sections4–5 reviewed; dated notices/exact paths recorded and prior notices/licensing/attribution retained. No external additions; existing release provenance gates remain open. Visual review pending.

## 2026-11-03 — Tactical readability and status-bar follow-up

- Modified `Assets/Scripts/UI/TacticalHudView.cs`: preserve slider styling, explicitly restore red HP fill, use shared bright vector checkmarks at existing checkbox positions, move equipment boxes/headings halfway back up and shift details equally.
- Modified `Assets/Scripts/Subwindows/InfoPanel.cs`: tactical traits and innate spells use70%text; other core text remains unchanged.
- Modified `Assets/Scripts/UI/MessageLogPanel.cs`: compact height257 (three fewer reference pixels), bottom100 aligns with the status bar's top; no gap. Expand/collapse behavior retained.
- Actual License.txt4–5 reviewed; dated notices/exact source paths recorded, earlier attribution/licensing preserved. No external additions; existing release provenance gates remain open. Visual review pending.

## 2026-11-02 — Tactical selection and equipment clearance

- Modified `Assets/Scripts/UI/TacticalHudView.cs`: match selection-outline pivot to the relocated command buttons so their transform origins align; retain120x50 outline size. Move equipment headings/boxes60reference units down and shift the detail viewport equally, clearing VOR/STM and the final stat row. No command behavior or fonts changed.
- Actual License.txt sections4–5 reviewed; dated notice and exact path recorded, earlier notices/licensing/attribution preserved. No external additions; existing release provenance gates remain open. Visual confirmation remains pending.

## 2026-11-02 — Spec 16 feedback: item boxes, command spacing and battle log repair

- Modified `Assets/Scripts/UI/TacticalHudView.cs`: four bordered item boxes in two rows, with headings outside the boxes and truly empty strings for unequipped slots. Removed placeholder/natural-weapon fallback text from those equipment slots. Saved the source with a UTF-8 BOM to avoid the older compiler's misreading of unmarked Unicode. Attack/Move/Vore/End Turn now share a padded row; End Turn is twice its original width. Separate utility and equal-width Auto-resolve Battle/AI: Finish Turn groups, with gaps between them. Core command/stat text fonts remain unchanged.
- Modified `Assets/Scripts/UI/MessageLogPanel.cs`: correct compact log width to match its horizontal stretch anchors, retaining height 260/font 22. The previous width 1920 added to the parent width, moving content/size controls offscreen. Fullscreen uses normalized stretch offsets; returning to compact/collapsed/restored view uses one shared layout. Resizing and filter changes request the existing next-frame scroll refresh.
- Actual License.txt sections4–5 reviewed; both dated source notices and exact paths recorded, earlier notices/licensing/attribution preserved. No outside code/assets/dependencies introduced. Compilation/read-only scene check passed; fresh private Windows build passed exit 0 with no logged C#/shader errors. Graphical review was attempted but blocked by screenshot/input geometry errors; no battle playtest is claimed. See [spec 16](docs/implementation/16-tactical-interface.md). Existing release/provenance gates remain open.

## 2026-11-02 — Spec 16: tactical battle interface and unified Attack

- Modified `Assets/Scripts/Modes/TacticalMode.cs`: host the runtime blue HUD; both existing Attack hotkeys/buttons select one distance-based command, with matching weapon/range checks, target odds and damage previews. Adjacent targets use melee; farther targets require a valid ranged weapon. Native damage/AP, AI and special-attack implementations remain.
- Added fork source `Assets/Scripts/UI/TacticalHudView.cs` and associated `Assets/Scripts/UI/TacticalHudView.cs.meta`: shared dark blue tactical panels/buttons, light labels, hidden redundant faction/controller labels, Attack/Auto-resolve Battle/AI: Finish Turn captions, four melee/ranged/spell equipment readouts for company and legacy skirmish inventories. Core command/stat/name/species font sizes and accepted setup portraits remain unchanged.
- Modified `Assets/Scripts/Subwindows/InfoPanel.cs` and `Assets/Scripts/UI/UnitInfoPanel.cs`: populate the tactical readout, retain item hover lookup and use 85% trait text only in the tactical inspection panel. Other unit details remain full size.
- Modified `Assets/Scripts/UI/MessageLogPanel.cs`: font 28 to 22, compact height 160 to 260, retained after full-view restore, to provide at least 8 ordinary lines. Modified `Assets/Scripts/UI/RightClickMenu.cs`: shared Attack selection/previews and styled dynamic buttons. Modified `Assets/Scripts/UI/Connectors/AdvancedUnitCommands.cs`: shared blue dynamic ability buttons with subtle category tint, preserving their fonts/sizes/callbacks.
- See [implementation and combined review](docs/implementation/16-tactical-interface.md). Unity 2018.3.12f1 clean compile/read-only base scene check passed; no actual visual/attack playtest claimed. Fresh private Win64 build passed return 0 without logged C#/shader errors; scoped hashes confirm only the intended six existing sources plus the new source/meta, no missing or other game files/settings changed.
- GPL source-change review: actual License.txt sections 4–5 read; dated notices on all 7 source files, new fork addition identified and exact source/meta paths recorded here. Earlier notices/licensing/attribution preserved. No copied external code, assets or dependencies; no source-art/scene/prefab/settings edits. Existing distribution/provenance gates remain open; this local review build is not release certification.

## 2026-11-02 — Correct sample-unit clearance inside its box

- Modified `Assets/Scripts/Subwindows/SingleBattleSetupView.cs`: move the doll's local top offset150to139, upward11reference units (about14.85screen pixels at1080p), while keeping the card position/dimensions and2xscale unchanged. The previous whole-card nudge moved its child unit too and did not change feet clearance; this change adjusts the unit relative to the box.
- ActualLicense.txt4–5 reviewed; dated notice/exact path recorded, earlier notices/licensing/attribution preserved. No external additions or distribution; existing provenance gates remain open. Standalone build passed return0/no logged C# or shader errors; scoped review confirms only the intended source changed, no added/missing/other game files/settings. Feet clearance still needs visual confirmation.

## 2026-11-02 — Nudge Single Battle sample boxes downward

- Modified `Assets/Scripts/Subwindows/SingleBattleSetupView.cs`: card top offset-8to3, moving both complete preview cards11reference units downward (about14.85screen pixels at1080p). Size180square,2xscale and doll centering within each card remain unchanged. No other layout/behavior changes.
- ActualLicense.txt4–5 reviewed; dated source notice/exact path recorded, prior notices/licensing/attribution preserved. No external additions or distribution; existing provenance gates remain open. Standalone build passed return0 without logged C#/shader errors; scoped review confirms only the intended source change, no other game files/settings or missing paths. Actual visual position remains for user review.

## 2026-11-02 — More room for 2x Single Battle samples

- Modified `Assets/Scripts/Subwindows/SingleBattleSetupView.cs`: increase cards168to180reference pixels while preserving lower-left location; approximately16extra screen pixels at1080p with the existing Expand scaler. Recenter the2x doll, narrow Race/AIEnabled rows, move the options viewport/scrollbar16reference pixels down, and widen panel backgrounds12to contain the expanded cards. All fields and scroll access retained; no rendering/artwork/gameplay changes.
- ActualLicense.txt4–5 reviewed; dated notice/exact source path recorded, earlier notices/licensing/attribution preserved. No external additions or distribution; inherited provenance gates remain open. Current standalone build completed return0, no logged C#/shader errors; scoped review confirms the single intended source change and no other asset/settings/additions/missing paths. Visual fit remains pending.

## 2026-11-02 — Single Battle caption-free 2x previews

- Modified `Assets/Scripts/Modes/StartMode.cs` to remove the temporary Portrait Comparison main-menu hook; tool source is retained, no files deleted.
- Modified `Assets/Scripts/Subwindows/SingleBattleSetupView.cs` to hide species captions, retain their callback references, and set both sample dolls to2x UI scale centered at84,144 in the168square card (includes the renderer's60pixel scaled upward offset). Existing species dropdowns, artwork and adaptive sampling remain; roster alignment is unchanged.
- GPL source-change review: actual License.txt4–5 reviewed, dated notices and exact paths here, earlier attribution/notices/licensing retained. No external additions or distribution; existing provenance/distribution gates remain open. Fresh private Windows build completed return0 without logged C#/shader errors; scoped review confirms only the two intended source changes, no other asset/settings or missing paths. Actual portrait fit remains for visual review.

## 2026-11-02 — Pixel-art preference: crisp magnification, smoothing only when shrinking

- Modified `Assets/Resources/Sprites-ColorSwap.shader` to point-sample full-resolution texel centers at native/larger screen size and smooth decoded colors only when both axes strictly minify. Uses screen derivatives with a conservative native-size cutoff, explicit base mip and palette-aware premultiplied interpolation; preserves original blend/stencil/PixelSnap behavior. Shader target3.0 required. No source art or import-setting changes.
- Modified `Assets/Scripts/Races/Graphics/SpriteContainer.cs` to use Simple UI paper-doll images (user's B preference). Modified `Assets/Scripts/Subwindows/PortraitComparisonPanel.cs` to keep A/B fixed crisp references, with A legacy Sliced and B Simple; C/D remain always-smoothed comparisons. No roster alignment/size, camera zoom-step, save or gameplay changes.
- Unity2018.3.12f1 compile/read-only scene/texture audit passed; no headless check is claimed as GPU visual/performance verification. See [15E user choice and adaptive rendering](docs/implementation/15e-portrait-rendering-comparison.md). No new tests/framework or player build.
- GPL source-change review: actual License.txt4–5 reviewed; dated notices and exact source paths here, original notices/licensing/attribution preserved. No external additions, deletion/move or publication; existing provenance/distribution gates remain open.

## 2026-11-02 — Spec15E: portrait rendering investigation and comparison

- Modified `Assets/Scripts/Modes/StartMode.cs` to add a temporary Portrait comparison entry. Added `Assets/Scripts/Subwindows/PortraitComparisonPanel.cs` and `Assets/Scripts/Subwindows/PortraitComparisonPanel.cs.meta` for matched-appearance A/B/C/D cards, species/size controls, raw-source matching and session-only preference marking.
- Added `Assets/Resources/PortraitPaletteSmooth.shader` and `Assets/Resources/PortraitPaletteSmooth.shader.meta`, derived from retained Sprites-ColorSwap.shader with its original upgrade notice preserved, for experimental palette-aware/base-mip interpolation. No shared materials/textures, original artwork or production rendering changed.
- Added `Assets/Editor/PortraitRenderingAudit.cs` and `Assets/Editor/PortraitRenderingAudit.cs.meta` for read-only bundle/source dimensions inspection. Final Unity2018compile/scene/bundle audit passed after correcting an editor API mismatch; no actual GPU/Play-mode comparison claimed. See [evidence, options and review](docs/implementation/15e-portrait-rendering-comparison.md). Company/recruitment alignment accepted; larger row layout deferred.
- GPL source review: actual License.txt4–5 reviewed; dated source/shader notices, new fork additions/derivation identified, exact paths/meta associations above. Existing notices/attribution/licensing retained; no third-party additions. Existing provenance/distribution gates remain open. No file deletion/move or publication.

## 2026-11-02 — Portrait alignment and magnification feedback

- Modified `Assets/Scripts/Subwindows/SingleBattleSetupView.cs`: reduce preview magnification1.4to1.0 and recenter its anchor104to94, preserving the enlarged card and original sprite assets/filtering. This reduces enlargement of existing pixels; it does not increase source-art resolution.
- Modified `Assets/Scripts/Subwindows/CompanyManagementUI.cs`: move shared company/recruitment/detail portraits18reference pixels upward, keeping their enlarged scale and horizontal clearance.
- Source-only adjustment; no new compile/test/Play-mode run. Dated notices and exact paths recorded after reviewing License.txt sections4–5; prior notices/licensing/attribution preserved, no external additions. Existing provenance/distribution gates remain open.

## 2026-11-02 — Larger paper-doll previews

- Modified `Assets/Scripts/Subwindows/SingleBattleSetupView.cs`: enlarge preview cards into the heading space, increase doll scale0.75to1.4, and compensate for the renderer's30pixel upward offset. Keep names below the artwork.
- Modified `Assets/Scripts/Subwindows/CompanyManagementUI.cs`: increase shared company/recruitment doll scale0.4to0.9 and center dolls within rows using the renderer offset; give recruitment descriptions extra left clearance and larger detail portraits (1.6scale). No appearance generation, statistics or gameplay changes.
- Source diff reviewed; no compile/Play-mode test performed for these presentation changes. Focused visual confirmation remains pending. Actual License.txt sections4–5 reviewed; dated notices/exact paths recorded, existing notices/licensing/attribution preserved. No external additions; inherited distribution/provenance reviews remain open.

## 2026-11-02 — Spec 15D: Single Battle setup layout

- Modified `Assets/Scripts/Subwindows/CreateTacticalGame.cs` to host the new runtime presentation and refresh existing display/dependency handlers, with inactive-child access for two retained label updates. Added `Assets/Scripts/Subwindows/SingleBattleSetupView.cs` and `Assets/Scripts/Subwindows/SingleBattleSetupView.cs.meta`: centered blue layout, top core controls, Attackers/Defenders columns with both original species previews, independent scrolling, two subtle dividers, bottom AI-watch choice and all conditional bulk actions.
- Retained all29inputs,6original action buttons, percentage readouts, species restrictions, values/ranges/options and callbacks. Battle construction, validation, batching, gameplay and global preference semantics remain. No fields removed, no new artwork/dependencies or serialized scene/prefab changes. See [15D implementation and focused review](docs/implementation/15d-single-battle-setup.md).
- Unity2018.3.12f1 compile/read-only scene passed, return0, no new normalized compiler warnings; inspected references/72terrain sprites retained. Scoped review confirms all fields accounted for, unchanged public declarations/generation and no other Assets/Packages/ProjectSettings changes or missing paths. Actual graphical field/preview/dropdown/battle entry review remains pending. No new tests/framework, bulk jobs, agent Play-mode check or player build.
- GPL source-change review: actual License.txt sections4–5 reviewed, dated notices on both C#sources and exact associated meta path above; new source identified as this fork's addition, prior notices/licensing/attribution preserved. No external code/assets/dependencies or distribution. Existing provenance/distribution gates remain open; no deletion/move/commit/publication.

## 2026-11-02 — Main-menu sizing and supplied Vore Warband logo

- Modified `Assets/Scripts/Modes/StartMode.cs`: larger main/corner buttons and32-point labels,16-point row gaps, Credits/Help separation, and the user-supplied logo replacing old title text through a noninteractive aspect-preserving Image. Preserved button wording/order/actions and existing load/save/settings behavior. See [15C feedback record](docs/implementation/15c-main-menu-redesign.md).
- Added `Assets/Resources/UI/VoreWarbandLogo.png` by byte-for-byte copy of the attached user-provided2172×724RGBA artwork, plus `Assets/Resources/UI/VoreWarbandLogo.png.meta` and `Assets/Resources/UI.meta` for single-sprite UI import. The PNG itself is unmodified (SHA256 a8fbea4f27b0e83912bab56aa34dcc4497e74607ad0d4b8433adde68ae91e3f9). This entry associates the new non-commentable asset/import metadata with this dated fork addition.
- Unity2018.3.12f1 compile/import/read-only scene passed, return0, with no new compiler warnings and retained inspected references/72terrain sprites. Scoped review confirms only StartMode plus the three intended asset/metadata additions; no other asset or scene changes. Actual logo appearance/spacing awaits one focused review; no new tests/framework, Play-mode pass or player build.
- GPL source-change review: actual License.txt sections4–5 reviewed; dated source notice and exact paths recorded here; earlier notices/licensing/attribution retained. No external code/dependency additions. Supplied artwork has no embedded author/license metadata: record its origin and obtain applicable redistribution/attribution evidence before release; do not assume a GPL grant for it. Existing distribution/provenance gates remain open. No publication or file deletion/move.

## 2026-11-02 — Spec 15C: main menu presentation

- Modified `Assets/Scripts/Modes/StartMode.cs`: shared blue styling, smaller captions, elevated central New Campaign / Single Battle / Load Saved Game / Options order, retired visible presets/startup listeners and displayed Version: 1.0. Preserved title and peripheral Credits/Help/Exit/developer transforms, selected race-edit slot and existing setup/options callbacks. Uses Expand on the existing reference canvas.
- Modified `Assets/Scripts/Subwindows/CompanyCampaignView.cs` to expose the existing checkpoint browser, and connected the main-menu load route to it in StartMode instead of the retired legacy save screen. No new save format or load policy. Modified `Assets/Scripts/Core/State.cs` to clear routine race-loading success text and match two guidance messages to the renamed/current buttons; actual race loading and internal version identifiers remain.
- See [15C implementation and focused review](docs/implementation/15c-main-menu-redesign.md). No new assets/dependencies, scene/prefab rewrite or broad Help change. Content Settings accepted; current main-menu visual/interaction review remains pending.
- Final Unity2018.3.12f1 compile/read-only scene passed, return0, without new normalized compiler warnings; inspected references/72terrain sprites retained. Source/notice/wiring audit confirms exactly three intended source changes and no other asset/package/ProjectSettings changes or missing paths. No new tests/framework, Play-mode/visual loading test, personal campaign load or player build.
- GPL source-change review: actual License.txt sections 4–5 reviewed; dated notices on all three source files with exact paths here, earlier notices/licensing/attribution retained. No external additions or distribution. Inherited provenance/distribution-method gates remain open.

## 2026-11-02 — Clarify the global-default button

- Modified `Assets/Scripts/Subwindows/ContentSettingsView.cs`: renamed “Apply to all games” to the exact user-requested “Apply now and as global default”. Existing ExitAndSave callback and other behavior unchanged. Source diff reviewed; no new compile/tests or Play-mode check for this label-only edit.
- GPL source-change review: reviewed actual License.txt sections 4–5; updated dated modification notice and recorded the exact source path here. Prior licensing/attribution/notices preserved; no external additions or distribution. Existing provenance/distribution gates remain open.

## 2026-11-02 — Content Settings apply-button labels

- Modified `Assets/Scripts/Subwindows/ContentSettingsView.cs`: “Apply and close” is now “Apply to this game” (existing `ContentSettings.Exit` callback); “Save and close” is now “Apply to all games” (existing `ContentSettings.ExitAndSave` callback). Traced both through SetValues and ContentSettingsData.SavePreferences: both apply the current settings, while only ExitAndSave persists global preferences. Other saved game files are not rewritten. No callback, setting/storage, layout or normal Options label changes.
- Source mapping/diff reviewed; no new compile, tests, Play-mode run or manual test round for two labels. GPL review: actual License.txt sections 4–5 reviewed; dated notice and exact source path recorded, prior notices/licensing/attribution retained, no third-party additions or distribution. Existing provenance/distribution gates remain open.

## 2026-11-02 — Content Settings terminology correction

- Modified `Assets/Scripts/Subwindows/ContentSettingsView.cs` to restore the preferred herm/herms wording in five labels expanded during the redesign. Modified `Assets/Scripts/Utility/Texts/DefaultTooltips.cs` to use Herm in the corresponding tooltip and correct diegesting → digesting, vales → values, occuring → occurring, and effect → affect in “affect starting Exp”. No gameplay, settings, layout or identifier changes.
- Recorded the user's direction in `AGENTS.md`: preserve terminology and existing wording; freely correct outright typos, and ask about ambiguous wording. Broad Help rewriting remains deferred. Source diff reviewed; no compile/test/playtest run for these text-only edits and no new manual testing round requested.
- GPL source-change review: actual License.txt sections 4–5 reviewed; both source files have dated conversion notices, paths recorded here, existing notices/attribution/licensing retained. No external code/assets/dependencies or distribution; inherited provenance/distribution review remains open.

## 2026-11-02 — Spec 15B: Content Settings redesign and visible checkmarks

- Modified `Assets/Scripts/Subwindows/ContentSettings.cs` and added `Assets/Scripts/Subwindows/ContentSettingsView.cs` with `Assets/Scripts/Subwindows/ContentSettingsView.cs.meta`: centered blue scroll pages, smaller text, retained top page dropdown without subcategories, 50 original controls plus curated species choices, and existing load/apply/save paths. Retired the nine user-listed controls; retained settings and generation restrictions remain.
- Modified `Assets/Scripts/Core/ContentSettingsData.cs` so those nine retired preferences use their existing code defaults, without loading/writing old overrides or deleting preference keys. No new defaults, species approvals or unrelated balance changes.
- Modified `Assets/Scripts/UI/UITheme.cs` and `Assets/Scripts/Subwindows/OptionsView.cs`, and added `Assets/Scripts/UI/SettingsCheckmark.cs` with `Assets/Scripts/UI/SettingsCheckmark.cs.meta`: shared bright vector checkmarks for both menus/dropdown items and unchanged corrected slider geometry. No external assets/dependencies. See [implementation and focused review](docs/implementation/15b-content-settings-redesign.md).
- Final Unity 2018.3.12f1 compile/read-only scene passed, with no new compiler warnings and retained inspected references/72 terrain sprites. Scoped review accounts for all retained controls and unchanged existing defaults; no unrelated asset/package/ProjectSettings changes. Actual graphical behavior and preference persistence await one focused player review. No new tests/framework or player build.
- GPL source-change review: actual License.txt sections 4–5 reviewed, dated notices on all six C# sources and exact associated meta paths above, additions identified as this fork's work, prior notices/licensing/attribution preserved. Inherited provenance/distribution-method gates remain open. No deletion/move or publication.

## 2026-11-02 — Options feedback: aligned sliders, larger hover text and vore terminology

- Modified `Assets/Scripts/Subwindows/OptionsView.cs` to remove inherited2xslider track/fill scaling and align background/fill/handle on the same8px-inset rail. Normalize their transforms and initialize the fill/handle from the existing normalized value; preserve numeric min/max/current values, direction and callbacks. Hover text rises from15to19points. Labels now read AI vore actions and Vore volume.
- Modified `Assets/Scripts/Utility/Texts/HelpText.cs`, `Assets/Scripts/Utility/Texts/HoveringTooltip.cs` and `Assets/Scripts/AI/TacticalAI.cs` to restore vore in player-facing help, the downed-unit tooltip and the retreat log. This is terminology correction, not the deferred Help rewrite or a change to tactical mechanics. Recorded the explicit player-facing terminology preference in AGENTS.md (documentation).
- User finds the remaining Options presentation mostly okay; beautification is deferred. Final Unity2018.3.12f1 compile/read-only scene passed with retained references/72terrain sprites. Source/geometry/preference-scope review performed; actual slider endpoints/fill alignment and hover readability need one focused visual check. No new tests, data regression rerun or current player build.
- GPL source-change review: actual License.txt4–5 reviewed, dated notices on all4sources and exact paths here; prior notices/licensing/attribution/warranty terms retained. No external source/assets/dependencies introduced. Inherited provenance/distribution-method gates remain open. No path deletion/move, scene/prefab/package changes, commit or publication.

## 2026-11-02 — Spec15A: categorized blue Options menu

- Modified `Assets/Scripts/Subwindows/Options.cs` to host a centered single-column presentation while preserving normal preference storage, save/close and maintenance behavior, including the active company's revival setting.
- Added `Assets/Scripts/Subwindows/OptionsView.cs` and `Assets/Scripts/Subwindows/OptionsView.cs.meta` for a scrollable categorized layout reusing32retained controls: Move Delays, Audio, Graphics, Controls, context-specific Campaign, and approved Maintenance. Four requested controls are retired from the visible page; historical references/values remain, without an unrelated balance/config reset.
- Added `Assets/Scripts/UI/UITheme.cs` and `Assets/Scripts/UI/UITheme.cs.meta` for the reusable restrained blue palette, smaller typography and aligned control styling. No other screens, scene/prefab/art/packages or dependencies changed. See [implementation and focused review](docs/implementation/15a-options-redesign.md).
- Actual final Unity2018.3.12f1 compile/read-only scene and existing32managed checks passed; no new test cases/framework. New graphical Options/dropdown/persistence/return workflows await one player review. No current player rebuild or maintenance operation performed.
- GPL source-change review: actual License.txt4–5 reviewed; dated notices on all3sources, new sources identified as this fork's additions and exact associated meta paths above. Earlier notices/license/attribution/warranty terms preserved; no external copied code/assets/dependencies. Inherited provenance/distribution-method review remains open. No path deletion/move, commit or publication. Help prose remains deferred.

## 2026-11-02 — Spec14 feedback: centered draggable region and normal Options

- Modified `Assets/Scripts/Subwindows/CompanyCampaignView.cs` for a bounded centered campaign frame, top Contracts/Records buttons, lower active-contract strip and removal of the right column. Added a clipped348-control map pool and normal Options handoff/return. Retained terrain/fog/adjacency and existing campaign actions.
- Added `Assets/Scripts/UI/CompanyMapScroll.cs` and `Assets/Scripts/UI/CompanyMapScroll.cs.meta` for left/middle dragging and drag-release click suppression, without spending actions or altering the campaign.
- Modified `Assets/Scripts/Subwindows/Options.cs` and `Assets/Scripts/Subwindows/CompanyMissionsUI.cs` to integrate the active company's leader-revival preference into normal Options and retire its separate campaign page. Global preferences remain separate; setup/creator cannot target a stale hidden company.
- Modified `Assets/Scripts/Core/CompanyCampaign.cs` to generate80x60new maps with exactly6towns, per the user's size/density clarification. Modified `Assets/Scripts/Core/CompanyCampaignSave.cs` to resume validated supported map sizes; existing maps keep their saved geometry and towns.
- [Feedback implementation and focused review](docs/implementation/14b-centered-map-and-options.md). Final Unity2018.3.12f1 compile/read-only scene and existing32managed checks passed; actual generated80x60/6town save/resume values inspected. New runtime layout/drag/Options behavior awaits player review. No new test cases/framework or current player rebuild.
- GPL source-change review: actual License.txt4–5 reviewed, dated notices on6affected sources, new source identified as this fork's addition and meta associated above. Earlier notices/licensing/attribution/warranty preserved; no external code/assets/dependencies introduced. Inherited provenance/distribution-method gates remain. No file deletion/move, scene/prefab/art/package changes, commit or publication. Help prose remains deferred.

## 2026-11-02 — Spec14: user-defined campaign interface

- Modified `Assets/Scripts/Subwindows/CompanyCampaignView.cs`, `Assets/Scripts/Subwindows/CompanyManagementUI.cs`, `Assets/Scripts/Subwindows/CompanyMissionsUI.cs` and `Assets/Scripts/Subwindows/CompanyRecoveryUI.cs` for persistent centered Company/Map/Town navigation, a larger retained-art map, grouped bottom company/calendar/finance status, town service column and direct roster level-up actions. Removed redundant general Return/World map buttons while preserving task-specific decisions/deployment and creator contexts. Reused retained terrain/settlement references; no assets or scenes changed.
- Modified `Assets/Scripts/Core/CompanyCampaign.cs` to require the full daily action budget before starting rest, in addition to UI eligibility. End turn remains distinct and does not set the resting flag. Rest1day/custom duration remain; queued Resume/Cancel display only when relevant. No save-schema change.
- Related [spec14](docs/specs/14-campaign-interface.md) and [implementation/player review](docs/implementation/14-campaign-interface.md). Actual final Unity2018.3.12f1 compile/read-only scene check passed; inspected references/72terrain retained. Existing32managed regression checks passed; no new tests/framework. Initial local-variable compile error corrected before successful checks. Runtime layout/navigation/full-budget-rest behavior awaits one combined player review; no current player rebuild.
- GPL source-change review: actual License.txt sections4–5 reviewed, dated notices on all5modified sources and exact paths here, earlier notices/license/attribution/warranty terms preserved. No external source/assets/dependencies added. Inherited provenance/distribution-method gates remain open. No file deletion/move, commit/push or publication. Help prose rewrite remains deferred.

## 2026-11-02 — Spec13 accepted with lighter slug encounters

- Modified `Assets/Scripts/Core/CompanyRoadEvents.cs`: random-encounter power fraction reduced from0.80 to0.56, a30% reduction. For equivalent company/slug power, an encounter that previously rounded to7slugs now rounds to5. Company scaling, leader weighting, slug stats, contract generation, event frequency/cooldown and duel outcomes are unchanged. Already generated/saved forces retain their recorded roster; this tuning applies to new generation.
- User reports the slice basically good and accepts it with this adjustment, explicitly waiving an additional test. Spec13 acceptance and deferred help rewrite recorded in the implementation/resume documents. No new compile/build/playtest or tests were run for this constant-only follow-up; previous phase checks remain historical evidence.
- GPL source-change review: actual License.txt sections4–5 reviewed; dated conversion notice and this exact source record added, earlier notices/license/attribution/warranty terms preserved. No external source/assets/dependencies introduced. Inherited provenance and distribution-method review remain open before distribution. No file deletion/move, commit or publication.

## 2026-11-02 — Spec13: road encounters, practice duel and per-level XP

- Added `Assets/Scripts/Core/CompanyRoadEvents.cs` and its `Assets/Scripts/Core/CompanyRoadEvents.cs.meta` Unity identity for approved saved travel events: forced wild Spitter Slug combat and a two-mercenary practice-duel choice. Includes provisional travel frequency/cooldown, weighted full-company scaling, exact shared 50/50 duel outcomes and serialization validation. No legacy event definitions returned.
- Modified `Assets/Scripts/Core/CompanyCampaign.cs`, `Assets/Scripts/Core/CompanyContracts.cs`, `Assets/Scripts/Core/CompanyBattle.cs`, `Assets/Scripts/Core/CompanyCampaignSave.cs`, `Assets/Scripts/Subwindows/CompanyCampaignView.cs` and `Assets/Scripts/Subwindows/CompanyMissionsUI.cs` to persist/route events, share approved enemy creation and tactical fate/deployment/return, and keep road combat separate from contract payments/reputation/counts/bonus XP. Event occurrence and committed travel save before presentation; existing contract saves remain supported.
- Modified `Assets/Scripts/Entities/Units/Unit.cs`, `Assets/Scripts/Subwindows/CompanyManagementUI.cs` and `Assets/Scripts/Subwindows/InfoPanel.cs` to show per-level XP progress/cost with surplus preserved after leveling. Cumulative save totals, leveling choices and existing pacing remain unchanged. Help rewriting remains deferred.
- See [implementation, tuning and player review](docs/implementation/13-road-events-and-xp.md). Actual checks: Unity2018.3.12f1 compile/read-only scene audit passed after a network-enabled license renewal retry; no new compiler warnings, retained72terrain and scene references; existing32managed regression checks passed. New live events/XP UI await one combined review. No current player rebuild or new tests/framework.
- GPL source-change check: actual License.txt sections4–5 reviewed, dated notices on all10affected C#sources and exact associated meta path here; earlier notices, license/attribution/warranty terms preserved. New source identified as this fork's addition; no external source/assets/dependencies introduced. Inherited provenance and distribution-method review remain gates. No file deletion/move, commit/push or publication.

## 2026-11-02 — Spec12 acceptance and payday visibility

- User reports all combined review results good with no material issues. This records their confirmation, without claiming unreported specialist combat cases or release approval.
- Modified `Assets/Scripts/Subwindows/CompanyCampaignView.cs` to show a non-interrupting world-map countdown above the contract list: Payday in 7 days down to 1 day, then back to 7 after the existing weekly wage stage. Uses saved NextPayday minus ElapsedDays, including after rest/loading; a due but unprocessed payday reads Payday due. Map-only previews omit it. Wage timing, amounts, debt and saves are unchanged.
- Help copy is explicitly deferred for a later user-led rewrite: remove design/developer commentary and unnecessary detail. No help strings changed in this batch.
- GPL source-change review: actual License.txt sections 4–5 reviewed; dated conversion notice and exact source record added, earlier notices/licensing/attribution preserved. No external code/assets/dependencies added. Inherited provenance and distribution-method review remain release gates.
- This UI-only feedback batch was reviewed against its task-start source diff; no compilation, new player build or playtest was run. The earlier Win64 build predates this countdown. No files deleted/moved, commit or publication.

## 2026-11-02 — Spec12: integrated campaign review and current help

- Modified `Assets/Scripts/Utility/Texts/HelpText.cs` and `Assets/Scripts/UI/HelpPanel.cs` to replace conquest/village/diplomacy/training instructions with the implemented company loop, wages/recovery, combat/fates, progression, save/Ironman limits and project/license information. Corrected the invalid Race-array cast in trait enumeration, filtered retired references and deferred trait/spell reference generation until selected.
- Modified `Assets/Scripts/Subwindows/CompanyCampaignView.cs`, `Assets/Scripts/Subwindows/CompanyMissionsUI.cs` and `Assets/Scripts/Subwindows/CompanyManagementUI.cs`: twelve paged company-help chapters with safe setup/creator/decision/result return; Retry checkpoint reachable on every company page, only while a write is actually failed; shared resume validation; independent contract-expiry acknowledgment instead of wage controls. Creator help returns to the existing draft without regenerating it.
- Modified `Assets/Scripts/Core/CompanyCampaignSave.cs` to centralize prospective load validation, enforce matching company folders/supported dimensions and latest-valid-only Ironman resumption, and extend the existing serialization preservation guard to mana, level/XP, raw stats, equipped item identities/uses/cooldowns. Older valid generations remain intact for recovery; live battle saving still uses the accepted deployment fallback.
- Modified `Assets/Scripts/Core/CompanyInjuries.cs` to restore the original Army.Refresh rate of60%maximum mana per elapsed day through the single resumable recovery stage for members and active targets. No HP, injury, wage or XP tuning changes. Modified `Assets/Scripts/Entities/Units/Actor_Unit.cs` so summons entering the fear/nonlethal downed path disappear instead of retaining a permanent-character downed state. Ordinary downed characters remain alive and latched out of action.
- Modified `Assets/Scripts/Core/State.cs` to replace obsolete company-not-available messages with current checkpoint guidance. No source additions, scenes, prefabs, artwork, packages or dependency changes.
- Related [spec12](docs/specs/12-mvp-review-and-help.md), [integration report](docs/implementation/12-integrated-review-and-help.md) and [release-readiness record](docs/release-readiness.md). Managed data, Unity compile/scene, current Win64 player build and headless startup checks are recorded separately from pending graphical player/battle checks. The private build includes the root license/change record and inspected third-party notices; full distribution/provenance obligations remain release gates. No publication or release claim.
- GPL source-change check: all nine modified C# sources have2026-11-02conversion notices, meaningful earlier legal/modification notices remain and exact paths are recorded here. Actual License.txt sections4–5 reviewed; original license/attribution/warranty terms retained. No copied external code/assets/dependencies. Separate inherited provenance, interactive-notice and distribution-method obligations remain release gates; this is not a blanket distribution compliance certification. No deletion/move, commit or publishing action.

## 2026-11-02 — Spec11 feedback: injury outline and final positioning

- Modified `Assets/Scripts/Races/Graphics/UnitSprite.cs`: moved injury text from1tile to0.75tiles right of the unit (a quarter tile left), increased fixed font size from1.5to2.5, and explicitly enabled the text shader's outline with opaque black and a minimum0.2outline width. Outline settings apply to the independent injury material. The accepted2.5-second timing, left alignment and ordinary combat text remain unchanged.
- Related [spec11 feedback report](docs/implementation/11-feedback-progression-and-notices.md). Dated conversion notice and exact source record added; earlier notices, license, attribution and warranty terms preserved. Actual License.txt sections4–5 reviewed. No external code, assets, dependencies, scene/prefab edits, deletion/move, commit or publication. Inherited provenance and distribution review remain release gates; source-change review is not distribution certification.

## 2026-11-02 — Spec11 feedback: compact injury text to the right

- Modified `Assets/Scripts/Races/Graphics/UnitSprite.cs` to disable injury-label auto-sizing and use a fixed1.5font size, substantially smaller than the previous6.5ceiling. The label now has a left-edge pivot, explicit left alignment and one-line text, starting one tactical world unit to the injured unit's right at the same height. Existing canvas draw depth is retained. This replaces the centered, above-unit placement; the accepted2.5-second timing and independent damage/healing labels remain unchanged.
- Related [spec11 feedback report](docs/implementation/11-feedback-progression-and-notices.md). User accepts popup timing, XP functionality and Departures; size/placement awaits focused visual confirmation.
- GPL source-change check: dated conversion notice added, meaningful earlier notices preserved and exact source identified here. Actual License.txt sections4–5 reviewed; original license/attribution/warranty terms retained. No external code/assets/dependencies, prefab/scene changes, file deletion/move, commit or publication. Inherited provenance/distribution review remains a release gate.

## 2026-11-02 — Spec11 feedback: readable injury notices

- Modified `Assets/Scripts/Races/Graphics/UnitSprite.cs` to display each queued injury in its own runtime text component above the ordinary combat numbers. Reuses the installed font/style with a65% font-size ceiling, a wider text area and an independent color material; no prefab or art changes. Each injury remains steady for2.5unscaled seconds, independent of repeated sprite updates or later damage/healing. Ordinary damage/healing setup, font and0.75-second timer remain unchanged.
- User confirmed XP is functional with later tuning expected, and the Departures screen is accepted. Only injury-text readability remains for focused visual confirmation. Related [spec11 feedback report](docs/implementation/11-feedback-progression-and-notices.md).
- GPL source-change check: dated conversion notice added, meaningful earlier notices and license/attribution/warranty terms preserved, exact source identified here. Actual License.txt sections4–5 reviewed; no copied external code, new assets or dependencies. Inherited provenance and distribution-method obligations remain release gates. No file deletion/move, commit or publication.

## 2026-11-01 — Spec11 feedback: progression and battle/departure feedback

- Modified `Assets/Scripts/Entities/Units/Unit.cs` and `Assets/Scripts/Core/CompanyEconomy.cs`: company XP thresholds now use a provisional cumulative exponential curve independent of retired world caps; weaker-enemy action XP and lower-rated contract completion XP diminish for veterans. Surviving deployed starters qualify for their first level-up on completing a contract; existing player choices remain. Rules-version4 migration repairs earlier sentinel-valued veteran XP without discarding ordinary earned XP or changing levels. Modified `Assets/Scripts/Core/CompanyBattle.cs` for once-only contract completion XP and result feedback, and `Assets/Scripts/Entities/Units/Actor_Unit.cs` to correct spell XP calculations that compared the target's level with itself.
- Modified `Assets/Scripts/Core/CompanyInjuries.cs` and `Assets/Scripts/Races/Graphics/UnitSprite.cs`: each new injury creates a named informational battle-log entry and queues orange tactical text after damage numbers. Hidden/contained units do not display a map popup, and instant simulation skips visual notifications while preserving log entries. No prefab/artwork changes.
- Modified `Assets/Scripts/Core/CompanyCampaign.cs`, `Assets/Scripts/Core/CompanyContracts.cs`, `Assets/Scripts/Core/CompanyCampaignSave.cs`, `Assets/Scripts/Subwindows/CompanyCampaignView.cs` and `Assets/Scripts/Subwindows/CompanyManagementUI.cs`: rules-version4 state, a distinct OK-only departure notice, and persistence/routing for a separate simultaneous wage warning when necessary. No further campaign day is consumed between those notices; the promised payment grace day remains available. Departure text no longer says departed mercenaries are willing to wait or offers payment controls.
- Related spec11; [feedback report](docs/implementation/11-feedback-progression-and-notices.md) records source trace, initial tuning, checks and focused player review. All11modified C# sources have dated conversion notices; prior notices and original license/attribution/warranty terms are preserved. Actual License.txt sections4–5 reviewed; no outside code/assets/dependencies or new licensing terms. Inherited provenance and section5(d)/6 distribution review remain release gates, not a completed distribution certification. No source additions, file deletion/move, publication or commit.

## 2026-11-01 — Spec11: persistent injuries, recovery and downtime

- Added this fork's `Assets/Scripts/Core/CompanyInjuries.cs` and `Assets/Scripts/Subwindows/CompanyRecoveryUI.cs` for independent minor/serious/old-injury records, derived stacking penalties, body-part armor risk reduction, daily company/enemy recovery, healthy-member sparring, injury inspection and paid per-member town treatment. Associated non-commentable metadata: `Assets/Scripts/Core/CompanyInjuries.cs.meta`, `Assets/Scripts/Subwindows/CompanyRecoveryUI.cs.meta`. Numerical values are provisional.
- Modified `Assets/Scripts/Entities/Units/Unit.cs` and `Assets/Scripts/Entities/Units/Actor_Unit.cs` to roll injury risk once per actual lasting-damage entry (plus one deep-attrition opportunity per battle), serialize the injury backing field, apply combat penalties without modifying base stats/maximum HP, and detach cloned injury records. Temporary HP shields injury risk; ordinary zero HP remains downed, not dead. Modified `Assets/Scripts/Subwindows/InfoPanel.cs` for injury inspection in both game modes.
- Modified `Assets/Scripts/Core/CompanyCampaign.cs`, `Assets/Scripts/Core/CompanyEconomy.cs` and `Assets/Scripts/Core/CompanyContracts.cs` for version3 migration, saved explicit resting-day intent and the existing resumable clock. Modified `Assets/Scripts/Core/CompanyCampaignSave.cs` to verify injury/treatment/immunity and interrupted recovery progress before publishing checkpoints and after readback. Modified `Assets/Scripts/Core/CompanyBattle.cs` to clear leader wounds on special revival and consolidate target healing into the shared recovery stage. Modified `Assets/Scripts/Core/CompanyEquipment.cs` for safe armor coverage in nine-slot company and original Pure Tactical inventories.
- Modified `Assets/Scripts/Subwindows/CompanyCampaignView.cs`, `Assets/Scripts/Subwindows/CompanyManagementUI.cs` and `Assets/Scripts/Subwindows/CompanyMissionsUI.cs` for chosen-day rest, town treatment, roster/member XP and injury details, and final-deployment injury counts. Existing contract, wages/debt, terrain, species eligibility, actual digestion/fate and battle-time rules remain in use.
- Related specification: [11](docs/specs/11-injuries-rest-and-progression.md). Source trace, tuning and grouped review: [implementation report](docs/implementation/11-injuries-rest-and-progression.md). User accepted the preceding HP/naming feedback before this milestone.
- GPL source-change check: all14 C# sources have dated fork notices; earlier legal/modification notices and original license/attribution/warranty terms are preserved. Exact source and new metadata paths are identified here. Actual License.txt sections4–5 reviewed; no outside code, assets or dependencies introduced. Inherited asset/dependency provenance and section5(d)/6 distribution review remain release gates; this is not a blanket distribution compliance certification. No file deletion/move, publication or commit.

## 2026-11-01 — 09–10 feedback: HP persistence and individual names

- Modified `Assets/Scripts/Entities/Units/Unit.cs` to serialize the HP backing field explicitly, retaining an alias for the earlier Health field. The installed Odin version ignores custom-accessor properties, which caused both checkpoint and detached-battle-copy HP loss. Modified `Assets/Scripts/Core/CompanyCampaignSave.cs` and `Assets/Scripts/Core/CompanyBattle.cs` to verify health/fate values on checkpoint readback and battle copying. No blanket healing or existing checkpoint repair; the user prioritized future games.
- Modified `Assets/Scripts/Core/SpeciesPolicy.cs` for singular spaced display labels and reversible label-to-enum parsing; all species IDs, keys and eligibility remain unchanged. User-confirmed labels include Matron's Minion, T. rex and Space Roach. Modified `Assets/Scripts/Core/CompanyContracts.cs` for enemy-only species/archetype names, and `Assets/Scripts/Core/GameManager.cs` for the Enemy forces tactical side label. Player/recruit identities and personal names are preserved.
- Modified display/binding consumers: `Assets/Scripts/Subwindows/CompanyManagementUI.cs`, `Assets/Scripts/Subwindows/CompanyMissionsUI.cs`, `Assets/Scripts/Subwindows/CompanyCampaignView.cs`, `Assets/Scripts/Subwindows/ContentSettings.cs`, `Assets/Scripts/Subwindows/CreateTacticalGame.cs`, `Assets/Scripts/Subwindows/RaceEditorPanel.cs` and `Assets/Scripts/Subwindows/InfoPanel.cs`. Race Editor mapping and skirmish selections resolve the original IDs behind the display names. Battle memorials use individual labels. No new species was curated.
- [Cause, checks, boundaries and focused review](docs/implementation/09-10-feedback-hp-and-labels.md). Unity compilation/scene opening and managed HP/label diagnostics passed; actual reserve/revival/reload workflow remains pending player confirmation. Company health recovery remains planned in11, not implemented in this fix. No new framework/project tests, source files/metadata, assets/dependencies, checkpoint edits, deletion/move, commit or publication.
- GPL source-change check: all13modified C# sources have dated conversion notices, meaningful earlier legal/modification notices remain, and exact paths are identified here. Actual License.txt sections4–5 reviewed; license text, attribution/copyright/warranty notices and existing dependency grants preserved. No external code/assets/dependencies or changed licensing terms introduced. Inherited provenance and section5(d)/6 distribution review remain release gates; this is a source-change check, not a blanket distribution compliance certification.

## 2026-11-01 — Grouped specs09–10: first contract and battle loop

- Added this fork's `Assets/Scripts/Core/CompanyContracts.cs`, `Assets/Scripts/Core/CompanyBattle.cs` and `Assets/Scripts/Subwindows/CompanyMissionsUI.cs`: saved local rout offers/targets, bounded fixed-power procedural enemies, reputation/deadlines, final deployment, detached once-only battle settlement, results/revival phases, active-campaign memorial/statistics and target recovery. Numeric values are provisional. Associated non-commentable metadata: `Assets/Scripts/Core/CompanyContracts.cs.meta`, `Assets/Scripts/Core/CompanyBattle.cs.meta`, `Assets/Scripts/Subwindows/CompanyMissionsUI.cs.meta`.
- Modified `Assets/Scripts/Core/CompanyCampaign.cs`, `Assets/Scripts/Core/CompanyEconomy.cs` and `Assets/Scripts/Core/CompanyEquipment.cs`: mission/fate state and accepted-save migration, offer refresh/hire counters, enemy-only natural gear and merchant revenue accounting. Modified `Assets/Scripts/Subwindows/CompanyCampaignView.cs` and `Assets/Scripts/Subwindows/CompanyManagementUI.cs` for map target/list/town/deployment/result routing and the connected mission interface. Existing inventory/people/debt identities remain canonical; no new species approval.
- Modified `Assets/Scripts/Core/World.cs`, `Assets/Scripts/Core/GameManager.cs` and `Assets/Scripts/Core/State.cs`: isolated battle services without empire simulation/config resets, explicit company battle entry/return, restored prior services and a guard against incomplete legacy company tactical saves. The supported save boundary is deployment/results; live tactical-turn saving is not implemented.
- Modified `Assets/Scripts/Entities/Units/Unit.cs`, `Assets/Scripts/Entities/Units/Actor_Unit.cs`, `Assets/Scripts/Entities/Units/PredatorComponent.cs` and `Assets/Scripts/Races/Traits.cs`: separate lasting/temporary HP, nonserialized combat context, latched downed state, explicit actual digestion death, movement/direct-attack guards and lasting retained digestion recovery. Zero-damage containment does not kill merely because a passenger is downed at0HP. Modified `Assets/Scripts/Modes/TacticalMode.cs` for actual survivor completion, continued absorption/healing/XP, enemy captive-flight blocking, stalemates instead of forced deaths, company reconciliation and an anytime revival preference; removed obsolete private abstract/strategic result helpers while retaining glamour restoration.
- Modified `Assets/Scripts/AI/TacticalAI.cs`, `Assets/Scripts/Entities/Spells/SpellList.cs` and `Assets/Scripts/Modes/Tactical/TacticalAction.cs`: downed direct-target/command filtering and voluntary surrender retirement. Modified `Assets/Scripts/Races/Graphics/UnitSprite.cs`, `Assets/Scripts/UI/RightClickMenu.cs`, `Assets/Scripts/Subwindows/InfoPanel.cs` and `Assets/Scripts/Utility/Texts/HoveringTooltip.cs` for grey temporary HP, downing previews and accurate player-facing explanations.
- [Implementation, provisional values and combined runtime checklist](docs/implementation/09-10-contract-and-battle-loop.md). User accepted07–08 before this milestone. Shared HP/result changes apply to Pure Tactical as well; campaign injuries/treatment/company daily recovery/sparring remain11. No new tests/framework, scene/prefab/terrain changes, dependency/engine upgrades, file deletion/move, commit or publication.
- GPL source-change review: reread actual License.txt sections4–5; all23changed/new C# sources have dated conversion/addition notices, meaningful prior legal/modification notices remain, and this entry identifies all source/metadata paths. No external code/assets/dependencies or changed license grants were introduced; existing GPL terms, attribution, copyrights and warranty notices are preserved. New code reuses existing Unity/Odin/project interfaces and artwork. Inherited third-party provenance and applicable section5(d)/6 distribution obligations remain explicit release-review items; source notices/changelog/compilation do not certify every distribution obligation. Final checks and runtime limits are recorded in the report/checkpoint.

## 2026-11-01 — Grouped specs07–08: curated people and company economy

- Curated only the user-selected Avians/ranged, Lizards/magic, Gnolls/melee and animal SpitterSlugs/weak ranged support. Modified `Assets/Scripts/Core/SpeciesPolicy.cs` and `Assets/Scripts/Races/RaceParameters.cs` for eligibility and authorized provisional default stats/traits. Existing developer override rows retain precedence. Modified `Assets/Scripts/Entities/Items/ItemRepository.cs` to lower the slug natural ranged weapon's damage/cost; no other species approval or retired90 reactivation.
- Added this fork's `Assets/Scripts/Core/CompanyEconomy.cs`, `Assets/Scripts/Core/CompanyEquipment.cs` and `Assets/Scripts/Subwindows/CompanyManagementUI.cs`: leader creation, canonical candidates/former members, recruitment/roster/nicknames/reserves, weekly advance wages/prorating/debt/player-selected repayment/departures, nine typed gear slots/ownership/safe HP adjustment, and town shopping. Values are provisional; contracts, company combat and injury recovery remain later specs.
- Modified `Assets/Scripts/Core/CompanyCampaign.cs`, `Assets/Scripts/Core/CompanyCampaignSave.cs` and `Assets/Scripts/Subwindows/CompanyCampaignView.cs`: economy state, validation/rebinding, persisted decisions/rest interruption, normal checkpoint history/latest-valid Ironman access, connected management and original terrain-sprite rendering. No scene, prefab, artwork or bundle was edited in this milestone.
- Modified `Assets/Scripts/Entities/Units/Unit.cs`: independent company item context, persisted company nine-slot arrays, pure company MaxHealth reads and no re-equipping on load. Company slots resist legacy Resourceful resizing and slotted books are not consumed by BookEater. Modified `Assets/Scripts/Entities/Units/Actor_Unit.cs` only for Unit-owned item-provider access in best-weapon/portrait rendering. Modified `Assets/Scripts/Entities/Items/Item.cs` for stable definition/instance IDs, `Assets/Scripts/Utility/Rand.cs` for explicit seeded instances, and `Assets/Scripts/UI/Connectors/CustomizerPanel.cs` for company-owned creator callbacks. Legacy standalone battle setup retains its two/three-slot layout.
- Extended `Assets/Editor/SpeciesResourceAudit.cs` with a read-only creator/portrait/terrain reference inspection; no asset-saving, new test framework or gameplay test was added. Associated new non-commentable script metadata: `Assets/Scripts/Core/CompanyEconomy.cs.meta`, `Assets/Scripts/Core/CompanyEquipment.cs.meta`, `Assets/Scripts/Subwindows/CompanyManagementUI.cs.meta`.
- GPL source-change check: reread actual License.txt sections4–5; all fifteen changed/new C# sources carry dated conversion/addition notices, meaningful earlier notices are preserved, and this record covers exact source/metadata paths. Existing License.txt, upstream attribution/copyright/warranty notices and dependency grants remain unchanged. New implementation uses existing project/Unity/Odin APIs, UI and terrain assets; no external code, asset, dependency or new licensing terms were introduced. Inherited asset/dependency provenance and any eventual section5(d)/6 distribution obligations remain release-review matters, not certified by compilation or this changelog. No distribution/commit/deletion/move occurred.
- Implementation and provisional values: [people/economy report](docs/implementation/07-08-people-and-economy.md). Unity compilation and explicit base-scene checks passed; scene references and source/resource preservation were inspected. Automated window capture failed; the nonempty save/equipment/debt workflow and curated tactical battle still need the one combined runtime review. See the report/checkpoint for exact final logs and limitations; do not treat this as user acceptance or a complete playable contract loop.

## 2026-10-30 â€” Compliance workflow and documentation

- Added the per-source-change GPL-3.0 review, dated source-notice policy, and change-record requirements to `AGENTS.md`.
- Introduced this `CHANGES.md` record and linked it from `README.md` so fork modifications can be distinguished from upstream work.
- This is a documentation/governance change. No game source code, gameplay, scene, prefab, or asset was modified by this change; `License.txt` was preserved.

## 2026-10-30 â€” Architecture packet 01C (documentation only)

- Added `docs/architecture/01c-combat-equipment-and-results.md` and its equipment/damage reference inventories; updated the roadmap and affected planning specs with source-based migration findings.
- No game source code, scene, prefab, asset, or license text was changed. No source-file modification notices were added to untouched upstream code. This records study documentation, not implemented conversion features.

## 2026-10-30 â€” Design clarification record (documentation only)

- Recorded the user's 24 answers and follow-up decisions in `docs/design-clarifications-2026-10-30.md`; updated `docs/mercenary-company-campaign-plan.md`, the roadmap, species/options checklists, architecture notes 01B/01C, and specs 01, 03, and 05â€“12.
- Distinguished digestion-only leader revival from survivable downing; recorded temporary/lasting HP, summons, typed loadouts, species preservation, enemy persistence, saves, rest interruptions, and player-directed debt payment. Flagged remaining abandonment/draw interactions instead of inventing outcomes.
- Read existing mercenary generation and monster-spawner settings code to explain current species-disable behavior. No runtime checks, game source changes, assets, new dependencies, or license changes were made. Upstream legal/attribution notices remain untouched; this is planning documentation, not an implemented conversion or a completed distribution compliance audit.

## 2026-10-30 â€” Stalemate and actual casualty outcomes (documentation only)

- Updated `docs/design-clarifications-2026-10-30.md`, `docs/mercenary-company-campaign-plan.md`, `docs/conversion-roadmap.md`, `docs/architecture/01c-combat-equipment-and-results.md`, and specs 01, 09, 10, and 12 with the user's final follow-up answers.
- Superseded earlier left-behind/assumed-loss rules: continue actual battle simulation when standing enemies face downed company members. Stalemates occur when both sides lack active units or a battle remains unable to finish after a generous limit; remaining units flee and survive. Fled target enemies count defeated even on a draw, allowing normal contract completion/rewards. Two hundred turns is illustrative rather than final balance.
- Documentation only: no game source, scenes, prefabs, assets, dependencies, or license notices changed. No Unity build or gameplay tests performed; upstream attribution and licensing remain untouched.

## 2026-10-30 â€” Architecture packet 01D and consolidated order (documentation only)

- Added `docs/architecture/01d-campaign-persistence-and-identity.md` and `docs/architecture/01d-removal-sequence-and-risks.md`. Traced strategic scheduling/recovery, world generation/movement/fog, World/State save/load, tactical snapshots/checkpoints, randomness, item rebinding and character identity. Proposed the company boundary and interleaved 02â€“05 cleanup order.
- Updated `docs/conversion-roadmap.md`, `docs/mercenary-company-campaign-plan.md`, `docs/design-clarifications-2026-10-30.md`, and specs 01â€“13 with findings and implementation preflights. Recommended 02A (defaults/configuration ownership) as the first reviewable cleanup packet; no implementation started.
- Source inspection and documentation only. No gameplay source, scene, prefab, asset, dependency, or license text changed. No Unity build/save round-trip/playtest performed. Existing GPL/attribution notices remain untouched; source-change modification notices are not applicable to untouched game files. This is not a completed release/dependency-license audit.

## 2026-10-30 â€” Packet 02A: content defaults and preference loading

- Modified `Assets/Scripts/Subwindows/ContentSettings.cs`: removed default ownership from widget bindings and delegated data loading to a separate service. Existing screens, keys, apply/save paths, values, sorting and legacy species/spawner selections are retained.
- Added `Assets/Scripts/Core/ContentSettingsData.cs`, extracting the 105 upstream toggle defaults, 89 scalar assignments and mercenary/spawner preference-loading logic into an explicit WorldConfig-targeted service. This new fork source retains upstream-derived logic; it does not claim new ownership of the original values/implementation.
- Added `Assets/Scripts/Core/ContentSettingsData.cs.meta` as the associated Unity script metadata. This dated entry identifies that exact non-commentable file; the C# sources carry prominent dated addition/modification notices referencing this record and License.txt.
- Added `docs/implementation/02a-settings-defaults-and-precedence.md`; updated spec 02, roadmap, campaign-plan status and affected architecture/design source links. Actual balance enforcement, screen removals and new species eligibility remain later packets. This is the first implementation batch, not a completed conversion.
- GPL change check: reviewed License.txt sections 2, 4 and 5; dated notices and this entry cover changed/new source and metadata. Existing license, attribution and legal notices are preserved. No new third-party code, dependencies or assets were introduced. This does not certify inherited third-party redistribution terms or authorize publishing.
- Validation: source-move comparison retained all literal toggle values, scalar statements and binding counts. Fresh Unity 2018.3.12f1 compilation passed with no C# errors and return code 0 (`Logs/packet-02a-compile-confirm.log`, ignored). No player build or manual settings/save playtest. Unity-removed orphan metadata was restored byte-for-byte; unrelated project assets are retained.

## 2026-10-30 â€” Packet 02B: tool entry guards and Race Editor access

- Modified `Assets/Scripts/Subwindows/GameMenu.cs`, `Assets/Scripts/Modes/StartMode.cs`, and `Assets/Scripts/Core/GameManager.cs`: blocked retired authoring/tutorial/map/empire/world/cheat entry methods, introduced a shared masked Race Editor password/session gate, and retained nested tag editing and normal tabs. The user-supplied convenience password is intentionally source-discoverable and omitted from reports.
- Modified `Assets/Scripts/Subwindows/CheatMenu.cs`: its existing startup/load preference hook now disables the five retired player cheat flags without deleting stored preferences or changing species/content balance settings.
- Modified `Assets/Scripts/Subwindows/RaceEditorPanel.cs`, `Assets/Scripts/Subwindows/UnitTagEditor.cs`, and `Assets/Scripts/UI/Connectors/UnitTagMenu.cs`: restricted editor/nested-tag access to the authorized session and revoke access when Race Editor closes.
- Modified `Assets/Scripts/Subwindows/InfoPanel.cs`, `Assets/Scripts/UI/UnitInfoPanel.cs`, `Assets/Scripts/UI/UnitEditorPanel.cs`, and `Assets/Scripts/Subwindows/VariableEditor.cs`: removed the UnitEditor link/click route and blocked unit/reflection editor entry methods.
- Modified `Assets/base.unity`: eleven retired menu button instances are inactive with persistent callbacks disabled; the trait-warning instance now invokes the same gated editor callback. Exactly twelve prefab-instance blocks changed, with no shared prefab rewrite. This dated exact-path record is the associated modification notice for the non-commentable Unity scene.
- Added `docs/implementation/02b-tool-retirement-and-race-editor-gate.md`; recorded the user's 02A persistence confirmation and updated spec 02, roadmap, screen checklist, campaign-plan status and historical-study annotations. Content-page removal/balance locking and new campaign mechanics remain later work.
- GPL change check: all eleven modified C# paths carry dated fork modification notices and the scene is identified above. License.txt and existing attribution/notices are preserved; no new dependency or externally copied code/assets. No files are deleted or moved; retained legacy component files are deliberately inert pending later retirement.
- Validation: targeted source/scene diff review and Unity 2018.3.12f1 import/compile check passed with no C# errors/return code 0 (`Logs/packet-02b-compile.log`, ignored). Current assemblies were compiled in the user's open editor and verified newer than the source; final batch check reused them. Restored both known Unity-removed orphan metadata files byte-for-byte. Manual password/navigation checks remain pending; no new player build or automated tests/framework.

## 2026-10-30 â€” Packet 02C: settings retirement and code-owned defaults

- Modified `Assets/Scripts/Subwindows/ContentSettings.cs`: retain five pages and 59 preferences, remove retired bindings/callbacks, dynamic legacy spawning UI and building-settings load/save hooks; preserve retained apply/save and live gender-preference behavior.
- Modified `Assets/Scripts/Core/ContentSettingsData.cs`: separate an explicit retained-preference allowlist from code-owned defaults, ignore old hidden player overrides, save only retained preferences, and read the existing HermBreastSizeModifier saved key. Legacy species availability is temporary pending 03A, not curated here.
- Modified `Assets/Scripts/Core/State.cs` and `Assets/Scripts/Subwindows/CreateStrategicGame.cs`: enforce the same policy after saved/imported WorldConfig replacement and after load migrations; use code building defaults rather than saved building overrides.
- Modified `Assets/Scripts/Utility/DisplayTooltip.cs`: remove the obsolete hard-coded building-page index and preserve retained tooltip routing with the shorter array.
- Modified `Assets/base.unity`: align five panel references/selector options, disconnect 154 obsolete field bindings, deactivate 48 page/widget roots and disable 77 obsolete persistent callbacks. Exactly 100 blocks changed against the post-02B baseline; no object blocks added/removed or shared prefabs edited. This dated exact-path entry is the associated modification notice for the non-commentable scene.
- Added `docs/implementation/02c-settings-retirement-and-balance-locking.md` and `docs/implementation/02c-settings-control-map.md`; recorded the user's three 02B confirmations and updated affected status/planning references. Curated-species generation/page integration and new campaign mechanics remain later work.
- Validation: fresh Unity 2018.3.12f1 runtime/editor assembly compilation passed without C# errors and batch shutdown returned 0 (`Logs/packet-02c-compile.log`, ignored). Source/scene scope review passed; no new tests/framework or player build. Restored both known Unity-removed orphan metadata files byte-for-byte; original scene line endings preserved. 02C in-game checks remain pending.
- GPL source-change check: all five C# files have prominent dated fork notices retaining earlier notices; the exact scene is identified above. License.txt Git content and existing upstream attribution/legal notices are unchanged. No new dependency or externally copied code/assets; no file deletions/moves. Distribution/third-party redistribution terms remain a separate review.

## 2026-10-30 — Packet 02C repair: restore settings scene deserialization

- Modified `Assets/base.unity`: removed exactly two orphaned `type: 3}` continuation lines from ContentSettings component 1620494162. The earlier retirement edit had removed the first lines of the two wrapped prefab fields but left their continuations, corrupting the component's YAML and preventing Unity from restoring its retained control/page references. No retained controls were intentionally retired by this repair; no other scene blocks changed.
- The user's Play-mode report and Editor.log established the regression: Unity could not parse base.unity, followed by null-reference errors in ContentSettings.Open and ActivatePage. The earlier compile-only check was insufficient to validate scene loading. Follow-up validation explicitly opens the scene using the installed Unity version; the packet report records the result and remaining manual checks.
- Validation: Unity 2018.3.12f1 explicitly opened `Assets/base.unity` and exited with code 0; its log contains no parse errors, broken GameObject references or exceptions (`Logs/packet-02c-scene-repair-open.log`, ignored). The first batch-mode scene-open attempt was rejected; the successful follow-up ran without batch mode. Manual visibility/persistence checks remain pending. Restored the two known Unity-removed orphan metadata files byte-for-byte.
- This dated exact-path entry is the modification notice for the non-commentable scene repair. No C# source, dependency, asset license or attribution changed. License.txt sections 4/5 and preserved notices were reviewed; full distribution/third-party obligations remain a separate review. No files deleted or moved.

## 2026-10-30 — Packet 03A1: approved species and safe empty setup

- Added `Assets/Scripts/Core/SpeciesPolicy.cs` and `Assets/Scripts/Core/SpeciesPolicy.cs.meta`: empty code-owned approved roster, explicit sapient/animal uses, 90 mapped retired species, global player enablement restricted to approved entries and safe empty random-selection results. The dated exact metadata path is the associated notice for that non-commentable file. No species selected/approved.
- Modified `Assets/Scripts/Core/ContentSettingsData.cs` and `Assets/Scripts/Core/WorldConfig.cs`: load/save the new species preferences and replace temporary true legacy Merc keys with approved enablement. Existing characters are not discarded by this policy.
- Modified `Assets/Scripts/Subwindows/ContentSettings.cs`: construct the sixth Species Availability page from retained UI styling with an empty-list explanation and approved-only toggles; retain the five existing pages. No scene or shared prefab edit.
- Modified `Assets/Scripts/Subwindows/CreateTacticalGame.cs`: approved-only choices/previews/random selection, safe empty single/bulk entry guards, taxonomy-filtered batch participants and correct defender dropdown selection.
- Modified `Assets/Scripts/Modes/StartMode.cs` and `Assets/Scripts/Subwindows/CreateStrategicGame.cs`: stop empty-roster strategic setup/world creation before mutation.
- Modified `Assets/Scripts/Entities/MercenaryHouse.cs`: generate approved sapient candidates only, preserve existing candidates when no new pool is available, and stop named-unique generation. Modified `Assets/Scripts/Entities/Spells/SpellList.cs`: generic creature summon requires an approved animal and no longer falls back to Vagrants; empty pools spend no cast resources.
- Added `docs/implementation/03a1-approved-species-and-empty-setup.md`, recorded the user's repaired-02C success and refined specs/statuses. 03A is split into foundation/setup (03A1) and remaining runtime generation/mapping audit (03A2). Legacy helpers/resources, special summons, transformations and old simulation sites are not declared retired or fully filtered by this packet.
- Validation and review: fresh Unity 2018.3.12f1 runtime/editor compilation and subsequent scene opening passed without C# errors, parse failures or exceptions and both exited with code 0; evidence is recorded in the packet report; new page rendering, setup guards and retained navigation require manual review. No automated tests/framework or player build added. `Assets/base.unity` remains byte-for-byte unchanged for this packet.
- GPL source-change check: eight modified C# files carry dated 03A1 notices preserving earlier notices; the new source is marked as this fork's addition and metadata is recorded above. License.txt and upstream attribution/legal text retained; no third-party code, asset or dependency added. No files deleted/moved. Distribution/inherited-asset terms remain a separate review.

## 2026-10-30 — Packet 03A2: guarded tactical species routes and metadata previews

- Modified `Assets/Scripts/Core/SpeciesPolicy.cs`: explicit copy/spawn/transformation uses, negative-sentinel and unknown-use rejection; the approved roster remains empty and the 90 rejected IDs remain unchanged.
- Modified `Assets/Scripts/Entities/Spells/SpellList.cs` and `Assets/Scripts/Modes/TacticalMode.cs`: preflight copied/mapped summon species, filter AstralCall and weighted animal-ant summons, and guard legacy building reinforcements before participant construction.
- Modified `Assets/Scripts/Entities/Units/PredatorComponent.cs`, `Assets/Scripts/Entities/Units/Actor_Unit.cs`, `Assets/Scripts/Entities/Units/Prey.cs`, and `Assets/Scripts/Entities/Units/Unit.cs`: guard spawn/new-form entry paths before their local mutation/callbacks, propagate failed template changes, and preserve original-form restoration/copy helpers. These guards do not claim full retirement of legacy revival, conversion or allegiance effects.
- Modified `Assets/Scripts/UI/HoveringRacePicture.cs`, `Assets/Scripts/UI/CustomizationLoaderWindow.cs`, and `Assets/Scripts/UI/UniformLoaderWindow.cs`: remove Cats placeholder hover, retain gated developer inspection and existing-character appearance, filter unapproved foreign previews, and use metadata rather than gameplay constructors for cosmetic previews.
- Added `docs/implementation/03a2-species-runtime-routes.md` and `docs/implementation/resume-checkpoint.md`; recorded the user's successful 03A1 checks and carried residual legacy generation providers to their owning 04/05 cleanup packets. The checkpoint is maintained for the user's requested interruption recovery. No species approved, files deleted/moved, scene/prefab rewritten or dependencies added.
- Validation: fresh Unity 2018.3.12f1 runtime/editor compilation completed without C# errors, exit 0. The initial import logged a CSharpNamespaceParser diagnostic for PredatorComponent; a confirmation import and subsequent scene loading were exception-free and exited 0. The packet report records this limitation. Scene bytes/restoration bodies and License.txt are unchanged; both known orphan metadata files restored. Manual review and future nonempty battle/save paths remain pending; no tests/framework or player build added.
- GPL source-change check: ten prominent dated file notices and this exact-path entry distinguish the modifications; prior notices, License.txt and upstream attribution/legal text are preserved. Validation results and pending runtime checks are recorded in the packet report. This is not a completed distribution/inherited-asset license audit.

## 2026-10-30 — Packet 04A: retire legacy 4X events

- Modified `Assets/Scripts/Core/State.cs`: remove the legacy event dispatcher and its startup/reset construction; omit events from both shipped text-file seed lists while preserving unrelated initialization.
- Modified `Assets/Scripts/Entities/Empire.cs` and `Assets/Scripts/Modes/StrategyMode.cs`: remove the human/AI turn event triggers and UI binding; preserve saved event-history fields and unrelated turn behavior.
- Modified `Assets/Scripts/Utility/EventList.cs` and `Assets/Scripts/Utility/CustomEventList.cs`: replace their executable contents with dated retirement notices, retaining files/metadata. The 36 player cases, 32 AI cases, custom schema/file parser, event choice callbacks/effects and helper factories no longer compile into the game.
- Modified `Assets/Scripts/UI/EventScreen.cs`: leave the serialized layout inert, reject activation, clear text, disable/remove choice listeners and remove keyboard choices and strategic-regeneration-on-disable behavior.
- Modified `Assets/StreamingAssets/events.txt`: replace the 141-record legacy payload with a dated inactive notice. This exact-path dated entry and its in-file notice identify the changed text asset. Personal event files are not read, overwritten or deleted by the packet.
- Added `docs/implementation/04a-legacy-event-retirement.md`; recorded successful 03A2 user review and maintained `docs/implementation/resume-checkpoint.md`. Tactical prose replacement/new campaign events remain 04B/13; no species approved.
- Validation: fresh Unity 2018.3.12f1 runtime/editor compilation and subsequent explicit scene opening passed without C# errors, import exceptions or scene parse/broken-reference errors; both exited 0. Cross-Assets reference closure, removed seeding, empty shipped catalog and unchanged scene/license were checked. Both known orphan metadata restored. No tests/framework, player build or personal-data reset; manual menu smoke review remains pending.
- GPL source-change review: six C# notices and the text notice identify actual changes/dates; earlier notices, License.txt and upstream attribution/legal text preserved. No files deleted/moved, scene/prefab rewritten, external source/dependency/assets introduced or distribution performed. Validation evidence and limits are recorded in the packet report.

## 2026-10-30 — Packet 04B1: neutral central combat text and missing-target context

- Modified `Assets/Scripts/Modes/Tactical/Log/StoredLogTexts.cs`: replace the legacy 22-category prose collection with neutral placeholders and an explicit unknown fallback; retain entry schema/category/list identities and add null-safe highest-priority formatting. Retain upstream contribution credits to Tatltuae and Flame and the acknowledgement of Ted ArCon.
- Modified `Assets/Scripts/Modes/Tactical/TacticalMessageLog.cs`: replace private typed prose generators with functional messages covering all 46 event values; preserve names, weapon/spell labels, damage/healing/experience and odds. Preserve registration and UI/filter methods apart from six missing-prey assignments; remove the fabricated Human default. Retain upstream Tatltuae attribution.
- Modified `Assets/Scripts/Modes/Tactical/Log/LogUtilities.cs`: remove its constructed Human fallback and return a missing-target context instead. Remaining descriptors and free-form callback messages are explicitly 04B2.
- Added `docs/implementation/04b1-central-combat-text-reset.md` and `docs/implementation/04b2-miscellaneous-call-sites.md` (301 lexical candidate lines, no old narrative reproduced); recorded the user's successful 04A review and maintained the recovery checkpoint.
- RNG choice stated before implementation: retain State.Rand for future multi-entry choices; deterministic single placeholders use no draw. Removed narrative/helper draws change same-seed sequences; no new RNG architecture or gameplay formulas introduced. Miscellaneous payloads remain unreset until caller review, so this is not a claim that all prose is replaced.
- Validation: fresh Unity 2018.3.12f1 runtime/editor compilation and subsequent explicit scene opening passed with no C# errors, import exceptions or scene parse/broken-reference errors; both exited 0. Static event/schema/category/registration/filter correspondence checked, scene/license unchanged and known orphan metadata restored. No runtime battle-text test, test framework or player build; manual smoke review remains pending.
- GPL source-change review: all three exact C# paths carry dated fork notices and preserved contribution attribution; License.txt and upstream legal notices retained. No scene/prefab/package changes, new external code/dependencies/assets, deletions/moves or distribution. Validation evidence and remaining battle/manual checks are recorded in the packet report.

## Conversion baseline context

The design and architecture work recorded in this collaboration has so far produced planning documents and an unchanged-source Unity import/compile/Windows-build check. The user confirmed main-menu startup. The recorded starting source revision is `c2910ee56f0468b718c27f7137dfc5a7a2958aea`; see [the baseline report](docs/unity-baseline-report.md).

No gameplay-source conversion changes preceded packet 02A. That packet now records the first behavior-preserving source extraction above. Earlier repository history is not reconstructed here, and this document is not a completed third-party license or release audit. Future actual source changes must have their own dated entries and file notices before handoff.

## 2026-10-30 — Packet 04B2: callback and descriptor text reset

- Modified `Assets/Scripts/Entities/Units/PredatorComponent.cs`: replaced 22 presentation-only wording switches, the transfer-finalization narrative segment and direct callback prose with neutral notices; preserved actual containment placement, both dialog callbacks and all surrounding mechanical effects.
- Modified `Assets/Scripts/Entities/Units/Actor_Unit.cs`, `Assets/Scripts/Entities/Spells/SpellList.cs`, `Assets/Scripts/AI/TacticalAI.cs` and `Assets/Scripts/Modes/Tactical/TacticalUtilities.cs`: replaced inline prose/wording pools with functional action/status notices; retained timing, movement costs, effects and existing useful numeric notifications. Revival/conversion/surrender rule changes remain their later owning packets.
- Modified `Assets/Scripts/Modes/Tactical/Log/LogUtilities.cs` and `Assets/Scripts/Modes/Tactical/Log/PreyLocStrings.cs`: neutralized three descriptors and randomized synonyms, retaining public helper APIs, pronouns, weapon labels and 04B1's missing-target behavior. No species was approved or substituted for testing.
- Added `docs/implementation/04b2-callback-and-descriptor-reset.md`; updated the historical call-site inventory, spec 04, roadmap, campaign-plan status, narrative inventory and interruption checkpoint. The 301 lexical miscellaneous call-site candidates now comprise 102 functional call-site lines; already-functional notices were reviewed rather than suppressed. Old saved raw text, help and mechanical descriptions are outside this reset.
- Verification: fresh Unity 2018.3.12f1 compilation and subsequent explicit base.unity opening passed without C# errors/import exceptions/scene errors, both return code 0 (`Logs/packet-04b2-compile.log`, `Logs/packet-04b2-scene.log`, ignored). Mechanical source correspondence and API checks passed; no Play-mode battle test, new player build, tests or framework. Empty roster still prevents battle playtesting. Scene bytes and license content unchanged; known Unity-removed orphan metadata restored byte-for-byte. No file deletions/moves.
- GPL change check: reviewed License.txt sections 4/5, dated notices cover all seven source paths and this entry lists them explicitly. Existing upstream notices and narrative-contributor credits are retained, including Tatltuae. No new third-party code, assets or dependencies. No licensing/version-grant change; inherited redistribution review remains necessary before distribution.
- Shared-RNG limit: removing text draws changes subsequent random consumption; combat formulas are retained, but same-seed outcome equivalence is not claimed. User menu smoke review and future approved-species battle-log checks remain pending.

## 2026-10-30 — Packet 03B dependency/ownership preflight (documentation only)

- Recorded the user's successful 04B2 menu smoke review. Added `docs/implementation/03b-rejected-species-preflight.md`, `docs/implementation/03b-rejected-species-dependency-matrix.md` and `docs/implementation/03b-shared-sprite-and-bundle-inventory.md`.
- Reconciled all 90 rejected entries with the permanent-retirement policy and current enum/registry aliases/classes. Traced eager definitions, parameter/override/editor/name providers, direct shared rendering/clothing consumers and residual 4X dependencies. Inventoried all 383 bundle manifest source sheets/metadata (present, matching bundle labels), 45 directly shared sprite-field candidates and 372 sheets with serialized GUID references; distinguished potentially stale static-field serialization from live use.
- Proposed five bounded cleanup packets, starting with retired metadata catalogs and the uncalled named-unique factory. Updated spec 03/04, roadmap, campaign/species status, historical-map annotation and recovery checkpoint. No species selected, new gameplay implemented or resource retired by this study.
- Source/reference study only: no game C# source, scene, prefab, bundle, name list, license text or dependency modified. No Unity build/import/Play-mode test, bundle rebuild, new tests or framework. Existing destructive bundle hard-rebuild and name-reset operations were inspected, not invoked. Current game-file content hashes were checked against preflight start; known Unity-removed orphan metadata from the prior manual run was restored byte-for-byte.
- GPL/source workflow: this is documentation work, so no modification headers were added to untouched game files. Existing GPL terms, copyright/attribution and earlier source notices are preserved; no third-party code/assets added. This study does not establish exclusive asset ownership, binary packaging compatibility or complete release compliance. Actual source retirement requires its own dated notices and change entry.

## 2026-10-30 — Packet 03B1: retired metadata catalogs and named-unique factory

- Modified `Assets/Scripts/Core/SpeciesPolicy.cs`: expose permanent retirement as a read-only query, preserving the 90-entry set, empty curated dictionary, saved identities and all gameplay eligibility/preferences.
- Modified `Assets/Scripts/Subwindows/RaceEditorPanel.cs`: filter five species/mapping catalogs and all ten bulk trait/tag traversals, preserve exact raw unavailable/default mapping values with Keep existing mapping, and guard programmatic control updates against reentrant saves. Keep password/session lifecycle, normal tabs and remaining uncurated inspection.
- Modified `Assets/Scripts/UI/HoveringRacePicture.cs`: reject permanently retired species before constructing previews, including developer inspection.
- Modified `Assets/Scripts/UI/Connectors/UnitTagMenu.cs`: filter nested IsRace catalogs, preserve old unavailable/sentinel/unknown condition IDs with Keep existing target and copy those target IDs into duplicate UI rows. Select stored targets by identity instead of option count. Preserve modifier enums/schema, other conditions and unrelated editor behavior.
- Modified `Assets/Scripts/Entities/MercenaryHouse.cs`: retire the uncalled CreateUniqueMercenary method while preserving normal recruitment, stock data and existing individuals. No files deleted or moved; named-special data/enum identities remain for later dependency retirement.
- Added `docs/implementation/03b1-retired-metadata-catalogs.md`; updated spec/roadmap/checklist status and recovery notes. There are 64 remaining editor candidates, all still unapproved for gameplay; no test species enabled. Name sourcing, parameter/graphics registries, resources and old providers remain later packets.
- Verification: final fresh Unity 2018.3.12f1 compilation and subsequent explicit base.unity opening passed, exit 0, with no C# errors/import exceptions/scene errors (`Logs/packet-03b1-compile-final.log`, `Logs/packet-03b1-scene-final.log`, ignored). No agent Play-mode test, new player build or tests/framework. Source correspondence confirms unaffected editor methods, normal recruitment/stock, policy sets, nested modifier enums and non-species load/save branches remain unchanged.
- Scope/GPL change check: reviewed License.txt sections 4/5; all five C# paths carry dated notices and this exact-path record. License content and upstream attribution retained; no external code, new dependencies or assets. Scene bytes match the packet snapshot; only five intended game files differ from preflight hashes. Restored known Unity-removed orphan metadata byte-for-byte. Interactive editor/save checks remain for the user; this is not a complete distribution/asset-license audit.

## 2026-10-30 — 03B1 manual-check clarification (documentation only)

- Recorded the user's successful checks 1/3 and clarified check 2 in `docs/implementation/03b1-retired-metadata-catalogs.md` and the recovery checkpoint. Keep existing mapping preserves the current saved field; it is not a stored mapping value, reset or undo. Returning to a previously saved species after choosing it is expected.
- Corrected the misleading switch-away/back instruction. The exact already-unavailable raw-value round trip remains a source-reviewed verification limit, rather than a reported failure. No C# source, UI behavior, personal data, assets or license notices changed; no new build or tests were run for documentation-only clarification.

## 2026-10-30 — Packet 03B2: rejected name sources

- Recorded the user's acceptance of all 03B1 checks, including the clarified no-op mapping behavior. Added `docs/implementation/03b2-rejected-name-sources.md`; updated spec/roadmap/campaign/species status and the recovery checkpoint.
- Modified `Assets/Scripts/Core/State.cs`: removed 103 exact retired-owner seed basenames, leaving 114 in original order for both initialization paths and explicit reset. ResetNamelists directly overwrites active lists instead of deleting destination paths first; retired/unlisted files remain untouched.
- Modified `Assets/Scripts/Utility/Texts/NameGenerator.cs`: filtered retired per-species imports and army-name sections, retired 23 town/three creature pools and their branches plus an unused retired declaration, and added neutral labels for stale rejected-species naming requests. Kept 28 remaining list initializers, generic names, 19 nonretired town branches and public naming APIs. Player enablement/curation and existing stored names are not changed.
- Modified `Assets/Scripts/Subwindows/Options.cs`: accurately describe active-list reset/reload behavior. Other option/persistence logic and callbacks are preserved. Shipped name files remain inactive as applicable rather than being deleted/rewritten; no personal name-file contents were read or changed by the agent and no reset/reload was invoked.
- Verification: fresh Unity 2018.3.12f1 compilation and subsequent explicit scene opening passed with no C# errors/import exceptions/scene errors, exit 0 (`Logs/packet-03b2-compile.log`, `Logs/packet-03b2-scene.log`, ignored). Source correspondence verified retained list literals, seed order/source-file presence, town branches/APIs and unaffected State/Options code. No new tests/framework, Play-mode test or player build. Empty roster still prevents visible character-name battle tests.
- Scope/GPL check: reviewed License.txt sections 4/5; all three modified C# paths have dated conversion notices and exact-path coverage here. Prior notices, attribution, license terms/version grants preserved; no external source, dependency or asset introduced. Scene bytes unchanged, known Unity orphan metas restored byte-for-byte, and game-file hash review shows only the intended three changes beyond accepted 03B1. No files deleted/moved or personal files reset. Full inherited-asset/distribution review remains separate.
- RNG limit: retired naming requests now use deterministic labels and avoid their old random draws; same-seed legacy outcomes are not promised. Actual generation/provider and registry/resource retirement remain later packets; no gameplay species approved.

## 2026-10-30 — Packet 03B3A: override loading before definition retirement

- Recorded the user's successful 03B2 startup/name-reload check. Added `docs/implementation/03b3a-override-hydration-boundary.md`, tracing 154 parameter mappings/155 prototypes and the remaining cross-species links, and refining the first data packet before stat/graphics retirement.
- Modified only `Assets/Scripts/Utility/Stored/RaceSettings.cs`: skip permanently retired keys in both Sanitize passes, preserving raw old records rather than rehydrating their defaults or clamping their values. Preserve nonretired hydration/clamps/FeralBats behavior and player-disabled/uncurated metadata handling.
- Correct newly created RaceSettingsItem LeaderRace and MorphRace assignments to use their own prototype fields rather than ConversionRace. Existing explicit saved overrides are not migrated. All 40 serialized fields/attributes, enum IDs, lookup/reset/save paths and generation approval remain unchanged.
- Verification: fresh Unity 2018.3.12f1 compilation and subsequent explicit scene opening passed without C# errors/import exceptions/scene errors, exit 0 (`Logs/packet-03b3a-compile.log`, `Logs/packet-03b3a-scene.log`, ignored). Source correspondence shows only the intended guards/loop/default assignments and dated notice; schema and four reference-file snapshots match. No Play-mode test, personal preset inspection/migration, new player build or tests/framework.
- Scope/GPL check: reviewed License.txt sections 4/5, added a dated conversion notice and this exact-path record, retained original licensing/attribution and added no third-party source/dependency/asset. Scene bytes/license content preserved; known Unity orphan metadata restored byte-for-byte. Only the intended single source differs beyond accepted 03B1/03B2. No files/slots/characters deleted or moved. Distribution/inherited-content compliance remains a separate review.
- Updated roadmap/spec/campaign/species status and recovery notes. Stat prototypes, graphics definitions, direct legacy getters and 4X providers remain for later retirement; no playable fallback, constructor-wide exception, species curation or new campaign mechanic was introduced. Interactive editor-load/mapping persistence checks remain pending.

## 2026-10-31 — 03B3B stat read-boundary review (documentation only)

- Recorded user acceptance of 03B3A. Added `docs/implementation/03b3b-stat-read-boundary-review.md` with current direct override-read candidates and retained combat/metadata/legacy-save dispositions; updated roadmap and recovery checkpoint.
- Advanced the narrow 05A1 application/save entry boundary ahead of stat prototype removal. Existing fallback and direct dereference paths make immediate removal unsafe without those callers being closed. No replacement species, new stats, save import or campaign mechanic selected.
- Documentation only: game source/assets, licensing/attribution and source notices unchanged. No Unity build/import/Play test, personal-data access, dependency, new tests/framework, file deletion or move. No complete generation/retirement or distribution-compliance claim.

## 2026-10-31 — 05A1 legacy world entry/loading retirement

- Modified `Assets/Scripts/Core/State.cs`, `Assets/Scripts/Core/GameManager.cs`, `Assets/Scripts/Modes/StartMode.cs`, `Assets/Scripts/Subwindows/CreateStrategicGame.cs`, and `Assets/Scripts/Subwindows/SaveLoad.cs`: retire old world load/preview/migration, quickload transition, empire creation and secondary saved-world settings import. Preserve files, Pure Tactical setup, save writing and snapshot/settings schemas. Legacy-format save resume is temporarily unavailable, including tactical saves.
- Added `docs/implementation/05a1-legacy-world-entry-retirement.md`; source comparison verified 89 unaffected bodies and no remaining World deserializer in Assets/Scripts. Final fresh Unity compilation passed; explicit scene check recorded in the report. No personal data read/deleted, tests/framework or player build.
- GPL check: actual License.txt sections 4/5 reviewed; all five sources carry dated modification notices and exact-path coverage here, retaining earlier attribution/license notices. No new dependencies/assets/external code or license change. Full redistribution review remains separate. No files deleted/moved.

## 2026-10-31 — 05A2 strategic scheduler retirement

- Modified `Assets/Scripts/Modes/StrategyMode.cs`: retire strategic setup/regeneration/input/button/turn entry bodies and private AI, turn, economy/growth and victory scheduler methods; remove scheduler-only timer/window-state remnants. Keep callbacks, shared helpers, source file and scene bindings.
- Modified `Assets/Scripts/Core/GameManager.cs`: block strategic activation while preserving the PureTactical exit-to-menu branch verbatim. No new company mechanics, curation, tactical AI or snapshot changes.
- Added `docs/implementation/05a2-strategic-scheduler-retirement.md`, recorded 05A1 acceptance and updated roadmap/spec/checkpoint. Source comparisons preserve unaffected bodies except obsolete window-state writes. Final compile/scene results recorded in the report; no new tests/framework, player build or agent battle test.
- GPL change check: License.txt sections 4/5 reviewed; both changed sources carry dated notices and exact-path coverage here. Prior notices/licensing/attribution preserved, no third-party code/dependencies/assets introduced, no files deleted/moved. Physical provider/stat/resource retirement and full distribution review remain separate.

## 2026-10-31 — 05A3A campaign world/battle entry retirement

- Modified `Assets/Scripts/Core/World.cs`: retire unused strategic-creation constructor while retaining tactical World(false), fields and other helpers.
- Modified `Assets/Scripts/Core/GameManager.cs`: retire campaign delayed/queued/direct battle activation and loaded-world transition; remove obsolete queue payload/automatic camera-end battle dispatch, retain default read-compatibility state. Pure Tactical activation and return unchanged.
- Modified `Assets/Scripts/Modes/TacticalMode.cs`: guard direct Begin before mutations unless PureTactical; retain all subsequent battle setup/combat/AI/results code and standalone village skirmish support.
- Added `docs/implementation/05a3a-campaign-battle-entry-retirement.md`, recorded 05A2 acceptance and updated staged notes. Source correspondence checks retained World/TacticalMode code and 22 unaffected GameManager methods. Final compile/scene evidence recorded in report; no agent Play-mode test, new tests/framework or player build. Battle playtesting awaits approved species.
- GPL check: actual License.txt sections 4/5 read; all three sources carry dated modification notices/exact paths here. Earlier notices/attribution and license terms retained. No new assets/dependencies/external source, file deletion/move or personal-data change. Complete resource/provider retirement and release compliance are not claimed.

## 2026-10-31 — 05A3B1 post-battle replacement retirement

- Modified `Assets/Scripts/Modes/TacticalMode.cs`: retire ProcessReplaceable call/method and consecutive Transmigration/Reincarnation/SavedCopy result branches, implementing approved retirement of those replacement outcomes. Preserve IDs/schemas and all other battle calculation/cleanup/summary code.
- Added `docs/implementation/05a3b1-post-battle-replacement-retirement.md`; recorded 05A3A acceptance and updated staged notes. Whole-file correspondence accounts for exactly the intended removals. Fresh compilation passed; scene outcome recorded in report. No new tests/framework, player build, agent battle test or personal-data access.
- GPL check: License.txt sections 4/5 reviewed; dated source notice and exact-path entry present, prior notices/attribution/license retained. No new dependencies/assets/external code, file deletion/move or schema change. Other revival/provider/result work remains; no leader revival or new company fate/health system implemented. Distribution compliance is not certified by this change check.

## 2026-10-31 — 05A3B2 battle-result territory retirement

- `Assets/Scripts/Modes/TacticalMode.cs`: remove empire battle/lost-army statistics writes, territorial claim and village-conquest callback from battle finalization.
- `Assets/Scripts/Modes/Tactical/TacticalUtilities.cs`: retire the now-uncalled CleanVillage helper, including village ownership, population devastation and monster conquest/movement effects.
- Related spec: 05. Standalone village battlefield/garrison setup, result summaries, tactical Wins counters, participant cleanup, relocation and bounty/loot remain unchanged. This is not the new company result system.
- Dated fork notices added; existing license/attribution retained. No external code/dependency/asset additions or file deletions/moves.

## 2026-10-31 — 05A3B3 post-battle relocation retirement

- `Assets/Scripts/Modes/TacticalMode.cs`: retire the return-to-army/flee-to-town prompts and FledReturn choice state, keeping the existing default winning-force rejoin behavior. Remove three post-battle traveling-unit dispatches; preserve retreat-list pruning, participant assignment, village battlefield support, rewards and summaries. Narrow the tactical utility context call to retained inputs.
- `Assets/Scripts/Modes/Tactical/TacticalUtilities.cs`: retire ProcessTravelingUnits, GenerateFleeingArmy and uncalled GenerateAnotherArmy. Remove the army/village/garrison fields and context arguments used solely by retired helpers.
- Related spec: 05. No new company retreat, Downed, casualty or campaign travel rules. Dated fork notices and prior attribution preserved; no external additions, file deletions or moves.

## 2026-10-31 — 05A3B4 battle reward retirement

- `Assets/Scripts/Modes/TacticalMode.cs`: remove old empire bounty payouts, the LootItems call/helper and reward-text suffixes from battle finalization. The retired helper collected casualty spell books and transferred army/village item stocks.
- Preserve tactical win counters, standard battle summaries, participant accounting, combat, item definitions and stored inventory/bounty schemas. Future company contract gold and loot rules are not implemented here. Related spec: 05.
- Dated modification notice added; upstream licensing/attribution preserved. No external source/dependency/asset additions or files deleted/moved.

## 2026-10-31 — 05A3B5 post-battle survival-trait revival retirement

- `Assets/Scripts/Modes/TacticalMode.cs`: remove the remaining two post-battle resurrection branches for Eternal, LuckySurvival, Reformer, Revenant, DeathCheater and CloseCall, including their health restoration and revived-unit collection moves.
- Preserve normal size/temporary-boost cleanup, combat resolution, surviving-unit accounting and summaries. Trait IDs, definitions and other runtime references remain pending separate retirement; this is not new Downed/leader-revival behavior. Related spec: 05 and recorded design clarification 1.
- Dated fork notice added; existing license/attribution preserved. No external source/dependency/asset additions or file deletions/moves.

## 2026-10-31 — 05A3B6 Resurrection spell retirement

- `Assets/Scripts/Entities/Spells/SpellList.cs`: preserve Resurrection ID/metadata as retired, remove its revive callback, and reject retired spells in both TryCast overloads before actor/resource/effect access. The retirement predicate currently names only Resurrection.
- `Assets/Scripts/Entities/Units/Unit.cs`: filter retired spells after assembling all personal, consumable, racial, configured and book-granted spell sources. Stored grants and equipment are preserved.
- Related spec: 05. Reanimate, summons, other on-death/digestion revival and conversion remain separate review. No new leader revival, Downed or health-pool rule. Dated source notices and existing legal/attribution notices retained; no external additions or file deletions/moves.

## 2026-10-31 — 05A3B7 Respawns and CloseCall runtime retirement

- `Assets/Scripts/Entities/Units/Actor_Unit.cs`: remove Respawner/RespawnerIII status grants, weapon/spell death respawn blocks and the obsolete Respawns exclusion from ordinary automatic surrender. Remove CloseCall's corpse suppression/false fled notice; use ordinary casualty presentation.
- `Assets/Scripts/Entities/Units/PredatorComponent.cs`: remove the digestion-death Respawns block, including restoration, release and early return; continue the ordinary completed-digestion path.
- `Assets/Scripts/Entities/Items/ItemRepository.cs`: remove Goddess Pendant's recurring respawn grant and corresponding description sentence; preserve its battle-start Bolstered effect and stored item identity.
- Related spec: 05. Status/trait/item IDs and Unit status helpers remain; no new Downed/HP/leader-revival system or temporary summon change. Dated conversion notices and prior attribution retained. No external additions, file deletions or moves.

## 2026-10-31 — 05A3B8 digestion conversion/rebirth retirement

- `Assets/Scripts/Entities/Units/PredatorComponent.cs`: retire four digestion/absorption conversion or rebirth branches driven by KuroTenko/PredConverter, DigestionConversion, DigestionRebirth and PredRebirther. These restored prey HP, changed allegiance/species and released characters instead of completing ordinary digestion/absorption.
- Keep the ordinary absorption log, XP, healing/mana, callbacks, trait effects and prey cleanup. Remove the uncalled, comment-only HandleShapeshifterRebirth helper tied to those branches. Stored IDs/traits/settings and temporary disguise/summon behavior remain unchanged. Related spec: 05 and design clarifications 1/11/12.
- Dated notice added; prior licensing/attribution preserved. No external additions or file deletions/moves.

## 2026-10-31 — 05A3B9 unused Resurrection helper retirement

- `Assets/Scripts/Modes/Tactical/TacticalUtilities.cs`: retire FindUnitToResurrect, CreateResurrectionPanel, Resurrect and uncalled GetRandomTileForActor; preserve the separate reanimation picker/runtime.
- `Assets/Scripts/AI/TacticalAI.cs`: remove TryResurrect/CheckResurrect and the Resurrection action-planning branch.
- `Assets/Scripts/AI/StandardTacticalAI.cs`, `Assets/Scripts/AI/NonCombatantTacticalAI.cs`, `Assets/Scripts/AI/HedonistTacticalAI.cs`: remove obsolete TryResurrect calls; NonCombatant also loses its Resurrection planning branch. Remaining AI order and reanimation/spell behavior unchanged.
- Related spec: 05, following 05A3B6/B7. Stored spell/item/trait IDs remain. Dated conversion notices and existing attribution preserved; no external additions or file deletions/moves.

## 2026-10-31 — 05A3B10 Reanimate spell retirement

- `Assets/Scripts/Entities/Spells/SpellList.cs`: retire Reanimate's casualty-restoration callback, label its preserved metadata retired, and include it in the existing IsRetired predicate used by usable-spell filtering and both cast guards.
- `Assets/Scripts/Entities/Units/Actor_Unit.cs`: remove the Reanimator trait's automatic single-use Reanimate grant. Stored grants, spell/item/trait identifiers and unrelated summon behavior remain.
- Related spec: 05 and design clarifications 1/10/12. Bind's separate casualty re-summoning path, reanimation helpers and AI remain pending their own cleanup. Dated notices and existing licensing/attribution preserved; no external additions or files deleted/moved.

## 2026-10-31 — 05A3B11 Bind/Resummon retirement

- `Assets/Scripts/Entities/Spells/SpellList.cs`: remove Bind's actor/tile callbacks, mark its retained metadata retired and include Bind in IsRetired for existing usable-list/cast guards.
- `Assets/Scripts/Entities/Units/Actor_Unit.cs`: remove Binder's single-use grant and CastBind/SummonBound routines, retiring summon theft, persistent BoundUnit capture, unbound permanent-character conversion and casualty/later-battle re-summoning.
- Related spec: 05 and design clarifications 1/10/12. Stored BoundUnit and spell/trait/item IDs remain, as do independent temporary summons/clones/disguises. Obsolete AI and shared reanimation helpers remain the next bounded cleanup. Dated notices and prior attribution/license preserved; no external additions, file deletions or moves.

## 2026-10-31 — 05A3B12 obsolete reanimation/Bind helper retirement

- `Assets/Scripts/Modes/Tactical/TacticalUtilities.cs`: remove FindUnitToReanimate/CreateReanimationPanel/Reanimate, their private picker context and now-unused UnityEngine.UI import.
- `Assets/Scripts/Modes/TacticalMode.cs`: remove unused HandleReanimationSideEffects, preserving the serialized UnitPickerUI field and scene.
- `Assets/Scripts/AI/TacticalAI.cs`: remove TryReanimate/CheckReanimate/RunBind/CheckBind and their action-planning branches.
- `Assets/Scripts/AI/StandardTacticalAI.cs`, `Assets/Scripts/AI/HedonistTacticalAI.cs`: remove obsolete reanimation/Bind action calls. Remaining AI priorities and ordinary summon creation remain unchanged.
- Related spec: 05, following 05A3B10/B11. Stored identities/trait/item metadata retained. Dated notices and prior license/attribution preserved; no external additions or files deleted/moved.

## 2026-10-31 — 05A3B13 damage/surrender defection retirement

- `Assets/Scripts/Entities/Units/Actor_Unit.cs`: remove TurnCoat's damage-triggered allegiance switch and automatic surrender's defection branch, including its AI eating-avoidance and conversion notices. Remove associated Goddess Mercy full-health restoration for both defecting and loyal surrender outcomes.
- Preserve existing damage/barrier processing, Cowardly surrender, automatic-surrender trigger/1 HP/zero movement and configured prey release. New Downed/temporary HP/survivor baseline work is not implemented. Related spec: 05 and design clarifications 8/12.
- Trait/settings IDs retained; dated notice and prior attribution/license preserved. No external additions or file deletions/moves.

## 2026-10-31 — 05A3B14 allegiance-changing spell retirement

- `Assets/Scripts/Entities/Spells/SpellList.cs`: retire CaptureNet, Charm, HypnoGas and Whispers execution callbacks, mark preserved metadata retired and add all four to IsRetired for existing usable-spell filtering/cast rejection.
- `Assets/Scripts/Entities/Units/Actor_Unit.cs`: remove automatic Charmer/HypnoticGas grants; reject retired direct CastStatusSpell calls before mana/side effects, covering Whispers' trait callback bypass.
- Related spec: 05 and design clarification 12. Stored spell/item/trait IDs and status definitions remain; ordinary summons/disguises and other spells unchanged. This does not finish all trait/containment allegiance paths. Dated notices, existing license/attribution preserved; no external additions or files deleted/moved.

## 2026-11-01 — 05A3B15 support-trait allegiance retirement

- `Assets/Scripts/Entities/Units/Actor_Unit.cs`: retire SeductiveTouch's enemy-support permission exception and its 38-line conversion/distraction branch, including allegiance/fixed-side changes and linked paralysis outcome. Preserve the underlying support action and other permission sources.
- Related spec: 05 and design clarification 12. Trait IDs/metadata remain; possession/metamorphosis/containment callbacks remain separate review. Dated source notice and existing license/attribution preserved; no external additions or files deleted/moved.

## 2026-11-01 — 05A3B16 metamorphosis-family retirement

- `Assets/Scripts/Races/Traits.cs`: retain Metamorphosis, MetamorphicConversion and ForcedMetamorphosis class/registry identities with retired descriptions and inert inherited callbacks. Retire full-health revival, XP/species/side mutation, forced grants, no-escape interfaces and the forced trait's incoming escape boost.
- ForcedMetamorphosis now derives from non-boosting VoreTrait so no null stat-booster delegate is invoked. Existing callbacks continue ordinary digestion. Other traits/disguises/summons remain unchanged. Related spec: 05 and design clarifications 1/11/12.
- Dated conversion notice added; upstream comments/attribution/license preserved. No external additions or file deletions/moves.

## 2026-11-01 — 05A3B17 consolidated ability-retirement completion

- `Assets/Scripts/Enums/Tags.cs`
- `Assets/Scripts/Races/Traits.cs`
- `Assets/Scripts/Entities/Units/Unit.cs`
- `Assets/Scripts/Entities/Units/Actor_Unit.cs`
- `Assets/Scripts/Entities/Units/Prey.cs`
- `Assets/Scripts/Modes/TacticalMode.cs`
- `Assets/Scripts/Modes/Strategic/StrategicUtilities.cs`
- `Assets/Scripts/Modes/Recruit_Mode.cs`
- `Assets/Scripts/AI/StrategicArmyCommander.cs`
- `Assets/Scripts/Utility/Texts/HoveringTooltip.cs`
- `Assets/Scripts/Entities/Items/ItemRepository.cs`

- Added one 31-ID retirement policy for the already excluded revival/replacement, conversion, possession and allegiance traits. Active trait lookup/views/counts and random selection suppress retired entries without changing stored IDs/data; tooltip descriptions identify retirement.
- Closed direct possession/controller/corruption, status charm/hypnosis/respawn, prey-side conversion, tactical alignment, Friendosoma result-conversion, legacy empire-leader revival and soul-imprint creation paths. Existing ordinary non-conversion result fallback is preserved; no new fate system.
- Removed Leadership aura, automatic growth, AI growth weight and combat power terms. Filtered retired ability books from random generation. Preserved other spells, temporary disguises, clone/animal summons and assimilation runtime (only its old authoring tool was approved for removal).
- Related spec 05; this completes the current approved ability-retirement pass, not all strategic/provider cleanup or the new combat/health system. Dated source notices, exact-path record and upstream license/attribution retained; no external code/dependencies/assets or file deletions/moves.
- Documentation: recorded the user's combined-test cadence in AGENTS.md; this instruction edit is not a gameplay-source change.

## 2026-11-01 — 05A4 campaign combat-provider retirement

- `Assets/Scripts/Modes/TacticalMode.cs`: retire empire-trait injection and both armies' strategic tower/barrier/caster/encampment combat effects, including activation dialogs, spell/mana/soul/camp bookkeeping, reinforcements and related wait coroutines/fields/report text.
- Disconnect the campaign race-defection processor; initialize extra participant lists and initial statistics directly. Neutralize obsolete defection-only empty-side messages and make the optional village race fallback null-safe.
- Preserve tactical map/scenery buildings, village skirmishes, creature/trait summons, tactical pause, AI policies, statistics/results and Pure Tactical startup/return. Related spec: 05. This removes campaign providers, not the shared Army/World compatibility types or new company mechanics.
- Dated source notice added; prior licensing/attribution preserved. No third-party source/dependencies/assets added, paths deleted/moved or scene/prefab/schema edits.

## 2026-11-01 — 05A5 World/Army campaign lifecycle retirement

- `Assets/Scripts/Core/World.cs`: remove unused map-editor construction branch and five uncalled monster/empire initialization/rebuild/turn-order/population-name routines. Keep inert compatibility entries for old bandit-limit/turn-order UI callers; retain all World fields and lookup services. Constructor now explicitly creates the retained standalone context without a boolean mode.
- `Assets/Scripts/Subwindows/CreateTacticalGame.cs`: update the sole World constructor call; standalone initialization matches the previous false branch.
- `Assets/Scripts/Entities/Army.cs`: retire campaign Refresh, civilian DevourHeal/capacity, paid Train and village recovery calculation; remove now-uncalled village inventory/XP/trait processing and AI spell redistribution. Keep empty compatibility entries where old callers remain; preserve tactical roster/item/size/movement helpers and serialized fields.
- Related spec 05. New company recovery, wages, training and world generation are not implemented or delegated to old campaign routines. Dated notices and existing licensing/attribution preserved; no external additions, paths deleted/moved or scene/prefab/schema edits.

## 2026-11-01 — 05A6 battle side/diplomacy isolation

- `Assets/Scripts/Modes/Tactical/TacticalUtilities.cs`: retain existing standalone control, preferred-side, hostility and disguise-visibility logic without empire diplomacy/alliances or LastHumanEmpire. Shared signatures remain for callers.
- `Assets/Scripts/Modes/TacticalMode.cs`: retire campaign-only AI retreat setup/calls, world-empire-dependent skip override and strategic village-color override. Preserve existing Pure Tactical AI defaults, skip rules, manual retreat and scene presentation.
- `Assets/Scripts/Entities/Units/Unit.cs`: remove four strategic AI cheat-level XP multipliers and empire-trait reinjection in ReloadTraits; retain ordinary XP formulas, trait/conditional modifiers and level progression.
- Related spec 05. New campaign retreat policies and battle-context integration remain separate work. Dated source notices, prior attribution/license retained; no external additions, file deletions/moves or scene/prefab/schema edits.

## 2026-11-01 — 05A7 residual-boundary review and direct metadata lookup

- `Assets/Scripts/Races/RaceParameters.cs`: replace the cached randomized dummy Unit used by GetRaceTraits with direct enum-to-definition dispatch. GetTraitData preserves its disabled-racial-traits/null-unit behavior and delegates to the same metadata dispatch. Prototype payloads, all case/result mappings, eligibility and unknown-ID fallback remain unchanged.
- Remove metadata-triggered dummy stat rolls; gameplay RNG consumption changes at the former first metadata allocation, with no same-seed equivalence promise. No balance/stat/graphics/override schema changed.
- Added consolidated active/retired/shared provider review and refreshed caller inventory for the next grouped stat/override boundary work. Related specs 05/03. Dated notice, prior license/attribution preserved; no external additions or file deletions/moves. This is not removal of rejected species prototypes or completion of phase 2.


## 2026-11-01 — 03B3C stored species reads and intentional edits

- `Assets/Scripts/Utility/Stored/RaceSettings.cs`
- `Assets/Scripts/Entities/Units/Unit.cs`
- `Assets/Scripts/Races/Graphics/DefaultRaceData.cs`
- `Assets/Scripts/Entities/Units/PredatorComponent.cs`
- `Assets/Scripts/Entities/MercenaryHouse.cs`
- `Assets/Scripts/Entities/Village.cs`
- `Assets/Scripts/Modes/Strategic/StrategicUtilities.cs`
- `Assets/Scripts/AI/RaceServantTacticalAI.cs`
- `Assets/Scripts/Subwindows/RaceEditorPanel.cs`
- `Assets/Scripts/Subwindows/RandomizerTraitEditor.cs`
- `Assets/Scripts/Subwindows/TaggedTraitEditor.cs`

Separate non-creating GetExisting reads from GetOrCreate writes throughout the caller group. Resolve power multipliers without hydrating graphics or settings, retain zero-value fallback, and preserve global cosmetic defaults when no override exists. Editor/assimilation edits explicitly create records; randomizer cleanup only edits existing records. New settings for permanently retired species are rejected before metadata/graphics hydration, while existing rows remain accessible. No parameter prototypes, enum IDs, assets, save fields or approved species changed. See docs/implementation/03b3c-stored-override-read-boundary.md.


## 2026-11-01 — 03B3D rejected species stat retirement

- `Assets/Scripts/Races/RaceParameters.cs`: retire all 90 rejected species prototype declarations, initializer payloads and matching enum dispatch arms together. Preserve all 64 nonretired species definitions plus Default exactly. Explicitly reject retired metadata requests before the racial-traits-disabled/default path; retain existing null-unit/traits-disabled and unrelated unknown-ID behavior. Numeric IDs, serialized settings records, remaining raw mapping destinations, shared supporting types, graphics and assets unchanged. See `docs/implementation/03b3d-rejected-stat-definition-retirement.md`.


## 2026-11-01 — 03B4 rejected graphics registry retirement

- `Assets/Scripts/Races/Races.cs`
- `Assets/Scripts/Subwindows/UnitCustomizer.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Centaur.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Humans.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Imps.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/MainlandElves.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Umbreon.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Yordles.cs`
- `Assets/Scripts/Races/Graphics/Mercs/Cockatrice.cs`
- `Assets/Scripts/Races/Graphics/Mercs/Goblins.cs`
- `Assets/Scripts/Races/Graphics/Mercs/OoviKat.cs`
- `Assets/Scripts/Races/Graphics/Mercs/Renamon.cs`
- `Assets/Scripts/Races/Graphics/Mercs/Vipers.cs`
- `Assets/Scripts/Races/Graphics/Mercs/Youko.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Auri.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Salix.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Zoey.cs`
- `Assets/Scripts/Modes/Tactical/TacticalGraphicalEffects.cs`
- `Assets/Scripts/Entities/Clothing/ClothingDiscards.cs`

Retire 91 eager rejected graphics instances (the 90 species plus Slime Queen) and 90 lookup arms, with explicit rejection before BlankSlate fallback. Preserve 66 other instance declarations, 64 species mappings and Ant Queen. Disconnect rejected catalog/customizer/projectile hooks; private retired clothing callbacks use the drawing actor instead of deleted global instance fields so the retained source remains compilable. No graphics class files, sprites, loaders, bundles or serialized assets deleted/moved/rewritten. See docs/implementation/03b4-rejected-graphics-registry-retirement.md.


## 2026-11-01 — 03B5 retired-only sprite-loader bindings

- `Assets/Scripts/Core/AssetBundleLoader.cs`: replace 156 bundle sprite-array loads whose code readers are confined to the disconnected rejected graphics classes with explicit null assignments. Preserve the remaining 199 sprite-array load expressions, all four mixed bundle opens, static field declarations, source artwork, bundle files and serialized data. Explicit clearing also prevents stale bindings if the loader is called again. No bundle-size/memory-saving or asset-exclusivity claim. See `docs/implementation/03b5-retired-only-sprite-loader-bindings.md`.


## 2026-11-01 — 03B6 obsolete serialized sprite arrays

- `Assets/Editor/SpeciesResourceAudit.cs` (new fork editor inspection command) and `Assets/Editor/SpeciesResourceAudit.cs.meta`: capture Unity 2018 serialized fields, object references and dependencies into project Logs without resaving assets or rebuilding bundles.
- `Assets/Prefabs/Sprite Dictionary.prefab`: remove exactly 156 obsolete saved blocks corresponding to retired-only static fields already cleared by 03B5 (9,478 lines / 9,181 raw GUID references). Unity confirms none is serialized and its complete inspection snapshots before/after are identical. All other prefab text, identities, active fields and dependencies preserved. This entry supplies the associated modification notice for the non-commentable prefab/meta. No source artwork or bundle changed; no file deletion/move. See `docs/implementation/03b6-obsolete-serialized-sprite-arrays.md`.


## 2026-11-01 — 03B7 source disposition review (documentation only)

Inventory the 91 disconnected rejected graphics definition paths, external type-operation candidates and internal inheritance/imports. Define the grouped payload/effect retirement gate while preserving shared helpers and paths. No gameplay-source or asset changes; see docs/implementation/03b7-disconnected-source-disposition-review.md.


## 2026-11-01 — 03B8 disconnected graphics payloads and projectiles

- `Assets/Scripts/Races/Graphics/MainRaces/Aabayx.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Abakhanskya.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Alraune.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Bears.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Cats.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Centaur.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Crux.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Crypters.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Dogs.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Driders.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Eevee.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Equaleon.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Foxes.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Ghosts.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Harpies.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Humans.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Imps.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Lamia.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/MainlandElves.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Mermen.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Scylla.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/SlimeQueen.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Slimes.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Taurus.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Umbreon.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Wolves.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Yordles.cs`
- `Assets/Scripts/Races/Graphics/Mercs/Cockatrice.cs`
- `Assets/Scripts/Races/Graphics/Mercs/DewSprite.cs`
- `Assets/Scripts/Races/Graphics/Mercs/Goblins.cs`
- `Assets/Scripts/Races/Graphics/Mercs/Hamsters.cs`
- `Assets/Scripts/Races/Graphics/Mercs/OoviKat.cs`
- `Assets/Scripts/Races/Graphics/Mercs/Puca.cs`
- `Assets/Scripts/Races/Graphics/Mercs/Renamon.cs`
- `Assets/Scripts/Races/Graphics/Mercs/RwuMercenaries.cs`
- `Assets/Scripts/Races/Graphics/Mercs/Succubi.cs`
- `Assets/Scripts/Races/Graphics/Mercs/Tigers.cs`
- `Assets/Scripts/Races/Graphics/Mercs/Viisels.cs`
- `Assets/Scripts/Races/Graphics/Mercs/Vipers.cs`
- `Assets/Scripts/Races/Graphics/Mercs/Youko.cs`
- `Assets/Scripts/Races/Graphics/Monsters/Bella.cs`
- `Assets/Scripts/Races/Graphics/Monsters/BoomBunnies.cs`
- `Assets/Scripts/Races/Graphics/Monsters/Cake.cs`
- `Assets/Scripts/Races/Graphics/Monsters/Collector.cs`
- `Assets/Scripts/Races/Graphics/Monsters/Compy.cs`
- `Assets/Scripts/Races/Graphics/Monsters/DarkSwallower.cs`
- `Assets/Scripts/Races/Graphics/Monsters/EarthDryad.cs`
- `Assets/Scripts/Races/Graphics/Monsters/Earthworms.cs`
- `Assets/Scripts/Races/Graphics/Monsters/Fairy.cs`
- `Assets/Scripts/Races/Graphics/Monsters/FeraEqualeon.cs`
- `Assets/Scripts/Races/Graphics/Monsters/FeralEevee.cs`
- `Assets/Scripts/Races/Graphics/Monsters/FeralOrcas.cs`
- `Assets/Scripts/Races/Graphics/Monsters/Frogs.cs`
- `Assets/Scripts/Races/Graphics/Monsters/FungalDryad.cs`
- `Assets/Scripts/Races/Graphics/Monsters/Iliijiith.cs`
- `Assets/Scripts/Races/Graphics/Monsters/Pudding.cs`
- `Assets/Scripts/Races/Graphics/Monsters/Raptor.cs`
- `Assets/Scripts/Races/Graphics/Monsters/RiverDryad.cs`
- `Assets/Scripts/Races/Graphics/Monsters/Schiwardez.cs`
- `Assets/Scripts/Races/Graphics/Monsters/Sharks.cs`
- `Assets/Scripts/Races/Graphics/Monsters/SoulSprite.cs`
- `Assets/Scripts/Races/Graphics/Monsters/Vagrants.cs`
- `Assets/Scripts/Races/Graphics/Monsters/ViraeUltimae.cs`
- `Assets/Scripts/Races/Graphics/Monsters/Voilin.cs`
- `Assets/Scripts/Races/Graphics/Monsters/Whisp.cs`
- `Assets/Scripts/Races/Graphics/Monsters/WoodDryad.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Asura.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Auri.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Cherub.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Cierihaka.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/DRACO.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Erin.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Feit.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Ki.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Konane.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Nectar.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Olivia.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Ryan.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Salix.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Scorch.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Selicia.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Seraph.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Seville.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Singularity.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Skapa.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Taraluxia.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Tatltuae.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Vision.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Xelhilde.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Zera.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Zoey.cs`
- `Assets/Scripts/Modes/Tactical/TacticalGraphicalEffects.cs`

Retire 91 disconnected graphics class bodies while retaining paths, earlier modification/legal comment blocks and the shared FairyType/FairyUtil preamble. Remove 27 rejected-only projectile branches/setup helpers from TacticalGraphicalEffects; retain other species and general effects. Initial compile exposed FairyUtil sharing; final validation recorded in docs/implementation/03b8-disconnected-graphics-payload-retirement.md. No art/bundle/scene deletion or new species.


## 2026-11-01 — Consolidated milestone cadence (documentation only)

Record user acceptance of 03B8 and revise AGENTS.md, roadmap and resume checkpoint to group remaining cleanup and conversion work into larger milestones, with manual testing at meaningful workflow/risk boundaries. No gameplay-source or asset changes.


## 2026-11-01 — Consolidated functional cleanup closure

- `Assets/Scripts/Core/AssetBundleLoader.cs`: clear 14 newly orphaned sprite bindings after the accepted 03B8 class/projectile retirement; preserve the remaining 187 asset requests and four mixed bundle opens. Exact bindings and retained residuals in docs/implementation/phase-2-cleanup-closure.md.
- Reconcile specs02–05, documented retained compatibility services, manual acceptance and deferred combat/release checks. Documentation is not new campaign implementation. No files removed/moved, art/bundles rebuilt or species approved.


## 2026-11-01 — 06 company campaign foundation preview

- Added `Assets/Scripts/Core/CompanyCampaign.cs` and `.cs.meta`: canonical identity/roster/town/world state, seeded region and persisted RNG, discovery/movement/action budget and ordered resumable daily-stage contract with campaign-state rollback.
- Added `Assets/Scripts/Core/CompanyCampaignSave.cs` and `.cs.meta`: separate schema/magic, bounded/checksummed immutable generations, validated loading and same-company recovery. Uses existing Odin serializer; no new dependency.
- Added `Assets/Scripts/Subwindows/CompanyCampaignView.cs` and `.cs.meta`: runtime Unity UI map/town/company preview, rest/day navigation, checkpoints and failure handling without scene/prefab rewrites.
- `Assets/Scripts/Modes/StartMode.cs`: main campaign entry opens the explicitly labeled foundation preview; suppress old menu input behind it and close it on returning to main menu. Legacy campaign/load paths remain retired. No species/leader invented; actual campaign creation remains07. Associated notices for new .meta paths are this entry. See docs/implementation/06-campaign-foundation-preview.md.


## 2026-11-01 — People/economy preparation and terrain-art requirement (documentation only)

Record user acceptance of06 preview and requirement to reuse original4X terrain artwork. Trace grouped07–08 Unit/item/customizer/wage dependencies and request species/tuning decisions together. No runtime source, assets, species approvals or numerical tuning changed. See docs/implementation/07-08-people-economy-preparation.md.
