static class Combat{
    public static bool FirstTurn;
    /// <summary>
    /// Starts a combat-turn.
    /// </summary>
    /// <param name="CurrentState">Current game-state. Will default to exploration if enemy doesn't exist, or is dead</param>
    static public Game.State CombatTurn(Game.State CurrentState){
        // Check if enemy is dead. If it is, make it player turn and spawn new enemy.
        if(Enemy.CurrentEnemy == null || Enemy.CurrentEnemy.IsDead()){
            Enemy.CurrentEnemy = null;
            Globals.PlayerTurn = true;
            return Game.State.explore;
        }
        // Don't do the enemy-action choic (block), while enemy is stunned.
        //  A bit unclean/dirty, as we'll want to do something when enemy is stunned 'later'.
        //  As this is effectively an 'bonus action'
        if(!(!Globals.PlayerTurn && Enemy.CurrentEnemy.Stunned)){
            if(Globals.Player.Stunned){
                Globals.PlayerTurn = false;
                Globals.Player.ProgressStunned();
            }
            // Get player action.
            PlayerAction();
        }
      
        // Get Ai action
        AiAction();
        // Switch turn.
        Globals.PlayerTurn = !Globals.PlayerTurn;
        if(Globals.Player.Health <= 0){
            CurrentState = Game.State.lose;
        }

        FirstTurn = false; // TO be used with arrows. Lets you do an +1 attack action.
        return CurrentState;
    }

    /// <summary>
    /// Ai's turn to act.
    /// </summary>
    static void AiAction(){
        if(!Globals.PlayerTurn && Enemy.CurrentEnemy != null){
            Globals.Player.Stunned = false;
            if(Enemy.CurrentEnemy.Stunned){
                Enemy.CurrentEnemy.ProgressStunned();
                Display.EnemyIsStunned(Enemy.CurrentEnemy);
            }else{
                int damage = Enemy.CurrentEnemy.Attack();
                if(Globals.Player.IsBlocking){
                    Globals.Player.IsBlocking = false;
                    Globals.Player.Block(damage);
                }else{
                    Display.TakeDamagePrint(damage);
                    Globals.Player.Health -= damage;
                }
            }
        }
    }

    /// <summary>
    /// Valid action keys
    /// </summary>
    enum Action {
        nothing = ' ',
        attack = 'a',
        block = 'b',
        status = 'c',
        examine = 'e',
        shootArrow = 's',
        torch = 't',
    }

    /// <summary>
    /// Gets the action the player wish to perform
    /// </summary>
    /// <returns>Action player choose to perform</returns>
    static Action GetPlayerAction(){
        Display.PlayerMenu(Globals.PlayerTurn, Enemy.CurrentEnemy, Globals.Player);
        string? input = Console.ReadLine();
        Console.Clear();
        
        // Always able to exit
        if(input == "exit" || input == "quit"){
            Environment.Exit(0);
        }

        if (input == null){ 
            Environment.Exit(-1);
        }else if(input == ""){
            input = " ";
        }else{
            input = input.ToLower();
        }
        
        if(Globals.PlayerTurn){
            if(input[0] == 'a'){
                return Action.attack;
            }else if(input[0] == 'e'){
                return Action.examine;
            }else if(input[0] == 't'){
                return Action.torch;
            }else if (input[0] == 's'){
                return Action.shootArrow;
            }
        }
        // player can always do these actions.
        if(input[0] == 'b' && !Globals.Player.Stunned){
            return Action.block;
        }

        return Action.nothing;
    }
    /// <summary>
    /// Executes player's action based on what he choose to do.
    /// </summary>
    static void PlayerAction(){
        Action playerAction = GetPlayerAction();     
        switch(playerAction){
            case Action.attack:
                Enemy.CurrentEnemy.Health -= Globals.Player.CalcDamage();
            break;
            case Action.block: 
                // add drinking.
                Globals.Player.IsBlocking = Globals.Player.CanBlock();
                // Only get the bonus if we spend entire turn blocking, and not block as reaction.
                if(Globals.PlayerTurn){
                    Globals.Player.BlockRoll = Globals.Player.Attack();
                }
            break;
            case Action.examine: 

            break;
            case Action.shootArrow:
                if(Enemy.CurrentEnemy != null){
                    if(Globals.Player.Inventory.UseItem("arrows")){
                        Enemy.CurrentEnemy.Stun();
                        Enemy.CurrentEnemy.StunCause = Display.StunCause.arrow;
                        Console.WriteLine("You shot an arrow at the enemy!");
                    }
                }
            break;
            case Action.status: 
                Display.StatusMessage(Globals.Player);
            break;
            case Action.torch:
                if(Enemy.CurrentEnemy != null){
                    if(Globals.Player.Inventory.UseItem("torch")){
                        Enemy.CurrentEnemy.StunCause = Display.StunCause.torch;
                        Enemy.CurrentEnemy.Stun(3); // Stuns for effectively 3 turns.
                        Display.ThrewTorch();
                    }
                }
            break;
            default:
                if(!Globals.Player.Stunned)
                    Display.DoNothingMessage();
            break;
        }

        if(playerAction == Action.examine){
            Display.PrintState(Enemy.CurrentEnemy);
        }
    }

}