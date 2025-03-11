using FluentValidation;
using LibrariesWeb.Application.Request;

namespace LibrariesWeb.Application.Validator
{
    public class BookRequestValidator:AbstractValidator<BookRequest>
    {
        public BookRequestValidator()
        {
            RuleFor(book => book.ISBN)
                .NotEmpty().WithMessage("ISBN is required.")
                .Matches(@"^[A-Z]{2}\d{7}$").WithMessage("ISBN must consist of 2 uppercase letters followed by 7 digits.");

            RuleFor(book => book.Title)
                .NotEmpty().WithMessage("Title is required.")
                .Matches(@"^[A-Z].{0,19}$").WithMessage("Title must start with an uppercase letter and can be up to 20 characters long.");

            RuleFor(book => book.Genre)
                .NotEmpty().WithMessage("Genre is required.")
                .Matches(@"^[A-Z].{0,19}$").WithMessage("Genre must start with an uppercase letter and can be up to 20 characters long.");

            RuleFor(book => book.Description)
                .NotEmpty().WithMessage("Description is required.")
                .Matches(@"^[A-Z].{0,19}$").WithMessage("Description must start with an uppercase letter and can be up to 20 characters long.");

            RuleFor(book => book.PickOfDate)
                .LessThan(book => book.ReturnDate).WithMessage("Pick-up date must be before return date.")
                .Must(date => date >= DateTime.Now.Date).WithMessage("Pick-up date must be today or in the future.");


            RuleFor(book => book.ReturnDate)
                .NotEmpty().WithMessage("Return date is required.")
                .GreaterThan(book => book.PickOfDate).WithMessage("Return date must be after pick-up date.")
                .Must(date => date > DateTime.Now.Date).WithMessage("Return date must be in the future.");

        }
    }
}
