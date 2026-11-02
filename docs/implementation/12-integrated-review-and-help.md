# 12 - Integrated campaign review and help

**2026-11-02. Combined player review accepted; help rewrite deferred.** The user authorized this phase after accepting the spec 11 feedback, including XP, departure notices and final injury text. Those accepted cosmetic changes are preserved. A private Win64 player build and headless startup check also passed; this is not a release or distribution approval.

## Corrections and help

| Source | Implemented work |
| --- | --- |
| Utility/Texts/HelpText.cs | Replaces conquest, village income, diplomacy, garrisons and training instructions with the current company loop. Covers travel/days, wages/debt, equipment, combat/downed/fates, injuries, progression, saving/Ironman and project/license information. Corrects an invalid Race-array cast while enumerating Traits; retired traits/spells are filtered. |
| UI/HelpPanel.cs | Renames old topics, adds saving/license chapters and generates trait/spell reference text when selected rather than while constructing all buttons. |
| Subwindows/CompanyCampaignView.cs | Adds Campaign help and a Retry checkpoint control at the root, reachable even during pending results, revival or daily decisions. Retry is available only while a save is actually failed and is refreshed when pages/load state change. Uses the shared prospective-resume validation. |
| Subwindows/CompanyMissionsUI.cs | Twelve paged company-help chapters; Return restores setup, the existing leader draft, a pending decision or the current result/map route. Help does not advance time or choose outcomes. |
| Subwindows/CompanyManagementUI.cs | Separate OK-only contract-expiry notice instead of wage payment controls. Exposes safe return to an existing creator draft. Existing wage/departure UI is retained. |
| Core/CompanyCampaignSave.cs | Centralizes ownership/dimension/live-battle checks and latest-valid-only Ironman resume. Extends preservation checks to mana, level/XP, raw stats, equipped instance IDs, uses and cooldowns alongside HP/injuries/fates. |
| Core/CompanyInjuries.cs | Adds the original Army.Refresh daily mana rate of 60% maximum mana to the single daily recovery stage for members and surviving active contract targets. |
| Entities/Units/Actor_Unit.cs | A summon entering the downed path through fear or nonlethal damage now reaches0HP without persistent Downed. Ordinary characters remain alive and latched out of action. |
| Core/State.cs | Replaces obsolete company-not-available/load messages with current checkpoint guidance. Unsupported legacy world saves remain rejected without deserialization. |

Nine existing C# sources changed; no new source, metadata, scene, prefab, art, package, dependency or test-framework additions. No files deleted/moved, commit, upload or publication. Exact paths are dated in CHANGES.md and task-start snapshots/diffs are in Logs/mvp-review-*. Existing legal/modification notices and file encoding are retained.

The save-retry fix addresses a demonstrated navigation trap: a failed write during a pending summary prevented mutation/Continue, while the old retry was accessible only on the map, whose route immediately reopened that summary. The root control removes that trap without choosing an outcome or discarding the in-memory state. Its visibility resets after another valid company is loaded, so it cannot linger as an extra Ironman manual-save control.

Company mana recovery was absent after the empire lifecycle retired; original HEAD Army.Refresh explicitly called RestoreManaPct(.6f). The same rate now follows the resumable daily clock, not menus, load or battle return. No new stamina recovery rule, HP/injury probability, XP, wage or contract tuning was introduced. Summon cleanup uses the existing death/disappearance cleanup; it does not revive or permanently kill ordinary mercenaries.

## Integration evidence

Unity 2018.3.12f1 compilation plus read-only base scene/wiring checks passed at initial, summon-fix and final handoff stages. The final help/scene log is `Logs/mvp-review-final-help-and-scene.log`: return code 0, no C# errors/exceptions, no new normalized warnings against the preceding actual compilation baseline. Inspected scene references remain present and 72 terrain sprites remain wired. The current Windows player also built successfully through the previously verified MultiBuild.Builder.BuildCommandLine procedure (False, Win64), using a new output folder and the existing enabled base scene. A headless run initialized the engine/assemblies and project-local startup data, remained alive for about 54 seconds with no logged exceptions/errors, then was stopped by the agent. This is not a graphical menu/gameplay pass. Build evidence: Logs/mvp-review-windows-build.log and Logs/mvp-review-build-result.json. Local executable: `Builds/MVP-Review-20261102-e350dc13/Win64/VoreWar.exe`. Existing GPL/change and inspected third-party notices accompany the local review artifact; full source/distribution/provenance gates remain open.

`Logs/mvp-review-managed-diagnostics.json` records 32 passed synthetic managed checks against the final compiled production assembly and installed Odin under .NETFramework:

- Normal history can resume either checkpoint; newer saves leave earlier bytes intact. Folder ownership mismatch, invalid-only directories and invalid candidate loads are rejected without mutating an existing company object.
- Ironman refuses an older valid entry, accepts the latest valid entry, and identifies/skips a corrupt newest file while recovering within that same company's history.
- Equipped item aliases, raw item stat bonuses, HP, mana, XP, injury/treatment, uses and cooldowns survive actual immutable checkpoint file round trips. Deliberately changed mana, XP and cooldown values are rejected by the production preservation guard.
- Saving/reloading a single resting day interrupted first by payday and then by contract expiry resumes each stage once. No duplicate wage charge, recovery, sparring or extra day; the wage grace day remains available. Injured reserves still owe wages. Ordinary days after paused rest use ordinary HP recovery; mana caps normally.
- Repeated completion cannot duplicate contract gold/reputation/counts. Saved summary to revival and saved low-gold revival preserve reserves and consume no day/pay interval; revival cannot charge twice after its phase returns.
- Disabling all species removes new company/skirmish eligibility. An existing disabled-species former member can be rehired with the same identity, HP, level, injuries and gear after settling debt, and remains intact through save/load. Preferences were changed in memory only and restored; personal preferences were not written.
- Twelve help chapters pair nonempty titles/text, trait reference generation uses the proper enum and excludes retired traits, and saving guidance describes actual deployment/latest-valid boundaries rather than conquest.
- The actual Actor_Unit downed setter dismisses summons and keeps an ordinary above-zero-HP character alive/downed even if a reset is attempted.

These are synthetic data/method checks, not a running tactical simulation, UI click-through, generated-roster calibration or a long campaign. Fixtures initialize nonserialized derived caches after loading; they do not replace project behavior. All generated valid/corrupt probe files are intentionally retained only under `Logs/mvp-review-checkpoints`, not personal save locations. An initial fixture expected mana from the wrong stat index; using the fixture's actual maximum corrected that expectation, with no production workaround. No new project tests/framework were added.

`Logs/mvp-review-source-review.json` records final hashes, dated notice/CHANGES coverage, preserved earlier notices/BOM/newlines, warning comparison and unchanged resource/scene/license evidence. The previously accepted UnitSprite injury rendering is unchanged.

## Performed and pending coverage

| Area | Current evidence | Remaining player/build coverage |
| --- | --- | --- |
| Creation, travel, recruitment/gear, contracts, daily loop | Prior accepted workflows; current source review and managed integration | One connected current campaign after these integration corrections |
| Normal/Ironman history and corruption fallback | Production file/data checks; shared resume policy | Actual checkpoint browser/button behavior in Ironman and normal mode |
| Paydays, expiry, rest, resources and former-member identity | Production clock/settlement/file checks | Notice layout, help navigation, and visible mana/recovery behavior |
| Rewards, summary and paid leader revival | Production pure settlement/file checks; earlier user feedback | Current UI/engine transition with no duplicate rewards or extra campaign day |
| Downed/temporary HP/digestion and reserves | Source review, earlier accepted HP feedback, direct setter checks | Manual versus instant simulation, actual containment and continuing battle with downed members |
| Retreat with captives and stalemate | Retained guards and200-turn/all-inactive completion reviewed | Live release/retreat behavior and actual draw completion; do not infer a simulation pass from source |
| Summons, copies and nine-slot trait refresh | Setter/gear persistence checks and retained boundaries reviewed | Actual summon/copy combat, cooldown/trait refresh and ordinary skirmish isolation |
| Settings/curated species | New eligibility and existing disabled-character persistence checked | Current settings across company/skirmish and owner-only Race Editor behavior |
| Help and retry failure UI | Data/source paths and compile/scene checks | Actual help/creator Return layout and Retry visibility on an actual write failure |
| Player build and provenance | Win64 build and headless startup passed; notice inventory | Graphical player gameplay, target data packaging review and rights/notice review |

Deferred tactical cases are not silently marked passed. Death/fate and mode-isolation regressions are release blockers if observed. A failed-save UI check should be exercised only if a real write failure occurs; do not damage personal files to provoke it.

## Provisional balance review

The review retains the current tuning:50 starting gold; four initial candidate offers from approved species; ordinary base wages18-22 plus 6 per level; weekly advance payments;4 local contracts,7-day refresh and30-day deadlines; gold 60 times rating with a modest rounded margin; fixed enemy budgets 45 + 30 times rating within 8%; company HP recovery 3% ordinary / 10% camp / 15% town; treatment 10 gold reducing eligible injury recovery by 2 days; healthy sparring 2 XP/day; geometric company thresholds starting 20, 56, 121, 238, 448, 826; revival 20 gold per leader level. The leader remains free and starter gear basic. The only restored resource value is inherited daily mana60%.

At nominal18-22wages, two starter hires cost36-44of 50 gold at the start, leaving 6-14 for basic choices. This is a static affordability check, not proof of every generated pool, contract composition or travel/rest economy. The six-town finite region and action costs affect income per week. Easy-contract income, long-rest XP, late-game power composition, high rosters, performance and32 MiBsave growth still need live calibration. No unrequested wage/reward/XP rebalance was imposed.

## One combined player review

Use the current editor or the private Windows build above.

1. **Help and a normal company:** open Campaign help from setup and during leader creation, then Return. The existing draft should be kept. In a new or healthy current company, hire/equip starters, accept nearby work, inspect final deployment and finish a contract. Check current help topics, including saving, and the ordinary menu's Traits/Spells pages when convenient.
2. **Several campaign days:** keep a member in reserve, use ordinary days and rest/treatment after combat, and check a mage has recovered mana on entering the next battle after days pass. Cross payday and a contract deadline; wage decisions and contract-expiry OK should be separate. Save/reload a pending decision or summary and continue; days, charges, recovery and rewards should not repeat. No repeat injury-font tuning or forced grinding for every injury category.
3. **Ironman boundary:** create a short Ironman company, confirm selection stays fixed and history does not offer old-entry resumption. Start a battle and use the supported return/interruption path; it should resume deployment. Finish it and reload the saved result/return: no duplicate payment. Older valid recovery copies are intentional, not selectable history for Ironman.
4. **Battle/mode integration:** use manual and instant-simulation combat across the run, then open/finish a Pure Tactical skirmish and return to the company. The company should be unchanged by that skirmish. Report any observed summon, downed/captive retreat, draw or leader-revival issue. Live engine checks for those specialist cases remain pending unless actually exercised; do not run 200 turns solely to satisfy a cosmetic check.

Review this as one milestone. After feedback, fix demonstrated regressions together rather than restarting accepted per-control menu checks. Do not start random road events/species expansion or publish a build from this handoff.

## Release status

See [release readiness](../release-readiness.md). The proof of concept is prepared for integrated player review; graphical player/battle and inherited provenance/distribution gates remain open. The private Win64 build is available for that review. This report is not a finished balance assessment or GPL distribution certification.


## User acceptance and deferred help (2026-11-02)

The user reports all review results good and no material issues. This accepts the combined milestone; it does not establish which previously pending specialist cases they exercised, or authorize release. Do not repeat this checklist unless a new regression warrants it.

The current help is too verbose and includes design information intended for development rather than players. Defer the rewrite until the user is ready to review it; aim for concise instructions explaining what players need to play. No immediate help edits are authorized by this feedback.

Payday visibility feedback adds a non-interrupting world-map label above active contracts. It derives days remaining from saved NextPayday minus ElapsedDays, starting at 7, showing singular day at 1, and resetting to 7 after the weekly wage stage. Due but unprocessed wages read Payday due. Rest and reload use the same existing state; no stored field, wage amount/timing or modal added. Preview mode has no countdown. Only CompanyCampaignView.cs changed, with dated notice and root CHANGES entry. Task-start diff reviewed; no new compile/build/playtest. Earlier Win64 review build does not include this label. Next normal editor campaign can confirm the countdown across payday; no separate full milestone retest is requested.


### Payday confirmation (2026-11-02)

The user confirms the countdown looks good and authorizes the next roadmap work. Accept the display; no further focused payday test is needed. This is user feedback, not a new agent compile/build or a change to the older private Win64 artifact. Help rewriting remains deferred.
