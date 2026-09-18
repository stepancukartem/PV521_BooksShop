using FluentValidation;
using PV521_BookssShop.Dtos;

namespace PV521_BookssShop.Validators
{
    public class UpdateAuthorValidator : AbstractValidator<UpdateAuthorDto>
    {
        public UpdateAuthorValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Ім'я автора є обов'язковим.")
                .MaximumLength(255)
                .WithMessage("Ім'я автора не може містити більше 255 символів.");

            RuleFor(x => x.Biography)
                .MaximumLength(5000)
                .WithMessage("Біографія не може містити більше 5000 символів.");

            RuleFor(x => x.Country)
                .MaximumLength(255)
                .WithMessage("Назва країни не може містити більше 255 символів.");

            RuleFor(x => x.BirthDate)
                .NotEmpty()
                .WithMessage("Дата народження є обов'язковою.")
                .LessThan(DateTime.UtcNow)
                .WithMessage("Дата народження не може бути в майбутньому.");

            RuleFor(x => x.Image)
                .Must(file =>
                    file == null ||
                    file.ContentType == "image/jpeg" ||
                    file.ContentType == "image/png" ||
                    file.ContentType == "image/webp")
                .WithMessage("Дозволені тільки JPG, PNG та WEBP зображення.");

            RuleFor(x => x.Image)
                .Must(file => file == null || file.Length <= 5 * 1024 * 1024)
                .WithMessage("Розмір зображення не може перевищувати 5 MB.");
        }
    }
}