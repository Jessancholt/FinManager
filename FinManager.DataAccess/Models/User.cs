namespace FinManager.DataAccess.Models;

public record User : BaseEntity
{
    public string Name { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public ICollection<Account> Accounts { get; init; } = new HashSet<Account>();
    public ICollection<Category> Categories { get; init; } = new HashSet<Category>();
}
