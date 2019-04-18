using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Item : DummyBaseClass
{
    public int iconId;

    public Item(string name, Statistics stats, Sprite image, int iconId) : base(name, stats, image)
    {
        this.name = name;
        baseStats = stats;
        this.image = image;
        this.iconId = iconId;
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
    Head, Armor
};
public enum ArmorClass
{
    Leather, Iron, Steel, Cloth
};
public enum PotionType
{
    Attack, Defense, Heal
}
#endregion

public abstract class ObjectFactory : Item
{
    public bool equipped;
    public int id;
    public ObjectFactory(string name, Statistics stats, Sprite image, bool equipped, int id) : base (name, stats, image, id)
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
    static int id = ItemXML.ident;

    public ObjectControllerFactory(string name, Statistics stats, Sprite image, bool equipped, int id) : base(name, stats, image, equipped, id)
    {
        base.name = name;
        baseStats = stats;
        base.image = image;
        base.equipped = equipped;
        base.id = id;
    }

    public override ObjectFactory CreateWeapon()
    {
        return new MeleeWeapon(name, baseStats, image, weaponType, weaponClass, equipped, id);
    }
    public override ObjectFactory CreateArmor()
    {
        return new Armor(name, baseStats, image, armorType, armorClass, equipped, id);
    }

    public static Item[] LootObjects()
    {
        //randomisation du nombre d'items
        int randomNumber = 4;
        //randomization de la chance d'avoir un item
        int randomLuck = 101; //TODO remplacer le 100 par la chance du personnage
        //randomization du type d'objet créé
        int randomItem = 3;
        //randomization de weapon type
        int randomWeapType = 2;
        //randomization de weapon class
        int randomWeapClass = 6;
        //randomization de armor type
        int randomArmorType = 2;
        //randomization de armor class
        int randomArmorClass = 4;
        //ranom potion
        int randomPotionType = 3;

        //list of the items created TORETURN
        var items = new List<ObjectControllerFactory>();
        MeleeClass weaponName = (MeleeClass)randomWeapClass;
        WeaponType weaponTypeName = (WeaponType)randomWeapType;
        ArmorClass armorName = (ArmorClass)randomArmorClass;
        ArmorType armorTypeName = (ArmorType)randomArmorType;
        PotionType potionType = (PotionType)randomPotionType;

        PlayerBaseClass levelMC = GameManager.gameManager.MainCharacter.GetComponent<PlayerBaseClass>();

        for (int i = 0; i < randomNumber; i++)
        {
            id++;
            //randomization de la chance d'avoir un item
            randomLuck = random.Next(0, 101); //TODO remplacer le 100 par la chance du personnage
            //randomization du type d'objet créé
            randomItem = random.Next(0, 3);
            //randomization de weapon type
            randomWeapType = random.Next(0, 2);
            //randomization de weapon class
            randomWeapClass = random.Next(0, 6);
            //randomization de armor type
            randomArmorType = random.Next(0, 2);
            //randomization de armor class
            randomArmorClass = random.Next(0, 4);
            //random potion
            randomPotionType = random.Next(0, 3);

            weaponName = (MeleeClass)randomWeapClass;
            weaponTypeName = (WeaponType)randomWeapType;
            armorName = (ArmorClass)randomArmorClass;
            armorTypeName = (ArmorType)randomArmorType;
            potionType = (PotionType)randomPotionType;
            Debug.Log($"Les chances de looter un objet sont de {randomLuck}");
            if (randomLuck > 0)
            {
                Debug.Log("au moins un objet devrait être looté");
                if (randomItem == 1)
                {
                    MakeFullArmor(id);
                }
                else if (randomItem == 0)
                {
                    MakeFullWeapon(id);
                }
                else if (randomItem == 2)
                {
                    MakePotion(id);
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
        void MakePotion(int id)
        {
            if (randomPotionType == 0)
            {
                Potion potionTemp = new Potion($"{potionType.ToString()}", NewStats(levelMC.level), Potion.SpritePotion(potionType.ToString()), (PotionType)randomPotionType, false, id);
                items.Add(potionTemp);
                Debug.Log($"Lootbox {potionTemp.ToString()} {potionTemp.baseStats} {potionTemp.ToString()} {(PotionType)randomPotionType} {id.ToString()}");
                ItemXML.SaveItem(potionTemp);
            }
        }
        void MakeFullWeapon(int id)
        {
            if (randomWeapType == 0)
            {
                MeleeWeapon weaponTemp = new MeleeWeapon($"{weaponName.ToString()}", NewStats(levelMC.level), MeleeWeapon.SpriteMeleeWeapon(weaponName.ToString()), (WeaponType)randomWeapType, (MeleeClass)randomWeapClass, false, id);
                items.Add(weaponTemp);
                Debug.Log($"Lootbox {weaponName.ToString()} {NewStats(levelMC.level)} {weaponName.ToString()} {(WeaponType)randomWeapType} {(MeleeClass)randomWeapClass} {id.ToString()}");
                ItemXML.SaveItem(weaponTemp);
            }
            else if (randomWeapType == 1)
            {
                DistanceWeapon weaponTemp = new DistanceWeapon($"{weaponName.ToString()}", NewStats(levelMC.level), DistanceWeapon.SpriteDistanceWeapon(weaponName.ToString()), (WeaponType)randomWeapType, (DistanceClass)randomWeapClass, false, id);
                items.Add(weaponTemp);
                Debug.Log($"Lootbox {weaponName.ToString()} {NewStats(levelMC.level)} {weaponName.ToString()} {(WeaponType)randomWeapType} {(DistanceClass)randomWeapClass} {id.ToString()}");
                ItemXML.SaveItem(weaponTemp);
            }
        }
        void MakeFullArmor(int id)
        {
            Armor armorTemp = new Armor($"{armorName.ToString()} {armorTypeName.ToString()}", NewStats(levelMC.level), Armor.SpriteArmor(armorName.ToString()), (ArmorType)randomArmorType, (ArmorClass)randomArmorClass, false, id);
            items.Add(armorTemp);
            Debug.Log($"Lootbox {armorName.ToString()} {NewStats(levelMC.level)} {armorName.ToString()} {(ArmorType)randomArmorType} {(ArmorClass)randomArmorClass} {id.ToString()}");
            ItemXML.SaveItem(armorTemp);
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
