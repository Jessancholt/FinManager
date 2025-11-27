namespace FinManager.DataAccess.Models;

public record Account
{
    public int Id { get; init; }
    public string Name { get; init; }
    public decimal Balance { get; init; }
    public int CurrencyId { get; init; }
    public Currency Currency { get; init; }
}
