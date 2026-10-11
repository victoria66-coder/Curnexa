using Curnexa.Models.Entities;
using Curnexa.Models.ViewModels.Authentication;
using Curnexa.Models.ViewModels.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Curnexa.Services.Authentication;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public AuthService(
        UserManager<User> userManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<BaseResponse<AuthResponse>> RegisterAsync(
        RegisterRequest request)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);

        if (existingUser != null)
        {
            return new BaseResponse<AuthResponse>
            {
                Success = false,
                Message = "A user with this email already exists.",
                Data = null
            };
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = request.FullName,
            Email = request.Email,
            UserName = request.Email,
            BaseCurrencyId = request.BaseCurrencyId,
            CreatedAt = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(
            user,
            request.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(
                ", ",
                result.Errors.Select(e => e.Description));

            return new BaseResponse<AuthResponse>
            {
                Success = false,
                Message = errors,
                Data = null
            };
        }

        var token = GenerateJwtToken(user);

        return new BaseResponse<AuthResponse>
        {
            Success = true,
            Message = "Registration successful.",
            Data = new AuthResponse
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email!,
                Token = token
            }
        };
    }

    public async Task<BaseResponse<AuthResponse>> LoginAsync(
        LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            return new BaseResponse<AuthResponse>
            {
                Success = false,
                Message = "Invalid email or password.",
                Data = null
            };
        }

        var passwordValid = await _userManager.CheckPasswordAsync(
            user,
            request.Password);

        if (!passwordValid)
        {
            return new BaseResponse<AuthResponse>
            {
                Success = false,
                Message = "Invalid email or password.",
                Data = null
            };
        }

        var token = GenerateJwtToken(user);

        return new BaseResponse<AuthResponse>
        {
            Success = true,
            Message = "Login successful.",
            Data = new AuthResponse
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email!,
                Token = token
            }
        };
    }

    private string GenerateJwtToken(User user)
    {
        var jwtKey = _configuration["Jwt:Key"]!;
        var jwtIssuer = _configuration["Jwt:Issuer"]!;
        var jwtAudience = _configuration["Jwt:Audience"]!;

        var claims = new List<Claim>
        {
            new Claim(
                ClaimTypes.NameIdentifier,
                user.Id.ToString()),

            new Claim(
                ClaimTypes.Name,
                user.FullName),

            new Claim(
                ClaimTypes.Email,
                user.Email!)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtKey));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}