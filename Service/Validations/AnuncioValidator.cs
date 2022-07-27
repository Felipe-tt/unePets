using Domain;
using FluentValidation;

namespace Service.Validations
{
    public class AnuncioValidator : AbstractValidator<Anuncio>
    {
        public AnuncioValidator()
        {
            RuleFor(a => a.Nome).NotEmpty().WithMessage("Nome deve ser informado.");
            RuleFor(a => a.Nome).Length(3, 50).WithMessage("Nome deve conter entre 3 e 50 caracteres.");

            RuleFor(a => a.Porte).NotEmpty().WithMessage("Porte do animal deve ser informado.");

            RuleFor(a => a.Sexo).NotEmpty().WithMessage("Sexo do animal deve ser informado.");

            RuleFor(a => a.Tipo).NotEmpty().WithMessage("Tipo do animal deve ser informado.");

            RuleFor(a => a.DataNascimento).NotEmpty().WithMessage("Data de nascimento do animal deve ser informado.");

            RuleFor(a => a.Descricao).NotEmpty().WithMessage("Descricao deve ser informado.");
            RuleFor(a => a.Descricao).Length(3, 200).WithMessage("Descrição deve conter entre 3 e 200 caracteres.");

        }
    }
}
