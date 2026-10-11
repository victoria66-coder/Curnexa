using Microsoft.AspNetCore.Identity;

namespace Curnexa.Models.Entities
{
    public class User:IdentityUser<Guid>
    {
       
        public string FullName { get; set; } = string.Empty;

        public Guid? BaseCurrencyId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        /* Relationships
        public Currency BaseCurrency { get; set; } = null!;

        public ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

        public ICollection<Conversion> Conversions { get; set; } = new List<Conversion>();

        public ICollection<Category> Categories { get; set; } = new List<Category>();*/
    }
}
