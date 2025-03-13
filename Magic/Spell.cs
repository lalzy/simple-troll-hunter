class Spell{
 
    public enum ValidSpells{
        Fireball = 0,
        Freeze = 1,
        ShieldSpell = 2,
        light = 3,
    }


    public ValidSpells SpellType;
    public bool ExploreSpell;
    public int Amount;
    public int MaxAmount;
    public Spell(ValidSpells spellType, int amount = 0, int maxAmount = -1){
        SpellType = spellType;
        Amount = amount;
        MaxAmount = maxAmount < 0 ? amount : maxAmount;
        SetExploreSpell(spellType);
    }

    public void SetExploreSpell(ValidSpells spellType){
        switch(spellType){
            case ValidSpells.light:
                ExploreSpell = true;
                return;
        }
    }

    public void Restore(double Factor = 0.25){
        Amount += (int) Math.Ceiling(Factor * MaxAmount);
        if(Amount > MaxAmount)
            Amount = MaxAmount;
    }

    public bool Use(int amountToUse = 1){
        if(Amount > 0){
            Amount -= amountToUse;
            return true;
        }else{
            return false;
        }
    }
}