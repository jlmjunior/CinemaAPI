using Cinema.Data;
using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cinema.Controllers
{
    public class MoviesController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage BuscarFilme(int value)
        {
            if (value == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"mensagem: valor não informado");
            }

            try
            {
                MovieDAO movieDAO = new MovieDAO();

                FilmeModel filme = movieDAO.GetMovie(value);
                List<string> generos = movieDAO.GetGenres(value);

                if (filme != null)
                {
                    filme.Generos = generos;

                    return Request.CreateResponse(HttpStatusCode.OK, filme);
                }
                    
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, $"mensagem: filme não encontrado");
        }
    }
}
