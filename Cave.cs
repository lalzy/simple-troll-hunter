using System.Runtime.InteropServices;

static class Cave{
    public enum RoomType{
        endofCave = -1,
        empty = 0,
        enemy = 1,
        boss = 2,
    }
    static RoomType[] floor = {RoomType.empty, RoomType.enemy, RoomType.enemy, 
    RoomType.empty, RoomType.enemy, RoomType.empty, RoomType.empty, RoomType.enemy, 
    RoomType.enemy, RoomType.boss};
    static public int _room = 0;
 
    static public RoomType CurrentRoom(){
        if(_room < floor.Length){
            return (RoomType) floor[_room++];
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