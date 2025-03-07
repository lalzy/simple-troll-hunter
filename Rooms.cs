public class Rooms{
    public static bool Blacksmith(){
        Display.Rooms.BlackSmithMenu();
        int.TryParse(Console.ReadLine(), out int choice);
        Console.Clear();

        switch(choice){
            case 1:
                int min = Globals.Player.MinDamage;
                int max = Globals.Player.MaxDamage;
                int improvement = new Random().Next(-5, 5);
                Display.Rooms.SharpenMenu();
                int.TryParse(Console.ReadLine(), out int sharpenChoice);
                Console.Clear();
                if(sharpenChoice == 1){
                    Globals.Player.MinDamage = Math.Max(0, min + improvement);
                }else if (sharpenChoice == 2){
                    Globals.Player.MaxDamage = Math.Max(0, max + improvement);
                }else{
                    Display.NothingHappened();
                }

                if(improvement < 0){
                    Display.Rooms.SwordBroke();
                }else if (improvement > 0){
                    Display.Rooms.SwordSharpened();
                }
                return false;
            case 2:
                Display.Rooms.InspectSwordDamage();
                return true;
        }
    
        return false;
    }

    public static bool Kitchen(){
        Item food = Globals.Player.Inventory.GetItem(Inventory.Items.food);
        bool canPickUpFood = Display.Rooms.DiscoverKitchen();
        int.TryParse(Console.ReadLine(), out int choice);
        switch(choice){
            case 1:
                Console.WriteLine("You eat he food and feel your health increase!");
                Globals.Player.Health += 5;
                return true;
            break;
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
        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Wether there was an valid selection or not.
    // Used for wether to skip input or not.</returns>
    public static bool Armory(){
        int newShieldCon = new Random().Next(1,3);
        bool[] validSelections = Display.Rooms.DiscoverArmory(newShieldCon);
        // Only prompt if there is something to select
        if(!Display.Rooms.NoSelection(validSelections)){
            Console.Write(">> ");
            string? input = Console.ReadLine();
            if(!String.IsNullOrEmpty(input)){
                int.TryParse(input, out int choice);
                if(validSelections[--choice]){
                    switch(choice){
                        case (int) Display.ValidItemChoices.shield:
                            Globals.Player.Inventory.AddItem(Inventory.Items.shield, newShieldCon);
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
                        default:
                            Display.Rooms.ArmoryDidntChoose();
                        return true;
                    }
                }
            }
        }
        return false;
    }
}