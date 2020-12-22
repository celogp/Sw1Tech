using System;
using System.Collections.Generic;
using Sw1Tech.App.Interfaces;
using Sw1Tech.Domain.Interface.Service;
using Sw1Tech.Domain.Validation;
using Sw1Tech.Domain.Entities;
//using Sw1Tech.Infra.Context.Interfaces.Dapper;
using Sw1Tech.Infra.Context.Interfaces.EF;
using System.Linq.Expressions;

namespace Sw1Tech.App
{
    public class ModeloAppService : AppService, IModeloAppService
    {
        private readonly IModeloService _service;
        private readonly IModeloKitService _serviceModeloKit;
        private readonly IUnitOfWork _uow;

        public ModeloAppService(IModeloService service,
                                IModeloKitService serviceModeloKit,
                                IUnitOfWork uow)
        {
            _service = service;
            _serviceModeloKit = serviceModeloKit;
            _uow = uow;
        }

        public ValidationResult DoAdicionar(Modelo modelo)
        {
            ValidationResult.Add(_service.DoIsValid(modelo));
            if (!ValidationResult.IsValid)
            {
                return ValidationResult;
            }
            _uow.DoBeginTransaction();
            ValidationResult.Add(_service.DoAdicionar(modelo));
            if (ValidationResult.IsValid) _uow.DoCommit();
            return ValidationResult;
        }

        public ValidationResult DoAtualizar(Modelo modelo)
        {
            ValidationResult.Add(_service.DoIsValid(modelo));
            if (!ValidationResult.IsValid)
            {
                return ValidationResult;
            }
            _uow.DoBeginTransaction();
            ValidationResult.Add(_service.DoAtualizar(modelo));
            if (ValidationResult.IsValid) _uow.DoCommit();
            return ValidationResult;
        }

        public ValidationResult DoDeletar(Modelo modelo)
        {
            if (modelo.Id != 0){
                ValidationResult.Add(_service.DoExisteDependencia(modelo));
                if (ValidationResult.IsValid)
                {
                    IEnumerable<ModeloKit> lstModeloKit = _serviceModeloKit.DoObterPor(i => i.ModeloId == modelo.Id);
                    _uow.DoBeginTransaction();
                    ValidationResult.Add(_serviceModeloKit.DoDeletarRange(lstModeloKit));
                    ValidationResult.Add(_service.DoDeletar(modelo));
                    if (ValidationResult.IsValid) _uow.DoCommit();
                }
            }
            return ValidationResult;
        }

        public Modelo DoObterPorId(int id)
        {
            return _service.DoObterPorId(id);
        }

        public IEnumerable<Modelo> DoObterTodos()
        {
            return _service.DoObterTodos();
        }

        public IEnumerable<Modelo> DoObterPor(Expression<Func<Modelo, bool>> where = null)
        {
            return _service.DoObterPor(where);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}