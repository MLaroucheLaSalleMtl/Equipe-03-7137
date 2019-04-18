using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Potion : ObjectControllerFactory
{
    public PotionType potionType;
    public Potion(string name, Statistics stats, string image, PotionType potionType, bool equipped, int id) : base(name, stats, image, equipped, id)
    {
        this.name = name;
        baseStats = stats;
        this.image = image;
        this.potionType = potionType;
        this.equipped = equipped;
        this.id = id;
    }
}
