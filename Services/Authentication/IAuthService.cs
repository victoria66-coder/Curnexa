using Curnexa.Models.ViewModels.Authentication;
using Curnexa.Models.ViewModels.Base;

namespace Curnexa.Services.Authentication;

public interface IAuthService
{
    Task<BaseResponse<AuthResponse>> RegisterAsync(
        RegisterRequest request);

    Task<BaseResponse<AuthResponse>> LoginAsync(
        LoginRequest request);
}