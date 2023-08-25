using FluentValidation;

namespace BiblioTech.Domain.Validations;

public class LoanValidation : AbstractValidator<Loan>
{
    public LoanValidation()
    {
        RuleFor(x => x.User)
            .NotEmpty();

        RuleFor(x => x.Books)
            .NotEmpty();

        RuleFor(x => x.Loan_Date)
            .NotEmpty();

        RuleFor(x => x.Due_Date)
            .NotEmpty();

        RuleFor(x => x.Status_Checkout)
            .NotEmpty();
    }
}
