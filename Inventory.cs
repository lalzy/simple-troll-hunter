using Microsoft.VisualBasic;

public class Inventory{

    public enum Items{
        food = 0,
        tools = 2,
        torch = 3,
        arrows = 4,
        
    }

    /// <summary>
    /// Not used, yet.
    /// Idea is to keep the Items-name of the item(like food, torch, shield) followed by it's 'amount' or 'uses'.
    /// </summary>
    public Inventory(){
        CreateItem(Items.food, 0, 3);
        CreateItem(Items.tools, 0, 3);
        CreateItem(Items.torch, 0, 1);
        CreateItem(Items.arrows,0, 3);
    }
    private Dictionary<Items, Item> _inventory = new Dictionary<Items, Item>();

    /// <summary>
    /// Get the requested item from the inventory
    /// </summary>
    /// <param name="itemIdentifier">name of the item</param>
    /// <returns>the item object</returns>
    public Item GetItem(Items itemIdentifier){
        return _inventory[itemIdentifier];
    }

    /// <summary>
    /// Use the item (subtracts it's amount)
    /// </summary>
    /// <param name="itemIdentifier">name of the item</param>
    /// <returns>If item was successfully used or not</returns>
    public bool UseItem(Items itemIdentifier){
        var item = _inventory[itemIdentifier];
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
    /// <param name="itemIdentifier">Item name</param>
    /// <param name="amount">The amount (Will not exceed max for the created item)</param>
    /// <returns>Item Created</returns>
    public void CreateItem(Items itemIdentifier, int amount = 1, int maxAmount = 2){
        if(_inventory.ContainsKey(itemIdentifier)){
            _inventory[itemIdentifier].Amount = amount;
            _inventory[itemIdentifier].MaxAmount = maxAmount;
        }else{
            _inventory.Add(itemIdentifier, new Item(amount, maxAmount));
        }
    }

    /// <summary>
    /// Add Item to the inventory
    /// </summary>
    /// <param name="itemIdentifier">name of the item</param>
    /// <param name="amount">How many items to be added (will never exceed max).</param>
    public void AddItem(Items itemIdentifier,int amount = 1){
        _inventory[itemIdentifier].AddItem(amount);
    }
}