using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sw1Tech.App.Interfaces;
using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Entities.Filter;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Orcamento")]
    [Authorize("Bearer")]
    public class OrcamentoController : Controller
    {
        private readonly IOrcamentoAppService _serviceApp;
        private ValidationResult _validationResult;

        public OrcamentoController(IOrcamentoAppService serviceApp)
        {
            _serviceApp = serviceApp;
        }

        [HttpPost]
        [Route("DoPesquisar")]
        public dynamic DoPesquisar([FromBody] OrcamentoFilter filter = null)
        {
            if (filter != null)
            {
                if (filter.Id != 0){
                    return _serviceApp.DoObterPor(p => p.Id.Equals(filter.Id));
                }else if (filter.Numero != 0){
                    return _serviceApp.DoObterPor(p => p.Numero.Equals(filter.Numero));
                }else if (filter.NomeUsuario != ""){
                    return _serviceApp.DoObterPor(p => p.Usuario.Nome.Contains(filter.NomeUsuario));
                }else if (filter.NomeParceiro != ""){
                    return _serviceApp.DoObterPor(p => p.Parceiro.Nome.Contains(filter.NomeParceiro));
                }
            }
            return _serviceApp.DoObterTodos();
        }

        [HttpPost]
        [Route("DoSalvar")]
        public dynamic DoSalvar([FromBody] Orcamento orcamento)
        {
            try
            {
                if (orcamento.Id == 0)
                {
                    _validationResult = _serviceApp.DoAdicionar(orcamento);
                }
                else
                {
                    _validationResult = _serviceApp.DoAtualizar(orcamento);
                }
            }
            catch (System.Exception ex)
            {
                _validationResult.Add("Objeto contem campos nulo. {0}" + ex.Message.ToString());
            }
            return new { validationResult = _validationResult, orcamento.Id};
        }

        [HttpPost]
        [Route("DoApagar")]
        public dynamic DoApagar([FromBody] Orcamento orcamento)
        {
            try
            {
                _validationResult = _serviceApp.DoDeletar(orcamento);
            }
            catch (System.Exception ex)
            {
                _validationResult.Add("Objeto contem campos nulo. {0}" + ex.Message.ToString());
            }
            return new { validationResult = _validationResult, orcamento.Id };
        }

        [HttpPost]
        [Route("DoDuplicar")]
        public dynamic DoDuplicar([FromBody] Orcamento orcamento)
        {
            try
            {
                _validationResult = _serviceApp.DoDuplicar(orcamento);
            }
            catch (System.Exception ex)
            {
                _validationResult.Add("Objeto contem campos nulo. {0}" + ex.Message.ToString());
            }
            return new { validationResult = _validationResult, orcamento.Id };
        }

        [HttpPost]
        [Route("DoBloquear")]
        public dynamic DoBloquear([FromBody] Orcamento orcamento)
        {
            try
            {
                _validationResult = _serviceApp.DoBloquear(orcamento);
            }
            catch (System.Exception ex)
            {
                _validationResult.Add("Objeto contem campos nulo. {0}" + ex.Message.ToString());
            }
            return new { validationResult = _validationResult, orcamento.Id };
        }
    }
}