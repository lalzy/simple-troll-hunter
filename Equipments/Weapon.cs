class Weapon{
    public int MinAttribute; // current Durability for shield.
    public int MaxAttribute; // Base/Max durability for shield.
    public enum WeaponType{
        sword = 0,
        axe = 1,
        mace = 2,
        bow = 3,
        dagger = 4,
        staff = 5,
        shield = 6,
    }
    public WeaponType Type;
    public bool TwoHanded;

    public Weapon(int minAttribute, int maxAttribute, WeaponType weaponType, bool twoHanded = false){
        MinAttribute = minAttribute;
        MaxAttribute = maxAttribute;
        Type = weaponType;
        TwoHanded = twoHanded;
    }
    public bool NotDamageWeapon(){
        switch(Type){
            case WeaponType.shield:
                return true;
            default:
                return false;
        }
    }
    // Calculate damage here instead.
}