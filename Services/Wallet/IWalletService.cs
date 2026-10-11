using Curnexa.Models.ViewModels.Base;
using Curnexa.Models.ViewModels.Wallet;

namespace Curnexa.Services.Wallet;

public interface IWalletService
{
    Task<BaseResponse<WalletResponse>> CreateWalletAsync(
        CreateWalletRequest request,
        Guid userId);

    Task<BaseResponse<List<WalletResponse>>> GetWalletsAsync(
        Guid userId);

    Task<BaseResponse<WalletResponse>> GetWalletByIdAsync(
        Guid id,
        Guid userId);
}