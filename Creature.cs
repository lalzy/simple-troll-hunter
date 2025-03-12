using System.Collections;
using System.Formats.Asn1;
using System.Security.Cryptography;

class Creature {
    public string Name = "";
    public int Health;
    public int BaseHealth;
    public bool Stunned;
    public int StunDuration;
    public Display.StunCause StunCause;

    public void ProgressStunned(){
        if(StunDuration > 0){
            StunDuration--;
        }else if (Stunned){
            Stunned = false;
        }
    }
    /// <summary>
    /// Stuns the enemy for amount of turns specified.
    /// </summary>
    /// <param name="turns">How many turns.</param>
    public void Stun(int turns = 1){
        if(this.Stunned){
            this.StunDuration += (turns - 1);
        }else{
            // We subtract by 1 turn, due to how we check stunning. 
            // it'll always be N+1 turns (So we now make it (N-1)+1 turns).
            this.StunDuration = turns - 1;
            Stunned = true;
        }
    }
    /// <summary>
    ///  Restore health of the creature.
    /// </summary>
    /// <param name="amount">Amount of health to recover</param>
    public void Heal(int amount, bool overHeal = false){
        Health += amount;

        if(Health > BaseHealth && !overHeal) Health = BaseHealth;
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

