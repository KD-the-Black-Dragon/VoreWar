# Unity and original-game baseline

**Status:** Partially verified, 2026-10-29. Import/compilation and the Windows build passed. The user confirmed the built game reached its main menu. Broader gameplay checks remain pending; automated window capture is unavailable. Spec 00 is not yet complete.

## Reference and environment

- Starting Git revision: `c2910ee56f0468b718c27f7137dfc5a7a2958aea`. This identifies the original tracked source used for this check; no new commit or tag was created.
- Existing work at start: modified campaign-plan documentation, untracked roadmap, and untracked specs. Preserve these changes.
- Workspace: `C:\VS Code Projects\VoreWar`, Windows.
- Required/used editor: Unity 2018.3.12f1; executable `C:\Program Files\Unity\Hub\Editor\2018.3.12f1\Editor\Unity.exe`.
- Unity Hub and Unity 2022.3.3f1 are also installed. The latter was not used.
- Windows standalone support is installed. Other platform modules were not found in this editor's PlaybackEngines directory.
- The enabled build scene is `Assets/base.unity`.
- Project settings use the newer scripting runtime (`scriptingRuntimeVersion: 1`), with no explicit per-platform scripting-backend overrides in the inspected file.

## Dependencies and existing build tooling

The existing package manifest includes Ads 2.0.8, Analytics 3.2.2, Collab Proxy 1.2.15, Package Manager UI 2.0.7, Purchasing 2.0.3, TextMesh Pro 1.3.0, Addressables 1.13.1, and built-in Unity modules. No package or engine upgrade was requested or performed.

An existing command-line build entry point was found in `Assets/MultiBuild/Editor/Builder.cs`: `MultiBuild.Builder.BuildCommandLine`. It takes an explicit output folder, a development-build boolean, and target names after the method name. `Win64` maps to Windows 64-bit. The tool selects enabled scenes and uses the configured product name (`VoreWar`).

The saved default build settings point at `C:/Users/darkf/Desktop/VoreWar V43A`. Do not use the default `Builder.Build()` for this baseline; the explicit command-line path avoids that obsolete location. The inspected `AssetBundlesPreBuilder.OnPreprocessBuild` callback is empty.

## Checks and observations

| Check | Observed result |
|---|---|
| Editor executable and recorded project version | Match: 2018.3.12f1. |
| Concurrent editor/project lock before starting | No Unity editor process or project lock was found. |
| Initial automated launch with restricted permissions | Stopped at licensing and reported no valid activation; network access also timed out. Process was stopped. This did not establish an account/license failure. |
| Retry with normal license/network access | Licensing passed; engine 2018.3.12f1 initialized and package registration/asset import started. |
| Complete import and C# compilation | Passed. The log reports successful batch shutdown and termination with return code 0. |
| Windows player build | Passed through the existing MultiBuild entry point; the build command returned exit code 0. Executable and supporting player/data files are present in Builds/Baseline-2018/Win64. |
| Graphical launch and main menu | User confirmed successful main-menu launch on 2026-10-29. The agent located the running VoreWar window using the exact newly built executable path; the menu itself could not be independently inspected. |
| Automated visual inspection | Initial capture and recovery failed with window-capture timeouts. A fresh attempt after the user's main-menu confirmation also timed out. Accessibility-only inspection succeeded but exposed only the window frame, not the rendered game menu. No gameplay controls were clicked. |
| Original strategic/tactical gameplay | Not checked; main-menu confirmation does not establish combat or campaign behavior. |
| Save/load and settings persistence | Not checked. |

The headless launch reported shader fallback warnings with its Null graphics device. These alone do not establish a problem in a rendered game. Any actual rendering check requires a graphical launch.

During import Unity identified orphan metadata for missing generated folders: `Assets/Scripts/Core/obj.meta` and `Assets/Scripts/Core/bin/Debug/net10.0.meta`. It removed those metadata files automatically. After the editor exited, both files were restored from the starting Git revision; the final diff has no game-code, scene, asset, or metadata changes. A restricted `git restore` attempt could not create the Git index lock, so the reference contents were read with `git show` and written back to the two missing paths without changing the index.

An existing separately installed copy of VoreWar was already running. It was not used or controlled during these checks. The newly built copy was launched separately, and the user subsequently confirmed its main menu. Follow-up inspection targeted only that build's running window.

## Repeatable commands

Run from a PowerShell session with normal access to the installed editor's license and required network services. Keep other Unity instances for this project closed. Paths with spaces must stay quoted.

Import/compile check:

```powershell
& 'C:\Program Files\Unity\Hub\Editor\2018.3.12f1\Editor\Unity.exe' `
  -batchmode -nographics -quit `
  -projectPath 'C:\VS Code Projects\VoreWar' `
  -logFile 'C:\VS Code Projects\VoreWar\Logs\baseline-2018-import-retry.log'
```

Verified Windows build command, using the existing build tool:

```powershell
& 'C:\Program Files\Unity\Hub\Editor\2018.3.12f1\Editor\Unity.exe' `
  -batchmode -nographics -quit `
  -projectPath 'C:\VS Code Projects\VoreWar' `
  -logFile 'C:\VS Code Projects\VoreWar\Logs\baseline-2018-build.log' `
  -executeMethod MultiBuild.Builder.BuildCommandLine `
  'C:\VS Code Projects\VoreWar\Builds\Baseline-2018' False Win64
```

All Unity arguments must precede `-executeMethod` in this build command because the project's parser treats every argument following its output/development options as a target name. Expected output is `Builds/Baseline-2018/Win64/VoreWar.exe`.

Use a new output/log path for future independent baselines when preserving prior artifacts. Logs and build output are ignored by the existing Git configuration; the report retains the important findings without committing raw logs or credentials.

## Gameplay reference still required

Main-menu entry is confirmed by the user. The remaining reference checks are strategic day/turn, battle setup and terrain, ordinary combat, vore/digestion, retreat, battle completion/return, Pure Tactical, settings persistence, and normal save/load. Distinguish agent observations from user playtesting. These remaining checks are pending, not reported as passed.

The architecture study can inspect source if this baseline exposes a blocker, but any prerequisite repair must be documented and scoped separately. Do not silently upgrade Unity, replace packages, or begin feature retirement to obtain a passing baseline.
