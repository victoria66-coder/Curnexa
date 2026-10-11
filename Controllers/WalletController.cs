using Curnexa.Models.ViewModels.Base;
using Curnexa.Models.ViewModels.Wallet;
using Curnexa.Services.Wallet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Curnexa.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class WalletController : ControllerBase
{
    private readonly IWalletService _walletService;

    public WalletController(IWalletService walletService)
    {
        _walletService = walletService;
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse<WalletResponse>>> CreateWallet(
        CreateWalletRequest request)
    {
        var userId = Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await _walletService.CreateWalletAsync(
            request,
            userId);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<List<WalletResponse>>>> GetWallets()
    {
        var userId = Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await _walletService.GetWalletsAsync(userId);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponse<WalletResponse>>> GetWalletById(
        Guid id)
    {
        var userId = Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await _walletService.GetWalletByIdAsync(
            id,
            userId);

        if (!result.Success)
        {
            return NotFound(result);
        }

        return Ok(result);
    }
}