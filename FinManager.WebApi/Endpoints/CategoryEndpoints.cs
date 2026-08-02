using Azure.Core;
using FinManager.DataAccess;
using FinManager.DataAccess.Models;
using FinManager.WebApi.ApiModels;
using FinManager.WebApi.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FinManager.WebApi.Endpoints;

public class CategoryEndpoints : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("/category");
        group.MapGet("/", GetAllCategories);
        group.MapGet("/{id:guid}", GetCategory);
        group.MapPost("/", CreateCategory);
        group.MapPut("/{id:guid}", UpdateCategory);
        group.MapDelete("/{id:guid}", DeleteCategory);
    }

    private async Task DeleteCategory(Guid id, AppDbContext context, CancellationToken ct)
    {
        var category = await context.Categories
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new NotFoundException("Category", id);

        context.Remove(category);
        await context.SaveChangesAsync(ct);
    }

    private async Task UpdateCategory(Guid id, CategoryUpdateRequest request, AppDbContext context, CancellationToken ct)
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

    private async Task CreateCategory(CategoryCreateRequest request, AppDbContext context, CancellationToken ct)
    {
        await context.AddAsync(new Category
        {
            Name = request.Name,
            UserId = request.UserId,
        }, ct);

        await context.SaveChangesAsync(ct);
    }

    private async Task<List<CategoryResponse>> GetAllCategories(AppDbContext context, CancellationToken ct)
    {
        var categories = await context.Categories
            .Select(x => new CategoryResponse(x.Id, x.Name, x.UserId))
            .ToListAsync(ct);

        return categories;
    }

    private async Task<CategoryResponse> GetCategory(Guid id, AppDbContext context, CancellationToken ct)
    {
        var category = await context.Categories
            .Select(x => new CategoryResponse(x.Id, x.Name, x.UserId))
            .FirstOrDefaultAsync(x => x.Id == id, ct)
            ?? throw new NotFoundException("Category", id);

        return category;
    }
}
