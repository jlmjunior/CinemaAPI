using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cinema.Models
{
    public class CadastroModel
    {
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Compare("Senha")]
        [DataType(DataType.Password)]
        public string ConfirmarSenha { get; set; }
    }
}