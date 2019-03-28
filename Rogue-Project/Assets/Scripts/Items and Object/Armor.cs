using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Armor : ObjectControllerFactory
{
    ArmorType armorType;
    ArmorClass armorClass;

    public Armor(string name, Statistics stats, string image, ArmorType armorType, ArmorClass armorClass) : base(name, stats, image)
    {
        this.name = name;
        baseStats = stats;
        this.image = image;
        this.armorType = armorType;
        this.armorClass = armorClass;
    }
}
