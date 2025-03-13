using System.Collections;
using System.Security.Cryptography;

class Player : Creature{
    public enum PerksEnum{
        ChargeUp = 0,
        CanUseShield = 1, // Will grant + points rather than subtract for custom.
        blacksmithing = 2,
        BowMaster = 3, // Jump back after an arrow is shot
        Meditation = 4, // Recover spells on rest.
        CanFindBow = 5,
    }
    public List<PerksEnum> Perks;
    public List<Spell> Spells;
    //public Classes Class;
    public Inventory Inventory;
    public Equipment Equipment;
    public bool IsBlocking = false;
    public bool skipped;
    public bool AmbushImmune;
    public int AmbushImmuneDuration = 0;
    
    /// <summary>
    /// Additional Damage
    /// </summary>
    public int ExtraDamage = 0;

    public  Player(int hp){
        Inventory = new Inventory();
        Perks = new List<PerksEnum>();
        Equipment = new Equipment();
        Spells = new List<Spell>();
        // Class = Classes.custom;
        this.SetHealth(hp);
    }

    public void ProgressAmbushImmunity(){
        Display.AmbushImmuneProgress(AmbushImmuneDuration);
        if (AmbushImmune && AmbushImmuneDuration > 0){
            AmbushImmuneDuration--;
        }else{
            AmbushImmune = false;
        }
    }

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
            if(exploreOnly){
                count += spell.ExploreSpell ? spell.Amount : 0;
            }else{
                count += spell.Amount;
            }
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
        string input = Display.GetInput();

        // Handle inputing.
        switch(char.ToLower(input[0])){
            case 'h':
                this.Heal((int) Math.Round(this.BaseHealth * .20));
                Display.Rooms.Resting();
                Inventory.GetItem(Inventory.Items.food).UseItem();
                Console.WriteLine($"You ate food, food left: {Inventory.GetItem(Inventory.Items.food).Amount}");
                return true;
            case 'm':
                if(Perks.Contains(PerksEnum.Meditation)){
                    Display.Rooms.MagicRest();
                    RestoreSpells();
                    return true;
                }else{
                    Display.Rooms.MaicRestFail();
                    return false;
                }
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
    public bool Rest(bool showMagic = false){
        bool rested = false;
        if(showMagic){
            Display.MagicMenuEnterText();
        }
        if(Inventory.GetItem(Inventory.Items.food).Amount > 0){
            if(Display.Rooms.RestMenu(Inventory.GetItem(Inventory.Items.food).Amount)){
                rested = RestInput();
            }
        }else{
            Display.Rooms.NoFoodText();
            Display.PressAnyKey();
            Console.ReadKey();
        }
        return rested;
    }

    /// <summary>
    /// Return if player was surpised or not. Chance is aquired from the Globals Surprised chance, which gets defined in Game.SelectDifficulty()
    /// </summary>
    public void CheckSurprised(){
        // Torch makes you immune to surprise.
        if(Inventory.GetItem(Inventory.Items.torch).Amount > 0 || AmbushImmune) return;
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

    private int GetBaseDamage(Weapon? weapon = null){
        if(weapon == null){
            return 0;
        }else if (weapon.NotDamageWeapon()){
            return 0;
        }else{
            return new Random().Next(weapon.MinAttribute, weapon.MaxAttribute);
        }
    }

    /// <summary>
    /// Get the damage the creature will deal.
    /// </summary>
    /// <returns>Damage number rolled</returns>
    public int Attack(Weapon? weapon = null){
        int damage = GetBaseDamage(weapon);
        damage += ExtraDamage;
        ExtraDamage = 0; // Reset extraDamage
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

    public bool CombatMagic(int choice, Enemy enemy){
        switch(choice){
            case (int) Spell.ValidSpells.Fireball + 1: // Fireball
                Spell? fireball = this.GetSpell(Spell.ValidSpells.Fireball);
                if(fireball != null && fireball.Use()){
                    enemy.TakeDamage(20);
                    if(enemy.IsDead()){
                        enemy.DeathText = $"You burned the {enemy.Name} to cinders!";
                    }else{
                        Console.WriteLine($"You cast a fireball at the {enemy.Name}.");
                    }
                }else{
                    Console.WriteLine("You have no fireball slots left!");
                }
            return true;
            case (int) Spell.ValidSpells.Freeze + 1: // Freeze
                Spell? freeze = this.GetSpell(Spell.ValidSpells.Freeze);
                if(freeze != null && freeze.Use()){
                    enemy.Stun(2);
                    enemy.StunCause = Display.StunCause.freeze;
                    Console.WriteLine("You freeze the enemy in a block of ice!");
                }else{
                    Console.WriteLine("You have no freeze scrolls");
                }
            return true;
            case (int) Spell.ValidSpells.ShieldSpell + 1: // ShieldSpell
            return true;
        }
        return false;
    }

    public bool ExploreMagic(int choice){
        switch(choice){
            case (int) Spell.ValidSpells.light + 1:
                Spell? light = this.GetSpell(Spell.ValidSpells.light);
                if(light != null && light.Use()){
                    Console.WriteLine("You illuminate the area");
                    this.AmbushImmune = true;
                    this.AmbushImmuneDuration = 5;
                }else{
                    Console.WriteLine("You have no more light spell slots.");
                }
                return true;
        }
        return false;
    }

    public enum MagicMenusToShow{
        combatOnly = 0,
        exploreOnly = 1,
        both = 2,
    }

    public void Magic(MagicMenusToShow magicMenuToShow = MagicMenusToShow.combatOnly){
        Enemy? enemy = Enemy.CurrentEnemy;
        Player player = Globals.Player;

        if (magicMenuToShow == MagicMenusToShow.combatOnly || magicMenuToShow == MagicMenusToShow.both){
            if(player.SpellCount() > 0){
                Display.MagicMenu();
                int.TryParse(Display.GetInput(), out int choice);
                Console.Clear();
                if(this.CombatMagic(choice, enemy)) return;
            }
        }
        else if(magicMenuToShow == MagicMenusToShow.exploreOnly || magicMenuToShow == MagicMenusToShow.both){
            if(player.SpellCount(true) > 0){
                Display.MagicMenu(true);
                int.TryParse(Display.GetInput(), out int choice);
                Console.Clear();
                if(this.ExploreMagic(choice)) return;
            }
        }
        Console.WriteLine("Invalid Selection");
    }
}