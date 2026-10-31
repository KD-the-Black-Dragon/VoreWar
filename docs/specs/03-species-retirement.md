# 03 — Disable the legacy roster and retire rejected species

**Status:** In progress, 2026-10-30. The user accepted 03A/04, 03B preflight, 03B1 and 03B2. [03B3A](../implementation/03b3a-override-hydration-boundary.md) prepares override loading for definition retirement and corrects new mapping defaults; compile/scene checks passed, interactive review pending. Stat definitions, graphics/resources and 05 provider retirement remain. Spec 03 is not complete; no species approved.

**Phase:** Phase 2 — retire unwanted systems

**Prerequisites:** 01 reviewed; coordinate settings entry points with 02.

## Purpose and scope

Study 01A identified two UI/data paths that must not revive disabled species: ContentSettings.CreateList generates Merc toggles from every nonnegative Race value with fallback true, and startup loads the selected EditedRaces data slot. WorldConfig.ResetDictionary also sets all legacy Merc keys true. 02C has now retired the dynamic UI and hidden the legacy page, but its temporary data policy still defaults legacy Merc keys true; retiring the page alone does not enforce eligibility. 03A must replace that temporary policy as well as the generation paths. See [the settings study](../architecture/01a-navigation-and-settings.md); the complete generation/runtime audit belongs to packet 01B.

### Preparation from study 01B

[Packet 01B](../architecture/01b-species-events-and-narrative.md) maps [all 90 rejected names](../architecture/01b-species-removal-map.md) to enum IDs and registry classes, and inventories [construction/race-write candidates](../architecture/01b-species-creation-sites.md). These are source/dependency findings, not completed removals.

- Preflight eligible choices before constructing units. Pure Tactical previews use Cats, random selection falls back to a numeric main-race range, and the Summon spell inserts Vagrants when its pool is empty. The log and LogUtilities also create Humans dummy units. Replace these dependencies explicitly for the empty-roster stage.
- Validate species role and enablement in bulk tests, actual generation, spawn/conversion/morph/leader mappings, direct Race writes, disguises, and loaded appearance/override data. Constructor-only checks miss several routes.
- Generic creature summoning selects animals only; temporary sapient clones/doubles/illusions are allowed. Review other summons individually rather than applying a blanket animal-only rule. Deprecate permanent animal polymorph and the excluded conversion mechanic; no spontaneous mid-battle allegiance changes. See [the clarification record](../design-clarifications-2026-10-30.md).
- Retire eager registry/parameter/graphic references in dependency order. Four mixed graphics bundles are unconditionally loaded; do not remove a shared bundle based on one rejected species.
- Preserve existing numeric identity while remaining references depend on it. BlankSlate fallback is not an eligible replacement species. Candidate manifest paths require ownership/repacking review before file retirement.

### Agreed scope

[Study 01D](../architecture/01d-campaign-persistence-and-identity.md) adds a persistence invariant: existing identities remain valid when their species is disabled for new generation. Do not reuse the generation filter as a load validator that removes the leader, offered candidates, former members or contract enemies. The [cleanup order](../architecture/01d-removal-sequence-and-risks.md) separates eligibility/empty-roster work (03A) from rejected-reference retirement (03B), with narrative/dummy-context cleanup before dependent species references disappear.

- Make every legacy species unavailable by default in both modes. Follow the user's outright-removal list exactly; do not choose favorites or infer sapience from old empire/monster categories.
- Create one eligibility boundary for curated status, sapient/animal classification, and global player enablement; route all relevant generation and selection through it.
- Audit named uniques, special hires, summons, random generation, bulk battles, fallback units, race overrides, and external data so none can silently re-enable rejected or uncurated species.
- Map every user-supplied removal name to code and assets before retiring its references. Preserve enum/serialized identity where required rather than renumbering unrelated entries.
- Retire rejected species code/content as fully as practical after shared dependencies are identified. Document any necessary residual references and why they remain.
- Handle an empty approved roster explicitly: disable invalid start/generation actions with a clear message instead of crashing, looping, or choosing an unapproved fallback.

## Completion and review criteria

- No species appears through normal generation in either mode before curation.
- The player's availability page lists only curated entries and cannot enable uncurated content.
- Every designated removal has a mapped disposition; no whole-file removal is performed outside the repository's file-operation rules.
- The zero-species state compiles and opens its available menus safely. Actual battle checks are recorded as pending until 07 enables approved species.

## Decisions, constraints, and risks

- Do not substitute an automatically selected species to keep battles launchable.
- Mid-campaign global disabling blocks new generation while preserving all existing individuals, including leader, offered candidates, former members, and contract enemies. Reappearance/rehire is not new generation. No forced campaign-start lock; verify save/load preserves the same rule.
- Source follow-up found mercenary toggles filter new candidates, while ContentSettings.SetValues can clear a disabled monster spawner's existing empire armies. Do not copy that side effect into the replacement availability page or claim the legacy page uniformly preserves existing individuals.
- Species asset references may exist in bundles or serialized data; code searches alone do not prove complete retirement.

## Deliverable

Species eligibility rules, removal manifest, safe empty-roster behavior, and updated curation checklist.

## Execution record

- 03A is split into two reviewed packets: 03A1 establishes approval/roles/preferences, the simplified availability page and zero-roster setup; 03A2 completes special creation/mapping, metadata and existing-world boundaries before species curation. See the [03A1 source/scope record](../implementation/03a1-approved-species-and-empty-setup.md).
- 03A1 adds an empty code-owned approved dictionary and 90 permanently retired entries, replaces true legacy availability defaults, guards tactical/campaign setup, filters normal mercenary and generic animal-summon pools, and adds the sixth settings page without changing base.unity.
- The user validated 03A1. 03A2 now guards reviewed tactical new-copy/spawn/form routes and removes gameplay construction from cosmetic previews; original-state restoration is preserved. See its [route/residual record](../implementation/03a2-species-runtime-routes.md). Legacy event/4X/narrative providers must be retired through their owning 04/05 packets before the complete generation invariant can be accepted.
- Actual nonempty pools, battles and the whole generation audit are not marked complete. 03B physical/reference retirement remains pending.
- 03B preflight reconciles all 90 entries against current registries; records 157 eager graphics fields, shared clothing/render dependencies, 383 available bundle source sheets and 372 sheets with serialized GUID candidates; proposes catalogs/uniques, names, registry/typed-consumer, leaf-content and staged-packing packets. See its [current matrix](../implementation/03b-rejected-species-dependency-matrix.md) and [ownership inventory](../implementation/03b-shared-sprite-and-bundle-inventory.md). These are source candidates, not asset-exclusivity or runtime proof. 03B1 is now implemented across five sources, including nested tag condition lists and old mapping/condition ID preservation; no scene/assets/name files changed. 03B1 is user-accepted. 03B2 now removes 103 rejected seed names, filters species/army imports and retires exclusive built-in pools while preserving generic names and personal paths. Next after startup/reload review is a refined 03B3 metadata/stat-registry packet.

Before execution, refine this spec with the architecture study, list its small change batches, and resolve decisions needed for the next batch. Follow the review and file-operation rules in [the roadmap](../conversion-roadmap.md). Design intent comes from [the campaign plan](../mercenary-company-campaign-plan.md) and the three curation checklists.


### 03B3 subdivision

03B3A changes only RaceSettings.cs: retired records remain stored but are skipped during Sanitize, while new leader/morph mappings use their own defaults. All serialized fields and existing lookup/save behavior remain. This is an accepted-scope preparation step with checks recorded in its report, not removal of 90 stat definitions. After user review, 03B3B must close direct metadata/read dependencies before prototype retirement; graphics and shared-clothing dependencies stay a separate bounded packet.
