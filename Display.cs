class Display{
    static public void CreatureDiesMessage(Enemy enemy){
        Console.WriteLine($"You killed the {enemy.Name}!");
        Console.WriteLine("press {any} key to continue.");
        Console.ReadKey();
        Console.Clear();
    }

    static public void NewFloorText(){
        Console.WriteLine("You enter a new floor");
    }

    static public void CreatureDiesMessage(Player player){
        Console.WriteLine("Player died!");
    }

    static public void SpawnEnemyText(Enemy enemy){
        if(enemy.GetType() == typeof(Troll)){
            Console.WriteLine("The troll appears!");
        }else if (enemy.GetType() == typeof(GoblinBoss)){
            Console.WriteLine("Room is guarded by a big Goblin!");
        }else{
            Console.WriteLine($"an {enemy.Name} emerge from the shadows!");
        }
    }

    static public void PlayerMenu(bool playerTurn, Enemy currentEnemy, Player player){
        if(playerTurn){
            Console.WriteLine($"You're facing a, <{currentEnemy.Name}>");
        }
        Console.WriteLine($"HP: {player.Health}");
        if(player.Surprised && playerTurn){
            playerTurn = false;
            Console.WriteLine("You were surprised! Lost a turn");
        }
        Console.WriteLine("-------------------------");
        if(playerTurn){
            Console.WriteLine("[A]ttack with your sword!");
            Console.WriteLine("[E]xamine the enemy, see it's condition.");
        }
        if(player.GetShieldHealth() > 0 && !player.IsBlocking){
            string extra = playerTurn ? "(and do 2 attack rolls, keeping the highest)" : "";
            Console.WriteLine($"[B]lock with your shield{extra}.");
            Console.WriteLine($"     Shield Condition:{player.ShieldConditionText()}");
        }
        string text = playerTurn ? "skip" : "continue";
        Console.WriteLine($"press enter to {text}");
        
        Console.Write(">> ");
    }
    
    static public void ShieldBlockMessage(){
        Console.WriteLine("You blocked the attack!");
    }

    static public void VictoryMessage (){
        Console.WriteLine("Congratualations, you won!");
    }

    static public void TieMessage(){
        Console.WriteLine("but, you died..");
    }

    static public void LoseMessage(){
        Console.WriteLine("You died...");
    }


    static public void DoNothingMessage(){
        Console.WriteLine("You do nothing");
    }

    static public void StatusMessage(Player player){
        Console.WriteLine($"You currently have: {player.Health}hp left");
        Console.WriteLine($"Your shield's condition is: {player.ShieldConditionText()}");
    }


    static public void PrintState(Creature creature){
        if(creature.BaseHealth == creature.Health){
            Console.WriteLine("It's not injured at all!");
        }else if(creature.BaseHealth / 2 < creature.Health){
            Console.WriteLine("It's barely injured");
        }else if(creature.BaseHealth / 2 > creature.Health){
            Console.WriteLine("It seems minorly injured");
        }else if (creature.BaseHealth / 4 > creature.Health){
            Console.WriteLine("It seems quite injured");
        }else{
            Console.WriteLine("It looks close to death");
        }
    }

    static public void PrintWelcomeMessage(){
        Console.WriteLine("Welcome to the troll cave.");
        Console.WriteLine("It's currently occupied by a troll, and it's famly (Goblins)");
        Console.WriteLine("You've been tasked to clear out the cave.");
        Console.WriteLine("Remember, you can always type {exit} or {quit} to exit");
        Console.WriteLine("---------------------------------------------------------------\n");
    }

    static public void DifficultyMessage(){
        Console.WriteLine("Type and press enter to select.");
        Console.WriteLine("Only the first character actually matters and valid characters are found between '[]'");
        Console.WriteLine("What difficulty do you want to play?");
        Console.WriteLine("[E]asy - can't be surprised (lose turn on enemy spawn)");
        Console.WriteLine("[M]edium - 30% chance of being surprised");
        Console.WriteLine("[H]ard - 50% chance of being surprised, boss-stats are random");
        Console.WriteLine("[I] want to test my luck - Same as hard + -20 HP for player.");
    }

    static public void InvalidDifficultySelectionMessage(){
            Console.WriteLine("You choose to tempt the fates...");
            Console.WriteLine();
    }

}