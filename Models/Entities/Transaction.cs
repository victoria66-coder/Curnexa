namespace Curnexa.Models.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid WalletId { get; set; }

        public Guid? CategoryId { get; set; }

        public TransactionType Type { get; set; }

        public decimal Amount { get; set; }

        public string? Description { get; set; }

        public DateTime TransactionDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /* Relationships
        public User User { get; set; } = null!;

        public Wallet Wallet { get; set; } = null!;

        public Category? Category { get; set; }*/
    }

    public enum TransactionType
    {
        Income,
        Expense,
        Transfer
    }
}

