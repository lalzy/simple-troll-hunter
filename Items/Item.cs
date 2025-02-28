using System.Runtime;

public class Item{
    private int _amount;
    public int MaxAmount;
    public Item(int amount, int maxAmount = 3){
        this.MaxAmount = maxAmount;
        AddItem(amount);
    }

    public int GetItemCount(){
        return _amount;
    }

    public bool UseItem(){
        if(_amount > 0){
            _amount--;
            return true;
        }else{
            return false;
        }
    }

    public void AddItem(int? amount){
        if(amount == null) amount = MaxAmount;
        _amount += (int) amount;
        if(_amount > MaxAmount) _amount = MaxAmount;
    }
}