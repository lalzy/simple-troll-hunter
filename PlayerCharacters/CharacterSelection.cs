static class CharacterSelection{
    static public Player CharacterCreationInput(){
        while(true){
        Console.Write(">> ");
        int.TryParse(Console.ReadLine().ToString(), out int choice);
            switch(choice){
                case 0:
                    return CharacterCreation.CustomCharacter();
                case 1:
                    return CreateKnight();
                case 2:
                    return CreateArcher();
            }
            Console.WriteLine("Invalid Selection");
        }
    }


    static public void StartCharacterSelection(){
        Display.CharacterSelectionMenu();
        Globals.Player = CharacterCreationInput();
    }

    // Classes

    private static Player CreateArcher(){
        Player player = new Player(30, 1, 5, 5, 10);
        player.Class = Player.Classes.archer;
        player.Inventory.AddItem(Inventory.Items.food, 3);
        player.Inventory.AddItem(Inventory.Items.torch, 1);
        player.Inventory.CreateItem(Inventory.Items.arrows, 20, 20);
        return player;
    }

    private static Player CreateKnight(){
        Player player = new Player(50, 5, 10);
        player.Class = Player.Classes.knight;
        player.Inventory.AddItem(Inventory.Items.food, 3);
        player.Inventory.AddItem(Inventory.Items.shield, 3);
        player.Inventory.AddItem(Inventory.Items.torch, 1);
        player.Inventory.AddItem(Inventory.Items.tools, 1);

        return player;
    }
}