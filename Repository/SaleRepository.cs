using System.Data;
using System.Data.SqlClient;
using RealTimeDatabase.Models;

namespace RealTimeDatabase.Repository;

public class SaleRepository
{
    private readonly string connectionString;
    public SaleRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public List<Sale> GetSales()
    {
        var sales = new List<Sale>();
        Sale sale;
        var data = GetSaleDetailsFromDb();
        foreach (DataRow row in data.Rows)
        {
            sale = new Sale
            {
                Id = Convert.ToInt32(row["Id"]),
                Customer = row["Customer"].ToString()!,
                Amount = Convert.ToDecimal(row["Amount"]),
                PurchasedOn = Convert.ToDateTime(row["PurchasedOn"]).ToString("dd-MM-yyyy")
            };
            sales.Add(sale);
        }
        return sales;
    }
    private DataTable GetSaleDetailsFromDb()
    {
        var query = "SELECT Id, Customer, Amount, PurchasedOn FROM Sale";
        var dataTable = new DataTable();

        using var connection = new SqlConnection(connectionString);
        try
        {
            connection.Open();
            using var command = new SqlCommand(query, connection);
            using var reader = command.ExecuteReader();
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