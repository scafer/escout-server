using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace escout.Helpers;

public static class Configurations
{
    public static IConfigurationBuilder GetAppSettings()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true);
    }

    public static string GetAppSettings(string key)
    {
        var dictionary = GetAppSettings().Build().GetSection("AppSettings").Get<Dictionary<string, string>>();
        var value = dictionary.GetValueOrDefault(key, "");
        return value;
    }

    public static string GetNpgsqlConnectionString()
    {
        var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL") ?? ConstValues.DEFAULT_DATABASE_URL;
        var databaseUri = new Uri(databaseUrl);
        var userInfo = databaseUri.UserInfo.Split(':');

        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = databaseUri.Host,
            Port = databaseUri.Port,
            Username = userInfo[0],
            Password = userInfo[1],
            Database = databaseUri.LocalPath.TrimStart('/'),
            SslMode = SslMode.Prefer,
            TrustServerCertificate = true
        };
        return builder.ToString();
    }

    public static string GetDefaultAccessLevel()
    {
        return Environment.GetEnvironmentVariable("DEFAULT_USER_ACCESS_LEVEL") ?? "3";
    }
}