using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sw1Tech.App.Interfaces;
using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Entities.Filter;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Service.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Financeiro")]
    [Authorize("Bearer")]
    public class FinanceiroController : Controller
    {
        private readonly IFinanceiroAppService _serviceApp;
        private ValidationResult _validationResult;

        public FinanceiroController(IFinanceiroAppService serviceApp)
        {
            _serviceApp = serviceApp;
        }

        [HttpPost]
        [Route("DoPesquisar")]
        public dynamic DoPesquisar([FromBody] FinanceiroFilter filter = null)
        {
            if (filter != null)
            {
                if (filter.Id != 0)
                {
                    return _serviceApp.DoObterPor(p => p.Id.Equals(filter.Id));
                }else if (filter.OrcamentoId != 0){
                    return _serviceApp.DoObterPor(p => p.OrcamentoId.Equals(filter.OrcamentoId));
                }
            }
            return _serviceApp.DoObterTodos();
        }

        [HttpPost]
        [Route("DoSalvar")]
        public dynamic DoSalvar([FromBody] Financeiro financeiro)
        {
            try
            {
                if (financeiro.Id == 0)
                {
                    _validationResult = _serviceApp.DoAdicionar(financeiro);
                }
                else
                {
                    _validationResult = _serviceApp.DoAtualizar(financeiro);
                }
            }
            catch (System.Exception)
            {
                _validationResult.Add("Objeto contem campos nulo.");
            }
            return new { validationResult = _validationResult, Id = financeiro.Id };
        }

        [HttpPost]
        [Route("DoApagar")]
        public dynamic DoApagar([FromBody] Financeiro financeiro)
        {
            try
            {
                _validationResult = _serviceApp.DoDeletar(financeiro);
            }
            catch (System.Exception)
            {
                _validationResult.Add("Objeto contem campos nulo.");
            }
            return new { validationResult = _validationResult, Id = financeiro.Id };
        }

        [HttpPost]
        [Route("DoSalvarLstFinanceiro")]
        public dynamic DoSalvarLstFinanceiro([FromBody] IEnumerable<Financeiro> lstFinanceiro)
        {
            try
            {
                _validationResult = _serviceApp.DoSalvarLstFinanceiro(lstFinanceiro);
            }
            catch (System.Exception)
            {
                _validationResult.Add("Objeto contem campos nulo.");
            }
            return new { validationResult = _validationResult };
        }
        
        [HttpPost]
        [Route("DoApagarLstFinanceiro")]
        public dynamic DoApagarLstFinanceiro([FromBody] IEnumerable<Financeiro> lstFinanceiro)
        {
            try
            {
                _validationResult = _serviceApp.DoApagarLstFinanceiro(lstFinanceiro);
            }
            catch (System.Exception)
            {
                _validationResult.Add("Objeto contem campos nulo.");
            }
            return new { validationResult = _validationResult };
        }
    }
}