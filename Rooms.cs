public class Rooms{
    public static bool Libray(){
        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Boolean for if it should skip press-any-key or not</returns>
    public static bool Blacksmith(bool showMagic){
        Display.Rooms.BlackSmithMenu(showMagic);
        Player player = Globals.Player;
        Weapon? weapon = player.GetMainWeapon();
        string? input = Display.GetInput();

        if(input[0] == 'm' && showMagic){
            player.Magic(Player.MagicMenusToShow.exploreOnly);
            return true;

        }else{
            int.TryParse(input, out int choice);
            Console.Clear();
            if(weapon != null){
                switch(choice){
                    case 1:
                        int min = weapon.MinAttribute;
                        int max = weapon.MaxAttribute;
                        // can't break sword if has blacksmithing perk.
                        int improvement =player.Perks.Contains(Player.PerksEnum.blacksmithing) ? new Random().Next(5) : new Random().Next(-2, 5);
                        
                        Display.Rooms.SharpenMenu();
                        int.TryParse(Display.GetInput(), out int sharpenChoice);
                        Console.Clear();
                        if(sharpenChoice == 1){
                            weapon.MinAttribute = Math.Max(0, min + improvement);
                        }else if (sharpenChoice == 2){
                            weapon.MaxAttribute = Math.Max(0, max + improvement);
                        }else{
                            Display.NothingHappened();
                        }
                        
                        weapon.MinAttribute = Math.Min(weapon.MinAttribute, weapon.MaxAttribute);
                        weapon.MaxAttribute = Math.Max(weapon.MinAttribute, weapon.MaxAttribute);

                        if(improvement < 0){
                            Display.Rooms.SwordBroke();
                        }else if (improvement > 0){
                            Display.Rooms.SwordSharpened();
                        }else{
                            Display.Rooms.SwordNothing();
                        }
                        break;
                    case 2:
                        Display.Rooms.InspectSwordDamage();
                        break;
                }
            }
        }
        return false;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns>Boolean for if it should skip press-any-key or not</returns>
    public static bool Kitchen(bool showMagic){
        Item food = Globals.Player.Inventory.GetItem(Inventory.Items.food);
        bool canPickUpFood = Display.Rooms.DiscoverKitchen(showMagic);
        Player player = Globals.Player;
        
        string? input = Display.GetInput();

        if(input[0] == 'm'){
            player.Magic(Player.MagicMenusToShow.exploreOnly);
            return true;

        }else{
            int.TryParse(input, out int choice);
                
            switch(choice){
                case 1:
                    Console.WriteLine("You eat he food and feel your health increase!");
                    Globals.Player.Health += 5;
                    return true;
                case 2:
                    if(food.Amount < food.MaxAmount){
                        food.Amount = food.MaxAmount;
                    }else{
                        Display.Rooms.FoodFull();
                    }
                    return true;
                default:
                    Display.DoNothingMessage();
                    break;
            }
        }
        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Wether there was an valid selection or not.
    // Used for wether to skip input or not.</returns>
    public static bool Armory(bool showMagic){
        Weapon? shield = Globals.Player.Equipment.OffHand;
        Player player = Globals.Player;
        int newShieldCon = 0;
        if(Globals.Player.HasShield()){
            newShieldCon = new Random().Next(0, shield.MaxAttribute);
        }
        bool[] validSelections = Display.Rooms.DiscoverArmory(newShieldCon, showMagic);
        
        string? input = Display.GetInput();

        if(input[0] == 'm'){
            player.Magic(Player.MagicMenusToShow.exploreOnly);
            return true;

        }else{
            // Only prompt if there is something to select
            if(!Display.Rooms.NoSelection(validSelections)){
                Console.Write(">> ");
                if(!String.IsNullOrEmpty(input)){
                    int.TryParse(input, out int choice);
                    if(validSelections[choice]){
                        switch(choice){
                            case (int) Display.ValidItemChoices.shield:
                                if(shield == null && !Globals.Player.Perks.Contains(Player.PerksEnum.CanUseShield)){
                                    shield = new Weapon(new Random().Next(1,3), 3, Weapon.WeaponType.shield);
                                }else if (shield != null){
                                    shield.MinAttribute = newShieldCon;
                                }else{
                                    Console.WriteLine("You can't use shields!");
                                }
                            return true;
                            case (int) Display.ValidItemChoices.torch:
                                Globals.Player.Inventory.AddItem(Inventory.Items.torch);
                            return true;
                            case (int) Display.ValidItemChoices.arrows:
                                Inventory inv = Globals.Player.Inventory;
                                int arrowCount = new Random().Next(1, 3);
                                inv.AddItem(Inventory.Items.arrows, arrowCount);
                                Display.Rooms.PickedUpArrows(arrowCount);
                            return true;
                            case (int) Display.ValidItemChoices.bow:
                                inv = Globals.Player.Inventory;
                                if (inv.GetItem(Inventory.Items.arrows).MaxAmount < 3){
                                    inv.CreateItem(Inventory.Items.arrows, 3, 3);
                                }else{
                                    inv.AddItem(Inventory.Items.arrows, 3);
                                }
                                Globals.Player.Equipment.OffHand = Weapons.GetShortBow();
                                return true;
                            default:
                                Display.Rooms.ArmoryDidntChoose();
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }
}