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
    public class RegistroExportacaoAppService : AppService, IRegistroExportacaoAppService
    {
        private readonly IRegistroExportacaoService _service;
        private readonly IUnitOfWork _uow;

        public RegistroExportacaoAppService(IRegistroExportacaoService service, IUnitOfWork uow)
        {
            _service = service;
            _uow = uow;
        }

        public ValidationResult DoAdicionar(RegistroExportacao registroExportacao)
        {
            ValidationResult.Add(_service.DoIsValid(registroExportacao));
            if (!ValidationResult.IsValid)
            {
                return ValidationResult;
            }
            _uow.DoBeginTransaction();
            ValidationResult.Add(_service.DoAdicionar(registroExportacao));
            if (ValidationResult.IsValid) _uow.DoCommit();
            return ValidationResult;
        }

        public ValidationResult DoAtualizar(RegistroExportacao registroExportacao)
        {
            ValidationResult.Add(_service.DoIsValid(registroExportacao));
            if (!ValidationResult.IsValid)
            {
                return ValidationResult;
            }
            _uow.DoBeginTransaction();
            ValidationResult.Add(_service.DoAtualizar(registroExportacao));
            if (ValidationResult.IsValid) _uow.DoCommit();
            return ValidationResult;
        }

        public ValidationResult DoDeletar(RegistroExportacao registroExportacao)
        {
            if (registroExportacao.Id != 0){
                _uow.DoBeginTransaction();
                ValidationResult.Add(_service.DoDeletar(registroExportacao));
                if (ValidationResult.IsValid) _uow.DoCommit();
            }
            return ValidationResult;
        }

        public RegistroExportacao DoObterPorId(int id)
        {
            return _service.DoObterPorId(id);
        }

        public IEnumerable<RegistroExportacao> DoObterTodos()
        {
            return _service.DoObterTodos();
        }

        public IEnumerable<RegistroExportacao> DoObterPor(Expression<Func<RegistroExportacao, bool>> where = null)
        {
            return _service.DoObterPor(where);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}