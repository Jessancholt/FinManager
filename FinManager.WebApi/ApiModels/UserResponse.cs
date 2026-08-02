namespace FinManager.WebApi.ApiModels;

public record UserResponse(
    Guid Id,
    string Name,
    string Email);