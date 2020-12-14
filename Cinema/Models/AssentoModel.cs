using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Models
{
    public class AssentoModel
    {
        public int Id { get; set; }
        public string Linha { get; set; }
        public int Coluna { get; set; }
        public int Especial { get; set; }
        public bool Ocupado { get; set; }
    }
}