class Enemy : Creature{
    public Enemy(int health, int minDamage, int maxDamage){
        this.Health = health;
        this.MinDamage = minDamage;
        this.MaxDamage = maxDamage;
    }
    public void Attack(Creature enemy){
        int damage = this.AttackAction(enemy);

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

        
    public static Stack<Enemy> initEnemies (){
        Stack<Enemy> enemies = new Stack<Enemy>();
        enemies.Push(new Enemy(20,5, 10));
        enemies.Push(new Enemy(20,5, 10));
        enemies.Push(new Enemy(20,5, 10));
        return enemies;
    }
}