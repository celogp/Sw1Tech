using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Infra.Repository.EF.Context;
using Sw1Tech.Domain.Entities;
using Sw1Tech.Infra.EF.Common;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections;

namespace Sw1Tech.Infra.Repository.EF
{
    public class OrcamentoItemRepository : Repository<OrcamentoItem>, IOrcamentoItemRepository
    {
        public OrcamentoItemRepository(Sw1TechContext context) : base(context)
        {
        }

        public decimal DoSomaVlrBruto(Expression<Func<OrcamentoItem, bool>> where = null)
        {
            var SomaVlrBruto = _dbSet.AsNoTracking().Where(where).Sum(i => i.VlrBruto);
            return SomaVlrBruto;
        }

        public decimal DoSomaVlrTotal(Expression<Func<OrcamentoItem, bool>> where = null)
        {
            var SomaVlrTotal =  _dbSet.AsNoTracking().Where(where).Sum(i => i.VlrTotal);
            return SomaVlrTotal;
        }

        new public IEnumerable<OrcamentoItem> DoObterPor(Expression<Func<OrcamentoItem, bool>> where = null)
        {
            return _dbSet.AsNoTracking().Include(p => p.Produto).Where(where).ToList();
        }

        new public IEnumerable<OrcamentoItem> DoObterTodos()
        {
            return _dbSet.AsNoTracking().Include(p => p.Produto).ToList();
        }

        public IEnumerable DoObterSomaAmbientes(Expression<Func<OrcamentoItem, bool>> where = null)
        {
            return _dbSet.AsNoTracking().Where(where)
            .GroupBy(i => new {
                i.Ambiente
            })
            .Select(i => new {
                i.Key.Ambiente,
                VlrBruto = i.Sum(s => s.VlrBruto),
                VlrTotal = i.Sum(s => s.VlrTotal),
                VlrDesconto = i.Sum(s => s.VlrDesconto)
            })
            .AsNoTracking()
            .ToList();
        }

        public IEnumerable DoObterSomaBases(Expression<Func<OrcamentoItem, bool>> where = null)
        {
            return _dbSet.AsNoTracking().Where(where)
            .GroupBy(i => new {
                i.ProdutoId,
                i.Produto.Nome,
                i.Produto.Volume,
                i.IndDescontoProdutoFinal,
                i.VlrUnitario
            })
            .Select(i => new {
                i.Key.ProdutoId,
                i.Key.Nome,
                i.Key.Volume,
                i.Key.IndDescontoProdutoFinal,
                i.Key.VlrUnitario,
                CountReg = i.Count(),
                Area = i.Sum(s => s.Quantidade * s.Area),
                VlrBruto = i.Sum(s => s.VlrBruto),
                PerDesconto = i.Sum(s => s.PerDesconto),
                VlrDesconto = i.Sum(s => s.VlrDesconto),
                VlrTotal = i.Sum(s => s.VlrTotal)
            })
            .AsNoTracking()
            .ToList();
        }
    }
}