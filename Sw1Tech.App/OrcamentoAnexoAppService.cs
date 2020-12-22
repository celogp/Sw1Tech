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
    public class OrcamentoAnexoAppService : AppService, IOrcamentoAnexoAppService
    {
        private readonly IOrcamentoAnexoService _service;
        private readonly IUnitOfWork _uow;

        public OrcamentoAnexoAppService(IOrcamentoAnexoService service, IUnitOfWork uow)
        {
            _service = service;
            _uow = uow;
        }

        public ValidationResult DoAdicionar(OrcamentoAnexo orcamentoAnexo)
        {
            ValidationResult.Add(_service.DoIsValid(orcamentoAnexo));
            if (!ValidationResult.IsValid){
                return ValidationResult;
            }
            _uow.DoBeginTransaction();
            ValidationResult.Add(_service.DoAdicionar(orcamentoAnexo));
            if (ValidationResult.IsValid) _uow.DoCommit();
            return ValidationResult;
        }

        public ValidationResult DoAtualizar(OrcamentoAnexo orcamentoAnexo)
        {
            ValidationResult.Add(_service.DoIsValid(orcamentoAnexo));
            if (!ValidationResult.IsValid){
                return ValidationResult;
            }
            _uow.DoBeginTransaction();
            ValidationResult.Add(_service.DoAtualizar(orcamentoAnexo));
            if (ValidationResult.IsValid) _uow.DoCommit();
            return ValidationResult;
        }

        public ValidationResult DoDeletar(OrcamentoAnexo orcamentoAnexo)
        {
            if (orcamentoAnexo.Id != 0){
                _uow.DoBeginTransaction();
                ValidationResult.Add(_service.DoDeletar(orcamentoAnexo));
                if (ValidationResult.IsValid) _uow.DoCommit();
            }
            return ValidationResult;
        }

        public OrcamentoAnexo DoObterPorId(int id)
        {
            return _service.DoObterPorId(id);
        }

        public IEnumerable<OrcamentoAnexo> DoObterTodos()
        {
            return _service.DoObterTodos();
        }

        public IEnumerable<OrcamentoAnexo> DoObterPor(Expression<Func<OrcamentoAnexo, bool>> where = null)
        {
            return _service.DoObterPor(where);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}