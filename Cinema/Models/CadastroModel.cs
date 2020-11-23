using System.ComponentModel.DataAnnotations;

namespace Cinema.Models
{
    public class CadastroModel
    {
        public string Usuario { get; set; }

        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Compare("Senha")]
        [DataType(DataType.Password)]
        public string ConfirmarSenha { get; set; }
    }
}