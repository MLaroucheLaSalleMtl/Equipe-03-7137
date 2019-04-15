using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

public static class ItemXML
{
    public static void SaveItem(string name, Statistics statistics, string image, string objectType, string objectClass, bool equipped, int id)
    {
        XDocument itemsDoc = XDocument.Load(@"Assets/Databases/Database/equipment.xml");
        XNamespace dataPath = itemsDoc.Root.Name.Namespace;
        var count = itemsDoc.Descendants("item").Count();
        string attack = statistics.Attack.ToString();
        string defense = statistics.Defense.ToString();
        string support = statistics.Support.ToString();

        itemsDoc.Root.Add(
            new XElement("item", 
            new XAttribute("Name",name), 
            new XAttribute("Attack", attack), 
            new XAttribute("Defense", defense), 
            new XAttribute("Support", support),
            new XAttribute("image", image),
            new XAttribute("objectType", objectType),
            new XAttribute("objectClass", objectClass),
            new XAttribute("equipped", equipped),
            new XAttribute("id", id)));
        itemsDoc.Save(@"Assets/Databases/Database/equipment.xml");
    }

    public static void LoadItem(int id)
    {
        XDocument itemsDoc = XDocument.Load(@"Assets/Databases/Database/equipment.xml");
        XNamespace dataPath = itemsDoc.Root.Name.Namespace;
        var count = itemsDoc.Descendants("item").Count();
        itemsDoc.Root.Elements("id");
    }
}
