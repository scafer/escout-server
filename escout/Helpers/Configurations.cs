using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;

namespace escout.Helpers
{
    public static class Configurations
    {
        public static IConfigurationBuilder GetAppSettings()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        }

        public static string GetAppSettings(string key)
        {
            var dictionary = Configurations.GetAppSettings().Build().GetSection("AppSettings").Get<Dictionary<string, string>>();
            var value = dictionary.GetValueOrDefault(key, "");
            return value;
        }

        public static string GetNpgsqlConnectionString()
        {
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL") ?? "postgres://postgres:password@localhost:5432/postgres";
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/'),
                SslMode = SslMode.Prefer
            };
            return builder.ToString();
        }

        public static string GetDefaultAccessLevel()
        {
            return Environment.GetEnvironmentVariable("DEFAULT_USER_ACCESS_LEVEL") ?? "3";
        }
    }
}
