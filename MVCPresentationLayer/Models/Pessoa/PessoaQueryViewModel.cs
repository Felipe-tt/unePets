using System;

namespace MVCPresentationLayer.Models.Pessoa
{
    public class PessoaQueryViewModel
    {
        public string Nome { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string CEP { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }
        public int? PessoaID { get; set; }

    }
}
