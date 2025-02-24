using System.Threading.Tasks.Dataflow;

class Game{
    static Stack<Enemy>? Enemies;
    static Player _player = new Player(1,1,1);
    static Enemy? CurrentEnemy;
    static bool PlayerTurn;

    static void PrintMenu(){
        
        Console.Write(">> ");
    }

    static Stack<Enemy> StartInput(){
        string? input = Console.ReadLine();
        if(input == null) Environment.Exit(-1);
        int goblinCount = 0;
        bool randomized = false;
        bool randomizedBoss = false;
        bool invalidSelection = false;

        switch(input.ToLower()[0]){
            case 'e':
                goblinCount = 4;
                break;
            case 'm':
                goblinCount = 3;
                randomized = true;
            break;
            case 'h':
                goblinCount = 4;
                randomized = true;
                break;
            case 'i':
                goblinCount = 5;
                randomized = true;
                randomizedBoss = true;
                break;
            default:
                goblinCount = 1;
                randomized = true;
                randomizedBoss = true;
                invalidSelection = true;
            break;
        }

        if(invalidSelection){
            Display.InvalidDifficultySelectionMessage();
        }
        return Enemy.initEnemies(goblinCount, randomized, randomizedBoss);
    }


    static void SelectDifficulty(){
        Display.DifficultyMessage();
        Enemies = StartInput();
        Console.Clear();
    }

    enum State {
        running = 0,
        won = 1,
        lose = 2
    }

    static void AiAction(){
        if(!PlayerTurn && CurrentEnemy != null){
            _player.Surprised = false;

            int damage = CurrentEnemy.Attack();
            if(_player.IsBlocking){
                _player.IsBlocking = false;
                _player.Block(damage);
            }else{
                _player.TakeDamagePrint(damage);
                _player.Health -= damage;
            }
        }
    }

    enum Action {
        nothing = ' ',
        attack = 'a',
        block = 'b',
        status = 'c',
        examine = 'e',
    }

    static Action GetPlayerAction(){
        if(CurrentEnemy == null) return Action.nothing; // Can never happen, but it prevents annoying Warnings.

        Display.PlayerMenu(PlayerTurn, CurrentEnemy, _player);
        string? input = Console.ReadLine();
        Console.Clear();
        if (input == null){ 
            Environment.Exit(-1);
        }else if(input == ""){
            input = " ";
        }else{
            input = input.ToLower();
        }
        
        if(PlayerTurn){
            if(input[0] == 'a'){
                return Action.attack;
            }else if(input[0] == 'e'){
                return Action.examine;
            }
        }
        // player can always do these actions.
        if(input[0] == 'b' && !_player.Surprised){
            return Action.block;
        }else if (input[0] == 'c' || input[0]=='s'){
            return Action.status;
        }else if(input == "exit" || input == "quit"){
            Environment.Exit(0);
        }
        return Action.nothing;
    }
    static void PlayerAction(){
        if (CurrentEnemy == null) return; // Will never happen, but warnings are annoying.
        Action playerAction = GetPlayerAction();
        switch(playerAction){
            case Action.attack:
                CurrentEnemy.Health -= _player.CalcDamage();
            break;
            case Action.block: 
                // add drinking.
                _player.IsBlocking = _player.CanBlock();
                // Only get the bonus if we spend entire turn blocking, and not block as reaction.
                if(PlayerTurn){
                }
            break;
            case Action.examine: 

            break;
            case Action.status: 
                Display.StatusMessage(_player);
            break;
            default:
                Display.DoNothingMessage();
            break;
        }
        if(playerAction == Action.examine){
            Display.PrintState(CurrentEnemy);
        }
    }

    static State GameTurns(){
        // Check if enemy is dead. If it is, make it player turn and spawn new enemy.
        if(CurrentEnemy == null || CurrentEnemy.IsDead()){
            PlayerTurn = true;
            CurrentEnemy = Enemy.SpawnEnemy(Enemies);
                
            // Check if enemy. If no enemy, return win-state.
            if(CurrentEnemy == null){
                return State.won;
            }
        }
        // Get player action.
        PlayerAction();

        // Get Ai action
        AiAction();
        // Switch turn.
        PlayerTurn = !PlayerTurn;
        if(_player.Health <= 0){
            return State.lose;
        }
        return State.running;
    }

    
    static void CreatePlayer(){
        _player = new Player(50, 5, 10);
    }
    public static bool MainLoop(){
        // difficulty selection
        SelectDifficulty();
        CreatePlayer();
        bool gameRunning = true;
        PlayerTurn = true;
        Display.PrintWelcomeMessage();
        while(gameRunning){
            switch(GameTurns()){
                case State.running:
                break;
                case State.won:
                    gameRunning = false;
                    Display.VictoryMessage();
                    break;
                case State.lose: 
                    gameRunning = false;
                    if(Enemies != null && Enemies.Count == 0){
                        Display.TieMessage();
                    }else{
                        Display.LoseMessage();
                    }
                break;
            }
        }
        return false;
    }

}