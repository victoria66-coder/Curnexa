using Curnexa.Models.ViewModels.ExchangeRate;
using Curnexa.Services.ExchangeRate;
using Microsoft.AspNetCore.Mvc;

namespace Curnexa.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExchangeRateController : ControllerBase
{
    private readonly IExchangeRateService _exchangeRateService;

    public ExchangeRateController(
        IExchangeRateService exchangeRateService)
    {
        _exchangeRateService = exchangeRateService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateExchangeRate(
        [FromBody] CreateExchangeRateRequest request)
    {
        var result = await _exchangeRateService
            .CreateExchangeRateAsync(request);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetExchangeRates()
    {
        var result = await _exchangeRateService
            .GetExchangeRatesAsync();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetExchangeRateById(Guid id)
    {
        var result = await _exchangeRateService
            .GetExchangeRateByIdAsync(id);

        return Ok(result);
    }
}