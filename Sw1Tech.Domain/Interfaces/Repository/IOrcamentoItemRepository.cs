using System;
using System.Collections;
using System.Linq.Expressions;
using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Interfaces.Repository.Common;

namespace Sw1Tech.Domain.Interfaces.Repository
{
    public interface IOrcamentoItemRepository : IRepository<OrcamentoItem>
    {
        decimal DoSomaVlrBruto(Expression<Func<OrcamentoItem, bool>> where = null);
        decimal DoSomaVlrTotal(Expression<Func<OrcamentoItem, bool>> where = null);
        IEnumerable DoObterSomaAmbientes(Expression<Func<OrcamentoItem, bool>> where = null);
        IEnumerable DoObterSomaBases(Expression<Func<OrcamentoItem, bool>> where = null);
    }
}
