﻿using Cinema.Data;
using Cinema.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Cinema.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AuthController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Cadastrar(CadastroModel cadastro)
        {
            if (cadastro == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"mensagem: usuário vazio");
            }

            try
            {
                AuthDAO authDAO = new AuthDAO();

                if (!authDAO.ExisteUsuario(cadastro.Usuario))
                {
                    Services.Hash hash = new Services.Hash();

                    string senhaMD5 = hash.GerarMD5($"{cadastro.Usuario}{cadastro.Senha}");

                    authDAO.Cadastrar(cadastro.Usuario, senhaMD5);

                    return Request.CreateResponse(HttpStatusCode.OK, $"mensagem: usuário cadastrado");
                }
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"mensagem: Erro inesperado");
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, $"mensagem: usuário já existe");
        }

        [HttpPost]
        public HttpResponseMessage Login(LoginModel login)
        {
            if (login == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            try
            {
                Services.Hash hash = new Services.Hash();
                AuthDAO authDAO = new AuthDAO();

                string senhaMD5 = hash.GerarMD5($"{login.Usuario}{login.Senha}");

                UsuarioModel usuario = authDAO.BuscarUsuario(login.Usuario, senhaMD5);

                if (usuario != null)
                {
                    usuario.Token = hash.GerarMD5($"{login.Usuario}{login.Senha}{DateTime.Now}");

                    authDAO.InserirToken(usuario.Usuario, usuario.Token);

                    return Request.CreateResponse(HttpStatusCode.OK, new { userInfo = usuario });
                }
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }


    }
}
