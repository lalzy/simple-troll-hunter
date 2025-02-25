class Troll : Enemy{
    public Troll(bool randomizedBoss = false){
        if(randomizedBoss){
            Random rnd = new Random();
            this.Name = "Gigantic Troll!!";
            this.SetHealth(rnd.Next(20,75));
            this.MinDamage = rnd.Next(2, 15);
            this.MaxDamage = rnd.Next(5, 20);
        }else{
            this.Name = "nasty Troll";
            this.SetHealth(50);
            this.MinDamage = 2;
            this.MaxDamage = 10;
        }
    }
}