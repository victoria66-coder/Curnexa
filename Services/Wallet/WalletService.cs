using Curnexa.Data;
using Curnexa.Models.ViewModels.Base;
using Curnexa.Models.ViewModels.Wallet;
using Microsoft.EntityFrameworkCore;
using WalletEntity = Curnexa.Models.Entities.Wallet;

namespace Curnexa.Services.Wallet;

public class WalletService : IWalletService
{
    private readonly CurnexaDbContext _context;

    public WalletService(CurnexaDbContext context)
    {
        _context = context;
    }

    public async Task<BaseResponse<WalletResponse>> CreateWalletAsync(
        CreateWalletRequest request,
        Guid userId)
    {
        var currencyExists = await _context.Currencies
            .AnyAsync(c => c.Id == request.CurrencyId);

        if (!currencyExists)
        {
            return new BaseResponse<WalletResponse>
            {
                Success = false,
                Message = "Currency not found.",
                Data = null
            };
        }

        var walletExists = await _context.Wallets
            .AnyAsync(w =>
                w.UserId == userId &&
                w.CurrencyId == request.CurrencyId);

        if (walletExists)
        {
            return new BaseResponse<WalletResponse>
            {
                Success = false,
                Message = "You already have a wallet for this currency.",
                Data = null
            };
        }

        var wallet = new WalletEntity
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            CurrencyId = request.CurrencyId,
            Balance = request.InitialBalance,
            CreatedAt = DateTime.UtcNow
        };

        await _context.Wallets.AddAsync(wallet);
        await _context.SaveChangesAsync();

        var response = new WalletResponse
        {
            Id = wallet.Id,
            CurrencyId = wallet.CurrencyId,
            Balance = wallet.Balance,
            CreatedAt = wallet.CreatedAt
        };

        return new BaseResponse<WalletResponse>
        {
            Success = true,
            Message = "Wallet created successfully.",
            Data = response
        };
    }

    public async Task<BaseResponse<List<WalletResponse>>> GetWalletsAsync(
        Guid userId)
    {
        var wallets = await _context.Wallets
            .Where(w => w.UserId == userId)
            .Select(w => new WalletResponse
            {
                Id = w.Id,
                CurrencyId = w.CurrencyId,
                Balance = w.Balance,
                CreatedAt = w.CreatedAt
            })
            .ToListAsync();

        return new BaseResponse<List<WalletResponse>>
        {
            Success = true,
            Message = "Wallets retrieved successfully.",
            Data = wallets
        };
    }

    public async Task<BaseResponse<WalletResponse>> GetWalletByIdAsync(
        Guid id,
        Guid userId)
    {
        var wallet = await _context.Wallets
            .FirstOrDefaultAsync(w =>
                w.Id == id &&
                w.UserId == userId);

        if (wallet == null)
        {
            return new BaseResponse<WalletResponse>
            {
                Success = false,
                Message = "Wallet not found.",
                Data = null
            };
        }

        var response = new WalletResponse
        {
            Id = wallet.Id,
            CurrencyId = wallet.CurrencyId,
            Balance = wallet.Balance,
            CreatedAt = wallet.CreatedAt
        };

        return new BaseResponse<WalletResponse>
        {
            Success = true,
            Message = "Wallet retrieved successfully.",
            Data = response
        };
    }
}