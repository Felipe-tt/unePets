using Domain.Enum;
using System;
using System.Collections.Generic;

namespace Domain
{
    public class Anuncio

    {
        public int ID { get; set; }
        public DateTime DataAnuncio { get; set; }
        public string Descricao { get; set; }
        public string Nome { get; set; }
        public Status Status { get; set; }
        public bool EhDeficiente { get; set; }
        public bool EhCastrado { get; set; }
        public DateTime DataNascimento { get; set; }
        public int PessoaID { get; set; }
        public Pessoa Pessoa { get; set; }
        public Sexo Sexo { get; set; }
        public Porte Porte { get; set; }
        public TipoPet Tipo { get; set; }
        public ICollection<Pessoa> PessoasInteressadas { get; set; }
        public ICollection<Mensagem> MensagensEnviadas { get; set; }
        public ICollection<Mensagem> MensagensRecebidas { get; set; }
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
