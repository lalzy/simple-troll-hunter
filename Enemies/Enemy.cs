class Enemy : Creature{
        
    public static Stack<Enemy> initEnemies (){
        Stack<Enemy> enemies = new Stack<Enemy>();
        enemies.Push(new Troll());
        enemies.Push(new Goblin());
        enemies.Push(new Goblin());
        enemies.Push(new Goblin());
        return enemies;
    }
}