namespace FinManager.WebApi.ApiModels.User;

public record UserRequest(
    string Name,
    string Email,
    string Password);