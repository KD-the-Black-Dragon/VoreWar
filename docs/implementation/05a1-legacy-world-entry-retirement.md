# 05A1 — retire legacy world entry and loading

**Date:** 2026-10-31. Implemented; final Unity compile passed. Explicit scene load passed with exit 0 and no parse errors/exceptions; interactive checks pending. User authorized this packet after accepting 03B3A and the read-boundary review.

## Scope

Five modified sources:

- `Assets/Scripts/Core/State.cs`: retire the old Load body, including world deserialization/migration/mode restoration, and PreviewSave body. Preview returns null without file reads; direct loading gives a shared compatibility message. Retain signatures for serialized/call-site compatibility. Save writing, settings persistence and snapshot schemas remain unchanged.
- `Assets/Scripts/Core/GameManager.cs`: quickload immediately reports the unsupported format. Its direct callback reaches the same guarded State.Load without first hiding the results screen.
- `Assets/Scripts/Modes/StartMode.cs`: empire campaign creation reports retirement regardless of species availability. Main-menu load can still open the browser, whose load controls explain the restriction.
- `Assets/Scripts/Subwindows/CreateStrategicGame.cs`: direct CreateWorld cannot construct an empire campaign. Retire the saved-world settings picker/importer too; that secondary deserializer could otherwise replace balance settings.
- `Assets/Scripts/Subwindows/SaveLoad.cs`: selection shows the compatibility message, disables loading/deletion controls, clears stale preview text and never deserializes a world. Direct Load reports the same message without confirmation/transition. Other save/browser code is retained.

No files are deleted or moved. Existing save files are retained. Pure Tactical setup still uses World(false); tactical snapshot types, settings/preset serialization, battle execution and save writing remain. **There is currently no supported save-resume path**, including old-format tactical saves produced by the retained writer. New campaign saving and reviewed tactical resume belong to later save work; a new company format is not implemented here.

## Evidence and limits

Source comparison verified 89 unaffected method bodies against packet snapshots. No DeserializeValue<World> remains in Assets/Scripts; no source call constructs the strategic World overload after CreateWorld retirement. Direct State.Load, quickload callbacks and the secondary settings importer are closed before their former reads/world mutation. Reflection/external plugins and arbitrary user modifications are not covered by lexical closure.

Fresh Unity 2018.3.12f1 compilation passed with exit 0 and no C# errors/import exceptions: ignored Logs/packet-05a1-compile-final.log. Earlier unsuffixed compilation predates the secondary importer change and is not final evidence. Explicit scene log: Logs/packet-05a1-scene.log. Scene bytes match packet snapshot. No player build, agent Play-mode test, personal-save inspection/reset, or new tests/framework.

All five files carry dated notices and exact-path CHANGES coverage. Reviewed License.txt sections 4/5; retained licensing/attribution, no third-party source/assets/dependencies. This is not a distribution compliance certification. Do not restore obsolete orphan metadata paths no longer tracked by HEAD.

This closes application/load entry, not the entire 4X scheduler, live tactical world dependencies or 90 stat definitions. Next audit/packet must disconnect strategic turn/provider execution before returning to stat read/prototype closure. Keep shared map/combat algorithms and numeric IDs; do not supply generic playable substitutes.

## User check

1. Main menu -> Load Game: browser should explain legacy saves cannot load; selecting a filename must not show the old world preview or enable Load. Files should remain listed.
2. Old campaign creation should show its retirement/not-yet-available message. Pure Tactical should retain the expected empty-approved-roster message; settings/Race Editor should open normally.

Interactive results remain pending. No existing save is needed: typing a name in the browser should show the same compatibility explanation.

The user confirmed all requested 05A1 manual checks passed and authorized 05A2.
