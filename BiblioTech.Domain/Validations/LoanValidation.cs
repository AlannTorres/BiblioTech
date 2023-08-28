using FluentValidation;

namespace BiblioTech.Domain.Validations;

public class LoanValidation : AbstractValidator<Loan>
{
    public LoanValidation()
    {
        RuleFor(x => x.User)
            .NotNull();

        RuleFor(x => x.Books)
            .NotNull();

        RuleFor(x => x.Loan_Date)
            .NotNull();

        RuleFor(x => x.Status_Checkout)
            .NotNull();
    }
}
