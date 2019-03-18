using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Weapon : ObjectControllerFactory
{
    WeaponType weaponType;
    MeleeClass weaponClass;

    public Weapon(string name, Statistics stats, string image, WeaponType weaponType, MeleeClass weaponClass) : base(name, stats, image)
    {
        this.name = name;
        baseStats = stats;
        this.image = image;
        this.weaponType = weaponType;
        this.weaponClass = weaponClass;
    }
}
