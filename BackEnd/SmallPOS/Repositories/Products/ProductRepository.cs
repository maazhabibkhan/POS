using Microsoft.Data.SqlClient;
using SmallPOS.API.Data;
using SmallPOS.API.Models.Products;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SmallPOS.API.Repositories.Products;

public class ProductRepository : IProductRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public ProductRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<ProductResponse>> GetAllAsync()
    {
        var products = new List<ProductResponse>();

        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_GetProducts", connection);

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            products.Add(MapProductResponse(reader));
        }

        return products;
    }

    public async Task<ProductResponse?> GetByIdAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_GetProductById", connection);

        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@Id", id);

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        return await reader.ReadAsync()
            ? MapProductResponse(reader)
            : null;
    }

    public async Task<int> CreateAsync(ProductRequest request)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_CreateProduct", connection);

        command.CommandType = CommandType.StoredProcedure;

        AddProductParameters(command, request);

        await connection.OpenAsync();

        var result = await command.ExecuteScalarAsync();

        return Convert.ToInt32(result);
    }

    public async Task<bool> UpdateAsync(int id, ProductRequest request)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_UpdateProduct", connection);

        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@Id", id);

        AddProductParameters(command, request);

        await connection.OpenAsync();

        var rowsAffected = await command.ExecuteNonQueryAsync();

        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_DeleteProduct", connection);

        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@Id", id);

        await connection.OpenAsync();

        var rowsAffected = await command.ExecuteNonQueryAsync();

        return rowsAffected > 0;
    }

    private static void AddProductParameters(
        SqlCommand command,
        ProductRequest request)
    {
        command.Parameters.AddWithValue("@Name", request.Name);
        command.Parameters.AddWithValue("@SKU", request.SKU);
        command.Parameters.AddWithValue(
            "@CategoryId",
            request.CategoryId ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@PurchasePrice", request.PurchasePrice);
        command.Parameters.AddWithValue("@SalePrice", request.SalePrice);
        command.Parameters.AddWithValue("@Stock", request.Stock);
        command.Parameters.AddWithValue("@Status", request.Status);
    }

    private static ProductResponse MapProductResponse(SqlDataReader reader)
    {
        return new ProductResponse
        {
            Id = Convert.ToInt32(reader["Id"]),
            Name = reader["Name"].ToString() ?? string.Empty,
            SKU = reader["SKU"].ToString() ?? string.Empty,
            CategoryId = reader["CategoryId"] == DBNull.Value
                ? null
                : Convert.ToInt32(reader["CategoryId"]),
            PurchasePrice = Convert.ToDecimal(reader["PurchasePrice"]),
            SalePrice = Convert.ToDecimal(reader["SalePrice"]),
            Stock = Convert.ToInt32(reader["Stock"]),
            Status = reader["Status"].ToString() ?? string.Empty
        };
    }
}
