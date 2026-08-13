using FinManager.DataAccess;
using FinManager.DataAccess.Models;
using FinManager.Shared.Exceptions;
using FinManager.WebApi.ApiModels.User;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FinManager.WebApi.Services;

public class UserService(AppDbContext context, IValidator<UserRequest> validator)
{
    public async Task<List<UserResponse>> GetAll(CancellationToken ct)
    {
        var users = await context.Users
            .Select(x => new UserResponse(x.Id, x.Name, x.Email))
            .ToListAsync(ct);

        return users ?? throw new NotFoundException("Users");
    }

    public async Task<UserResponse> Get(Guid id, CancellationToken ct)
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

    public async Task Create(UserRequest request, CancellationToken ct)
    {
        if (await context.Users.AnyAsync(x => x.Email == request.Email, ct))
            throw new BadRequestException("Email exists");

        await context.Users.AddAsync(new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
        }, ct);

        await context.SaveChangesAsync(ct);
    }
}
