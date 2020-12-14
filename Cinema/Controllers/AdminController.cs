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
        public HttpResponseMessage RetornarUsuarios(string token)
        {
            if (!adminDAO.HasAdminToken(token))
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            DataTable dt = adminDAO.BuscarUsuarios();
            var usuarios = new object();

            if (dt.Rows.Count > 0)
            {
                usuarios = dt.AsEnumerable().Select(row => new
                {
                    Usuario = row.Field<string>("usuario"),
                    Role = row.Field<int>("id_role"),
                    DataCriacao = row.Field<DateTime>("data_criacao")
                }).ToList();
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { users = usuarios });
        }

        [HttpGet]
        public HttpResponseMessage RetornarSessoes(string token)
        {
            if (!adminDAO.HasAdminToken(token))
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

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

        [HttpGet]
        public HttpResponseMessage RetornarFilmes()
        {
            DataTable dt = adminDAO.BuscarFilmes();
            var filmes = new object();

            if (dt.Rows.Count > 0)
            {
                filmes = dt.AsEnumerable().Select(row => new
                {
                    Id = row.Field<int>("id"),
                    Titulo = row.Field<string>("titulo")
                }).ToList();
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { movies = filmes });
        }

        [HttpGet]
        public HttpResponseMessage RetornarSalas()
        {
            DataTable dt = adminDAO.BuscarSalas();
            var salas = new object();

            if (dt.Rows.Count > 0)
            {
                salas = dt.AsEnumerable().Select(row => new
                {
                    Id = row.Field<int>("id"),
                    Descricao = row.Field<string>("descricao")
                }).ToList();
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { rooms = salas });
        }

        [HttpDelete]
        public HttpResponseMessage DeletarUsuario(string usuario, string token)
        {
            if (!adminDAO.HasAdminToken(token))
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            if (string.IsNullOrWhiteSpace(usuario))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Mensagem: valor inválido");
            }

            try
            {
                adminDAO.DeletarUsuario(usuario);

                return Request.CreateResponse(HttpStatusCode.OK, "Mensagem: usuário deletado");
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Mensagem: Erro desconhecido");
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeletarSessao(int id, string token)
        {
            if (!adminDAO.HasAdminToken(token))
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            if (id == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Mensagem: valor inválido");
            }

            try
            {
                adminDAO.DeletarSessao(id);

                return Request.CreateResponse(HttpStatusCode.OK, "Mensagem: sessão deletada");
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Mensagem: Erro desconhecido");
            }
        }

        [HttpPost]
        public HttpResponseMessage CadastrarSessao(CadastroSessaoModel sessao, string token)
        {
            if (!adminDAO.HasAdminToken(token))
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            if (sessao == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Mensagem: valor inválido");
            }

            try
            {
                adminDAO.CadastrarSessao(sessao);

                return Request.CreateResponse(HttpStatusCode.OK, "Mensagem: sessão cadastrada");
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Mensagem: Erro desconhecido");
            }
        }
    }
}
