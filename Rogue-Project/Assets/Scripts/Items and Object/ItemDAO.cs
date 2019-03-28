using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine;

public static class ItemDAO
{
    public static string databasePath { get; set; }

    public static Armor[] GetArmor(ArmorType armorType)
    {
        List<Armor> armors = new List<Armor>();
        string connector = "URI=file:" + Application.dataPath + "/Databases/Database/Equipment.db";
        IDbConnection dbConnection;
        dbConnection = (IDbConnection)new SqliteConnection(connector);
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        string sqlQuery = "SELECT * " + "FROM Equipment " + "WHERE ArmorClass = " + (int)armorType;
        dbCommand.CommandText = sqlQuery;
        IDataReader dataReader = dbCommand.ExecuteReader();
        while (dataReader.Read())
        {
            string[] dbAccess = dataReader.GetString((int)armorType).Split(':');
            string name = "null";
            string attack = "null";
            string defense = "null";
            string support = "null";
            string image = "null";
            string armor = "null";
            string armorClass = "null";

            if (dbAccess.Length > 1)
            {
                name = dbAccess[0];
                attack = dbAccess[1];
                defense = dbAccess[2];
                support = dbAccess[3];
                image = dbAccess[4];
                armor = dbAccess[5];
                armorClass = dbAccess[6];
            }
        }
        return armors.ToArray();
    }
}
