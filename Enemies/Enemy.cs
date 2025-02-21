class Enemy : Creature{
        
    public static Stack<Enemy> initEnemies (int amountOfGoblins, bool randomEnemies){
        Stack<Enemy> enemies = new Stack<Enemy>();
        Random rnd = new Random();

        enemies.Push(new Troll());
        for(int i = 0; i < amountOfGoblins ; i++){
            if(randomEnemies){
                enemies.Push(new Goblin(rnd.Next(3 - 1)));
            }
        }
        return enemies;
    }
}