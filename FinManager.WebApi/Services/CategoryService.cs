using FinManager.DataAccess;
using FinManager.DataAccess.Models;
using FinManager.Shared.Exceptions;
using FinManager.WebApi.ApiModels.Category;
using Microsoft.EntityFrameworkCore;

namespace FinManager.WebApi.Services;

public class CategoryService(AppDbContext context)
{
    public async Task<List<CategoryResponse>> GetAll(CancellationToken ct)
    {
        var categories = await context.Categories
            .Select(x => new CategoryResponse(x.Id, x.Name, x.UserId))
            .ToListAsync(ct);

        return categories;
    }

    public async Task<CategoryResponse> GetById(Guid id, CancellationToken ct)
    {
        var category = await context.Categories
            .Where(x => x.Id == id)
            .Select(x => new CategoryResponse(
                x.Id,
                x.Name,
                x.UserId))
            .FirstOrDefaultAsync(ct)
            ?? throw new NotFoundException("Category", id);

        return category;
    }

    public async Task<CategoryResponse> Create(CategoryCreateRequest request, CancellationToken ct)
    {
        var newCategory = new Category
        {
            Name = request.Name,
            UserId = request.UserId,
        };

        await context.AddAsync(newCategory, ct);
        await context.SaveChangesAsync(ct);

        return new CategoryResponse(newCategory.Id, newCategory.Name, newCategory.UserId);
    }

    public async Task Update(Guid id, CategoryUpdateRequest request, CancellationToken ct)
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
    }

    public async Task Delete(Guid id, CancellationToken ct)
    {
        var category = await context.Categories
            .FirstOrDefaultAsync(x => x.Id == id, ct)
            ?? throw new NotFoundException("Category", id);

        context.Remove(category);
        await context.SaveChangesAsync(ct);
    }
}
