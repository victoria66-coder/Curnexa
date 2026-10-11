using Curnexa.Data;
using Curnexa.Models.Entities;
using Curnexa.Models.ViewModels.Base;
using Curnexa.Models.ViewModels.ExchangeRate;
using Microsoft.EntityFrameworkCore;
using ExchangeRateEntity = Curnexa.Models.Entities.ExchangeRate;

namespace Curnexa.Services.ExchangeRate;

public class ExchangeRateService : IExchangeRateService
{
    private readonly CurnexaDbContext _context;

    public ExchangeRateService(CurnexaDbContext context)
    {
        _context = context;
    }

    public async Task<BaseResponse<ExchangeRateResponse>> CreateExchangeRateAsync(
        CreateExchangeRateRequest request)
    {
        if (request.FromCurrencyId == request.ToCurrencyId)
        {
            return new BaseResponse<ExchangeRateResponse>
            {
                Success = false,
                Message = "From currency and To currency cannot be the same.",
                Data = null
            };
        }

        if (request.Rate <= 0)
        {
            return new BaseResponse<ExchangeRateResponse>
            {
                Success = false,
                Message = "Exchange rate must be greater than zero.",
                Data = null
            };
        }

        var currenciesExist = await _context.Currencies
            .CountAsync(c =>
                c.Id == request.FromCurrencyId ||
                c.Id == request.ToCurrencyId);

        if (currenciesExist != 2)
        {
            return new BaseResponse<ExchangeRateResponse>
            {
                Success = false,
                Message = "One or both currencies were not found.",
                Data = null
            };
        }

        var exchangeRate = new ExchangeRateEntity
        {
            Id = Guid.NewGuid(),
            FromCurrencyId = request.FromCurrencyId,
            ToCurrencyId = request.ToCurrencyId,
            Rate = request.Rate,
            RetrievedAt = DateTime.UtcNow,
            Source = request.Source
        };

        await _context.ExchangeRates.AddAsync(exchangeRate);
        await _context.SaveChangesAsync();

        var response = new ExchangeRateResponse
        {
            Id = exchangeRate.Id,
            FromCurrencyId = exchangeRate.FromCurrencyId,
            ToCurrencyId = exchangeRate.ToCurrencyId,
            Rate = exchangeRate.Rate,
            RetrievedAt = exchangeRate.RetrievedAt,
            Source = exchangeRate.Source
        };

        return new BaseResponse<ExchangeRateResponse>
        {
            Success = true,
            Message = "Exchange rate created successfully.",
            Data = response
        };
    }

    public async Task<BaseResponse<List<ExchangeRateResponse>>> GetExchangeRatesAsync()
    {
        var exchangeRates = await _context.ExchangeRates
            .Select(e => new ExchangeRateResponse
            {
                Id = e.Id,
                FromCurrencyId = e.FromCurrencyId,
                ToCurrencyId = e.ToCurrencyId,
                Rate = e.Rate,
                RetrievedAt = e.RetrievedAt,
                Source = e.Source
            })
            .ToListAsync();

        return new BaseResponse<List<ExchangeRateResponse>>
        {
            Success = true,
            Message = "Exchange rates retrieved successfully.",
            Data = exchangeRates
        };
    }

    public async Task<BaseResponse<ExchangeRateResponse>> GetExchangeRateByIdAsync(
        Guid id)
    {
        var exchangeRate = await _context.ExchangeRates
            .FirstOrDefaultAsync(e => e.Id == id);

        if (exchangeRate == null)
        {
            return new BaseResponse<ExchangeRateResponse>
            {
                Success = false,
                Message = "Exchange rate not found.",
                Data = null
            };
        }

        var response = new ExchangeRateResponse
        {
            Id = exchangeRate.Id,
            FromCurrencyId = exchangeRate.FromCurrencyId,
            ToCurrencyId = exchangeRate.ToCurrencyId,
            Rate = exchangeRate.Rate,
            RetrievedAt = exchangeRate.RetrievedAt,
            Source = exchangeRate.Source
        };

        return new BaseResponse<ExchangeRateResponse>
        {
            Success = true,
            Message = "Exchange rate retrieved successfully.",
            Data = response
        };
    }
}