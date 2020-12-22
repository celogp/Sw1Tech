using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Infra.Repository.EF.Context;
using Sw1Tech.Domain.Entities;
using Sw1Tech.Infra.EF.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace Sw1Tech.Infra.Repository.EF
{
    public class OrcamentoAnexoRepository : Repository<OrcamentoAnexo>, IOrcamentoAnexoRepository
    {
        public OrcamentoAnexoRepository(Sw1TechContext context) : base(context)
        {
        }
        new public IEnumerable<OrcamentoAnexo> DoObterPor(Expression<Func<OrcamentoAnexo, bool>> where = null)
        {
            return _dbSet.AsNoTracking().Where(where).ToList();
        }
    }
}