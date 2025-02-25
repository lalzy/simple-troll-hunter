using System.Collections;
using System.Formats.Asn1;
using System.Security.Cryptography;

class Creature {
    public string Name = "";
    public int Health;
    public int BaseHealth;
    public int MinDamage;
    public int MaxDamage;

    public int Attack(){
        int damage = new Random().Next(MinDamage, MaxDamage);
        return damage;
    }

    public void Heal(int amount){
        if(amount > BaseHealth){
            this.Health = this.BaseHealth;
        }else{
            this.Health = amount;
        }
    }
    
    public void SetHealth(int health){
        this.Health = health;
        this.BaseHealth = health;
    }

    public bool IsDead(){
        return this.Health <= 0;
    }

}

