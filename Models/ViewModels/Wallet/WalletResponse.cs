namespace Curnexa.Models.ViewModels.Wallet
{
    public class WalletResponse
    {
        public Guid Id { get; set; }
        public Guid CurrencyId { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
