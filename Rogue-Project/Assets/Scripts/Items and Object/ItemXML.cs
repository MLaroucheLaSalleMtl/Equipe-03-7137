using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public static class ItemXML
{
    public static int ident;
    static int armorCount = 0, meleeWeaponCount = 0, distanceWeaponCount = 0, potionCount = 0;

    public static void SaveItem(Armor armor)
    {
        XDocument itemsDoc = XDocument.Load(@"Assets/Databases/Database/equipment.xml");
        XNamespace dataPath = itemsDoc.Root.Name.Namespace;
        var count = itemsDoc.Descendants("armor").Count();
        string attack = armor.baseStats.Attack.ToString();
        string defense = armor.baseStats.Defense.ToString();
        string support = armor.baseStats.Support.ToString();
        string name = armor.name.ToString();
        string image = armor.image.ToString();
        string objectType = armor.armorType.ToString();
        string objectClass = armor.armorClass.ToString();
        string equipped = armor.equipped.ToString();
        string id = armor.id.ToString();

        itemsDoc.Root.Add(
            new XElement($"armor{armorCount++}",
            new XAttribute("name", name),
            new XAttribute("attack", attack),
            new XAttribute("defense", defense),
            new XAttribute("support", support),
            new XAttribute("image", image),
            new XAttribute("objectType", objectType),
            new XAttribute("objectClass", objectClass),
            new XAttribute("equipped", equipped),
            new XAttribute("id", id)));
        itemsDoc.Save(@"Assets/Databases/Database/equipment.xml");
    }
    public static void SaveItem(MeleeWeapon weaponMelee)
    {
        XDocument itemsDoc = XDocument.Load(@"Assets/Databases/Database/equipment.xml");
        XNamespace dataPath = itemsDoc.Root.Name.Namespace;
        var count = itemsDoc.Descendants("meleeWeapon").Count();
        string attack = weaponMelee.baseStats.Attack.ToString();
        string defense = weaponMelee.baseStats.Defense.ToString();
        string support = weaponMelee.baseStats.Support.ToString();
        string name = weaponMelee.name.ToString();
        string image = weaponMelee.image.ToString();
        string objectType = weaponMelee.weaponType.ToString();
        string objectClass = weaponMelee.meleeClass.ToString();
        string equipped = weaponMelee.equipped.ToString();
        string id = weaponMelee.id.ToString();

        itemsDoc.Root.Add(
            new XElement($"meleeWeapon{meleeWeaponCount++}",
            new XAttribute("name", name),
            new XAttribute("attack", attack),
            new XAttribute("defense", defense),
            new XAttribute("support", support),
            new XAttribute("image", image),
            new XAttribute("objectType", objectType),
            new XAttribute("objectClass", objectClass),
            new XAttribute("equipped", equipped),
            new XAttribute("id", id)));
        itemsDoc.Save(@"Assets/Databases/Database/equipment.xml");
    }
    public static void SaveItem(DistanceWeapon weaponDistance)
    {
        XDocument itemsDoc = XDocument.Load(@"Assets/Databases/Database/equipment.xml");
        XNamespace dataPath = itemsDoc.Root.Name.Namespace;
        var count = itemsDoc.Descendants("distanceWeapon").Count();
        string attack = weaponDistance.baseStats.Attack.ToString();
        string defense = weaponDistance.baseStats.Defense.ToString();
        string support = weaponDistance.baseStats.Support.ToString();
        string name = weaponDistance.name.ToString();
        string image = weaponDistance.image.ToString();
        string objectType = weaponDistance.weaponType.ToString();
        string objectClass = weaponDistance.distanceClass.ToString();
        string equipped = weaponDistance.equipped.ToString();
        string id = weaponDistance.id.ToString();

        itemsDoc.Root.Add(
            new XElement($"distanceWeapon{distanceWeaponCount++}",
            new XAttribute("name", name),
            new XAttribute("attack", attack),
            new XAttribute("defense", defense),
            new XAttribute("support", support),
            new XAttribute("image", image),
            new XAttribute("objectType", objectType),
            new XAttribute("objectClass", objectClass),
            new XAttribute("equipped", equipped),
            new XAttribute("id", id)));
        itemsDoc.Save(@"Assets/Databases/Database/equipment.xml");
    }
    public static void SaveItem(Potion potion)
    {
        XDocument itemsDoc = XDocument.Load(@"Assets/Databases/Database/equipment.xml");
        XNamespace dataPath = itemsDoc.Root.Name.Namespace;
        var count = itemsDoc.Descendants("potion").Count();
        string attack = potion.baseStats.Attack.ToString();
        string defense = potion.baseStats.Defense.ToString();
        string support = potion.baseStats.Support.ToString();
        string name = potion.name.ToString();
        string image = potion.image.ToString();
        string objectType = potion.potionType.ToString();
        string equipped = potion.equipped.ToString();
        string id = potion.id.ToString();

        itemsDoc.Root.Add(
            new XElement($"potion{potionCount++}",
            new XAttribute("name", name),
            new XAttribute("attack", attack),
            new XAttribute("defense", defense),
            new XAttribute("support", support),
            new XAttribute("image", image),
            new XAttribute("objectType", objectType),
            new XAttribute("equipped", equipped),
            new XAttribute("id", id)));
        itemsDoc.Save(@"Assets/Databases/Database/equipment.xml");
    }
    public static void SaveItem(string name, Statistics statistics, string image, string objectType, string objectClass, bool equipped, int id)
    {
        XDocument itemsDoc = XDocument.Load(@"Assets/Databases/Database/equipment.xml");
        XNamespace dataPath = itemsDoc.Root.Name.Namespace;
        var count = itemsDoc.Descendants("item").Count();
        string attack = statistics.Attack.ToString();
        string defense = statistics.Defense.ToString();
        string support = statistics.Support.ToString();

        itemsDoc.Root.Add(
            new XElement($"item{ident++}", 
            new XAttribute("name",name), 
            new XAttribute("attack", attack), 
            new XAttribute("defense", defense), 
            new XAttribute("support", support),
            new XAttribute("image", image),
            new XAttribute("objectType", objectType),
            new XAttribute("objectClass", objectClass),
            new XAttribute("equipped", equipped),
            new XAttribute("id", id)));
        itemsDoc.Save(@"Assets/Databases/Database/equipment.xml");
    }

    public static void LoadItem(int id)
    {
        //get 1 item
        XDocument itemsDoc = XDocument.Load(@"Assets/Databases/Database/equipment.xml");
        XNamespace dataPath = itemsDoc.Root.Name.Namespace;
        var count = itemsDoc.Descendants("item").Count();
        itemsDoc.Root.Attribute($"{id}");
    }
    public static void LoadAllItems(ref List<Armor> armors, ref List<MeleeWeapon> melees, ref List<DistanceWeapon> distances, ref List<Potion> potions)
    {
        armors = LoadArmors();
        melees = LoadMeleeWeapons();
        distances = LoadDistanceWeapons();
        potions = LoadPotions();
        
        foreach (var armor in armors)
        {
            UnityEngine.Object.Instantiate(armor);
        }
        foreach (var melee in melees)
        {
            GameObject.Instantiate(melee);
        }
        foreach (var distance in distances)
        {
            GameObject.Instantiate(distance);
        }
        foreach (var potion in potions)
        {
            GameObject.Instantiate(potion);
        }
    }
    public static List<Armor> LoadArmors()
    {
        //get all items
        XDocument itemsDoc = XDocument.Load(@"Assets/Databases/Database/equipment.xml");
        int count = 0;
        for (int i = 0; i < 12; i++)
        {
            count += itemsDoc.Descendants($"armor{i}").Count();
        }
        
        List<Armor> items = new List<Armor>();

        for (int i = 0; i < count; i++)
        {
            int.TryParse(itemsDoc.Descendants($"armor{i}").Select(element => element.Attribute("attack").Value).ToString(), out int attack);
            int.TryParse(itemsDoc.Descendants($"armor{i}").Select(element => element.Attribute("defense").Value).ToString(), out int defense);
            int.TryParse(itemsDoc.Descendants($"armor{i}").Select(element => element.Attribute("support").Value).ToString(), out int support);
            Enum.TryParse(itemsDoc.Descendants($"armor{i}").Select(element => element.Attribute("objectType").Value).ToString(), out ArmorType armorType);
            Enum.TryParse(itemsDoc.Descendants($"armor{i}").Select(element => element.Attribute("objectClass").Value).ToString(), out ArmorClass armorClass);
            bool.TryParse(itemsDoc.Descendants($"armor{i}").Select(element => element.Attribute("equipped").Value).ToString(), out bool equipped);
            int.TryParse(itemsDoc.Descendants($"armor{i}").Select(element => element.Attribute("id").Value).ToString(), out int id);

            Debug.Log(itemsDoc.Descendants($"armor{i}").Select(element => element.Attribute("image").Value).ToString());
            items.Add(new Armor(itemsDoc.Descendants($"armor{i}").Select(element => element.Attribute("name").Value).ToString(),
                                stats: new Statistics()
                                {
                                    Attack = attack,
                                    Defense = defense,
                                    Support = support
                                },
                                Armor.SpriteArmor(itemsDoc.Descendants($"armor{i}").Select(element => element.Attribute("image").Value).ToString()),
                                armorType, armorClass, equipped, id));
            Debug.Log(items[i]);
        }
        return items;
    }
    public static List<MeleeWeapon> LoadMeleeWeapons()
    {
        //get all items
        XDocument itemsDoc = XDocument.Load(@"Assets/Databases/Database/equipment.xml");
        var count = itemsDoc.Descendants("meleeWeapon").Count();

        List<MeleeWeapon> items = new List<MeleeWeapon>();

        for (int i = 0; i < count; i++)
        {
            items[i].name =                 itemsDoc.Descendants($"meleeWeapon{i}").Select(element => element.Attribute("name").Value).ToString();
            items[i].baseStats.Attack =     int.Parse(itemsDoc.Descendants($"meleeWeapon{i}").Select(element => element.Attribute("attack").Value).ToString());
            items[i].baseStats.Defense =    int.Parse(itemsDoc.Descendants($"meleeWeapon{i}").Select(element => element.Attribute("defense").Value).ToString());
            items[i].baseStats.Support =    int.Parse(itemsDoc.Descendants($"meleeWeapon{i}").Select(element => element.Attribute("support").Value).ToString());
            items[i].image =                MeleeWeapon.SpriteMeleeWeapon(itemsDoc.Descendants($"meleeWeapon{i}").Select(element => element.Attribute("image").Value).ToString());
            items[i].weaponType =           (WeaponType)Enum.Parse(typeof(WeaponType), itemsDoc.Descendants($"meleeWeapon{i}").Select(element => element.Attribute("objectType").Value).ToString());
            items[i].meleeClass =           (MeleeClass)Enum.Parse(typeof(MeleeClass), itemsDoc.Descendants($"meleeWeapon{i}").Select(element => element.Attribute("objectClass").Value).ToString());
            items[i].equipped =             bool.Parse(itemsDoc.Descendants($"meleeWeapon{i}").Select(element => element.Attribute("equipped").Value).ToString());
            items[i].id =                   int.Parse(itemsDoc.Descendants($"meleeWeapon{i}").Select(element => element.Attribute("id").Value).ToString());
        }
        return items;
    }
    public static List<DistanceWeapon> LoadDistanceWeapons()
    {
        //get all items
        XDocument itemsDoc = XDocument.Load(@"Assets/Databases/Database/equipment.xml");
        var count = itemsDoc.Descendants("distanceWeapon").Count();

        List<DistanceWeapon> items = new List<DistanceWeapon>();

        for (int i = 0; i < count; i++)
        {
            items[i].name =                 itemsDoc.Descendants($"distanceWeapon{i}").Select(element => element.Attribute("name").Value).ToString();
            items[i].baseStats.Attack =     int.Parse(itemsDoc.Descendants($"distanceWeapon{i}").Select(element => element.Attribute("attack").Value).ToString());
            items[i].baseStats.Defense =    int.Parse(itemsDoc.Descendants($"distanceWeapon{i}").Select(element => element.Attribute("defense").Value).ToString());
            items[i].baseStats.Support =    int.Parse(itemsDoc.Descendants($"distanceWeapon{i}").Select(element => element.Attribute("support").Value).ToString());
            items[i].image =                MeleeWeapon.SpriteMeleeWeapon(itemsDoc.Descendants($"distanceWeapon{i}").Select(element => element.Attribute("image").Value).ToString());
            items[i].weaponType =           (WeaponType)Enum.Parse(typeof(WeaponType), itemsDoc.Descendants($"distanceWeapon{i}").Select(element => element.Attribute("objectType").Value).ToString());
            items[i].distanceClass =        (DistanceClass)Enum.Parse(typeof(MeleeClass), itemsDoc.Descendants($"distanceWeapon{i}").Select(element => element.Attribute("objectClass").Value).ToString());
            items[i].equipped =             bool.Parse(itemsDoc.Descendants($"distanceWeapon{i}").Select(element => element.Attribute("equipped").Value).ToString());
            items[i].id =                   int.Parse(itemsDoc.Descendants($"distanceWeapon{i}").Select(element => element.Attribute("id").Value).ToString());
        }
        return items;
    }
    public static List<Potion> LoadPotions()
    {
        //get all items
        XDocument itemsDoc = XDocument.Load(@"Assets/Databases/Database/equipment.xml");
        var count = itemsDoc.Descendants("potion").Count();

        List<Potion> items = new List<Potion>();

        for (int i = 0; i < count; i++)
        {
            items[i].name =                 itemsDoc.Descendants($"potion{i}").Select(element => element.Attribute("name").Value).ToString();
            items[i].baseStats.Attack =     int.Parse(itemsDoc.Descendants($"potion{i}").Select(element => element.Attribute("attack").Value).ToString());
            items[i].baseStats.Defense =    int.Parse(itemsDoc.Descendants($"potion{i}").Select(element => element.Attribute("defense").Value).ToString());
            items[i].baseStats.Support =    int.Parse(itemsDoc.Descendants($"potion{i}").Select(element => element.Attribute("support").Value).ToString());
            items[i].image =                MeleeWeapon.SpriteMeleeWeapon(itemsDoc.Descendants($"potion{i}").Select(element => element.Attribute("image").Value).ToString());
            items[i].potionType =           (PotionType)Enum.Parse(typeof(WeaponType), itemsDoc.Descendants($"potion{i}").Select(element => element.Attribute("objectType").Value).ToString());
            items[i].equipped =             bool.Parse(itemsDoc.Descendants($"potion{i}").Select(element => element.Attribute("equipped").Value).ToString());
            items[i].id =                   int.Parse(itemsDoc.Descendants($"potion{i}").Select(element => element.Attribute("id").Value).ToString());
        }
        return items;
    }
}
