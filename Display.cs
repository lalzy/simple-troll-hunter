    class Display{
        static public void CreatureDiesMessage(Enemy enemy){
            Console.WriteLine($"You killed the {enemy.Name}!");
            Console.WriteLine("press {any} key to continue.");
            Console.ReadKey();
            Console.Clear();
        }

        static public void CreatureDiesMessage(Player player){
            Console.WriteLine("Player died!");
        }

        static public void SpawnEnemyText(Enemy enemy){
            Console.WriteLine($"{enemy.Name} Appears!");
        }

        static public void PlayerMenu(bool playerTurn, Enemy currentEnemy, Player player){
            if(playerTurn){
                Console.WriteLine($"You're facing a, <{currentEnemy.Name}>");
            }
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
            }
            Console.WriteLine("[C]heck your [S]tatus");
            string text = playerTurn ? "skip" : "continue";
            Console.WriteLine($"press enter to {text}");
            
            Console.Write(">> ");
        }
        static public void ShieldBlockMessage(){
            Console.WriteLine("You blocked the attack!");
        }
}