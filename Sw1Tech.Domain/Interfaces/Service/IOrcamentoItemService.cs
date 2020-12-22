using System;
using System.Collections;
using System.Linq.Expressions;
using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Interfaces.Service.Common;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Domain.Interface.Service
{
    public interface IOrcamentoItemService : IService<OrcamentoItem>
    {
        decimal DoSomaVlrBruto(Expression<Func<OrcamentoItem, bool>> where = null);
        decimal DoSomaVlrTotal(Expression<Func<OrcamentoItem, bool>> where = null);
        void DoCalculaIndiceDesconto(OrcamentoItem orcamentoItem);
        void DoCalculaVlrLiquido(OrcamentoItem orcamentoItem);
        IEnumerable DoObterSomaAmbientes(Expression<Func<OrcamentoItem, bool>> where = null);
        int DoObterRootId(OrcamentoItem orcamentoItem);
        //ValidationResult DoDeletarItensKit(int RootId);
        decimal DoObterPrecoDaBase(OrcamentoItem orcamentoItem);
        IEnumerable DoObterSomaBases(Expression<Func<OrcamentoItem, bool>> where = null);
        //IEnumerable DoObterSomaComponentes(Expression<Func<OrcamentoItem, bool>> where = null);
    }
}
