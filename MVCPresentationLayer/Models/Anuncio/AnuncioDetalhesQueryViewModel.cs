using Domain.Enum;
using System;
using System.Collections.Generic;

namespace MVCPresentationLayer.Models.Anuncio
{
    public class DetalhesQueryViewModel
    {
        public int ID { get; set; }
        public string Descricao { get; set; }  //textarea
        public string Nome { get; set; }   //input type text
        public Status Status { get; set; } //select
        public bool EhDeficiente { get; set; } //input type checkbox 
        public bool EhCastrado { get; set; }  //input type checkbox 
        public DateTime DataNascimento { get; set; } //input type date
        public int Idade { get; set; }
        public DateTime DataAnuncio { get; set; } //input type date
        public Sexo Sexo { get; set; }  //select
        public Porte Porte { get; set; } //select
        public TipoPet Tipo { get; set; } //select
        public Domain.Pessoa Pessoa { get; set; }
        public Domain.Anuncio Anuncio { get; set; }
        public int PessoaID { get; set; }

        public ICollection<Domain.Pessoa> PessoasInteressadas { get; set; }
        public int QtdPessoasInteressadas
        {
            get
            {
                return PessoasInteressadas != null ? PessoasInteressadas.Count : 0;
            }
        }
    }
}
