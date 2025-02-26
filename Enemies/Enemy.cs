class Enemy : Creature{

    private static Stack<dynamic>? _bosses;
    public static Enemy CurrentEnemy;

    /// <summary>
    /// Initialize the boss stack, which depends on the amount of floors.
    /// </summary>
    /// <param name="floors">How many dungeon floors there are</param>
    /// <param name="randomizedBoss">if the bosses should be randomized or not</param>
    public static void PopulateBosses(int floors, bool randomizedBoss){
        Random rnd = new Random();
        _bosses =  new Stack<dynamic>();
        _bosses.Push(new Troll(randomizedBoss));
        for(int i = 0; i < floors-1 ; i++){
            switch(rnd.Next(2)){
                case 0:
                    _bosses.Push(new GoblinBoss(0, randomizedBoss));
                break;
            }
        }
    }

    /// <summary>
    /// Spawns an enemy to fight.
    /// </summary>
    /// <param name="room">Current room type</param>
    public static void SpawnEnemy(Cave.RoomType room){
        CurrentEnemy = null;
        if(Cave.RoomType.enemy == room){
            CurrentEnemy = new Goblin(new Random().Next(3 - 1));
        }else if(Cave.RoomType.boss == room){
            if(_bosses == null){
                CurrentEnemy = new Goblin(new Random().Next(3- 1));
            }else{
                CurrentEnemy = _bosses.Pop();
            }
        }
        Globals.Player.CheckSurprised();
        if(CurrentEnemy != null){
            Display.SpawnEnemyText(CurrentEnemy);
        }
    }

    /// <summary>
    /// Check if the enemy is dead or not.
    /// </summary>
    /// <returns>Wether enemy is dead or not</returns>
    new public bool IsDead(){
        if(this.Health <= 0){
            Display.CreatureDiesMessage(this);
            return true;
        }else{
            return false;
        }
    }
}