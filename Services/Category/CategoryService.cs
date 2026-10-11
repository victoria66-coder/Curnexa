using Curnexa.Data;
using Curnexa.Models.Entities;
using Curnexa.Models.ViewModels.Base;
using Curnexa.Models.ViewModels.Category;
using Microsoft.EntityFrameworkCore;
using CategoryEntity = Curnexa.Models.Entities.Category;

namespace Curnexa.Services.Category;

public class CategoryService : ICategoryService
{
    private readonly CurnexaDbContext _context;

    public CategoryService(CurnexaDbContext context)
    {
        _context = context;
    }

    public async Task<BaseResponse<CategoryResponse>> CreateCategoryAsync(
        CreateCategoryRequest request,
        Guid userId)
    {
        var categoryExists = await _context.Categories
            .AnyAsync(c =>
                c.UserId == userId &&
                c.Name == request.Name);

        if (categoryExists)
        {
            return new BaseResponse<CategoryResponse>
            {
                Success = false,
                Message = "Category already exists.",
                Data = null
            };
        }

        var category = new CategoryEntity
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Name = request.Name,
            Type = request.Type,
            IsActive = true
        };

        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();

        var response = new CategoryResponse
        {
            Id = category.Id,
            Name = category.Name,
            Type = category.Type,
            IsActive = category.IsActive
        };

        return new BaseResponse<CategoryResponse>
        {
            Success = true,
            Message = "Category created successfully.",
            Data = response
        };
    }

    public async Task<BaseResponse<List<CategoryResponse>>> GetCategoriesAsync(
        Guid userId)
    {
        var categories = await _context.Categories
            .Where(c => c.UserId == userId && c.IsActive)
            .Select(c => new CategoryResponse
            {
                Id = c.Id,
                Name = c.Name,
                Type = c.Type,
                IsActive = c.IsActive
            })
            .ToListAsync();

        return new BaseResponse<List<CategoryResponse>>
        {
            Success = true,
            Message = "Categories retrieved successfully.",
            Data = categories
        };
    }

    public async Task<BaseResponse<CategoryResponse>> GetCategoryByIdAsync(
        Guid id,
        Guid userId)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c =>
                c.Id == id &&
                c.UserId == userId &&
                c.IsActive);

        if (category == null)
        {
            return new BaseResponse<CategoryResponse>
            {
                Success = false,
                Message = "Category not found.",
                Data = null
            };
        }

        var response = new CategoryResponse
        {
            Id = category.Id,
            Name = category.Name,
            Type = category.Type,
            IsActive = category.IsActive
        };

        return new BaseResponse<CategoryResponse>
        {
            Success = true,
            Message = "Category retrieved successfully.",
            Data = response
        };
    }
}