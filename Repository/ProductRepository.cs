using System.Data;
using System.Data.SqlClient;
using RealTimeDatabase.Models;

namespace RealTimeDatabase.Repository;

public class ProductRepository
{
    private readonly string connectionString;
    public ProductRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public List<Product> GetProducts()
    {
        var products = new List<Product>();
        Product product;

        var data = GetProductDetailFromDb();
        foreach (DataRow row in data.Rows)
        {
            product = new Product
            {
                Id = Convert.ToInt32(row["Id"]),
                Name = row["Name"].ToString()!,
                Category = row["Category"].ToString()!,
                Price = Convert.ToDecimal(row["Price"])
            };
            products.Add(product);
        }
        return products;
    }

    DataTable GetProductDetailFromDb()
    {
        var query = "SELECT Id, Name, Category, Price FROM Product";
        var dataTable = new DataTable();

        using var connection = new SqlConnection(connectionString);
        try
        {
            connection.Open();
            using var command = new SqlCommand(query, connection);
            using SqlDataReader reader = command.ExecuteReader();
            dataTable.Load(reader);
            return dataTable;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
        finally
        {
            connection.Close();
        }
    }
}