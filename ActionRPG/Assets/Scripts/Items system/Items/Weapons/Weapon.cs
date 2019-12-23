using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    private int maxDamage = 0;
    private int minDamage = 0;
    private float attackSpeed = 1;
    private WeaponType weaponType = WeaponType.noType;
    private Effect[] weaponEffects;
    private Rarity rarity = Rarity.weak;

    public override void initItem(int mapLevel = 1)
    {
        this.equipType = EquipType.oneHand;
        int baseDmg = 0;
        // type = (Type)Random.Range(0, 7);
        weaponType = WeaponType.sword;
        print("Items/Weapons/Sprite/" + this.weaponType.ToString());
        this._obj = Resources.Load<Sprite>("Items/Weapons/Sprite/" + this.weaponType.ToString());
        //this._icon = Resources.Load<GameObject>("Items/Weapons/Icon/" + this.type.ToString());

        switch (weaponType)
        {
            case WeaponType.sword:
                this.itemName = rarity.ToString() + " sword";
                baseDmg = 10;
                attackSpeed = 1 + Random.Range(0, (int)rarity / 10);
                maxDamage = baseDmg + mapLevel + (int)Mathf.Sqrt((int)rarity) + Random.Range((int)Mathf.Sqrt((int)rarity), (int)Mathf.Sqrt((int)rarity) + mapLevel + 2);
                minDamage = maxDamage - (Random.Range(baseDmg/2 - (int)rarity, baseDmg - (int)rarity));
                break;
            case WeaponType.axe:
                baseDmg = 15;
                attackSpeed = 0.8f + Random.Range(0, (int)rarity / 10);
                maxDamage = baseDmg + mapLevel + (int)Mathf.Sqrt((int)rarity) + Random.Range((int)Mathf.Sqrt((int)rarity), (int)Mathf.Sqrt((int)rarity) + mapLevel + 2);
                minDamage = maxDamage - (Random.Range(baseDmg / 2 - (int)rarity*2, baseDmg - (int)rarity)*2);
                break;
            case WeaponType.hammer:
                baseDmg = 25;
                attackSpeed = 0.5f + Random.Range(0, (int)rarity / 10);
                maxDamage = baseDmg + mapLevel + (int)Mathf.Sqrt((int)rarity) + Random.Range((int)Mathf.Sqrt((int)rarity), (int)Mathf.Sqrt((int)rarity) + mapLevel + 2);
                minDamage = maxDamage - (Random.Range(baseDmg / 2 - (int)rarity, baseDmg - (int)rarity));
                break;
            case WeaponType.bow:
                baseDmg = 10;
                attackSpeed = 1 + Random.Range(0, (int)rarity / 10);
                maxDamage = baseDmg + mapLevel + (int)Mathf.Sqrt((int)rarity) + Random.Range((int)Mathf.Sqrt((int)rarity), (int)Mathf.Sqrt((int)rarity) + mapLevel + 2);
                minDamage = maxDamage - (Random.Range(baseDmg / 2 - (int)rarity, baseDmg - (int)rarity));
                break;
            case WeaponType.crossBow:
                baseDmg = 15;
                attackSpeed = 0.6f + Random.Range(0, (int)rarity / 10);
                maxDamage = baseDmg + mapLevel + (int)Mathf.Sqrt((int)rarity) + Random.Range((int)Mathf.Sqrt((int)rarity), (int)Mathf.Sqrt((int)rarity) + mapLevel + 2);
                minDamage = maxDamage - (Random.Range(baseDmg / 2 - (int)rarity, baseDmg - (int)rarity));
                break;
            case WeaponType.dagger:
                baseDmg = 5;
                attackSpeed = 1.5f + Random.Range(0, (int)rarity / 10);
                maxDamage = baseDmg + mapLevel + (int)Mathf.Sqrt((int)rarity) + Random.Range((int)Mathf.Sqrt((int)rarity), (int)Mathf.Sqrt((int)rarity) + mapLevel + 2);
                minDamage = maxDamage - (Random.Range(baseDmg / 2 - (int)rarity, baseDmg - (int)rarity));
                break;
        }

    }

    public override string ToString()
    {
        return base.ToString() + ", Damage: " + "min:" + minDamage + ", max: " + maxDamage + ", attack speed: " + attackSpeed;
    }
}
