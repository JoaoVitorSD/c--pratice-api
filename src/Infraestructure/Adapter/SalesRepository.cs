using Microsoft.AspNetCore.Http.HttpResults;
using Npgsql;
using Microsoft.AspNetCore.Mvc;
public class SalesRepositoryAdapter: ISaleRepository
{
    private readonly string _connectionString;

    public SalesRepositoryAdapter(string connectionString)
    {
        _connectionString = connectionString;
    }

    public Task<List<Sale>> FindAllSales()
    {
        var sales = new List<Sale>();

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            using var cmd = new NpgsqlCommand("SELECT * FROM sales", connection);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var sale = new Sale
                {
                    Id = reader.GetInt32(0),
                    ProductName = reader.GetString(1),
                    Price = reader.GetDecimal(2),
                    SaleDate = reader.GetDateTime(3)
                };
                sales.Add(sale);
            }
        }
        return Task.FromResult(sales);
    }

    public async Task<Sale> Create(Sale sale)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        using var cmd = new NpgsqlCommand("INSERT INTO sales (name, price, date) VALUES (@name, @price, @date) returning id", connection);
        cmd.Parameters.AddWithValue("name", sale.ProductName);
        cmd.Parameters.AddWithValue("price", sale.Price);
        cmd.Parameters.AddWithValue("date", sale.SaleDate);
        var result = cmd.ExecuteScalar();
        sale.Id =result!=null?(int)result:0;

        return sale;
    }

    public async Task<Sale> Update( Sale sale)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        using var cmd = new NpgsqlCommand("UPDATE sales SET name = @name, price = @price, date = @date WHERE id = @id", connection);
        cmd.Parameters.AddWithValue("name", sale.ProductName);
        cmd.Parameters.AddWithValue("price", sale.Price);
        cmd.Parameters.AddWithValue("date", sale.SaleDate);
        cmd.Parameters.AddWithValue("id", sale.Id);
        cmd.ExecuteNonQuery();
        return sale;
    }
    public Task Delete(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        using var cmd = new NpgsqlCommand("DELETE FROM sales WHERE Id = @id", connection);
        cmd.Parameters.AddWithValue("id", id);
        cmd.ExecuteNonQuery();

        return Task.CompletedTask;
    }
}
