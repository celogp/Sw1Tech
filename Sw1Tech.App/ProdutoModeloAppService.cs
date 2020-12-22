using System;
using System.Collections.Generic;
using Sw1Tech.App.Interfaces;
using Sw1Tech.Domain.Interface.Service;
using Sw1Tech.Domain.Validation;
using Sw1Tech.Domain.Entities;
//using Sw1Tech.Infra.Context.Interfaces.Dapper;
using Sw1Tech.Infra.Context.Interfaces.EF;
using System.Linq.Expressions;
using System.Collections;

namespace Sw1Tech.App
{
    public class ProdutoModeloAppService : AppService, IProdutoModeloAppService
    {
        private readonly IProdutoModeloService _service;
        private readonly IUnitOfWork _uow;

        public ProdutoModeloAppService(IProdutoModeloService service, IUnitOfWork uow)
        {
            _service = service;
            _uow = uow;
        }

        public ValidationResult DoAdicionar(ProdutoModelo produtoModelo)
        {
            ValidationResult.Add(_service.DoIsValid(produtoModelo));
            if (!ValidationResult.IsValid)
            {
                return ValidationResult;
            }
            _uow.DoBeginTransaction();
            ValidationResult.Add(_service.DoAdicionar(produtoModelo));
            if (ValidationResult.IsValid) _uow.DoCommit();
            return ValidationResult;
        }

        public ValidationResult DoAtualizar(ProdutoModelo produtoModelo)
        {
            ValidationResult.Add(_service.DoIsValid(produtoModelo));
            if (!ValidationResult.IsValid)
            {
                return ValidationResult;
            }
            _uow.DoBeginTransaction();
            ValidationResult.Add(_service.DoAtualizar(produtoModelo));
            if (ValidationResult.IsValid) _uow.DoCommit();
            return ValidationResult;
        }

        public ValidationResult DoDeletar(ProdutoModelo produtoModelo)
        {
            if (produtoModelo.Id != 0){
                _uow.DoBeginTransaction();
                ValidationResult.Add(_service.DoDeletar(produtoModelo));
                if (ValidationResult.IsValid) _uow.DoCommit();
            }
            return ValidationResult;
        }

        public ProdutoModelo DoObterPorId(int id)
        {
            return _service.DoObterPorId(id);
        }

        public IEnumerable<ProdutoModelo> DoObterTodos()
        {
            return _service.DoObterTodos();
        }

        public IEnumerable<ProdutoModelo> DoObterPor(Expression<Func<ProdutoModelo, bool>> where = null)
        {
            return _service.DoObterPor(where);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}