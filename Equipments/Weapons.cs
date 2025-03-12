static class Weapons{
    public static Weapon GetShortSword(){
        return new Weapon(5, 10, Weapon.WeaponType.sword);
    }
    public static Weapon GetBattleAxe(){
        return new Weapon(8, 15, Weapon.WeaponType.axe, true);
    }

    public static Weapon GetDagger(){
        return new Weapon(1, 5, Weapon.WeaponType.dagger);
    }

    public static Weapon GetShortBow(){
        return new Weapon(1, 5, Weapon.WeaponType.bow);
    }

    public static Weapon GetLongBow(){
        return new Weapon(5,10, Weapon.WeaponType.bow);
    }

    public static Weapon GetStaff(){
        return new Weapon(1,3, Weapon.WeaponType.staff);
    }

    public static Weapon GetKiteShield(){
        return new Weapon(3, 3, Weapon.WeaponType.shield);
    }
}