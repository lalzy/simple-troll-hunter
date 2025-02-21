using System.Security.Cryptography;

class Creature {
    public int Health;
    public int MinDamage;
    public int MaxDamage;

public int AttackAction(Creature attacked){
    Random rnd = new Random();
    int damage = rnd.Next(this.MinDamage, this.MaxDamage);
    attacked.Health -= damage;

    return damage;
}


    public bool IsDead(){
        return this.Health <= 0;
    }

}

