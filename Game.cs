using System.Runtime.InteropServices;
using System.Threading.Tasks.Dataflow;

class Game{
    public static Game.State CurrentState;

    static void StartInput(){
        string? input = Console.ReadLine();
        if(input == null) Environment.Exit(-1);
        bool invalidSelection = false;
        bool randomizedBoss = false;
        switch(input.ToLower()[0]){
            case 'e':
                Globals.SurprisedChance = 0;
                Cave.GenerateCave(1, 10);
                break;
            case 'm':
                Cave.GenerateCave(2, 10);
                Globals.SurprisedChance = 30;
                break;
            case 'h':
                Cave.GenerateCave(2, 10);
                Globals.SurprisedChance = 50;
                randomizedBoss = true;
                break;
            case 'i':
                Cave.GenerateCave(2, 10);
                Globals.Player.SetHealth(Globals.Player.Health - 20);
                Globals.SurprisedChance = 50;
                randomizedBoss = true;
                break;
            default:
                Cave.GenerateCave(5, 10);
                randomizedBoss = true;
                Globals.Player.SetHealth(Globals.Player.Health - 20); 
                Globals.Player.SetShieldHealth(2);
                Globals.SurprisedChance = 100;
                invalidSelection = true;
            break;
        }

        if(invalidSelection){
            Display.InvalidDifficultySelectionMessage();
        }
    }


    static void SelectDifficulty(){
        Display.DifficultyMessage();
        StartInput();
        Console.Clear();
    }


    public enum State {
        abandoned = -3,
        lose = -2,
        won = -1,
        start = 0,
        explore = 1,
        combat = 2,
    }
    static int rooms = 1;
    static void GameTurns ()
    {
        switch(Game.CurrentState){
            case State.start:
                SelectDifficulty();
                CreatePlayer();
                Display.PrintWelcomeMessage();
                CurrentState = State.explore;
                Console.WriteLine("procceed inside?");
                Console.WriteLine("[Y]es - to enter");
                Console.WriteLine("[N]o - to head back");

                while(true){
                    string? input = Console.ReadLine();
                    Console.Clear();
                    if (input == null) Environment.Exit(-1);
                    if (input[0] == 'n'){
                        CurrentState = State.abandoned;
                        return;
                    }else if (input[0] == 'y'){
                        CurrentState = State.explore;
                        return;
                    }
                    Console.WriteLine("Please enter yes or no!");
                }
            case State.won:
                _gameRunning = false;
                Display.VictoryMessage();
                break;
            case State.abandoned:
            case State.lose: 
                _gameRunning = false;
                Display.LoseMessage();
                break;
            case State.explore:
                Cave.RoomType currentRoom = Cave.CurrentRoom();
                Enemy.SpawnEnemy(currentRoom);
                Console.WriteLine($"room: {rooms++}");
                if (Globals.CurrentEnemy != null){
                    Game.CurrentState = State.combat;
                }else if(Cave.RoomType.endofCave == currentRoom){
                    CurrentState = State.won;
                }else{
                    Console.WriteLine($"Room was empty..");
                    Console.ReadKey();
                    Console.Clear();
                }
                break;
            case State.combat:
                Combat.CombatTurn();
                break;
        }
    }
    static void CreatePlayer(){
        Globals.Player = new Player(50, 5, 10);
    }
    static bool _gameRunning = true;
    public static bool MainLoop(){
        CurrentState = State.start;
        // difficulty selection
        Globals.PlayerTurn = true;
        while(_gameRunning){
            GameTurns();
        }
        return false;
    }

}