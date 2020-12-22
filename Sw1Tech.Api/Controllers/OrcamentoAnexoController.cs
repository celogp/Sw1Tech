using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sw1Tech.App.Interfaces;
using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Entities.Filter;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/OrcamentoAnexo")]
    [Authorize("Bearer")]
    public class OrcamentoAnexoController : Controller
    {
        private readonly IOrcamentoAnexoAppService _serviceApp;

        private ValidationResult _validationResult;

        public OrcamentoAnexoController(IOrcamentoAnexoAppService serviceApp)
        {
            _serviceApp = serviceApp;
        }

        [HttpPost]
        [Route("DoPesquisar")]
        public dynamic DoPesquisar([FromBody] OrcamentoAnexoFilter filter = null)
        {
            if (filter != null)
            {
                if (filter.OrcamentoId != 0)
                {
                    return _serviceApp.DoObterPor(p => p.OrcamentoId.Equals(filter.OrcamentoId));
                }
            }
            return _serviceApp.DoObterTodos();
        }

        [HttpPost]
        [Route("DoSalvar")]
        public dynamic DoSalvar([FromBody] OrcamentoAnexo orcamentoAnexo)
        {
            try
            {
                if (orcamentoAnexo.Id == 0)
                {
                    _validationResult = _serviceApp.DoAdicionar(orcamentoAnexo);
                }
                else
                {
                    _validationResult = _serviceApp.DoAtualizar(orcamentoAnexo);
                }
            }
            catch (System.Exception)
            {
                _validationResult.Add("Objeto contem campos nulo.");
            }
            return new { validationResult = _validationResult, Id = orcamentoAnexo.Id };
        }

        [HttpPost]
        [Route("DoApagar")]
        public dynamic DoApagar([FromBody] OrcamentoAnexo orcamentoAnexo)
        {
            try
            {
                _validationResult = _serviceApp.DoDeletar(orcamentoAnexo);
            }
            catch (System.Exception)
            {
                _validationResult.Add("Objeto contem campos nulo.");
            }
            return new { validationResult = _validationResult, Id = orcamentoAnexo.Id };
        }
    }
}