using FluentValidation;
using LibraryManagementAPI.DTOs.Members;

namespace LibraryManagementAPI.Validators
{
    public class CreateMemberValidator : AbstractValidator<CreateMemberRequestDto>
    {
        public CreateMemberValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(150);
        }
    }
}
