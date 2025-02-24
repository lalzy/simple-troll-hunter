using System.Collections;
using System.Formats.Asn1;
using System.Security.Cryptography;

class Creature {
    public string Name = "";
    public int Health;
    public int BaseHealth;
    public int MinDamage;
    public int MaxDamage;

    public void PrintState(){
        if(BaseHealth == Health){
            Console.WriteLine("It's not injured at all!");
        }else if(BaseHealth / 2 < Health){
            Console.WriteLine("It's barely injured");
        }else if(BaseHealth / 2 > Health){
            Console.WriteLine("It seems minorly injured");
        }else if (BaseHealth / 4 > Health){
            Console.WriteLine("It seems quite injured");
        }else{
            Console.WriteLine("It looks close to death");
        }
    }

    public int Attack(){
        int damage = new Random().Next(MinDamage, MaxDamage);
        return damage;
    }

    
    public bool IsDead(){
        return this.Health <= 0;
    }

}

