using Microsoft.Data.SqlClient;
using SmallPOS.API.Data;
using SmallPOS.API.Models;
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


    // =========================
    // GET ALL PRODUCTS
    // =========================

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        var products = new List<Product>();

        using var connection = _connectionFactory.CreateConnection();

        using var command = new SqlCommand(
            "sp_GetProducts",
            connection
        );

        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            products.Add(MapProduct(reader));
        }

        return products;
    }


    // =========================
    // GET PRODUCT BY ID
    // =========================

    public async Task<Product?> GetByIdAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();

        using var command = new SqlCommand(
            "sp_GetProductById",
            connection
        );

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@Id", id);

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return MapProduct(reader);
        }

        return null;
    }


    // =========================
    // CREATE PRODUCT
    // =========================

    public async Task<int> CreateAsync(Product product)
    {
        using var connection = _connectionFactory.CreateConnection();

        using var command = new SqlCommand(
            "sp_CreateProduct",
            connection
        );

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
            "@Name",
            product.Name
        );

        command.Parameters.AddWithValue(
            "@SKU",
            product.SKU
        );

        command.Parameters.AddWithValue(
            "@CategoryId",
            product.CategoryId ?? (object)DBNull.Value
        );

        command.Parameters.AddWithValue(
            "@PurchasePrice",
            product.PurchasePrice
        );

        command.Parameters.AddWithValue(
            "@SalePrice",
            product.SalePrice
        );

        command.Parameters.AddWithValue(
            "@Stock",
            product.Stock
        );

        command.Parameters.AddWithValue(
            "@Status",
            product.Status
        );

        await connection.OpenAsync();

        var result = await command.ExecuteScalarAsync();

        return Convert.ToInt32(result);
    }


    // =========================
    // UPDATE PRODUCT
    // =========================

    public async Task<bool> UpdateAsync(Product product)
    {
        using var connection = _connectionFactory.CreateConnection();

        using var command = new SqlCommand(
            "sp_UpdateProduct",
            connection
        );

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
            "@Id",
            product.Id
        );

        command.Parameters.AddWithValue(
            "@Name",
            product.Name
        );

        command.Parameters.AddWithValue(
            "@SKU",
            product.SKU
        );

        command.Parameters.AddWithValue(
            "@CategoryId",
            product.CategoryId ?? (object)DBNull.Value
        );

        command.Parameters.AddWithValue(
            "@PurchasePrice",
            product.PurchasePrice
        );

        command.Parameters.AddWithValue(
            "@SalePrice",
            product.SalePrice
        );

        command.Parameters.AddWithValue(
            "@Stock",
            product.Stock
        );

        command.Parameters.AddWithValue(
            "@Status",
            product.Status
        );

        await connection.OpenAsync();

        var rowsAffected = await command.ExecuteNonQueryAsync();

        return rowsAffected > 0;
    }


    // =========================
    // DELETE PRODUCT
    // =========================

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();

        using var command = new SqlCommand(
            "sp_DeleteProduct",
            connection
        );

        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue(
            "@Id",
            id
        );

        await connection.OpenAsync();

        var rowsAffected = await command.ExecuteNonQueryAsync();

        return rowsAffected > 0;
    }


    // =========================
    // MAP PRODUCT
    // =========================

    private static Product MapProduct(SqlDataReader reader)
    {
        return new Product
        {
            Id = Convert.ToInt32(reader["Id"]),

            Name = reader["Name"].ToString()
                ?? string.Empty,

            SKU = reader["SKU"].ToString()
                ?? string.Empty,

            CategoryId = reader["CategoryId"] == DBNull.Value
                ? null
                : Convert.ToInt32(reader["CategoryId"]),

            PurchasePrice = Convert.ToDecimal(
                reader["PurchasePrice"]
            ),

            SalePrice = Convert.ToDecimal(
                reader["SalePrice"]
            ),

            Stock = Convert.ToInt32(
                reader["Stock"]
            ),

            Status = reader["Status"].ToString()
                ?? string.Empty
        };
    }
}