using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Item : DummyBaseClass
{
    public Item(string name, Statistics stats, string image) : base(name, stats, image)
    {
        this.name = name;
        baseStats = stats;
        this.image = image;
    }
    //this is just to make the rest more readable (inheriting from item, instead of dummybaseclass)
}

abstract class ObjectFactory : Item
{
    //declaration des enums pour tous les objets
    public enum ItemType
    {
        Weapon, Armor
    };
    public enum WeaponType
    {
        Melee, Distance
    };
    public enum MeleeClass
    {
        Sword, Spear, Dagger, Hammer, Axe, DoubleAxe
    };
    public enum DistanceClass
    {
        Bow, CrossBow, ThrowDaggers
    };
    public enum ArmorType
    {
        Head, Shoulders, Plate, Legs, Gloves, Boots
    };
    public enum ArmorClass
    {
        Leather, Iron, Steel, Cloth
    };

    public ObjectFactory(string name, Statistics stats, string image) : base (name, stats, image)
    {
        this.name = name;
        baseStats = stats;
        this.image = image;
    }

    abstract public ObjectFactory CreateWeapon();
    abstract public ObjectFactory CreateArmor();
}

class ObjectControllerFactory : ObjectFactory
{
    static System.Random random = new System.Random();
    WeaponType weaponType;
    MeleeClass weaponClass;
    ArmorType armorType;
    ArmorClass armorClass;

    public ObjectControllerFactory(string name, Statistics stats, string image) : base(name, stats, image)
    {
        base.name = name;
        baseStats = stats;
        base.image = image;
    }

    public override ObjectFactory CreateWeapon()
    {
        return new Weapon(name, baseStats, image, weaponType, weaponClass);
    }
    public override ObjectFactory CreateArmor()
    {
        return new Armor(name, baseStats, image, armorType, armorClass);
    }

    public static Item[] LootObjects()
    {
        //randomisation du nombre d'items
        int randomNumber = random.Next(0, 4);
        //randomization de la chance d'avoir un item
        int randomLuck = random.Next(0, 101); //TODO remplacer le 100 par la chance du personnage
        //randomization du type d'objet créé
        int randomItem = random.Next(0, 2);
        //randomization de weapon type
        int randomWeapType = random.Next(0, 2);
        //randomization de weapon class
        int randomWeapClass = random.Next(0, 6);
        //randomization de armor type
        int randomArmorType = random.Next(0, 6);
        //randomization de armor class
        int randomArmorClass = random.Next(0, 4);
        //list of the items created TORETURN
        var items = new List<ObjectControllerFactory>();
        MeleeClass weaponName = (MeleeClass)randomWeapClass;
        ArmorClass armorName = (ArmorClass)randomArmorClass;

        PlayerBaseClass levelMC = GameManager.gameManager.MainCharacter.GetComponent<PlayerBaseClass>();

        for (int i = 0; i < randomNumber; i++)
        {
            Debug.Log($"Les chances de looter un objet sont de {randomLuck}");
            if (randomLuck > 30)
            {
                Debug.Log("au moins un objet devrait être looté");
                if (randomItem == 1)
                {
                    MakeFullArmor();
                }
                else
                {
                    MakeFullWeapon();
                }
            }
        }

        Statistics NewStats(int level)
        {
            Statistics statistics = new Statistics();
            statistics.Attack += random.Next(0, level + 1);
            statistics.Defense += random.Next(0, level + 1);
            statistics.Support += random.Next(0, level + 1);
            return statistics;
        }
        void MakeFullWeapon()
        {
            items.Add(new Weapon(weaponName.ToString(), NewStats(levelMC.level), weaponName.ToString(), (WeaponType)randomWeapType, (MeleeClass)randomWeapClass));
            Debug.Log($"Lootbox {weaponName.ToString()} {NewStats(levelMC.level)} {weaponName.ToString()} {(WeaponType)randomWeapType} {(MeleeClass)randomWeapClass}");
        }
        void MakeFullArmor()
        {
            items.Add(new Armor(armorName.ToString(), NewStats(levelMC.level), armorName.ToString(), (ArmorType)randomArmorType, (ArmorClass)randomArmorClass));
            Debug.Log($"Lootbox {armorName.ToString()} {NewStats(levelMC.level)} {armorName.ToString()} {(ArmorType)randomArmorType} {(ArmorClass)randomArmorClass}");
        }
        return items.ToArray();
    }
}

/*
MakeAWeapon();
MakeAnArmor();

void MakeAWeapon()
{
    //make a weapon
    if (randomItem == (int)ItemType.Weapon) // 0 = weapon
    {
        if (randomWeapType == (int)WeaponType.Melee) // 0 = melee
        {
            MakeAWeapClass(randomWeapClass);
        }
        else if (randomWeapType == (int)WeaponType.Distance)  // 1 = distance
        {
            MakeAWeapClass(randomWeapClass);
        }
    }
}
void MakeAnArmor()
{
    //make an armor
    if (randomItem == (int)ItemType.Armor)  // 1 = armure
    {
        MakeAnArmorType(randomArmorType);
    }
}
void MakeAnArmorType(int randomType)
{
    switch (randomType)
    {
        case 0:
            //head
            MakeAnArmorClass(randomArmorClass);
            break;
        case 1:
            //shoulders
            MakeAnArmorClass(randomArmorClass);
            break;
        case 2:
            //plate
            MakeAnArmorClass(randomArmorClass);
            break;
        case 3:
            //legs
            MakeAnArmorClass(randomArmorClass);
            break;
        case 4:
            //gloves
            MakeAnArmorClass(randomArmorClass);
            break;
        case 5:
            //boots
            MakeAnArmorClass(randomArmorClass);
            break;
    }
}
void MakeAnArmorClass(int randomClass)
{
    switch (randomClass)
    {
        case 0:
            //leather

            break;
        case 1:
            //iron

            break;
        case 2:
            //steel

            break;
        case 3:
            //cloth

            break;
    }
}
void MakeAWeapClass(int randomClass)
{
    switch (randomClass)
    {
        case 0:
            //sword

            break;
        case 1:
            //spear

            break;
        case 2:
            //dagger

            break;
        case 3:
            //hammer

            break;
        case 4:
            //axe

            break;
        case 5:
            //doubleaxe

            break;
    }
}*/
