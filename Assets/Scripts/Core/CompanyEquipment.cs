// Added by the VoreWar mercenary-company conversion fork.
// 2026-11-01: Nine-slot company catalog, stable gear ownership and safe stat changes (spec 08).
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
        public int Cost, Protection;
        public bool EnemyOnly;
        public Func<Item> Create;
    }
    // Whitelist intentionally omits potions, healing equipment and retired ability grants.
    internal static readonly Definition[] Catalog =
    {
        Weapon("weapon.mace", "Mace", 4, false, 4, 1),
        Weapon("weapon.axe", "Axe", 12, false, 8, 1),
        Weapon("weapon.bow", "Simple bow", 6, true, 4, 5),
        Weapon("weapon.compound", "Compound bow", 12, true, 6, 7),
        Armor("armor.helmet", "Helmet", CompanySlot.Helmet, 4, Stat.Agility, 1),
        Armor("armor.torso", "Torso armor", CompanySlot.Torso, 8, Stat.Strength, 2),
        Armor("armor.legs", "Leg armor", CompanySlot.Legs, 6, Stat.Strength, 1),
        Armor("armor.arms", "Arm armor", CompanySlot.Arms, 6, Stat.Strength, 1),
        Armor("accessory.charm", "Insight charm", CompanySlot.Accessory, 4, Stat.Intelligence, 1),
        new Definition { Id = "natural.slug", Name = "Slug slime", Slot = CompanySlot.Ranged, EnemyOnly = true,
            Create = () => new Weapon("Slug slime", "Weak natural ranged attack", 0, 0, 2, 5, 1, true) },
        Book("spell.bolt", "Power Bolt", SpellTypes.PowerBolt, 8),
        Book("spell.valor", "Valor", SpellTypes.Valor, 8),
        Book("spell.shield", "Shield", SpellTypes.Shield, 8)
    };
    static ItemRepository repository;
    internal static ItemRepository Repository { get { return repository ?? (repository = new ItemRepository()); } }
    static Definition Weapon(string id, string name, int cost, bool ranged, int damage, int range)
    { return new Definition { Id = id, Name = name, Cost = cost, Slot = ranged ? CompanySlot.Ranged : CompanySlot.Melee,
        Create = () => new Weapon(name, "Company weapon", cost, id == "weapon.axe" ? 2 : id == "weapon.compound" ? 6 : ranged ? 4 : 0, damage, range, id == "weapon.mace" || id == "weapon.bow" ? 1.25f : 1) }; }
    static Definition Armor(string id, string name, CompanySlot slot, int cost, Stat stat, int bonus)
    { return new Definition { Id = id, Name = name, Cost = cost, Slot = slot, Protection = slot == CompanySlot.Accessory ? 0 : 1,
        Create = () => new Accessory(name, "+" + bonus + " " + stat, cost, (int)stat, bonus) }; }
    static Definition Book(string id, string name, SpellTypes spell, int cost)
    { return new Definition { Id = id, Name = name, Slot = CompanySlot.Spell1, Cost = cost,
        Create = () => new SpellBook(name, "Company spell", cost, 1, spell) }; }
    internal static Definition Find(string id) { return Catalog.FirstOrDefault(d => d.Id == id); }
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
        var d = Find(id); if (d == null || d.EnemyOnly || state.Gold < d.Cost) throw new InvalidOperationException("Not enough gold.");
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
