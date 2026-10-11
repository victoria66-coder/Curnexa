namespace Curnexa.Models.Entities
{
    public class Currency
    {
        
        public Guid Id { get; set; }

        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Symbol { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        /* Relationships
        public ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();

        public ICollection<ExchangeRate> ExchangeRatesFrom { get; set; } = new List<ExchangeRate>();

        public ICollection<ExchangeRate> ExchangeRatesTo { get; set; } = new List<ExchangeRate>();

        public ICollection<User> UsersWithThisAsBaseCurrency { get; set; } = new List<User>();*/
    }
}

