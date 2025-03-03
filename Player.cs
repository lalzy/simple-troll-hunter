using System.Collections;
using System.Security.Cryptography;

class Player : Creature{
    public enum Classes{
        custom = 0,
        knight = 1,
        archer = 2,
    }
    public Classes Class;
    public Inventory Inventory = new Inventory();
    public bool IsBlocking = false;
    
    /// <summary>
    /// Damage roll if blocking was a full action.
    /// </summary>
    public int BlockRoll = 0;
    public  Player(int hp, int minDamage, int maxDamage, int rangedMin = 1, int rangedmax = 5){
        this.MinDamage = minDamage;
        this.MaxDamage = maxDamage;
        Class = Classes.custom;
        this.SetHealth(hp);
    }

    /// <summary>
    /// Gets the max health of the shield.
    /// </summary>
    /// <returns>The max-health of the shield</returns>
    public int GetShieldHealthBase(){
        return Inventory.GetItem("shield").MaxAmount;
    }

    /// <summary>
    /// Repairs the shield if it's damaged.
    /// </summary>
    private void RepairShield(){
        if(Inventory.GetItem("shield").Amount < Inventory.GetItem("shield").MaxAmount
        && Inventory.GetItem("tools").Amount > 0){
            Inventory.GetItem("tools").Amount--;
            Inventory.GetItem("shield").Amount++;
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
                Inventory.GetItem("food").UseItem();
                Console.WriteLine($"You ate food, food left: {Inventory.GetItem("food").Amount}");
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
        if(Inventory.GetItem("food").Amount > 0){
            if(Display.Rooms.RestMenu(Inventory.GetItem("food").Amount)){
                rested = RestInput();
            }
        }else{
            Display.Rooms.NoFoodText();
        }
        return rested;
    }

    /// <summary>
    /// Set's the amount of times you can block.
    /// </summary>
    /// <param name="health">The shield's block-count you want</param>
    public void SetShieldHealth(int health = 3){
        Inventory.GetItem("shield").Amount = health;
        Inventory.GetItem("shield").MaxAmount = health;
    }

    /// <summary>
    /// Return if player was surpised or not. Chance is aquired from the Globals Surprised chance, which gets defined in Game.SelectDifficulty()
    /// </summary>
    public void CheckSurprised(){
        // Torch makes you immune to surprise.
        if(Inventory.GetItem("torch").Amount == 0){
            if (new Random().Next(1, 100) < Globals.SurprisedChance){
                this.Stun(); // stuns for 1 turn.
                Display.SurprisedMessage();
            }
        }
    }

    /// <summary>
    /// Calculate how much damage we do, which also depends on if we blocked or not.
    /// </summary>
    /// <returns>The damage dealt</returns>
    public int CalcDamage (){
            int damage = this.Attack();
            Display.PlayerAttack(damage);
            if (this.BlockRoll > damage){
                damage = this.BlockRoll;
            }
            this.BlockRoll = 0; // reset block-roll
            return damage;
    }

    /// <summary>
    /// Print a fitting message based on shield condition, and returns if you can block or not.
    /// </summary>
    /// <returns>Wether player can block or not.</returns>
    public bool CanBlock(){
        Display.ShieldBlockMessage();
        return Inventory.GetItem("shield").Amount > 0;
    }

    /// <summary>
    /// Subtrack enemy-damage from the shield's health. Higher damage = does more damage to the shield.
    /// </summary>
    /// <param name="enemyDamage">How much damage the enemy deals</param>
    public void Block(int enemyDamage){
        Display.BlockedEnemyMessage();
        Inventory.GetItem("shield").Amount -= (int) Math.Ceiling(enemyDamage / 10.0); 
    }

    /// <summary>
    /// Returns the current health of the shield.
    /// </summary>
    /// <returns>Current health of the shield</returns>
    public int GetShieldHealth(){
        return Inventory.GetItem("shield").Amount;
    }
    



}