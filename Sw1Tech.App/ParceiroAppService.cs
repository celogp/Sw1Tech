using System;
using System.Collections.Generic;
using Sw1Tech.App.Interfaces;
using Sw1Tech.Domain.Interface.Service;
using Sw1Tech.Domain.Validation;
using Sw1Tech.Domain.Entities;
//using Sw1Tech.Infra.Context.Interfaces.Dapper;
using Sw1Tech.Infra.Context.Interfaces.EF;
using System.Linq.Expressions;
using System.Linq;

namespace Sw1Tech.App
{
    public class ParceiroAppService : AppService, IParceiroAppService
    {
        private readonly IParceiroService _service;
        private readonly IUnitOfWork _uow;

        public ParceiroAppService(IParceiroService service, IUnitOfWork uow)
        {
            _service = service;
            _uow = uow;
        }

        public ValidationResult DoAdicionar(Parceiro parceiro)
        {
            ValidationResult.Add(_service.DoIsValid(parceiro));
            if (!ValidationResult.IsValid)
            {
                return ValidationResult;
            }
            _uow.DoBeginTransaction();
            ValidationResult.Add(_service.DoAdicionar(parceiro));
            if (ValidationResult.IsValid) _uow.DoCommit();
            return ValidationResult;
        }

        public ValidationResult DoAtualizar(Parceiro parceiro)
        {
            ValidationResult.Add(_service.DoIsValid(parceiro));
            if (!ValidationResult.IsValid)
            {
                return ValidationResult;
            }
            _uow.DoBeginTransaction();
            ValidationResult.Add(_service.DoAtualizar(parceiro));
            if (ValidationResult.IsValid) _uow.DoCommit();
            return ValidationResult;
        }

        public ValidationResult DoDeletar(Parceiro parceiro)
        {
            if (parceiro.Id != 0){
                ValidationResult.Add(_service.DoExisteDependencia(parceiro));
                if (ValidationResult.IsValid){
                    _uow.DoBeginTransaction();
                    ValidationResult.Add(_service.DoDeletar(parceiro));
                    if (ValidationResult.IsValid) _uow.DoCommit();
                }
            }
            return ValidationResult;
        }

        public Parceiro DoObterPorId(int id)
        {
            return _service.DoObterPorId(id);
        }

        public IEnumerable<Parceiro> DoObterTodos()
        {
            return _service.DoObterTodos();
        }

        public IEnumerable<Parceiro> DoObterPor(Expression<Func<Parceiro, bool>> where = null)
        {
            return _service.DoObterPor(where);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public ValidationResult DoSalvarLstParceiros(IEnumerable<Parceiro> lstParceiros = null)
        {
            IEnumerable<Parceiro> lstParceirosInclusao = lstParceiros.Where(p => p.Id == 0);
            IEnumerable<Parceiro> lstParceirosAtualizacao = lstParceiros.Where(p => p.Id != 0);
            var qtdRegInclusao = lstParceirosInclusao.Count();
            var qtdRegAtualizacao = lstParceirosAtualizacao.Count();
            _uow.DoBeginTransaction();
            if (qtdRegInclusao != 0)
            {
                ValidationResult.Add(_service.DoAdicionarRange(lstParceirosInclusao));
            }
            if (qtdRegAtualizacao != 0)
            {
                ValidationResult.Add(_service.DoAtualizarRange(lstParceirosAtualizacao));
            }
            if (ValidationResult.IsValid) _uow.DoCommit();

            return ValidationResult;
        }
    }
}