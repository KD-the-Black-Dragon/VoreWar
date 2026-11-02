# 03B8 â€” Disconnected graphics payload and projectile retirement

Date: 2026-11-01. User authorized implementation after 03B7 review.

## Exact source scope

- `Assets/Scripts/Races/Graphics/MainRaces/Aabayx.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Abakhanskya.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Alraune.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Bears.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Cats.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Centaur.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Crux.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Crypters.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Dogs.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Driders.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Eevee.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Equaleon.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Foxes.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Ghosts.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Harpies.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Humans.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Imps.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Lamia.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/MainlandElves.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Mermen.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Scylla.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/SlimeQueen.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Slimes.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Taurus.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Umbreon.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Wolves.cs`
- `Assets/Scripts/Races/Graphics/MainRaces/Yordles.cs`
- `Assets/Scripts/Races/Graphics/Mercs/Cockatrice.cs`
- `Assets/Scripts/Races/Graphics/Mercs/DewSprite.cs`
- `Assets/Scripts/Races/Graphics/Mercs/Goblins.cs`
- `Assets/Scripts/Races/Graphics/Mercs/Hamsters.cs`
- `Assets/Scripts/Races/Graphics/Mercs/OoviKat.cs`
- `Assets/Scripts/Races/Graphics/Mercs/Puca.cs`
- `Assets/Scripts/Races/Graphics/Mercs/Renamon.cs`
- `Assets/Scripts/Races/Graphics/Mercs/RwuMercenaries.cs`
- `Assets/Scripts/Races/Graphics/Mercs/Succubi.cs`
- `Assets/Scripts/Races/Graphics/Mercs/Tigers.cs`
- `Assets/Scripts/Races/Graphics/Mercs/Viisels.cs`
- `Assets/Scripts/Races/Graphics/Mercs/Vipers.cs`
- `Assets/Scripts/Races/Graphics/Mercs/Youko.cs`
- `Assets/Scripts/Races/Graphics/Monsters/Bella.cs`
- `Assets/Scripts/Races/Graphics/Monsters/BoomBunnies.cs`
- `Assets/Scripts/Races/Graphics/Monsters/Cake.cs`
- `Assets/Scripts/Races/Graphics/Monsters/Collector.cs`
- `Assets/Scripts/Races/Graphics/Monsters/Compy.cs`
- `Assets/Scripts/Races/Graphics/Monsters/DarkSwallower.cs`
- `Assets/Scripts/Races/Graphics/Monsters/EarthDryad.cs`
- `Assets/Scripts/Races/Graphics/Monsters/Earthworms.cs`
- `Assets/Scripts/Races/Graphics/Monsters/Fairy.cs`
- `Assets/Scripts/Races/Graphics/Monsters/FeraEqualeon.cs`
- `Assets/Scripts/Races/Graphics/Monsters/FeralEevee.cs`
- `Assets/Scripts/Races/Graphics/Monsters/FeralOrcas.cs`
- `Assets/Scripts/Races/Graphics/Monsters/Frogs.cs`
- `Assets/Scripts/Races/Graphics/Monsters/FungalDryad.cs`
- `Assets/Scripts/Races/Graphics/Monsters/Iliijiith.cs`
- `Assets/Scripts/Races/Graphics/Monsters/Pudding.cs`
- `Assets/Scripts/Races/Graphics/Monsters/Raptor.cs`
- `Assets/Scripts/Races/Graphics/Monsters/RiverDryad.cs`
- `Assets/Scripts/Races/Graphics/Monsters/Schiwardez.cs`
- `Assets/Scripts/Races/Graphics/Monsters/Sharks.cs`
- `Assets/Scripts/Races/Graphics/Monsters/SoulSprite.cs`
- `Assets/Scripts/Races/Graphics/Monsters/Vagrants.cs`
- `Assets/Scripts/Races/Graphics/Monsters/ViraeUltimae.cs`
- `Assets/Scripts/Races/Graphics/Monsters/Voilin.cs`
- `Assets/Scripts/Races/Graphics/Monsters/Whisp.cs`
- `Assets/Scripts/Races/Graphics/Monsters/WoodDryad.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Asura.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Auri.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Cherub.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Cierihaka.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/DRACO.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Erin.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Feit.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Ki.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Konane.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Nectar.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Olivia.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Ryan.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Salix.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Scorch.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Selicia.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Seraph.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Seville.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Singularity.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Skapa.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Taraluxia.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Tatltuae.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Vision.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Xelhilde.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Zera.cs`
- `Assets/Scripts/Races/Graphics/UniqueMercs/Zoey.cs`
- `Assets/Scripts/Modes/Tactical/TacticalGraphicalEffects.cs`

Retire the 91 disconnected class bodies (90 rejected species plus SlimeQueen), preserving paths/.meta and existing legal/meaningful earlier modification comment blocks. Preserve shared base classes, generic clothing and palettes. Fairy.cs retains its exact FairyType/FairyUtil pre-class helper block because Unit construction calls it; this helper is not a graphics registry entry or species approval.

The expanded lexical scan recorded casts/generics/type-member/serialized-type candidates. Candidates outside the disconnected files resolve to enum values, palette/sprite fields, Bestiary GameObjects, local customization methods and text, rather than these graphics types. This remains source/text evidence, not arbitrary reflection/binary proof.

TacticalGraphicalEffects removes 27 rejected-only branches and setup methods; the first remaining ArrowType branch becomes if rather than else-if. Generic spell effects and retained species/projectile behavior are preserved. No replacement species or effect introduced. Sprite loader/resource ownership needs a refreshed follow-up now that these consumers are gone; do not automatically clear a shared resource from names alone.

## Verification

Initial compile failed on shared FairyUtil, exposing a pre-class helper missed by the class-name scan. That exact helper/enum block was retained before the final compile. Initial failure log preserved: Logs/packet-03b8-compile.log. Final compile PASSED exit0, no C# errors: packet-03b8-compile-final.log. Warning delta versus full03B5 compile: 1 new warnings, recorded in Logs/packet-03b8-warning-delta.json. Source snapshots, expected text, legal comment records, effect removed spans and type scan under ignored Logs/packet-03b8-*.

Explicit base.unity scene PASSED exit0. Source correspondence, legal comment/notice coverage, exact effect spans/shared helper preservation, unchanged License hash and scoped whitespace checks passed. Fairy EOF whitespace-only cleanup followed compilation; executable code unchanged. No Unity process remains. No new tests/framework or player build. Actual combat remains blocked by empty approved registry. After agent checks, one combined startup/setup/Race Editor smoke check; no per-species testing cadence or settings tour.

## GPL and remaining boundaries

Every changed source carries an actual 2026-11-01 conversion notice and exact CHANGES path record; earlier legal/attribution comments retained. No external code/assets/dependencies introduced. No file deletion/move, bundle rebuild, artwork/scene edits or species approval. License.txt unchanged; distribution/asset provenance remains a separate release review. Spec03 and phase2 remain open.
