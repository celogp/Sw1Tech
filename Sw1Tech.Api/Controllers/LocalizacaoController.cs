using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sw1Tech.App.Interfaces;
using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Entities.Filter;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Localizacao")]
    [Authorize("Bearer")]
    public class LogradouroController : Controller
    {
        private readonly ILocalizacaoAppService _serviceApp;
        private ValidationResult _validationResult;

        public LogradouroController(ILocalizacaoAppService serviceApp)
        {
            _serviceApp = serviceApp;
        }

        [HttpPost]
        [Route("DoPesquisar")]
        public dynamic DoPesquisar([FromBody] LocalizacaoFilter filter = null)
        {
            if (filter != null)
            {
                if (filter.Id != 0)
                {
                    return _serviceApp.DoObterPor(p => p.Id.Equals(filter.Id));
                }
                else if(filter.Logradouro != "")
                {
                    return _serviceApp.DoObterPor(p => p.Logradouro.Contains(filter.Logradouro));
                }
                else if (filter.Cep != "")
                {
                    return _serviceApp.DoObterPor(p => p.Cep.Contains(filter.Cep));
                }
            }
            return _serviceApp.DoObterTodos();
        }

        [HttpPost]
        [Route("DoSalvar")]
        public dynamic DoSalvar([FromBody] Localizacao localizacao)
        {
            try
            {
                if (localizacao.Id == 0)
                {
                    _validationResult = _serviceApp.DoAdicionar(localizacao);
                }
                else
                {
                    _validationResult = _serviceApp.DoAtualizar(localizacao);
                }
            }
            catch (System.Exception)
            {
                _validationResult.Add("Objeto contem campos nulo.");
            }
            return new { validationResult = _validationResult, Id = localizacao.Id };
        }

        [HttpPost]
        [Route("DoApagar")]
        public dynamic DoApagar([FromBody] Localizacao localizacao)
        {
            try
            {
                _validationResult = _serviceApp.DoDeletar(localizacao);
            }
            catch (System.Exception)
            {
                _validationResult.Add("Objeto contem campos nulo.");
            }
            return new { validationResult = _validationResult, Id = localizacao.Id };
        }

        [HttpPost]
        [Route("DoPesquisarLocalidade")]
        public dynamic DoPesquisarLocalidade([FromBody] LocalidadeFilter filter = null)
        {
            return _serviceApp.DoObterLocalidade(p => p.Localidade.Contains(filter.Localidade));
        }

        [HttpPost]
        [Route("DoPesquisarBairro")]
        public dynamic DoPesquisarBairro([FromBody] BairroFilter filter = null)
        {
            return _serviceApp.DoObterBairro(p => p.Bairro.Contains(filter.Bairro));
        }

        [HttpPost]
        [Route("DoPesquisarCep")]
        public dynamic DoPesquisarCep([FromBody] LocalizacaoFilter filter = null)
        {
            if (filter != null)
            {
                if (filter.StrViaCep != "")
                {
                    return _serviceApp.DoBuscarPorCEP(filter.StrViaCep);
                }
                else if (filter.Cep != "")
                {
                    return _serviceApp.DoBuscarPorCEP(filter.Cep);
                }
            }
            return new Localizacao();
        }
    }
}