using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Infra.Repository.EF.Context;
using Sw1Tech.Domain.Entities;
using Sw1Tech.Infra.EF.Common;
using System;
using System.Linq.Expressions;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace Sw1Tech.Infra.Repository.EF
{
    public class ProdutoModeloRepository : Repository<ProdutoModelo>, IProdutoModeloRepository
    {
        public ProdutoModeloRepository(Sw1TechContext context) : base(context)
        {
        }

        new public IEnumerable<ProdutoModelo> DoObterPor(Expression<Func<ProdutoModelo, bool>> where = null)
        {
            return _dbSet.AsNoTracking().Include(p => p.Modelo).Where(where).ToList();
        }

        new public IEnumerable<ProdutoModelo> DoObterTodos()
        {
            return _dbSet.AsNoTracking().Include(p => p.Modelo).ToList();
        }
    }
}