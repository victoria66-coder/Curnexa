using Curnexa.Models.ViewModels.Base;
using Curnexa.Models.ViewModels.Currency;

namespace Curnexa.Services.Currency;

public interface ICurrencyService
{
    Task<BaseResponse<List<CurrencyResponse>>> GetAllCurrenciesAsync();

    Task<BaseResponse<CurrencyResponse>> GetCurrencyByIdAsync(Guid id);
    Task<BaseResponse<CurrencyResponse>> CreateCurrencyAsync(CurrencyCreateRequest request);
}