using FinManager.WebApi.ApiModels.Category;
using FluentValidation;

namespace FinManager.WebApi.Validators.Category
{
    public class CategoryUpdateRequestValidator : AbstractValidator<CategoryUpdateRequest>
    {
        public CategoryUpdateRequestValidator()
        {
            RuleFor(x => x)
                .NotNull();

            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}
