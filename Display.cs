/// <summary>
/// We maintain text here for easy expanding/rewriting.
/// </summary>
class Display{
    public static void PressAnyKey(){
        Console.WriteLine("press any key to continue");
    }

    public static string ShieldConditionText(){
        switch (Globals.Player.GetShieldHealth()){
            case 0:
                return "You have no shield";
            case 1:
                return "It's on it's last legs";
            case 2:
                return "It's seen some use";
            case 3:
                return "it's brand new!";
            default:
                return "";
        }
    }

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
            Console.WriteLine($"     Shield Condition:{ShieldConditionText()}");
        }
        string text = playerTurn ? "skip" : "continue";
        Console.WriteLine($"press enter to {text}");
        
        Console.Write(">> ");
    }


    public static void PlayerAttack(int damage){
        if (damage <= (Globals.Player.MaxDamage / 4)){
            Console.WriteLine("You barely scratched it..");
        }else if (damage <= (Globals.Player.MaxDamage / 2)){
            Console.WriteLine("An alright hit");
        }else if(damage == Globals.Player.MaxDamage){
            Console.WriteLine("A perfect hit!");
        }else{
            Console.WriteLine("A good hit!");
        }
    }

    public static void TakeDamagePrint(int damage){
        if (damage <= (Globals.Player.MaxDamage / 4)){
            Console.WriteLine("He barely scratched you..");
        }else if (damage <= (Globals.Player.MaxDamage / 2)){
            Console.WriteLine("That hurt");
        }else if(damage == Globals.Player.MaxDamage){
            Console.WriteLine("That's not good...");
        }else{
            Console.WriteLine("You should learn to dodge...");
        }
    }

    static public void ShieldBlockMessage(){
        if(Globals.Player.GetShieldHealth() == 1){
            Console.WriteLine("You get ready to block with what remains of your shield, better make it count!");
        }else if(Globals.Player.GetShieldHealth() == 0){
            Console.WriteLine("block with what? You have no shield anymore.");
        }else{
            Console.WriteLine("You get ready to block with the shield");
        }

    }
    static public void BlockedEnemyMessage(){
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
        Console.WriteLine($"Your shield's condition is: {ShieldConditionText()}");
    }

    static public void PrintCaveEntranceMessage(){
        Console.WriteLine("procceed inside?");
        Console.WriteLine("[Y]es - to enter");
        Console.WriteLine("[N]o - to head back");
    }
    static public void PrintState(Creature? creature){
        if(creature == null){ 
            Console.WriteLine("There is no creature here");
            return;
        }else if(creature.BaseHealth == creature.Health){
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

    public static class Rooms{
        public static void Empty(){
            Console.WriteLine("Room was empty");
        }

        public static void Resting(){
            Console.WriteLine("You rested");
        }

        public static bool RestMenu(int food){
            bool hasSelection = false;
            Inventory inventory = Globals.Player.Inventory;
            Console.WriteLine($"This looks like a great place to rest (Rest available: {food})");
            if(Globals.Player.Health < Globals.Player.BaseHealth){
                hasSelection = true;
                Console.WriteLine("[H]eal - Tend to your wounds");
            }if(Globals.Player.GetShieldHealth() < Globals.Player.GetShieldHealthBase()
                                    && inventory.GetItem("tools").Amount > 0){
                hasSelection = true;
                Console.WriteLine("[F]ix your shield.");
                Console.WriteLine($"   Tools available: {inventory.GetItem("tools").Amount}");
            }
            
            if (!hasSelection){
                Console.WriteLine("But, you have no need to rest!");
                return false;
            }else{
                return true;
            }
        }

        public static void FixShield(){
            Console.WriteLine("You fixed up your shield.");
        }

        public static void InvalidRestSelection(){
            Console.WriteLine("You choose to do nothing.");
        }

        public static void NoFoodText(){
            Console.WriteLine("looks like the perfect spot to rest, unfortunately, you have no more food...");
        }
    }

}