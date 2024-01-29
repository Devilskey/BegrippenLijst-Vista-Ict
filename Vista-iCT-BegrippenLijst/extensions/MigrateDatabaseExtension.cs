using Handlers;
using Microsoft.AspNetCore.Builder;
using MySql.Data.MySqlClient;
using Serilog;

namespace extensions
{
    public static class MigrateDatabaseExtension
    {
        public static void UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (DatabaseHandler database = new DatabaseHandler())
            {
                Log.Information("Database Migrated");
                string[] qeuries = File.ReadAllLines("./Database.sql");
                database.Migrate(qeuries);

                MySqlCommand AddRoot = new MySqlCommand();
                AddRoot.CommandText = "INSERT IGNORE INTO `Administrator` (Id, Email, Password) VALUES (1, \"Root@vistacollege.nl\", @password);";
                AddRoot.Parameters.AddWithValue("@password", BCryptHandler.BcrypyBasicEncryption("root"));
                database.Insert(AddRoot);
            }
        }
    }
}
