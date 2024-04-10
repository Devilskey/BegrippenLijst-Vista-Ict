using Handlers;
using Microsoft.AspNetCore.Builder;
using MySql.Data.MySqlClient;
using Serilog;

namespace extensions
{
    public static class MigrateDatabaseExtension
    {
        /// <summary>
        /// Extension that migrates the database on api launche 
        /// </summary>
        /// <param name="app"></param>
        public static void UseDatabaseMigration(this IApplicationBuilder app)
        {
            // Creates a new database handler
            using (DatabaseHandler database = new DatabaseHandler())
            {
                //Gets alle queries from the database.sql file and after that sends them to the database.
                string[] qeuries = File.ReadAllLines("./Database.sql");
                database.Migrate(qeuries);

                //Creates a new SQLCommand called add root.
                //Gives the command the query Insert with the @password parameter.
                MySqlCommand AddRoot = new MySqlCommand();
                AddRoot.CommandText = "INSERT IGNORE INTO `Administrator` (Id, Email, Password) VALUES (1, \"Root@vistacollege.nl\", @password);";

                //Replaces the @password parameter with the password root.
                AddRoot.Parameters.AddWithValue("@password", BCryptHandler.BcrypyBasicEncryption("root"));

                //Sends the insert query to the database.
                database.Insert(AddRoot);

                //Logs message Database migrate.
                Log.Information("Database Migrated");

            }
        }
    }
}
