using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Weapon : ObjectControllerFactory
{
    WeaponType weaponType;
    MeleeClass meleeClass;
    DistanceClass distanceClass;

    public Weapon(string name, Statistics stats, string image, WeaponType weaponType, MeleeClass weaponClass, bool equipped, int id) : base(name, stats, image, equipped, id)
    {
        this.name = name;
        baseStats = stats;
        this.image = image;
        this.weaponType = weaponType;
        this.meleeClass = weaponClass;
        this.equipped = equipped;
        this.id = id;
    }

    public Weapon(string name, Statistics stats, string image, WeaponType weaponType, DistanceClass weaponClass, bool equipped, int id) : base(name, stats, image, equipped, id)
    {
        this.name = name;
        baseStats = stats;
        this.image = image;
        this.weaponType = weaponType;
        this.distanceClass = weaponClass;
        this.equipped = equipped;
        this.id = id;
    }
}
