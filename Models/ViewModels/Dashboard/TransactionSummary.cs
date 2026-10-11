using Curnexa.Models.Entities;

namespace Curnexa.Models.ViewModels.Dashboard;

public class TransactionSummary
{
    public Guid Id { get; set; }

    public TransactionType Type { get; set; }

    public decimal Amount { get; set; }

    public string? Description { get; set; }

    public DateTime TransactionDate { get; set; }
}