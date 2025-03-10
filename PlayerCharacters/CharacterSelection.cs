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
                case 3:
                    return CreateBerserker();
                case 4:
                    return CreateMagician();
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

    private static Player CreateBerserker(){
        Player player = new Player(60, 8, 15);
        player.Inventory.CreateItem(Inventory.Items.food, 0, 2);
        player.Abilities.Add(Player.AbilityEnum.ChargeUp);
        player.Inventory.AddItem(Inventory.Items.torch, 1);
        player.Inventory.CreateItem(Inventory.Items.shield, 0, 0);
        player.Inventory.CreateItem(Inventory.Items.arrows, 0, 0);
        player.Inventory.CreateItem(Inventory.Items.Fireball, 0, 0);
        player.Inventory.CreateItem(Inventory.Items.Freeze, 0, 0);
        player.Inventory.CreateItem(Inventory.Items.ShieldSpell, 0, 0);
        return player;
    }

    private static Player CreateMagician (){
        Player player = new Player(15, 5, 5);
        player.Inventory.CreateItem(Inventory.Items.tools, 0, 0);
        player.Inventory.CreateItem(Inventory.Items.arrows, 0, 0);
        player.Inventory.CreateItem(Inventory.Items.shield, 0, 3);
        player.Inventory.CreateItem(Inventory.Items.food, 0, 1);
        player.Inventory.CreateItem(Inventory.Items.Fireball, 10, 20);
        player.Inventory.CreateItem(Inventory.Items.Freeze, 10, 20);
        player.Inventory.CreateItem(Inventory.Items.ShieldSpell, 5, 20);
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