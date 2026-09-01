namespace FinManager.DataAccess.Models;

public record Category : BaseEntity
{
    public string Name { get; init; }
    public Guid UserId { get; init; }
    public User User { get; init; }
    public ICollection<Transaction> Transactions { get; init; } = new List<Transaction>();
}
