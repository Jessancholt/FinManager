namespace FinManager.DataAccess.Models;

public record Account : BaseEntity
{
    public string Name { get; init; }
    public decimal Balance { get; init; }
    public Guid CurrencyId { get; init; }
    public Currency Currency { get; init; }
    public Guid UserId { get; init; }
    public User User { get; init; }
    public ICollection<Transaction> Transactions { get; init; } = new List<Transaction>();
}
