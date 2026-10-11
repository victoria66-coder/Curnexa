using Curnexa.Models.ViewModels.Transaction;
using Curnexa.Services.Transaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Curnexa.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransaction(
        [FromBody] CreateTransactionRequest request)
    {
        var userId = Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await _transactionService
            .CreateTransactionAsync(request, userId);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetTransactions()
    {
        var userId = Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await _transactionService
            .GetTransactionsAsync(userId);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTransactionById(
        Guid id)
    {
        var userId = Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await _transactionService
            .GetTransactionByIdAsync(id, userId);

        return Ok(result);
    }
}