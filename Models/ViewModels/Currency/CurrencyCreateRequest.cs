namespace Curnexa.Models.ViewModels.Currency
{
    public class CurrencyCreateRequest
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
    }
}
