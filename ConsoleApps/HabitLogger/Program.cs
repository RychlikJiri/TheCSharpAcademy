using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
class HabitLogger
{

    static public SQLiteConnection database = new SQLiteConnection("Data Source=mydb.sqlite;Version=3;");

    static string menu =
    """
        MAIN MENU

        What would you like to do?

        Type 0 to Close Application
        Type 1 to View All Records
        Type 2 to Insert Record
        Type 3 to Delete Record
        Type 4 to Update Record
        -------------------------------------
    """;
    static void Main(string[] args)
    {
        if (!File.Exists("mydb.sqlite"))
        	CreateDatabase();
        database.Open();
        while (true)
        {
            Console.Clear();
            Console.WriteLine(menu);

            var key = Console.ReadKey().Key;

            Console.Clear();

            switch (key)
            {
                case ConsoleKey.D0:
                    database.Close();
                    return;
                case ConsoleKey.D1:
                    GetRecord();
                    break;
                case ConsoleKey.D2:
                    InsertRecord();
                    break;
                case ConsoleKey.D3:
                    DeleteRecord();
                    break;
                case ConsoleKey.D4:
                    UpdateRecord();
                    break;
            }
        }
    }

    static void CreateDatabase()
    {
        SQLiteConnection.CreateFile("mydb.sqlite");

        database.Open();

        string sql = @"CREATE TABLE IF NOT EXISTS users (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        habit TEXT,
                        quantity INT
                    );";

        using var cmd = new SQLiteCommand(sql, database);
        cmd.ExecuteNonQuery();
        Console.WriteLine("Database has been created");
        database.Close();
    }

    static void InsertRecord()
    {
        Console.WriteLine("INSERT NAME OF THE HABIT: ");
        string habit = Console.ReadLine();
        Console.WriteLine("INSERT QUANTITY OF THE HABIT");
        int quantity;

        while (!Int32.TryParse(Console.ReadLine(), out quantity))
        {
            Console.Clear();
            Console.WriteLine("INSERT QUANTITY OF THE HABIT");
            Console.WriteLine("SELECTED VALUE IS NOT RECOGNIZED AS NUMBER");
         }

        string sql = "INSERT INTO users (habit, quantity) VALUES (@habit, @quantity)";
        using var cmd = new SQLiteCommand(sql, database);
        cmd.Parameters.AddWithValue("@habit", habit);
        cmd.Parameters.AddWithValue("@quantity", quantity);
        cmd.ExecuteNonQuery();
    }

    static void GetRecord()
    {
        string sql = $"SELECT * FROM users";

        using var cmd = new SQLiteCommand(sql, database);

        using SQLiteDataReader reader = cmd.ExecuteReader();
        Console.WriteLine("RECORDS: ");
        while (reader.Read())
        {
            string habit = reader.GetString(1);  // column 1
            int quantity = reader.GetInt32(2);       // column 2

            Console.WriteLine($"HABIT: {habit}; QUANTITY: ({quantity}); ID: {reader.GetInt32(0)}");
        }
        Console.ReadKey();
    }
    static void UpdateRecord()
    {
        int id;
        Console.WriteLine("INSERT ID OF THE HABIT:");
        while (!Int32.TryParse(Console.ReadLine(), out id))
        {
            Console.Clear();
            Console.WriteLine("INSERT ID OF THE HABIT:");
            Console.WriteLine("SELECTED VALUE IS NOT RECOGNIZED AS NUMBER");
        }

        Console.WriteLine("INSERT NEW NAME FOR THE HABIT: ");
        string newHabit = Console.ReadLine();
        Console.WriteLine("INSERT NEW QUANTITY FOR THE HABIT:");
        int newQuantity;
        while (!Int32.TryParse(Console.ReadLine(), out newQuantity))
        {
            Console.Clear();
            Console.WriteLine("INSERT NEW QUANTITY FOR THE NEW HABIT:");
            Console.WriteLine("SELECTED VALUE IS NOT RECOGNIZED AS NUMBER");
        }
        Console.WriteLine("INSERT ID OF THE RECORD U WISH TO CHANGE");
        string sql = $"UPDATE users SET habit = \"{newHabit}\", quantity = \"{newQuantity}\" WHERE id = {id}";

        using var cmd = new SQLiteCommand(sql, database);
        cmd.ExecuteNonQuery();
    }
    static void DeleteRecord()
    {
        Console.WriteLine("INSERT NUMBER OF THE RECORD U WISH TO DELETE: ");
        int id;
        while (!Int32.TryParse(Console.ReadLine(), out id))
        {
            Console.Clear();
            Console.WriteLine("INSERT NUMBER OF THE RECORD U WISH TO DELETE:");
            Console.WriteLine("SELECTED VALUE IS NOT RECOGNIZED AS NUMBER");
        }

        string sql = $"DELETE FROM users WHERE id = \"{id}\"";

        new SQLiteCommand(sql, database).ExecuteNonQuery();
    }
}
