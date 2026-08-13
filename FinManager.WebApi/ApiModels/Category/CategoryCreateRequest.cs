namespace FinManager.WebApi.ApiModels.Category;

public class CategoryCreateRequest
{
    public required string Name { get; set; }
    public Guid UserId { get; init; }
}