using FluentValidation;
using PV521_BookssShop.Dtos;

namespace PV521_BookssShop.Validators
{
    public class CreateRoleValidator
        : AbstractValidator<CreateRoleDto>
    {
        public CreateRoleValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Ім'я ролі є обов'язковим.")
                .MaximumLength(100)
                .WithMessage("Ім'я ролі не може містити більше 100 символів.");
        }
    }
}
