using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sw1Tech.App.Interfaces;
using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Entities.Filter;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Service.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/ProdutoModelo")]
    [Authorize("Bearer")]
    public class ProdutoModeloController : Controller
    {
        private readonly IProdutoModeloAppService _serviceApp;
        private ValidationResult _validationResult;

        public ProdutoModeloController(IProdutoModeloAppService serviceApp)
        {
            _serviceApp = serviceApp;
        }

        [HttpPost]
        [Route("DoPesquisar")]
        public dynamic DoPesquisar([FromBody] ProdutoModeloFilter filter = null)
        {
            if (filter != null)
            {
                if (filter.Id != 0)
                {
                    return _serviceApp.DoObterPor(p => p.Id.Equals(filter.Id));
                }
                else if (filter.ProdutoId != 0)
                {
                    if (filter.ModeloId != 0)
                    {
                        return _serviceApp.DoObterPor(p => p.ProdutoId.Equals(filter.ProdutoId) && p.ModeloId.Equals(filter.ModeloId));
                    }
                    else if (filter.Nome != "")
                    {
                        return _serviceApp.DoObterPor(p => p.ProdutoId.Equals(filter.ProdutoId) && p.Modelo.Nome.Contains(filter.Nome));
                    }
                    else
                    {
                        return _serviceApp.DoObterPor(p => p.ProdutoId.Equals(filter.ProdutoId));
                    }
                }
            }
            return _serviceApp.DoObterPor(p => p.ProdutoId.Equals(-1));
        }

        [HttpPost]
        [Route("DoSalvar")]
        public dynamic DoSalvar([FromBody] ProdutoModelo produtomodelo)
        {
            try
            {
                if (produtomodelo.Id == 0)
                {
                    _validationResult = _serviceApp.DoAdicionar(produtomodelo);
                }
                else
                {
                    _validationResult = _serviceApp.DoAtualizar(produtomodelo);
                }
            }
            catch (System.Exception)
            {
                _validationResult.Add("Objeto contem campos nulo.");
            }
            return new { validationResult = _validationResult, Id = produtomodelo.Id };
        }

        [HttpPost]
        [Route("DoApagar")]
        public dynamic DoApagar([FromBody] ProdutoModelo produtomodelo)
        {
            try
            {
                _validationResult = _serviceApp.DoDeletar(produtomodelo);
            }
            catch (System.Exception)
            {
                _validationResult.Add("Objeto contem campos nulo.");
            }
            return new { validationResult = _validationResult, Id = produtomodelo.Id };
        }
    }
}