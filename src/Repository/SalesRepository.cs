using System;
using System.Collections.Generic;
using Npgsql;

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

    public void AddSale(Sale sale)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        using var cmd = new NpgsqlCommand("INSERT INTO sales (ProductName, Price, SaleDate) VALUES (@productName, @price, @saleDate)", connection);
        cmd.Parameters.AddWithValue("productName", sale.ProductName);
        cmd.Parameters.AddWithValue("price", sale.Price);
        cmd.Parameters.AddWithValue("saleDate", sale.SaleDate);
        cmd.ExecuteNonQuery();
    }
}
