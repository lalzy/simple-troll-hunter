class Enemy : Creature{
        
    public static Stack<Enemy> initEnemies (int amountOfGoblins, bool randomEnemies, bool RandomizedTroll){
        Stack<Enemy> enemies = new Stack<Enemy>();
        Random rnd = new Random();
        Troll troll = new Troll();
        if(RandomizedTroll){
            troll.BaseHealth = rnd.Next(10, 150);
            troll.Health = troll.BaseHealth;
            troll.MinDamage = rnd.Next(1, 5);
            troll.MaxDamage = rnd.Next(1, 20);
        }
        enemies.Push(troll);

        for(int i = 0; i < amountOfGoblins ; i++){
            if(randomEnemies){
                enemies.Push(new Goblin(rnd.Next(3 - 1)));
            }
        }
        return enemies;
    }
}