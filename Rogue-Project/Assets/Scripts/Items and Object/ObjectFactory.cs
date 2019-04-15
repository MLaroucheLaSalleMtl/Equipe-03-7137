using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Item : DummyBaseClass
{
    public Item(string name, Statistics stats, string image) : base(name, stats, image)
    {
        this.name = name;
        baseStats = stats;
        this.image = image;
    }

    //this is just to make the rest more readable (inheriting from item, instead of dummybaseclass)
}

#region ENUMS
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
#endregion

public abstract class ObjectFactory : Item
{
    public bool equipped;
    public int id;
    public ObjectFactory(string name, Statistics stats, string image, bool equipped, int id) : base (name, stats, image)
    {
        this.name = name;
        baseStats = stats;
        this.image = image;
        this.equipped = equipped;
        this.id = id;
    }

    abstract public ObjectFactory CreateWeapon();
    abstract public ObjectFactory CreateArmor();
}

public class ObjectControllerFactory : ObjectFactory
{
    static System.Random random = new System.Random();
    WeaponType weaponType;
    MeleeClass weaponClass;
    ArmorType armorType;
    ArmorClass armorClass;
    static int id = 0;

    public ObjectControllerFactory(string name, Statistics stats, string image, bool equipped, int id) : base(name, stats, image, equipped, id)
    {
        base.name = name;
        baseStats = stats;
        base.image = image;
        base.equipped = equipped;
        base.id = id;
    }

    public override ObjectFactory CreateWeapon()
    {
        return new Weapon(name, baseStats, image, weaponType, weaponClass, equipped, id);
    }
    public override ObjectFactory CreateArmor()
    {
        return new Armor(name, baseStats, image, armorType, armorClass, equipped, id);
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
        WeaponType weaponTypeName = (WeaponType)randomWeapType;
        ArmorClass armorName = (ArmorClass)randomArmorClass;
        ArmorType armorTypeName = (ArmorType)randomArmorType;

        PlayerBaseClass levelMC = GameManager.gameManager.MainCharacter.GetComponent<PlayerBaseClass>();

        for (int i = 0; i < randomNumber; i++)
        {
            id++;
            //randomization de la chance d'avoir un item
            randomLuck = random.Next(0, 101); //TODO remplacer le 100 par la chance du personnage
            //randomization du type d'objet créé
            randomItem = random.Next(0, 2);
            //randomization de weapon type
            randomWeapType = random.Next(0, 2);
            //randomization de weapon class
            randomWeapClass = random.Next(0, 6);
            //randomization de armor type
            randomArmorType = random.Next(0, 6);
            //randomization de armor class
            randomArmorClass = random.Next(0, 4);
            weaponName = (MeleeClass)randomWeapClass;
            weaponTypeName = (WeaponType)randomWeapType;
            armorName = (ArmorClass)randomArmorClass;
            armorTypeName = (ArmorType)randomArmorType;
            Debug.Log($"Les chances de looter un objet sont de {randomLuck}");
            if (randomLuck > 0)
            {
                Debug.Log("au moins un objet devrait être looté");
                if (randomItem == 1)
                {
                    MakeFullArmor(id);
                }
                else
                {
                    MakeFullWeapon(id);
                }
            }
            else
            {
                Debug.Log("aucun objet cette fois-ci");
            }
        }

        Statistics NewStats(int level)
        {
            Statistics statistics = new Statistics();
            statistics.Attack += random.Next(1, level + 1);
            statistics.Defense += random.Next(1, level + 1);
            statistics.Support += random.Next(1, level + 1);
            return statistics;
        }
        void MakeFullWeapon(int id)
        {
            if (randomWeapType == 0)
            {
                Weapon weaponTemp = new Weapon($"{weaponName.ToString()}", NewStats(levelMC.level), weaponName.ToString(), (WeaponType)randomWeapType, (MeleeClass)randomWeapClass, false, id);
                items.Add(weaponTemp);
                Debug.Log($"Lootbox {weaponName.ToString()} {NewStats(levelMC.level)} {weaponName.ToString()} {(WeaponType)randomWeapType} {(MeleeClass)randomWeapClass} {id.ToString()}");
                ItemXML.SaveItem(weaponTemp.name, weaponTemp.baseStats, weaponTemp.image, weaponTemp.weaponType.ToString(), weaponTemp.weaponClass.ToString(), false, id);
            }
            else if (randomWeapType == 1)
            {
                Weapon weaponTemp = new Weapon($"{weaponName.ToString()}", NewStats(levelMC.level), weaponName.ToString(), (WeaponType)randomWeapType, (DistanceClass)randomWeapClass, false, id);
                items.Add(weaponTemp);
                Debug.Log($"Lootbox {weaponName.ToString()} {NewStats(levelMC.level)} {weaponName.ToString()} {(WeaponType)randomWeapType} {(DistanceClass)randomWeapClass} {id.ToString()}");
                ItemXML.SaveItem(weaponTemp.name, weaponTemp.baseStats, weaponTemp.image, weaponTemp.weaponType.ToString(), weaponTemp.weaponClass.ToString(), false, id);
            }
        }
        void MakeFullArmor(int id)
        {
            Armor armorTemp = new Armor($"{armorName.ToString()} {armorTypeName.ToString()}", NewStats(levelMC.level), armorName.ToString(), (ArmorType)randomArmorType, (ArmorClass)randomArmorClass, false, id);
            items.Add(armorTemp);
            Debug.Log($"Lootbox {armorName.ToString()} {NewStats(levelMC.level)} {armorName.ToString()} {(ArmorType)randomArmorType} {(ArmorClass)randomArmorClass} {id.ToString()}");
            ItemXML.SaveItem(armorTemp.name, armorTemp.baseStats, armorTemp.image, armorTemp.armorType.ToString(), armorTemp.armorClass.ToString(), false, id);
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
