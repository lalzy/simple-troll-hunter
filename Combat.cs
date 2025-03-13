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
        if(Enemy.CurrentEnemy == null) return;
        if(!Globals.PlayerTurn){
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
                    Globals.Player.TakeDamage(damage);
                }
            }
        }else if(Enemy.CurrentEnemy.IsDead()){
            Console.WriteLine(Enemy.CurrentEnemy.DeathText);
            Enemy.CurrentEnemy = null;
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
        shootArrow = 's',
        torch = 't',
        magic = 'm',
        chargeUp = 'p',
        skip = '-', // Just empty
    }

    /// <summary>
    /// Gets the action the player wish to perform
    /// </summary>
    /// <returns>Action player choose to perform</returns>
    static Action GetPlayerAction(){
        bool AtLeastOneSelection = Display.PlayerMenu(Globals.PlayerTurn, Enemy.CurrentEnemy, Globals.Player);
        if(!AtLeastOneSelection){
            return Action.skip;
        }
    
        string? input = Display.GetInput();
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
            }else if(input[0] == 't'){
                return Action.torch;
            }else if (input[0] == 's'){
                return Action.shootArrow;
            }else if(input[0] == 'm'){
                return Action.magic;
            }else if(input[0] == 'p'){
                if(Globals.Player.Perks.Contains(Player.PerksEnum.ChargeUp)){
                    return Action.chargeUp;
                }
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
            case Action.skip:
                return;
            case Action.attack:
                Enemy.CurrentEnemy.TakeDamage(Globals.Player.Attack(Globals.Player.GetMainWeapon()));
                if(Enemy.CurrentEnemy.IsDead()){
                    Enemy.CurrentEnemy.DeathText = Display.GetKillMessage(Enemy.CurrentEnemy);
                }
                break;
            case Action.block: 
                // add drinking.
                Globals.Player.IsBlocking = Globals.Player.CanBlock();
                break;
            case Action.chargeUp:
                Display.ChargeUp();
                Weapon? weapon = Globals.Player.GetMainWeapon();
                Globals.Player.ExtraDamage =  weapon != null ? (int) (weapon.MaxAttribute * 0.25 ): 1;
                break;
            case Action.shootArrow:
                if(Enemy.CurrentEnemy != null){
                    if((FirstTurn || Globals.Player.Perks.Contains(Player.PerksEnum.BowMaster)) || Enemy.CurrentEnemy.Stunned){
                        if(Globals.Player.Inventory.UseItem(Inventory.Items.arrows)){
                            Enemy.CurrentEnemy.Stun();
                            Enemy.CurrentEnemy.StunCause = Display.StunCause.arrow;
                            Display.ShootArrow();


                            Enemy.CurrentEnemy.TakeDamage(Globals.Player.Attack(Globals.Player.Equipment.OffHand));
                            if(Enemy.CurrentEnemy.IsDead()){
                                Enemy.CurrentEnemy.DeathText = Display.GetKillMessage(Enemy.CurrentEnemy, Globals.Player.Equipment.OffHand);
                            }

                        }else{
                            Display.NoArrows();
                        }
                    }else{
                        Display.CanOnlyDoFirstCombatTurn();
                    }
                    
                }
                break;
            case Action.magic:
                Globals.Player.Magic();
                break;
            case Action.torch:
                if(Enemy.CurrentEnemy != null){
                    if(Globals.Player.Inventory.UseItem(Inventory.Items.torch)){
                        Enemy.CurrentEnemy.StunCause = Display.StunCause.torch;
                        Enemy.CurrentEnemy.Stun(3); // Stuns for effectively 3 turns.
                        Display.ThrewTorch();
                    }
                }
                break;
            default:
                if(!Globals.Player.Stunned && Globals.PlayerTurn)
                    Display.DoNothingMessage();
                break;
        }
    }
}