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
        return Resources.Load($"{image}", typeof(Sprite)) as Sprite;
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
        return Resources.Load($"{image}", typeof(Sprite)) as Sprite;
    }
}
