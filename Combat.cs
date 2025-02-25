static class Combat{
    static public void CombatTurn(){
        // Check if enemy is dead. If it is, make it player turn and spawn new enemy.
        if(Globals.CurrentEnemy == null || Globals.CurrentEnemy.IsDead()){
            Globals.PlayerTurn = true;
            Game.CurrentState = Game.State.explore;
            return;
        }
        // Get player action.
        PlayerAction();

        // Get Ai action
        AiAction();
        // Switch turn.
        Globals.PlayerTurn = !Globals.PlayerTurn;
        if(Globals.Player.Health <= 0){
            Game.CurrentState = Game.State.lose;
        }
    }

    static void AiAction(){
        if(!Globals.PlayerTurn && Globals.CurrentEnemy != null){
            Globals.Player.Surprised = false;

            int damage = Globals.CurrentEnemy.Attack();
            if(Globals.Player.IsBlocking){
                Globals.Player.IsBlocking = false;
                Globals.Player.Block(damage);
            }else{
                Globals.Player.TakeDamagePrint(damage);
                Globals.Player.Health -= damage;
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
        if(Globals.CurrentEnemy == null) return Action.nothing; // Can never happen, but it prevents annoying Warnings.

        Display.PlayerMenu(Globals.PlayerTurn, Globals.CurrentEnemy, Globals.Player);
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
            }
        }
        // player can always do these actions.
        if(input[0] == 'b' && !Globals.Player.Surprised){
            return Action.block;
        }

        return Action.nothing;
    }
    static void PlayerAction(){
        if (Globals.CurrentEnemy == null) return; // Will never happen, but warnings are annoying.
        Action playerAction = GetPlayerAction();
        switch(playerAction){
            case Action.attack:
                Globals.CurrentEnemy.Health -= Globals.Player.CalcDamage();
            break;
            case Action.block: 
                // add drinking.
                Globals.Player.IsBlocking = Globals.Player.CanBlock();
                // Only get the bonus if we spend entire turn blocking, and not block as reaction.
                if(Globals.PlayerTurn){
                }
            break;
            case Action.examine: 

            break;
            case Action.status: 
                Display.StatusMessage(Globals.Player);
            break;
            default:
                Display.DoNothingMessage();
            break;
        }
        if(playerAction == Action.examine){
            Display.PrintState(Globals.CurrentEnemy);
        }
    }

}