using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FinManager.WebApi.Validators;

public sealed class ValidationFilter<T>(IValidator<T> validator) : IAsyncActionFilter
where T : class
{
    private readonly IValidator<T> _validator = validator;

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var parameter = context.ActionDescriptor.Parameters
            .OfType<ControllerParameterDescriptor>()
            .FirstOrDefault(x =>
                x.ParameterInfo.ParameterType == typeof(T));

        if (parameter is null)
        {
            await next();
            return;
        }

        if (!context.ActionArguments.TryGetValue(
                parameter.Name,
                out var argument))
        {
            await next();
            return;
        }

        if (argument is not T model)
        {
            await next();
            return;
        }

        var result = await _validator.ValidateAsync(
            model,
            context.HttpContext.RequestAborted);

        if (!result.IsValid)
        {
            var errors = result.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    x => x.Key,
                    x => x.Select(x => x.ErrorMessage).ToArray());

            context.Result = new BadRequestObjectResult(
                new ValidationProblemDetails(errors)
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Validation failed."
                });

            return;
        }

        await next();
    }
}
