using Cinema.Data;
using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Cinema.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MoviesController : ApiController
    {
        private readonly MovieDAO movieDAO = new MovieDAO();

        [HttpGet]
        public HttpResponseMessage BuscarFilme(int value)
        {
            if (value == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"mensagem: valor não informado");
            }

            try
            {
                FilmeModel filme = movieDAO.GetMovie(value);
                List<string> generos = movieDAO.GetGenres(value);
                List<SessaoModel> sessoes = movieDAO.GetSessions(value);

                if (filme != null)
                {
                    filme.Generos = generos.Count > 0 ? generos : null;
                    filme.Sessoes = sessoes.Count > 0 ? sessoes : null;

                    return Request.CreateResponse(HttpStatusCode.OK, filme);
                }
                    
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, $"mensagem: filme não encontrado");
        }

        [HttpGet]
        public HttpResponseMessage BuscarAssentos(int idSessao)
        {
            AssentoColunasModel assentosColunas = new AssentoColunasModel();

            List<AssentoModel> assentos = movieDAO.BuscarAssentos(idSessao);

            foreach(AssentoModel assento in assentos)
            {
                switch (assento.Linha)
                {
                    case "A":
                        assentosColunas.A.Add(assento);
                        break;
                    case "B":
                        assentosColunas.B.Add(assento);
                        break;
                    case "C":
                        assentosColunas.C.Add(assento);
                        break;
                    case "D":
                        assentosColunas.D.Add(assento);
                        break;
                    case "E":
                        assentosColunas.E.Add(assento);
                        break;
                    case "F":
                        assentosColunas.F.Add(assento);
                        break;
                    case "G":
                        assentosColunas.G.Add(assento);
                        break;
                    case "H":
                        assentosColunas.H.Add(assento);
                        break;
                    case "I":
                        assentosColunas.I.Add(assento);
                        break;
                    case "J":
                        assentosColunas.J.Add(assento);
                        break;
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, assentosColunas);
        }

        [HttpPost]
        public HttpResponseMessage ComprarIngresso(IngressoModel ingresso)
        {
            if (ingresso == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            if (movieDAO.ExisteIngresso(ingresso.IdSessao, ingresso.IdAssento))
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            try
            {
                movieDAO.ComprarIngresso(ingresso);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        public HttpResponseMessage MeuIngresso(UsuarioModel usuario)
        {
            if (usuario == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            try
            {
                var ingresso = movieDAO.BuscarIngresso(usuario.Usuario);

                return Request.CreateResponse(HttpStatusCode.OK, ingresso);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
