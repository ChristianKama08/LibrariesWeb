using FluentValidation;
using LibrariesWeb.Application.Request;

namespace LibrariesWeb.Application.Validator
{
    public class AuthorRequestValidator : AbstractValidator<AuthorRequest>
    {
        public AuthorRequestValidator()
        {
            RuleFor(author => author.Name)
                .NotEmpty().WithMessage("Name is required")
                .Matches(@"^[A-Z].{0,20}$").WithMessage("Name must start with an uppercase letter and can be up to 20 characters long.");

            RuleFor(author => author.Surname)
                .NotEmpty().WithMessage("Surname is required")
                .Matches(@"^[A-Z].{0,20}$").WithMessage("Surname must start with an uppercase letter and can be up to 20 characters long.");

            RuleFor(author => author.Country)
                .NotEmpty().WithMessage("Country is required")
                .Matches(@"^[A-Z].{0,20}$").WithMessage("Country must start with an uppercase letter and can be up to 20 characters long.");

            RuleFor(author => author.BirthDay)
                .NotEmpty().WithMessage("Birthday is required.")
                .LessThan(DateTime.Now).WithMessage("Birthday must be in the past.")
                .Must(birthDay => birthDay.Year > 1900).WithMessage("Birthday must be a valid year.");

        }
    }
}
