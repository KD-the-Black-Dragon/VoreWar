# Packet 02B — retired tool routes and the Race Editor gate

**Status:** Implemented and passed the Unity import/compile check, 2026-10-30; awaiting manual navigation/password review before 02C. User approved proceeding after confirming Show Unit Sides persistence, supplied a project-only convenience password, and closed Unity for completion. No files are deleted or moved.

## Intended visible result

- Remove the startup Randomizer/Custom/Conditional/Assimilable authoring entries, tutorial and Map Editor entries, and in-game Map Editor, Empire Traits, World Settings, Cheat Menu and Bestiary entries.
- Keep Race Editor available through a masked password prompt. Correct input opens its unchanged General Options, Trait Options and Unit Tags Options; incorrect input and Cancel/Escape do not open it. Closing the editor revokes access, so the next opening asks again. The actual password is deliberately omitted from documentation/log output.
- Preserve nested tag list/modifier editing and the tagged-trait search inside an authorized Race Editor session. Block standalone tag opening; closing Race Editor closes its nested panels. Escape returns through nested panels and then cancels Race Editor without saving, using its existing cancel routine.
- Disable the five persisted player cheat flags without deleting their stored preferences. Remove the UnitEditor hyperlink and click path, and block both Unit Editor opens and the reflection Variable Editor entry.
- Preserve normal Options, Content Settings, Help navigation, save/load, Pure Tactical configuration, existing species/gameplay and the currently still-present strategic creation flow. Their later retirement/rework is not folded into this packet.

## Source changes

| Source | Change |
|---|---|
| `Assets/Scripts/Subwindows/GameMenu.cs` | Shared password/session owner, masked existing InputBox prompt, retired public callback guards, nested tag access, editor Escape routing, and prevention of menu refresh re-enabling three retired controls. |
| `Assets/Scripts/Modes/StartMode.cs` | Block startup authoring/tutorial/map entry methods, retain normal game creation/presets, route trait warning through the gate, remove its ungated editing listener, and dispatch editor Escape while on the main menu. |
| `Assets/Scripts/Core/GameManager.cs` | Block the alternate in-game map editor callback before any mode/world mutation. |
| `Assets/Scripts/Subwindows/CheatMenu.cs` | Block opening and force all five retired cheat flags false through the loader already used at startup and State.Load; do not erase preferences. |
| `Assets/Scripts/Subwindows/RaceEditorPanel.cs` | Require authorized activation/ShowPanel; revoke session access on disable. Normal tab/body/save logic is retained. |
| `Assets/Scripts/Subwindows/UnitTagEditor.cs` | Gate nested tag-list opening. |
| `Assets/Scripts/UI/Connectors/UnitTagMenu.cs` | Gate modifier-panel opening and preserve its return route. |
| `Assets/Scripts/Subwindows/InfoPanel.cs` | Stop rendering the UnitEditor link. |
| `Assets/Scripts/UI/UnitInfoPanel.cs` | Remove the special link click handler; retain ordinary inspection. |
| `Assets/Scripts/UI/UnitEditorPanel.cs` | Block actor/unit entry methods. |
| `Assets/Scripts/Subwindows/VariableEditor.cs` | Block public-facing reflection-edit opening; Race Editor's retained tabs do not use it. |

Retired public callback methods remain small explanatory guards so retained serialized references cannot resurrect a tool or mutate a world before showing a retired-tool message. Old component files/helpers remain compiled for later dependency retirement; this packet does not claim all their source/assets have been physically removed. Existing gameplay trait/tag data is not removed with its authoring menus.

## Targeted serialized scene changes

The affected controls are instances of the existing UI Button prefab (`21f47303e48e1fe469c779c39917362d`), whose default callback array is empty. The source prefab's root object is `1913157303044632`. Added or updated each listed instance's root active override to zero and disabled its existing persistent call-state overrides. All unrelated scene blocks and the shared prefab remain unchanged.

| Prefab instance ID in `Assets/base.unity` | Retired entry |
|---:|---|
| 501285158 | Startup Custom Traits Pool |
| 705546191 | Startup Map Editor |
| 780291862 | In-game Empire Traits |
| 818878891 | Startup Random Traits Pool |
| 887282397 | In-game Map Editor |
| 906853544 | In-game World Settings |
| 1347230471 | In-game Bestiary (direct GameObject.SetActive route) |
| 1588888078 | In-game Cheat Menu |
| 1609019696 | Startup Tutorial |
| 1871040032 | Startup Assimilable Traits |
| 1905818411 | Startup Conditional Traits Pool |

Retained the startup and in-game Race Editor callbacks (instances 420957750 and 1809089759) and preset behavior. For trait warning 1301418983, the earlier inventory found a call-state override of 2, but its actual array size was **zero**. The old working behavior came from StartMode's ungated runtime listener. Set its array size to one for the already specified OpenRaceEditor callback and removed that old listener. Thus the warning is a third explicitly gated route, rather than relying on a misleading call-state value outside the live array.

The fourteen unresolved Content Settings bindings from study 01A are not edited here; their page/control work remains 02C's preflight. This packet resolves its own affected menu prefab and source callbacks without rewriting the entire scene.

## Subsequent user feedback

The user confirmed the three requested retired-entry/password/nested-tag checks and authorized the next packet. The pending/manual statements below record the original handoff state; they are superseded for those three checks only. Broader mode/save behavior was not separately confirmed.

## Validation and user review

- 02A user evidence: Show Unit Sides persisted between exiting and coming back. This records exactly the reported check; broader settings/save round-trips were not independently playtested.
- Source/scene preflight reviewed: exact callback owners, prefab GUID/root identity, inherited empty callback array, and normal Race Editor tabs/nested editor routes.
- Source review confirmed that the six normal tab/save/cancel/nested-entry method bodies checked in RaceEditorPanel are unchanged. All eleven modified scripts have dated notices.
- Scene review compared serialized block identities and content against the baseline: exactly the twelve listed prefab instances changed; no object blocks were added/removed, and the ordinary Race Editor and three preset instances are unchanged. The scene diff is 59 added/19 removed lines; no shared prefab or unrelated scene serialization changed.
- Unity 2018.3.12f1 import/compile check succeeded with no C# errors and return code 0; evidence is ignored `Logs/packet-02b-compile.log`. The user's open editor had already compiled Assembly-CSharp and Assembly-CSharp-Editor after the code changes, so the final batch check reused the current assemblies. Verified Assembly-CSharp is newer than every changed runtime source file; this was not a new player build or a runtime password test.
- Restored `Assets/Scripts/Core/obj.meta` and `Assets/Scripts/Core/bin/Debug/net10.0.meta` byte-for-byte from their tracked contents after Unity removed those known orphan metadata files. License.txt Git content is unchanged; no package/project-settings changes.
- Earlier desktop inspection located the correct open editor. Screenshot capture failed with `FrameArrived timed out`; indexed menu input then reported `coordinate input geometry is unavailable`. No Play toggle, close command, password entry, tool action or save/reset was successfully performed through desktop automation. The user subsequently confirmed Unity was closed; scene edits and the batch check then completed. Manual navigation/password behavior remains unverified.
- No automated tests/framework added. Do not describe source inspection as a successful runtime password/navigation test.

Player-facing checks after compilation:

1. Open the starting scene in Unity Play mode and check that the listed retired entries are gone from their menus.
2. Open Race Editor: try Cancel/Escape and a wrong password; then use the configured password and inspect all three tabs.
3. Open nested Unit Tags editing and return; close Race Editor and confirm reopening asks for the password again. Cancel changes rather than altering species data for this check.
4. In Pure Tactical setup and an in-game menu, confirm retained setup/Options/Content Settings/Help/save routes still work and retired tools stay absent. Confirm ordinary unit inspection works without a UnitEditor link.

## GPL/source-change check

All eleven modified C# files have prominent dated fork notices and retain pre-existing attribution/legal text. Root CHANGES.md lists exact paths and the associated scene path/date for non-commentable serialization. No new dependencies, externally copied code or assets are introduced. The convenience password is intentionally source-discoverable and is not printed in reports. License.txt/upstream attribution are retained; distribution and inherited third-party terms remain a separate review.
