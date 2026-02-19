using FluentValidation;
using LibraryManagementAPI.DTOs.Books;

namespace LibraryManagementAPI.Validators
{
    public class CreateBookValidator : AbstractValidator<CreateBookRequestDto>
    {
        public CreateBookValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Isbn)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.CopiesTotal)
                .GreaterThan(0);

            RuleFor(x => x.AuthorId)
                .GreaterThan(0);
        }
    }
}
