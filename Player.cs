using System.Security.Cryptography;

class Player : Creature{
    private Dictionary<string, int> _inventory = new Dictionary<string,int>();
    private int _shieldHealth = 3;
    private int _shieldHealthBase = 3;
    public bool IsBlocking = false;
    public int BlockRoll = 0;
    public bool Surprised = false;
    public  Player(int hp, int minDamage, int maxDamage){
        this.SetHealth(hp);
        this.MinDamage = minDamage;
        this.MaxDamage = maxDamage;

    }


    public void Rest(){
        Display.RestText();

        int healAmount = (int) Math.Round(this.BaseHealth * .20);

        if (_shieldHealth > 0 && _shieldHealth < _shieldHealthBase){
            _shieldHealth++;
        }
        this.Heal(healAmount);
    }

    public void SetShieldHealth(int health = 3){
        // use _shieldHealthBase that defines max-shield health unless health is passed.
        if(health > this._shieldHealthBase){
            this._shieldHealth = health;
            this._shieldHealthBase = health;
        }else{
            this._shieldHealth = health;
        }
    }

    public void CheckSurprised(){
        Surprised = (new Random().Next(1, 100) < Globals.SurprisedChance);
    }

    public int CalcDamage (){
            int damage = this.Attack();
            this.AttackPrint(damage);
            if (this.BlockRoll > damage){
                damage = this.BlockRoll;
            }
            this.BlockRoll = 0; // reset block-roll
            return damage;
    }

    public string ShieldConditionText(){
        switch (_shieldHealth){
            case 0:
                return "You have no shield";
            case 1:
                return "It's on it's last legs";
            case 2:
                return "It's seen some use";
            case 3:
                return "it's brand new!";
            default:
                return "";
        }
    }

    public bool CanBlock(){
        if(_shieldHealth == 1){
            Console.WriteLine("You get ready to block with what remains of your shield, better make it count!");
        }else if(_shieldHealth == 0){
            Console.WriteLine("block with what? You have no shield anymore.");
        }else{
            Console.WriteLine("You get ready to block with the shield");
        }

        return _shieldHealth > 0;
    }

    public void TakeDamagePrint(int damage){
        if (damage <= (this.MaxDamage / 4)){
            Console.WriteLine("He barely scratched you..");
        }else if (damage <= (this.MaxDamage / 2)){
            Console.WriteLine("That hurt");
        }else if(damage == this.MaxDamage){
            Console.WriteLine("That's not good...");
        }else{
            Console.WriteLine("You should learn to dodge...");
        }
    }

    public void Block(int enemyDamage){
        Display.ShieldBlockMessage();
        _shieldHealth -= (int) Math.Ceiling(enemyDamage / 10.0); 
    }

    public int GetShieldHealth(){
        return _shieldHealth;
    }
    
    public int GetitemCount(string item){
        return (from invItem in _inventory where invItem.Key.ToLower() == item.ToLower() select invItem.Value).FirstOrDefault();
    }

    public void AttackPrint(int damage){
        if (damage <= (this.MaxDamage / 4)){
            Console.WriteLine("You barely scratched it..");
        }else if (damage <= (this.MaxDamage / 2)){
            Console.WriteLine("An alright hit");
        }else if(damage == this.MaxDamage){
            Console.WriteLine("A perfect hit!");
        }else{
            Console.WriteLine("A good hit!");
        }
    }
}