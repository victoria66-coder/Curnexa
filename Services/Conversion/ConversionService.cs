using Curnexa.Data;
using Curnexa.Models.ViewModels.Base;
using Curnexa.Models.ViewModels.Conversion;
using Curnexa.Services.ExchangeRate;
using Curnexa.Services.External;
using Microsoft.EntityFrameworkCore;

using ConversionEntity = Curnexa.Models.Entities.Conversion;

namespace Curnexa.Services.Conversion;

public class ConversionService : IConversionService
{
    private readonly CurnexaDbContext _context;
    private readonly IExchangeRateApiService _exchangeRateApiService;

    public ConversionService(
        CurnexaDbContext context,
        IExchangeRateApiService exchangeRateApiService)
    {
        _context = context;
        _exchangeRateApiService = exchangeRateApiService;
    }

    public async Task<BaseResponse<ConversionResponse>> CreateConversionAsync(
        CreateConversionRequest request, Guid userId)
    {
        if (request.FromWalletId == request.ToWalletId)
        {
            return new BaseResponse<ConversionResponse>
            {
                Success = false,
                Message = "From wallet and To wallet cannot be the same.",
                Data = null
            };
        }

        if (request.FromAmount <= 0)
        {
            return new BaseResponse<ConversionResponse>
            {
                Success = false,
                Message = "Amount must be greater than zero.",
                Data = null
            };
        }

        if (request.ExchangeRate <= 0)
        {
            return new BaseResponse<ConversionResponse>
            {
                Success = false,
                Message = "Exchange rate must be greater than zero.",
                Data = null
            };
        }

        var fromWallet = await _context.Wallets
            .FirstOrDefaultAsync(w =>
                w.Id == request.FromWalletId &&
                w.UserId == userId);

        if (fromWallet == null)
        {
            return new BaseResponse<ConversionResponse>
            {
                Success = false,
                Message = "From wallet not found.",
                Data = null
            };
        }

        var toWallet = await _context.Wallets
            .FirstOrDefaultAsync(w =>
                w.Id == request.ToWalletId &&
                w.UserId == userId);

        if (toWallet == null)
        {
            return new BaseResponse<ConversionResponse>
            {
                Success = false,
                Message = "To wallet not found.",
                Data = null
            };
        }

        if (fromWallet.Balance < request.FromAmount)
        {
            return new BaseResponse<ConversionResponse>
            {
                Success = false,
                Message = "Insufficient wallet balance.",
                Data = null
            };
        }

        var toAmount = request.FromAmount * request.ExchangeRate;

        fromWallet.Balance -= request.FromAmount;
        toWallet.Balance += toAmount;

        var conversion = new ConversionEntity
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            FromWalletId = request.FromWalletId,
            ToWalletId = request.ToWalletId,
            FromAmount = request.FromAmount,
            ToAmount = toAmount,
            ExchangeRate = request.ExchangeRate,
            ConvertedAt = DateTime.UtcNow
        };

        await _context.Conversions.AddAsync(conversion);

        await _context.SaveChangesAsync();

        var response = new ConversionResponse
        {
            Id = conversion.Id,
            UserId = conversion.UserId,
            FromWalletId = conversion.FromWalletId,
            ToWalletId = conversion.ToWalletId,
            FromAmount = conversion.FromAmount,
            ToAmount = conversion.ToAmount,
            ExchangeRate = conversion.ExchangeRate,
            ConvertedAt = conversion.ConvertedAt
        };

        return new BaseResponse<ConversionResponse>
        {
            Success = true,
            Message = "Currency conversion completed successfully.",
            Data = response
        };
    }

    public async Task<BaseResponse<List<ConversionResponse>>> GetConversionsAsync(
        Guid userId)
    {
        var conversions = await _context.Conversions
            .Where(c => c.UserId == userId)
            .Select(c => new ConversionResponse
            {
                Id = c.Id,
                UserId = c.UserId,
                FromWalletId = c.FromWalletId,
                ToWalletId = c.ToWalletId,
                FromAmount = c.FromAmount,
                ToAmount = c.ToAmount,
                ExchangeRate = c.ExchangeRate,
                ConvertedAt = c.ConvertedAt
            })
            .ToListAsync();

        return new BaseResponse<List<ConversionResponse>>
        {
            Success = true,
            Message = "Conversions retrieved successfully.",
            Data = conversions
        };
    }

    public async Task<BaseResponse<ConversionResponse>> GetConversionByIdAsync(
        Guid id,
        Guid userId)
    {
        var conversion = await _context.Conversions
            .FirstOrDefaultAsync(c =>
                c.Id == id &&
                c.UserId == userId);

        if (conversion == null)
        {
            return new BaseResponse<ConversionResponse>
            {
                Success = false,
                Message = "Conversion not found.",
                Data = null
            };
        }

        var response = new ConversionResponse
        {
            Id = conversion.Id,
            UserId = conversion.UserId,
            FromWalletId = conversion.FromWalletId,
            ToWalletId = conversion.ToWalletId,
            FromAmount = conversion.FromAmount,
            ToAmount = conversion.ToAmount,
            ExchangeRate = conversion.ExchangeRate,
            ConvertedAt = conversion.ConvertedAt
        };

        return new BaseResponse<ConversionResponse>
        {
            Success = true,
            Message = "Conversion retrieved successfully.",
            Data = response
        };
    }
}