using Curnexa.Data;
using Curnexa.Models.Entities;
using Curnexa.Models.ViewModels.Base;
using Curnexa.Models.ViewModels.Transaction;
using Microsoft.EntityFrameworkCore;
using TransactionEntity = Curnexa.Models.Entities.Transaction;

namespace Curnexa.Services.Transaction;

public class TransactionService : ITransactionService
{
    private readonly CurnexaDbContext _context;

    public TransactionService(CurnexaDbContext context)
    {
        _context = context;
    }

    public async Task<BaseResponse<TransactionResponse>> CreateTransactionAsync(
        CreateTransactionRequest request,
        Guid userId)
    {
        var wallet = await _context.Wallets
            .FirstOrDefaultAsync(w =>
                w.Id == request.WalletId &&
                w.UserId == userId);

        if (wallet == null)
        {
            return new BaseResponse<TransactionResponse>
            {
                Success = false,
                Message = "Wallet not found.",
                Data = null
            };
        }

        if (request.Amount <= 0)
        {
            return new BaseResponse<TransactionResponse>
            {
                Success = false,
                Message = "Amount must be greater than zero.",
                Data = null
            };
        }

        var transaction = new TransactionEntity
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            WalletId = request.WalletId,
            CategoryId = request.CategoryId,
            Type = request.Type,
            Amount = request.Amount,
            Description = request.Description,
            TransactionDate = request.TransactionDate,
            CreatedAt = DateTime.UtcNow
        };

        await _context.Transactions.AddAsync(transaction);

        if (request.Type == TransactionType.Income)
        {
            wallet.Balance += request.Amount;
        }
        else if (request.Type == TransactionType.Expense)
        {
            wallet.Balance -= request.Amount;
        }

        await _context.SaveChangesAsync();

        var response = new TransactionResponse
        {
            Id = transaction.Id,
            WalletId = transaction.WalletId,
            CategoryId = transaction.CategoryId,
            Type = transaction.Type,
            Amount = transaction.Amount,
            Description = transaction.Description,
            TransactionDate = transaction.TransactionDate,
            CreatedAt = transaction.CreatedAt
        };

        return new BaseResponse<TransactionResponse>
        {
            Success = true,
            Message = "Transaction created successfully.",
            Data = response
        };
    }

    public async Task<BaseResponse<List<TransactionResponse>>> GetTransactionsAsync(
        Guid userId)
    {
        var transactions = await _context.Transactions
            .Where(t => t.UserId == userId)
            .Select(t => new TransactionResponse
            {
                Id = t.Id,
                WalletId = t.WalletId,
                CategoryId = t.CategoryId,
                Type = t.Type,
                Amount = t.Amount,
                Description = t.Description,
                TransactionDate = t.TransactionDate,
                CreatedAt = t.CreatedAt
            })
            .ToListAsync();

        return new BaseResponse<List<TransactionResponse>>
        {
            Success = true,
            Message = "Transactions retrieved successfully.",
            Data = transactions
        };
    }

    public async Task<BaseResponse<TransactionResponse>> GetTransactionByIdAsync(
        Guid id,
        Guid userId)
    {
        var transaction = await _context.Transactions
            .FirstOrDefaultAsync(t =>
                t.Id == id &&
                t.UserId == userId);

        if (transaction == null)
        {
            return new BaseResponse<TransactionResponse>
            {
                Success = false,
                Message = "Transaction not found.",
                Data = null
            };
        }

        var response = new TransactionResponse
        {
            Id = transaction.Id,
            WalletId = transaction.WalletId,
            CategoryId = transaction.CategoryId,
            Type = transaction.Type,
            Amount = transaction.Amount,
            Description = transaction.Description,
            TransactionDate = transaction.TransactionDate,
            CreatedAt = transaction.CreatedAt
        };

        return new BaseResponse<TransactionResponse>
        {
            Success = true,
            Message = "Transaction retrieved successfully.",
            Data = response
        };
    }
}