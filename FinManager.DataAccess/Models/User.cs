using System.Collections;

namespace FinManager.DataAccess.Models;

public record User
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public HashSet<Account> Accounts { get; init; }
    public HashSet<Category> Categories { get; init; }
}
