namespace Curnexa.Models.ViewModels.ExchangeRate;

public class CreateExchangeRateRequest
{
    public Guid FromCurrencyId { get; set; }

    public Guid ToCurrencyId { get; set; }

    public decimal Rate { get; set; }

    public string Source { get; set; } = string.Empty;
}