using FinManager.DataAccess;
using FinManager.DataAccess.Models;
using FinManager.WebApi.ApiModels.User;
using FinManager.Shared.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinManager.WebApi.Services;

namespace FinManager.WebApi.Controllers;

[ApiController]
[Route("users")]
public class UsersController(UserService userService, AppDbContext context, IValidator<UserRequest> validator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<UserResponse>>> GetAll(CancellationToken ct)
        => await userService.GetAll(ct);

    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponse>> Get(Guid id, CancellationToken ct)
        => await userService.Get(id, ct);

    [HttpPost]
    public async Task<IActionResult> Create(UserRequest request, CancellationToken ct)
    {
        var validationResult = await validator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
        }

        await userService.Create(request, ct);

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UserRequest request, CancellationToken ct)
    {
        var validationResult = await validator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
        }

        var user = await context.Users
            .FirstOrDefaultAsync(x => x.Id == id, ct)
            ?? throw new NotFoundException("User", id);

        var updatedUser = user with
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password
        };

        context.Users.Update(updatedUser);

        await context.SaveChangesAsync(ct);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(x => x.Id == id, ct)
            ?? throw new NotFoundException("User", id);

        context.Remove(user);
        await context.SaveChangesAsync(ct);

        return NoContent();
    }
}
