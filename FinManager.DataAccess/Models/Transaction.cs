using FinManager.DataAccess.Models.Predefined;

namespace FinManager.DataAccess.Models;

public record Transaction
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public DateTime PaymentDate { get; init; }
    public int AccountId { get; init; }
    public int CategoryId { get; init; }
    public Guid UserId { get; init; }
    public Account Account { get; init; }
    public Category Category { get; init; }
    public User User { get; init; }
    public decimal Amount { get; init; }
    public TransactionType Type { get; init; }
}
