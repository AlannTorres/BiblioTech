using FluentValidation;

namespace BiblioTech.Domain.Validations;

public class ReserveValidation : AbstractValidator<Reserve>
{
    public ReserveValidation()
    {
        RuleFor(x => x.User)
            .NotNull();

        RuleFor(x => x.Book)
            .NotNull();

        RuleFor(x => x.Reserve_Date)
            .NotNull();

        RuleFor(x => x.Status_Reserve)
            .NotNull();

    }
}
