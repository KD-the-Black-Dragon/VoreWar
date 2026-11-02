# Release readiness

Updated 2026-11-02 for spec 12. **No release candidate, distribution approval or publication is declared.** The combined company player review and subsequent payday display are user-accepted. See the [implementation and coverage report](implementation/12-integrated-review-and-help.md).

## Player acceptance and follow-up (2026-11-02)

The user reports the combined review good with no material issues. This accepts that milestone, without establishing unreported specialist coverage or authorizing publication. Help needs a later concise player-facing rewrite; it is explicitly deferred. A world-map payday countdown was subsequently added in source only (diff reviewed, no new compile/build/playtest). The user subsequently confirmed the payday label looks good. The private Win64 artifact described below predates that label. The remaining gates still apply.

## Spec13 follow-up (2026-11-02)

Approved travel events and per-level XP display are implemented: [phase13 report](implementation/13-road-events-and-xp.md). Unity compile/read-only scene and existing32managed regression checks passed; the user subsequently reported the slice basically good and accepted it with new slug encounter budgets reduced30%. That constant-only follow-up was source-reviewed, without an additional compile/playtest as recorded in the phase report. The older Win64 artifact predates both payday and these events; no current player rebuild is claimed. No new dependencies/assets or release approval. The full help rewrite remains deferred.

## Spec14 interface follow-up (2026-11-02)

The user-defined campaign layout is implemented: [interface report](implementation/14-campaign-interface.md). Final editor compile/read-only scene and existing32managed regression checks passed; actual runtime layout/navigation and the new rest eligibility await one combined UI review. No current Windows player rebuild or multi-resolution visual pass. Help prose rewriting remains deferred; no distribution approval.

## Spec14 feedback revision (2026-11-02)

[14B](implementation/14b-centered-map-and-options.md) revises the unaccepted initial layout: bounded centered frame, top Contracts/Records, lower contract strip, normal Options and a pooled draggable map. User-specified new regions are80x60with six towns. Final compile/scene, existing32managed checks and actual map generation/save/resume inspection passed. The user subsequently accepted the functional layout with no material placement critique; this does not identify every individual drag/Options/aspect case they exercised. A common design for campaign/Options/content/tactical UI is now the planned precursor to deferred beautification. No new player build or publication approval.

## Options-first shared UI (2026-11-02)

[15A](implementation/15a-options-redesign.md) introduces a restrained blue, compact categorized normal Options presentation. Compile/read-only scene and existing32managed regression checks passed; the user subsequently found the layout mostly okay. Slider alignment, larger hover text and vore terminology corrections passed a later compile/scene check, with focused visual confirmation pending. No unreported preference/visual case is inferred as passed. Existing preference storage and maintenance callback bodies remain. Other screens and Help prose remain later work. No current player build or distribution approval.

## Evidence in this checkout

- Engine: Unity 2018.3.12f1 (`ProjectSettings/ProjectVersion.txt`). Current build settings enable `Assets/base.unity`. Packages and build configuration were not upgraded.
- Current editor compilation and read-only scene checks pass. Synthetic production-code integration checks cover company data/settlement/save behavior. Prior player confirmations cover the recorded staged workflows and spec 11 feedback. A current Win64 player build also passed the existing MultiBuild procedure, followed by a headless startup check. These do not prove graphical player gameplay or every tactical case.
- Root `License.txt`, original attribution, dependency notices and dated source modification notices are retained; copies of the root license/change record and inspected dependency notices also accompany the private Win64 review build; `CHANGES.md` identifies exact completed fork changes. No external code/assets/dependencies were introduced in spec 12.
- Current UI help explains the company loop, supported checkpoints, Ironman limits and project/license information. It does not replace delivering full license texts and other required notices with a future distribution.

## Known functional boundaries

- New company saves only; no legacy empire-save import. Early checkpoints affected by the earlier omitted-HP defect are not repaired, per user direction. Use fresh or confirmed healthy company state for review.
- Company tactical saving resumes the prebattle deployment checkpoint, not a live tactical turn. Results and revival transitions save separately. Ironman shares this boundary and resumes only the latest valid generation through the interface.
- Normal history and corruption recovery retain immutable checkpoint files under the no-delete workflow. Storage rotation/pruning is not implemented. A32 MiBpayload guard is explicit; long-run growth and high-roster behavior are not calibrated.
- Active/reserve deployment has no new fixed cap. The intended company is usually small; large forces/performance require live review.
- Two proof-of-concept travel events are user-accepted with lighter slug encounter tuning. Further events, extra points of interest, additional species and user-authored narrative remain later work. Only the currently curated species generate. Existing terrain artwork is retained.
- Numeric balance remains provisional. Specialist live combat and visual failure-recovery cases are listed as pending in the integration report, not passed by source inspection.

## Gates before any release

1. The combined player review is accepted. Resolve any newly demonstrated gameplay/save/UI regressions. Exercise the deferred fatal-fate, captive/retreat/draw, summon/copy, manual/turbo and skirmish-isolation paths before treating the combat integration as release-tested.
2. The Win64 build and headless startup check passed in this phase using the recorded Unity version and existing build procedure. Complete graphical startup/gameplay and packaged-data checks before release, including assets, fonts, plugins, platform-specific bundles and required data. Other target platforms are untested; this check does not authorize a public package.
3. Resolve the inherited asset/dependency provenance issues below and decide what is actually being conveyed. No file is assumed freely redistributable merely because Unity imported it.
4. Recheck the applicable actual GPL terms for that distribution. Preserve license/copyright/attribution/additional-term/warranty notices and prominent modification notices/dates. Review interactive Appropriate Legal Notices under 5(d), including its stated exception; do not assume a help paragraph alone settles it. For binaries, select and satisfy an applicable section 6 Corresponding Source method, required build/support material and Installation Information where applicable. No distribution method is selected here. The conversion currently includes uncommitted changes and untracked source additions: a future source delivery must contain the exact working tree used for the binary, not just a link to an older remote revision. No automatic commit or push was made.
5. Obtain the user's explicit authorization for publication/upload/distribution. No credential, remote, hosting or release service was changed by spec 12.

## Inherited notice inventory and unresolved provenance

| Material | Evidence inspected | Status before packaging |
| --- | --- | --- |
| Covered VoreWar fork source | Root License.txt (GNU GPLv3); README project attribution; CHANGES; per-file modification notices | Preserved. Distribution-specific obligations remain to be checked for the actual package/method. |
| Odin Serializer binaries/source context | `Assets/Plugins/Sirenix/Odin Serializer/License.txt` identifies Apache License 2.0; plugin folder includes JIT/AOT/EditorOnly variants | Notice retained, no plugin changes. Determine shipped variants and preserve applicable notices/terms in the chosen package. |
| Newtonsoft.Json 13.0.3 | `Assets/Plugins/Newtonsoft.Json.13.0.3/LICENSE.md` identifies MIT and James Newton-King attribution | Retained, no dependency changes; include relevant notice if conveyed. |
| Packaged Unity/GitHub tools | `Assets/package/LICENSE.md`, `Assets/package/Third Party Notices.md`, `Assets/package/com.unity.editor.tasks/LICENSE.md` | Existing notices retained. Determine whether these editor/tools components are conveyed; review all applicable third-party notices for that package. |
| Unity packages/platform runtime | Versions in `Packages/manifest.json`; installed engine/package contents | Not upgraded or newly introduced. Actual player/binary package terms and included notices need target-specific review. |
| Artwork, sprite sheets and bundles | Existing resource inventories/retirement ownership records; retained hashes and source graphics attribution | Asset-by-asset rights/notice mapping is incomplete. Source inclusion and a passing editor do not establish the right to redistribute every sheet/bundle. Resolve contributor provenance and any separate terms before release. |
| User-supplied Vore Warband menu logo | Assets/Resources/UI/VoreWarbandLogo.png, supplied by the user on2026-11-02; original PNG copied unchanged,2172×724RGBA, SHA256 a8fbea4f27b0e83912bab56aa34dcc4497e74607ad0d4b8433adde68ae91e3f9; no embedded author/license metadata | Integration authorized by the user. Creator/source, applicable redistribution terms and any required attribution remain unverified before packaging/publication. No GPL grant or copyright ownership inferred. |
| Existing TMP/font assets | Current text materials/font references preserved; installed TMP package inspected for API compatibility | No standalone font-license notice was found by the filename inventory in the inspected TMP asset folder. Verify the font origin and required notice supplied with the selected packaged font; absence from that inventory is not a conclusion that it is unlicensed. |

This is a concrete readiness record, not a claim of complete GPL compliance or a new license grant for separately licensed material. Inherited provenance remains unresolved; the newly supplied menu logo also requires its own provenance/terms record before distribution. No license grant is invented by its inclusion.

## Current local player artifact

`Builds/MVP-Review-20261102-e350dc13/Win64/VoreWar.exe` was built without overwriting earlier builds. `Logs/mvp-review-build-invocation.json` records the exact existing MultiBuild method/arguments; `Logs/mvp-review-build-result.json` records the executable hash, copied legal notices and headless startup result. Project settings remain identical to their pre-build snapshot. The hidden player test was stopped after checking its log; no visual capture or gameplay was attempted. Public source delivery, complete provenance and other distribution obligations remain unselected/unresolved, so the notice-bearing private build is not a distribution certification.
