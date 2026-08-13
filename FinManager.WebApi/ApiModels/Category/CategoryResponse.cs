namespace FinManager.WebApi.ApiModels.Category;

public record CategoryResponse(
    Guid Id,
    string Name,
    Guid UserId);