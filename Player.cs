using System.Collections;
using System.Security.Cryptography;

class Player : Creature{
    /// <summary>
    /// Not used, yet.
    /// Idea is to keep the string-name of the item(like food, torch, shield) followed by it's 'amount' or 'uses'.
    /// </summary>
    private Inventory inventory = new Inventory();

    public bool IsBlocking = false;
    
    /// <summary>
    /// Damage roll if blocking was a full action.
    /// </summary>
    public int BlockRoll = 0;
    public bool Surprised = false;
    public  Player(int hp, int minDamage, int maxDamage){
        this.MinDamage = minDamage;
        this.MaxDamage = maxDamage;
        this.SetHealth(hp);
        inventory.AddItem("food");
        inventory.AddItem("shield");
    }

    /// <summary>
    /// Gets the max health of the shield.
    /// </summary>
    /// <returns>The max-health of the shield</returns>
    public int GetShieldHealthBase(){
        return inventory.GetItem("shield").MaxAmount;
    }

    /// <summary>
    /// Repairs the shield if it's damaged.
    /// </summary>
    private void RepairShield(){
        if(inventory.GetItem("shield").Amount < inventory.GetItem("shield").MaxAmount){
            inventory.GetItem("shield").Amount++;
        }

    }

    /// <summary>
    /// Handles the player-input to choose what to do.
    /// </summary>
    /// <returns>Returns true if we did a valid rest, otherwise returns false (for text feedback to player only)</returns>
    private bool restInput(){
        string? input = Console.ReadLine();
        if (input == null) Environment.Exit(-1);
        else if (input == "") input = " ";

        // Handle inputing.
        switch(char.ToLower(input[0])){
            case 'h':
                this.Heal((int) Math.Round(this.BaseHealth * .20));
                return true;
            case 'r':
            case 'f':
                RepairShield();
                return true;
            default:
                return false;
        }
    }

    /// <summary>
    /// Player rest mechanic
    /// </summary>
    public void Rest(){
        if(inventory.GetItem("food").Amount > 0){
            bool validRest = false;
            if(Display.Rooms.RestMenu(inventory.GetItem("food").Amount)){
                validRest = restInput();
                Console.Clear();
            }else{
                return;
            }
            if(!validRest){
                Display.Rooms.InvalidRestSelection();
            }else{
                Display.Rooms.Resting();
                inventory.GetItem("food").UseItem();
                Console.WriteLine($"You ate food, food left: {inventory.GetItem("food").Amount}");
                Console.ReadKey();
            }
        }else{
            Display.Rooms.NoFoodText();
        }

    }

    /// <summary>
    /// Set's the amount of times you can block.
    /// </summary>
    /// <param name="health">The shield's block-count you want</param>
    public void SetShieldHealth(int health = 3){
        inventory.GetItem("shield").Amount = health;
        inventory.GetItem("shield").MaxAmount = health;
    }

    /// <summary>
    /// Return if player was surpised or not. Chance is aquired from the Globals Surprised chance, which gets defined in Game.SelectDifficulty()
    /// </summary>
    public void CheckSurprised(){
        Surprised = (new Random().Next(1, 100) < Globals.SurprisedChance);
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
        return inventory.GetItem("shield").Amount > 0;
    }

    /// <summary>
    /// Subtrack enemy-damage from the shield's health. Higher damage = does more damage to the shield.
    /// </summary>
    /// <param name="enemyDamage">How much damage the enemy deals</param>
    public void Block(int enemyDamage){
        Display.BlockedEnemyMessage();
        inventory.GetItem("shield").Amount -= (int) Math.Ceiling(enemyDamage / 10.0); 
    }

    /// <summary>
    /// Returns the current health of the shield.
    /// </summary>
    /// <returns>Current health of the shield</returns>
    public int GetShieldHealth(){
        return inventory.GetItem("shield").Amount;
    }
    



}