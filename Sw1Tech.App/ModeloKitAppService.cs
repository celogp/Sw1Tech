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
    public class ModeloKitAppService : AppService, IModeloKitAppService
    {
        private readonly IModeloKitService _service;
        private readonly IUnitOfWork _uow;

        public ModeloKitAppService(IModeloKitService service, IUnitOfWork uow)
        {
            _service = service;
            _uow = uow;
        }

        public ValidationResult DoAdicionar(ModeloKit modeloKit)
        {
            ValidationResult.Add(_service.DoIsValid(modeloKit));
            if (!ValidationResult.IsValid)
            {
                return ValidationResult;
            }
            _uow.DoBeginTransaction();
            ValidationResult.Add(_service.DoAdicionar(modeloKit));
            if (ValidationResult.IsValid) _uow.DoCommit();
            return ValidationResult;
        }

        public ValidationResult DoAtualizar(ModeloKit modeloKit)
        {
            ValidationResult.Add(_service.DoIsValid(modeloKit));
            if (!ValidationResult.IsValid)
            {
                return ValidationResult;
            }
            _uow.DoBeginTransaction();
            ValidationResult.Add(_service.DoAtualizar(modeloKit));
            if (ValidationResult.IsValid) _uow.DoCommit();
            return ValidationResult;
        }

        public ValidationResult DoDeletar(ModeloKit modeloKit)
        {
            if (modeloKit.Id != 0){
                _uow.DoBeginTransaction();
                ValidationResult.Add(_service.DoDeletar(modeloKit));
                if (ValidationResult.IsValid) _uow.DoCommit();
            }
            return ValidationResult;
        }

        public ModeloKit DoObterPorId(int id)
        {
            return _service.DoObterPorId(id);
        }

        public IEnumerable<ModeloKit> DoObterTodos()
        {
            return _service.DoObterTodos();
        }

        public IEnumerable<ModeloKit> DoObterPor(Expression<Func<ModeloKit, bool>> where = null)
        {
            return _service.DoObterPor(where);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}