using Curnexa.Services.External;
using Microsoft.AspNetCore.Mvc;

namespace Curnexa.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExchangeRateApiController : ControllerBase
{
    private readonly IExchangeRateApiService _exchangeRateApiService;

    public ExchangeRateApiController(
        IExchangeRateApiService exchangeRateApiService)
    {
        _exchangeRateApiService = exchangeRateApiService;
    }

    [HttpGet("{baseCurrency}/{quoteCurrency}")]
    public async Task<IActionResult> GetExchangeRate(
        string baseCurrency,
        string quoteCurrency)
    {
        var result = await _exchangeRateApiService
            .GetExchangeRateAsync(baseCurrency, quoteCurrency);

        if (result == null)
        {
            return NotFound(new
            {
                success = false,
                message = "Exchange rate could not be retrieved."
            });
        }

        return Ok(new
        {
            success = true,
            message = "Exchange rate retrieved successfully.",
            data = result
        });
    }
}