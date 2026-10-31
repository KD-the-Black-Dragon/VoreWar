# Packet 03A2 — runtime species routes and restoration boundaries

**Status:** Implemented, 2026-10-30. Fresh compilation, subsequent scene loading and confirmation import passed; the user subsequently confirmed both requested manual checks succeeded and authorized 04A. The user validated 03A1 and authorized this packet, requesting durable interruption notes. See [the checkpoint](resume-checkpoint.md) for exact current work state.

## Bounded result

The empty approved roster is unchanged. This packet extends the approved-species policy into special tactical creation and form-changing paths, and separates gameplay generation from cosmetic inspection and existing-form restoration. It introduces no new species, traits, events or campaign mechanics. No scene/prefab/resource files are edited, deleted or moved.

A disabled species cannot be introduced through the guarded new-copy/spawn/transformation paths. Restoring a saved or temporarily hidden individual's original state is deliberately unrestricted by the generation policy. This preserves existing identities instead of deleting or substituting them when species preferences change.

## Implemented source routes

| Exact source | Boundary changed |
|---|---|
| `Assets/Scripts/Core/SpeciesPolicy.cs` | Explicit Copy, Spawn and Transformation uses; reject negative sentinel species and unknown use values. All new uses still require code approval and player enablement. |
| `Assets/Scripts/Entities/Spells/SpellList.cs` | SummonDoppelganger checks the copied species; SummonSpawn resolves and checks its real mapped species before CastSpell. Generic animal summoning remains as implemented in 03A1. No new fallback or replacement species. |
| `Assets/Scripts/Modes/TacticalMode.cs` | AstralCall contributors must have an approved copy species; pheromone ant summons choose only explicitly approved animals, retaining the original one-to-two weighting among eligible entries. Empty animal pools return before random selection. Legacy caster-tower Fairies and defense-encampment reinforcements are guarded before construction; those systems still belong to later 4X retirement. |
| `Assets/Scripts/Entities/Units/PredatorComponent.cs` | All CreateSpawn callers share a destination gate before construction. A failed ChangeRaceAny now returns failure before saving a template or sharing traits. |
| `Assets/Scripts/Entities/Units/Unit.cs` | Guard ChangeRace, HideRace, CreateRaceShape, TriggerMorph, CopyTacticalUnit and TriggerEeveelution before new-form mutation. Morph checks happen before cloning, adding status, changing traits or appearance; rejected tactical copies clear only the pending copy reference. |
| `Assets/Scripts/Entities/Units/Actor_Unit.cs` | ChangeRaceAny rejects the template before smoke/traits/appearance/side changes. |
| `Assets/Scripts/Entities/Units/Prey.cs` | ChangeRace checks before resetting hidden/shared state or running removal/swallow callbacks. |
| `Assets/Scripts/UI/HoveringRacePicture.cs` | Preview the requested species directly, eliminating Cats as a dummy. Outside the gated Race Editor, only approved enabled dropdown previews are shown. An authorized editor session retains inspection, without granting generation approval. Initialize cached components before first hover. |
| `Assets/Scripts/UI/CustomizationLoaderWindow.cs` | Unapproved foreign-species preview entries are filtered; same-species existing-character inspection remains. Use the short metadata constructor rather than a full gameplay Unit constructor. |
| `Assets/Scripts/UI/UniformLoaderWindow.cs` | Apply the same foreign-preview rule and use a metadata constructor for incompatible-uniform previews. Existing compatible-character previews stay references to that character. |

The low-level Unit constructors and Races.GetRace remain available for metadata, startup and restoration. A blanket exception at those locations would break the deliberately empty roster and legacy metadata dependencies. No constructor-wide eligibility guarantee is claimed.

## Existing-state and data review

UnhideRace, RevertMorph, RevertCopiedUnit and HardCopyUnit bodies are unchanged: they must restore the existing original, including a species disabled after its creation. State.Load/deserialization is not filtered into deleting individuals. Clone itself is also unchanged; its saved/temporary-object uses differ from introducing a new gameplay participant.

RaceSettings exposes spawn/conversion/leader/morph destinations from edited data. Those values do not approve species: the new actual summon/spawn/form ingress guards validate their results. Its raw metadata getters and developer presets remain inspectable. Legacy leader generation and saved-world migrations still belong to the old campaign providers listed below.

CustomizerData.CopyToUnit and UniformData.CopyToUnit were inspected and do not assign the target's species. Their data loaders remain intact for same-species existing appearance. The show-all customization preview route required the separate foreign-species filter. Metadata previews still use the upstream short Unit constructor, which consumes State.Rand; avoiding full gameplay constructors also changes random draws. Exact same-seed sequence equivalence is not claimed. Separating presentation randomness remains coordinated with 04B rather than silently introducing a new RNG system here.

## Residual inventory and owning packets

03A's complete generation invariant remains open until its legacy providers are disconnected in their scheduled packets. The [creation/write inventory](../architecture/01b-species-creation-sites.md) remains a checklist, not proof that every lexical entry is a live or now-guarded route.

| Remaining family | Current disposition / next owner |
|---|---|
| Village creation/recruitment/garrisons, empire leader creation, Recruit_Mode, Goblin/Monster/legacy strategic AI and map/spawner population | Ordinary new setup is empty-roster guarded, but these old-world internals are not all filtered here. Retire their 4X entry/scheduler/providers in 05A/B; review residual retained factories before curation. |
| Legacy player/AI/custom events constructing or converting units | Retire the trigger, loader and callback sources in 04A, rather than adding policy checks to events that must never return. |
| Human dummies in TacticalMessageLog and LogUtilities | Metadata, not company members. Replace their narrative context with 04B before rejected Humans registry removal. No Human fallback approved. |
| Permanent resurrection/replacement, Reanimate, spontaneous allegiance changes, excluded conversion and permanent polymorph | Already approved for deprecation in design. Their full mechanical retirement is 05C/10. A species gate in this packet does not claim their unrelated healing/allegiance effects are gone. |
| Infection/spawn traits and CreateSpawn callers | Actual new participant construction is gated centrally. Existing damage, absorption and trait callbacks are not broadly removed as a side effect; review their retained design during trait curation/05C. |
| Race-specific registries, shared bundles, namelists, descriptions and obsolete UI helpers | Eligibility does not physically remove content. Follow 04B then 03B's dependency/resource review and preserve stable enum IDs. |
| Legacy saves and migration writes, original-form restoration, developer editor data | Preserve existing identities and metadata access. No old-campaign import into the new format is promised. New campaign schema/generation owns 06/07 integration. |

Next scheduled cleanup is 04A (old event retirement), after this packet's review. Carry the residual inventory through 04B/03B/05 and reconcile it before approving species. Do not mark all species routes complete merely because empty menus are safe.

## Validation and limits

Fresh Unity 2018.3.12f1 runtime/editor assembly compilation completed with no C# errors and return code 0 (`Logs/packet-03a2-compile.log`, ignored). That first import logged one editor CSharpNamespaceParser / IllegalNamespaceParsing diagnostic for PredatorComponent.cs before compilation; it is a non-MonoBehaviour class in a multi-type upstream file. No namespace/class layout was changed by the packet, but the first log is not described as exception-free. A subsequent confirmation import completed with no exceptions or C# errors and return code 0 (`Logs/packet-03a2-import-confirm.log`, reused current assemblies). The freshly compiled assemblies are newer than every changed source.

An explicit scene open after compilation passed without parse errors, broken-reference messages or exceptions and returned 0 (`Logs/packet-03a2-scene.log`, ignored). Verified unchanged base.unity bytes and unchanged restoration helper bodies, checked dated notices/CHANGES coverage and preserved License.txt content, and restored both known Unity-removed orphan metadata files byte-for-byte. The import diagnostic's absence on the clean follow-up is evidence of current import state, not proof that old Unity's namespace reader can never repeat it on a future full reimport. No new tests/framework or player build. Required checks are: fresh runtime/editor assembly compilation; explicit scene open after compilation; unchanged base.unity bytes; dated notice/CHANGES coverage; preserved license and known orphan metadata restoration. Nonempty summon/shape/appearance routes cannot be battle-playtested while no species is approved.

Manual review after compilation:

1. Confirm Content Settings still shows its six pages and the species list remains intentionally empty; Pure Tactical still shows the safe unavailable state.
2. Open the password-gated Race Editor, inspect the normal tabs and any available species hover preview, then close/reopen to check the password boundary. Developer inspection must not populate Species Availability.
3. Check the Console for new red errors while navigating these screens. Actual summons, transformations and existing-member restoration require later battle/save coverage with approved content; no temporary species is enabled for this review.

## GPL/source-change check

All ten modified C# files carry dated 03A2 notices and retain meaningful earlier notices. Root CHANGES.md identifies their exact paths. No new third-party source, dependencies or assets; License.txt and original attribution/legal text remain unchanged. No new metadata or serialized scene notices are needed for untouched paths. This is the source-change workflow check, not a complete distribution/third-party-license audit.

## Subsequent user confirmation

The user confirmed both requested checks worked correctly and authorized the next packet, 04A. No nonempty battle/save checks are inferred from that report.
