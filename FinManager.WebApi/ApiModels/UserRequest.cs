namespace FinManager.WebApi.ApiModels;

public record UserRequest(
    string Name,
    string Email,
    string Password);