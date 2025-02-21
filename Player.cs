using System.Security.Cryptography;

class Player : Creature{
    private Dictionary<string, int> _inventory = new Dictionary<string,int>();
    private int _shieldHealth = 3;
    public  Player(int hp, int minDamage, int maxDamage){
        this.Health = hp;
        this.MinDamage = minDamage;
        this.MaxDamage = maxDamage;

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

    public void ReduceShield(int enemyDamage){
        _shieldHealth -= (int) Math.Ceiling((double) enemyDamage / 10); 
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