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
    public class OrcamentoOcorrenciaRepository : Repository<OrcamentoOcorrencia>, IOrcamentoOcorrenciaRepository
    {
        public OrcamentoOcorrenciaRepository(Sw1TechContext context) : base(context)
        {
        }
        new public IEnumerable<OrcamentoOcorrencia> DoObterPor(Expression<Func<OrcamentoOcorrencia, bool>> where = null)
        {
            return _dbSet.AsNoTracking().Include(p => p.Ocorrencia).Include(p => p.Usuario).Where(where).ToList();
        }
    }
}