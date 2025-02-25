class GoblinBoss : Enemy{
    public GoblinBoss(int index = 0, bool randomizedBoss = false){
        if(randomizedBoss){
            Random rnd = new Random();
            this.Name = "Berserker Champion!";
            this.SetHealth(rnd.Next(15, 30));
            this.MinDamage = rnd.Next(1, 15);
            this.MaxDamage = rnd.Next(5,25);
        }else{
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
}