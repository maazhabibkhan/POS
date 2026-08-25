using Microsoft.Data.SqlClient;

namespace SmallPOS.API.Data;

public class SqlConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(IConfiguration configuration)
    {
        _connectionString =
            configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "Database connection string is not configured."
            );
    }

    public SqlConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
}