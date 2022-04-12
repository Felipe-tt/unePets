using Domain.Enum;
using System;
using System.Collections.Generic;

namespace MVCPresentationLayer.Models.Pessoa
{
    public class PessoaProfileViewModel
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
        public bool Ativo { get; set; }
        public ICollection<Domain.Anuncio> Anuncios { get; set; }
        public ICollection<Domain.Anuncio> Interesses { get; set; }

    }
}
