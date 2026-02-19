using FluentValidation;
using LibraryManagementAPI.DTOs.Loans;

namespace LibraryManagementAPI.Validators
{
    public class BorrowBookValidator : AbstractValidator<BorrowBookRequestDto>
    {
        public BorrowBookValidator()
        {
            RuleFor(x => x.BookId).GreaterThan(0);
            RuleFor(x => x.MemberId).GreaterThan(0);

            RuleFor(x => x.LoanDays)
                .GreaterThan(0)
                .LessThanOrEqualTo(30);
        }
    }
}
