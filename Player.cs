using System.Collections;
using System.Security.Cryptography;

class Player : Creature{
    public  Player(int hp){
        Inventory = new Inventory();
        Perks = new List<PerksEnum>();
        Equipment = new Equipment();
        Spells = new List<Spell>();
        // Class = Classes.custom;
        this.SetHealth(hp);
    }
    // Make classes have unique abilities, and move some things, like spells, from items to here.
    public enum PerksEnum{
        ChargeUp = 0,
        CanUseShield = 1, // Will grant + points rather than subtract for custom.
        blacksmithing = 2,
        BowMaster = 3, // Jump back after an arrow is shot
    }
    public List<PerksEnum> Perks;
    public enum Classes{
        custom = 0,
        knight = 1,
        archer = 2,
    }
    public List<Spell> Spells;

    // public bool ExploreSpell(Spell.ValidSpells spell){
    //     switch(spell){
    //         case Spell.ValidSpells.Light:
    //             return true;
    //     }
    //     return false;
    // }

    /// <summary>
    /// Get the total count of spells you have access too.
    /// </summary>
    /// <returns>returns the total amount of spells available</returns>
    public int SpellCount(bool exploreOnly = false){
        int count = 0;
        foreach(Spell spell in Spells){
            // if(exploreOnly){
            //     if(ExploreSpell(spell.SpellType)){
            //         count += spell.Amount;
            //     }else{
            //         continue;
            //     }
            // }else{
                count += spell.Amount;
            // }
        }
        return count;
    }

    public void RestoreSpells(){
        foreach(Spell spell in Spells){
            spell.Restore();
        }
    }

    public Spell? GetSpell(Spell.ValidSpells spellToCheck){
        return (from spell in Spells where spell.SpellType == spellToCheck select spell).FirstOrDefault();
    }

    //public Classes Class;
    public Inventory Inventory;
    public Equipment Equipment;
    public bool IsBlocking = false;
    public bool skipped;
    
    /// <summary>
    /// Additional Damage
    /// </summary>
    public int ExtraDamage = 0;

    /// <summary>
    /// Repairs the shield if it's damaged.
    /// </summary>
    private void RepairShield(){
        Weapon? shield = Equipment.OffHand;
        if(shield != null && shield.Type == Weapon.WeaponType.shield &&
        shield.MinAttribute < shield.MaxAttribute && Inventory.UseItem(Inventory.Items.tools)){
            shield.MinAttribute++;
        }
    }

    /// <summary>
    /// Handles the player-input to choose what to do.
    /// </summary>
    /// <returns>Returns true if we did a valid rest, otherwise returns false (for text feedback to player only)</returns>
    private bool RestInput(){
        string? input = Console.ReadLine();
        if (input == null) Environment.Exit(-1);
        else if (input == "") input = " ";

        // Handle inputing.
        switch(char.ToLower(input[0])){
            case 'h':
                this.Heal((int) Math.Round(this.BaseHealth * .20));
                Display.Rooms.Resting();
                Inventory.GetItem(Inventory.Items.food).UseItem();
                Console.WriteLine($"You ate food, food left: {Inventory.GetItem(Inventory.Items.food).Amount}");
                return true;
            case 'm':
                Display.Rooms.MagicRest();
                RestoreSpells();
                return true;
            case 'r':
            case 'f':
                Display.Rooms.FixShield();
                RepairShield();
                return true;
            default:
                Display.Rooms.InvalidRestSelection();
                return true;
        }
    }

    /// <summary>
    /// Player rest mechanic
    /// </summary>
    public bool Rest(){
        bool rested = false;
        if(Inventory.GetItem(Inventory.Items.food).Amount > 0){
            if(Display.Rooms.RestMenu(Inventory.GetItem(Inventory.Items.food).Amount)){
                rested = RestInput();
            }
        }else{
            Display.Rooms.NoFoodText();
        }
        return rested;
    }

    /// <summary>
    /// Return if player was surpised or not. Chance is aquired from the Globals Surprised chance, which gets defined in Game.SelectDifficulty()
    /// </summary>
    public void CheckSurprised(){
        // Torch makes you immune to surprise.
        if(Inventory.GetItem(Inventory.Items.torch).Amount > 0) return;
        else{
            if (new Random().Next(1, 100) < Globals.SurprisedChance){
                this.Stun(); // stuns for 1 turn.
                Display.SurprisedMessage();
            }
        }
    }

    public Weapon? GetMainWeapon(){
        Weapon? weapon = Equipment.MainHand;
        if(weapon == null){
            weapon = Equipment.OffHand;
        }
        // Bow should not be used as main weapon
        if(weapon == null || weapon.NotDamageWeapon() || weapon.Type == Weapon.WeaponType.bow){
            return null;
        }else{
            return weapon;
        }
    }

    /// <summary>
    /// Get the damage the creature will deal.
    /// </summary>
    /// <returns>Damage number rolled</returns>
    public int Attack(Weapon? weapon = null){
        if(weapon == null){
            return 0;
        }else if (weapon.NotDamageWeapon()){
            return 0;
        }else{
            return new Random().Next(weapon.MinAttribute, weapon.MaxAttribute);
        }
    }
    /// <summary>
    /// Calculate how much damage we do, which also depends on if we blocked or not.
    /// </summary>
    /// <returns>The damage dealt</returns>
    public int CalcDamage (){
        int damage = this.Attack();
        damage += ExtraDamage;
        ExtraDamage = 0;
        
        Display.PlayerAttack(damage);
        return damage;
    }

    public bool ShieldDamaged(){
        if(HasShield() && Equipment.OffHand.MinAttribute < Equipment.OffHand.MaxAttribute){
            return true;
        }else{
            return false;
        }
    }

    /// <summary>
    /// Print a fitting message based on shield condition, and returns if you can block or not.
    /// </summary>
    /// <returns>Wether player can block or not.</returns>
    public bool CanBlock(){
        Display.ShieldBlockMessage();
        if(HasShield() && Equipment.OffHand.MinAttribute > 0){
            return true;
        }else{
            return false;
        }
    }

    public bool HasBow(){
        if(Equipment.OffHand != null && Equipment.OffHand.Type == Weapon.WeaponType.bow){
            return true;
        }else{
            return false;
        }
    }

    public bool HasShield(){
        if (Equipment.OffHand != null && Equipment.OffHand.Type == Weapon.WeaponType.shield){
            return true;
        }else{
            return false;
        }
    }

    /// <summary>
    /// Subtrack enemy-damage from the shield's health. Higher damage = does more damage to the shield.
    /// </summary>
    /// <param name="enemyDamage">How much damage the enemy deals</param>
    public void Block(int enemyDamage){
        Display.BlockedEnemyMessage();
        if(HasShield()){
            Equipment.OffHand.MinAttribute -= (int) Math.Ceiling(enemyDamage / 10.0);
        }
    }
}