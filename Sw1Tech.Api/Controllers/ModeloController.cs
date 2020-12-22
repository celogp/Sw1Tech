using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sw1Tech.App.Interfaces;
using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Entities.Filter;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Modelo")]
    [Authorize("Bearer")]
    public class ModeloController : Controller
    {
        private readonly IModeloAppService _serviceApp;
        private readonly IModeloKitAppService _servicekitApp;
        private ValidationResult _validationResult;

        public ModeloController(IModeloAppService serviceApp, IModeloKitAppService servicekitApp)
        {
            _serviceApp = serviceApp;
            _servicekitApp = servicekitApp;
        }

        [HttpPost]
        [Route("DoPesquisar")]
        public dynamic DoPesquisar([FromBody] ModeloFilter filter = null)
        {
            if (filter != null)
            {
                if (filter.Id != 0)
                {
                    return _serviceApp.DoObterPor(p => p.Id.Equals(filter.Id));
                }
                else if(filter.Nome != "" && filter.Nome != null)
                {
                    return _serviceApp.DoObterPor(p => p.Nome.Contains(filter.Nome));
                }
            }
            return _serviceApp.DoObterTodos();
        }

        [HttpPost]
        [Route("DoPesquisarKit")]
        public dynamic DoPesquisarKit([FromBody] ModeloKitFilter filter = null)
        {
            if (filter != null)
            {
                if (filter.Id != 0){
                    return _servicekitApp.DoObterPor(p => p.Id.Equals(filter.Id));
                }else if ((filter.ModeloId != 0) && (filter.ProdutoId == 0)){
                    return _servicekitApp.DoObterPor(p => p.ModeloId.Equals(filter.ModeloId));
                }else if ((filter.ModeloId != 0) && (filter.ProdutoId != 0)){
                    return _servicekitApp.DoObterPor(p => p.ModeloId.Equals(filter.ModeloId) && p.ProdutoId.Equals(filter.ProdutoId));
                }
            }
            return _servicekitApp.DoObterTodos();
        }

        [HttpPost]
        [Route("DoSalvar")]
        public dynamic DoSalvar([FromBody] Modelo modelo)
        {
            try
            {
                if (modelo.Id == 0)
                {
                    _validationResult = _serviceApp.DoAdicionar(modelo);
                }
                else
                {
                    _validationResult = _serviceApp.DoAtualizar(modelo);
                }
            }
            catch (System.Exception)
            {
                _validationResult.Add("Objeto contem campos nulo.");
            }
            return new { validationResult = _validationResult, Id = modelo.Id };
        }

        [HttpPost]
        [Route("DoSalvarKit")]
        public dynamic DoSalvarKit([FromBody] ModeloKit modelokit)
        {
            try
            {
                if (modelokit.Id == 0)
                {
                    _validationResult = _servicekitApp.DoAdicionar(modelokit);
                }
                else
                {
                    _validationResult = _servicekitApp.DoAtualizar(modelokit);
                }
            }
            catch (System.Exception)
            {
                _validationResult.Add("Objeto contem campos nulo.");
            }
            return new { validationResult = _validationResult, Id = modelokit.Id };
        }

        [HttpPost]
        [Route("DoApagar")]
        public dynamic DoApagar([FromBody] Modelo modelo)
        {
            try
            {
                _validationResult = _serviceApp.DoDeletar(modelo);
            }
            catch (System.Exception)
            {
                _validationResult.Add("Objeto contem campos nulo.");
            }
            return new { validationResult = _validationResult, Id = modelo.Id };
        }

        [HttpPost]
        [Route("DoApagarKit")]
        public dynamic DoApagarKit([FromBody] ModeloKit modelokit)
        {
            try
            {
                _validationResult = _servicekitApp.DoDeletar(modelokit);
            }
            catch (System.Exception)
            {
                _validationResult.Add("Objeto contem campos nulo.");
            }
            return new { validationResult = _validationResult, Id = modelokit.Id };
        }
    }
}