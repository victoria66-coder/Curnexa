namespace Curnexa.Models.Entities
{
    public class Wallet
   
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid CurrencyId { get; set; }

        public decimal Balance { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        /* Relationships
        public User User { get; set; } = null!;

        public Currency Currency { get; set; } = null!;

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>(); */
    }
}

