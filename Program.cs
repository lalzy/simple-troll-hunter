// See https://aka.ms/new-console-template for more information

using System.Collections;



void printMenu(bool playerTurn, Player player, bool blocking){
    if(playerTurn){
        Console.WriteLine("[A]ttack with your sword!");
        Console.WriteLine("[E]xamine the enemy, see it's condition.");
    }
    if(player.GetShieldHealth() > 0 && !blocking){
        Console.WriteLine("[B]lock with your shield (and do 2 attack rolls, keeping the highest).");
    }
    string text = playerTurn ? "skip" : "continue";
    Console.WriteLine($"press enter to {text}");
    
    Console.Write(">> ");
}

void printStartMenu(){
    Console.WriteLine("Welcome to the troll cave.");
    Console.WriteLine("It's currently occupied by a troll, and it's famly (Golbins)");
    Console.WriteLine("You've been tasked to clear out this hive.");
    Console.WriteLine("Remember, you can always type {exit} or {quit} to exit");
    Console.WriteLine("---------------------------------------------------------------\n");
}


Stack<Enemy> enemies = Enemy.initEnemies(3, true);
Enemy currentEnemy = enemies.Pop(); // Get the current enemy.
Player player = new Player(50, 5, 10);

void start(){
    bool playerTurn = true;
    bool gameRunning = true;
    bool surprised = false;
    bool blocking = false;
    int blockRoll = 0;

    string? input = "";
    Console.Clear();

    printStartMenu();
    do{
        if(playerTurn){
            Console.WriteLine($"You're facing a, <{currentEnemy.Name}>");
        }
        if(surprised && playerTurn){
            playerTurn = false;
            Console.WriteLine("You were surprised! Lost a turn");
        }
        printMenu(playerTurn, player, blocking);
        input = Console.ReadLine();
        if (input == null){ 
            Environment.Exit(-1);
        }else if(input == ""){
            input = " ";
        }else{
            input = input.ToLower();
        }
        
        Console.Clear();
        if(playerTurn){
            if(input[0] == 'a'){
                int damage = player.Attack();
                player.AttackPrint(damage);
                if (blockRoll > damage){
                    damage = blockRoll;
                }
                blockRoll = 0; // reset block-roll
                currentEnemy.Health -= damage;
            }else if(input[0] == 'e'){
                currentEnemy.PrintState();
            }else{
                Console.WriteLine($"You do nothing");
            }
        }
        // player can always do these actions.
        if(input[0] == 'b' && !surprised){
            // add drinking.
            blocking = player.CanBlock();
            blockRoll = player.Attack();
        }else if(input == "exit" || input == "quit"){
            Environment.Exit(0);
        }
        
        // enemy turn
        if(!playerTurn){
            surprised = false;
            int damage = currentEnemy.Attack();
            if(blocking){
                blocking = false;
                Console.WriteLine("You blocked the attack!");
                player.ReduceShield(damage);
            }else{
                player.TakeDamagePrint(damage);
                player.Health -= damage;
            }
        }

        // post-turn cleanups
        playerTurn = !playerTurn;

        if(currentEnemy.IsDead()){
            if (currentEnemy.GetType() == typeof(Goblin)){
                Console.WriteLine("You killed the Goblin!");
            }else{
                Console.WriteLine("You killed a troll!!");
            }
            if(enemies.Count > 0){
                currentEnemy = enemies.Pop();
                if(typeof(Troll) == currentEnemy.GetType()){
                    Console.WriteLine("The troll appears!");
                }else{
                    Console.WriteLine($"an {currentEnemy.Name} emerge from the shadows!");
                }
                playerTurn = true;
                surprised = (new Random().Next(1, 100) > 50);
            }else{
                Console.WriteLine("You've killed all enemies!");
                gameRunning = false;
            }
        }

        // post-enemy-turn
        if(player.IsDead()){
            if(enemies.Count == 0){
                Console.WriteLine("but, you died..");
            }else{
                Console.WriteLine("You died!");
            }
            gameRunning = false;
        }
    }while (gameRunning);
};


start();
if(player.Health > 0){
    Console.WriteLine("You survived with:");
    Console.WriteLine($"HP left: {player.Health}");
    if(player.GetShieldHealth() > 0){
        Console.WriteLine("and even kept your shield, impressive!");
    }
}