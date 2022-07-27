using Domain.Enum;
using Microsoft.AspNetCore.Http;
using System;

namespace MVCPresentationLayer.Models.Anuncio
{
    public class AnuncioInsertViewModel
    {
        public string Descricao { get; set; }  //textarea
        public string Nome { get; set; }   //input type text
        public bool EhDeficiente { get; set; } //input type checkbox 
        public bool EhCastrado { get; set; }  //input type checkbox 
        public DateTime DataNascimento { get; set; } //input type date
        public Sexo Sexo { get; set; }  //select
        public Porte Porte { get; set; } //select
        public TipoPet Tipo { get; set; } //select
        public IFormFile PrimeiraFoto { get; set; } //input type file
        public IFormFile SegundaFoto { get; set; }
        public IFormFile TerceiraFoto { get; set; }

    }
}
