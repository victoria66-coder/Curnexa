namespace Curnexa.Models.Entities
{
    public class ExchangeRate
   
    {
        public Guid Id { get; set; }

        public Guid FromCurrencyId { get; set; }

        public Guid ToCurrencyId { get; set; }

        public decimal Rate { get; set; }

        public DateTime RetrievedAt { get; set; } = DateTime.UtcNow;

        public string Source { get; set; } = string.Empty;

        /* Relationships
        public Currency FromCurrency { get; set; } = null!;

        public Currency ToCurrency { get; set; } = null!;*/
    }
}

