class Goblin : Enemy{
    /// <summary>
    /// Generate a gobling
    /// </summary>
    /// <param name="rank">Rank = The type of goblin we want to spawn. Currently 3 types (index'ed), out of bounds will make an unbeatable goblin</param>
    public Goblin(int rank = 0){
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
}