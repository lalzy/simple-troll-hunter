using System.Security.Cryptography;

class Creature {
    public int Health;
    public int MinDamage;
    public int MaxDamage;


    public int Attack(){
        int damage = new Random().Next(MinDamage, MaxDamage);
        return damage;
    }


    public bool IsDead(){
        return this.Health <= 0;
    }

}

