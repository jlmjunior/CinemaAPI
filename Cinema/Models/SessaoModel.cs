using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Models
{
    public class SessaoModel
    {
        public int Id { get; set; }
        public DateTime Horario { get; set; }
        public int  IdSala { get; set; }
    }
}