using System.Net.Http.Json;
using Curnexa.Models.External;

namespace Curnexa.Services.External;

public class ExchangeRateApiService : IExchangeRateApiService
{
    private readonly HttpClient _httpClient;

    public ExchangeRateApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ExchangeRateApiResponse?> GetExchangeRateAsync(
        string baseCurrency,
        string quoteCurrency)
    {
        var url =
            $"v2/rate/{baseCurrency.ToUpper()}/{quoteCurrency.ToUpper()}";

        var response = await _httpClient
            .GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content
            .ReadFromJsonAsync<ExchangeRateApiResponse>();
    }
}