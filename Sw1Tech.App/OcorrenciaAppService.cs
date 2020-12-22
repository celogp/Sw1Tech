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
    public class OcorrenciaAppService : AppService, IOcorrenciaAppService
    {
        private readonly IOcorrenciaService _service;
        private readonly IUnitOfWork _uow;

        public OcorrenciaAppService(IOcorrenciaService service, IUnitOfWork uow)
        {
            _service = service;
            _uow = uow;
        }

        public ValidationResult DoAdicionar(Ocorrencia ocorrencia)
        {
            ValidationResult.Add(_service.DoIsValid(ocorrencia));
            if (!ValidationResult.IsValid)
            {
                return ValidationResult;
            }
            _uow.DoBeginTransaction();
            ValidationResult.Add(_service.DoAdicionar(ocorrencia));
            if (ValidationResult.IsValid) _uow.DoCommit();
            return ValidationResult;
        }

        public ValidationResult DoAtualizar(Ocorrencia ocorrencia)
        {
            ValidationResult.Add(_service.DoIsValid(ocorrencia));
            if (!ValidationResult.IsValid)
            {
                return ValidationResult;
            }
            _uow.DoBeginTransaction();
            ValidationResult.Add(_service.DoAtualizar(ocorrencia));
            if (ValidationResult.IsValid) _uow.DoCommit();
            return ValidationResult;
        }

        public ValidationResult DoDeletar(Ocorrencia ocorrencia)
        {
            if (ocorrencia.Id != 0){
                _uow.DoBeginTransaction();
                ValidationResult.Add(_service.DoDeletar(ocorrencia));
                if (ValidationResult.IsValid) _uow.DoCommit();
            }
            return ValidationResult;
        }

        public Ocorrencia DoObterPorId(int id)
        {
            return _service.DoObterPorId(id);
        }

        public IEnumerable<Ocorrencia> DoObterTodos()
        {
            return _service.DoObterTodos();
        }

        public IEnumerable<Ocorrencia> DoObterPor(Expression<Func<Ocorrencia, bool>> where = null)
        {
            return _service.DoObterPor(where);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}