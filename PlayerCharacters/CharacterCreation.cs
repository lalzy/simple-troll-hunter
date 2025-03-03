static class CharacterCreation{
    static public void StartCharacterCreation(){
        Display.CharacterSelectionMenu();
        // Globals.Player = new Archer();
        Globals.Player = new Knight();
        // Globals.Player = new Player(50, 5, 10);
    }
}