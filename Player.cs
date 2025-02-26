using System.Security.Cryptography;

class Player : Creature{
    /// <summary>
    /// Not used, yet.
    /// </summary>
    private Dictionary<string, int> _inventory = new Dictionary<string,int>();
    /// <summary>
    ///  How many times you can block.
    /// </summary>
    private int _shieldHealth = 3 ;
    private int _shieldHealthBase = 3;
    public bool IsBlocking = false;
    /// <summary>
    /// Damage roll if blocking was a full action.
    /// </summary>
    public int BlockRoll = 0;
    public bool Surprised = false;
    public  Player(int hp, int minDamage, int maxDamage){
        this.SetHealth(hp);
        this.MinDamage = minDamage;
        this.MaxDamage = maxDamage;

    }

    /// <summary>
    /// Player rest mechanic
    /// </summary>
    public void Rest(){
        Display.Rooms.Rest();

        int healAmount = (int) Math.Round(this.BaseHealth * .20);

        if (_shieldHealth > 0 && _shieldHealth < _shieldHealthBase){
            _shieldHealth++;
        }
        this.Heal(healAmount);
    }

    /// <summary>
    /// Set's the amount of times you can block.
    /// </summary>
    /// <param name="health">The shield's block-count you want</param>
    public void SetShieldHealth(int health = 3){
        // use _shieldHealthBase that defines max-shield health unless health is passed.
        if(health > this._shieldHealthBase){
            this._shieldHealth = health;
            this._shieldHealthBase = health;
        }else{
            this._shieldHealth = health;
        }
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
        return _shieldHealth > 0;
    }

    /// <summary>
    /// Subtrack enemy-damage from the shield's health. Higher damage = does more damage to the shield.
    /// </summary>
    /// <param name="enemyDamage">How much damage the enemy deals</param>
    public void Block(int enemyDamage){
        Display.BlockedEnemyMessage();
        _shieldHealth -= (int) Math.Ceiling(enemyDamage / 10.0); 
    }

    /// <summary>
    /// Returns the current health of the shield.
    /// </summary>
    /// <returns>Current health of the shield</returns>
    public int GetShieldHealth(){
        return _shieldHealth;
    }
    
    /// <summary>
    /// Not used yet.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int GetitemCount(string item){
        return (from invItem in _inventory where invItem.Key.ToLower() == item.ToLower() select invItem.Value).FirstOrDefault();
    }

}