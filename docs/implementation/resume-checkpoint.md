# Current work checkpoint

Updated: 2026-10-30. Read AGENTS.md and Git status before work; this checkpoint preserves the staged conversion across interruption.

## Authority and state

- User confirmed 03B2 startup/name reload and authorized the next step. **03B3A is implemented/checked and awaiting editor-load/persistence feedback.**
- No Unity process remained after the successful 03B2 compile/scene checks. Recheck before later compilation; ask for closure only if actually reopened.
- Substantial prior source/docs work is uncommitted, including the user's README changes. No resets, silent commits, deletions, moves or unrelated cleanup. Preserve all existing work.
- AGENTS.md: never delete files/directories; each changed source needs dated notices/exact CHANGES coverage/GPL review. No new tests/framework without request. Scene and source scope comparisons use packet snapshots, not HEAD, which predates the conversion.
- Approved species registry remains intentionally empty; no sample species. All 90 rejected IDs stay permanently retired. No company loop has been implemented yet.

## Accepted work

- 02A defaults extraction, 02B tool retirement/password-gated Race Editor, 02C settings/default enforcement: user accepted. Initial 02C scene YAML break was repaired and verified; keep explicit scene-loading checks alongside compilation.
- 03A1 approval/roles/preferences and empty-roster setup; 03A2 tactical generation/mapping/metadata/restoration boundaries: accepted. Whole generation closure still depends on 03B/05.
- 04A legacy 4X event sources/triggers/loaders/callbacks retired; 04B1 central registry/typed text/Human dummies and 04B2 mixed callback/descriptor reset: accepted. Actual battle-text checks await curation. Prose RNG removal changes shared random consumption; no same-seed equivalence promise.
- 03B preflight accepted: [report](03b-rejected-species-preflight.md), [90-entry matrix](03b-rejected-species-dependency-matrix.md), [resource inventory](03b-shared-sprite-and-bundle-inventory.md). 157 eager graphics fields/154 case mappings; shared clothing/data/name providers. All 383 manifest source sheets/metas exist with matching labels; 372 have serialized GUID candidates, many in Sprite Dictionary prefab. Some serialization is stale relative to static fields. No ownership proof/repack/deletion. Existing bundle hard-rebuild recursively deletes and must not be invoked.
- 03B1 [report](03b1-retired-metadata-catalogs.md): retirement predicate, filtered parent/nested metadata catalogs/previews, unchanged stored mapping/condition preservation, uncalled unique factory retirement. Five sources; final compile/scene passed. User accepted all checks, including after clarification that Keep existing mapping is a no-op on the current saved value, not reset/undo. Do not add a reset mechanic or repeat accepted checks.

## Completed 03B2

- [03B2 source/verification report](03b2-rejected-name-sources.md). Three modified sources: Assets/Scripts/Core/State.cs, Assets/Scripts/Utility/Texts/NameGenerator.cs, Assets/Scripts/Subwindows/Options.cs. Snapshots: ignored Logs/packet-03b2-<filename>-before.txt plus base.unity snapshot.
- State seed manifest: 217 -> 114 basenames, removing 103 exact retired-owner entries. Remaining order preserved, all 114 shipped source files exist. Keep the historical FeralSlimes basename; enum FeralSlime is unretired and correcting that spelling is unrelated. Both initialization paths and explicit name reset share the active manifest.
- NameGenerator: remove 23 retired town pools/3 creature pools (Compy, Vagrants, ViraeUltimae), 24 town branches incl Vagrants special fallback, three creature cases and unused feralOrcasNames declaration. All 28 retained list initializers incl generic male/female/monster names remain verbatim. All 19 remaining town branches/public naming APIs retained.
- Personal species imports skip negative/retired enums, not uncurated/player-disabled entries. Mixed army-name import starts at Race.none and ignores retired/invalid-header sections without leaking them into the preceding allowed section. Shipped army file has 39 recognized headers, 22 retired ignored, 17 retained. Generic/shared and raw shipped files unchanged; no personal name-file contents read/touched.
- Stale retired name requests return neutral Unit / Company / Town N labels. Existing stored names and nonretired naming logic are unchanged. Names may consume State.Rand; rejected requests no longer make their old draws, so no same-seed legacy equivalence.
- Affected ResetNamelists now uses File.Copy(..., true) for active files instead of deleting paths before copying. Normal explicit reset still overwrites active personal lists if the user chooses it. Options reset/reload descriptions reflect active scope; retired/unlisted files remain. Agent did NOT invoke reset, reload, whole-user-data wipe or Play mode.
- Fresh Unity 2018.3.12f1 compile PASSED, exit 0, no C# errors/import exceptions: ignored Logs/packet-03b2-compile.log. Subsequent explicit base.unity load PASSED, exit 0, no parse errors/exceptions: Logs/packet-03b2-scene.log. No player build, automated tests/framework or agent Play-mode test.
- Source correspondence: retained list literals/seed order/town branches/APIs; State outside seed manifest/reset body unchanged; Options outside two descriptions unchanged, apart from notices. Detailed ignored evidence: packet-03b2-source-review.json, retired-name-inventory.json, army-sections.json and source diffs.
- Scene bytes equal packet-start snapshot; license Git content hash preserved; two known Unity-removed orphan metas restored byte-for-byte. Preflight hashes show only accepted 03B1 files plus the three intended current sources changed. Scenes/prefabs/manifests/bundles/raw name files unchanged. Dated notices and exact-path CHANGES entry present; no third-party source/dependency/asset additions. No file deletions/moves or personal-data reset.

## Completed 03B3A

- [03B3A report](03b3a-override-hydration-boundary.md). User accepted 03B2 startup/name reload. Source snapshots: RaceSettings.cs and unchanged references RaceParameters.cs, State.cs, RaceEditorPanel.cs, SpeciesPolicy.cs plus base.unity in Logs/packet-03b3a-*-before.txt.
- Changed ONLY Assets/Scripts/Utility/Stored/RaceSettings.cs. Both Sanitize passes skip permanently retired keys while retaining their raw stored rows; no deletion or default hydration/clamping for them. Nonretired repair/clamps/FeralBats behavior and player-disabled/uncurated handling unchanged.
- Correct NEW RaceSettingsItem LeaderRace/MorphRace assignments from mistaken racePar.ConversionRace to their respective fields. No existing explicit override migration. All 40 serialized declarations/attributes/order, lookup/reset/save APIs and enums preserved.
- RaceParameters still has 154 enum cases/155 prototypes. All 90 retired entries still have prototypes; no stat/graphics definitions removed here. Two remaining owners point to retired species: MatronsMinions.LeaderRace -> Abakhanskya; FeralUmbreon.ConversionRace -> FeralEevee. Do not guess replacement species or reclassify owners.
- State.LoadEditedRaces invokes Sanitize after deserialization and falls back to a new settings object on exceptions. RaceSettingsItem constructor calls both parameter and graphics metadata. Direct Get/read paths remain active, including legacy providers; the Sanitize guards alone are NOT full retired-data closure. No blanket constructor exception, playable fallback, source IDs, gameplay approval, scene/resource or schema changes.
- Fresh Unity compile PASSED, no C# errors/import exceptions, exit 0: Logs/packet-03b3a-compile.log. Subsequent explicit base.unity opening PASSED, no parse errors/exceptions, exit 0: Logs/packet-03b3a-scene.log. No Unity process remained after checks. No agent Play-mode test, personal settings inspection/reset, player build or tests/framework.
- Source reconstruction confirms only two guards/loop adjustment, two corrected assignments and dated notice differ; 40 serialized fields and all four reference files unchanged. Ignored source-review.json and diff hold evidence. Scene bytes match snapshot, license Git hash preserved, both known orphan metas restored byte-for-byte. Game-file hash review shows only RaceSettings.cs beyond accepted 03B1/03B2. Notices/CHANGES and packet diff checks complete; no files deleted/moved or third-party additions.

## Review gate / exact next action

Await user review: enter Play mode, open gated Race Editor, save an existing inspected species without changing mappings; stop/restart Play mode, reopen the same entry and confirm explicit saved choices remain, with no new settings-load/red Console errors. Keep existing mapping no-op semantics are unchanged; do not ask for resets or add undo. No sample species approved.

After feedback, refine/implement bounded **03B3B stat-definition/read-boundary retirement**. Trace direct RaceSettings.Get and RaceParameters getters before removing 90 prototypes. Keep raw retired rows retained but inactive through reviewed paths; ensure nonretired/default metadata remains safe. Preserve numeric identity and supported disabled individuals. Explicit remaining mappings above require retirement-aware handling rather than substitute species. Split graphics registry/eager construction, leader variants and shared clothing/material/customizer changes into separate packets, coordinating real 4X generation/save callers with 05. The existing RaceParameters default fallback must not become an implicit playable substitute. No unsolicited combat/balance/campaign changes, curation or repacking.

03B4 disconnected leaf source and 03B5 shared serialized resources/staged packing remain. Existing raw name files/content stay inactive where applicable under no-delete rules; do not mark physical removal or all generation providers complete.

## Verification commands and cautions

- Unity: C:/Program Files/Unity/Hub/Editor/2018.3.12f1/Editor/Unity.exe
- Fresh compilation: -batchmode -nographics -quit -projectPath <project> -logFile <new packet log>. Normal licensing needs approved/elevated tool execution. Inspect completed log/exit, not only launch return.
- Explicit scene load AFTER compile: Start-Process -WindowStyle Hidden with -quit -openfile <Assets/base.unity> -logFile <new scene log>. Unity rejects -openfile in batch mode; normal open/quit alone may skip compilation.
- Unity removes tracked Assets/Scripts/Core/obj.meta and Assets/Scripts/Core/bin/Debug/net10.0.meta. Restore missing paths byte-for-byte using git show HEAD:<path>, verified inside workspace. Never delete or use destructive reset.
- Preserve scene LF bytes. Python text reads/writes specify UTF-8. License.txt uses an older encoding; do not rewrite it. Compare its Git content hash.
- Existing README trailing whitespace is prior user work; scope diff checks to current files. Password is source-discoverable by user choice; do not repeat it in chat/reports/checkpoints.
