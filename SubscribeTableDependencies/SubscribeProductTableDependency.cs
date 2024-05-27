using RealTimeDatabase.Hubs;
using RealTimeDatabase.Models;
using TableDependency.SqlClient;

namespace RealTimeDatabase.SubscribeTableDependencies;

public class SubscribeProductTableDependency : ISubscribeTableDependencies
{
    SqlTableDependency<Product> tableDependency = null!;
    private readonly Dashboard dashboard;

    public SubscribeProductTableDependency(Dashboard dashboard)
    {
        this.dashboard = dashboard;
    }

    public void SubscribeTableDependency(string connectionString)
    {
        tableDependency = new SqlTableDependency<Product>(connectionString);
        tableDependency.OnChanged += TableDependency_OnChanged;
        tableDependency.OnError += TableDependency_OnError;
        tableDependency.Start();
    }

    private async void TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Product> e)
    {
        if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
        {
            await dashboard.SendProducts();
        }
    }

    private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
    {
        Console.WriteLine($"{nameof(Product)} SqlTableDependency error: {e.Error.Message}");
    }
}