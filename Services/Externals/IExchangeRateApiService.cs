using Curnexa.Models.External;

namespace Curnexa.Services.External;

public interface IExchangeRateApiService
{
    Task<ExchangeRateApiResponse?> GetExchangeRateAsync(
        string baseCurrency,
        string quoteCurrency);
}