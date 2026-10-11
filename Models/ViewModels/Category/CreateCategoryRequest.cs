using Curnexa.Models.Entities;

namespace Curnexa.Models.ViewModels.Category;

public class CreateCategoryRequest
{
    public string Name { get; set; } = string.Empty;

    public TransactionType Type { get; set; }
}