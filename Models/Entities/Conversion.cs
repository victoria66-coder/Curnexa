namespace Curnexa.Models.Entities
{
    public class Conversion
   
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid FromWalletId { get; set; }

        public Guid ToWalletId { get; set; }

        public decimal FromAmount { get; set; }

        public decimal ToAmount { get; set; }

        public decimal ExchangeRate { get; set; }

        public DateTime ConvertedAt { get; set; } = DateTime.UtcNow;

        /* Relationships
        public User User { get; set; } = null!;

        public Wallet FromWallet { get; set; } = null!;

        public Wallet ToWallet { get; set; } = null!;*/
    }
}
