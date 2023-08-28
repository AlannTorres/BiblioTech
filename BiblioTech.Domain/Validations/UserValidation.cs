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

            RuleFor(x => x.CPF)
                .NotEmpty().WithMessage("O campo CPF é obrigatório.")
                .Must(BeValidCPF).WithMessage("O campo CPF não é válido.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O campo e-mail é obrigatório.")
                .MaximumLength(100).WithMessage("O campo e-mail não pode ter mais de 100 caracteres.")
                .EmailAddress().WithMessage("O campo e-mail não é um endereço de e-mail válido.");

            RuleFor(x => x.PasswordHash)
                .NotEmpty().WithMessage("O campo senha é obrigatório.");

            RuleFor(x => x.Telephone)
                .NotEmpty().WithMessage("O campo telefone é obrigatório.");

            RuleFor(x => x.Adress)
                .NotEmpty().WithMessage("O campo endereço é obrigatório.");

        }

        private bool BeValidCPF(string cpf)
        {
            // Remove caracteres não numéricos do CPF
            string cleanedCPF = new (cpf.Where(char.IsDigit).ToArray());

            // Verifica se o CPF possui 11 dígitos
            if (cleanedCPF.Length != 11)
                return false;

            // Verifica se todos os dígitos são iguais
            if (cleanedCPF.All(digit => digit == cleanedCPF[0]))
                return false;

            // Calcula os dígitos verificadores
            int[] cpfDigits = cleanedCPF.Select(digit => int.Parse(digit.ToString())).ToArray();
            int[] firstVerifier = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] secondVerifier = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int firstSum = cpfDigits.Take(9).Select((digit, index) => digit * firstVerifier[index]).Sum();
            int firstDigit = (firstSum % 11) < 2 ? 0 : 11 - (firstSum % 11);
            int secondSum = cpfDigits.Take(10).Select((digit, index) => digit * secondVerifier[index]).Sum();
            int secondDigit = (secondSum % 11) < 2 ? 0 : 11 - (secondSum % 11);

            // Verifica se os dígitos verificadores são válidos
            return cpfDigits[9] == firstDigit && cpfDigits[10] == secondDigit;
        }
    }
}
