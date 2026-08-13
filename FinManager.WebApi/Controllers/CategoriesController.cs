using FinManager.DataAccess;
using FinManager.DataAccess.Models;
using FinManager.WebApi.ApiModels.Category;
using FinManager.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinManager.WebApi.Controllers;

[ApiController]
[Route("category")]
public class CategoriesController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<CategoryResponse>>> GetAll(CancellationToken ct)
    {
        var categories = await context.Categories
            .Select(x => new CategoryResponse(x.Id, x.Name, x.UserId))
            .ToListAsync(ct);

        return categories;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryResponse>> Get(Guid id, CancellationToken ct)
    {
        var category = await context.Categories
            .Select(x => new CategoryResponse(x.Id, x.Name, x.UserId))
            .FirstOrDefaultAsync(x => x.Id == id, ct)
            ?? throw new NotFoundException("Category", id);

        return category;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryCreateRequest request, CancellationToken ct)
    {
        await context.AddAsync(new Category
        {
            Name = request.Name,
            UserId = request.UserId,
        }, ct);

        await context.SaveChangesAsync(ct);

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, CategoryUpdateRequest request, CancellationToken ct)
    {
        var category = await context.Categories
            .FirstOrDefaultAsync(x => x.Id == id, ct)
            ?? throw new NotFoundException("Category", id);

        var updatedCategory = category with
        {
            Name = request.Name
        };

        context.Categories.Update(updatedCategory);

        await context.SaveChangesAsync(ct);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var category = await context.Categories
            .FirstOrDefaultAsync(x => x.Id == id, ct)
            ?? throw new NotFoundException("Category", id);

        context.Remove(category);
        await context.SaveChangesAsync(ct);

        return NoContent();
    }
}
