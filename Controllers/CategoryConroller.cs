using Curnexa.Models.ViewModels.Category;
using Curnexa.Services.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Curnexa.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(
        [FromBody] CreateCategoryRequest request)
    {
        var userId = Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await _categoryService
            .CreateCategoryAsync(request, userId);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var userId = Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await _categoryService
            .GetCategoriesAsync(userId);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(
        Guid id)
    {
        var userId = Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await _categoryService
            .GetCategoryByIdAsync(id, userId);

        return Ok(result);
    }
}