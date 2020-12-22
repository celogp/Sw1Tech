using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Infra.Repository.EF.Context;
using Sw1Tech.Domain.Entities;
using Sw1Tech.Infra.EF.Common;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Sw1Tech.Infra.Repository.EF
{
    public class OrcamentoRepository : Repository<Orcamento>, IOrcamentoRepository
    {
        public OrcamentoRepository(Sw1TechContext context) : base(context)
        {
        }
        new public IEnumerable<Orcamento> DoObterPor(Expression<Func<Orcamento, bool>> where = null)
        {
            return _dbSet.AsNoTracking().Include(p => p.Parceiro).Include(p => p.Usuario).Where(where).ToList();
        }
        new public IEnumerable<Orcamento> DoObterTodos()
        {
            return _dbSet.AsNoTracking().Include(p => p.Parceiro).Include(p => p.Usuario).ToList();
        }
    }
}