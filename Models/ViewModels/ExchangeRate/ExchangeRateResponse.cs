namespace Curnexa.Models.ViewModels.ExchangeRate;

public class ExchangeRateResponse
{
    public Guid Id { get; set; }

    public Guid FromCurrencyId { get; set; }

    public Guid ToCurrencyId { get; set; }

    public decimal Rate { get; set; }

    public DateTime RetrievedAt { get; set; }

    public string Source { get; set; } = string.Empty;
}