# Spec21 — Consolidated manifest for approval

2026-11-03. This is a proposed implementation/move batch, not a report of completed removal. No source, artwork, Unity settings or assets have been edited/moved. The user approved the plan and clarified generic/shared retention; final physical relocation approval remains required.

## Exact path list and disposition counts

`21-retired-content-move-manifest.csv` records each existing source path, proposed project-root Recycle Bin destination, metadata companion, size, hash and dependency evidence. There are 894 rows including metadata. Current dispositions:

- PROPOSED_ARCHIVE_AFTER_REFERENCE_CLEANUP: 386 rows.
- RETAIN_GENERIC_PALETTE: 64 rows.
- PROPOSED_ARCHIVE_AFTER_CODE_CLEANUP: 10 rows.
- PROPOSED_MOVE: 178 rows.
- RETAIN_GENERIC_BUILDING: 2 rows.
- RETAIN_SHARED_PENDING_FRAME_REVIEW: 44 rows.
- PRESERVE_PARTS_HOLD_ORIGINAL: 2 rows.
- RETAIN_GENERIC_BODY_PARTS: 2 rows.
- PROPOSED_ARCHIVE_AFTER_SEED_REVIEW: 206 rows.

Archive statuses describe prerequisites, not separate tiny approval packets. Execute the approved closure batch in dependency order, with internal compile/source checks, and request one combined player review afterward. Do not move an asset if its reference clearance fails; reclassify it as retained/shared or stop that item for review rather than breaking a future species.

## Confirmed retention

Preserve all non-rejected future species, generic palettes/building/body-part artwork and shared sheets used by them or curated species. Preserve the Asura-derived accessory/discard graphics used by Sergal and the shared accessory system: runtime indices39/40 must be verified against actual bundle subasset names/GUID/local IDs, then extracted/repointed so the rest of the OC atlas can be archived. Extraction is limited to these reusable parts, not the entire character portrait/body. Keep artist/legal attribution with the retained derived asset. No image extraction has happened yet.

Mixed Avian, Cockatrice, Human and other retained sheets are not whole-file archive targets. Selicia-specific branches exist in retained renderers; 21-selicia-shared-frame-review.json records 598 conditions across63 files for source cleanup and frame reuse review. A Selicia condition does not prove that a generic shape frame depicts Selicia. Preserve generic geometry/parts; isolate any actual OC depiction identified during visual review. Do not discard whole Avian sheets or create replacement generic art merely because retired code borrowed them.

## Confirmed retirement scope

Tatltuae's exclusive animation is now removed from the preservation exception. Archive its OC sheet and EntropicChaos source/meta after removing the Tatltuae-only attack caller, effect helper, loader and sprite catalog entries. Do not add its effect or attack to generic Avians. Selicia-specific sheets/alternate depictions and other rejected OC/species-exclusive art remain archive targets after references are cleared. SlimeQueen is explicitly included as a disconnected variant of rejected Slimes; it has only a comment stub plus obsolete loader/dictionary fields.

FairyUtil's only external source call is inside Unit's race==Race.Fairies constructor branch. Therefore remove that retired-only branch and archive Fairy.cs; the earlier suggestion to relocate a generic helper is no longer necessary. Crux-specific clothing and concatenated Crux body/gear/misc sheets likewise require retiring their exclusive loader/type wiring before archival.

## Source and serialized cleanup accompanying the moves

Use 21-retired-content-code-references.json and a fresh scan at execution to remove rejected identity enum names/special cases, registry/presentation/editor entries, generation/name defaults, sprite/palette/effect loader bindings and serialized references. These documents are search evidence, not permission to remove unrelated contents of a matching file. Preserve surviving Race integer IDs explicitly without compacting gaps. Check saved curated-company data and default settings with synthetic data, not personal saves; if a real format incompatibility becomes necessary, present it before proceeding. Preserve copyright/artist/legal notices even where an author's name matches an OC identifier.

Move approved exclusive files directly with .meta companions to `Recycle Bin/Retired Content/Assets/...`, after validating absolute paths and refusing collisions. Keep source hierarchy and hashes. No directory or file deletion, no copy-then-delete and no force-adding the archive. No whole-folder move until all contents and directory metadata are explicitly enumerated.

## Bundles and completion criteria

Rebuild the four mixed prebuilt content bundles from remaining inputs. Their exact manifest source inputs are recorded in 21-retired-content-bundle-audit.json. After successful replacement validation, archive the superseded binary/manifests with exact destinations/hashes added to the execution record; never move a mixed bundle without a working retained replacement. Existing historical private builds remain untouched. No stale OC sheets may remain merely because a loader no longer requests them.

A successful closure requires: current source/serialized reference clearance, retained/future-species shared dependencies preserved, new bundles/player asset inventory free of retired-exclusive content, compiler/scene checks, dated per-file notices and CHANGES coverage, no other source/art/settings changes, and one combined manual review. The manifest remains conditional on these technical checks; approval is not permission to ignore failed checks or invent replacement artwork. Runtime subasset order, pixels inside mixed sheets, arbitrary reflection and complete binary payload absence have not yet been independently proved by this static audit.

## Approval requested

Approve this consolidated source cleanup, limited reusable-part separation, bundle replacement and exact proposed archive paths as a single implementation batch, with the retention rules and stop conditions above. Until approved, no source edits or physical moves will occur. This file supersedes earlier blanket HOLD counts and the earlier recommendation to remove the Sergal accessory. The Tatltuae-removal decision supersedes its temporary preservation exception.
