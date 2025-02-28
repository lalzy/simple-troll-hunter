public class Item{
    public int Amount;
    public int MaxAmount;
    public Item(int amount, int maxAmount = 3){
        this.Amount = amount;
        this.MaxAmount = maxAmount;
    }

    public void AddItem(int? amount = 1){
        int add = amount == null ? MaxAmount : (int) amount;
        int added = Amount + add;
        Amount = added > MaxAmount ? MaxAmount : added;
    }

    public bool UseItem(){
        if (Amount > 0){
            Amount--;
            return true;
        }
        return false;
    }
}