using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogo.Presentation.Models
{
    public class Comentario
    {
        public int PokemonId { get; set; }   
        public string Usuario { get; set; }  
        public string Texto { get; set; }   
        public DateTime Fecha { get; set; } 
    }
}