public class Rooms{
    /// <summary>
    /// 
    /// </summary>
    /// <returns>Wether there was an valid selection or not.
    // Used for wether to skip input or not.</returns>
    public static bool Armory(){
        int newShieldCon = new Random().Next(1,3);
        bool[] validSelections = Display.Rooms.DiscoverArmory(newShieldCon);
        // Only prompt if there is something to select
        if(!Display.Rooms.HasNoItemSelection(validSelections)){
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