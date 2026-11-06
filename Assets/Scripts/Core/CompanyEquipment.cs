// Added by the VoreWar mercenary-company conversion fork.
// 2026-11-05: Make basic protection affordable while preserving premium investment and combat values (spec 38 phase B).
// 2026-11-05: Calibrate light armor evasion from paired native battle measurements (spec 38 phase A).
// 2026-11-01: Nine-slot company catalog, stable gear ownership and safe stat changes (spec 08).
// 2026-11-04: Armor deflection/evasion, item comparisons and merchant categories (spec 34 slice 4).
// 2026-11-04: Three reputation-unlocked gear tiers, investment prices and recruitment valuation (spec 34 slice 5).
// 2026-11-01: Define enemy-only weak slug ranged gear without adding it to company shops (spec 09).
// 2026-11-01: Provide safe body-part armor protection for both company and original Pure Tactical inventories (spec 11).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
// 2026-11-03: Implement four active stats and agreed combat rules (spec 29).
using System;
using System.Collections.Generic;
using System.Linq;

internal enum CompanyEmployment { Candidate, Employed, Former, Unavailable }
internal enum CompanySlot { Helmet, Torso, Legs, Arms, Accessory, Melee, Ranged, Spell1, Spell2 }
internal static class CompanyEquipment
{
    internal sealed class Definition
    {
        public string Id, Name;
        public CompanySlot Slot;
        public int Cost, Protection, ArmorClass, Tier = 1;
        public float Evasion;
        public bool EnemyOnly;
        public Func<Item> Create;
    }
    // Whitelist intentionally omits potions, healing equipment and retired ability grants.
    internal static readonly Definition[] Catalog = BuildCatalog().ToArray();
    static IEnumerable<Definition> BuildCatalog()
    {
        // Stable legacy IDs/stats remain valid for already serialized equipment.
        yield return Weapon("weapon.mace", "Mace", 70, false, 4, 1);
        yield return Weapon("weapon.axe", "Axe", 200, false, 8, 1, 2);
        yield return Weapon("weapon.bow", "Simple bow", 90, true, 4, 5);
        yield return Weapon("weapon.compound", "Compound bow", 240, true, 6, 7, 2);
        yield return Armor("armor.helmet", "Helmet", CompanySlot.Helmet, 12, Stat.Agility, 1, 1);
        yield return Armor("armor.torso", "Torso armor", CompanySlot.Torso, 25, Stat.Strength, 2, 2);
        yield return Armor("armor.legs", "Leg armor", CompanySlot.Legs, 18, Stat.Strength, 1, 0, .06f);
        yield return Armor("armor.arms", "Arm armor", CompanySlot.Arms, 14, Stat.Strength, 1, 1);
        yield return Armor("accessory.charm", "Insight charm", CompanySlot.Accessory, 60, Stat.Intelligence, 1);
        yield return new Definition { Id = "natural.slug", Name = "Slug slime", Slot = CompanySlot.Ranged, EnemyOnly = true,
            Create = () => new Weapon("Slug slime", "Weak natural ranged attack", 0, 0, 2, 5, 1, true) };
        yield return Book("spell.bolt", "Power Bolt", SpellTypes.PowerBolt, 30);
        yield return Book("spell.valor", "Valor", SpellTypes.Valor, 60);
        yield return Book("spell.shield", "Shield", SpellTypes.Shield, 60);
        yield return Weapon("weapon.cleaver", "Cleaver", 100, false, 6, 1);
        yield return Weapon("weapon.hunting", "Hunting bow", 120, true, 6, 4);
        yield return Weapon("weapon.mace.2", "Fine mace", 180, false, 7, 1, 2, 1.4f);
        yield return Weapon("weapon.bow.2", "Fine bow", 230, true, 7, 6, 2, 1.4f);
        yield return Weapon("weapon.warbow.2", "War bow", 260, true, 10, 6, 2);
        yield return Weapon("weapon.mace.3", "Masterwork mace", 750, false, 10, 1, 3, 1.55f);
        yield return Weapon("weapon.axe.3", "Masterwork axe", 900, false, 14, 1, 3);
        yield return Weapon("weapon.bow.3", "Masterwork bow", 850, true, 10, 7, 3, 1.55f);
        yield return Weapon("weapon.warbow.3", "Masterwork war bow", 1000, true, 14, 8, 3);
        var labels = new[] { "Helmet", "Torso armor", "Leg armor", "Arm armor" };
        var keys = new[] { "helmet", "torso", "legs", "arms" };
        var heavyPrices = new[] { new[] { 12, 25, 18, 14 }, new[] { 120, 230, 140, 140 }, new[] { 450, 900, 550, 550 } };
        var lightPrices = new[] { new[] { 14, 24, 18, 12 }, new[] { 150, 240, 190, 130 }, new[] { 550, 950, 750, 500 } };
        // Helmet, torso, legs, arms. Full sets total 16%, 28% and 40%.
        var evasions = new[] { new[] { .03f, .04f, .06f, .03f }, new[] { .06f, .08f, .08f, .06f }, new[] { .08f, .10f, .14f, .08f } };
        for (int tier = 1; tier <= 3; tier++) for (int slot = 0; slot < 4; slot++)
        {
            string quality = tier == 1 ? "" : tier == 2 ? "Fine " : "Masterwork ";
            var stat = slot == 0 ? Stat.Agility : Stat.Strength;
            int bonus = slot == 1 ? 2 : 1;
            // Preserve the existing heavy helmet/torso/arms and evasive leg armor as their tier-I branch.
            if (tier > 1 || slot == 2)
                yield return Armor("armor." + keys[slot] + ".heavy." + tier, quality + "Guarded " + labels[slot].ToLowerInvariant(),
                    (CompanySlot)slot, heavyPrices[tier - 1][slot], stat, bonus, (slot == 1 ? 2 : 1) * tier, 0, tier);
            if (tier > 1 || slot != 2)
                yield return Armor("armor." + keys[slot] + ".light." + tier, quality + "Light " + labels[slot].ToLowerInvariant(),
                    (CompanySlot)slot, lightPrices[tier - 1][slot], stat, slot == 2 ? 1 : 0, 0, evasions[tier - 1][slot], tier);
        }
        foreach (var stat in WarbandStats.Active) for (int tier = 1; tier <= 3; tier++)
        {
            if (stat == Stat.Intelligence && tier == 1) continue;
            string quality = tier == 1 ? "" : tier == 2 ? "Fine " : "Masterwork ";
            yield return Armor("accessory." + WarbandStats.ShortName(stat).ToLowerInvariant() + "." + tier,
                quality + WarbandStats.Name(stat) + " charm", CompanySlot.Accessory, new[] { 60, 220, 800 }[tier - 1], stat, new[] { 1, 2, 4 }[tier - 1], tier: tier);
        }
    }
    static ItemRepository repository;
    internal static ItemRepository Repository { get { return repository ?? (repository = new ItemRepository()); } }
    static Definition Weapon(string id, string name, int cost, bool ranged, int damage, int range, int tier = 1, float accuracy = 1)
    { return new Definition { Id = id, Name = name, Cost = cost, Tier = tier, Slot = ranged ? CompanySlot.Ranged : CompanySlot.Melee,
        Create = () => new Weapon(name, "Company weapon", cost, id.Contains("axe") || id.Contains("cleaver") ? 2 : id.Contains("compound") ? 6 : ranged ? 4 : 0, damage, range,
            id == "weapon.mace" || id == "weapon.bow" ? 1.25f : accuracy) }; }
    static Definition Armor(string id, string name, CompanySlot slot, int cost, Stat stat, int bonus, int armorClass = 0, float evasion = 0, int tier = 1)
    { return new Definition { Id = id, Name = name, Cost = cost, Tier = tier, Slot = slot, Protection = slot == CompanySlot.Accessory ? 0 : 1, ArmorClass = armorClass, Evasion = evasion,
        Create = () => new Accessory(name, bonus == 0 ? "Armor" : "+" + bonus + " " + WarbandStats.ShortName(stat), cost, (int)stat, bonus) }; }
    static Definition Book(string id, string name, SpellTypes spell, int cost)
    { return new Definition { Id = id, Name = name, Slot = CompanySlot.Spell1, Cost = cost,
        Create = () => new SpellBook(name, "Company spell", cost, 1, spell) }; }
    internal static Definition Find(string id) { return Catalog.FirstOrDefault(d => d.Id == id); }
    internal static int RequiredReputation(int tier) { return new[] { 0, 10, 25 }[Math.Max(1, Math.Min(3, tier)) - 1]; }
    internal static int UnlockedTier(int reputation) { return reputation >= RequiredReputation(3) ? 3 : reputation >= RequiredReputation(2) ? 2 : 1; }
    internal static IEnumerable<Definition> Available(CompanyCampaign state)
    { return Catalog.Where(d => !d.EnemyOnly && state.Reputation >= RequiredReputation(d.Tier)).OrderBy(d => d.Tier).ThenBy(d => d.Cost).ThenBy(d => d.Id, StringComparer.Ordinal); }
    internal static int Value(Item item) { var d = item == null ? null : Find(item.CompanyDefinitionId); return d == null || d.EnemyOnly ? 0 : d.Cost; }
    internal static int GearPrice(Unit unit) { return checked(unit.Items.Where(item => item != null).Sum(Value)); }
    internal static Definition Generated(CompanyCampaign state, CompanySlot slot, int level)
    {
        int tier = level >= 10 ? 3 : level >= 5 ? 2 : 1;
        var choices = Catalog.Where(d => !d.EnemyOnly && d.Slot == slot && d.Tier == tier).ToArray();
        return choices[state.Next(choices.Length)];
    }
    internal static string SlotName(CompanySlot slot)
    { return new[] { "Helmet", "Torso Armor", "Leg Armor", "Arm Armor", "Accessory", "Melee Weapon", "Ranged Weapon", "Spell 1", "Spell 2" }[(int)slot]; }
    internal static string Summary(Item item)
    {
        if (item == null) return "Empty";
        var weapon = item as Weapon;
        if (weapon != null) return "Dmg " + weapon.Damage + " | Range " + weapon.Range + " | Accuracy rating " + weapon.AccuracyModifier.ToString("0.##") + "x";
        var d = Find(item.CompanyDefinitionId);
        if (d != null && item is Accessory) return item.Description + (d.ArmorClass > 0 ? " | AC +" + d.ArmorClass : "") +
            (d.Evasion > 0 ? " | Evasion +" + (d.Evasion * 100).ToString("0") + "%" : "");
        var book = item as SpellBook;
        if (book != null)
        {
            Spell spell;
            if (SpellList.SpellDict.TryGetValue(book.ContainedSpell, out spell))
                return spell.Description + " | Mana " + spell.ManaCost + " | Range " + spell.Range.Min + "–" + spell.Range.Max;
        }
        return item.Description;
    }
    static IEnumerable<Definition> WornArmor(Unit unit)
    {
        if (unit.Items == null) yield break;
        for (int slot = 0; slot < 4; slot++)
        {
            if (unit.CompanyLoadout)
            {
                var item = slot < unit.Items.Length ? unit.Items[slot] : null;
                var d = item == null ? null : Find(item.CompanyDefinitionId);
                if (d != null && (int)d.Slot == slot) yield return d;
            }
            else if (Protection(unit, (CompanySlot)slot) > 0)
                yield return Catalog.First(d => (int)d.Slot == slot && !d.EnemyOnly);
        }
    }
    internal static int ArmorClass(Unit unit) { return WornArmor(unit).Sum(d => Math.Max(0, d.ArmorClass)); }
    internal static float Evasion(Unit unit) { return Math.Min(1, WornArmor(unit).Sum(d => Math.Max(0, d.Evasion))); }
    internal static int Deflect(Unit unit, int damage)
    {
        int armor = ArmorClass(unit);
        return Math.Max(1, damage - (armor > 0 ? State.Rand.Next(armor + 1) : 0));
    }
    // Exact average of max(1, damage - roll), without consuming combat RNG.
    internal static double ExpectedAfterArmor(int damage, int armor, int barrier = 0)
    {
        damage = Math.Max(1, damage); armor = Math.Max(0, armor); barrier = Math.Max(0, barrier);
        if (barrier == 0)
        {
            int useful = Math.Min(armor, damage - 1);
            return ((double)damage * (useful + 1) - (double)useful * (useful + 1) / 2 + armor - useful) / (armor + 1.0);
        }
        int positive = Math.Min(armor + 1, Math.Max(0, damage - barrier));
        return ((double)(damage - barrier) * positive - (double)positive * (positive - 1) / 2) / (armor + 1.0);
    }
    internal static Item Create(string id)
    {
        var d = Find(id); if (d == null) throw new InvalidOperationException("Unknown company gear definition.");
        Item item = d.Create(); item.CompanyDefinitionId = id; item.CompanyInstanceId = Guid.NewGuid().ToString("N"); return item;
    }
    internal static bool CanPlace(Item item, int slot)
    {
        if (slot < 0 || slot >= 9) return false;
        if (item == null) return true;
        var d = Find(item.CompanyDefinitionId);
        return d != null && ((int)d.Slot == slot || (d.Slot == CompanySlot.Spell1 && slot == (int)CompanySlot.Spell2));
    }
    internal static int Protection(Unit unit, CompanySlot slot)
    {
        if (unit.Items == null) return 0;
        if (unit.CompanyLoadout)
        {
            var item = (int)slot < unit.Items.Length ? unit.Items[(int)slot] : null;
            var d = item == null ? null : Find(item.CompanyDefinitionId);
            return d == null ? 0 : d.Protection;
        }
        // Pure Tactical retains the original two-item inventory; do not index it as nine slots.
        return unit.Items.Any(i => i != null && (slot == CompanySlot.Helmet ? i.Name == "Helmet" :
            slot == CompanySlot.Torso ? i.Name == "Body Armor" : slot == CompanySlot.Legs ? i.Name == "Shoes" :
            slot == CompanySlot.Arms && (i.Name == "Gauntlet" || i.Name == "Gloves"))) ? 1 : 0;
    }
    internal static int Power(Item item)
    { var d = item == null ? null : Find(item.CompanyDefinitionId); return d == null ? 0 : item is Weapon ? ((Weapon)item).Damage * (((Weapon)item).Range > 1 ? 2 : 1) : item is Accessory ? ((Accessory)item).StatBonus * 2 + d.Protection : 6; } // Provisional combat input for09, independent of price.
    internal static void Equip(CompanyCampaign state, CompanyCharacter who, Item item, int slot)
    {
        if (state.InBattle || state.UpdatingDay || !state.Members.Any(m => m.CharacterId == who.Id)) throw new InvalidOperationException("Equipment can only change outside battle for current members.");
        if (!CanPlace(item, slot) || (item != null && !state.Inventory.Contains(item))) throw new InvalidOperationException("Invalid equipment selection.");
        if (item != null && state.Characters.Any(c => c.Unit.Items.Contains(item))) throw new InvalidOperationException("That item is already equipped.");
        var unit = who.Unit; Item previous = unit.Items[slot]; int hp = unit.Health, maximum = unit.MaxHealth;
        int uses = unit.ItemUses[slot], cooldown = unit.ItemCooldowns[slot];
        unit.SetItem(item, slot);
        int newHealth = hp + unit.MaxHealth - maximum;
        if (newHealth < 1)
        {
            unit.SetItem(previous, slot); unit.Health = hp; unit.ItemUses[slot] = uses; unit.ItemCooldowns[slot] = cooldown;
            throw new InvalidOperationException("Recover more health before removing this HP-granting item.");
        }
        unit.Health = Math.Min(unit.MaxHealth, newHealth);
        unit.UpdateSpells();
    }
    internal static void Buy(CompanyCampaign state, string id)
    {
        if (state.InBattle || state.Busy || !state.Towns.Any(t => t.Tile == state.Position)) throw new InvalidOperationException("Visit a town to shop.");
        var d = Find(id); if (d == null || !Available(state).Contains(d)) throw new InvalidOperationException("That equipment is not available at the company's reputation.");
        if (state.Gold < d.Cost) throw new InvalidOperationException("Not enough gold.");
        Item item = Create(id); state.Inventory.Add(item); state.Gold -= d.Cost;
    }
    internal static void Sell(CompanyCampaign state, Item item)
    {
        if (state.InBattle || state.Busy || !state.Towns.Any(t => t.Tile == state.Position) || !state.Inventory.Contains(item) || state.Characters.Any(c => c.Unit.Items.Contains(item)))
            throw new InvalidOperationException("Only unequipped company items can be sold.");
        var d = Find(item.CompanyDefinitionId); if (d == null) throw new InvalidOperationException("Unknown gear.");
        state.Inventory.Remove(item); state.Gold += d.Cost / 2; state.TotalGoldEarned += d.Cost / 2;
    }
}
