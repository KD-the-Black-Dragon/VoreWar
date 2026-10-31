# Packet 04A — legacy 4X event retirement

**Status:** Implemented, 2026-10-30; fresh compilation and scene loading passed; the user subsequently confirmed the manual checks looked good and authorized 04B. The user confirmed both 03A2 review items worked and authorized this packet. No species has been approved.

## Result and scope

Retired the original 4X event system at its sources: built-in player/AI catalogs, custom-file schema/parser, dispatch, response callbacks/effects, turn triggers, startup/reset event-file seeding and old panel lifecycle. This is separate from the tactical combat log/prose reset in 04B and from new company events in 13.

The user-owned events.txt file is neither read nor modified by this work. The shipped StreamingAssets payload is now a dated inactive notice. No file or directory is deleted/moved. The Unity scene and shared prefabs are unchanged.

## Source chain and changes

| Exact source/content path | Prior route | Implemented retirement |
|---|---|---|
| `Assets/Scripts/Core/State.cs` | Static EventList, startup and reset construction, two system text-file seed lists including events | Remove the static dispatcher and both constructions; both seed lists retain customTraits but omit events. Other names/traits/configuration initialization is unchanged. |
| `Assets/Scripts/Entities/Empire.cs` | CheckEvent delegates to State.EventList.CheckStartEvent | Remove that method; leave event-history fields/LoadFix/constructor initialization intact for existing serialized shape. |
| `Assets/Scripts/Modes/StrategyMode.cs` | Start binds EventUI; strategic round calls CheckStartAIEvent; human turn calls ActingEmpire.CheckEvent | Remove UI binding and both trigger families. Other turn scheduling, income and combat are not rewritten here. |
| `Assets/Scripts/Utility/EventList.cs` | 36 built-in player cases, 32 AI cases, custom dispatch, listener attachment and all event-specific effects/helpers | Replace executable contents with a dated retirement notice; no catalog/dispatcher class remains compiled. File path and metadata remain. |
| `Assets/Scripts/Utility/CustomEventList.cs` | CustomEvent schema, events list, random selection and ISO-8859-1 user-file parser | Replace executable contents with a dated retirement notice. No parser, file read or random-event selector remains. File path/metadata remain. |
| `Assets/Scripts/UI/EventScreen.cs` | Choice buttons and 1/2/3 shortcuts; OnDisable schedules StrategyMode.Regenerate | Preserve serialized fields/layout only. OnEnable clears state and rejects activation; OnDisable clears state without scheduling strategy work; runtime listeners are removed and persistent calls disabled; text cleared and buttons disabled. Remove keyboard invocation entirely. |
| `Assets/StreamingAssets/events.txt` | 141 Event markers and legacy 4X prompt/choice/effect payloads | Replace content with a dated inactive notice; no Event records or old schema remain. Stop copying/loading rather than deleting personal copies. |

Source review found no callers of EventList, CustomEventList or CustomEvent outside the retired sources and three lifecycle/turn owners. After edits, searches across all Assets C# sources find no remaining type/dispatch references. EventList and CustomEventList were non-Mono data types, not scene attachments.

EventScreen remains an enabled scene component: ID 1962416437 on GameObject 1962416436, with choice references 175545727, 1938070468 and 1278808415. Its activation guard handles an accidentally reactivated old panel without requiring a broad scene edit. Because both runtime and persistent listeners are disabled, a direct UnityEvent invocation cannot run an old choice. Closing it no longer updates/regenerates the retired strategic mode.

## Retained scaffolding and boundaries

- StrategyMode.EventUI is an inert serialized reference to the retained layout; no code binds it to an event controller. EventScreen must receive a new lifecycle/controller if reused for spec 13.
- Empire.EventHappened and RecentEvents remain serialized history only. They do not dispatch or replay anything; no save migration/deletion is introduced.
- The unused generic RandomEvent text/action holder remains untouched. It was not the old active controller and is not a newly implemented campaign event framework.
- Two DefaultTooltips entries still describe retired event controls. Those controls were removed by 02C; tooltip text is dormant metadata, not a loader/dispatcher. Broad obsolete-help/description cleanup remains its owning later packet.
- Existing reset/maintenance methods were not invoked. Their unrelated existing file operations were not expanded. No personal event file needs deletion to prevent legacy events returning.
- This packet removes event-specific generation, gold/population/happiness/relations/trait effects together with their definitions; it does not change retained tactical rules or reset tactical narrative strings.
- Event checks previously consumed the shared State.Rand. Removing them changes possible subsequent random draw sequences. Exact same-seed outcome equivalence is not claimed; no new RNG architecture is introduced. 04B still needs its explicit narrative-randomness decision before implementation.

## Validation and limits

Fresh Unity 2018.3.12f1 runtime/editor compilation passed with no C# errors or import exceptions and return code 0 (`Logs/packet-04a-compile.log`, ignored). The subsequent explicit scene open passed with no parse/broken-reference errors or exceptions and return code 0 (`Logs/packet-04a-scene.log`, ignored). Verified both assemblies were freshly compiled and runtime assembly was newer than all six changed C# files. Closure searches across Assets contain no old event constructors/types/trigger calls; startup/reset have no event seed and the shipped payload has no Event markers. All seven paths have dated source/content notices and exact CHANGES entries. Scene bytes and License.txt Git content are unchanged; both known Unity-removed orphan metadata paths were restored from tracked bytes. No personal-data file was traversed or reset as a check. No new tests/framework or player build.

Review already performed: packet-start snapshots are in ignored Logs/packet-04a-<filename>-before.txt and scene snapshot; narrow caller/panel diffs inspected; no explicit copyright/license/credit notice found in replaced payloads. License.txt/upstream attribution remain preserved. Packet-scoped whitespace check passes; an unrelated pre-existing README trailing space is left untouched.

Runtime legacy-event/battle coverage is unavailable while the approved roster is empty. The source catalogs and dispatch types are absent rather than relying on an event-rate toggle. Startup/reset seeding closure is established by source review; no personal-data reset was executed as a test.

Short player review: open the main menu, Content Settings and Pure Tactical setup, then return. The six settings pages and intentional empty roster should behave as before, with no old event window or new red Console errors. No legacy save, data reset or battle is needed for this smoke check.

After feedback, proceed to 04B's neutral tactical-prose reset and narrative-context work. First refine its implementation coverage and state the shared-randomness handling; do not silently author replacement flavor text or turn old 4X events into company events.

## GPL/source-change check

Six modified C# files have prominent dated 04A notices, with State's earlier notice retained. The shipped text asset carries its own dated notice and is identified by exact path in CHANGES.md alongside every C# path. Existing license/attribution/legal notices are preserved; no third-party code, assets or dependencies introduced. File paths/metas remain, and no personal files are removed or published. This is the packet's source-change workflow check, not a completed distribution or third-party-license audit.

## Subsequent user confirmation

The user reported that the manual tests looked good and authorized the next packet, 04B. No legacy battle or personal-data reset was requested or inferred.
