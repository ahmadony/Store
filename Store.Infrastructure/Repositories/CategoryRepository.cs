using Microsoft.Data.SqlClient;
using Store.Application.Interface;
using Store.Domain.Entities;
using Store.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Store.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SqlConnectionFactory _sqlFactory;
        public CategoryRepository(SqlConnectionFactory sqlFactory)
        {
            _sqlFactory = sqlFactory;
        }


        public async Task<Category> AddCategory(Category category)
        {
            await using var conn = _sqlFactory.CreateConnection();
            await using var command = new SqlCommand("spInsertCategory", conn);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Name", category.CategoryName);
            command.Parameters.AddWithValue("@Description", category.Description ?? (object)DBNull.Value);
            await conn.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            category.CategoryId = Convert.ToInt32(result);
            return category;
        }

        public async Task DeleteCategory(int categoryId)
        {
            await using var conn = _sqlFactory.CreateConnection();
            await using var command = new SqlCommand("spDeleteCategory", conn);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ID", categoryId);
            await conn.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            var categories = new List<Category>();
            await using var conn = _sqlFactory.CreateConnection();
            await using var command = new SqlCommand("spGetAllCategories", conn);
            command.CommandType = CommandType.StoredProcedure;

            await conn.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                categories.Add(new Category
                {
                    CategoryId = reader.GetInt32("CategoryId"),
                    CategoryName = reader.GetString("CategoryName"),
                    Description = reader.IsDBNull("Description") ? null : reader.GetString("Description"),
                    IsActive = reader.GetBoolean("IsActive"),
                    CreatedAt = reader.GetDateTime("CreatedAt")
                });
            }
            return categories;
        }

        public async Task<Category?> GetCategoryById(int categoryId)
        {
            await using var conn = _sqlFactory.CreateConnection();
            await using var command = new SqlCommand("spGetCategoryById", conn);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ID", categoryId);

            await conn.OpenAsync();

            await using var reader = await command.ExecuteReaderAsync();

            if(await reader.ReadAsync())
            {
                var category = new Category
                {
                    CategoryId = reader.GetInt32("CategoryId"),
                    CategoryName = reader.GetString("CategoryName"),
                    Description = reader.IsDBNull("Description") ? null : reader.GetString("Description"),
                    IsActive = reader.GetBoolean("IsActive"),
                    CreatedAt = reader.GetDateTime("CreatedAt")
                };
                return category;
            }

            return null;
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            await using var conn = _sqlFactory.CreateConnection();
            await using var command = new SqlCommand("spUpdateCategory", conn);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ID", category.CategoryId);
            command.Parameters.AddWithValue("@Name", category.CategoryName);
            command.Parameters.AddWithValue("@Description", category.Description ?? (object)DBNull.Value);

            await conn.OpenAsync();
            await command.ExecuteNonQueryAsync();
            return category;
        }
    }
}
