using FinManager.DataAccess.Models.Predefined;

namespace FinManager.DataAccess.Models;

public record Transaction : BaseEntity
{
    public string Description { get; init; }
    public decimal Amount { get; init; }
    public TransactionType Type { get; init; }
    public DateTimeOffset PaymentDate { get; init; }
    public Guid AccountId { get; init; }
    public Account Account { get; init; }
    public Guid CategoryId { get; init; }
    public Category Category { get; init; }
}
