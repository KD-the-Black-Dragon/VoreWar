#11 — Injuries, recovery and downtime

**2026-11-01. Initial grouped implementation; player review pending.** The user accepted the preceding HP/naming feedback and authorized this milestone. Numerical tuning is provisional, as authorized. No further species, art, scenes or dependencies are introduced. This report covers one connected milestone, not per-ability/menu test packets.

## Implemented wiring

- `Core/CompanyInjuries.cs` owns serializable injury records, injury checks, descriptions, derived penalties, transitions, treatment, the Recovery daily stage and the Sparring daily stage. `Unit.cs` explicitly marks the private injury backing field for Odin serialization; a custom getter alone is insufficient in the installed serializer. Base stats are never rewritten to apply injuries. Each character owns their injury records; Unit.Clone detaches the list and each record.
- `Actor_Unit.SubtractHealth` and `Unit.NonFatalDamage` use `Unit.ApplyCombatDamage`. It measures lasting HP lost after temporary HP absorption, then makes one injury entry for that damage call. The damage calculator does not also roll. Thus retained attacks, direct/status spells, poison/virus, digestion and environment paths using these entries are covered. HP costs, item stat changes, initialization and ordinary healing do not roll. Already-digested units and magical summons do not gain persistent injuries. Downed units remain alive under the accepted digestion-only loss rules.
- Armor protection maps head to Helmet, torso to Torso, leg to Legs and hand/arm to Arms. The original Pure Tactical inventory is searched safely by its retained armor item names instead of indexed as nine slots. Initial body-part selection is uniform; curated animals use head/body rather than human hands/legs. Damage-type targeting and preferential hits to unarmored parts remain future refinements.
- DEX/MIND penalties are derived in Unit.GetStat; arm/body weapon damage and leg movement penalties are applied at the final shared combat calculations. Stats, movement and weapon damage retain a minimum of1. STR/END and maximum HP are not penalized, avoiding getter/equipment HP drift. Penalties stack and are not applied again on load.
- `CompanyCampaign` rules version3 adds saved `RestingDay` intent. The single clock still runs Wages → Deadlines → Refresh → Recovery → Sparring. A queued rest plan does not itself imply that an ordinary day is a resting day. Interrupted rest retains its current day's intent and stage; finishing that day consumes one planned rest day. Menu navigation, load and zero-campaign-time battle return do not process a day.
- `CompanyEconomy.Bind` upgrades older accepted company checkpoints without replaying past days, changing HP or regenerating targets. Only an older interrupted update with queued rest infers resting intent. The company leader is identified for permanent-injury immunity. The existing HP/fate round-trip guard now also verifies every injury field, treatment, immunity and interrupted recovery progress before publishing a checkpoint and after readback, as well as before detached battle settlement.
- Company members, active and reserve, recover through the same daily stage. Surviving active contract targets recover through that stage too; defeated IDs remain defeated. Former mercenaries retain their existing state without new off-company travel/recovery simulation. Existing wages, debt, contract expiry and recruit/contract refresh continue while resting.
- Minor injuries disappear on recovery. Serious injuries become weaker permanent old injuries; old injuries stack and treatment never removes them. The leader's serious injuries disappear completely instead. The special leader revival service clears all leader wounds and restores full HP, with the already accepted fee/no-time transition. Ordinary battle-end survival retains the accepted15% HP baseline and does not clear injuries.
- `CompanyRecoveryUI` adds paged injury inspection and per-member town treatment. Roster/member screens show injury counts and member XP; every deployment shows injury counts. Tactical InfoPanel shows injury names and penalties in both modes. Pure Tactical retains only its one-battle consequences. Map rest includes1/3/7-day shortcuts and a chosen1–365-day input; rest pauses for existing required decisions and must be explicitly resumed afterward.

## Provisional tuning

| Rule | Initial value |
| --- | --- |
| Ordinary daily company HP recovery |3% maximum HP, rounded up; at least1 |
| Rest in the field |10% HP and2 injury recovery days per elapsed day |
| Rest on a town tile |15% HP and3 injury recovery days per elapsed day |
| Ordinary injury recovery |1 recovery day per elapsed day |
| Surviving active contract target |2% HP rounded up and1 injury recovery day |
| Healthy sparring |2 XP/day through existing GiveExp and its trait modifiers |
| Healthy |Full lasting HP and no minor/serious injury after that day's recovery; old injuries alone are allowed |
| Minor/serious duration |3/8 ordinary recovery days |
| Treatment |10 gold/member; shorten every eligible untreated temporary injury by2 recovery days, minimum1 remaining; once per injury |
| Initial serious hit chance |1.5% +10% × fraction of maximum HP lost as lasting damage, before armor |
| Initial minor hit chance |If no serious hit injury:8% × (0.2 +0.8 × lasting-damage fraction), before armor |
| Deep attrition |On the first damaging entry reaching20% lasting HP or less in a battle, an additional20% serious-injury chance before armor |
| Armor |Each covered protection point divides that part's injury risk by1 + protection; current armor halves risk |

Damage fraction is capped at1. Temporary HP absorbs damage before these calculations. The deep-attrition flag resets at battle start, not each tactical turn; later hits can still make their normal hit checks. No special extra multiplier exists for becoming downed at0HP rather than a comparable near-zero hit. Nothing guarantees an injury. Successful hit and attrition checks can create separate injuries.

| Body part | Minor penalty | Serious penalty | Old-injury penalty |
| --- | --- | --- | --- |
| Hand |DEX -2 |DEX -5 |DEX -2 |
| Arm |Weapon damage -1 |Weapon damage -5 |Weapon damage -2 |
| Leg |Movement -1 |Movement -2 |Movement -1 |
| Head |MIND -2 |MIND -5 |MIND -2 |
| Body |Weapon damage -1 |Weapon damage -4 |Weapon damage -1 |

Names remain plain, such as Sore hand, Battered arm, Unsteady hand and Aching body. This is not a narrative rewrite. Wages still cover the lore notion of supplies/upkeep; no separate supply meter or cost has been added. Treatment is a time reduction, not an immediate HP spell. Long-rest XP/economy balance, probability calibration and larger/trait-heavy battles remain integrated MVP work in12.

## Verification

- Initial Unity2018.3.12f1 compilation plus read-only base scene/wiring inspection passed, return0, no C# errors/exceptions (`Logs/attrition-compile.log`). Final persistence/clone/XP refinements passed the handoff compilation/read-only scene pass too, return0 with no C# errors/exceptions (`Logs/attrition-handoff-compile-and-scene.log`). No new normalized compiler warnings versus the preceding actual HP compilation (`Logs/hp-display-compile.log`); a scene-only log without compiler output is not used as a warning baseline.
-24 synthetic managed diagnostics passed against the compiled production assembly and installed Odin, under .NETFramework (`Logs/attrition-managed-diagnostics.json`). Covered minor/serious/old transitions and stacking, leader exemption, treatment price/time/once-only guard, ordinary/camp/town recovery, healthy/temporarily injured/reserve sparring, armor and short inventory safety, temporary-HP shielding, zero-HP survival, injury/treatment/rest serialization and deliberate corruption rejection, actual clock payday pause/resume without extra resting days, independent cloned injury records, an actual production immutable checkpoint file round trip using synthetic data, and version2 interrupted-rest upgrade without replaying a day.
- These are synthetic managed data/method checks, not Play mode, random injury-roll calibration, actual tactical damage playback or player-interface validation. The initial direct reflection setup needed corrections; the State initializer depends on Unity-native services, so random rolls were not invoked outside the engine. No new test framework or project test source was added. No player build is claimed.
- `Logs/attrition-source-review.json` records all14dated source notices/CHANGES coverage, preserved earlier notices/encoding, no introduced source trailing whitespace or removed legal-notice candidates,1248unchanged retained-resource hashes, unchanged base scene and License.txt, and the warning comparison. Unity is closed. Existing provenance and distribution obligations remain a release gate. No personal checkpoint recovery, destructive cleanup, file moves, commit or publication.

## One connected player review

1. Use a company to fight a contract, keeping one member in reserve. On return, inspect survivors' HP and **Company → Manage → Injuries / recovery**. Injury rolls are random; no injury on a particular hit is expected. Check a damaged survivor's HP rises after an ordinary End day and rises faster when resting. Reserve members should recover too.
2. When an injury occurs naturally, inspect its name/penalty and remaining recovery. In town, use **Treatment** on that member, then rest using either a shortcut or the chosen-days field. Treatment should charge once, shorten temporary recovery and leave old injuries untouched. Minor wounds clear; a regular mercenary's serious injury settles into a weaker old injury. The leader never retains an old injury. Do not grind repeated battles just to force every injury category in this review; report any naturally observed cases.
3. During the same run, cross payday with a multi-day rest. If wages cannot be paid, rest must stop at the decision and leave the promised grace day available. Resolve/pay as desired, then choose Resume rest or cancel the remaining plan. An ordinary day after pausing rest should grant ordinary healing only. A healthy member's visible XP should increase over days, including a healthy reserve.
4. Save/reload during this workflow. HP, injury records, treatment and remaining rest must match; loading should not heal again or add XP/days. Briefly check tactical unit inspection shows any injuries that occur. No repeated full menu tour is requested.

After this grouped review is accepted, proceed to12 integrated MVP checks, balance/help and release-readiness review. Do not mark the whole conversion complete from this milestone.

## Feedback correction

2026-11-01: User found the inherited XP cap sentinel, requested injury announcements and a separate departure dialog. [The feedback report](11-feedback-progression-and-notices.md) records company progression tuning, injury log/queued popup and OK-only departures with a separate simultaneous wage warning. The original11player review is not being marked fully accepted.
