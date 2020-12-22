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
    public class ProdutoAppService : AppService, IProdutoAppService
    {
        private readonly IProdutoService _service;
        private readonly IProdutoModeloService _serviceProdutoModelo;

        private readonly IUnitOfWork _uow;

        public ProdutoAppService(IProdutoService service, 
                                IProdutoModeloService serviceProdutoModelo, 
                                IUnitOfWork uow)
        {
            _service = service;
            _serviceProdutoModelo = serviceProdutoModelo;
            _uow = uow;
        }

        public ValidationResult DoAdicionar(Produto produto)
        {
            ValidationResult.Add(_service.DoIsValid(produto));
            if (!ValidationResult.IsValid)
            {
                return ValidationResult;
            }
            _uow.DoBeginTransaction();
            ValidationResult.Add(_service.DoAdicionar(produto));
            if (ValidationResult.IsValid) _uow.DoCommit();
            return ValidationResult;
        }

        public ValidationResult DoAtualizar(Produto produto)
        {
            ValidationResult.Add(_service.DoIsValid(produto));
            if (!ValidationResult.IsValid)
            {
                return ValidationResult;
            }
            _uow.DoBeginTransaction();
            ValidationResult.Add(_service.DoAtualizar(produto));
            if (ValidationResult.IsValid) _uow.DoCommit();
            return ValidationResult;
        }

        public ValidationResult DoDeletar(Produto produto)
        {
            if (produto.Id != 0){
                ValidationResult.Add(_service.DoExisteDependencia(produto));
                if (ValidationResult.IsValid){
                    IEnumerable<ProdutoModelo> lstProdutoModelo = _serviceProdutoModelo.DoObterPor(i => i.ProdutoId == produto.Id);
                    _uow.DoBeginTransaction();
                    ValidationResult.Add(_serviceProdutoModelo.DoDeletarRange(lstProdutoModelo));
                    ValidationResult.Add(_service.DoDeletar(produto));
                    if (ValidationResult.IsValid) _uow.DoCommit();
                }
            }
            return ValidationResult;
        }

        public Produto DoObterPorId(int id)
        {
            return _service.DoObterPorId(id);
        }

        public IEnumerable<Produto> DoObterTodos()
        {
            return _service.DoObterTodos();
        }

        public IEnumerable<Produto> DoObterPor(Expression<Func<Produto, bool>> where = null)
        {
            return _service.DoObterPor(where);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}