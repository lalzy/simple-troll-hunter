using System.Runtime.InteropServices;

static class Cave{
    private const int RoomTypeCount = 15;
    private static RoomType _currentRoom;
    public enum RoomType{
        endofCave = -1,
        empty = 0,
        rest = 1,
        armory = 2,
        kitchen = 3,
        blacksmith = 4,
        enemy = 9,
        boss = 10,
    }
    /// <summary>
    /// The "map" that you traverse through.
    /// </summary>
    static private Stack<RoomType>[]? _cave;
    /// <summary>
    /// Current floor we're on.
    /// </summary>
    static private int _floor;

    /// <summary>
    /// We procedurally generate the dungeon.
    /// </summary>
    /// <param name="floors">How many floors the dungeon should have.</param>
    /// <param name="rooms">How many rooms each floor should have.</param>
    /// <param name="randomizedBoss">Wether the boss-stats should be it's static form, or a procedural one</param>
    static public void GenerateCave (int floors = 2, int rooms = 10, bool randomizedBoss = false){
        Random rnd = new Random();
        _floor = 0; // Resets the current floor counter

        _cave = new Stack<RoomType>[floors];
        Enemy.PopulateBosses(floors, randomizedBoss);

        for(int floor = 0; floor < floors ; floor++){
            _cave[floor] = new Stack<RoomType>();
            _cave[floor].Push(RoomType.boss); // Boss added to the last room of every floor.

            for (int room = 0; room < rooms - 1; room++){
                // We use a switch due to us wanting to define different rooms of different types later.
                switch(rnd.Next(RoomTypeCount)){ // Reflect room types.
                    case 0:
                        _cave[floor].Push(RoomType.armory);
                    break;
                    case 1:
                        _cave[floor].Push(RoomType.rest);
                    break;
                    case 2:
                        _cave[floor].Push(RoomType.kitchen);
                    break;
                    case 3:
                        _cave[floor].Push(RoomType.blacksmith);
                    break;
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                        _cave[floor].Push(RoomType.empty);
                    break;
                    default:
                        _cave[floor].Push(RoomType.enemy);
                    break;
                }
            }
        }
    }

    /// <summary>
    /// proceed through the cave.
    /// </summary>
    /// <returns>Return the current room, if no more rooms, return endOfCave</returns>
    static private RoomType AdvanceRoom(){
        if(_cave == null) return RoomType.endofCave;
        if(_cave[_floor].Count > 0){
            return _cave[_floor].Pop();
        }else if(_floor < _cave.Length - 1){
            _floor++;
            Display.NewFloorText();
            Globals.Player.Rest();
            Console.Clear();
            return _cave[_floor].Pop();
        }else{
            return RoomType.endofCave;
        }
    }

    /// <summary>
    /// Different types of 'empty' rooms with unique effects are handled here.
    /// </summary>
    /// <param name="currentRoom">The current room we're on</param>
    public static void ExploreRoom(){
        bool skipKeyRead = false;
        switch(_currentRoom){
            case RoomType.empty:
                Display.Rooms.Empty();
            break;
            case RoomType.armory:
                skipKeyRead = Rooms.Armory();
            break;
            case RoomType.blacksmith:
                skipKeyRead = Rooms.Blacksmith();
            break;
            case RoomType.rest:
                skipKeyRead = Globals.Player.Rest();
            break;
            case RoomType.kitchen:
                skipKeyRead = Rooms.Kitchen();
            break;
        }
        if(!skipKeyRead){
            Display.PressAnyKey();
            Console.ReadKey();
        }
        Console.Clear();
    }

    /// <summary>
    /// proceed through the dungeon
    /// </summary>
    /// <param name="rooms"></param>
    /// <returns>The current game-state based on the room.</returns>
    public static Game.State HandleRoomExploration(int rooms){
        _currentRoom = Cave.AdvanceRoom();
        Enemy.SpawnEnemy(_currentRoom);
        Console.WriteLine($"room: {rooms}");
        if (Enemy.CurrentEnemy != null){
            Combat.FirstTurn = true;
            // Guranteed to be surprised against the troll.
            if(Enemy.CurrentEnemy.GetType() == typeof(Troll)){
                Globals.SurprisedChance = 100;
            }
            return Game.State.combat;
        }else if(Cave.RoomType.endofCave == _currentRoom){
            return Game.State.won;
        }else{
            ExploreRoom();
            return Game.State.explore;
        }
    }
}