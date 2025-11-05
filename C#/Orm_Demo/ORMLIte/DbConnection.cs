using System.Data;
using ServiceStack.OrmLite;

public static class DbConnProvider
{
    private static readonly OrmLiteConnectionFactory factory =
    new("Server=localhost;Database=shopdb;User Id=root;Password=root;", MySqlConnectorDialect.Provider);

    public static IDbConnection GetConnection() => factory.Open();
}