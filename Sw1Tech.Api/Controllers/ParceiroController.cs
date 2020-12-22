using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sw1Tech.App.Interfaces;
using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Entities.Filter;
using Sw1Tech.Domain.Validation;
using System.Collections.Generic;

namespace Sw1Tech.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Parceiro")]
    [Authorize("Bearer")]
    public class ParceiroController : Controller
    {
        private readonly IParceiroAppService _serviceApp;
        private ValidationResult _validationResult;

        public ParceiroController(IParceiroAppService serviceApp)
        {
            _serviceApp = serviceApp;
        }

        [HttpPost]
        [Route("DoPesquisar")]
        public dynamic DoPesquisar([FromBody] ParceiroFilter filter = null)
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
                else if (filter.Razao != "")
                {
                    return _serviceApp.DoObterPor(p => p.Razao.Contains(filter.Razao));
                }
                else if (filter.Cnpj != "")
                {
                    return _serviceApp.DoObterPor(p => p.Cnpj.Contains(filter.Cnpj));
                }
                else if (filter.Cpf != "")
                {
                    return _serviceApp.DoObterPor(p => p.Cpf.Contains(filter.Cpf));
                }
                else if (filter.Email != "")
                {
                    return _serviceApp.DoObterPor(p => p.Email.Contains(filter.Email));
                }
                else if (filter.Fone != "")
                {
                    return _serviceApp.DoObterPor(p => p.Fone.Contains(filter.Fone));
                }
                else if (filter.Celular != "")
                {
                    return _serviceApp.DoObterPor(p => p.Celular.Contains(filter.Celular));
                }
                else if (filter.Contato != "")
                {
                    return _serviceApp.DoObterPor(p => p.Contato.Contains(filter.Contato));
                }
                else if (filter.Logradouro != "")
                {
                    return _serviceApp.DoObterPor(p => p.Localizacao.Logradouro.Contains(filter.Logradouro));
                }
                else if (filter.DhAtualizacao.ToString() != "")
                {
                    return _serviceApp.DoObterPor(p => p.DhAtualizacao > filter.DhAtualizacao);
                }
            }
            return _serviceApp.DoObterTodos();
        }

        [HttpPost]
        [Route("DoSalvar")]
        public dynamic DoSalvar([FromBody] Parceiro parceiro)
        {
            try
            {
                if (parceiro.Id == 0)
                {
                    _validationResult = _serviceApp.DoAdicionar(parceiro);
                }
                else
                {
                    _validationResult = _serviceApp.DoAtualizar(parceiro);
                }
            }
            catch (System.Exception ex)
            {
                _validationResult.Add("Objeto contem campos nulo. {0] " + ex.Message.ToString());
            }
            return new { validationResult = _validationResult, parceiro.Id};
        }

        [HttpPost]
        [Route("DoApagar")]
        public dynamic DoApagar([FromBody] Parceiro parceiro)
        {
            try
            {
                _validationResult = _serviceApp.DoDeletar(parceiro);
            }
            catch (System.Exception ex)
            {
                _validationResult.Add("Objeto contem campos nulo. {0] " + ex.Message.ToString());
            }
            return new { validationResult = _validationResult, parceiro.Id };
        }

        [HttpPost]
        [Route("DoSalvarLstParceiros")]
        public dynamic DoSalvarLstParceiros([FromBody] IEnumerable<Parceiro> lstParceiros)
        {
            try
            {
                _validationResult = _serviceApp.DoSalvarLstParceiros(lstParceiros);
            }
            catch (System.Exception ex)
            {
                _validationResult.Add("Objeto contem campos nulo. {0] " + ex.Message.ToString());
            }
            return new { validationResult = _validationResult};
        }

    }
}