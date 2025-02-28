using Microsoft.VisualBasic;

public class Inventory{
    /// <summary>
    /// Not used, yet.
    /// Idea is to keep the string-name of the item(like food, torch, shield) followed by it's 'amount' or 'uses'.
    /// </summary>
    private Dictionary<string, Item> _inventory = new Dictionary<string, Item>();

    public dynamic getItem(string itemName){
        return _inventory[itemName.ToLower()];
    }

    /// <summary>
    /// Use the item (subtracts it's amount)
    /// </summary>
    /// <param name="itemName">name of the item</param>
    /// <returns>If item was successfully used or not</returns>
    public bool UseItem(string itemName){
        var item = _inventory[itemName];
        if(item != null){
            if (item.UseItem()){
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Create a new item-slot in the inventory.
    /// </summary>
    /// <param name="itemName">Item name</param>
    /// <param name="amount">The amount (Will not exceed max for the created item)</param>
    /// <returns>Item Created</returns>
    public dynamic? CreateItem(string itemName, int? amount = 1){
        switch (itemName.ToLower()){
            case "food":
                return new Food(amount == null ? 3 : (int) amount);
        }
        return null;
    }

    /// <summary>
    /// Add Item to the inventory
    /// </summary>
    /// <param name="itemName">name of the item</param>
    /// <param name="amount">How many items to be added (will never exceed max).</param>
    public void AddItem(string itemName,int? amount = null){
        try{
            _inventory[itemName].AddItem(amount);
        }catch (KeyNotFoundException) {
            var item = CreateItem(itemName, amount);
            if(item == null) return;

            _inventory.Add(itemName, CreateItem(itemName, amount));
        }
    }
}