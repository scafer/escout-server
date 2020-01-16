using Npgsql;
using System;

namespace escout.Helpers
{
    public class Configurations
    {
        public static string GetNpgsqlConnectionString()
        {
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/')
            };

            return builder.ToString();
        }

        public static string GetDatabaseScriptsPath()
        {
            return Environment.GetEnvironmentVariable("./DatabaseScripts/");
            //return Environment.GetEnvironmentVariable("DATABASE_SCRIPTS_PATH");
        }
    }
}
