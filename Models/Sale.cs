using System.ComponentModel.DataAnnotations.Schema;

namespace RealTimeDatabase.Models;

public class Sale
{
    public int Id { get; set; }
    public string Customer { get; set; } = string.Empty;
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Amount { get; set; }
    public string PurchasedOn { get; set; } = string.Empty;
}