namespace Curnexa.Models.ViewModels.Dashboard;

public class WalletSummary
{
    public Guid WalletId { get; set; }

    public string CurrencyCode { get; set; } = string.Empty;

    public decimal Balance { get; set; }
}