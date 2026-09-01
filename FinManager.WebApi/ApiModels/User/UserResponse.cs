namespace FinManager.WebApi.ApiModels.User;

public record UserResponse(
    Guid Id,
    string Name,
    string Email);