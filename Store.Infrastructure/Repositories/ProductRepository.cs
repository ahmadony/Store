using Microsoft.Data.SqlClient;
using Store.Application.Interface;
using Store.Domain.Entities;
using Store.Infrastructure.Data;
using System.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.WebSockets;

namespace Store.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SqlConnectionFactory _sqlFactory;

        public ProductRepository(SqlConnectionFactory sqlFactory)
        {
            _sqlFactory = sqlFactory;
        }

        public async Task<Product> AddProduct(Product product)
        {
            await using var conn = _sqlFactory.CreateConnection();
            await using var command = new SqlCommand("spInsertProduct", conn);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ProductName", product.ProductName);
            command.Parameters.AddWithValue("@Price", product.Price);
            command.Parameters.AddWithValue("@StockQuantity", product.StockQuantity);
            command.Parameters.AddWithValue("@CategoryId", product.CategoryId);

            await conn.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            product.ProductId = Convert.ToInt32(result);
            return product;
        }

        public async Task DeleteProduct(int productId)
        {
            await using var conn = _sqlFactory.CreateConnection();
            await using var command = new SqlCommand("spDeleteProduct", conn);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ID", productId);

            await conn.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var products = new List<Product>();

            await using var conn = _sqlFactory.CreateConnection();
            await using var command = new SqlCommand("spGetAllProducts", conn);
            command.CommandType = CommandType.StoredProcedure;

            await conn.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();

            while(await reader.ReadAsync())
            {
                products.Add(new Product
                {
                    ProductId = reader.GetInt32("ProductId"),
                    ProductName = reader.GetString("ProductName"),
                    Price = reader.GetDecimal("Price"),
                    StockQuantity = reader.GetInt32("StockQuantity"),
                    CategoryId = reader.GetInt32("CategoryId"),
                    IsActive = reader.GetBoolean("IsActive"),
                    CreatedAt = reader.GetDateTime("CreatedAt")
                });
            }
            return products;
        }

        public async Task<Product?> GetProductById(int productId)
        {
            await using var conn = _sqlFactory.CreateConnection();
            await using var command = new SqlCommand("spGetProductById", conn);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ID", productId);

            await conn.OpenAsync();

            await using var reader = await command.ExecuteReaderAsync();

            if(await reader.ReadAsync())
            {
                var product = new Product
                {
                    ProductId = reader.GetInt32("ProductId"),
                    ProductName = reader.GetString("ProductName"),
                    Price = reader.GetDecimal("Price"),
                    StockQuantity = reader.GetInt32("StockQuantity"),
                    CategoryId = reader.GetInt32("CategoryId"),
                    IsActive = reader.GetBoolean("IsActive"),
                    CreatedAt = reader.GetDateTime("CreatedAt")
                };
                return product;
            }
            return null;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            await using var conn = _sqlFactory.CreateConnection();
            await using var command = new SqlCommand("spUpdateProduct", conn);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ID", product.ProductId);
            command.Parameters.AddWithValue("@ProductName", product.ProductName);
            command.Parameters.AddWithValue("@Price", product.Price);
            command.Parameters.AddWithValue("@StockQuantity", product.StockQuantity);
            command.Parameters.AddWithValue("@CategoryId", product.CategoryId);

            await conn.OpenAsync();
            await command.ExecuteNonQueryAsync();
            return product;
        }
    }
}
