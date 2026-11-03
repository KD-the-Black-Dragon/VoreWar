# 21 — Physical retirement of rejected species and named characters

DRAFT FOR USER APPROVAL — 2026-11-03. Planning/inventory only. No game code or assets changed, moved or deleted.

Earlier retirement disabled creation and removed many implementations, but deliberately retained paths, artwork and bundles. The current checkout still contains 90 permanently rejected identities in SpeciesPolicy, named-character source paths, Race enum identifiers, presentation/compatibility references and four prebuilt sprite bundles in StreamingAssets. Disabled generation alone does not meet this new request.

## Scope boundaries

Remove the 90 explicitly rejected identities below from active source and shipped content, plus existing named unique characters covered by the earlier instruction to remove all named uniques. Preserve Avians, Lizards, Gnolls and SpitterSlugs. Other uncurated species are pending future curation, not automatically permanently rejected: do not archive them merely because they are currently disabled. SlimeQueen and any other dependent variant must be explicitly tied to a rejected identity or placed in the review list before moving it. Do not treat similar names as identical species (Raptor versus Utahraptors, for example).

The CSV is a preliminary path inventory, NOT an approved move manifest. Its matches are deliberately labeled candidates: aliases, shared atlases and misleading filenames require ownership verification. The JSON lists remaining explicit Race.X source references and is an audit input, not permission to alter every matching file. Both omit .meta companions from their rows; the final manifest must explicitly include every companion and any directory metadata.

## Removal list

| Rejected identity | Candidate named paths |
|---|---:|
| Aabayx | 6 |
| Abakhanskya | 3 |
| Alraune | 6 |
| Asura | 4 |
| Auri | 4 |
| Bears | 3 |
| Bella | 9 |
| BoomBunnies | 3 |
| Cake | 4 |
| Cats | 1 |
| Centaur | 3 |
| Cherub | 2 |
| Cierihaka | 2 |
| Cockatrice | 5 |
| Collectors | 1 |
| Compy | 4 |
| Crux | 7 |
| Crypters | 2 |
| DRACO | 2 |
| DarkSwallower | 3 |
| DewSprites | 0 |
| Dogs | 1 |
| Driders | 2 |
| EarthDryad | 2 |
| Earthworms | 4 |
| Eevee | 4 |
| Equaleon | 2 |
| Erin | 2 |
| Fairies | 0 |
| Feit | 2 |
| FeralEevee | 2 |
| FeralEqualeon | 1 |
| FeralFrogs | 1 |
| FeralOrcas | 3 |
| FeralSharks | 1 |
| Foxes | 1 |
| FungalDryad | 2 |
| Ghosts | 5 |
| Goblins | 13 |
| Hamsters | 3 |
| Harpies | 2 |
| Humans | 7 |
| Iliijiith | 6 |
| Imps | 14 |
| Ki | 2 |
| Konane | 2 |
| Lamia | 5 |
| MainlandElves | 2 |
| Merfolk | 0 |
| Nectar | 2 |
| Olivia | 3 |
| OoviKat | 6 |
| Puca | 3 |
| Pudding | 5 |
| Raptor | 3 |
| Renamon | 7 |
| RiverDryad | 2 |
| RwuMercenaries | 10 |
| Ryan | 3 |
| Salix | 4 |
| Schiwardez | 4 |
| Scorch | 2 |
| Scylla | 2 |
| Selicia | 3 |
| Seraph | 2 |
| Seville | 3 |
| Singularity | 3 |
| Skapa | 2 |
| Slimes | 2 |
| SoulSprite | 5 |
| Succubi | 1 |
| Taraluxia | 2 |
| Tatltuae | 3 |
| Taurus | 1 |
| Tigers | 1 |
| Umbreon | 6 |
| Vagrants | 5 |
| Viisels | 4 |
| Vipers | 6 |
| ViraeUltimae | 3 |
| Vision | 2 |
| Voilin | 4 |
| Whisp | 4 |
| Wolves | 1 |
| WoodDryad | 2 |
| Xelhilde | 2 |
| Yordles | 9 |
| Youko | 2 |
| Zera | 9 |
| Zoey | 4 |

## Proposed implementation phases, after approval

1. **Resolve ownership and produce a final move manifest.** Trace each identity through sprite source PNGs/sheets, palettes, materials, prefabs, Resources loaders, SpriteDictionary/PaletteDictionary, race/graphics catalogs, character data, names, effects and bundle labels/manifests. Match GUID references, not just filenames. Include OC aliases and sprite-sheet names that do not match the character name. Classify each exact path as exclusive (move), shared (retain and remove only retired entries), or uncertain (hold for user review). Show source/destination, GUID, owner, dependent references and retained users. No files move during this audit. Present uncertain cases and the final manifest before physical relocation.
2. **Remove active code and serialized references in one consolidated batch.** Retire remaining identifiers, special cases, loader bindings, labels, palettes and character-specific data; archive exclusive source stubs too. Remove rejected Race enum names without renumbering surviving explicit values. Review old company saves and custom race-setting files before removing their compatibility paths: preserve current curated campaigns where feasible, and ask before introducing an incompatible save/settings change. If numeric tombstones are needed, explain them and seek approval rather than silently retaining named species implementations. Shared helpers (earlier FairyUtil is a known example) must be separated from the rejected identity while preserving their retained behavior. Keep copyright/author credits even where the corresponding implementation is retired.
3. **Move only approved exclusive files to the project archive.** Destination: project-root `Recycle Bin/Retired Content/`, preserving the original Assets/... hierarchy. This is outside Assets, Resources and StreamingAssets, so Unity neither compiles nor packages it. Move assets and their .meta companions together, and relevant directory metadata where an entire directory is archived. Verify resolved absolute source/destination paths are inside this project, refuse destination collisions, and use direct moves; never copy-then-delete. No files/directories are deleted and nothing in Recycle Bin is force-added to Git. Close Unity before source/serialized asset moves to prevent stale editor state from restoring them.
4. **Rebuild shipped sprite bundles and eliminate stale payloads.** Moving raw PNGs alone does not remove embedded artwork from the existing empire/merc/monster/specialmerc bundles. Trace retained ownership, rebuild from remaining approved inputs using the installed Unity version, and archive superseded bundles/manifests rather than deleting them. Mixed bundles cannot be moved wholesale if they still contain retained artwork. Resources/Addressables/build inclusion must also be reviewed. Inspect the fresh build's asset inventory; do not claim removal based only on a source-folder scan. Prior private builds contain historical content and remain untouched unless separately authorized for archival.
5. **Verify and hand off once.** Compile and check scene/prefab GUID bindings, retained species portraits/animations, generic effects and shared helpers, Race Editor, campaign creation/recruitment and Single Battle; review the resulting player/bundle contents for excluded identities and assets. Use the existing checks where sufficient; no new framework by default. Confirm no moved paths remain under Unity asset roots and no archive payload is shipped. Supply one combined manual checklist, not repeated per-species tests. Preserve prior user changes and save data; do not inspect personal saves without authorization.

## Approval gates and licensing

This draft requests approval of the scope and phased approach only. No implementation or move is authorized by this document. After the ownership audit, obtain approval for the concrete move manifest and any ambiguous/shared-content or compatibility decisions before relocation. The user may narrow or expand the removal list first.

Every later source change/move requires dated conversion notices, exact CHANGES.md records and preservation of existing license/attribution notices. Archive relevant author/provenance documents alongside retired assets; preserve required attribution in the active project where applicable. No license grant or external publication changes are proposed. Existing provenance/distribution gates remain separate.


## User scope clarification — 2026-11-03 (supersedes earlier recommendations)

Preserve generic/shared assets and non-rejected unreleased species for future curation. A retired consumer does not establish exclusive ownership or justify archiving a shared sheet. Remove permanently rejected character/species-specific depictions and dead exclusive code; Selicia is an explicit OC-removal example. Preserve the Sergal accessory and reusable Tatltuae animation, subject to separating their required frames from unrelated OC portraits/body artwork. Do not remove these borrowed presentations as previously recommended.

Current wiring was inspected: Sergal's BaseOutfit uses Asura frame39 as a discard sprite; MainAccessories.AsuraMask uses frame40 and CompleteSprite can display it via EarnedMask. EntropicChaos uses Tatltuae frames130–143. Its current attack caller is explicitly gated by Unit.Race == Race.Tatltuae in Actor_Unit; it is NOT currently a generic Avian animation. Preserve the reusable effect as requested, but do not silently add an Avian attack ability/animation to make it live. Visual frame ownership still requires review before extraction; retaining the effect is not approval to retain the entire OC sheet.

Manifest status PRESERVE_PARTS_HOLD_ORIGINAL marks Asura/Tatltuae source atlases and metadata. Generic mixed sheets remain held/protected until relevant ownership is established; filenames alone cannot select removal. No source/assets edited or moved. Next: frame-level separation/extraction manifest, retained generic ownership audit, and final physical move approval. Existing license/artist attribution follows retained derived assets; no claim about open-species status or asset redistribution rights is inferred from popularity or technical reuse.


## Latest decision and audit continuation — 2026-11-03

The user now explicitly retires Tatltuae's OC-only attack animation. This supersedes the earlier effect-preservation instruction. Proposed archival includes EntropicChaos.cs/.meta and Tatltuae.png/.meta after removing the Actor_Unit Tatltuae-only ranged-attack gate, TacticalGraphicalEffects.EntropicChaosEffect helper and obsolete sprite loader/dictionary bindings. No generic Avian gameplay or attack effect is added. Sergal accessory/discard parts from Asura indices39/40 remain protected, with runtime index-to-sprite mapping/visual ownership to be verified before separation.

The expanded audit found Selicia-specific rendering conditions inside retained graphics code, including Avians and Sergal. Exact conditions, source line numbers and direct sprite expressions are recorded in 21-selicia-shared-frame-review.json. These branches must be retired without discarding the whole retained sheets. Their conditions alone do not prove that each referenced frame depicts an OC; generic shape frames also require reuse checks before archival. Shared assets and generic color palettes/building art are now explicitly protected in the manifest. Unreleased non-rejected species remain protected.

The move manifest is versioned by status rather than pretending all rows are cleared. PROPOSED_MOVE is comment-only source stubs; other proposed groups explicitly require code/seed cleanup. HOLD, PRESERVE_PARTS and RETAIN statuses are not approved move operations. The summary JSON records current counts; earlier reports' totals are historical. There is no final complete artwork clearance yet. Next: finish the shared-frame/alias/seed and GUID/bundle-order review, then present the full actionable move manifest for approval. No source edits, image extraction, moves, deletion, Unity build/playtest or personal-save access occurred in this documentation audit.
