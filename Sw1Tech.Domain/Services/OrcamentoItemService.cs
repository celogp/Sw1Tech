using System;
using System.Linq.Expressions;
using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Enums;
using Sw1Tech.Domain.Interface.Service;
using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Domain.Services.Common;
using Sw1Tech.Domain.Entities.Validation;
using Sw1Tech.Domain.Validation;
using System.Collections;
using System.Linq;

namespace Sw1Tech.Domain.Service
{
    public class OrcamentoItemService : Service<OrcamentoItem>, IOrcamentoItemService
    {
        private readonly IOrcamentoItemRepository _repo;
        //private readonly IProdutoService _serviceProduto;
        //private readonly IModeloKitService _serviceModeloKit;
        private readonly IModeloKitRepository _repoModeloKit;
        public OrcamentoItemService(IOrcamentoItemRepository repo, IModeloKitRepository repoModeloKit) : base(repo)
        {
            _repo = repo;
            //_serviceProduto = serviceProduto;
            //_serviceModeloKit = serviceModeloKit;
            _repoModeloKit = repoModeloKit;
        }

        public void DoCalculaVlrLiquido(OrcamentoItem orcamentoItem)
        {
            orcamentoItem.VlrBruto = Math.Round(orcamentoItem.VlrBruto, 4);
            orcamentoItem.VlrTotal = Math.Round(orcamentoItem.VlrBruto, 4);
            if (orcamentoItem.PerDesconto>0)
            {
                orcamentoItem.VlrTotal = Math.Round((orcamentoItem.VlrTotal - (orcamentoItem.VlrTotal * orcamentoItem.PerDesconto )/100),4);
            }
            if (orcamentoItem.VlrDesconto>0)
            {
                orcamentoItem.VlrTotal = Math.Round(orcamentoItem.VlrTotal - orcamentoItem.VlrDesconto,4);
            }
            if ((orcamentoItem.Classificacao != (int)EClassificacaoProduto.FINAL) &&
                (orcamentoItem.IndDescontoProdutoFinal != 1))
            {
                orcamentoItem.VlrTotal = Math.Round(orcamentoItem.VlrTotal * orcamentoItem.IndDescontoProdutoFinal,4);
            }
        }

        public decimal DoSomaVlrTotal(Expression<Func<OrcamentoItem, bool>> where = null)
        {
            return _repo.DoSomaVlrTotal(where);
        }

        public decimal DoSomaVlrBruto(Expression<Func<OrcamentoItem, bool>> where = null)
        {
            return _repo.DoSomaVlrBruto(where);
        }

        new public ValidationResult DoIsValid(OrcamentoItem orcamentoItem)
        {
            var fiscal = new OrcamentoItemIsValid(_repo);
            ValidationResult.Add(fiscal.Valid(orcamentoItem));
            return ValidationResult;
        }

        public IEnumerable DoObterSomaAmbientes(Expression<Func<OrcamentoItem, bool>> where = null)
        {
            return _repo.DoObterSomaAmbientes(where);
        }

        public int DoObterRootId(OrcamentoItem orcamentoItem)
        {
            return orcamentoItem.RootId != 0 ? orcamentoItem.RootId : orcamentoItem.Id;
        }

        //public ValidationResult DoDeletarItensKit(int RootId)
        //{
        //    IEnumerable<OrcamentoItem> orcamentoItemKit = _repo.DoObterPor(i => i.RootId == RootId);
        //    if (!_repo.DoDeletarRange(orcamentoItemKit))
        //    {
        //        ValidationResult.Add("Houve um problema ao deletar os itens do kit, verifique o log por enquanto.");
        //    }
        //    return ValidationResult;
        //}

        public decimal DoObterPrecoDaBase(OrcamentoItem orcamentoItem)
        {
            decimal vlrBase = orcamentoItem.VlrUnitario;
            if (orcamentoItem.Classificacao != (int) EClassificacaoProduto.BASE) {
                int rootId = DoObterRootId(orcamentoItem);
                OrcamentoItem orcamentoItemBase;
                orcamentoItemBase = (OrcamentoItem) _repo.DoObterPor(i => i.OrcamentoId == orcamentoItem.OrcamentoId && i.RootId == rootId && i.Classificacao == (int) EClassificacaoProduto.BASE).SingleOrDefault();
                if (orcamentoItemBase != null){
                    //Produto produto = _serviceProduto.DoObterPorId(orcamentoItem.ProdutoId);
                    //if (produto.UsarPrecoProdutoBase == "S"){
                    //    vlrBase = orcamentoItemBase.VlrUnitario;
                    //}
                    vlrBase = orcamentoItemBase.VlrUnitario;
                }
                else if (orcamentoItem.ModeloId !=0){
                    ModeloKit modeloKit;
                    //modeloKit = (ModeloKit)_serviceModeloKit.DoObterPor(k => k.ModeloId == orcamentoItem.ModeloId && k.Produto.Classificacao == (int)EClassificacaoProduto.BASE).SingleOrDefault();
                    modeloKit = (ModeloKit) _repoModeloKit.DoObterPor(k => k.ModeloId == orcamentoItem.ModeloId && k.Produto.Classificacao == (int) EClassificacaoProduto.BASE).SingleOrDefault();
                    if (modeloKit != null){
                        if (modeloKit.Produto.UsarPrecoProdutoBase == "S"){
                            vlrBase = modeloKit.Produto.Preco;
                        }
                    }                        
                }
            }
            return vlrBase;
        }

        public IEnumerable DoObterSomaBases(Expression<Func<OrcamentoItem, bool>> where = null)
        {
            return _repo.DoObterSomaBases(where);
        }

        public void DoCalculaIndiceDesconto(OrcamentoItem orcamentoItem)
        {
            orcamentoItem.IndDescontoProdutoFinal = 1;
            if (orcamentoItem.Classificacao == (int)EClassificacaoProduto.FINAL)
            {
                orcamentoItem.IndDescontoProdutoFinal = Math.Round(orcamentoItem.VlrTotal / orcamentoItem.VlrBruto,4);
            }
        }

        //public IEnumerable DoObterSomaComponentes(Expression<Func<OrcamentoItem, bool>> where = null)
        //{
        //    var lstOrcamentoResumoItens = new List<OrcamentoItem>();
        //    var ProdutoBaseId = 0;
        //    IEnumerable<OrcamentoItem> lstOrcamentoTodosItens = _repo.DoObterPor(where);
        //    foreach (var item in lstOrcamentoTodosItens)
        //    {
        //        if (item.RootId != 0)
        //        {
        //            ProdutoBaseId = lstOrcamentoTodosItens.Where(i => i.Classificacao == (int)EClassificacaoProduto.BASE && i.RootId == item.RootId).SingleOrDefault().ProdutoId;
        //        }
        //        else
        //        {
        //            ProdutoBaseId = item.ProdutoId;
        //        }

        //        lstOrcamentoResumoItens.Add(
        //            new OrcamentoItem()
        //            {
        //                RootId = ProdutoBaseId,
        //                Classificacao = item.Produto.Classificacao,
        //                Produto = item.Produto,
        //                ProdutoId = item.ProdutoId,
        //                PerDesconto = item.PerDesconto,
        //                VlrDesconto = item.VlrDesconto,
        //                Quantidade = item.Quantidade,
        //                VlrUnitario = item.VlrUnitario,
        //                Area = item.Area,
        //                VlrBruto = item.VlrBruto,
        //                VlrTotal = item.VlrTotal
        //            }
        //        );
        //    }
        //    return lstOrcamentoResumoItens
        //                    .GroupBy(i => new { i.RootId, i.Classificacao, i.ProdutoId, i.Produto.Nome })
        //                    .Select(i => new {
        //                        i.Key.RootId,
        //                        i.Key.Classificacao,
        //                        i.Key.ProdutoId,
        //                        i.Key.Nome,
        //                        PerDesconto = i.Sum(s => s.PerDesconto),
        //                        VlrDesconto = i.Sum(s => s.VlrDesconto),
        //                        Quantidade = i.Sum(s => s.Quantidade),
        //                        VlrUnitario = i.Max(s => s.VlrUnitario),
        //                        Area = i.Sum(s => s.Area),
        //                        VlrBruto = i.Sum(s => s.VlrBruto),
        //                        VlrTotal = i.Sum(s => s.VlrTotal)
        //                    })
        //                    .OrderBy(o => o.RootId).ThenBy(o => o.Classificacao)
        //                    .ToList();
        //}
    }
}