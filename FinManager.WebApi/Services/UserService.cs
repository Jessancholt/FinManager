using FinManager.DataAccess;
using FinManager.DataAccess.Models;
using FinManager.Shared.Exceptions;
using FinManager.WebApi.ApiModels.User;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinManager.WebApi.Services;

public class UserService(AppDbContext context)
{
    public async Task<List<UserResponse>> GetAll(CancellationToken ct)
    {
        var users = await context.Users
            .Select(x => new UserResponse(x.Id, x.Name, x.Email))
            .ToListAsync(ct);

        return users ?? throw new NotFoundException("Users");
    }

    public async Task<UserResponse> GetById(Guid id, CancellationToken ct)
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

    public async Task<UserResponse> Create(UserRequest request, CancellationToken ct)
    {
        if (await context.Users.AnyAsync(x => x.Email == request.Email, ct))
            throw new BadRequestException("Email exists");

        var newUser = new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
        };

        await context.Users.AddAsync(newUser, ct);
        await context.SaveChangesAsync(ct);

        return new UserResponse(newUser.Id, newUser.Name, newUser.Email);
    }

    public async Task Update(Guid id, UserRequest request, CancellationToken ct)
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

    public async Task Delete(Guid id, CancellationToken ct)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(x => x.Id == id, ct)
            ?? throw new NotFoundException("User", id);

        context.Remove(user);
        await context.SaveChangesAsync(ct);
    }
}
