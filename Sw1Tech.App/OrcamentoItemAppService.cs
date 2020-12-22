using System;
using System.Collections.Generic;
using Sw1Tech.App.Interfaces;
using Sw1Tech.Domain.Interface.Service;
using Sw1Tech.Domain.Validation;
using Sw1Tech.Domain.Entities;
//using Sw1Tech.Infra.Context.Interfaces.Dapper;
using Sw1Tech.Infra.Context.Interfaces.EF;
using System.Linq.Expressions;
using Sw1Tech.Domain.Enums;
using System.Linq;
using System.Collections;

namespace Sw1Tech.App
{
    public class OrcamentoItemAppService : AppService, IOrcamentoItemAppService
    {
        private readonly IOrcamentoItemService _service;
        private readonly IModeloKitService _serviceModeloKit;
        private readonly IOrcamentoService _serviceOrcamento;
        private readonly IUnitOfWork _uow;
        private Orcamento orcamento;
        public OrcamentoItemAppService(IOrcamentoItemService service, IModeloKitService serviceModeloKit, 
                                        IOrcamentoService serviceOrcamento, 
                                        IUnitOfWork uow){
            _service = service;
            _serviceModeloKit = serviceModeloKit;
            _serviceOrcamento = serviceOrcamento;
            _uow = uow;
        }

        private ValidationResult DoOrcamentoIsValid(OrcamentoItem orcamentoItem){
            var orcamentoVal = _serviceOrcamento.DoObterPor(i => i.Id == orcamentoItem.OrcamentoId).SingleOrDefault();
            return _serviceOrcamento.DoIsValid(orcamentoVal) ;
        }

        private OrcamentoItem DoAdicionarItensKit(OrcamentoItem orcamentoItem)
        {
            var lstOrcamentoItemKit = new List<OrcamentoItem>();
            decimal area = 0, quantidade = 0, quantidadeKit = 0, vlrUnitario = 0, vlrBruto = 0, largura = 0, comprimento = 0, vlrBase = 0;
            decimal totBruto = 0, totLiquido = 0;
            IEnumerable<ModeloKit> lstModeloKit = _serviceModeloKit.DoObterPor(k => k.ModeloId == orcamentoItem.ModeloId);
            ModeloKit ModeloKitBase = (ModeloKit) lstModeloKit.Where(k => k.Produto.Classificacao == (int) EClassificacaoProduto.BASE).SingleOrDefault();
            if (ModeloKitBase != null){
                if (ModeloKitBase.Produto.Preco != 0){
                    vlrBase = ModeloKitBase.Produto.Preco;
                }
            }
            //Itens do Kit
            foreach (var item in lstModeloKit)
            {
                quantidade = orcamentoItem.Quantidade;
                if (item.Quantidade != 0){
                    quantidadeKit = item.Quantidade;
                }
                if ((vlrBase != 0) && (item.Produto.UsarPrecoProdutoBase == "S")){
                    vlrUnitario = vlrBase;
                }else{
                    vlrUnitario = item.Produto.Preco;
                }
                if (item.Produto.Classificacao == (int)EClassificacaoProduto.SERVICO){
                    largura = 1;
                    comprimento = 1;
                }else{
                    largura = orcamentoItem.Largura;
                    comprimento = orcamentoItem.Comprimento;
                }
                area = (largura * comprimento);
                vlrBruto = (quantidade * quantidadeKit * area) * vlrUnitario;
                lstOrcamentoItemKit.Add(
                    new OrcamentoItem()
                    {
                        Ambiente = orcamentoItem.Ambiente,
                        Classificacao = item.Produto.Classificacao,
                        ModeloId = orcamentoItem.ModeloId,
                        OrcamentoId = orcamentoItem.OrcamentoId,
                        ProdutoId = item.ProdutoId,
                        RootId = orcamentoItem.Id,
                        PerDesconto = 0,
                        VlrDesconto = 0,
                        IndDescontoProdutoFinal = 1,
                        Quantidade = quantidade,
                        QuantidadeKit = quantidadeKit,
                        VlrCusto = item.Produto.Custo,
                        VlrUnitario = vlrUnitario,
                        Largura = largura,
                        Comprimento = comprimento,
                        Area = area,
                        VlrBruto = vlrBruto,
                        VlrTotal = vlrBruto
                    }
                );
                _service.DoCalculaVlrLiquido(lstOrcamentoItemKit.LastOrDefault());
                totLiquido = totLiquido + lstOrcamentoItemKit.LastOrDefault().VlrTotal;
                totBruto = totBruto + lstOrcamentoItemKit.LastOrDefault().VlrBruto;
            }
            if (totBruto != 0)
            {
                orcamentoItem.VlrBruto = totBruto;
                orcamentoItem.VlrTotal = totLiquido;
                orcamentoItem.VlrUnitario = (totBruto / (orcamentoItem.Area * orcamentoItem.Quantidade * orcamentoItem.QuantidadeKit));
                _service.DoCalculaVlrLiquido(orcamentoItem);
                _service.DoCalculaIndiceDesconto(orcamentoItem);
                if (orcamentoItem.IndDescontoProdutoFinal != 1)
                {
                    foreach (var item in lstOrcamentoItemKit)
                    {
                        item.IndDescontoProdutoFinal = orcamentoItem.IndDescontoProdutoFinal;
                        _service.DoCalculaVlrLiquido(item);
                    }
                }
                ValidationResult.Add(_service.DoAdicionarRange(lstOrcamentoItemKit));
            }
            return orcamentoItem;
        }

        private OrcamentoItem DoAtualizarItensKit(OrcamentoItem orcamentoItem)
        {
            decimal vlrUnitario = 0, vlrBruto = 0, vlrBase = 0, quantidade = 0, quantidadeKit = 0, largura = 0, comprimento = 0, area = 0;
            decimal totBruto=0, totLiquido = 0, indDescontoProdutoFinal = 1;
            int rootId = _service.DoObterRootId(orcamentoItem);
            IEnumerable<OrcamentoItem> lstOrcamentoItensKit = _service.DoObterPor(i => i.RootId == rootId);
            vlrBase = _service.DoObterPrecoDaBase(orcamentoItem);
            foreach (var item in lstOrcamentoItensKit)
            {
                quantidade = orcamentoItem.Quantidade;
                quantidadeKit = item.QuantidadeKit;
                item.VlrCusto = item.Produto.Custo;
                if (item.Id == orcamentoItem.Id)
                {
                    vlrUnitario = orcamentoItem.VlrUnitario;
                    item.VlrDesconto = orcamentoItem.VlrDesconto;
                }
                else
                {
                    if ((vlrBase != 0) && (item.Produto.UsarPrecoProdutoBase == "S"))
                    {
                        vlrUnitario = vlrBase;
                    }
                    else
                    {
                        //vlrUnitario = item.Produto.Preco;
                        vlrUnitario = item.VlrUnitario;
                    }
                }

                if (item.Produto.Classificacao == (int)EClassificacaoProduto.SERVICO) 
                {
                    largura = 1;
                    comprimento = 1;
                }
                else
                {
                    if (orcamentoItem.Classificacao == (int)EClassificacaoProduto.FINAL)
                    {
                        largura = orcamentoItem.Largura;
                        comprimento = orcamentoItem.Comprimento;
                    }
                    else
                    {
                        largura = item.Largura;
                        comprimento = item.Comprimento;
                    }
                }
                area = (largura * comprimento);
                vlrBruto = (quantidade * quantidadeKit * area) * vlrUnitario;
                item.Largura = largura;
                item.Comprimento = comprimento;
                item.Area = area;
                item.Quantidade = quantidade;
                item.QuantidadeKit = quantidadeKit;
                item.VlrUnitario = vlrUnitario;
                item.VlrBruto = vlrBruto;
                item.VlrTotal = vlrBruto;
                item.IndDescontoProdutoFinal = 1;
                item.Ambiente = orcamentoItem.Ambiente;
                item.Produto = null;
                _service.DoCalculaVlrLiquido(item);
                totBruto = totBruto + item.VlrBruto;
                totLiquido = totLiquido + item.VlrTotal;
            }
            if (orcamentoItem.Classificacao == (int)EClassificacaoProduto.FINAL)
            {
                //orcamentoItem.VlrBruto = totBruto;
                //orcamentoItem.VlrTotal = totLiquido;
                //orcamentoItem.VlrUnitario = (totBruto / (orcamentoItem.Area * orcamentoItem.Quantidade * orcamentoItem.QuantidadeKit));
                _service.DoCalculaVlrLiquido(orcamentoItem);
                _service.DoCalculaIndiceDesconto(orcamentoItem);
                indDescontoProdutoFinal = orcamentoItem.IndDescontoProdutoFinal;
            }
            else
            {
                OrcamentoItem orcamentoItemKit = _service.DoObterPorId(rootId);
                orcamentoItemKit.Produto = null;
                //orcamentoItemKit.VlrBruto = totBruto;
                //orcamentoItemKit.VlrTotal = totLiquido;
                //orcamentoItemKit.VlrUnitario = (totBruto / (orcamentoItemKit.Area * orcamentoItemKit.Quantidade * orcamentoItemKit.QuantidadeKit));

                orcamentoItemKit.VlrBruto = totLiquido;
                orcamentoItemKit.VlrTotal = totLiquido;
                orcamentoItemKit.VlrUnitario = (orcamentoItemKit.VlrTotal / (orcamentoItemKit.Area * orcamentoItemKit.Quantidade * orcamentoItemKit.QuantidadeKit));
                _service.DoCalculaVlrLiquido(orcamentoItemKit);
                _service.DoCalculaIndiceDesconto(orcamentoItemKit);
                indDescontoProdutoFinal = orcamentoItemKit.IndDescontoProdutoFinal;
            }

            if (indDescontoProdutoFinal != 1)
            {
                foreach (var item in lstOrcamentoItensKit)
                {
                    item.IndDescontoProdutoFinal = indDescontoProdutoFinal;
                    _service.DoCalculaVlrLiquido(item);
                }
            }
            if (orcamentoItem.Classificacao != (int)EClassificacaoProduto.FINAL)
            {
                var orcamentoItemAtu = lstOrcamentoItensKit.Where(i => i.Id == orcamentoItem.Id).SingleOrDefault();
                if (orcamentoItemAtu != null)
                {
                    orcamentoItem.Largura = orcamentoItemAtu.Largura;
                    orcamentoItem.Comprimento = orcamentoItemAtu.Comprimento;
                    orcamentoItem.Area = orcamentoItemAtu.Area;
                    orcamentoItem.Quantidade = orcamentoItemAtu.Quantidade;
                    orcamentoItem.QuantidadeKit = orcamentoItemAtu.QuantidadeKit;
                    orcamentoItem.IndDescontoProdutoFinal = orcamentoItemAtu.IndDescontoProdutoFinal;
                    orcamentoItem.VlrBruto = orcamentoItemAtu.VlrBruto;
                    orcamentoItem.VlrTotal = orcamentoItemAtu.VlrTotal;
                    orcamentoItem.VlrUnitario = orcamentoItemAtu.VlrUnitario;
                    _service.DoCalculaVlrLiquido(orcamentoItem);
                }
            }
            ValidationResult.Add(_service.DoAtualizarRange(lstOrcamentoItensKit.Where(i => i.Id != orcamentoItem.Id)));
            return orcamentoItem;
        }

        private void DoAtualizarTotalItemKitOld(OrcamentoItem orcamentoItem)
        {
            int rootId = _service.DoObterRootId(orcamentoItem);
            var vlrTotal = _service.DoSomaVlrTotal(i => i.OrcamentoId == orcamentoItem.OrcamentoId && i.RootId == rootId &&
                                                         i.Classificacao != (int)EClassificacaoProduto.FINAL);
            if (orcamentoItem.Classificacao != (int) EClassificacaoProduto.FINAL){
                OrcamentoItem orcamentoItemKit = _service.DoObterPorId(orcamentoItem.RootId);
                orcamentoItemKit.VlrBruto = vlrTotal / orcamentoItemKit.IndDescontoProdutoFinal;
                orcamentoItemKit.VlrTotal = vlrTotal;
                orcamentoItemKit.VlrUnitario = (orcamentoItemKit.VlrBruto / (orcamentoItemKit.Area * orcamentoItemKit.Quantidade * orcamentoItemKit.QuantidadeKit));
                ValidationResult.Add(_service.DoAtualizar(orcamentoItemKit));
            }else{
                orcamentoItem.VlrBruto = vlrTotal / orcamentoItem.IndDescontoProdutoFinal;
                orcamentoItem.VlrTotal = vlrTotal;
                orcamentoItem.VlrUnitario = (orcamentoItem.VlrBruto / (orcamentoItem.Area * orcamentoItem.Quantidade * orcamentoItem.QuantidadeKit));
                ValidationResult.Add(_service.DoAtualizar(orcamentoItem));
            }
        }

        private void DoAtualizarTotalItemKit(OrcamentoItem orcamentoItem)
        {
            int rootId = _service.DoObterRootId(orcamentoItem);
            var vlrTotal = _service.DoSomaVlrTotal(i => i.OrcamentoId == orcamentoItem.OrcamentoId && 
                                                        i.RootId == rootId &&
                                                        i.Classificacao != (int)EClassificacaoProduto.FINAL);
            OrcamentoItem orcamentoItemKit = _service.DoObterPorId(rootId);
            orcamentoItemKit.VlrBruto = vlrTotal / orcamentoItemKit.IndDescontoProdutoFinal;
            orcamentoItemKit.VlrTotal = vlrTotal;
            orcamentoItemKit.VlrUnitario = (orcamentoItemKit.VlrBruto / (orcamentoItemKit.Area * orcamentoItemKit.Quantidade * orcamentoItemKit.QuantidadeKit));
            _service.DoCalculaIndiceDesconto(orcamentoItemKit);
            _service.DoCalculaVlrLiquido(orcamentoItemKit);
            ValidationResult.Add(_service.DoAtualizar(orcamentoItemKit));
        }

        private void DoAtualizaTotalOrcamento(int orcamentoId)
        {
            orcamento = _serviceOrcamento.DoObterPorId(orcamentoId);
            orcamento.TotProdutos = _service.DoSomaVlrTotal(i => i.OrcamentoId == orcamentoId && i.Classificacao == (int) EClassificacaoProduto.FINAL);
            orcamento.TotBases = _service.DoSomaVlrTotal(i => i.OrcamentoId == orcamentoId && i.Classificacao == (int) EClassificacaoProduto.BASE);
            orcamento.TotAcabamentos = _service.DoSomaVlrTotal(i => i.OrcamentoId == orcamentoId && i.Classificacao == (int) EClassificacaoProduto.ACABAMENTO);
            orcamento.TotAcessorios = _service.DoSomaVlrTotal(i => i.OrcamentoId == orcamentoId && i.Classificacao == (int) EClassificacaoProduto.ACESSORIO);
            orcamento.TotServicos = _service.DoSomaVlrTotal(i => i.OrcamentoId == orcamentoId && i.Classificacao == (int) EClassificacaoProduto.SERVICO);
            orcamento.TotOrcamento = orcamento.TotProdutos;
            _serviceOrcamento.DoCalculaVlrLiquido(orcamento);
            ValidationResult.Add(_serviceOrcamento.DoAtualizar(orcamento));
        }

        private ValidationResult DoDeletarItensKit(int RootId)
        {
            IEnumerable<OrcamentoItem> orcamentoItemKit = _service.DoObterPor(i => i.RootId == RootId);
            ValidationResult.Add(_service.DoDeletarRange(orcamentoItemKit));
            return ValidationResult;
        }

        public ValidationResult DoAdicionarOld(OrcamentoItem orcamentoItem)
        {
            ValidationResult.Add(_service.DoIsValid(orcamentoItem));
            if (!ValidationResult.IsValid){
                return ValidationResult;
            }
            ValidationResult.Add(DoOrcamentoIsValid(orcamentoItem));
            if (!ValidationResult.IsValid){
                return ValidationResult;
            }
            if ((orcamentoItem.Classificacao != (int) EClassificacaoProduto.BASE) && 
                (orcamentoItem.Classificacao != (int) EClassificacaoProduto.FINAL)){
                    var vlrBase = _service.DoObterPrecoDaBase(orcamentoItem);
                    if (vlrBase != orcamentoItem.VlrUnitario){
                        orcamentoItem.VlrUnitario = vlrBase;
                        orcamentoItem.VlrBruto = orcamentoItem.VlrUnitario * orcamentoItem.Quantidade * orcamentoItem.QuantidadeKit * orcamentoItem.Area;
                        orcamentoItem.VlrTotal = orcamentoItem.VlrBruto;
                        _service.DoCalculaVlrLiquido(orcamentoItem);
                    }
                }
            _uow.DoBeginTransaction();
            ValidationResult.Add(_service.DoAdicionar(orcamentoItem));
            _uow.DoSavePoint();
            if (orcamentoItem.Classificacao == (int) EClassificacaoProduto.FINAL){
                if (orcamentoItem.ModeloId != 0){
                    DoAdicionarItensKit(orcamentoItem);
                    }
            }else{
                if (orcamentoItem.Classificacao == (int) EClassificacaoProduto.BASE){
                    DoAtualizarItensKit(orcamentoItem);
                }
            }
            _uow.DoSavePoint();
            DoAtualizarTotalItemKit(orcamentoItem);
            _uow.DoSavePoint();
            DoAtualizaTotalOrcamento(orcamentoItem.OrcamentoId);
            if (ValidationResult.IsValid) _uow.DoCommit();
            orcamento = _serviceOrcamento.DoObterPorId(orcamentoItem.OrcamentoId);
            ValidationResult.Result = new
            {
                orcamentoItem.Id,
                orcamento.TotProdutos,
                orcamento.TotBases,
                orcamento.TotAcabamentos,
                orcamento.TotAcessorios,
                orcamento.TotServicos,
                orcamento.TotOrcamento
            };
            return ValidationResult;
        }

        public ValidationResult DoAtualizarOld(OrcamentoItem orcamentoItem)
        {
            ValidationResult.Add(_service.DoIsValid(orcamentoItem));
            if (!ValidationResult.IsValid){
                return ValidationResult;
            }
            ValidationResult.Add(DoOrcamentoIsValid(orcamentoItem));
            if (!ValidationResult.IsValid){
                return ValidationResult;
            }
            if ((orcamentoItem.Classificacao != (int) EClassificacaoProduto.BASE) && 
                (orcamentoItem.Classificacao != (int) EClassificacaoProduto.FINAL)){
                    orcamentoItem.VlrUnitario = _service.DoObterPrecoDaBase(orcamentoItem);
                }
            _uow.DoBeginTransaction();
            ValidationResult.Add(_service.DoAtualizar(orcamentoItem));
            _uow.DoSavePoint();
            if (orcamentoItem.Classificacao == (int) EClassificacaoProduto.FINAL)
            {
                DoAtualizarItensKit(orcamentoItem);
            }
            else
            {
                if (orcamentoItem.Classificacao == (int) EClassificacaoProduto.BASE)
                {
                    DoAtualizarItensKit(orcamentoItem);
                }
            }
            _uow.DoSavePoint();
            DoAtualizarTotalItemKit(orcamentoItem);
            _uow.DoSavePoint();
            DoAtualizaTotalOrcamento(orcamentoItem.OrcamentoId);
            if (ValidationResult.IsValid) _uow.DoCommit();
            ValidationResult.Result = new
            {
                orcamentoItem.Id,
                orcamento.TotProdutos,
                orcamento.TotBases,
                orcamento.TotAcabamentos,
                orcamento.TotAcessorios,
                orcamento.TotServicos,
                orcamento.TotOrcamento
            };
            return ValidationResult;
        }

        public ValidationResult DoAdicionar(OrcamentoItem orcamentoItem)
        {
            ValidationResult.Add(_service.DoIsValid(orcamentoItem));
            if (!ValidationResult.IsValid)
            {
                return ValidationResult;
            }
            ValidationResult.Add(DoOrcamentoIsValid(orcamentoItem));
            if (!ValidationResult.IsValid)
            {
                return ValidationResult;
            }
            _uow.DoBeginTransaction();
            ValidationResult.Add(_service.DoAdicionar(orcamentoItem));
            _uow.DoSavePoint();
            if (orcamentoItem.Classificacao == (int)EClassificacaoProduto.FINAL)
            {
                if (orcamentoItem.ModeloId != 0)
                {
                    DoAdicionarItensKit(orcamentoItem);
                }
            }
            else
            {
                if ((orcamentoItem.Classificacao == (int)EClassificacaoProduto.BASE) ||
                    (orcamentoItem.IndDescontoProdutoFinal != 1))
                {
                    ValidationResult.Add(_service.DoAtualizar(DoAtualizarItensKit(orcamentoItem)));
                }
                else
                {
                    DoAtualizarTotalItemKit(orcamentoItem);
                }
            }
            _uow.DoSavePoint();
            DoAtualizaTotalOrcamento(orcamentoItem.OrcamentoId);
            if (ValidationResult.IsValid) _uow.DoCommit();
            orcamento = _serviceOrcamento.DoObterPorId(orcamentoItem.OrcamentoId);
            ValidationResult.Result = new
            {
                orcamentoItem.Id,
                orcamento.TotProdutos,
                orcamento.TotBases,
                orcamento.TotAcabamentos,
                orcamento.TotAcessorios,
                orcamento.TotServicos,
                orcamento.TotOrcamento
            };
            return ValidationResult;
        }

        public ValidationResult DoAtualizar(OrcamentoItem orcamentoItem)
        {
            ValidationResult.Add(_service.DoIsValid(orcamentoItem));
            if (!ValidationResult.IsValid)
            {
                return ValidationResult;
            }
            ValidationResult.Add(DoOrcamentoIsValid(orcamentoItem));
            if (!ValidationResult.IsValid)
            {
                return ValidationResult;
            }
            _uow.DoBeginTransaction();
            if ((orcamentoItem.Classificacao == (int)EClassificacaoProduto.FINAL) ||
                (orcamentoItem.Classificacao == (int)EClassificacaoProduto.BASE) ||
                (orcamentoItem.IndDescontoProdutoFinal != 1))
            {
                ValidationResult.Add(_service.DoAtualizar(DoAtualizarItensKit(orcamentoItem)));
            }
            else
            {
                ValidationResult.Add(_service.DoAtualizar(orcamentoItem));
                _uow.DoSavePoint();
                DoAtualizarTotalItemKit(orcamentoItem);
            }
            _uow.DoSavePoint();
            DoAtualizaTotalOrcamento(orcamentoItem.OrcamentoId);
            if (ValidationResult.IsValid) _uow.DoCommit();
            ValidationResult.Result = new
            {
                orcamentoItem.Id,
                orcamento.TotProdutos,
                orcamento.TotBases,
                orcamento.TotAcabamentos,
                orcamento.TotAcessorios,
                orcamento.TotServicos,
                orcamento.TotOrcamento
            };
            return ValidationResult;
        }

        public ValidationResult DoDeletar(OrcamentoItem orcamentoItem)
        {
            if (orcamentoItem.Id != 0){
                ValidationResult.Add(DoOrcamentoIsValid(orcamentoItem));
                if (!ValidationResult.IsValid){
                    return ValidationResult;
                }
                _uow.DoBeginTransaction();
                ValidationResult.Add(_service.DoDeletar(orcamentoItem));
                if (orcamentoItem.Classificacao == (int)EClassificacaoProduto.FINAL)
                {
                    //_service.DoDeletarItensKit(orcamentoItem.Id);
                    DoDeletarItensKit(orcamentoItem.Id);
                }
                else
                {
                    _uow.DoSavePoint();
                    if (orcamentoItem.IndDescontoProdutoFinal != 1)
                    {
                        DoAtualizarItensKit(orcamentoItem);
                    }
                    else
                    {
                        DoAtualizarTotalItemKit(orcamentoItem);
                    }
                }
                _uow.DoSavePoint();
                DoAtualizaTotalOrcamento(orcamentoItem.OrcamentoId);
                if (ValidationResult.IsValid) _uow.DoCommit();
                ValidationResult.Result = new
                {
                    orcamentoItem.Id,
                    orcamento.TotProdutos,
                    orcamento.TotBases,
                    orcamento.TotAcabamentos,
                    orcamento.TotAcessorios,
                    orcamento.TotServicos,
                    orcamento.TotOrcamento
                };
            }
            return ValidationResult;
        }

        public OrcamentoItem DoObterPorId(int id)
        {
            return _service.DoObterPorId(id);
        }

        public IEnumerable<OrcamentoItem> DoObterTodos()
        {
            return _service.DoObterTodos();
        }

        public IEnumerable<OrcamentoItem> DoObterPor(Expression<Func<OrcamentoItem, bool>> where = null)
        {
            return _service.DoObterPor(where);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public IEnumerable DoObterSomaAmbientes(Expression<Func<OrcamentoItem, bool>> where = null)
        {
            return _service.DoObterSomaAmbientes(where);
        }

        public ValidationResult DoSalvarAmbientes(IEnumerable<OrcamentoItem> lstOrcamentoItensFinais = null)
        {
            var orcamentoItemVal = lstOrcamentoItensFinais.LastOrDefault();
            var orcamentoId = orcamentoItemVal.OrcamentoId;
            var lstOrcamentoItensAtu = new List<OrcamentoItem>();
            ValidationResult.Add(_service.DoIsValid(orcamentoItemVal));
            if (!ValidationResult.IsValid){
                return ValidationResult;
            }
            ValidationResult.Add(DoOrcamentoIsValid(orcamentoItemVal));
            if (!ValidationResult.IsValid){
                return ValidationResult;
            }
            foreach (var item in lstOrcamentoItensFinais)
            {
                item.Produto = null;
                lstOrcamentoItensAtu.Add(item);
                IEnumerable<OrcamentoItem> lstOrcamentoItensKits = _service.DoObterPor(i => i.RootId == item.Id);
                foreach (var itemKit in lstOrcamentoItensKits)
                {
                    itemKit.Produto = null;
                    itemKit.IndDescontoProdutoFinal = item.IndDescontoProdutoFinal;
                    _service.DoCalculaVlrLiquido(itemKit);
                    lstOrcamentoItensAtu.Add(itemKit);
                }
            }
            _uow.DoBeginTransaction();
            ValidationResult.Add(_service.DoAtualizarRange(lstOrcamentoItensAtu));
            _uow.DoSavePoint();
            DoAtualizaTotalOrcamento(orcamentoId);
            if (ValidationResult.IsValid) _uow.DoCommit();
            ValidationResult.Result = new
            {
                Id = orcamentoId,
                orcamento.TotProdutos,
                orcamento.TotBases,
                orcamento.TotAcabamentos,
                orcamento.TotAcessorios,
                orcamento.TotServicos,
                orcamento.TotOrcamento
            };
            return ValidationResult;
        }

        public ValidationResult DoDuplicarAmbientes(IEnumerable<OrcamentoItem> lstOrcamentoItensFinais = null)
        {
            var orcamentoItemVal = lstOrcamentoItensFinais.LastOrDefault();
            var orcamentoId = orcamentoItemVal.OrcamentoId;
            ValidationResult.Add(_service.DoIsValid(orcamentoItemVal));
            if (!ValidationResult.IsValid){
                return ValidationResult;
            }
            ValidationResult.Add(DoOrcamentoIsValid(orcamentoItemVal));
            if (!ValidationResult.IsValid){
                return ValidationResult;
            }
            _uow.DoBeginTransaction();
            foreach (var item in lstOrcamentoItensFinais){
                IEnumerable<OrcamentoItem> lstOrcamentoItensKits = _service.DoObterPor(i => i.RootId == item.Id);
                item.Id = 0;
                item.Produto = null;
                ValidationResult.Add(_service.DoAdicionar(item));
                _uow.DoSavePoint();
                foreach (var itemKit in lstOrcamentoItensKits){
                    itemKit.Id = 0;
                    itemKit.Produto = null;
                    itemKit.RootId = item.Id;
                }
                ValidationResult.Add(_service.DoAdicionarRange(lstOrcamentoItensKits));
            }
            _uow.DoSavePoint();
            DoAtualizaTotalOrcamento(orcamentoId);
            if (ValidationResult.IsValid) _uow.DoCommit();
            ValidationResult.Result = new
            {
                Id = orcamentoId,
                orcamento.TotProdutos,
                orcamento.TotBases,
                orcamento.TotAcabamentos,
                orcamento.TotAcessorios,
                orcamento.TotServicos,
                orcamento.TotOrcamento
            };
            return ValidationResult;
        }

        public IEnumerable DoObterSomaBases(Expression<Func<OrcamentoItem, bool>> where = null)
        {
            return _service.DoObterSomaBases(where);
        }

        public ValidationResult DoSalvarBase(OrcamentoItem orcamentoItemBase)
        {
            ValidationResult.Add(_service.DoIsValid(orcamentoItemBase));
            if (!ValidationResult.IsValid)
            {
                return ValidationResult;
            }
            IEnumerable<OrcamentoItem> lstOrcamentoItensBases = _service.DoObterPor(i => i.OrcamentoId == orcamentoItemBase.OrcamentoId && 
                                                                                         i.ProdutoId == orcamentoItemBase.ProdutoId && 
                                                                                         i.Classificacao == (int) EClassificacaoProduto.BASE
                                                                                          && i.IndDescontoProdutoFinal == orcamentoItemBase.IndDescontoProdutoFinal);
            foreach (var itemBase in lstOrcamentoItensBases)
            {
                itemBase.Produto = null;
                itemBase.VlrCusto = orcamentoItemBase.VlrCusto;
                itemBase.VlrUnitario = orcamentoItemBase.VlrUnitario;
                itemBase.VlrDesconto = orcamentoItemBase.VlrDesconto;
                itemBase.PerDesconto = orcamentoItemBase.PerDesconto;
                itemBase.VlrBruto = itemBase.VlrUnitario * itemBase.Quantidade * itemBase.QuantidadeKit * itemBase.Area;
                itemBase.VlrTotal = itemBase.VlrBruto;
                _service.DoCalculaVlrLiquido(itemBase);
                ValidationResult.Add(DoAtualizar(itemBase));
            }
            return ValidationResult;
        }

        public IEnumerable DoObterSomaDetalhes(Expression<Func<OrcamentoItem, bool>> where = null)
        {
            var lstOrcamentoResumoItens = new List<OrcamentoItem>();
            var ProdutoBaseId = 0;
            IEnumerable<OrcamentoItem> lstOrcamentoTodosItens = _service.DoObterPor(where);
            foreach (var item in lstOrcamentoTodosItens)
            {
                if (item.RootId != 0)
                {
                    ProdutoBaseId = lstOrcamentoTodosItens.Where(i => i.Classificacao == (int)EClassificacaoProduto.BASE && i.RootId == item.RootId).SingleOrDefault().ProdutoId;
                }
                else
                {
                    ProdutoBaseId = item.ProdutoId;
                }

                lstOrcamentoResumoItens.Add(
                    new OrcamentoItem()
                    {
                        RootId = ProdutoBaseId,
                        Classificacao = item.Produto.Classificacao,
                        Produto = item.Produto,
                        IndDescontoProdutoFinal = item.IndDescontoProdutoFinal,
                        ProdutoId = item.ProdutoId,
                        PerDesconto = item.PerDesconto,
                        VlrDesconto = item.VlrDesconto,
                        Quantidade = item.Quantidade,
                        QuantidadeKit = item.QuantidadeKit,
                        VlrCusto = item.VlrCusto,
                        VlrUnitario = item.VlrUnitario,
                        Area = item.Area,
                        VlrBruto = item.VlrBruto,
                        VlrTotal = item.VlrTotal
                    }
                );
            }
            var result = lstOrcamentoResumoItens
                            .GroupBy(i => new { i.RootId, i.Classificacao, i.ProdutoId, i.IndDescontoProdutoFinal, i.Produto})
                            .Select(i => new {
                                i.Key.RootId,
                                i.Key.Classificacao,
                                i.Key.ProdutoId,
                                i.Key.IndDescontoProdutoFinal,
                                i.Key.Produto,
                                PerDesconto = i.Sum(s => s.PerDesconto),
                                VlrDesconto = i.Sum(s => s.VlrDesconto),
                                Quantidade = i.Sum(s => s.Quantidade),
                                QuantidadeKit = i.Sum(s => s.QuantidadeKit),
                                VlrCusto = i.Max(s => s.VlrCusto),
                                VlrUnitario = i.Max(s => s.VlrUnitario),
                                Area = i.Sum(s => s.Area),
                                VlrBruto = i.Sum(s => s.VlrBruto),
                                VlrTotal = i.Sum(s => s.VlrTotal) 
                            })
                            .OrderBy(o => o.RootId).ThenBy(o => o.Classificacao).ThenBy(o => o.IndDescontoProdutoFinal)
                            .ToList();
            return result;
        }
    }
}