class Goblin : Enemy{
    public Goblin(int rank){
        if(rank == 0){
            this.Name = "Generic Goblin";
            this.Health = 10;
            this.BaseHealth = 10;
            this.MinDamage = 1;
            this.MaxDamage = 5;
        }else if(rank == 1){
            this.Name = "Though Goblin";
            this.Health = 10;
            this.BaseHealth = 10;
            this.MinDamage = 3;
            this.MaxDamage = 5;
        }else if (rank == 2){
            this.Name = "Shield-wearing Goblin";
            this.Health = 15;
            this.BaseHealth = 15;
            this.MinDamage = 2;
            this.MaxDamage = 5;
        }else{
            this.Health = 100;
            this.BaseHealth = 100;
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