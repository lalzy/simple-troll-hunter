class Archer : Player{
    public Archer(int hp = 30, int minDamage = 1, int maxDamage = 5, int rangedMin = 5, int rangedMax = 10) : base(hp, minDamage, maxDamage)
    {
        this.Class = Classes.archer;
        this.SetHealth(hp);
        this.MinDamage = minDamage;
        this.MaxDamage = maxDamage;
        Inventory.AddItem("food", 3);
        Inventory.AddItem("torch", 1);
        Inventory.CreateItem("arrows", 20, 20);
    }
}