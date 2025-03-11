using FluentValidation;
using LibrariesWeb.Application.Request;

namespace LibrariesWeb.Application.Validator;

public class UserLoginValidator : AbstractValidator<UserLoginRequest>
{
    public UserLoginValidator()
    {
        RuleFor(u => u.Email)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress();

        RuleFor(u => u.Password)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty().WithMessage("Password is required")
            .Matches(@"^(?=.*[!?\*._^@$])(?=.*\d)(?=.*[A-Z])(?=.*[^\w\s]).{6,}$").WithMessage("Your password must contain at least one (!? *. _ ^ @ $)., one digit, one uppercase");
    }
}