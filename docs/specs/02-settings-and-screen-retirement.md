# 02 — Retire unwanted menus and lock balance settings

**Status:** Functional retirement milestone complete, 2026-11-01, with final agent checks passed as recorded in the [closure report](../implementation/phase-2-cleanup-closure.md). Earlier packet records remain historical. Combat/log scenarios await approved species; distribution/shared-asset review remains a release gate.

**Phase:** Phase 2 — retire unwanted systems

**Prerequisites:** 01 reviewed; use the exact lists in options-curation-checklist.md and screen-curation-checklist.md.

## Purpose and scope

### Preparation from study 01A

[The navigation/settings packet](../architecture/01a-navigation-and-settings.md) establishes this provisional implementation order, to be reconciled with the remaining study:

1. Establish retained defaults and configuration precedence independently of widgets; handle PlayerPrefs, saved WorldConfig replacement, and startup/load cheat flags without wiping unrelated preferences.
2. Close retired navigation routes and gate all Race Editor entry contexts. Include enabled prefab overrides and direct GameObject activation; preserve the nested Unit Tag Editor and required trait helpers.
3. Retire content pages while keeping SettingsPanels and PageSelector options aligned. Detach BuildingSettings hard/soft load/save calls and replace references to retired control objects.
4. Integrate the curated-species settings boundary from spec 03, then review old overrides, both mode menus, and retained preferences.

The scene contains three enabled OpenRaceEditor callbacks plus three disabled preset callbacks; presets also register separate slot-switch listeners at runtime. Bestiary entry directly activates a GameObject. Unit Editor is exposed by a preference-controlled clickable unit-information link. These are concrete paths for the removal/gating review, not a complete proof of runtime reachability.

Resolve the appendix's fourteen unmatched control bindings and relevant inherited callback details before editing affected scene references. General Options AutoUseAI changes tactical control; classify that behavior with the user before making a new removal decision. Existing keep/remove decisions still govern.

### Preparation from study 01D

[The campaign/save study](../architecture/01d-campaign-persistence-and-identity.md) confirms that State.Load installs legacy WorldConfig and loads old options/cheats before strategic/tactical routing. The [consolidated order](../architecture/01d-removal-sequence-and-risks.md) recommends 02A first: independent code balance defaults and explicit preference/load precedence, without removing widgets in that same first batch. Cover startup, new game, load, reset and Race Editor slot context; do not wipe user data. Resolve 01A's remaining scene bindings before their affected 02B/02C edits.

### Agreed scope

- Remove player entry points for the agreed empire tools, cheats, editors, Bestiary, tutorial, and obsolete settings pages. Keep ordinary Options and the agreed cosmetic/content preferences.
- Keep balance values adjustable by the developer in code. Remove player controls without accidentally zeroing values or allowing persisted old overrides to bypass the intended rules.
- Preserve Race Editor behind the user's convenience password, including General Options, Trait Options, and nested Unit Tags Options. Remove standalone access to other authoring screens while retaining runtime traits.
- Trace and remove alternate navigation and keyboard paths to retired screens. Keep only internal editor helpers demonstrably required by Race Editor.
- Treat removal of a settings control separately from removal of its mechanic. Implement only confirmed mechanic retirements; flag ambiguous cases.
- Handle the legacy Fog of War page according to the decision checkpoint; the new campaign must enforce exploration fog.

## Completion and review criteria

- Every retained/retired page and control can be accounted for against the source checklist.
- Retired screens cannot be reached through normal navigation or alternate callbacks.
- Old saved balance overrides do not restore player control; defaults remain explicitly recorded and code-tunable.
- The password gate preserves all three Race Editor tabs and returns safely on cancel or incorrect input.

## Decisions, constraints, and risks

- The actual developer password is needed only when implementing the gate; do not print it in reports.
- General Options remain as agreed unless study identifies a concrete incompatibility; bring that finding to the user.
- No new security service or credential infrastructure is needed for this convenience gate.

## Deliverable

Small menu/settings batches with updated control mapping and an in-game review checklist.

## Execution record

- 02A: [implementation/defaults/precedence record](../implementation/02a-settings-defaults-and-precedence.md). Added Core/ContentSettingsData.cs and its Unity metadata; ContentSettings.cs delegates preference loading and keeps widget/key bindings without default ownership. All 105 toggle defaults, 89 scalar assignments, existing preference keys and the legacy spawner subset were preserved.
- Kept WorldConfig raw fallbacks, building data, save-load precedence, UI apply/save behavior, species eligibility and gameplay rules unchanged. Later 02C must actually enforce code balance against old preferences/saves; the presence of the new loader does not claim that locking is already active.
- Source-move review completed. Fresh Unity 2018.3.12f1 compilation passed with no C# errors and successful return code 0; settings/persistence playtesting remains pending. No tests/framework, player build, preference reset or unrelated serialization changes added. Restored the known Unity-removed orphan metadata byte-for-byte after import.
- 02B implemented: eleven C# source files carry the retired-entry/session guards and twelve targeted prefab-instance blocks in Assets/base.unity hide eleven entries and route the warning through the gate. See [the tool/gate record](../implementation/02b-tool-retirement-and-race-editor-gate.md). Unity 2018.3.12f1 import/compile check passed with no C# errors/return code 0; manual password/navigation review remains pending. Known orphan metadata was restored, with no unrelated scene, prefab, package or project-settings edits.
- Subsequent user feedback confirmed the three requested 02B checks. Broader mode/save checks are not inferred from that confirmation.
- 02C implemented: five retained pages, 59 preference bindings, explicit code-owned retired defaults and enforcement at startup/load/import. Earlier unresolved bindings and inherited callbacks are resolved in [the control map](../implementation/02c-settings-control-map.md). Fresh Unity compilation passed; user navigation/persistence review remains pending. The replacement curated-species page/generation boundary follows with 03A; spec 02 is not yet complete. GPL source notices and root CHANGES.md accompany the batch.

Before each remaining batch, refine its concrete source/scene trace and resolve decisions needed for that batch. Follow the review and file-operation rules in [the roadmap](../conversion-roadmap.md). Design intent comes from [the campaign plan](../mercenary-company-campaign-plan.md) and the three curation checklists.



## Consolidated cleanup milestone closure (2026-11-01)

See [phase-2 cleanup closure](../implementation/phase-2-cleanup-closure.md) for current accepted work, exact retained services and deferred combat/release gates. Earlier packet status lines are historical. No further per-packet menu tests are required; final milestone verification status is in that report. New campaign/character mechanics remain subsequent specs.
