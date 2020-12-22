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
    public class UfAppService : AppService, IUfAppService
    {
        private readonly IUfService _service;
        private readonly IUnitOfWork _uow;

        public UfAppService(IUfService service, IUnitOfWork uow)
        {
            _service = service;
            _uow = uow;
        }

        public ValidationResult DoAdicionar(Uf uf)
        {
            ValidationResult.Add(_service.DoIsValid(uf));
            if (!ValidationResult.IsValid)
            {
                return ValidationResult;
            }
            _uow.DoBeginTransaction();
            ValidationResult.Add(_service.DoAdicionar(uf));
            if (ValidationResult.IsValid) _uow.DoCommit();
            return ValidationResult;
        }

        public ValidationResult DoAtualizar(Uf uf)
        {
            ValidationResult.Add(_service.DoIsValid(uf));
            if (!ValidationResult.IsValid)
            {
                return ValidationResult;
            }
            _uow.DoBeginTransaction();
            ValidationResult.Add(_service.DoAtualizar(uf));
            if (ValidationResult.IsValid) _uow.DoCommit();
            return ValidationResult;
        }

        public ValidationResult DoDeletar(Uf uf)
        {
            if (uf.Id != 0){
                _uow.DoBeginTransaction();
                ValidationResult.Add(_service.DoDeletar(uf));
                if (ValidationResult.IsValid) _uow.DoCommit();
            }
            return ValidationResult;
        }

        public Uf DoObterPorId(int id)
        {
            return _service.DoObterPorId(id);
        }

        public IEnumerable<Uf> DoObterTodos()
        {
            return _service.DoObterTodos();
        }

        public IEnumerable<Uf> DoObterPor(Expression<Func<Uf, bool>> where = null)
        {
            return _service.DoObterPor(where);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}