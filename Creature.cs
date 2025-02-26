using System.Collections;
using System.Formats.Asn1;
using System.Security.Cryptography;

class Creature {
    public string Name = "";
    public int Health;
    public int BaseHealth;
    public int MinDamage;
    public int MaxDamage;

    /// <summary>
    /// Get the damage the creature will deal.
    /// </summary>
    /// <returns>Damage number rolled</returns>
    public int Attack(){
        int damage = new Random().Next(MinDamage, MaxDamage);
        return damage;
    }

    

    /// <summary>
    ///  Restore health of the creature.
    /// </summary>
    /// <param name="amount">Amount of health to recover</param>
    public void Heal(int amount){
        if(amount > BaseHealth){
            this.Health = this.BaseHealth;
        }else{
            this.Health += amount;
        }
        Console.WriteLine();
    }
    
    /// <summary>
    /// Sets the health of the creature (full health).
    /// </summary>
    /// <param name="health">Health to set</param>
    public void SetHealth(int health){
        this.Health = health;
        this.BaseHealth = health;
    }

    /// <summary>
    /// Checks wether the creature is dead or not.
    /// </summary>
    /// <returns></returns>
    public bool IsDead(){
        return this.Health <= 0;
    }

}

