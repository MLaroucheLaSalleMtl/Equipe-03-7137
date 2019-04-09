using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

public static class ItemXML
{
    public static void SaveItem(string name, Statistics statistics, string image, string objectType, string objectClass)
    {
        XDocument itemsDoc = XDocument.Load(@"Assets/Databases/Database/equipment.xml");
        XNamespace dataPathOrWhatever = itemsDoc.Root.Name.Namespace;
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
            new XAttribute("objectClass", objectClass)));
        itemsDoc.Save(@"Assets/Databases/Database/equipment.xml");
    }

    public static void LoadItem(string id)
    {
        XDocument itemsDoc = XDocument.Load(@"Assets/Databases/Database/equipment.xml");
        XNamespace dataPathOrWhatever = itemsDoc.Root.Name.Namespace;

        
    }
}
