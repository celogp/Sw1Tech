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
    public class FinanceiroAppService : AppService, IFinanceiroAppService
    {
        private readonly IFinanceiroService _service;
        private readonly IOrcamentoService _serviceOrcamento;
        private readonly IUnitOfWork _uow;

        public FinanceiroAppService(IFinanceiroService service, IUnitOfWork uow, IOrcamentoService serviceOrcamento)
        {
            _service = service;
            _serviceOrcamento = serviceOrcamento;
            _uow = uow;
        }

        private ValidationResult DoOrcamentoIsValid(Financeiro financeiro){
            var orcamentoVal = _serviceOrcamento.DoObterPor(i => i.Id == financeiro.OrcamentoId).SingleOrDefault();
            return _serviceOrcamento.DoIsValid(orcamentoVal) ;
        }

        public ValidationResult DoAdicionar(Financeiro financeiro)
        {
            ValidationResult.Add(_service.DoIsValid(financeiro));
            if (!ValidationResult.IsValid)
            {
                return ValidationResult;
            }
            _uow.DoBeginTransaction();
            ValidationResult.Add(_service.DoAdicionar(financeiro));
            if (ValidationResult.IsValid) _uow.DoCommit();
            return ValidationResult;
        }

        public ValidationResult DoAtualizar(Financeiro financeiro)
        {
            ValidationResult.Add(_service.DoIsValid(financeiro));
            if (!ValidationResult.IsValid)
            {
                return ValidationResult;
            }
            _uow.DoBeginTransaction();
            ValidationResult.Add(_service.DoAtualizar(financeiro));
            if (ValidationResult.IsValid) _uow.DoCommit();
            return ValidationResult;
        }

        public ValidationResult DoDeletar(Financeiro financeiro)
        {
            if (financeiro.Id != 0){
                _uow.DoBeginTransaction();
                ValidationResult.Add(_service.DoDeletar(financeiro));
                if (ValidationResult.IsValid) _uow.DoCommit();
            }
            return ValidationResult;
        }

        public Financeiro DoObterPorId(int id)
        {
            return _service.DoObterPorId(id);
        }

        public IEnumerable<Financeiro> DoObterTodos()
        {
            return _service.DoObterTodos();
        }

        public IEnumerable<Financeiro> DoObterPor(Expression<Func<Financeiro, bool>> where = null)
        {
            return _service.DoObterPor(where);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public ValidationResult DoAdicionarLstFinanceiro(IEnumerable<Financeiro> lstFinanceiro)
        {
            if (lstFinanceiro.Count() != 0){
                var financeiroVal = lstFinanceiro.LastOrDefault();
                ValidationResult.Add(_service.DoIsValid(financeiroVal));
                if (!ValidationResult.IsValid){
                    return ValidationResult;
                }
                ValidationResult.Add(DoOrcamentoIsValid(financeiroVal));
                if (!ValidationResult.IsValid){
                    return ValidationResult;
                }
                _uow.DoBeginTransaction();
                ValidationResult.Add(_service.DoAdicionarRange(lstFinanceiro));
                if (ValidationResult.IsValid) _uow.DoCommit();
            }else{
                ValidationResult.Add("Lista de financeiro vazia, não foi possível adicionar.");
            }
            return ValidationResult;
        }

        public ValidationResult DoSalvarLstFinanceiro(IEnumerable<Financeiro> lstFinanceiro)
        {
            if (lstFinanceiro.Count() != 0){
                var financeiroVal = lstFinanceiro.LastOrDefault();
                if (financeiroVal.Id == 0){
                    return DoAdicionarLstFinanceiro(lstFinanceiro);
                }else{
                    ValidationResult.Add(DoOrcamentoIsValid(financeiroVal));
                    if (!ValidationResult.IsValid){
                        return ValidationResult;
                    }
                    ValidationResult.Add(_service.DoIsValid(financeiroVal ));
                    if (!ValidationResult.IsValid){
                        return ValidationResult;
                    }
                    _uow.DoBeginTransaction();
                    ValidationResult.Add(_service.DoAtualizarRange(lstFinanceiro));
                    if (ValidationResult.IsValid) _uow.DoCommit();
                }
            }else{
                ValidationResult.Add("Lista de financeiro vazia, não foi possível atualizar.");
            }
            return ValidationResult;
        }

        public ValidationResult DoApagarLstFinanceiro(IEnumerable<Financeiro> lstFinanceiro)
        {
            if (lstFinanceiro.Count() != 0){
                var financeiroVal = lstFinanceiro.LastOrDefault();
                if (financeiroVal.Id != 0){
                    ValidationResult.Add(_service.DoIsValid( financeiroVal ));
                    if (!ValidationResult.IsValid){
                        return ValidationResult;
                    }
                    ValidationResult.Add(DoOrcamentoIsValid(financeiroVal));
                    if (!ValidationResult.IsValid){
                        return ValidationResult;
                    }
                    _uow.DoBeginTransaction();
                    ValidationResult.Add(_service.DoDeletarRange(lstFinanceiro));
                    if (ValidationResult.IsValid) _uow.DoCommit();
                }
            }else{
                ValidationResult.Add("Lista de financeiro vazia, não foi possível apagar.");
            }
            return ValidationResult;
        }
        
    }
}