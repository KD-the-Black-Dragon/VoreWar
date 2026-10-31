# Packet 02C — settings retirement and code-owned balance

**Status:** Implemented and compiled, 2026-10-30. User review found blank pages; a malformed-scene repair is implemented, with scene loading validated and the user subsequently reporting successful repaired-settings tests. The user confirmed the three requested 02B checks and authorized the next packet, then confirmed Unity was closed before scene edits. No species eligibility or new campaign mechanics are implemented here.

## Visible result and scope

Content Settings now exposes five pages, in matching selector/array order:

| Page | Retained controls |
|---|---|
| Tactical Gameplay | Surrender Flags and Show Unit Sides only |
| Gender Settings | All existing preferences |
| Appearance Settings | All existing preferences |
| Vore Main | Existing preferences except the user's explicit removal list, including removal of the nested breast/bladder weights |
| Post-Vore | Existing preferences except the user's explicit removal list, including all three weight-loss chance sliders |

Strategic Gameplay, Tactical Misc, legacy Merc / Monster Spawning, Fog of War, Vore Advanced, Vore Misc and Building Settings are inactive and absent from the selector. The [exact control/scene map](02c-settings-control-map.md) accounts for all 215 previous field bindings: 41 toggle and 18 scalar preferences remain, along with navigation and tooltip references; 154 obsolete bindings are disconnected. Old scene objects/files remain; no path was deleted or moved. Normal General Options and the gated Race Editor are unchanged.

The spawning page is hidden pending **03A**, which will supply the simpler curated-species page, generation filters and safe empty-roster state. This packet does not claim an approved roster exists or disable all species. Legacy mercenary availability defaults true and exposed legacy spawner configurations use their code defaults (disabled); applying settings does not clear existing armies/individuals. New enforced exploration fog belongs to spec 06; this packet hides the legacy controls and fixes them to their existing defaults.

Retiring settings controls does not itself remove every related mechanic. For example, the recorded weight-gain/loss defaults remain enabled internally; breast/bladder vore remain disabled by default. Later combat/content packets implement the separate agreed mechanic retirements. No new balance numbers were selected here.

## Defaults and persistence

`Assets/Scripts/Core/ContentSettingsData.cs` is the central owner:

- `ToggleDefaults` retains all 105 values extracted in 02A. `PreferenceToggles` explicitly identifies the 41 keys still player-changeable; the other 64 always use their recorded code defaults.
- `ApplyLockedValues` assigns the 72 retired scalar/list values using the same literal fallbacks in the [02A inventory](02a-settings-defaults-and-precedence.md), plus ten fields whose old widgets wrote values without matching preference reads: NightRounds 2; BaseNightChance .01; NightChanceIncrease .01; DefualtTacticalSightRange 1; NightStrategicSightReduction 1; DayNightMonsterMovemnt Off; BaseCritChance .05; CritDamageMod 1.5; BaseGrazeChance .10; GrazeDamageMod .30. Developers can change these explicit values in code.
- `LoadSaved` first applies locked defaults, then reads only retained global preferences. `SavePreferences` writes only those retained keys and flushes PlayerPrefs. It does not delete retired keys, reset unrelated Options, or consume old saved spawning/balance overrides.
- HermBreastSizeModifier now reads its existing saved key with fallback zero, matching its retained slider and existing write. Previously it had a write but no corresponding startup preference read. All other retained keys, fallback values and slider/enum conversions are preserved.

| Boundary | Applied policy |
|---|---|
| Startup / return to start / explicit reload | Existing ContentSettings.Refresh route loads retained global preferences and code defaults; BuildingConfig uses its code initializers. |
| Opening settings | Enforce locked values without reloading/overwriting the current retained choices; populate only retained controls. |
| Exit / Save | Apply retained widgets and reassert locked values. Save writes only retained preferences; ordinary Exit remains current-session application. |
| State.Load | After installing saved WorldConfig, apply policy before unit/mode setup; reapply after legacy migrations so they cannot restore retired balance overrides. Saved building overrides are ignored in favor of code defaults. |
| Legacy setup's content-settings import | Apply the same global preference/default policy immediately after its WorldConfig assignment; ignore imported building overrides. |

Source review found these two WorldConfig replacement paths plus its static initialization. Unrelated saved map/setup fields are not replaced wholesale. Existing startup/return hooks enforce policy after initialization. Race Editor presets edit species data rather than supplying an alternative settings source.

## Source and scene changes

- `Assets/Scripts/Subwindows/ContentSettings.cs`: retain only live widget/key bindings and apply/open/save callbacks; remove dynamic mercenary/spawner construction, retired callbacks, and BuildingSettings hard/soft load/save hooks. Preserve the live-individual update attached to the retained gender preference. Bound page selection and use only retained weight-interaction controls.
- `Assets/Scripts/Core/ContentSettingsData.cs`: explicit preference allowlist, code-owned retired defaults and retained preference saving.
- `Assets/Scripts/Core/State.cs`: enforce policy at save restoration and after migrations; initialize building defaults instead of loading saved building rules.
- `Assets/Scripts/Subwindows/CreateStrategicGame.cs`: enforce the same rule on its legacy content import.
- `Assets/Scripts/Utility/DisplayTooltip.cs`: remove the hard-coded SettingsPanels[11] lookup/building tooltip branch; retained tooltip routing works with the shorter page array.
- `Assets/base.unity`: align five panel references and five selector labels; disconnect retired component fields; deactivate 48 page/widget roots; disable 77 obsolete callbacks targeting ContentSettings. Exactly 100 serialized blocks differ from the post-02B baseline. No blocks added/removed, shared prefab edited or unrelated serialization changes introduced.

For each inactive prefab instance, the root source GameObject was verified against the source prefab rather than assuming every widget used the Button prefab. Three weight-loss sliders inherit RefreshSliderText from the self-contained-text slider prefab; instance overrides now disable that callback. A pre-existing DigestionRampChanged callback had no implementation in ContentSettings and is also disabled with the retired controls. Kept controls with obsolete shared callbacks (for example CorpsesChanged) are detached from those callbacks while retaining their own preference.

## Earlier unresolved bindings

The earlier study's unresolved bindings were resolved through stripped objects, prefab instances, transforms and parents before editing. They are now included with IDs in the full control map:

| Binding/group | Resolved page |
|---|---|
| WeightLossFractionBreasts / Body / Dick | Post-Vore, each within its weight-loss slider group |
| AutoSurrenderChance / AutoSurrenderDefectChance | Tactical Gameplay |
| AllMercs / MercSortDirection / MonsterConquestTurns | Legacy Merc / Monster Spawning |
| ScoutMax / MaxArmies | Strategic Gameplay |
| LeaderTraits / MaleTraits / FemaleTraits / HermTraits / SpawnTraits | Tactical Gameplay |

## Follow-up: blank-page regression and repair

The user reported that every retained page was blank and Show Unit Sides was unavailable. Their Editor.log showed `Unable to parse file Assets/base.unity: [did not find expected key]` at line 320537, followed by null references in ContentSettings.Open and ActivatePage. The scene field-removal edit had left two continuation lines from the wrapped TogglePrefab and MonsterSpawnerPrefab references. Those fragments prevented the settings component's references from deserializing; the retained controls themselves had not been removed.

Removed only the two malformed continuation lines from component 1620494162. All other scene blocks and every retained field/page reference remain unchanged by this repair. The earlier compilation and block-scope checks did not establish valid scene deserialization; that limitation is now explicit. Future serialized-scene checks for these packets must actually open the affected scene and inspect Unity's log for parse/broken-reference errors, in addition to checking the diff and compilation.

Unity 2018.3.12f1 explicitly opened `Assets/base.unity` and exited with code 0, with no scene parse errors, broken GameObject reference messages or exceptions in `Logs/packet-02c-scene-repair-open.log` (ignored). The initial attempt using `-openfile` in batch mode was rejected by Unity; the successful follow-up opened the scene without batch mode and exited automatically. This checks scene loading, not Play-mode rendering or preference persistence. No new test framework or permanent diagnostic source was introduced. Manual Play-mode visibility and persistence remain pending after repair.

## Validation and review limits

- Fresh Unity 2018.3.12f1 compilation of Assembly-CSharp and Assembly-CSharp-Editor passed with no C# errors; batch import/compile exited successfully with return code 0. Evidence: ignored `Logs/packet-02c-compile.log`. This is not a player build or Play-mode test.
- Compared scene block identities/hashes with the post-02B preflight; exactly the intended 100 blocks changed, preserving 02B edits. The import did not rewrite the scene. Original LF line endings retained.
- Source review checked the retained key/default/conversion correspondence, both saved configuration replacement paths, callback disconnection, tooltip routing, and absence of remaining BuildingSettings load calls. No tests or framework added.
- Restored the two known Unity-removed orphan metadata files byte-for-byte from tracked contents: `Assets/Scripts/Core/obj.meta` and `Assets/Scripts/Core/bin/Debug/net10.0.meta`. No unrelated files removed, moved or reset.
- User confirmation covers the three requested 02B checks; it is not a claim that every broader mode/save route has been playtested. This packet's navigation, layout, preference round-trip and legacy-save restoration remain user/runtime checks.

Player-facing review in Unity Play mode, opening `Assets/base.unity`:

1. Open Content Settings and visit all five pages. Check that retired pages and listed controls are absent, Tactical Gameplay has only its two display toggles, and Gender/Appearance remain available.
2. Change Show Unit Sides, use Save/Exit, stop and restart Play mode, then reopen it to confirm persistence. Restore the preferred value afterward.
3. Hover several retained controls on different pages; confirm tooltips and page changes cause no red Console errors. Check normal Options and Race Editor still open normally (with the password for Race Editor).

After feedback, proceed with 03A's curated-species/empty-roster boundary and replacement species page, following the consolidated removal order. Spec 02 remains open for that integration and runtime checks.

## Subsequent user confirmation

The user reported that the repaired settings tests succeeded and authorized the next step. This closes the reported blank-page regression and this packet's requested manual review; it does not claim broader campaign/battle tests. 03A1 now integrates the replacement species page, with its own review gate.

## GPL/source-change check

All five changed C# files have prominent dated fork notices preserving meaningful earlier notices. Root CHANGES.md records their exact paths and the associated dated notice for `Assets/base.unity`. License.txt Git content and upstream attribution/legal text are preserved. No new dependency, externally copied code or asset was introduced. These checks satisfy this packet's source-change workflow; they are not a complete distribution/third-party-license audit or permission to publish.
