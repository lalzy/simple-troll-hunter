using System.Runtime;

class Item{
    private int _amount;
    public int MaxAmount;
    public Item(int amount, int maxAmount = 3){
        this.MaxAmount = maxAmount;
        AddItem(amount);
    }

    public void AddItem(int amount){
        _amount += amount;
        if(_amount > MaxAmount) _amount = MaxAmount;
    }
}