class Enemy : Creature{

    public static void PopulateBosses(int floors, bool randomizedBoss){
        Random rnd = new Random();
        Globals.Bosses =  new Stack<dynamic>();
        Globals.Bosses.Push(new Troll(randomizedBoss));
        for(int i = 0; i < floors-1 ; i++){
            switch(rnd.Next(2)){
                case 0:
                    Globals.Bosses.Push(new GoblinBoss(0, randomizedBoss));
                break;
            }
        }
    }

    public static void SpawnEnemy(Cave.RoomType room){
        Enemy? enemy = null;
        if(Cave.RoomType.enemy == room){
            enemy = new Goblin(new Random().Next(3 - 1));
        }else if(Cave.RoomType.boss == room){
            if(Globals.Bosses == null){
                enemy = new Goblin(new Random().Next(3- 1));
            }else{
                enemy = Globals.Bosses.Pop();
            }
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