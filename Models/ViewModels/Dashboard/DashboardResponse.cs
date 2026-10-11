namespace Curnexa.Models.ViewModels.Dashboard;

public class DashboardResponse
{
    public string BaseCurrencyCode { get; set; } = string.Empty;

    public decimal TotalBalance { get; set; }

    public decimal TotalIncome { get; set; }

    public decimal TotalExpense { get; set; }

    public int WalletCount { get; set; }

    public int TransactionCount { get; set; }

    public List<WalletSummary> Wallets { get; set; } = new();

    public List<TransactionSummary> RecentTransactions { get; set; } = new();
}