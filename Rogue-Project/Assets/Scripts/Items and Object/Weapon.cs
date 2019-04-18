using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MeleeWeapon : ObjectControllerFactory
{
    public WeaponType weaponType;
    public MeleeClass meleeClass;

    public MeleeWeapon(string name, Statistics stats, Sprite image, WeaponType weaponType, MeleeClass weaponClass, bool equipped, int id) : base(name, stats, image, equipped, id)
    {
        this.name = name;
        baseStats = stats;
        this.image = image;
        this.weaponType = weaponType;
        this.meleeClass = weaponClass;
        this.equipped = equipped;
        this.id = id;
    }
    
    public static Sprite SpriteMeleeWeapon(string image)
    {
        if (image == "Sword")
        {
            return Inventory.inventory.allImages[10];
        }
        if (image == "Spear")
        {
            return Inventory.inventory.allImages[10];
        }
        if (image == "Dagger")
        {
            return Inventory.inventory.allImages[4];
        }
        if (image == "Hammer")
        {
            return Inventory.inventory.allImages[6];
        }
        if (image == "Axe")
        {
            return Inventory.inventory.allImages[1];
        }
        else
        {
            return Inventory.inventory.allImages[5];
        }
    }
}

public class DistanceWeapon : ObjectControllerFactory
{
    public WeaponType weaponType;
    public DistanceClass distanceClass;

    public DistanceWeapon(string name, Statistics stats, Sprite image, WeaponType weaponType, DistanceClass weaponClass, bool equipped, int id) : base(name, stats, image, equipped, id)
    {
        this.name = name;
        baseStats = stats;
        this.image = image;
        this.weaponType = weaponType;
        this.distanceClass = weaponClass;
        this.equipped = equipped;
        this.id = id;
    }
    public static Sprite SpriteDistanceWeapon(string image)
    {
        if (image == "Bow")
        {
            return Inventory.inventory.allImages[3];
        }
        if (image == "CrossBow")
        {
            return Inventory.inventory.allImages[3];
        }
        else
        {
            return Inventory.inventory.allImages[4];
        }
    }
}
