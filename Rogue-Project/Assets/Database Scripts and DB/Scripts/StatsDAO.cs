//using Mono.Data.Sqlite;
//using System;
//using System.Collections.Generic;
//using System.Data;

//public class StatsDAO
//{
//    public static string DatabasePath { get; set; }

//    void Awake()
//    {
//        StatsDAO.DatabasePath = "URI=file:" + Application.dataPath + "/Database Scripts and DB/Database/Stats.db";
//    }


//    //Insert > Values inside the db in order.//
//    public static void Insert(string name, int cost)
//    {
//        ExecuteCommand($"INSERT INTO Stats VALUES ('{name}', '{cost}')", reader => { });
//    }

//    public static void ExecuteCommand(string commandText, Action<IDataReader> dataHandler)
//    {
//        IDbConnection connection = new SqliteConnection(DatabasePath);
//        connection.Open();
//        IDbCommand command = connection.CreateCommand();
//        command.CommandText = commandText;
//        IDataReader reader = command.ExecuteReader();

//        dataHandler(reader);

//        reader.Close();
//        connection.Close();
//    }
//}
