class Player : Creature{

    public  Player(int hp, int minDamage, int maxDamage){
        this.Health = hp;
        this.MinDamage = minDamage;
        this.MaxDamage = maxDamage;
    }
    
    
    public void Attack(Creature enemy){
        int damage = this.AttackAction(enemy);

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