﻿using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace Handlers
{
    public class DatabaseHandler : IDisposable
    {
        private MySqlConnection Connection { get; set; }

        private bool successful { get; set; }

        public DatabaseHandler()
        {
            string DatabaseName = Environment.GetEnvironmentVariable("DatabaseDb") ?? "ConceptDatabase";
            string Password = Environment.GetEnvironmentVariable("PasswordDb") ?? "password";
            string Username = Environment.GetEnvironmentVariable("UsernameDb") ?? "root";
            string Server = Environment.GetEnvironmentVariable("HostDb") ?? "192.168.2.13";

            string connectionString = $"server={Server};port=3306;uid={Username};pwd={Password};database={DatabaseName};";

            Connection = new MySqlConnection(connectionString);

            Connection.ConnectionString = connectionString; ;
            Connection.Open();
        }

        public DatabaseHandler Delete(MySqlCommand sqlCommand)
        {
            successful = ExecuteInsertDeleteUpdate(sqlCommand, "Delete");

            return this;
        }

        public DatabaseHandler Update(MySqlCommand sqlCommand)
        { 
            successful = ExecuteInsertDeleteUpdate(sqlCommand, "Update");

            return this;
        }

        public DatabaseHandler Insert(MySqlCommand sqlCommand)
        {
            successful = ExecuteInsertDeleteUpdate(sqlCommand, "Insert");

            return this;
        }

        public bool isSuccesfull => successful;

        private bool ExecuteInsertDeleteUpdate(MySqlCommand sqlCommand, string functie)
        {
            sqlCommand.Connection = Connection;
            try
            {
                int effected = sqlCommand.ExecuteNonQuery();

                if(effected == 0)
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message} : {ex.Data} : {ex} : {functie}");
                return false;
            }
        }

        public string Select(MySqlCommand sqlCommand)
        {
            sqlCommand.Connection = Connection;
            try
            {
                using (MySqlDataReader SelectData = sqlCommand.ExecuteReader())
                {
                    return SqlReaderToJson(SelectData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message} : {ex.Data} : {ex}");
                return "ex.Message";
            }
        }

        public int GetNumber(MySqlCommand sqlCommand)
        {
            sqlCommand.Connection = Connection;
            try
            {
                using (MySqlDataReader Reader = sqlCommand.ExecuteReader())
                {
                    return sqlReaderToInt(Reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"message: {ex.Message}, source {ex.Source}");
                return 0;
            }
        }

        public string SqlReaderToJson(MySqlDataReader reader)
        {
            List<object> JsonList = new List<object>();
            while (reader.Read())
            {
                Dictionary<string, Object> sqlDictionary = new Dictionary<string, Object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    sqlDictionary.Add(reader.GetName(i), reader[i]);
                }
                JsonList.Add(sqlDictionary);
            }
            return JsonConvert.SerializeObject(JsonList);
        }

        public int sqlReaderToInt(MySqlDataReader reader)
        {
            if (reader.Read())
            {
                return reader.GetInt32(0);
            }

            return 0;
        }

        public void Migrate(string[] sqlFileContent)
        {
            MySqlCommand sqlCommand = new MySqlCommand();
            foreach (string query in sqlFileContent)
            {
                sqlCommand.CommandText = query;
                sqlCommand.Connection = Connection;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void Dispose()
        {
            Connection.Close();
            Connection.Dispose();
        }
    }
}
