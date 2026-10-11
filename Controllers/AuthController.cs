using Curnexa.Models.ViewModels.Authentication;
using Curnexa.Models.ViewModels.Base;
using Curnexa.Services.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Curnexa.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<BaseResponse<AuthResponse>>> Register(
        RegisterRequest request)
    {
        var response = await _authService.RegisterAsync(request);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<BaseResponse<AuthResponse>>> Login(
        LoginRequest request)
    {
        var response = await _authService.LoginAsync(request);

        if (!response.Success)
        {
            return Unauthorized(response);
        }

        return Ok(response);
    }
}