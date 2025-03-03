class Knight : Player{
    public Knight(int hp = 50, int minDamage = 5, int maxDamage = 10) : base(hp, minDamage, maxDamage)
    {
        this.Class = Classes.knight;
        this.SetHealth(hp);
        this.MinDamage = minDamage;
        this.MaxDamage = maxDamage;
        Inventory.AddItem("food", 3);
        Inventory.AddItem("shield", 3);
        Inventory.AddItem("tools", 1);
        Inventory.AddItem("torch", 1);
    }
}