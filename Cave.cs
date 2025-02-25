using System.Runtime.InteropServices;

static class Cave{
    public enum RoomType{
        endofCave = -1,
        empty = 0,
        enemy = 1,
        boss = 10,
    }
    static private Stack<RoomType>[]? _cave;
    static private int _floor = 0;

    static public void GenerateCave (int floors = 2, int rooms = 10){
        Random rnd = new Random();
        _cave = new Stack<RoomType>[floors];
        Enemy.PopulateBosses(floors);
        for(int floor = 0; floor < floors ; floor++){
            _cave[floor] = new Stack<RoomType>();
            _cave[floor].Push(RoomType.boss); // Boss added to the last room of every floor.

            for (int room = 0; room < rooms - 1; room++){
                switch(rnd.Next(3)){ // Reflect room types.
                    case 0:
                    case 1:
                        _cave[floor].Push(RoomType.empty);
                    break;
                    case 2:
                        _cave[floor].Push(RoomType.enemy);
                    break;
                }
            }
        }
    }

    static public RoomType CurrentRoom(){
        if(_cave == null) return RoomType.endofCave;
        if(_cave[_floor].Count > 0){
            return _cave[_floor].Pop();
        }else if(_floor < _cave.Length - 1){
            _floor++;
            Display.NewFloorText();
            return _cave[_floor].Pop();
        }else{
            return RoomType.endofCave;
        }
    }
    /*
        The actual level structure.
        Should contain information of the room, wether it has an enemy or not.


        floor1:  no, no, enemy, no, enemy, enemy, no, enemy. 
    */
}