using FluentValidation;

namespace BiblioTech.Domain.Validations;

public class BookValidation : AbstractValidator<Book>
{
    public BookValidation()
    {
        RuleFor(x => x.Title)
            .NotEmpty();

        RuleFor(x => x.Description)
            .NotEmpty();

        RuleFor(x => x.ISBN) 
            .NotEmpty();

        RuleFor(x => x.Publishing)
            .NotEmpty();

        RuleFor(x => x.Quantity)
            .NotEmpty();

        RuleFor(x => x.Year_publication)
            .NotEmpty();
    }
}
