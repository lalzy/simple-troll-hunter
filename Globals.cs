/// <summary>
/// Configurational values that's used across the entire game, and instances of the player.
/// </summary>
class Globals{
    /// <summary>
    /// The chance of the player being surprised when an enemy spawns.
    /// </summary>
    public static int SurprisedChance = 50;   
    /// <summary>
    /// The player object.
    /// </summary>
    public static Player Player;
    /// <summary>
    /// Wether or not it's the player's turn to act in combat.
    /// </summary>
    public static bool PlayerTurn;
}