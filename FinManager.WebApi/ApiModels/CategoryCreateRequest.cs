namespace FinManager.WebApi.ApiModels;

public class CategoryCreateRequest
{
    public required string Name { get; set; }
    public Guid UserId { get; init; }
}