namespace FinManager.WebApi.ApiModels;

public record CategoryResponse(
    Guid Id,
    string Name,
    Guid UserId);