class Enemy : Creature{
    public static Enemy? SpawnEnemy(Stack<Enemy>? enemies){
        if(enemies == null || enemies.Count == 0){
            return null;
        }
        Enemy enemy = enemies.Pop();
        Display.SpawnEnemyText(enemy);
        return enemy;
    }

    public static Stack<Enemy> initEnemies (int amountOfGoblins, bool RandomizedTroll){
        Stack<Enemy> enemies = new Stack<Enemy>();
        Random rnd = new Random();
        Troll troll = new Troll();
        if(RandomizedTroll){
            troll.BaseHealth = rnd.Next(45, 150);
            troll.Health = troll.BaseHealth;
            troll.MinDamage = rnd.Next(1, 10);
            troll.MaxDamage = rnd.Next(5, 20);
        }
        enemies.Push(troll);

        for(int i = 0; i < amountOfGoblins ; i++){
            enemies.Push(new Goblin(rnd.Next(3 - 1)));
        }
        return enemies;
    }

    new public bool IsDead(){
        if(this.Health <= 0){
            Display.CreatureDiesMessage(this);
            return true;
        }else{
            return false;
        }
    }
}