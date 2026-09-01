using FinManager.WebApi.ApiModels.User;
using FinManager.WebApi.Services;
using FinManager.WebApi.Validators;
using Microsoft.AspNetCore.Mvc;

namespace FinManager.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(UserService userService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<UserResponse>>> GetAll(CancellationToken ct)
        => await userService.GetAll(ct);

    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponse>> GetById(Guid id, CancellationToken ct)
        => await userService.GetById(id, ct);

    [TypeFilter(typeof(ValidationFilter<UserRequest>))]
    [HttpPost]
    public async Task<IActionResult> Create(UserRequest request, CancellationToken ct)
    {
        var newUser = await userService.Create(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = newUser.Id }, newUser);
    }

    [TypeFilter(typeof(ValidationFilter<UserRequest>))]
    [HttpPut("{id}")]
    public async Task Update(Guid id, UserRequest request, CancellationToken ct) 
        => await userService.Update(id, request, ct);

    [HttpDelete("{id}")]
    public async Task Delete(Guid id, CancellationToken ct)
        => await userService.Delete(id, ct);
}
