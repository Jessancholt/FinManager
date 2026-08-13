using FinManager.WebApi.ApiModels.Category;
using FluentValidation;

namespace FinManager.WebApi.Validators.Category
{
    public class CategoryCreateRequestValidator : AbstractValidator<CategoryCreateRequest>
    {
        public CategoryCreateRequestValidator()
        {
            RuleFor(x => x)
                .NotNull();

            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.UserId)
                .NotEmpty();
        }
    }
}
