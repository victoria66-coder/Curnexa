using Curnexa.Models.ViewModels.Base;
using Curnexa.Models.ViewModels.Transaction;

namespace Curnexa.Services.Transaction;

public interface ITransactionService
{
    Task<BaseResponse<TransactionResponse>> CreateTransactionAsync(
        CreateTransactionRequest request,
        Guid userId);

    Task<BaseResponse<List<TransactionResponse>>> GetTransactionsAsync(
        Guid userId);

    Task<BaseResponse<TransactionResponse>> GetTransactionByIdAsync(
        Guid id,
        Guid userId);
}