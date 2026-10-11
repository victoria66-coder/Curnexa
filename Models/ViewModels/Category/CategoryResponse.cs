using Curnexa.Models.Entities;

namespace Curnexa.Models.ViewModels.Category;

public class CategoryResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public TransactionType Type { get; set; }

    public bool IsActive { get; set; }
}