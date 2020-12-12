using Cinema.Data;
using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Cinema.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AdminController : ApiController
    {
        private readonly AdminDAO adminDAO = new AdminDAO();

        [HttpGet]
        public HttpResponseMessage RetornarUsuarios()
        {
            List<UsuarioModel> usuarios = adminDAO.BuscarUsuarios();

            return Request.CreateResponse(HttpStatusCode.OK, new { users = usuarios });
        }

        [HttpGet]
        public HttpResponseMessage RetornarSessoes()
        {
            DataTable dt = adminDAO.BuscarSessoes();
            var sessoes = new object();

            if (dt.Rows.Count > 0)
            {
                sessoes = dt.AsEnumerable().Select(row => new
                {
                    Id = row.Field<int>("id"),
                    Titulo = row.Field<string>("titulo"),
                    Horario = row.Field<DateTime>("data_inicio"),
                    IdSala = row.Field<int>("id_sala")
                }).ToList();
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { sessions = sessoes });
        }
    }
}
