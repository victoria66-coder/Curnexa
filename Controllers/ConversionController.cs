using Curnexa.Models.ViewModels.Conversion;
using Curnexa.Services.Conversion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Curnexa.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ConversionController : ControllerBase
{
    private readonly IConversionService _conversionService;

    public ConversionController(
        IConversionService conversionService)
    {
        _conversionService = conversionService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateConversion(
        [FromBody] CreateConversionRequest request)
    {
        var userId = Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await _conversionService
            .CreateConversionAsync(request, userId);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetConversions()
    {
        var userId = Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await _conversionService
            .GetConversionsAsync(userId);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetConversionById(
        Guid id)
    {
        var userId = Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await _conversionService
            .GetConversionByIdAsync(id, userId);

        return Ok(result);
    }
}