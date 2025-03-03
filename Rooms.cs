public class Rooms{
    public static void Armory(){
        int newShieldCon = new Random().Next(1,3);
        bool[] validSelections = Display.Rooms.DiscoverArmory(newShieldCon);
        Console.Write(">> ");
        string? input = Console.ReadLine();
        if(input != null){
            int.TryParse(input, out int choice);
            if(validSelections[--choice]){
                switch(choice){
                    case (int) Display.ValidItemChoices.shield:
                        Globals.Player.Inventory.GetItem("shield").Amount = newShieldCon;
                    return;
                    case (int) Display.ValidItemChoices.torch:
                        Globals.Player.Inventory.AddItem("torch");
                    return;
                    case (int) Display.ValidItemChoices.arrows:
                        Inventory inv = Globals.Player.Inventory;
                        int arrowCount = new Random().Next(1, inv.GetItem("arrows").MaxAmount - inv.GetItem("arrows").Amount);
                        inv.AddItem("arrows", arrowCount);
                        Display.Rooms.PickedUpArrows(arrowCount);
                    return;
                    default:
                        Display.Rooms.ArmoryDidntChoose();
                    return;

                }
            }
        }
    }
}