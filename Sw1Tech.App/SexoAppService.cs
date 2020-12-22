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
    public class SexoAppService : AppService, ISexoAppService
    {
        private readonly ISexoService _service;
        private readonly IUnitOfWork _uow;

        public SexoAppService(ISexoService service, IUnitOfWork uow)
        {
            _service = service;
            _uow = uow;
        }

        public ValidationResult DoAdicionar(Sexo sexo)
        {
            ValidationResult.Add(_service.DoIsValid(sexo));
            if (!ValidationResult.IsValid)
            {
                return ValidationResult;
            }
            _uow.DoBeginTransaction();
            ValidationResult.Add(_service.DoAdicionar(sexo));
            if (ValidationResult.IsValid) _uow.DoCommit();
            return ValidationResult;
        }

        public ValidationResult DoAtualizar(Sexo sexo)
        {
            ValidationResult.Add(_service.DoIsValid(sexo));
            if (!ValidationResult.IsValid)
            {
                return ValidationResult;
            }
            _uow.DoBeginTransaction();
            ValidationResult.Add(_service.DoAtualizar(sexo));
            if (ValidationResult.IsValid) _uow.DoCommit();
            return ValidationResult;
        }

        public ValidationResult DoDeletar(Sexo sexo)
        {
            if (sexo.Id != 0){
                _uow.DoBeginTransaction();
                ValidationResult.Add(_service.DoDeletar(sexo));
                if (ValidationResult.IsValid) _uow.DoCommit();
            }
            return ValidationResult;
        }

        public Sexo DoObterPorId(int id)
        {
            return _service.DoObterPorId(id);
        }

        public IEnumerable<Sexo> DoObterTodos()
        {
            return _service.DoObterTodos();
        }

        public IEnumerable<Sexo> DoObterPor(Expression<Func<Sexo, bool>> where = null)
        {
            return _service.DoObterPor(where);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}