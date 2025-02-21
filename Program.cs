// See https://aka.ms/new-console-template for more information

using System.Collections;

void printCurrentState(int hp, int enemyHp){
    Console.WriteLine($"Current HP: {hp}" +
    $"Enemy HP: {enemyHp}");
}

void printMenu(bool playerTurn, Player player){
    if(playerTurn){
        Console.WriteLine($"HP: {player.Health}");
        Console.WriteLine("1 - to attack");
    }else{
        Console.WriteLine("press enter to continue");
    }
    Console.Write(">> ");
}

void printStartMenu(){
    Console.WriteLine("Welcome.");
    Console.WriteLine("Type {exit} to exit");
    Console.WriteLine("------------------------\n");
}


void start(){
    bool playerTurn = true;
    bool gameRunning = true;
    bool surprised = false;

    Stack<Enemy> enemies = Enemy.initEnemies();
    Enemy currentEnemy = enemies.Pop(); // Get the current enemy.

    Player player = new Player(50, 5, 10);

    string? input = "";
    Console.Clear();
    
    if(surprised && playerTurn){
        playerTurn = false;
        Console.WriteLine("You were surprised! Lost a turn");
    }

    printStartMenu();
    do{
        printMenu(playerTurn, player);
        input = Console.ReadLine();
        if (input == null){ 
            Environment.Exit(-1);
        }else{
            input = input.ToLower();
        }
        
        Console.Clear();
        if(playerTurn){
            bool validInput = false;
            switch (input){
                case "attack":
                case "atk":
                case "1":
                case "at":
                    player.Attack(currentEnemy);
                    validInput = true;
                    break;
                default:
                    Console.WriteLine($"{input}, is not a valid command. You lost a turn.");
                    break;
            }
        }
        // ensure we can always quit.
        if(input == "exit" || input == "quit"){
            Environment.Exit(0);
        }
        if(!playerTurn){
            // enemy turn
            currentEnemy.Attack(player);
        }

        // post-turn cleanups
        playerTurn = !playerTurn;

        if(currentEnemy.IsDead()){
            Console.WriteLine("You killed the enemy!");
            if(enemies.Count > 0){
                currentEnemy = enemies.Pop();
                Console.WriteLine("Another enemy appeared from the shadows!");
                playerTurn = true;
                surprised = ((new Random().Next(100) % 50) == 0);
            }
        }

        // post-enemy-turn
        if(player.IsDead()){
            Console.WriteLine("You died!");
            gameRunning = false;
        }
    }while (gameRunning);
};




start();