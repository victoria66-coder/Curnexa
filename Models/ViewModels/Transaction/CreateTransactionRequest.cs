using Curnexa.Models.Entities;

namespace Curnexa.Models.ViewModels.Transaction
{
    public class CreateTransactionRequest
    {
        public Guid WalletId { get; set; }
        public Guid? CategoryId { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
