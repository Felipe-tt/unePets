using Domain;
using FluentValidation;

namespace Service.Validations
{
    class PessoaValidator : AbstractValidator<Pessoa>
    {
        public PessoaValidator()
        {
            RuleFor(p => p.Nome).NotEmpty().WithMessage("Nome deve ser informado.");
            RuleFor(p => p.Nome).Length(3, 50).WithMessage("Nome deve conter entre 3 e 50 caracteres.");

            RuleFor(p => p.Profissao).NotEmpty().WithMessage("Profissão deve ser informado.");
            RuleFor(p => p.Profissao).Length(3, 50).WithMessage("Profissão deve conter entre 3 e 50 caracteres.");

            RuleFor(a => a.Descricao).NotEmpty().WithMessage("Descricao deve ser informado.");
            RuleFor(a => a.Descricao).Length(3, 200).WithMessage("Descrição deve conter entre 3 e 200 caracteres.");

            RuleFor(p => p.CPF).NotEmpty().WithMessage("CPF deve ser informado.");
            RuleFor(p => p.CPF).Length(11).WithMessage("CPF deve conter 11 caracteres.");

            RuleFor(p => p.CPF).Custom((x, y) =>
            {
                if (!CommonValidations.IsCpf(x))
                {
                    y.AddFailure("CPF inválido.");
                }
            });

            RuleFor(p => p.CEP).NotEmpty().WithMessage("CEP deve ser informado.");
            RuleFor(p => p.CEP).Length(8).WithMessage("CEP deve conter 8 caracteres.");
            // RuleFor(p => p.CEP).Matches(@"^\d{5}-\d{3}$").WithMessage("CEP invalido.");

            RuleFor(p => p.Email).NotEmpty().WithMessage("Email deve ser informado.");
            RuleFor(p => p.Email).Length(8, 100).WithMessage("Email deve conter entre 8 e 100 caracteres.");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Email Inválido.");

            RuleFor(p => p.Telefone).NotEmpty().WithMessage("Telefone deve ser informado.");
            RuleFor(p => p.Telefone).Length(11).WithMessage("Telefone deve conter 11 caracteres.");
            //RuleFor(p => p.Telefone).Matches(@"(\(?\d{2}\)?\s)?(\d{4,5}\-\d{4})").WithMessage("Telefone inválido.");

            RuleFor(p => p.Cidade).NotEmpty().WithMessage("Cidade deve ser informada.");

            RuleFor(p => p.Senha).NotEmpty().WithMessage("Senha deve ser informado.");

            RuleFor(p => p.DataNascimento).NotEmpty().WithMessage("DataNascimento deve ser informada.");

            RuleFor(p => p.UF).NotEmpty().WithMessage("UF deve ser informada.");
        }
    }
}
