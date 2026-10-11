namespace Curnexa.Models.ViewModels.Conversion;

public class ConversionResponse
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid FromWalletId { get; set; }

    public Guid ToWalletId { get; set; }

    public decimal FromAmount { get; set; }

    public decimal ToAmount { get; set; }

    public decimal ExchangeRate { get; set; }

    public DateTime ConvertedAt { get; set; }
}