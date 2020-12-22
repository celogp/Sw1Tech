using Microsoft.AspNetCore.Mvc;
using Sw1Tech.App.Interfaces;
using Sw1Tech.Domain.Entities.Filter;

namespace Sw1Tech.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/PesquisaTabela")]
    public class PesquisaTabelaController : Controller
    {
        [HttpGet]
        [Route("DoPesquisarSexo")]
        public dynamic DoPesquisarSexo([FromServices] ISexoAppService serviceSexo)
        {
            var _serviceSexo = serviceSexo;
            return _serviceSexo.DoObterTodos();
        }

        [HttpGet]
        [Route("DoPesquisarUf")]
        public dynamic DoPesquisarUf([FromServices] IUfAppService serviceUf)
        {
            var _serviceUf = serviceUf;
            return _serviceUf.DoObterTodos();
        }

        [HttpGet]
        [Route("DoPesquisarFormaPagamento")]
        public dynamic DoPesquisarFormaPagamento([FromServices] IFormaPagamentoAppService serviceFormaPagamento)
        {
            var _serviceFormaPagamento = serviceFormaPagamento;
            return _serviceFormaPagamento.DoObterTodos();
        }

        [HttpPost]
        [Route("DoPesquisarOcorrencia")]
        public dynamic DoPesquisarOcorrencia([FromServices] IOcorrenciaAppService serviceOcorrencia,
                                             [FromBody] OcorrenciaFilter filter = null)
        {
            var _serviceOcorrencia = serviceOcorrencia;
            if (filter != null)
            {
                if (filter.Id != 0)
                {
                    return _serviceOcorrencia.DoObterPor(p => p.Id.Equals(filter.Id));
                }
                else if (filter.Nome != "" && filter.Nome != null)
                {
                    return _serviceOcorrencia.DoObterPor(p => p.Nome.Contains(filter.Nome));
                }
            }
            return _serviceOcorrencia.DoObterTodos();
        }
    }
}