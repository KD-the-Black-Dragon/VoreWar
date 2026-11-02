#11 feedback — Progression, injury announcements and departures

2026-11-01. Implements the user's three feedback items; the reported feedback corrections are accepted as of2026-11-02. Provisional tuning remains authorized. This is a correction to11, not the start of12.

## Company progression

The legacy static XP calculation returns99999999 whenever current level reaches HardLevelCap. The retired WorldConfig default is0, so company characters inherited a reached-cap sentinel. Company instances now use CompanyProgression.Threshold instead of those old world-cap settings. The separate Pure Tactical configuration is unchanged.

Without trait modifiers, cumulative thresholds to reach levels2–7 are20,56,121,238,448,826. The formula is the rounded-up geometric sum20 × (1.8^completedLevels −1) /0.8, with overflow protection. Existing trait XP-requirement modifiers still apply. The player still chooses existing level-up improvements; eligibility does not silently choose stats.

On actual contract completion, surviving deployed members gain20 × rating base XP. Each level above the rating halves that award; below-rating members can receive a capped harder-challenge bonus. Every surviving deployed level1 character is made eligible for level2 on completing their first contract even with slower-learning traits. Reserves do not get this participation reward. They retain daily healthy sparring. Retreating without completing the target does not receive this contract award. The award is inside the existing detached, once-only settlement and guarded by CompanyContracts.Complete returning true; opening a results screen or loading it does not award again.

Company per-action scaled XP likewise halves per level of advantage rather than retaining the old30% minimum, and calculation previews match awards. Three spell-award sites compared the target's level with itself; they now compare caster and target. Existing trait gain modifiers, absorption sources and small ordinary action/battle bonuses remain. Exponential requirements and challenge scaling make trivial contracts progressively inefficient; these initial values do not constitute completed balance or an absolute prohibition on all possible farming.

Version4 upgrade preserves current levels and ordinary earned XP. Earlier generated veterans with sentinel-valued XP are reset to their current level's starting threshold, avoiding a backlog of accidental free levels. Normal checkpoint loads do not rebase experience again or advance time.

## Injury announcements

CompanyInjuries.Add records the injury, then adds an informational battle-log entry with the unit's name, injury name, grade, penalty and recovery. This also covers injuries in Pure Tactical. UnitSprite queues an orange Injury: <name> message in the existing DamageIndicator after current damage/status text finishes, so the same hit's damage number does not immediately overwrite it. Multiple injuries queue separately. Map messages are restricted to visible actors; contained/hidden actors still get log entries. Instant simulation preserves the log and skips visual notifications. No new artwork, prefab or scene changes.

## Departure notices

An actual departure now has its own title, departure-only wording and a single OK button. It has no debt summary, payment/group controls, View company button or reassurance that the departed members will wait.

Different members can reach different patience thresholds on the same day. If departures coincide with a new genuine warning or unpaid payday, that separate wage message is saved in DeferredWageDecision. OK on the departure notice presents the wage dialog without advancing time or resuming the daily stages. Only acknowledgment of that second dialog resumes the paused update. This retains the promised grace day. If nobody else needs a warning, departure OK simply resumes the day. Legacy pending departure notices are normalized on version4 upgrade. The checkpoint preservation guard covers the queued warning.

## Verification and limits

- Unity2018.3.12f1 compilation and read-only base scene/wiring check passed, return0 with no C# errors/exceptions (`Logs/progression-feedback-compile-and-scene.log`). No new normalized compiler warnings versus the earlier actual HP compilation baseline. No Unity process remains.
-11 synthetic managed production-code checks passed (`Logs/progression-feedback-managed-diagnostics.json`): threshold values, cap independence, weak-enemy action XP and preview agreement, slow-learning starter guarantee, veteran/reserve contract XP, sentinel migration, departure-only wording, a separate simultaneous threat, and an actual immutable checkpoint file preserving that warning without advancing the grace day. The initial synthetic save fixture contained an incomplete battle record; correcting the fixture allowed the production save validation to pass. No project source workaround or new test framework/project test files.
- Floating text, actual battle-log visibility, choice UI and modal appearance still require player confirmation; no Play-mode/visual pass or player build is claimed.
- `Logs/progression-feedback-source-review.json` records all11source notices/CHANGES paths, preserved earlier legal/modification notices and encoding, no source trailing-whitespace/legal-notice removals,1248unchanged retained-resource hashes, unchanged base scene/License.txt and warning comparison. Actual License.txt sections4–5 reviewed; no outside code/assets/dependencies, deletions, moves, commit or publication. Inherited asset/dependency provenance and distribution-method obligations remain release gates.

## Focused player review

1. Complete a starter contract with level1 participants. Check XP requirements are sensible and the surviving starters can choose their level2 improvements from Company → Manage → Level up. Later levels should demand more XP; experienced members should gain little from weak contracts.
2. When an injury occurs naturally, check for a named log entry and orange tactical text after the damage number. No need to force every injury type.
3. Let an unpaid member leave. Their notice should offer only OK and describe the departure. If a different member genuinely begins threatening departure the same day, a separate wage warning may follow; the campaign day must not advance between them.

## 2026-11-02 — Accepted XP/departures; injury readability adjustment

The user confirms XP progression is functional, with future tuning expected, and the Departures screen looks good. Those feedback items are accepted. Injury text was too brief and large.

Only UnitSprite.cs changes in this correction. The earlier shared-label queue described above is superseded by a dedicated runtime injury label using the existing font and layout style. Its font-size ceiling is65% of the ordinary label's ceiling, with twice the text width, positioned above the numbers. It has separate color state: a fresh text component avoids duplicating the mutable font material used by damage text. Each queued injury stays fully readable for2.5real-time seconds using Time.unscaledTime timestamps; repeated sprite updates cannot shorten its timer and later damage cannot overwrite it. Multiple notices receive their own duration. The original damage/healing setup and0.75-second timer are preserved exactly. Battle end or actor removal still removes that actor's overlay; the full injury record remains in the battle log/character state.

No prefab, scene, artwork, injury/XP/economy/save-rule changes or new tests/framework. Source snapshot/diff and final scope/compile evidence are in Logs/injury-readability-*. Visual confirmation remains pending: observe a naturally occurring injury, check the smaller orange label stays for about2.5seconds even while combat numbers change, and confirm ordinary numbers retain their previous speed. XP/departures do not need repeat testing.

Readability correction checks: final Unity2018.3.12f1 compilation/read-only scene inspection passed, return0 with no C# errors/exceptions and no new normalized warnings (`Logs/injury-readability-final-compile-and-scene.log`). `Logs/injury-readability-source-review.json` confirms the ordinary damage setup is identical, the separate2.5-second/65%-ceiling injury settings, dated notice/CHANGES/prior-notice/encoding coverage, unchanged1248retained resources/base scene/license and no removed legal notices or introduced source whitespace. No Play-mode/visual pass, new automated tests or framework. Initial source-audit reading needed explicit UTF-8 decoding; corrected audit passed without a project source workaround. Unity is closed. Inherited provenance/distribution review remains pending before release.

## 2026-11-02 — Timing accepted; smaller text one tile to the right

The user confirms injury timing is good. The auto-sized font was still too large, and the centered rectangle made the text extend to the left. This correction changes only UnitSprite.cs: fixed1.5font size with auto-sizing disabled, explicit left alignment and left-edge pivot, a30×3canvas-unit no-wrap rectangle, and a world position exactly one tile (one world unit) to the unit's right at the same height, preserving canvas draw depth. The unit prefab's text canvas is scaled0.1; the new text is much smaller than the earlier6.5font ceiling. Parent attachment follows the unit. Queuing, steady2.5unscaled seconds, color isolation, ordinary damage/healing text and gameplay are unchanged. No prefab/asset edits or new tests/framework.

Only size/placement needs player confirmation: a naturally occurring injury should show compact left-aligned text beginning one tile to that unit's right. Timing, XP and Departures are accepted and do not need repeated tests. This section supersedes the earlier above-unit/auto-sizing layout.

Placement correction verification: Unity2018.3.12f1 compile/read-only base-scene inspection passed, return0 with no C# errors/exceptions or new normalized warnings (`Logs/injury-placement-compile-and-scene.log`). `Logs/injury-placement-source-review.json` records the fixed font/left pivot/world-tile offset, identical accepted timing and remaining combat display code, one-source notice/CHANGES/prior-notice/encoding review and unchanged1248retained resources/base scene/license. Unity is closed. No Play-mode/visual pass or new tests/framework; the single size/placement review above remains pending. Licensing/attribution remain intact, with inherited provenance/distribution review still required before release.

## 2026-11-02 — Contrast and fine positioning

The user requests moving the text about a quarter tile left, adding a visible black outline and increasing its size by1–2points. Only UnitSprite.cs changes: the text begins0.75tiles right of the unit instead of1tile, and fixed font size rises from1.5to2.5(+1point). Existing black outline color alone was not sufficient, so the label now explicitly sets a minimum0.2outline width and enables the OUTLINE_ON shader feature on its independent material. Installed TextMeshPro1.3.0 and the existing outline font material were inspected; no dependency code or font asset was changed or copied. The2.5-second timer, queue, left alignment and normal damage/healing display are untouched.

Focused visual confirmation: an injury notice should start a quarter tile left of its previous location, remain left-aligned, and show slightly larger orange lettering with a clear black outline. Timing is already accepted. No repeated XP/departure/menu review is needed.

Outline correction checks: Unity2018.3.12f1 compilation/read-only scene inspection passed, return0, no C# errors/exceptions or new normalized warnings (`Logs/injury-outline-compile-and-scene.log`). `Logs/injury-outline-source-review.json` records the one-source diff, notices/CHANGES/encoding preservation, unchanged ordinary combat display and accepted timer, expected font/offset/outline settings, and unchanged1248retained resources/base scene/license. Unity is closed. Actual visual appearance remains for the focused player check; no Play-mode pass or new tests/framework is claimed. Licensing/attribution preserved; inherited provenance and distribution review remain release gates.

## 2026-11-02 — Final display accepted

The user accepted the final injury font, outline and placement. Timing had already been accepted; XP and Departures are also confirmed. All reported feedback items in this report are closed. Earlier pending-review statements above describe historical handoffs, not new review requests. No additional testing or source changes occurred in this acceptance update. Integrated campaign review and balance remain the next stage, spec12.
