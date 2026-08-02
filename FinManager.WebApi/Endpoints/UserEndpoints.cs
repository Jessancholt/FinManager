using FinManager.DataAccess;
using FinManager.DataAccess.Models;
using FinManager.WebApi.ApiModels;
using FinManager.WebApi.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FinManager.WebApi.Endpoints;

public class UserEndpoints : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("/users");

        group.MapGet("/", GetAllUsers);
        group.MapGet("/{id:guid}", GetUser);
        group.MapPost("/", CreateUser)
             .AddEndpointFilter<ValidationFilter<UserRequest>>();
        group.MapPut("/{id:guid}", UpdateUser)
             .AddEndpointFilter<ValidationFilter<UserRequest>>();
        group.MapDelete("/{id:guid}", DeleteUser);
    }

    private async Task DeleteUser(Guid id, AppDbContext context, CancellationToken ct)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(x => x.Id == id, ct)
            ?? throw new NotFoundException("User", id);

        context.Remove(user);
        await context.SaveChangesAsync(ct);
    }

    private async Task<UserResponse> GetUser(Guid id, AppDbContext context, CancellationToken ct)
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

    private async Task<List<UserResponse>> GetAllUsers(AppDbContext context, CancellationToken ct)
    {
        var users = await context.Users
        .Select(x => new UserResponse(x.Id, x.Name, x.Email))
        .ToListAsync(ct);

        return users ?? throw new NotFoundException("Users");
    }

    private async Task CreateUser(UserRequest request, AppDbContext context, CancellationToken ct)
    {
        if (context.Users.Where(x => x.Email == request.Email).Any())
            throw new BadRequestException("Email exists");

        await context.Users.AddAsync(new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
        }, ct);

        await context.SaveChangesAsync(ct);
    }

    private async Task UpdateUser(Guid id, UserRequest request, AppDbContext context, CancellationToken ct)
    {
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
    }

}
