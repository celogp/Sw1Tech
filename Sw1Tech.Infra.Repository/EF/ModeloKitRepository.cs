using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Infra.Repository.EF.Context;
using Sw1Tech.Domain.Entities;
using Sw1Tech.Infra.EF.Common;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace Sw1Tech.Infra.Repository.EF
{
    public class ModeloKitRepository : Repository<ModeloKit>, IModeloKitRepository
    {
        public ModeloKitRepository(Sw1TechContext context) : base(context)
        {
        }

        public new IEnumerable<ModeloKit> DoObterPor(Expression<Func<ModeloKit, bool>> where = null)
        {
            return _dbSet.AsNoTracking().Include(l => l.Produto).Where(where).ToList();
        }

        public new IEnumerable<ModeloKit> DoObterTodos()
        {
            return _dbSet.AsNoTracking().Include( l => l.Produto).ToList();
        }
    }
}