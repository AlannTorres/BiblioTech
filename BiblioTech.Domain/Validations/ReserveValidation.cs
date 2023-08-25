using FluentValidation;

namespace BiblioTech.Domain.Validations;

public class ReserveValidation : AbstractValidator<Reserve>
{
    public ReserveValidation()
    {
        RuleFor(x => x.User)
            .NotEmpty();

        RuleFor(x => x.Book)
            .NotEmpty();

        RuleFor(x => x.Reserve_Date)
            .NotEmpty();

        RuleFor(x => x.Reserve_Status)
            .NotEmpty();

    }
}
