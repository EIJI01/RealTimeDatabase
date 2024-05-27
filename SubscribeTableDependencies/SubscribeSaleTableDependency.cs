using RealTimeDatabase.Hubs;
using RealTimeDatabase.Models;
using TableDependency.SqlClient;

namespace RealTimeDatabase.SubscribeTableDependencies;

public class SubscribeSaleTableDependency : ISubscribeTableDependencies
{
    SqlTableDependency<Sale> tableDependency = null!;
    private readonly Dashboard dashboard;

    public SubscribeSaleTableDependency(Dashboard dashboard)
    {
        this.dashboard = dashboard;
    }

    public void SubscribeTableDependency(string connectionString)
    {
        tableDependency = new SqlTableDependency<Sale>(connectionString);
        tableDependency.OnChanged += TableDependency_OnChanged;
        tableDependency.OnError += TableDependency_OnError;
        tableDependency.Start();
    }

    private async void TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Sale> e)
    {
        if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
        {
            await dashboard.SendSales();
        }
    }

    private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
    {
        Console.WriteLine($"{nameof(Sale)} SqlTableDependency error: {e.Error.Message}");
    }
}