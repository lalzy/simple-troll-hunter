public class Item{
    public int Amount;
    public int MaxAmount;
    public Item(int amount, int maxAmount = 3){
        this.Amount = amount;
        this.MaxAmount = maxAmount;
    }

    public void AddItem(int amount = 1){
        Amount += amount;
        if(Amount > MaxAmount)
            Amount = MaxAmount;
    }

    public bool UseItem(){
        if (Amount > 0){
            Amount--;
            return true;
        }
        return false;
    }
}