using FinManager.WebApi.ApiModels.Category;
using FinManager.WebApi.Services;
using FinManager.WebApi.Validators;
using Microsoft.AspNetCore.Mvc;

namespace FinManager.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(CategoryService categoryService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<CategoryResponse>>> GetAll(CancellationToken ct)
        => await categoryService.GetAll(ct);

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryResponse>> GetById(Guid id, CancellationToken ct)
        => await categoryService.GetById(id, ct);

    [TypeFilter(typeof(ValidationFilter<CategoryCreateRequest>))]
    [HttpPost]
    public async Task<IActionResult> Create(CategoryCreateRequest request, CancellationToken ct)
    {
        var newCategory = await categoryService.Create(request, ct);

        return CreatedAtAction(nameof(GetById), new { id = newCategory.Id }, newCategory);
    }

    [TypeFilter(typeof(ValidationFilter<CategoryUpdateRequest>))]
    [HttpPut("{id}")]
    public async Task Update(Guid id, CategoryUpdateRequest request, CancellationToken ct)
        => await categoryService.Update(id, request, ct);

    [HttpDelete("{id}")]
    public async Task Delete(Guid id, CancellationToken ct)
        => await categoryService.Delete(id, ct);
}
