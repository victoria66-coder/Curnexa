using Curnexa.Models.ViewModels.Base;
using Curnexa.Models.ViewModels.Currency;
using Curnexa.Services.Currency;
using Microsoft.AspNetCore.Mvc;

namespace Curnexa.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CurrencyController : ControllerBase
{
    private readonly ICurrencyService _currencyService;

    public CurrencyController(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<List<CurrencyResponse>>>> GetAllCurrencies()
    {
        var response = await _currencyService.GetAllCurrenciesAsync();

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<BaseResponse<CurrencyResponse>>> GetCurrencyById(Guid id)
    {
        var response = await _currencyService.GetCurrencyByIdAsync(id);

        if (!response.Success)
        {
            return NotFound(response);
        }

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse<CurrencyResponse>>> CreateCurrency(
        CurrencyCreateRequest request)
    {
        var response = await _currencyService.CreateCurrencyAsync(request);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
}