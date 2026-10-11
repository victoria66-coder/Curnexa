using Curnexa.Data;
using Curnexa.Models.Entities;
using Curnexa.Models.ViewModels.Base;
using Curnexa.Models.ViewModels.Currency;
using Microsoft.EntityFrameworkCore;
using CurrencyEntity = Curnexa.Models.Entities.Currency;

namespace Curnexa.Services.Currency;

public class CurrencyService : ICurrencyService
{
    private readonly CurnexaDbContext _context;

    public CurrencyService(CurnexaDbContext context)
    {
        _context = context;
    }

    public async Task<BaseResponse<List<CurrencyResponse>>> GetAllCurrenciesAsync()
    {
        var currencies = await _context.Currencies
            .Where(x => x.IsActive)
            .Select(x => new CurrencyResponse
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Symbol = x.Symbol
            })
            .ToListAsync();

        return new BaseResponse<List<CurrencyResponse>>
        {
            Success = true,
            Message = "Currencies retrieved successfully.",
            Data = currencies
        };
    }

    public async Task<BaseResponse<CurrencyResponse>> GetCurrencyByIdAsync(Guid id)
    {
        var currency = await _context.Currencies
            .Where(x => x.Id == id && x.IsActive)
            .Select(x => new CurrencyResponse
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Symbol = x.Symbol
            })
            .FirstOrDefaultAsync();

        if (currency == null)
        {
            return new BaseResponse<CurrencyResponse>
            {
                Success = false,
                Message = "Currency not found.",
                Data = null
            };
        }

        return new BaseResponse<CurrencyResponse>
        {
            Success = true,
            Message = "Currency retrieved successfully.",
            Data = currency
        };
    }

    public async Task<BaseResponse<CurrencyResponse>> CreateCurrencyAsync(
        CurrencyCreateRequest request)
    {
        var existingCurrency = await _context.Currencies
            .FirstOrDefaultAsync(x => x.Code == request.Code);

        if (existingCurrency != null)
        {
            return new BaseResponse<CurrencyResponse>
            {
                Success = false,
                Message = "A currency with this code already exists.",
                Data = null
            };
        }

        var currency = new CurrencyEntity
        {
            Id = Guid.NewGuid(),
            Code = request.Code,
            Name = request.Name,
            Symbol = request.Symbol,
            IsActive = true
        };

        await _context.Currencies.AddAsync(currency);

        await _context.SaveChangesAsync();

        var response = new CurrencyResponse
        {
            Id = currency.Id,
            Code = currency.Code,
            Name = currency.Name,
            Symbol = currency.Symbol
        };

        return new BaseResponse<CurrencyResponse>
        {
            Success = true,
            Message = "Currency created successfully.",
            Data = response
        };
    }
}