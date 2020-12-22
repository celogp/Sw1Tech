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
using Sw1Tech.Domain.Enums;

namespace Sw1Tech.App
{
    public class OrcamentoAppService : AppService, IOrcamentoAppService
    {
        private readonly IOrcamentoService _service;
        private readonly IOrcamentoItemService _serviceOrcamentoItem;
        private readonly IFinanceiroService _serviceFinanceiro;
        private readonly IOrcamentoOcorrenciaService _serviceOrcamentoOcorrencia;
        private readonly IUnitOfWork _uow;

        public OrcamentoAppService(IOrcamentoService service, IUnitOfWork uow, 
                                    IOrcamentoItemService serviceOrcamentoItem,
                                    IFinanceiroService serviceFinanceiro,
                                    IOrcamentoOcorrenciaService serviceOrcamentoOcorrencia)
        {
            _service = service;
            _serviceOrcamentoItem = serviceOrcamentoItem;
            _serviceFinanceiro = serviceFinanceiro;
            _serviceOrcamentoOcorrencia = serviceOrcamentoOcorrencia;
            _uow = uow;
        }

        private ValidationResult DoAddOcorrenciaPrimeiraMedicao(Orcamento orcamento)
        {
            var orcamentoOcorrencia = new OrcamentoOcorrencia
            {
                OrcamentoId = orcamento.Id,
                DtOcorrencia = DateTime.Now,
                OcorrenciaId = (int)EOcorrenciaOrcamento.PRIMEIRA_MEDICAO,
                UsuarioId = orcamento.UsuarioId,
                Historico = "Primeira medição pelo sistema",
                Ocorrencia = null
            };

            return _serviceOrcamentoOcorrencia.DoAdicionar(orcamentoOcorrencia);
        }

        private ValidationResult DoAddOcorrenciaBloqueio(Orcamento orcamento)
        {
            var orcamentoOcorrencia = new OrcamentoOcorrencia
            {
                OrcamentoId = orcamento.Id,
                DtOcorrencia = DateTime.Now,
                OcorrenciaId = (int) EOcorrenciaOrcamento.BLOQUEAR_ORCAMENTO,
                UsuarioId = orcamento.UsuarioId,
                Historico = "Orçado e bloqueado pelo sistema",
                Ocorrencia = null
            };

            return _serviceOrcamentoOcorrencia.DoAdicionar(orcamentoOcorrencia);
        }

        public ValidationResult DoAdicionar(Orcamento orcamento)
        {
            ValidationResult.Add(_service.DoIsValid(orcamento));
            if (!ValidationResult.IsValid)
            {
                return ValidationResult;
            }
            _uow.DoBeginTransaction();
            ValidationResult.Add(_service.DoAdicionar(orcamento));
            _uow.DoSavePoint();
            ValidationResult.Add(DoAddOcorrenciaPrimeiraMedicao(orcamento));
            if (ValidationResult.IsValid) _uow.DoCommit();
            return ValidationResult;
        }

        public ValidationResult DoAtualizar(Orcamento orcamento)
        {
            ValidationResult.Add(_service.DoIsValid(orcamento));
            if (!ValidationResult.IsValid)
            {
                return ValidationResult;
            }
            _uow.DoBeginTransaction();
            ValidationResult.Add(_service.DoAtualizar(orcamento));
            if (ValidationResult.IsValid) _uow.DoCommit();
            return ValidationResult;
        }

        public ValidationResult DoDeletar(Orcamento orcamento)
        {
            if (orcamento.Id != 0){
                ValidationResult.Add(_service.DoIsValid(orcamento));
                if (!ValidationResult.IsValid){
                    return ValidationResult;
                }
                IEnumerable<OrcamentoItem> lstOrcamentoItens = _serviceOrcamentoItem.DoObterPor(i => i.OrcamentoId == orcamento.Id);
                IEnumerable<Financeiro> lstOrcamentoFinanceiro = _serviceFinanceiro.DoObterPor(i => i.OrcamentoId == orcamento.Id);
                IEnumerable<OrcamentoOcorrencia> lstOrcamentoOcorrencia = _serviceOrcamentoOcorrencia.DoObterPor(i => i.OrcamentoId == orcamento.Id);
                _uow.DoBeginTransaction();
                ValidationResult.Add(_service.DoDeletar(orcamento));
                ValidationResult.Add(_serviceOrcamentoItem.DoDeletarRange(lstOrcamentoItens));
                ValidationResult.Add(_serviceFinanceiro.DoDeletarRange(lstOrcamentoFinanceiro));
                ValidationResult.Add(_serviceOrcamentoOcorrencia.DoDeletarRange(lstOrcamentoOcorrencia));
                if (ValidationResult.IsValid) _uow.DoCommit();
            }
            return ValidationResult;
        }

        public Orcamento DoObterPorId(int id)
        {
            return _service.DoObterPorId(id);
        }

        public IEnumerable<Orcamento> DoObterTodos()
        {
            return _service.DoObterTodos();
        }

        public IEnumerable<Orcamento> DoObterPor(Expression<Func<Orcamento, bool>> where = null)
        {
            return _service.DoObterPor(where);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public ValidationResult DoDuplicar(Orcamento orcamento = null)
        {
            IEnumerable<OrcamentoItem> lstOrcamentoItens = _serviceOrcamentoItem.DoObterPor(i => i.OrcamentoId == orcamento.Id && i.Classificacao == (int) EClassificacaoProduto.FINAL);
            IEnumerable<OrcamentoItem> lstOrcamentoItensKit = _serviceOrcamentoItem.DoObterPor(i => i.OrcamentoId == orcamento.Id && i.Classificacao != (int) EClassificacaoProduto.FINAL);
            orcamento.Id = 0;
            _uow.DoBeginTransaction();
            ValidationResult.Add(_service.DoAdicionar(orcamento));
            _uow.DoSavePoint();
            ValidationResult.Add(DoAddOcorrenciaPrimeiraMedicao(orcamento));
            foreach (var item in lstOrcamentoItens)
            {
                var rootIdOld = item.Id;
                item.Id = 0;
                item.OrcamentoId = orcamento.Id;
                item.Produto = null;
                ValidationResult.Add(_serviceOrcamentoItem.DoAdicionar(item));
                _uow.DoSavePoint();
                foreach (var itemKit in lstOrcamentoItensKit.Where(i => i.RootId == rootIdOld) )
                {
                    itemKit.Id = 0;
                    itemKit.RootId = item.Id;
                    itemKit.Produto = null;
                    itemKit.OrcamentoId = orcamento.Id;
                }
            }            
            ValidationResult.Add(_serviceOrcamentoItem.DoAdicionarRange(lstOrcamentoItensKit));
            if (ValidationResult.IsValid) _uow.DoCommit();
            return ValidationResult;
        }

        public ValidationResult DoBloquear(Orcamento orcamento = null)
        {
            ValidationResult.Add(_service.DoIsValid(orcamento));
            if (!ValidationResult.IsValid){
                return ValidationResult;
            }
            _uow.DoBeginTransaction();
            ValidationResult.Add(DoAddOcorrenciaBloqueio(orcamento));
            ValidationResult.Add(_service.DoAtualizar(orcamento));
            if (ValidationResult.IsValid) _uow.DoCommit();
            return ValidationResult;
        }
    }
}