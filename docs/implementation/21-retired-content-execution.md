# Spec21 — Physical retirement execution report

2026-11-03. The user approved the consolidated manifest and subsequently confirmed that shared generic parts and non-rejected future species must remain. Tatltuae's OC-only animation is retired. Broad swallowed-Selicia gag sheets must remain intact for visual review.

## Completed scope

- 90 rejected identities removed from the active Race enum, parameters and dedicated graphics implementations. Surviving serialized Race IDs are unchanged. Unknown removed numeric species IDs fail the species policy; this does not promise compatibility with every old experimental checkpoint.
- 808 paths directly archived under `Recycle Bin/Retired Content/`: 192 source/metadata, 596 exclusive artwork/name/other asset/metadata, and 20 former bundle/manifest/metadata paths. Hashes verified; nothing deleted. Replacement bundle payloads occupy eight of the former paths and retain their Unity metadata GUIDs. The Recycle Bin is ignored and outside Assets.
- Generic/unreleased species, generic palettes/building/body parts and shared sheets retained. All 56 explicitly protected assets are byte-for-byte unchanged. Sergal accessory/discard frames, generic slash and Diamond Storm frames extracted losslessly and rebound; original atlas pixel rectangles/pivots/filter settings recorded in Logs/retire21-*extraction/effect*.json. No new artist assets or dependencies introduced.
- Retired exclusive OC equipment catalog, three OC traits, two unique skull types and OC-only sweep actions removed. Monster equipment index holes preserve surviving numeric lookups and are excluded from inventory enumeration. Shared historical palette/sprite names, original author credits and generic traits/effects may still mention historical species; these are not retained identity/spawn implementations.
- Exact modified, added and relocated paths: [execution path record](21-executed-source-paths.md). Archived source notices preserve original attribution and add the actual retirement date. No force-add of the archive.

## Verification actually performed

- Native Unity compile/read-only scene capture: exit 0, no C# errors or logged exceptions. Initial stale-cache missing-file errors in the earlier compile1 run are superseded by the clean-scene run and subsequent runtime/build compilations.
- Existing authorized synthetic campaign/battle Save / Load audit: **34 assertions passed** in Unity's runtime. Logs-only synthetic saves; actual deletion intercepted. No personal saves accessed, no new test framework.
- Replacement empire/merc/monster bundles built and loaded natively; retained loader request names checked against bundle contents, with no archived payload paths. specialmerc has no surviving loader request.
- Final static audit: archive hashes match; all 56 protected art hashes match; no actual serialized references to archived GUIDs; no surviving Race ID changes; dated source notices present. Replacement bundle .meta files declaring their own retained GUIDs are not missing-asset references.
- Fresh Windows player build: exit 0, no logged C# errors/exceptions. Seven license/change/dependency notice files packaged. Previous accepted builds are untouched.
- No graphical manual playtest is claimed. See combined review below.

## Combined player review

1. Run the new player build, create a company, inspect all three curated species in leader/recruitment previews and play a short contract battle. Confirm normal rendering, attacks and return to company/map without new errors or missing artwork.
2. Open Single Battle, select the curated species on both sides and enter battle. Confirm previews and generic melee/projectile effects render normally.
3. Inspect [the swallowed-Selicia artwork index](21-selicia-artwork-review.md). Dedicated archived candidates are separate from the unchanged broad-sheet frame list. No trimming of shared sheets has occurred or is requested by this execution.

## License and distribution limits

Actual License.txt sections 4–5 reviewed; dated per-file conversion notices, exact CHANGES path record and inherited attribution preserved. Extracted original pixels remain subject to original artwork terms; reuse is not new proof of redistribution permission. Existing asset-provenance and source/binary distribution gates in the release-readiness record remain open. This private review build is not a declaration of release readiness or complete GPL compliance.

Private executable: `Builds/Warband-Review-20261103-4ea43e6b/Win64/VoreWar.exe`.

## Subsequent investigation correction — 2026-11-03

The synthetic save assertions and player build passed, but the runtime log contains caught tagged-trait metadata import errors missed in the earlier clean-log summary. Three retired OC trait names remain in the shipped category file, aborting its import and causing the Untagged Traits warning. This subsystem was not covered by the save assertions. See [the Unit Tags investigation](22-unit-tags-investigation.md); the defect remains unfixed at this investigation checkpoint.

The subsequent Spec22 correction resolves the documented trait metadata defect; see [the correction report](22-unit-tags-correction.md) for the new clean runtime/build evidence.

