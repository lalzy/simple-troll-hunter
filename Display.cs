/// <summary>
/// We maintain text here for easy expanding/rewriting.
/// </summary>
class Display{
    public static void PressAnyKey(){
        Console.WriteLine("press any key to continue");
    }

    public static void KnightDetails(){
        Console.WriteLine("Starts with a shield, 50HP, 1 set of tools, 1 torch, and 3 pieces of food.");
    }

    public static void ArcherDetails(){
        Console.WriteLine("Starts with 20 arrows, Higher base-damage for arrows than knight. 30HP, can shoot outside of first-turn");
    }

    public static void CharacterSelectionMenu(){
        Console.WriteLine("1 - Knight");
        Console.WriteLine("2 - Archer");
    }

    public static void SurprisedMessage (){
        Console.WriteLine("You were surprised, and failed to act!");
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

    static public void ThrewTorch(){
        Console.WriteLine("You threw the torch at the enemy, setting it aflame!");
    }

    public enum StunCause{
        torch = 0,
        arrow = 1,
    }
    static public void EnemyIsStunned(Enemy enemy){
        switch(enemy.StunCause){
            case StunCause.torch:
                Console.WriteLine("Enemy is still figthing the flames!");
            return;
            case StunCause.arrow:
            return;
        }
    }

    static public void CreatureDiesMessage(Enemy enemy){
        Console.WriteLine($"You killed the {enemy.Name}!");
        Console.WriteLine("press {any} key to continue.");
        Console.ReadKey();
        Console.Clear();
    }

    static public void ShootArrow(){
        if(Globals.Player.Class == Player.Classes.archer){
            Console.WriteLine("You dodge back as you shoot an arrow. You're too quick for the enemy!");
        }else{
            Console.WriteLine("You shot an arrow at the enemy!");
        }
    }
    static public void NoArrows(){
        Console.WriteLine("You notice you have no arrows...");
    }
    static public void CanOnlyDoFirstCombatTurn(){
        Console.WriteLine("The enemy counters you (It's too close)!");
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

    static public void PlayerHUD(bool playerTurn, Enemy enemy, Player player){
        if(playerTurn){
            Console.WriteLine($"You're facing a, <{enemy.Name}>");
        }
        Console.WriteLine($"HP: {player.Health}");
    }

    static public void PlayerMenu(bool playerTurn, Enemy currentEnemy, Player player){
        PlayerHUD(playerTurn, currentEnemy, player);
        if(player.Stunned && playerTurn){
            player.ProgressStunned();
            playerTurn = false;
            Console.WriteLine("You were surprised! Lost a turn");
        }
        Console.WriteLine("-------------------------");
        if(playerTurn){
            Console.WriteLine("[A]ttack with your sword!");
            Console.WriteLine("[E]xamine the enemy, see it's condition.");
            if(player.Inventory.GetItem(Inventory.Items.torch).Amount > 0){
                Console.WriteLine("[T]hrow your torch at the enemy!");
            }
            if((Combat.FirstTurn || player.Class == Player.Classes.archer) && player.Inventory.GetItem(Inventory.Items.arrows).Amount > 0){
                Console.WriteLine("[S]hoot an arrow");
                Console.WriteLine($"     Arrows: {player.Inventory.GetItem(Inventory.Items.arrows).Amount}");
            }
        }
        if(player.GetShieldHealth() > 0 && !player.IsBlocking && !Globals.Player.Stunned){
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

    public enum ValidItemChoices{
        shield = 0,
        torch = 1,
        arrows = 2,
    }
    public static class Rooms{
        public static void Empty(){
            switch(new Random().Next(5)){
                case 0:
                    Console.WriteLine("Just an empty room...");
                return;
                case 1:
                    Console.WriteLine("A goblin bedchamber. Thankfully, it's empty.");
                return;
                case 2:
                    Console.WriteLine("Looks to be a bathroom.");
                return;
                case 3:
                    Console.WriteLine("Cobwebs, cobwebs everywhere.");
                return;
                case 4:
                    Console.WriteLine("An old storage room, absolutely nothing of value");
                return;
            }
        }

        public static bool HasNoItemSelection(bool[] validSelections){
            foreach (bool selection in validSelections)
            {
                if(selection){
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Armory handling
        /// </summary>
        /// <param name="newShieldCon"></param>
        /// <returns>Array of booleans of if the item is valid or not:
        // [shield, torch, arrows]
        // </returns>
        public static bool[] DiscoverArmory(int newShieldCon){
            Inventory inventory = Globals.Player.Inventory;
            Item shield = inventory.GetItem(Inventory.Items.shield);
            Item torch = inventory.GetItem(Inventory.Items.torch);
            Item arrows = inventory.GetItem(Inventory.Items.arrows);
            bool[] validSelections = new bool[]{false, false, false};
            
            Console.WriteLine("It's an armory!");
            if(shield.Amount < shield.MaxAmount){
                validSelections[(int) ValidItemChoices.shield] = true;
                Console.Write($"[1] You see a shield against the wall.");
                if(shield.Amount < newShieldCon){
                    Console.WriteLine("It's in a better condition than yours");
                }else{
                    Console.WriteLine("But your shield is in a better condition.");
                }
            }
            if(torch.Amount == 0){
                validSelections[(int) ValidItemChoices.torch] = true;
                Console.WriteLine($"[2] You see an unlit torch. pick it up?");
            }
            if(arrows.Amount < arrows.MaxAmount){
                validSelections[(int) ValidItemChoices.arrows] = true;
                Console.WriteLine($"[3] You see some arrows on a table.");
            }
            
            if(HasNoItemSelection(validSelections)){
                Console.WriteLine("But there's nothing you can use");
            }

            return validSelections;
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
                                    && inventory.GetItem(Inventory.Items.tools).Amount > 0){
                hasSelection = true;
                Console.WriteLine("[F]ix your shield.");
                Console.WriteLine($"   Tools available: {inventory.GetItem(Inventory.Items.tools).Amount}");
            }
            
            if (!hasSelection){
                Console.WriteLine("But, you have no need to rest!");
                return false;
            }else{
                return true;
            }
        }

        public static void PickedUpArrows(int arrowCount){
            Console.WriteLine($"You picked up {arrowCount} arrow{(arrowCount > 1 ? "s" : "")}");
        }

        public static void ArmoryDidntChoose(){
            Console.WriteLine("You didn't pick up anything.");
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