namespace Curnexa.Models.ViewModels.Wallet;

public class CreateWalletRequest
{
    public Guid CurrencyId { get; set; }

    public decimal InitialBalance { get; set; }
}