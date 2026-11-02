// Modified for the VoreWar mercenary-company conversion.
// 2026-11-02: Replace retired empire guidance with company gameplay, save boundaries and retained references; correct trait enumeration (spec 12).
// 2026-11-02: Restore vore terminology in existing help without rewriting its deferred content (Options feedback).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using System;
using System.Linq;
using System.Text;

static class HelpText
{
    public static string GameBasics = @"Mercenary campaign

Lead one company through a finite world generated for each new campaign. There is no conquest victory or final objective: keep the company alive, take work and develop its members.

Create your leader, then enter the starting town. Your initial gold comfortably hires about two inexperienced mercenaries; choose among the available names, species and backstories. The leader draws no wages and must join every battle.

Town services offer recruitment, equipment, contracts and treatment. Return to the world map between errands. The company roster is available outside battle; active members fight and reserves stay out. Every battle gives a final deployment choice.

Pure Tactical is a separate single battle with no campaign progress. Only curated species can generate. Global species preferences stop new individuals from appearing while preserving existing characters and offers.

Values are provisional. Use the other help chapters for travel, wages, injuries and supported saving.";

    public static string StrategyMode = @"World map and days

Your company is the only moving force. Click adjacent map tiles to travel; grass/desert currently cost one action and forest/hills two. Nearby terrain is revealed as you explore. Towns are static, and accepted contracts mark their exact target even under fog.

Each campaign turn is one day with ten actions. At zero actions you cannot pay for further movement; End day advances time and refills actions. Visiting a service and taking individual transactions are separate concepts; most service action costs currently remain zero.

Rest advances the chosen number of days, anywhere. Resting in town improves recovery. Every skipped day still processes wages, contract deadlines, pool refreshes, healing and healthy-member sparring. Required notices interrupt rest; deal with them and explicitly resume or cancel the remaining plan.

Tactical battles take no campaign days. Moving to a contract target opens final deployment before the battle starts.";

    public static string RecruitMode = @"Recruitment, roster and wages

Each town has a small separate recruit pool that refreshes over time. Higher company reputation allows more and stronger candidates. Species talents affect roles, but any sapient recruit can develop in any direction. Appearance is fixed; ordinary members may be nicknamed. The leader's creation choices remain fixed.

There is no fixed roster or deployment cap. Wages are the main growth constraint and include rations/upkeep in the fiction. Active and reserve members both draw wages, including when injured. Mark reserves from Company or final deployment; the leader always deploys.

Wages are paid in advance every seven days, first due seven days after campaign start. Midweek hiring costs a prorated advance through the next payday, plus any old debt. Quoted weekly wages rise with level.

If the full payday cannot be covered, everyone owed gains personal debt. You decide whom to pay, in full. Threatened members get their stated grace day. An actual departure gets a separate notice. Former members retain their level, injuries, equipment and unpaid debt, and may offer their services in a town again. Rehiring must settle their old debt.";

    public static string TacticalMode = @"Tactical battle

Control your company against the AI using the existing combat actions. Melee and ranged equipment are separate: a unit with both automatically uses the melee weapon for melee attacks. Without one, it attacks unarmed.

At zero HP a permanent character is downed and cannot take any action for the rest of the battle. Fear can also down someone above zero HP. Downed units are not ordinary direct-attack targets; collateral damage does not kill them. They remain vulnerable to vore. Magical summons disappear instead of becoming downed.

Reach the appropriate edge and use Retreat when the existing retreat rules permit it. Downed allies do not silently retreat or become assumed casualties: if opponents can still act, the battle continues. You may use the existing instant simulation to finish it.

Downed or contained units do not by themselves finish a battle. Both sides unable to act, or the generous 200-turn limit, cause a stalemate: remaining units are released and count as fled. Enemy flight counts as contract defeat, even on a draw. Enemies cannot leave while carrying a living captive.

After battle, read the results, then handle any leader revival. Campaign rewards settle automatically.";

    public static string Stats = @"Character stats

Strength: melee accuracy/damage and part of maximum HP.
Dexterity: ranged accuracy/damage and a smaller contribution to escape.
Agility: defense and tactical movement.
Endurance: maximum HP and resistance to some damage.
Mind: magic accuracy, damage and other spell effects.
Will: magic defense, mana and much of vore defense/escape.
Voracity: vore accuracy and related offense.
Stomach: capacity, retention and digestion.

Species and traits still influence these stats. Leadership is retired; the company leader instead starts with stronger ordinary stats.

Injury penalties are derived separately from base stats and stack. Hand injuries reduce Dexterity, head injuries Mind, leg injuries movement, and arm/body injuries weapon damage. Equipment and existing trait/status effects still matter. Inspect a unit for its current effects.";

    public static string Items = @"Company equipment

Nine slots: Helmet, Torso armor, Leg armor, Arm armor, Accessory, Melee weapon, Ranged weapon and two spells.

Buy gear at a town merchant, then equip it through Company > Manage. Equipment can change outside battle. The company inventory owns unequipped items and members' equipped items; sell spare unequipped gear at a merchant. Dismissed or departing members keep their gear.

An HP-granting item changes current and maximum HP by the same amount when equipped/removed. If removing one would leave its wearer without positive HP, recover first.

Armor also reduces injury risk for its covered body part. Helmets protect the head, torso armor the body, leg armor the legs and arm armor the hands/arms. There is no armor appearance system in this version.

Digested mercenaries' equipped gear is lost with them. The initial contract reward is gold only. Pure Tactical keeps its original equipment setup.";

    public static string Vore = @"Vore, HP and company fate

The retained vore/digestion mechanics remain central to combat. Health, stats, size and capacity affect attempts against standing units. Downed units have guaranteed vore odds when the attempt otherwise meets its capacity/range requirements.

A downed or contained character is still alive. Permanent characters are only removed by actual completed digestion; collateral damage cannot permanently kill a downed member. The battle continues to resolve those fates rather than inventing off-screen losses.

Grey temporary HP absorbs damage first and disappears after battle. Retained spells/traits normally grant temporary healing. Retained vore recovery raises lasting HP and survives battle end. Surviving critically hurt characters return with a modest HP baseline, currently 15%, and keep their injuries.

Digested non-leaders are permanently lost and recorded in the memorial. A digested leader can use the optional unique revival service if the company still has survivors: results/rewards first, then a nearest-town cutscene, fee 20 gold per leader level, full health and no remaining injuries. No campaign day passes. If gold is insufficient it falls to zero; revival creates no extra debt. With revival disabled, leader digestion ends the campaign immediately. Losing the whole company also ends it.";

    public static string Experience = @"Experience and level-ups

Surviving deployed starters qualify for level 2 after completing a contract. Choose their improvement through Company > Manage > Level up; the existing level-up choices are preserved.

Current cumulative XP thresholds without trait modifiers are 20, 56, 121, 238, 448 and 826 for levels 2 through 7. Requirements rise progressively faster. Trait learning modifiers still apply.

Contract completion awards participants challenge-scaled XP; reserves receive no participation award. Actions against weaker enemies give veterans much less XP. Repeating easy contracts is progressively less useful for development and their small payouts may not cover veteran wages.

All healthy current members gain a little daily sparring XP, active or reserve. Healthy means full lasting HP with no recovering temporary injury after daily recovery; old injuries alone do not prevent sparring. Sparring does not pick a level-up improvement for you. Wages rise with actual level.

These values are provisional and may be tuned after integrated playtesting.";

    public static string Controls = @"Controls

Company map: click adjacent tiles to move, use Enter town for services, Company for roster/gear and End day to advance time. Rest buttons support chosen days. Contract book lists work and exact targets. Use the campaign's Save checkpoint/Choose checkpoint controls for company saves.

Tactical defaults:1 melee attack,2 ranged attack,3 vore; right click for available actions. Use the movement mode/buttons and existing key bindings to select destinations. N selects a unit that can act; Enter ends the tactical turn. WASD/arrows or dragging move the camera, and the mouse wheel zooms. Bindings can differ in Options.

Use the existing AI action/instant-simulation controls to speed combat. Escape opens/closes the ordinary tactical menu; Pause lets you inspect an AI turn.

Legacy F5/F9 and the old save/load screen do not save/load a live company battle. A company battle resumes from its saved deployment checkpoint if interrupted. See Saving and Ironman for the exact boundary.";

    public static string Teams = @"Injuries and recovery

Minor and serious injuries can arise from lasting combat damage and deep attrition. Neither reaching 1 HP nor becoming downed guarantees an injury. Temporary HP protects against injury risk. New injuries appear in the battle log and as outlined orange tactical text.

Minor injuries heal away. Serious injuries impose stronger penalties while recovering, then become weaker permanent old injuries. Old injuries stack and cannot be cured. The leader never retains permanent old injuries, and special revival clears their temporary injuries.

Ordinary days recover a little HP and one injury recovery day. Resting in the field recovers more HP and two injury recovery days; town rest gives the best recovery and three injury recovery days. Surviving contract targets recover normally over elapsed days too. Mana recovers 60% of maximum per campaign day.

Town treatment costs gold per injured member and shortens untreated temporary injuries. It does not instantly refill HP, leaves at least one recovery day, and cannot treat the same injury repeatedly or cure an old injury. Inspect Company > Manage > Injuries / recovery for exact current penalties and recovery.";

    public static string Diplomacy = @"Contracts and reputation

Accept work in a discovered town. Each town has a limited local offer pool that refreshes slowly. Several contracts can be active with equal priority. The current mission type is rout the enemy force.

Ratings are numeric fixed enemy-power budgets, independent of your company size. Rosters are generated from units, levels, traits and equipment within a tight power range. Inspect the generated enemy roster before committing. Taking a contract marks the exact target tile even under fog.

Defeat the target before its generous deadline, currently 30 days after acceptance. Actual digested or fleeing enemies count as defeated; casualties do not respawn on a retry. A surviving target remains available until the deadline, recovering over time.

Completion automatically grants the quoted gold and reputation; no return trip is required. Abandoning and expiry have the same difficulty-scaled reputation penalty and remove the map target. Reputation is a visible company-wide number, changes only through contract outcomes and improves access to contracts/recruits. It does not decay just for waiting.";

    public static string Saving = @"Saving, recovery and Ironman

Company saves use their own checkpoint list. New campaigns do not import old empire saves. Normal companies can manually save and choose retained checkpoint history. A failed load does not replace the current company.

Automatic checkpoints are written after daily updates and committed company changes, with a deployment checkpoint before combat and saved results/revival phases afterward. Travel within the current day is captured by the next checkpoint; normal mode can use Save checkpoint before stopping. Loading does not advance days, repeat healing, add XP or re-award settled contract rewards.

Live tactical-turn saving is not implemented for company battles. Interruption or the company battle menu's return restores deployment, not the last combat turn. This limitation applies to Ironman too.

Ironman is chosen at creation and cannot be toggled later. It resumes only the latest valid checkpoint through the game interface. Older checkpoints are kept as recovery copies; an unreadable newest file can fall back to an earlier valid checkpoint. This is not protection against external file editing.

If a checkpoint write fails, use Retry checkpoint, available even over pending results or decisions. Existing generations are retained; further changes wait for a successful retry.";

    public static string Legal = @"Project and license

This is the VoreWar mercenary-company conversion fork. VoreWar continues darkevilme's Vore War; upstream project attribution remains applicable.

The covered derivative work remains under the applicable GNU GPL version 3 terms in License.txt. Modification dates and source paths are recorded in CHANGES.md. The program is provided without warranty under those terms.

Third-party assets and dependencies retain their own applicable notices and terms. Their inclusion is not a new claim of ownership by this fork. See the distributed source and accompanying notices for the full license texts.

This proof of concept has provisional tuning and documented limitations. Help describes the implemented campaign; further integrated testing and distribution review remain before a release.";

    public static string Traits()
    {
        var sb = new StringBuilder("Retained traits. A trait's presence here does not guarantee it can appear on every species.\n\n");
        foreach (Traits trait in Enum.GetValues(typeof(Traits)))
        {
            if (TraitsMethods.IsRetired(trait) || (int)trait >= 1000) continue;
            sb.AppendLine(trait + " - " + HoveringTooltip.GetTraitData(trait).Replace('\n', ' '));
        }
        return sb.ToString();
    }
    public static string Spells()
    {
        var sb = new StringBuilder("Retained spells use mana. Company mana recovers daily; ordinary magical healing supplies temporary HP.\n\n");
        if (SpellList.SpellDict == null) return sb.ToString();
        foreach (var spell in SpellList.SpellDict.Values.Where(s => !s.IsRetired).OrderBy(s => s.Name))
            sb.AppendLine(spell.Name + "\n" + spell.Description + "\nRange: " + spell.Range.Min + "-" + spell.Range.Max +
                " | Mana: " + spell.ManaCost + "\nTargets: " + string.Join(", ", spell.AcceptibleTargets) + "\n");
        return sb.ToString();
    }
    internal static readonly string[] CompanyTitles = { "Getting started", "World map and days", "Contracts", "Roster and wages", "Equipment", "Battle", "HP and company fate", "Recovery", "Progression", "Saving and Ironman", "Controls", "Project and license" };
    internal static readonly string[] CompanyPages = { GameBasics, StrategyMode, Diplomacy, RecruitMode, Items, TacticalMode, Vore, Teams, Experience, Saving, Controls, Legal };
}
