using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Armor : ObjectControllerFactory 
{
    public ArmorType armorType;
    public ArmorClass armorClass;

    public Armor(string name, Statistics stats, Sprite image, ArmorType armorType, ArmorClass armorClass, bool equipped, int id) : base(name, stats, image, equipped, id)
    {
        this.name = name;
        baseStats = stats;
        this.image = image;
        this.armorType = armorType;
        this.armorClass = armorClass;
        this.equipped = equipped;
        this.id = id;
    }

    public static Sprite SpriteArmor(string image)
    {
        return Resources.Load($"{image}", typeof(Sprite)) as Sprite;
    }
}
