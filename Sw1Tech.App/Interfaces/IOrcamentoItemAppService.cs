using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Sw1Tech.App.Interfaces.Common;
using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.App.Interfaces
{
    public interface IOrcamentoItemAppService : IAppService<OrcamentoItem>
    {
        IEnumerable DoObterSomaAmbientes(Expression<Func<OrcamentoItem, bool>> where = null);
        ValidationResult DoSalvarAmbientes(IEnumerable<OrcamentoItem> lstOrcamentoItensFinais = null);
        ValidationResult DoDuplicarAmbientes(IEnumerable<OrcamentoItem> lstOrcamentoItensFinais = null);
        IEnumerable DoObterSomaBases(Expression<Func<OrcamentoItem, bool>> where = null);
        ValidationResult DoSalvarBase(OrcamentoItem orcamentoItemBase);
        IEnumerable DoObterSomaDetalhes(Expression<Func<OrcamentoItem, bool>> where = null);
    }
}