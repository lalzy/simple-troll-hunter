class Goblin : Enemy{
    public Goblin(int health, int minDamage, int maxDamage)
        : base(health, minDamage, maxDamage) {
        this.Health = health;
        this.MinDamage = minDamage;
        this.MaxDamage = maxDamage;
    }
}