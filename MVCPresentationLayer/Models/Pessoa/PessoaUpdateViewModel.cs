using Domain.Enum;
using System;

namespace MVCPresentationLayer.Models.Pessoa
{
    public class PessoaUpdateViewModel
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Cidade { get; set; }
        public UF UF { get; set; }
        public string CEP { get; set; }
        public string Rua { get; set; }

        //public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string NovaSenha { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
