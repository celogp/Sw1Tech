using Microsoft.AspNetCore.Mvc;
using Sw1Tech.App.Interfaces;
using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Entities.Filter;
using Sw1Tech.Domain.Validation;
using Sw1Tech.Domain.Enums;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Sw1Tech.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/OrcamentoItem")]
    [Authorize("Bearer")]
    public class OrcamentoItemController : Controller
    {
        private readonly IOrcamentoItemAppService _serviceApp;
        private ValidationResult _validationResult;

        public OrcamentoItemController(IOrcamentoItemAppService serviceApp)
        {
            _serviceApp = serviceApp;
        }

        [HttpPost]
        [Route("DoPesquisar")]
        public dynamic DoPesquisar([FromBody] OrcamentoItemFilter filter = null)
        {
            if (filter.Id != 0)
            {
                return _serviceApp.DoObterPor(p => p.Id.Equals(filter.Id));
            }
            else if (filter.Classificacao == (int) EClassificacaoProduto.TODOS)
            {
                return _serviceApp.DoObterPor(p => p.OrcamentoId == filter.OrcamentoId && p.RootId == filter.RootId);
            }
            return _serviceApp.DoObterPor(p => p.OrcamentoId == filter.OrcamentoId && p.RootId == filter.RootId && p.Classificacao == filter.Classificacao);
        }

        [HttpPost]
        [Route("DoSalvar")]
        public dynamic DoSalvar([FromBody] OrcamentoItem orcamentoItem)
        {
            try
            {
                if (orcamentoItem.Id == 0)
                {
                    _validationResult = _serviceApp.DoAdicionar(orcamentoItem);
                }
                else
                {
                    _validationResult = _serviceApp.DoAtualizar(orcamentoItem);
                }

            }
            catch (System.Exception ex)
            {
                _validationResult.Add("Objeto contem problemas. {0}" + ex.Message.ToString());
            }

            return new { validationResult = _validationResult, orcamentoItem.Id};
        }

        [HttpPost]
        [Route("DoApagar")]
        public dynamic DoApagar([FromBody] OrcamentoItem orcamentoItem)
        {
            try
            {
                _validationResult = _serviceApp.DoDeletar(orcamentoItem);
            }
            catch (System.Exception ex)
            {
                _validationResult.Add("Objeto contem campos nulo. {0}" + ex.Message.ToString());
            }
            return new { validationResult = _validationResult, orcamentoItem.Id };
        }

        [HttpPost]
        [Route("DoObterSomaAmbientes")]
        public dynamic DoObterSomaAmbientes([FromBody] OrcamentoItemFilter filter = null)
        {
            return _serviceApp.DoObterSomaAmbientes(f => f.OrcamentoId == filter.OrcamentoId && f.Classificacao == (int) EClassificacaoProduto.FINAL);
        }
        
        [HttpPost]
        [Route("DoSalvarAmbientes")]
        public dynamic DoSalvarAmbientes([FromBody] IEnumerable<OrcamentoItem> lstOrcamentoItensFinais = null)
        {
            _validationResult = _serviceApp.DoSalvarAmbientes(lstOrcamentoItensFinais);
            return new { validationResult = _validationResult};
        }
        [HttpPost]
        [Route("DoDuplicarAmbientes")]
        public dynamic DoDuplicarAmbientes([FromBody] IEnumerable<OrcamentoItem> lstOrcamentoItensFinais = null)
        {
            try
            {
                _validationResult = _serviceApp.DoDuplicarAmbientes(lstOrcamentoItensFinais);
            }
            catch (System.Exception ex)
            {
                _validationResult.Add("Objeto contem campos nulo. {0} " + ex.Message.ToString());
            }
            return new { validationResult = _validationResult };
        }

        [HttpPost]
        [Route("DoObterSomaBases")]
        public dynamic DoObterSomaBases([FromBody] OrcamentoItemFilter filter = null)
        {
            return _serviceApp.DoObterSomaBases(f => f.OrcamentoId == filter.OrcamentoId && f.Classificacao == (int)EClassificacaoProduto.BASE);
        }

        [HttpPost]
        [Route("DoSalvarBase")]
        public dynamic DoSalvarBase([FromBody] OrcamentoItem orcamentoItem)
        {
            try
            {
                _validationResult = _serviceApp.DoSalvarBase(orcamentoItem);
            }
            catch (System.Exception ex)
            {
                _validationResult.Add("Objeto contem campos nulo. {0} " + ex.Message.ToString()) ;
            }
            return new { validationResult = _validationResult };
        }

        [HttpPost]
        [Route("DoObterSomaDetalhes")]
        public dynamic DoObterSomaDetalhes([FromBody] OrcamentoItemFilter filter = null)
        {
            return _serviceApp.DoObterSomaDetalhes(f => f.OrcamentoId == filter.OrcamentoId && f.Classificacao != (int) EClassificacaoProduto.FINAL);
        }
    }
}