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
        Player player = new Player(30);
        //player.Class = Player.Classes.archer;
        player.Equipment.MainHand = new Weapon(1, 5, Weapon.WeaponType.dagger);
        player.Equipment.OffHand = new Weapon(5, 10, Weapon.WeaponType.bow);
        player.Perks.Add(Player.PerksEnum.BowMaster);
        player.Inventory.AddItem(Inventory.Items.food, 3);
        player.Inventory.AddItem(Inventory.Items.torch, 1);
        player.Inventory.CreateItem(Inventory.Items.arrows, 20, 20);
        return player;
    }

    private static Player CreateBerserker(){
        Player player = new Player(60);
        player.Perks.Add(Player.PerksEnum.ChargeUp);
        player.Equipment.MainHand = new Weapon(8, 15, Weapon.WeaponType.axe, true);
        player.Inventory.CreateItem(Inventory.Items.food, 0, 2);
        player.Inventory.AddItem(Inventory.Items.torch, 1);
        player.Inventory.CreateItem(Inventory.Items.arrows, 0, 0);
        return player;
    }

    private static Player CreateMagician (){
        Player player = new Player(15);
        player.Equipment.MainHand = new Weapon(5, 5, Weapon.WeaponType.staff);
        player.Inventory.CreateItem(Inventory.Items.tools, 0, 0);
        player.Inventory.CreateItem(Inventory.Items.arrows, 0, 0);
        player.Inventory.CreateItem(Inventory.Items.food, 1, 2);
        player.Inventory.AddItem(Inventory.Items.torch); // Replace for Light-spell later.

        player.Spells = new List<Spell>{
            new Spell(Spell.ValidSpells.Fireball, 5, 5),
            new Spell(Spell.ValidSpells.Freeze, 5, 5),
        };
        return player;
    }

    private static Player CreateKnight(){
        Player player = new Player(50);
        // player.Class = Player.Classes.knight;
        player.Perks.Add(Player.PerksEnum.CanUseShield);
        player.Equipment.OffHand = new Weapon(3, 3, Weapon.WeaponType.shield);
        player.Equipment.MainHand = new Weapon(5, 10, Weapon.WeaponType.sword);
        player.Inventory.AddItem(Inventory.Items.food, 3);
        player.Inventory.AddItem(Inventory.Items.torch, 1);
        player.Inventory.AddItem(Inventory.Items.tools, 1);

        return player;
    }
}