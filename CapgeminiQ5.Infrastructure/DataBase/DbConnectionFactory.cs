using Microsoft.Data.Sqlite;
using System.Data;

namespace CapgeminiQ5.Infrastructure.Database
{
    public static class DbConnectionFactory
    {
        public static IDbConnection CreateConnection()
        {
            return new SqliteConnection(DatabaseBootstrap.GetConnectionString());
        }
    }
}
