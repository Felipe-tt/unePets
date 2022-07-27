using FluentValidation.Results;
using System.Text;

namespace Service.Extensions
{
    static class CommonExtensions
    {
        public static Response ToResponse(this ValidationResult result)
        {
            if (result.IsValid)
            {
                return new Response()
                {
                    Sucesso = true,
                    Mensagem = "Validação bem sucedida."
                };
            }

            StringBuilder sb = new StringBuilder();
            foreach (ValidationFailure item in result.Errors)
            {
                sb.AppendLine(item.ErrorMessage);
            }

            return new Response()
            {
                Sucesso = false,
                Mensagem = sb.ToString()
            };


        }
    }
}
