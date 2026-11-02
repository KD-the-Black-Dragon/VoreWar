# 03B3D — Rejected stat definition retirement

Date: 2026-11-01. User accepted 03B3C's Race Editor persistence/tab checks and authorized this next packet. This retires the full approved rejection set together, not a few species per test.

## Completed source scope

Only `Assets/Scripts/Races/RaceParameters.cs` changed. Retired 90 prototype fields, 90 complete initializer payloads and 90 matching switch arms. SpeciesPolicy's exact permanent-rejection set supplies the mapping; some prototype variable names differ from enum IDs, so no name-only assumption was used. No nonretired species shares a removed prototype. The before state had 155 prototypes and 154 species mappings; the after state has 65 prototypes (Default plus 64 species) and 64 species mappings.

Every remaining initializer, including its stats, traits, descriptions and raw mappings, matches packet start exactly. Supporting RaceTraits/RaceStats/VoreType types are unchanged. Numeric Race IDs, policy/curation status, serialized fields and dictionary records are untouched. No species has been approved for gameplay.

GetRaceTraits rejects permanently retired identities explicitly before the Config.RaceTraitsEnabled check. Removed cases cannot fall into the legacy Default substitution. GetTraitData routes non-null units through that same boundary even when racial traits are disabled. The existing null-unit/disabled-traits Default result is retained; null with traits enabled still fails. Nonretired Config-false behavior, unknown IDs and the special 700/701 Default behavior remain unchanged. No replacement species, null metadata value or synthetic retired prototype was introduced.

## Read-path closure

| Reader group | Applicable boundary |
|---|---|
| Startup/settings hydration | 03B3A Sanitize skips retired rows without rebuilding defaults; 03B3C blocks new retired RaceSettingsItem construction before metadata/graphics hydration. Existing raw rows remain accessible via GetExisting. |
| Ordinary settings/power/cosmetic reads | 03B3C removed the lazy read/create API across all callers. Typed prototype fallback now uses the explicit retirement check. This does not promise runtime support for a fabricated retired Unit. |
| Race Editor | 03B1 selections/bulk loops exclude permanent rejection but allow nonretired development metadata; 03B3C intentional edits still materialize mutable records. Raw rejected destination IDs display as Keep existing mapping and are not followed for editor metadata loads. |
| Skirmish/new units/spawns/copies/forms | Prior 03A policy checks select approved/enabled role-compatible species before construction or transformation. Skirmish village geometry receives its selected defender species. No substitute is introduced when the approved registry is empty. |
| Legacy world/load/provider code | 05A1 closes old World/tactical-save application; 05A2–A6 disconnect campaign creation/scheduling/combat providers. Their residual source is not licensed to instantiate rejected stats again. This packet is not a migration of old saves. |
| Runtime stats, containment, tooltip/shop/unit inspection | Consume the already-established nonretired unit identity. Player disabling is not retirement: nonretired individuals keep their metadata even when new generation is disabled. Invalid direct retired requests fail explicitly rather than becoming a different species. |

The two nonretired owners with rejected raw destinations remain byte-for-byte unchanged: MatronsMinions.LeaderRace -> Abakhanskya and FeralUmbreon.ConversionRace -> FeralEevee. Empire leader generation and allegiance conversion are retired owning paths; Pure Tactical leaders use their selected species directly. Existing mapping values are not metadata dereferences or species approval. Re-enabling an owning feature in the future must re-review these destinations rather than silently substitute another identity.

## Exact retired mapping

| Rejected Race ID | Retired prototype field |
|---|---|
| Aabayx | Aabayx |
| Abakhanskya | Abakhanskya |
| Alraune | Alraune |
| Asura | Asura |
| Auri | Auri |
| Bears | Bears |
| Bella | Bella |
| BoomBunnies | BoomBunnies |
| Cake | Cake |
| Cats | Cats |
| Centaur | Centaur |
| Cherub | Cherub |
| Cierihaka | Cierihaka |
| Cockatrice | Cockatrice |
| Collectors | Collectors |
| Compy | Compy |
| Crux | Crux |
| Crypters | Crypters |
| DRACO | DRACO |
| DarkSwallower | BlackSwallowers |
| DewSprites | DewSprite |
| Dogs | Dogs |
| Driders | Driders |
| EarthDryad | EarthDryad |
| Earthworms | Earthworms |
| Eevee | Eevee |
| Equaleon | Equaleon |
| Erin | Erin |
| Fairies | Fairies |
| Feit | Feit |
| FeralEevee | FeralEevee |
| FeralEqualeon | FeralEqualeon |
| FeralFrogs | Frogs |
| FeralOrcas | FeralOrcas |
| FeralSharks | Sharks |
| Foxes | Foxes |
| FungalDryad | FungalDryad |
| Ghosts | Ghosts |
| Goblins | Goblins |
| Hamsters | Hamsters |
| Harpies | Harpies |
| Humans | Humans |
| Iliijiith | Iliijiith |
| Imps | Imps |
| Ki | Ki |
| Konane | Konane |
| Lamia | Lamia |
| MainlandElves | MainlandElves |
| Merfolk | Mermen |
| Nectar | Nectar |
| Olivia | Olivia |
| OoviKat | OoviKat |
| Puca | Puca |
| Pudding | Pudding |
| Raptor | Raptor |
| Renamon | Renamon |
| RiverDryad | RiverDryad |
| RwuMercenaries | RwuMercenaries |
| Ryan | Ryan |
| Salix | Salix |
| Schiwardez | Schiwardez |
| Scorch | Scorch |
| Scylla | Scylla |
| Selicia | Selicia |
| Seraph | Seraph |
| Seville | Seville |
| Singularity | Singularity |
| Skapa | Skapa |
| Slimes | Slimes |
| SoulSprite | SoulSprite |
| Succubi | Succubi |
| Taraluxia | Taraluxia |
| Tatltuae | Tatltuae |
| Taurus | Taurus |
| Tigers | Tigers |
| Umbreon | Umbreon |
| Vagrants | Vagrants |
| Viisels | Viisels |
| Vipers | Vipers |
| ViraeUltimae | ViraeUltimae |
| Vision | Vision |
| Voilin | Voilin |
| Whisp | Whisp |
| Wolves | Wolves |
| WoodDryad | WoodDryad |
| Xelhilde | Xelhilde |
| Yordles | Yordles |
| Youko | Youko |
| Zera | Zera |
| Zoey | Zoey |

## Verification

Unity compilation PASSED exit 0 (Logs/packet-03b3d-compile.log), no C# errors/logged exceptions/new compiler warnings versus 03B3C. Source review verified all 90 matched removals, the 64 exact retained dispatch mappings, all 65 exact retained initializer payloads, unchanged supporting types and unchanged base.unity bytes. Before snapshots, removed spans, complete mapping, expected final source and diff are recorded in ignored Logs/packet-03b3d-*.

Explicit Assets/base.unity opening PASSED exit 0 (Logs/packet-03b3d-scene.log), without logged exceptions/errors. Final source correspondence, unchanged scene bytes, preserved BOM/line endings and unchanged unknown-ID/special-ID fallback text passed. git diff --check flags one pre-existing trailing-space line (Traits.Webber in the retained BlackWidow initializer) because the large removal changes diff alignment. Its exact packet-start text is preserved; a whitespace-line multiset comparison confirms no new trailing whitespace. It was not reformatted as unrelated cleanup. No Unity process remains; prior uncommitted changes are preserved. No new test framework/tests or player build. Actual battles cannot be tested until species curation; no temporary species was approved for testing. A focused manual check should open Pure Tactical setup and load a few remaining development species in Race Editor, confirming no rejected species appears there and no new red errors. This is not a request to revisit every content-settings page.

## Remaining boundaries

This completes rejected *stat prototype* retirement only. Eager graphics registry entries, typed graphics dependencies and shared bundles/assets still require a separate ownership/dependency pass. No files were deleted or moved; no asset or bundle removal is implied. Remaining enum tokens/raw settings/mappings and old dormant source are not proof that retired stat payloads still exist, nor proof of full content retirement. Spec 03/phase 2 remain open. New campaign, Downed, HP, injury and leader-revival implementation remains later work.

## GPL modification check

Read local License.txt sections 4/5. Actual 2026-11-01 conversion notice added to the modified source alongside the earlier notice; CHANGES.md names the exact path and completed retirement. Prior attribution/license material and License.txt preserved (hash matches HEAD). No new external code, dependencies or assets. Final notice/path coverage and source/scene scope review passed; earlier notices and unrelated work preserved. This source-modification check does not settle release/distribution obligations or outstanding shared-asset provenance.


User accepted the focused 03B3D checks. [03B4](03b4-rejected-graphics-registry-retirement.md) follows with the consolidated graphics registry/typed-consumer boundary; it does not remove shared sprites or bundles.
