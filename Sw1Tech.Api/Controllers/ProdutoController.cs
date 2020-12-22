using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sw1Tech.App.Interfaces;
using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Entities.Filter;
using Sw1Tech.Domain.Enums;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Produto")]
    [Authorize("Bearer")]
    public class ProdutoController : Controller
    {
        private readonly IProdutoAppService _serviceApp;
        private ValidationResult _validationResult;

        public ProdutoController(IProdutoAppService serviceApp)
        {
            _serviceApp = serviceApp;
        }

        [HttpPost]
        [Route("DoPesquisar")]
        public dynamic DoPesquisar([FromBody] ProdutoFilter filter = null)
        {
            if (filter != null)
            {
                if (filter.Id != 0)
                {
                    return _serviceApp.DoObterPor(p => p.Id.Equals(filter.Id));
                }
                else if (filter.Classificacao == (int) EClassificacaoProduto.FINAL)
                {
                    return _serviceApp.DoObterPor(p => p.Nome.Contains(filter.Nome) && p.Classificacao.Equals(filter.Classificacao));
                }else if (filter.Classificacao != (int) EClassificacaoProduto.FINAL){
                    return _serviceApp.DoObterPor(p => p.Nome.Contains(filter.Nome) && p.Classificacao != (int) EClassificacaoProduto.FINAL);
                }
            }
            return _serviceApp.DoObterTodos();
        }

        [HttpPost]
        [Route("DoSalvar")]
        public dynamic DoSalvar([FromBody] Produto produto)
        {
            try
            {
                if (produto.Id == 0)
                {
                    _validationResult = _serviceApp.DoAdicionar(produto);
                }
                else
                {
                    _validationResult = _serviceApp.DoAtualizar(produto);
                }
            }
            catch (System.Exception)
            {
                _validationResult.Add("Objeto contem campos nulo.");
            }
            return new { validationResult = _validationResult, Id = produto.Id };
        }

        [HttpPost]
        [Route("DoApagar")]
        public dynamic DoApagar([FromBody] Produto produto)
        {
            try
            {
                _validationResult = _serviceApp.DoDeletar(produto);
            }
            catch (System.Exception)
            {
                _validationResult.Add("Objeto contem campos nulo.");
            }
            return new { validationResult = _validationResult, Id = produto.Id };
        }
    }
}