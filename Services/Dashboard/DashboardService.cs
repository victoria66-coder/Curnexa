using Curnexa.Data;
using Curnexa.Models.ViewModels.Base;
using Curnexa.Models.ViewModels.Dashboard;
using Microsoft.EntityFrameworkCore;

namespace Curnexa.Services.Dashboard;

public class DashboardService : IDashboardService
{
    private readonly CurnexaDbContext _context;

    public DashboardService(CurnexaDbContext context)
    {
        _context = context;
    }

    public async Task<BaseResponse<DashboardResponse>> GetDashboardAsync(
        Guid userId)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return new BaseResponse<DashboardResponse>
            {
                Success = false,
                Message = "User not found.",
                Data = null
            };
        }

        var baseCurrency = await _context.Currencies
            .FirstOrDefaultAsync(c => c.Id == user.BaseCurrencyId);

        if (baseCurrency == null)
        {
            return new BaseResponse<DashboardResponse>
            {
                Success = false,
                Message = "Base currency not found.",
                Data = null
            };
        }

        var wallets = await _context.Wallets
            .Where(w => w.UserId == userId)
            .Join(
                _context.Currencies,
                wallet => wallet.CurrencyId,
                currency => currency.Id,
                (wallet, currency) => new WalletSummary
                {
                    WalletId = wallet.Id,
                    CurrencyCode = currency.Code,
                    Balance = wallet.Balance
                })
            .ToListAsync();

        var totalBalance = wallets
            .Where(w => w.CurrencyCode == baseCurrency.Code)
            .Sum(w => w.Balance);

        var totalIncome = await _context.Transactions
            .Where(t =>
                t.UserId == userId &&
                t.Type == Models.Entities.TransactionType.Income)
            .SumAsync(t => (decimal?)t.Amount) ?? 0;

        var totalExpense = await _context.Transactions
            .Where(t =>
                t.UserId == userId &&
                t.Type == Models.Entities.TransactionType.Expense)
            .SumAsync(t => (decimal?)t.Amount) ?? 0;

        var walletCount = wallets.Count;

        var transactionCount = await _context.Transactions
            .CountAsync(t => t.UserId == userId);

        var recentTransactions = await _context.Transactions
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.TransactionDate)
            .Take(5)
            .Select(t => new TransactionSummary
            {
                Id = t.Id,
                Type = t.Type,
                Amount = t.Amount,
                Description = t.Description,
                TransactionDate = t.TransactionDate
            })
            .ToListAsync();

        var response = new DashboardResponse
        {
            BaseCurrencyCode = baseCurrency.Code,
            TotalBalance = totalBalance,
            TotalIncome = totalIncome,
            TotalExpense = totalExpense,
            WalletCount = walletCount,
            TransactionCount = transactionCount,
            Wallets = wallets,
            RecentTransactions = recentTransactions
        };

        return new BaseResponse<DashboardResponse>
        {
            Success = true,
            Message = "Dashboard retrieved successfully.",
            Data = response
        };
    }
}