class Enemy : Creature{

    public static void PopulateBosses(int floors){
        Random rnd = new Random();
        Globals.Bosses.Push(new Troll());
        for(int i = 0; i < floors ; i++){
            switch(rnd.Next(2)){
                case 0:
                    Globals.Bosses.Push(new GoblinBoss(0));
                break;
            }
        }
    }

    public static void SpawnEnemy(Cave.RoomType room){
        Enemy? enemy = null;
        if(Cave.RoomType.enemy == room){
            enemy = new Goblin(new Random().Next(3 - 1));
        }else if(Cave.RoomType.boss == room){
            enemy = Globals.Bosses.Pop();
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