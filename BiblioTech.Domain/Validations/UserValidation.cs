using FluentValidation;

namespace BiblioTech.Domain.Validations
{
    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Campo nome obrigatorio")
                .NotEmpty()
                .Length(100);

            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .Length(100)
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);

            RuleFor(x => x.Password)
                .NotEmpty();

            RuleFor(x => x.Telephone);

            RuleFor(x => x.Address);

            RuleFor(x => x.Password);

        }
    }
}
