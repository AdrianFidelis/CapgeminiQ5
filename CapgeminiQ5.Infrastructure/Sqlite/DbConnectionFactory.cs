using Microsoft.Data.Sqlite;
using System.Data;

public static class DbConnectionFactory
{
    public static IDbConnection CreateConnection()
    {
        return new SqliteConnection(DatabaseBootstrap.GetConnectionString());
    }
}
