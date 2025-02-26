using System.Runtime.InteropServices;
using System.Threading.Tasks.Dataflow;

class Game{
    /// <summary>
    /// The current game-state (like exploring, combat, etc)
    /// </summary>
    private static Game.State CurrentState;

    /// <summary>
    /// player input for the difficulty.
    /// </summary>
    static void StartInput(){
        string? input = Console.ReadLine();
        if (input == "") input = " ";
        if(input == null) Environment.Exit(-1);
        bool invalidSelection = false;
        switch(input.ToLower()[0]){
            case 'e':
                Globals.SurprisedChance = 0;
                Cave.GenerateCave( 1, 10);
                break;
            case 'm':
                Cave.GenerateCave(2, 10);
                Globals.SurprisedChance = 30;
                break;
            case 'h':
                Cave.GenerateCave(2, 10);
                Globals.SurprisedChance = 50;
                break;
            case 'i':
                Cave.GenerateCave(2, 10);
                Globals.Player.SetHealth(Globals.Player.Health - 20);
                Globals.SurprisedChance = 50;
                break;
            default:
                Cave.GenerateCave(5, 10, true);
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

    /// <summary>
    /// Display the difficulty selection menu, and query the player for the choice.
    /// </summary>
    static void SelectDifficulty(){
        Display.DifficultyMessage();
        StartInput();
        Console.Clear();
    }

    /// <summary>
    /// Gameloop states
    /// </summary>
    public enum State {
        abandoned = -3,
        lose = -2,
        won = -1,
        start = 0,
        explore = 1,
        combat = 2,
    }
    /// <summary>
    /// How many rooms we've gone through (due to us lacking unique text for empty rooms)
    /// </summary>
    static int rooms = 1;

    /// <summary>
    /// The various Game-states 
    /// </summary>
    /// <returns>Wether the game loop should stop or not</returns>
    static bool GameTurns ()
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
                    else if (input == "") input = " ";
                    if (input[0] == 'n'){
                        CurrentState = State.abandoned;
                        return true;
                    }else if (input[0] == 'y'){
                        CurrentState = State.explore;
                        return true;
                    }
                    Console.WriteLine("Please enter yes or no!");
                }
            case State.won:
                Display.VictoryMessage();
                return false;
            case State.abandoned:
            case State.lose: 
                Display.LoseMessage();
                return false;
            case State.explore:
                CurrentState = Cave.HandleRoomExploration(rooms++);
                break;
            case State.combat:
                CurrentState = Combat.CombatTurn(CurrentState);
                break;
        }
        return true;
    }
    /// <summary>
    /// Initializes the player.
    /// </summary>
    static void CreatePlayer(){
        Globals.Player = new Player(50, 5, 10);
    }

    /// <summary>
    /// The main game-loop.
    /// </summary>
    /// <returns>Wether to exit the program or not</returns>
    public static bool MainLoop(){
        bool gameRunning = true;
        CurrentState = State.start;

        // difficulty selection
        Globals.PlayerTurn = true;

        while(gameRunning){
            gameRunning = GameTurns();
        }
        ConsoleKey input;
        do{
            Console.WriteLine("Do you want to go again?(Y)es, (N)o.");
            input = Console.ReadKey(true).Key;
        }while(input != ConsoleKey.N && input != ConsoleKey.Y);

        // We restart the game if yes.
        if(input == ConsoleKey.N){
            return false;
        }else{
            return true;
        }
    }

}