using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sw1Tech.App.Interfaces;
using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Entities.Filter;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Service.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/RegistroExportacao")]
    [Authorize("Bearer")]
    public class RegistroExportacaoController : Controller
    {
        private readonly IRegistroExportacaoAppService _serviceApp;
        private ValidationResult _validationResult;

        public RegistroExportacaoController(IRegistroExportacaoAppService serviceApp)
        {
            _serviceApp = serviceApp;
        }
        [HttpPost]
        [Route("DoPesquisar")]
        public dynamic DoPesquisar([FromBody] RegistroExportacaoFilter filter = null)
        {
            if (filter != null)
            {
                if (filter.Id != 0)
                {
                    return _serviceApp.DoObterPor(p => p.Id.Equals(filter.Id));
                }
                else if(filter.Tabela != "" && filter.Tabela != null)
                {
                    return _serviceApp.DoObterPor(p => p.Tabela.Contains(filter.Tabela));
                }
            }
            return _serviceApp.DoObterTodos();
        }

        [HttpPost]
        [Route("DoSalvar")]
        public dynamic DoSalvar([FromBody] RegistroExportacao registroExportacao)
        {
            if (registroExportacao.Id == 0)
            {
                _validationResult = _serviceApp.DoAdicionar(registroExportacao);
            }
            else
            {
                _validationResult = _serviceApp.DoAtualizar(registroExportacao);
            }
            return new { validationResult = _validationResult, registroExportacao.Id };
        }

    }
}