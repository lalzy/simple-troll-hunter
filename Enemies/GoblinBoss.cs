class GoblinBoss : Enemy{
    public GoblinBoss(int index = 0){
        switch(index){
            case 0:
                this.Name = "Goblin Champion";
                this.SetHealth(30);
                this.MinDamage = 3;
                this.MaxDamage = 15;
            break;
            default:
                this.SetHealth(50);
                this.MinDamage = 10;
                this.MaxDamage = 25;
            break;
        }
    }
}