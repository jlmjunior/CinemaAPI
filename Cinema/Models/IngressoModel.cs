using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Models
{
    public class IngressoModel
    {
        public string Usuario { get; set; }
        public int IdAssento { get; set; }
        public int IdSessao { get; set; }
    }
}