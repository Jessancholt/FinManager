namespace FinManager.DataAccess.Models;

public record Currency : BaseEntity
{
    public string Name { get; init; }
    public string Code { get; init; }
}
