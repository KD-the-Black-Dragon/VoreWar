# VoreWar fork changes

This checkout is developing a mercenary-company total conversion of VoreWar. The original project is described in [README.md](README.md) as a continuation of darkevilme's Vore War. This record distinguishes this fork's work from that earlier work; original copyright, attribution, license, and warranty notices remain applicable.

The covered derivative work continues under the applicable GNU GPL version 3 terms in [License.txt](License.txt). This record does not change existing license grants or the separate terms of third-party code, dependencies, or assets.

## Recording changes

Record completed source changes by actual date, affected paths, and a short description, with a related spec where useful. Modified source files also receive dated notices under the workflow in [AGENTS.md](AGENTS.md). Mark additions as this fork's additions. Preserve previous entries and do not describe planned removals or features as implemented.

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
