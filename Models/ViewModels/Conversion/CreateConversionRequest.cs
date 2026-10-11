namespace Curnexa.Models.ViewModels.Conversion;

public class CreateConversionRequest
{
    public Guid FromWalletId { get; set; }

    public Guid ToWalletId { get; set; }

    public decimal FromAmount { get; set; }

    public int ExchangeRate { get; internal set; }
}