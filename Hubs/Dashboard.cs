using Microsoft.AspNetCore.SignalR;
using RealTimeDatabase.Database;
using RealTimeDatabase.Repository;

namespace RealTimeDatabase.Hubs;

public class Dashboard : Hub
{
    private readonly ProductRepository productRepository;
    private readonly SaleRepository saleRepository;
    public Dashboard(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnections");
        productRepository = new ProductRepository(connectionString);
        saleRepository = new SaleRepository(connectionString);
    }
    public async Task SendProducts()
    {
        var products = productRepository.GetProducts();
        await Clients.All.SendAsync("ReceivedProducts", products);
    }

    public async Task SendSales()
    {
        var sales = saleRepository.GetSales();
        await Clients.All.SendAsync("ReceivedSales", sales);
    }
}