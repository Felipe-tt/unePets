using Domain;
using FluentValidation;

namespace Service.Validations
{
    public class MensagemValidator : AbstractValidator<Mensagem>
    {
        public MensagemValidator()
        {
            RuleFor(a => a.Corpo).NotEmpty().WithMessage("A mensagem deve ser informada.");
        }
    }
}
