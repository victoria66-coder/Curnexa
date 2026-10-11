using Curnexa.Models.ViewModels.Base;
using Curnexa.Models.ViewModels.ExchangeRate;

namespace Curnexa.Services.ExchangeRate;

public interface IExchangeRateService
{
    Task<BaseResponse<ExchangeRateResponse>> CreateExchangeRateAsync(
        CreateExchangeRateRequest request);

    Task<BaseResponse<List<ExchangeRateResponse>>> GetExchangeRatesAsync();

    Task<BaseResponse<ExchangeRateResponse>> GetExchangeRateByIdAsync(
        Guid id);
}