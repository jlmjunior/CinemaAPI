using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Models
{
    public class MeuIngressoModel
    {
        public int IdSala { get; set; }
        public string Linha { get; set; }
        public int Coluna { get; set; }
        public DateTime DtInicio { get; set; }
        public string Titulo { get; set; }
    }
}