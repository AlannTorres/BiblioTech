using FluentValidation;

namespace BiblioTech.Domain.Validations
{
    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O campo nome é obrigatório.")
                .MaximumLength(100).WithMessage("O campo nome não pode ter mais de 100 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O campo e-mail é obrigatório.")
                .MaximumLength(100).WithMessage("O campo e-mail não pode ter mais de 100 caracteres.")
                .EmailAddress().WithMessage("O campo e-mail não é um endereço de e-mail válido.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("O campo senha é obrigatório.");

            RuleFor(x => x.Telephone)
                .NotEmpty().WithMessage("O campo telefone é obrigatório.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("O campo endereço é obrigatório.");

        }
    }
}
