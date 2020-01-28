using System;
using System.Data.SQLite;

namespace DBD_ass1_ex1
{
    class Program
    {
        static void Main(string[] args)
        {
            string cs = "Data Source=test_sqlite_db.db;";

            using var con = new SQLiteConnection(cs);
            con.Open();

            string stm = "SELECT SQLITE_VERSION()";
            using var cmd = new SQLiteCommand(stm, con);
            string version = cmd.ExecuteScalar().ToString();

            Console.WriteLine($"SQLite version: {version}");



            bool q = true;
            while(q)
            {
                Console.WriteLine("Write GET for DB contents. INSERT for inserting data. UPDATE for updating data. DELETE for deleting data. Q to quit");
                string user_input = Console.ReadLine();

                switch (user_input)
                {
                    case "GET":
                        SQLiteCommand select_statement = new SQLiteCommand("SELECT * FROM Name", con);

                        try
                        {
                            var sqlite_datareader = select_statement.ExecuteReader();
                            while (sqlite_datareader.Read())
                            {
                                object idReader = sqlite_datareader.GetValue(0);
                                string nameReader = sqlite_datareader.GetString(1);

                                Console.WriteLine($"Id: {idReader} Name: {nameReader}");
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                        break;

                    case "INSERT":
                        Console.WriteLine("Write the name to store.");
                        string name = Console.ReadLine();

                        SQLiteCommand insert_statement = new SQLiteCommand("INSERT INTO Name (name) VALUES (@name)", con);
                        insert_statement.Parameters.AddWithValue("@name", name);

                        try
                        {
                            insert_statement.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                        break;

                    case "UPDATE":
                        Console.WriteLine("Write the name to update.");
                        string e_name = Console.ReadLine();

                        Console.WriteLine("Write what it should be updated to.");
                        string u_name = Console.ReadLine();

                        SQLiteCommand update_statement = new SQLiteCommand("UPDATE Name SET name = (@u_name) WHERE name = (@e_name)", con);
                        update_statement.Parameters.AddWithValue("@u_name", u_name);
                        update_statement.Parameters.AddWithValue("@e_name", e_name);

                        try
                        {
                            update_statement.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                        break;

                    case "DELETE":
                        Console.WriteLine("Write the name to delete.");
                        string d_name = Console.ReadLine();

                        SQLiteCommand delete_statement = new SQLiteCommand("DELETE FROM Name WHERE name = (@name)", con);
                        delete_statement.Parameters.AddWithValue("@name", d_name);

                        try
                        {
                            delete_statement.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                        break;

                    case "Q":
                        q = false;
                        break;
                }
            }
        }
    }
}
