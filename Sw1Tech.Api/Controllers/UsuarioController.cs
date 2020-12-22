using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Sw1Tech.App.Interfaces;
using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Entities.Filter;
using Sw1Tech.Domain.Validation;
using Sw1Tech.Service.Api;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Sw1Tech.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Usuario")]
    [Authorize("Bearer")]
    public class UsuarioController : Controller
    {
        private int _status;
        private string _message;
        private string _token = "";
        private DateTime dataCriacao;
        private DateTime dataExpiracao;

        private readonly IUsuarioAppService _serviceApp;

        private ValidationResult _validationResult;

        public UsuarioController(IUsuarioAppService serviceApp)
        {
            _serviceApp = serviceApp;
        }

        [HttpPost]
        [Route("DoPesquisar")]
        public dynamic DoPesquisar([FromBody] UsuarioFilter filter = null)
        {
            if (filter != null)
            {
                if (filter.Id != 0)
                {
                    return _serviceApp.DoObterPor(p => p.Id.Equals(filter.Id));
                }
                else if (filter.Nome != "")
                {
                    return _serviceApp.DoObterPor(p => p.Nome.Contains(filter.Nome));
                }
            }
            return _serviceApp.DoObterTodos();
        }

        [HttpPost]
        [Route("DoSalvar")]
        public dynamic DoSalvar([FromBody] Usuario usuario)
        {
            try
            {
                if (usuario.Id == 0)
                {
                    _validationResult = _serviceApp.DoAdicionar(usuario);
                }
                else
                {
                    _validationResult = _serviceApp.DoAtualizar(usuario);
                }

            }
            catch (Exception)
            {
                _validationResult.Add("Objeto contem campos nulo.");
            }

            return new { validationResult = _validationResult, Id = usuario.Id };
        }

        [HttpPost]
        [Route("DoApagar")]
        public dynamic DoApagar([FromBody] Usuario usuario)
        {
            try
            {
                _validationResult = _serviceApp.DoDeletar(usuario);
            }
            catch (Exception)
            {
                _validationResult.Add("Objeto contem campos nulo.");
            }
            return new { validationResult = _validationResult, Id = usuario.Id };
        }


        [HttpPost]
        [Route("DoLogin")]
        [AllowAnonymous]
        public dynamic DoLogin([FromBody] Usuario usuario,
                               [FromServices] SigningConfigurations signingConfigurations,
                               [FromServices] TokenConfigurations tokenConfigurations)
        {
            _status = -1;
            _message = "NÃO Autenticado.";
            usuario = _serviceApp.DoLogin(usuario);
            if (usuario != null)
            {
                _status = 1;
                _message = "Autenticado.";

                ClaimsIdentity identity = new ClaimsIdentity(
                        new GenericIdentity(usuario.Id.ToString(), "Login"),
                        new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, usuario.Id.ToString())
                        }
                    );

                dataCriacao = DateTime.Now;
                dataExpiracao = dataCriacao + TimeSpan.FromHours(tokenConfigurations.Hours);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });
                _token = handler.WriteToken(securityToken);
            }
            return new { status = _status, message = _message, result = usuario, token = _token, dataCriacao = dataCriacao, dataExpiracao = dataExpiracao};
        }

        [HttpGet]
        [Route("DoAuthenticado")]
        public dynamic DoAuthenticado()
        {
            _status = 1;
            _message = "Autenticado.";
            return new { status = _status, message = _message};
        }
    }
}