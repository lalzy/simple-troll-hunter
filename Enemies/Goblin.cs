class Goblin : Enemy{
    public Goblin(int rank){
        if(rank == 0){
            this.Name = "Generic Goblin";
            this.SetHealth(10);
            this.MinDamage = 1;
            this.MaxDamage = 5;
        }else if(rank == 1){
            this.Name = "Though Goblin";
            this.SetHealth(10);
            this.MinDamage = 3;
            this.MaxDamage = 5;
        }else if (rank == 2){
            this.Name = "Shield-wearing Goblin";
            this.SetHealth(15);
            this.MinDamage = 2;
            this.MaxDamage = 5;
        // Goblin Champion - Mini Boss
        }else{
            this.SetHealth(100);
            this.MinDamage = 100;
            this.MaxDamage = 100;
        }
    }
    public Goblin(){
        this.Health = 10;
        this.BaseHealth = 10;
        this.MinDamage = 1;
        this.MaxDamage = 5;
    }
}