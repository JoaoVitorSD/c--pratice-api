using Microsoft.AspNetCore.Http.HttpResults;
using Npgsql;
using Microsoft.AspNetCore.Mvc;
public class SalesRepository
{
    private readonly string _connectionString;

    public SalesRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Sale> GetAllSales()
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

        return sales;
    }

    public Sale AddSale(Sale sale)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        using var cmd = new NpgsqlCommand("INSERT INTO sales (ProductName, Price, SaleDate) VALUES (@productName, @price, @saleDate)", connection);
        cmd.Parameters.AddWithValue("productName", sale.ProductName);
        cmd.Parameters.AddWithValue("price", sale.Price);
        cmd.Parameters.AddWithValue("saleDate", sale.SaleDate);
        cmd.ExecuteNonQuery();
        sale = GetAllSales().Last();
        return sale;
    }

    public Sale UpdateSale(int id, Sale sale)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        using var cmd = new NpgsqlCommand("UPDATE sales SET ProductName = @productName, Price = @price, SaleDate = @saleDate WHERE Id = @id", connection);
        cmd.Parameters.AddWithValue("productName", sale.ProductName);
        cmd.Parameters.AddWithValue("price", sale.Price);
        cmd.Parameters.AddWithValue("saleDate", sale.SaleDate);
        cmd.Parameters.AddWithValue("id", id);
        cmd.ExecuteNonQuery();
        return FindById(id);
    }
    public Sale FindById(int id){
        using  var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        using var cmd = new NpgsqlCommand("SELECT * FROM sales WHERE Id = @id", connection);
        cmd.Parameters.AddWithValue("id", id);
        using var reader = cmd.ExecuteReader();
        reader.Read();
        if (!reader.HasRows)
        {
            return null;
        }
        var sale = new Sale
        {
            Id = reader.GetInt32(0),
            ProductName = reader.GetString(1),
            Price = reader.GetDecimal(2),
            SaleDate = reader.GetDateTime(3)
        };
        return sale;
    }

    public void DeleteSale(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        using var cmd = new NpgsqlCommand("DELETE FROM sales WHERE Id = @id", connection);
        cmd.Parameters.AddWithValue("id", id);
        cmd.ExecuteNonQuery();
    }
}
