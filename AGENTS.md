# AGENTS.md

Working instructions for contributors and AI agents in this VoreWar checkout. Read this file at the start of each task, then inspect the relevant project files and current Git state before making changes.

## Project identity

- **Project:** VoreWar, a turn-based tactical game with RPG elements in a vore-related setting. The README describes it as a continuation of darkevilme's Vore War.
- **This checkout:** The `KD-the-Black-Dragon/VoreWar` fork (`origin`). The configured source repository is `Flame-Valxsarion/VoreWar` (`upstream`). Verify `git remote -v` if repository identity or remotes matter; remotes can change.
- **Engine:** Unity **2018.3.12f1**, recorded in `ProjectSettings/ProjectVersion.txt`.
- **Code and content:** C# scripts and Unity assets are under `Assets/`; package dependencies are declared in `Packages/manifest.json`.
- **License:** The repository includes GNU GPL version 3 in `License.txt`. Retain the license and upstream copyright/attribution notices. Check individual asset and dependency notices before changing or redistributing them; they may have separate terms.
- **Open the project:** Use Unity Hub or the Unity Editor with the version recorded above. Do not assume Node, Vite, ChatGPT Sites, OpenSpec, or the previous project's release/deployment tools apply here.
- **Build and tests:** Follow instructions found in this checkout and its installed Unity project. No root-level automated test or build workflow was identified during initial orientation; inspect the relevant code, packages, and documentation before recommending or inventing one.

## Collaboration preferences

The user leads creative direction and may describe intent in technical terms. Preserve the requested experience and mechanics. Treat a proposed technical implementation as a way to express the goal, not automatically as a binding implementation detail. If another approach would materially improve correctness, performance, readability, maintainability, or compatibility with this older Unity version, explain the tradeoff in plain language before making a non-obvious substitution.

The user is not a programming specialist. Explain decisions and results in clear, concise language, and give player-facing checks rather than asking the user to perform code inspection or run technical commands when the agent can do that work.

A direct request to make a change authorizes work within that request. A request to plan, review, or discuss does not authorize implementation. Keep work to the requested scope; do not introduce unrelated cleanup, refactors, features, or changes to upstream behavior. For larger work, state the scope and split it into reviewable steps. Ask when a gameplay or product requirement is genuinely ambiguous; do not invent mechanics.

## Preserve the game and the fork

- Preserve existing behavior unless the user requests a change.
- Keep changes as small and clear as practical. Prefer straightforward, idiomatic C# that fits Unity 2018.3.12f1 and the existing codebase; check API and language compatibility before using newer Unity or C# features.
- Respect the upstream project's structure, attribution, copyright notices, asset provenance, and GPL-3.0 license. Do not replace or remove license notices as part of routine changes.
- Avoid unrelated asset reimports, package upgrades, scene/prefab rewrites, or broad formatting changes. Unity can modify serialized assets as a side effect; inspect the resulting diff and keep only changes required by the task.
- Do not assume this fork uses the upstream project's hosting, release process, credentials, or services. Publishing, uploading, or changing external services requires explicit authorization.

## Change workflow

1. Read this file, `README.md`, and any more specific `AGENTS.md` files that apply. Check `git status` before editing so existing user changes are preserved.
2. Trace the affected behavior through the relevant C# scripts, scenes, prefabs, ScriptableObjects, and assets before choosing an implementation.
3. Implement only the requested change. Update project documentation when behavior, setup, or contributor instructions change.
4. Run checks only when the user asks for testing/verification or the task explicitly includes it. Use the narrowest relevant checks documented by this Unity project, and report what ran and any limits. Do not add a test framework or new tests unless requested.
5. For meaningful gameplay or UI changes, provide a short, concrete in-game checklist in chat. Distinguish checks the agent actually performed from playtesting that still needs the user's confirmation. For staged work, wait for feedback on the current stage before starting the next one.

If this repository later adds an OpenSpec workflow or another project-specific process, follow its current instructions when applicable. Do not require artifacts from the previous project when they are absent here.

## File operations: do not delete

**Never delete files or directories.** This includes `git rm`, `rm`, `del`, `Remove-Item`, `git clean`, deleting through a GUI, or any command that destroys a path. Editing lines or content within a file is allowed.

If a file must be removed from active use, ask the user before taking action. If the user wants it retained but out of the working tree, move it into a project `Recycle Bin/` directory; do not copy it and then delete the original. Never force-add Recycle Bin contents.

Before any copy or move, verify that the operation is intentional and that the destination is inside the project or explicitly requested. Do not use destructive cleanup to resolve Unity-generated files or Git changes.

## Quality and reporting

- Keep code readable, consistent with nearby code, and free of unused imports, debug output, and dead code introduced by the change.
- Consider likely regressions in gameplay, save data, scenes/prefabs, input, and supported platform behavior.
- Do not claim a build, automated test, or manual playtest passed unless it was actually performed. State important limitations plainly.
- Before handing off, review the diff for scope, accidental Unity serialization changes, and preservation of unrelated user work.
