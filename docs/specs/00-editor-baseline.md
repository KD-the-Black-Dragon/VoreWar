# 00 — Editor and original-game baseline

**Status:** Partially verified following roadmap approval: import/compilation and Windows build passed; main-menu startup confirmed by the user. Broader gameplay reference remains pending, with automated visual capture unavailable. See [the baseline report](../unity-baseline-report.md). This spec is not complete.

**Phase:** Phase 0 — establish a reference

**Prerequisites:** None.

## Purpose and scope

- Use the installed Unity 2018.3.12f1 editor; record its executable, starting Git revision, platform, packages, and any existing import, compile, startup, or licensing failures.
- Inspect the project's build settings and installed platform support before choosing a reproducible local build command. Do not introduce a new test framework, package upgrade, or engine upgrade.
- Establish an original-game reference for main menu, a strategic turn, battle entry, ordinary combat, vore/digestion, retreat, battle completion, Pure Tactical, settings persistence, and save/load. Use neutral observations rather than copying legacy narrative prose.
- Record which checks the agent performed and which require the user's playtesting. Preserve unrelated local changes and inspect Unity's serialization changes.
- Record a recoverable Git reference for the starting state using the existing repository workflow; do not automatically commit unrelated work.

## Completion and review criteria

- A baseline report identifies the exact editor and the observed compile, launch, and build results. Installation alone is not reported as a successful compile.
- Existing defects and unavailable checks are explicit, with enough detail to distinguish them from later regressions.
- The report describes how to repeat the narrow editor/build checks without guessing at project-specific tooling.

## Decisions, constraints, and risks

- Editor installation, import/compilation, and Windows build are verified; the user confirmed main-menu startup. Remaining gameplay checks are recorded separately in the baseline report.
- Do not fix unrelated pre-existing failures silently. Propose any prerequisite repair as a bounded follow-up.

## Deliverable

A short environment/baseline report, reference revision, and repeatable check instructions.

## Execution record

- Source trace: editor/build settings, package manifest, and existing MultiBuild command-line entry point inspected.
- Baseline revision and starting worktree changes recorded; no new Git commit or tag created.
- Installed-editor check, import/compilation, and Windows player build passed. The user confirmed the built game reached its main menu; the agent located the correct running build, but capture still timed out and accessibility exposed only its window frame. Broader gameplay checks remain pending.
- Unity's two incidental tracked metadata deletions were restored; no gameplay implementation or asset changes are included.

Use these baseline findings to inform the architecture study. Follow the review and file-operation rules in [the roadmap](../conversion-roadmap.md). Design intent comes from [the campaign plan](../mercenary-company-campaign-plan.md) and the three curation checklists.

