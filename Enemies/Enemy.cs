class Enemy : Creature{

    public static void SpawnEnemy(Cave.RoomType room){
        Enemy? enemy = null;
        if(Cave.RoomType.enemy == room){
            enemy = new Goblin(new Random().Next(3 - 1));
        }else if(Cave.RoomType.boss == room){
            enemy = new Troll();
        }
        Globals.Player.CheckSurprised();
        if(enemy != null){
            Display.SpawnEnemyText(enemy);
        }
        Globals.CurrentEnemy = enemy;
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