using FinManager.DataAccess;
using FinManager.DataAccess.Models;
using FinManager.WebApi.ApiModels.User;
using FinManager.Shared.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinManager.WebApi.Controllers;

[ApiController]
[Route("users")]
public class UsersController(AppDbContext context, IValidator<UserRequest> validator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<UserResponse>>> GetAll(CancellationToken ct)
    {
        var users = await context.Users
            .Select(x => new UserResponse(x.Id, x.Name, x.Email))
            .ToListAsync(ct);

        return users ?? throw new NotFoundException("Users");
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserResponse>> Get(Guid id, CancellationToken ct)
    {
        var user = await context.Users
            .Where(x => x.Id == id)
            .Select(x => new UserResponse(
                x.Id,
                x.Name,
                x.Email))
            .FirstOrDefaultAsync(ct);

        return user ?? throw new NotFoundException("User", id);
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserRequest request, CancellationToken ct)
    {
        var validationResult = await validator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
        }

        if (context.Users.Where(x => x.Email == request.Email).Any())
            throw new BadRequestException("Email exists");

        await context.Users.AddAsync(new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
        }, ct);

        await context.SaveChangesAsync(ct);

        return NoContent();
    }

    [HttpPut("{id:guid}")]
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

    [HttpDelete("{id:guid}")]
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
