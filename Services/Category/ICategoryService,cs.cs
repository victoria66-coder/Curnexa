using Curnexa.Models.ViewModels.Base;
using Curnexa.Models.ViewModels.Category;

namespace Curnexa.Services.Category;

public interface ICategoryService
{
    Task<BaseResponse<CategoryResponse>> CreateCategoryAsync(
        CreateCategoryRequest request,
        Guid userId);

    Task<BaseResponse<List<CategoryResponse>>> GetCategoriesAsync(
        Guid userId);

    Task<BaseResponse<CategoryResponse>> GetCategoryByIdAsync(
        Guid id,
        Guid userId);
}