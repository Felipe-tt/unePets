using Domain.Enum;
using System;
using System.Collections.Generic;

namespace Domain
{
    public class Pessoa
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Cidade { get; set; }
        public string Rua { get; set; }
        public string Descricao { get; set; }
        public string Profissao { get; set; }
        public UF UF { get; set; }
        public string CEP { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool Ativo { get; set; }
        public ICollection<Mensagem> MensagensEnviadas { get; set; }
        public ICollection<Mensagem> MensagensRecebidas { get; set; }
        public ICollection<Anuncio> Anuncios { get; set; }
        public ICollection<Anuncio> Interesses { get; set; }
        public int Imagem { get; set; }


        public int Idade
        {
            get
            {
                DateTime today = DateTime.Today;
                int age = today.Year - this.DataNascimento.Year;

                if (this.DataNascimento.Date > today.AddYears(-age)) age--;
                return age;
            }
        }




    }
}
