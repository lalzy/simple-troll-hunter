/// <summary>
/// We maintain text here for easy expanding/rewriting.
/// </summary>
class Display{
    public static string GetInput(){
        string? input = Console.ReadLine();
        Console.Clear();
        if(string.IsNullOrEmpty(input)) return " ";
        else return input;
        
    }
    public static void PressAnyKey(){
        Console.WriteLine("press Enter key to continue");
    }

    public static string GetKillMessage(Enemy enemy, Weapon? weapon = null){
        weapon = weapon == null ? Globals.Player.GetMainWeapon() : weapon;
        bool instantDeath = enemy.InstantKilled;
        string name = enemy.Name;
        switch(weapon.Type){
            case Weapon.WeaponType.axe:
            return instantDeath ? $"You split the {name} in half!" : $"You sink your axe straight into the {name}!";
            case Weapon.WeaponType.sword:
            return instantDeath ? $"You sever the {name}'s arm!" : $"You cut open the {name}!";
            case Weapon.WeaponType.dagger:
            return instantDeath ? $"You slit the {name}'s throat!" : $"You plant your dagger ino the {name}'s torsor!";
            case Weapon.WeaponType.staff:
            return instantDeath ? $"You smash the skull of the {name}" : $"Somehow, the {name} evaporate!";
            case Weapon.WeaponType.mace:
            return instantDeath ? $"You smash the {name} to pieces." : $"You crushed the skull of the {name}!";
            case Weapon.WeaponType.bow:
            return instantDeath ? $"Lucky shot! It went straight through it's eye!" : $"Your arrow strikes true into the {name}, as it keels over.";
        }
        return "";
    }


    public static string WeaponText(Weapon weapon, bool capitalizeFirst = true){
        string toReturn = "Bare hands";
        if(weapon == null){
            toReturn = "bare hands";
        }else{
            switch(weapon.Type){
                case Weapon.WeaponType.axe: 
                toReturn = "axe";
                break;
                case Weapon.WeaponType.sword:
                toReturn = "sword";
                break;
                case Weapon.WeaponType.mace:
                toReturn = "mace";
                break;
                case Weapon.WeaponType.dagger:
                toReturn = "dagger";
                break;
                case Weapon.WeaponType.staff:
                toReturn = "staff";
                break;
                case Weapon.WeaponType.shield:
                toReturn = "shield";
                break;
            }
        }
        // Returns the string capitalized if capitalized, or not if not.
        return capitalizeFirst ? char.ToUpper(toReturn[0]) + toReturn.Substring(1) : toReturn;
    }

    public static string WeaponText(bool capitalizeFirst = true){
        Weapon? weapon = Globals.Player.GetMainWeapon();
        return WeaponText(weapon, capitalizeFirst);
    }

    public static void ChooseCharacter(){
        Console.WriteLine("Pick a character to play as:");
    }

    private static string KnightDetails(){
        return "50HP, Inventory[Shield, 1 tool, 1 torch, 3 food]\n"+
               "    - Moderate melee damage, low ranged damage";
    }

    private static string ArcherDetails(){
        return "30HP, Inventory[20 arrows, 3 food, 1 torch]\n"+
                "    - Can shoot arrows outside of first turn. Low Melee damage, high Ranged damage";
    }

    private static string BerserkerDetails(){
        return "60HP, Inventory[Nothing]\n"+
                "    - Can't use shields (so can't blocK), High Melee damage, can't use arrows.";
    }

    private static string MagicianDetails(){
        return "30HP, Inventory[1 food, 1 torch]]\n" +
                "    - Starts with 5 of each spell. Low damage, can't use arrows";
                // "      Has a spell that lights up, giving you 5 rooms of light";
    }

    public static void CharacterSelectionMenu(){
        Console.WriteLine("1 - Knight");
        Console.WriteLine("   " + KnightDetails());

        Console.WriteLine("2 - Archer");
        Console.WriteLine("   " + ArcherDetails());

        Console.WriteLine("3 - Berserker");
        Console.WriteLine("    " + BerserkerDetails());

        Console.WriteLine("4 - Magician");
        Console.WriteLine("    " + MagicianDetails());
    }

    public static void SurprisedMessage (){
        Console.WriteLine("You were surprised, and failed to act!");
    }

    public static string ShieldConditionText(){
        if(Globals.Player.HasShield()){
            switch (Globals.Player.Equipment.OffHand.MinAttribute){
                case 0:
                    return "You have no shield";
                case 1:
                    return "It's on it's last legs";
                case 2:
                    return "It's seen some use";
                default:
                    return "It's brand new!";
            }
        }else{
            return "You have no shield";
        }
    }
    static public void ChargeUp(){
        Console.WriteLine("You get ready to strike hard next turn!");
    }
    static public void ThrewTorch(){
        Console.WriteLine("You threw the torch at the enemy, setting it aflame!");
    }

    public enum StunCause{
        torch = 0,
        arrow = 1,
        freeze = 2,
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
        Console.WriteLine(enemy.DeathText);
        Console.WriteLine("press {any} key to continue.");
        Console.ReadKey();
        Console.Clear();
    }

    static public void CastSpell(Enemy enemy){
        Console.WriteLine($"You roast the {enemy.Name} alive");
    }

    static public void ShootArrow(){
        if(Globals.Player.Perks.Contains(Player.PerksEnum.BowMaster)){
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
            Console.WriteLine("From the darkness ahead, a massive black troll emerges!");
            Console.WriteLine("Its presence is overwhelming—this will be your toughest fight yet!");
        }else if (enemy.GetType() == typeof(GoblinBoss)){
            Console.WriteLine("A towering Goblin Boss blocks your path, eyes gleaming with malice!");
        }else{
            Console.WriteLine($"an {enemy.Name} emerge from the shadows!");
        }
    }

    static public void PlayerHUD(bool playerTurn, Enemy enemy, Player player){
        if(playerTurn){
            Console.WriteLine("----------------------------");
            Console.WriteLine($"You're facing a, <{enemy.Name}>");
            PrintState(enemy);
            Console.WriteLine("----------------------------");
            Console.WriteLine($"Your Health: {player.Health}");
        }
    }

    static public void AmbushImmuneProgress(int AmbushImmuneDuration){
        if(AmbushImmuneDuration - 1 == 0){
            Console.WriteLine("The light fades and you're surrounded by darkness again.");
        }else if (AmbushImmuneDuration - 1 == 1){
            Console.WriteLine("The light begins to fade, it'll last for one more room");
        }
    }

    static public void MagicMenu(bool exploreOnly = false){
        Player player = Globals.Player;
        Spell? fireball = player.GetSpell(Spell.ValidSpells.Fireball);
        Spell? freeze = player.GetSpell(Spell.ValidSpells.Freeze);
        Spell? shield = player.GetSpell(Spell.ValidSpells.ShieldSpell);
        Spell? light = player.GetSpell(Spell.ValidSpells.light);
        if(!exploreOnly){
            if(fireball != null && fireball.Amount > 0){
                Console.WriteLine($"{(int) Spell.ValidSpells.Fireball + 1} - Fireball");
                Console.WriteLine($"   - {fireball.Amount} Damages the enemy with high-damage");
            }
            if(freeze != null && freeze.Amount > 0){
                Console.WriteLine($"{(int) Spell.ValidSpells.Freeze + 1} - Freeze");
                Console.WriteLine($"   - {freeze.Amount} Freezes (stuns) the enemy temporarily");
            }
            if(shield != null && shield.Amount > 0){
                Console.WriteLine($"{(int) Spell.ValidSpells.ShieldSpell + 1} - Shield");
                Console.WriteLine($"   - {shield.Amount}");
            }
        }else {
            if(light != null && light.Amount > 0){
                Console.WriteLine($"{(int) Spell.ValidSpells.light + 1} - Light");
                Console.WriteLine($"   - Does nothing in combat, but prevent ambush at start");
            }
        }
    }

    static public bool PlayerMenu(bool playerTurn, Enemy currentEnemy, Player player){
        bool atLeastOneValid = false;
        PlayerHUD(playerTurn, currentEnemy, player);
        if(player.Stunned && playerTurn){
            player.ProgressStunned();
            playerTurn = false;
            Console.WriteLine("You were surprised! Lost a turn");
        }
        Console.WriteLine("-------------------------");
        if(playerTurn){
            atLeastOneValid = true;
            Console.WriteLine($"[A]ttack with your {WeaponText()}!");
            if(player.Inventory.GetItem(Inventory.Items.torch).Amount > 0){
                Console.WriteLine("[T]hrow your torch at the enemy!");
            }
            if((Combat.FirstTurn || player.Perks.Contains(Player.PerksEnum.BowMaster)) && player.Inventory.GetItem(Inventory.Items.arrows).Amount > 0){
                Console.WriteLine("[S]hoot an arrow");
                Console.WriteLine($"     Arrows: {player.Inventory.GetItem(Inventory.Items.arrows).Amount}");
            }
            if(player.Perks.Contains(Player.PerksEnum.ChargeUp) && player.ExtraDamage == 0){
                Console.WriteLine("[P]ower up your next attack increasing the damage");
            }
            if(player.SpellCount() > 0){
                Console.WriteLine("[M]agic menu");
            }
        }
        if(player.CanBlock() && !player.IsBlocking && !Globals.PlayerTurn && !Globals.Player.Stunned){
            atLeastOneValid = true;
            Console.WriteLine("[B]lock with your shield.");
            Console.WriteLine($"     Shield Condition:{ShieldConditionText()}");
        }
        if(atLeastOneValid){
            string text = playerTurn ? "skip" : "continue";
            Console.WriteLine($"press enter to {text}");
            
            Console.Write(">> ");
        }
        return atLeastOneValid;
    }

    public static void MagicMenuEnterText(){
        Console.WriteLine("[M] - to show spell menu.");
    }


    public static void PlayerAttack(int damage){
        Weapon? weapon = Globals.Player.GetMainWeapon();
        if(weapon == null){
            Console.WriteLine("You have no weapon to attack with");
        }else{
            if (damage <= (weapon.MaxAttribute / 4)){
                Console.WriteLine("You barely scratched it..");
            }else if (damage <= (weapon.MaxAttribute/ 2)){
                Console.WriteLine("An alright hit");
            }else if(damage == weapon.MaxAttribute){
                Console.WriteLine("A perfect hit!");
            }else{
                Console.WriteLine("A good hit!");
            }
        }
    }

    public static void TakeDamagePrint(int damage){
        Enemy? enemy = Enemy.CurrentEnemy;
        if (enemy != null){
            if (damage >= enemy.MaxDamage * .90){
                Console.WriteLine("It hits you with all it's might!");
            }else if (damage >= enemy.MaxDamage * .50){
                Console.WriteLine("It managed a good hit on you");
            }else if(damage >= enemy.MaxDamage * .25){
                Console.WriteLine("It seems to have stumbled and hit off it's mark!");
            }else{
                Console.WriteLine("It barely managed to swing the weapon!");
            }
        }
    }

    static public void ShieldBlockMessage(){
        Player player = Globals.Player;
        Weapon? offHand = player.Equipment.OffHand;
        if (player.HasShield()){
            if(player.HasShield() &&  player.Equipment.OffHand.MinAttribute == 1){
                Console.WriteLine("You get ready to block with what remains of your shield, better make it count!");
            }else if(!player.HasShield() || player.Equipment.OffHand.MinAttribute == 0){
                Console.WriteLine("There's nothing left of the shield..");
            }else{
                Console.WriteLine("You get ready to block with the shield");
            }
        }else{
                Console.WriteLine("block with what? You have no shield.");
        }
    }

    static public void BlockedEnemyMessage(){
        Console.WriteLine("You blocked the attack!");
    }

    static public void VictoryMessage (){
        Player player = Globals.Player;
        Console.WriteLine("The sound of the troll's massive body hitting the stone floor echoes throughougt the chamber.");
        if(player.Health < (player.BaseHealth / 4)){
            Console.WriteLine("Bloody and beaten, you barely stand, but even so, you did it, you won!");
        }else if (player.Health < (player.BaseHealth / 2)){
            Console.WriteLine("It was a thought battle, but you remain standing, with scars to prove your victory!");
        }else if(player.Health >= player.BaseHealth){
            Console.WriteLine("You look at the dead corpse, it never got a chance to even scratch you.");
            Console.WriteLine("Is this really all? This is what has been terrorizing the locals for so long?");
        }else{
            Console.WriteLine("With a few cuts and bruises, you stand victorious. It wasn't easy, but you've triumphed over the threat.");
        }

        Console.WriteLine("You leave the old, now empty keep behind, your job done.");
    }


    static public void TieMessage(){
        Console.WriteLine("but, you died..");
    }

    static public void LoseMessage(){
        switch(new Random().Next(5)){
            case 0:
                Console.WriteLine("The light fades from your eyes as the darkness takes hold. You fought valiantly, but this battle was not yours to win.");
            return;
            case 1:
                Console.WriteLine("Your body succumbs to the wounds, and the world slips away. All sound seems to fade...");

            return;
            case 2:
                Console.WriteLine("A final blow lands, and the ground rushes up to meet you. You’ve given everything, but everything wasn't enough.");

            return;
            case 3:
            Console.WriteLine("With one final, crushing blow, everything goes dark. The keep's secrets remain locked away, as your body lies still upon the cold stone.");
            return;
            case 4:
            Console.WriteLine("The darkness closes in around you. The battle is lost, and your journey ends here, in the depths of the forgotten keep.");

            return;
        }
        Console.WriteLine("You died...");
    }


    static public void DoNothingMessage(){
        Console.WriteLine("You do nothing");
    }

    static public void PrintCaveEntranceMessage(){
        Console.WriteLine("Do you wish to venture inside");
        Console.WriteLine("[Y]es - to enter");
        Console.WriteLine("[N]o - to head back");
    }
    static public void PrintState(Creature creature){
        if(creature.Health == creature.BaseHealth){
            Console.WriteLine("It's not injured at all!");
        }else if(creature.Health >= creature.BaseHealth * .85){
            Console.WriteLine("It's barely injured.");
        }else if (creature.Health >= creature.BaseHealth * .50){
            Console.WriteLine("It's injured.");
        }else if(creature.Health >= creature.BaseHealth * .25){
            Console.WriteLine("It's very injured.");
        }else if(creature.Health <= creature.BaseHealth * .05){
            Console.WriteLine("It's a miracle it's still standing!");
        }else{
            Console.WriteLine("It's close to death!");
        }
    }

    static public void NothingHappened(){
        Console.WriteLine("Nothing happened...");
    }
    static public void PrintWelcomeMessage(){
     Console.WriteLine("Welcome to the Forgotten Keep.");
    Console.WriteLine("This once-grand castle is now overrun by goblins and their ilk.");
    Console.WriteLine("Your job is to clear these ruins and ridding it of its unwanted occupants.");
    Console.WriteLine("Remember, you can always type {exit} or {quit} to leave this forsaken place.");
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
        bow = 3,
    }
    public static class Rooms{
        public static void Empty(bool showMagic){
            switch(new Random().Next(19) + 1){
                case 1:
                    Console.WriteLine("Ratty bedrolls are strewn across the floor, some still warm.");
                break;
                case 2:
                    Console.WriteLine("Looks to be a bathroom.");
                break;
                case 3:
                    Console.WriteLine("Cobwebs, cobwebs everywhere.");
                break;
                case 4:
                    Console.WriteLine("An old storage room, absolutely nothing of value");
                break;
                case 5:
                    Console.WriteLine("Dust hangs thick in the air, disturbed only by your footsteps.");
                break;
                case 6:
                    Console.WriteLine("A single candle flickers weakly, struggling against the darkness.");
                break;
                case 7:
                    Console.WriteLine("Rows of wooden shelves stand empty.");
                break;
                case 8:
                    Console.WriteLine("A broken chair sits in the center, as if someone left in a hurry.");
                break;
                case 9:
                    Console.WriteLine("The walls are covered in crude drawings, mostly depicting goblins stabbing things.");
                break;
                case 10:
                    Console.WriteLine("The floor creaks underfoot, protesting your intrusion.");
                break;
                case 11:
                    Console.WriteLine("A distant whisper seems to fade the moment you try to focus on it.");
                break;
                case 12:
                    Console.WriteLine("A lantern swings slightly from a hook, though there's no sign of wind.");
                break;
                case 13:
                    Console.WriteLine("The lingering scent of sweat and damp fur tells you this room was recently occupied.");
                break;
                case 14:
                    Console.WriteLine("A crude wooden table is littered with half-eaten scraps of questionable meat.");
                break;
                case 15:
                    Console.WriteLine("You spot a game of dice left mid-roll—someone was in a hurry.");
                break;
                case 16:
                    Console.WriteLine("A wooden training dummy, covered in knife marks, leans precariously to one side.");
                break;
                case 17:
                    Console.WriteLine("A pair of small, mismatched weapons rest against the wall, forgotten in the rush.");
                break;
                case 18:
                    Console.WriteLine("Empty bottles are scattered about, the pungent smell of fermented mushrooms hanging in the air.");
                break;
                case 19:
                    Console.WriteLine("Just an empty room...");
                break;
            }

            if(showMagic){
                MagicMenuEnterText();
            }
            string input = GetInput();
            if(showMagic && input[0] == 'm'){
                Globals.Player.Magic(Player.MagicMenusToShow.exploreOnly);
            }
        }

        public static bool NoSelection(bool[] validSelections){
            foreach (bool selection in validSelections)
            {
                if(selection){
                    return false;
                }
            }
            return true;
        }

        public static void InspectSwordDamage(){
            Weapon? weapon = Globals.Player.GetMainWeapon();
            if(weapon != null){
                Console.WriteLine($"Dmg: {weapon.MinAttribute} - {weapon.MaxAttribute}");
            }
        }

        public static void SwordNothing(){
            Console.WriteLine("You made no difference");
        }

        public static void SwordSharpened(){
            Console.WriteLine("You sharpened it!");}

        public static void SwordBroke(){
            Console.WriteLine($"You botched it... the {WeaponText()} is worse now");}

        public static void SharpenMenu(){
            Console.WriteLine("1 - Attempt to tune up the blade (min-damage)");
            Console.WriteLine("2 - Attempt to Sharpen the blade (max-damage)");
        }

        public static void BlackSmithMenu(bool showMagic){
            Console.WriteLine("You see a sharpener.\n");
            if(showMagic){
                MagicMenuEnterText();
            }
            Console.WriteLine($"1 - Try to sharpen your {WeaponText()}?");
            Console.WriteLine($"2 - Inspect your {WeaponText()} (min/max dmg)");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>If player can pick up food or not</returns>
        public static bool DiscoverKitchen(bool showMagic){
            bool canFillPack = false;
            Item food = Globals.Player.Inventory.GetItem(Inventory.Items.food);

            Console.WriteLine("You see a kitchen, filled with food");

            if(showMagic){
                MagicMenuEnterText();
            }
            Console.WriteLine("1 - Eat until stuffed (+5 hp)");
            if(food.Amount < food.MaxAmount){
                Console.WriteLine("2 - Fill your pack");
                canFillPack = true;
            }
            return canFillPack;
        }
        public static void FoodFull(){
            Console.WriteLine("You don't have enough space to pick up any food!");
        }

        /// <summary>
        /// Armory handling
        /// </summary>
        /// <param name="newShieldCon"></param>
        /// <returns>Array of booleans of if the item is valid or not:
        // [shield, torch, arrows]
        // </returns>
        public static bool[] DiscoverArmory(int newShieldCon, bool showMagic){
            Inventory inventory = Globals.Player.Inventory;
            Equipment equipment = Globals.Player.Equipment;
            Item torch = inventory.GetItem(Inventory.Items.torch);
            Item arrows = inventory.GetItem(Inventory.Items.arrows);
            bool[] validSelections = new bool[]{false, false, false, false};
            
            Console.WriteLine("It's an armory!");
            if(showMagic){
                MagicMenuEnterText();
            }
            if(Globals.Player.Perks.Contains(Player.PerksEnum.CanUseShield)){
                validSelections[(int) ValidItemChoices.shield] = true;
                    Console.WriteLine("[1] You see a shield against the wall.");
                if(Globals.Player.HasShield()){
                    if(equipment.OffHand.MinAttribute < newShieldCon){
                        Console.WriteLine("  And it's of better quality than what you have!");
                    }else{
                        Console.WriteLine("  But what you have is better!");
                    }
                }else if (equipment.OffHand != null){
                    Console.WriteLine($"   Replaces your {WeaponText(equipment.OffHand)} for the shield.");
                }
            }
            if(torch.Amount == 0){
                validSelections[(int) ValidItemChoices.torch] = true;
                Console.WriteLine($"[2] You see an unlit torch. pick it up?");
            }
            if(Globals.Player.HasBow() && arrows.Amount < arrows.MaxAmount){
                validSelections[(int) ValidItemChoices.arrows] = true;
                Console.WriteLine($"[3] You see some arrows on a table.");
            }
            
            if(!Globals.Player.HasBow() && Globals.Player.Perks.Contains(Player.PerksEnum.CanFindBow)){
                validSelections[(int) ValidItemChoices.bow] = true;
                Console.WriteLine($"[4] You see a shortbow on the table.");
                Weapon? offHand = Globals.Player.Equipment.OffHand;
                if(offHand != null){
                    Console.WriteLine($"    note: you'll be replacing your {WeaponText(offHand)}!");
                }
            }
            
            if(NoSelection(validSelections)){
                Console.WriteLine("But there's nothing you can use");
            }

            return validSelections;
        }

        public static void MagicRest(){
            Console.WriteLine("You restore some of your magic");
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
            }if(Globals.Player.HasShield() &&  Globals.Player.ShieldDamaged()
                                    && inventory.GetItem(Inventory.Items.tools).Amount > 0){
                hasSelection = true;
                Console.WriteLine("[F]ix your shield.");
                Console.WriteLine($"   Tools available: {inventory.GetItem(Inventory.Items.tools).Amount}");
            }
            if(Globals.Player.SpellCount() > 0 && Globals.Player.Perks.Contains(Player.PerksEnum.Meditation)){
                hasSelection = true;
                Console.WriteLine("[M]to restore some of your spells");
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
        public static void MaicRestFail(){
            Console.WriteLine("You do not have the ability to meditate!");
        }
    }

}