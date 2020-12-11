using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Models
{
    public class UsuarioModel
    {
        public string Usuario { get; set; }
        public string Token { get; set; }
        public int Role { get; set; }
    }
}