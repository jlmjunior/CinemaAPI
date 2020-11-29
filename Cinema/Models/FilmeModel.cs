using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Models
{
    public class FilmeModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public TimeSpan Duracao { get; set; }
        public string Sinopse { get; set; }
        public string Link { get; set; }
        public decimal Nota { get; set; }
        public List<string> Generos { get; set; }
    }
}