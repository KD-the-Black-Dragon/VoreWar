# Spec21 — Ownership audit and proposed move manifest

2026-11-03. Plan approved by the user; dependency audit only. No source/assets moved, edited or deleted. No Unity build/playtest run for this documentation stage.

## Review artifacts

- `21-retired-content-move-manifest.csv`: exact source/destination paths, paired .meta rows, GUIDs, SHA-256 hashes, sizes, known sprite-field consumers and serialized inbound references.
- `21-retired-content-holds.json`: unresolved primary-path dependencies; raw audit evidence (the CSV provides final retain overrides).
- `21-retired-content-bundle-audit.json`: five bundle manifests, including four content bundles and their root manifest.
- Earlier 312 filename candidates expanded using the original species-to-sprite-field inventory and three current/historical loader sources. This is static evidence, not complete binary/visual ownership proof. Unknown filenames, aliases or runtime string lookups can still require further investigation.

## Disposition

- HOLD: 706 rows, including metadata.
- PROPOSED_MOVE: 178 rows, including metadata.
- RETAIN: 6 rows, including metadata.

PROPOSED_MOVE currently covers 89 comment-only retired source stubs and their metadata. Their notices would be preserved in the archive. These are reviewable proposals, not moves already performed. Fairy.cs is excluded because it contains executable helpers. The 90th original retired definition is therefore held. SlimeQueen remains a separately identified dependent variant requiring explicit addition/ownership clearance.

The artwork portion is deliberately not labeled safe merely from its filename. HOLD includes serialized references that require cleanup, shared sprite sheets, aliases and names-only evidence. RETAIN overrides protect Avians3/Avians4 and Panther hats, which rejected species historically borrowed but which belong to retained species. No entire folder moves are proposed; folder metadata remains in place until a folder is proven wholly retired.

## Decisions needed before final artwork clearance

1. **Mixed sheets used by curated species.** `Assets/Sprites/Units/Cockatrice3.png` is still used by Gnolls, Deer and Mice. Avians3/Avians4 are likewise shared but belong to the retained Avian artwork. Human-named sheets supply clothing/body parts to multiple non-rejected species. A filename alone cannot establish that every pixel depicts the retired species. Recommended policy: keep retained/generic parts available, separate them into clearly owned assets where necessary, and archive the retired originals once retained users have been repointed. If the user wants every pixel sourced from these rejected sheets gone, their retained users need replacement artwork instead. No extraction, replacement or loss of appearance is assumed approved.
2. **OC-specific borrowed accessories.** `Asura.png` supplies MainAccessories.AsuraMask and a Sergal discard sprite. Recommended: retire the OC-specific accessory/discard use too, rather than preserving Asura art under a generic label. Tatltuae.png still supplies EntropicChaos animation; this similarly needs removal/replacement of the dependent presentation while preserving any retained gameplay. Confirm this policy before altering retained appearances/effects.

Other holds include AabayxHighPriest clothing, Crux clothing, Shark sheets, Slime tactical effects, TaurHumanHalf and FairyUtil/FairyType. Many callers are in non-rejected species waiting for future curation, so disabled status alone does not make them disposable. Resolve these groups in one source/asset dependency batch, rather than asking for approval on hundreds of individual files.

## Bundle and compatibility gates

All four prebuilt sprite bundles still list retired inputs. Raw PNG moves cannot remove their embedded copies. The bundle audit records exact listed paths; replacement bundles must be rebuilt and inspected before superseded binaries/manifests are archived. Historical private builds remain untouched. The static audit does not claim their embedded payloads are clean.

Surviving Race numeric values must remain explicit and unchanged. Removal of old enum names/custom-setting compatibility is an implementation decision to review before editing; current curated saves must be protected where feasible. No personal saves/settings were opened during this audit.

## Next action

Obtain the two policy decisions above, resolve shared/uncertain ownership, then present the complete cleared artwork/source/bundle manifest for final move approval. Do not move only the stub batch as a substitute for the requested full removal. User approval of the plan did not approve physical relocation or permit deletion. No files are moved at this handoff.


## User scope clarification — 2026-11-03 (supersedes earlier recommendations)

Preserve generic/shared assets and non-rejected unreleased species for future curation. A retired consumer does not establish exclusive ownership or justify archiving a shared sheet. Remove permanently rejected character/species-specific depictions and dead exclusive code; Selicia is an explicit OC-removal example. Preserve the Sergal accessory and reusable Tatltuae animation, subject to separating their required frames from unrelated OC portraits/body artwork. Do not remove these borrowed presentations as previously recommended.

Current wiring was inspected: Sergal's BaseOutfit uses Asura frame39 as a discard sprite; MainAccessories.AsuraMask uses frame40 and CompleteSprite can display it via EarnedMask. EntropicChaos uses Tatltuae frames130–143. Its current attack caller is explicitly gated by Unit.Race == Race.Tatltuae in Actor_Unit; it is NOT currently a generic Avian animation. Preserve the reusable effect as requested, but do not silently add an Avian attack ability/animation to make it live. Visual frame ownership still requires review before extraction; retaining the effect is not approval to retain the entire OC sheet.

Manifest status PRESERVE_PARTS_HOLD_ORIGINAL marks Asura/Tatltuae source atlases and metadata. Generic mixed sheets remain held/protected until relevant ownership is established; filenames alone cannot select removal. No source/assets edited or moved. Next: frame-level separation/extraction manifest, retained generic ownership audit, and final physical move approval. Existing license/artist attribution follows retained derived assets; no claim about open-species status or asset redistribution rights is inferred from popularity or technical reuse.


## Latest decision and audit continuation — 2026-11-03

The user now explicitly retires Tatltuae's OC-only attack animation. This supersedes the earlier effect-preservation instruction. Proposed archival includes EntropicChaos.cs/.meta and Tatltuae.png/.meta after removing the Actor_Unit Tatltuae-only ranged-attack gate, TacticalGraphicalEffects.EntropicChaosEffect helper and obsolete sprite loader/dictionary bindings. No generic Avian gameplay or attack effect is added. Sergal accessory/discard parts from Asura indices39/40 remain protected, with runtime index-to-sprite mapping/visual ownership to be verified before separation.

The expanded audit found Selicia-specific rendering conditions inside retained graphics code, including Avians and Sergal. Exact conditions, source line numbers and direct sprite expressions are recorded in 21-selicia-shared-frame-review.json. These branches must be retired without discarding the whole retained sheets. Their conditions alone do not prove that each referenced frame depicts an OC; generic shape frames also require reuse checks before archival. Shared assets and generic color palettes/building art are now explicitly protected in the manifest. Unreleased non-rejected species remain protected.

The move manifest is versioned by status rather than pretending all rows are cleared. PROPOSED_MOVE is comment-only source stubs; other proposed groups explicitly require code/seed cleanup. HOLD, PRESERVE_PARTS and RETAIN statuses are not approved move operations. The summary JSON records current counts; earlier reports' totals are historical. There is no final complete artwork clearance yet. Next: finish the shared-frame/alias/seed and GUID/bundle-order review, then present the full actionable move manifest for approval. No source edits, image extraction, moves, deletion, Unity build/playtest or personal-save access occurred in this documentation audit.
