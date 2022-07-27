using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPresentationLayer.Models.Anuncio
{
    public class PessoaPerfilQueryViewModel
    {
        public int PessoaID { get; set; }
        public string Descricao { get; set; }
        public string Profissao { get; set; }
        public string Nome { get; set; }
        public string Cidade { get; set; }
        public UF UF { get; set; }
        public string CEP { get; set; }
        public string Rua { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int Imagem { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
