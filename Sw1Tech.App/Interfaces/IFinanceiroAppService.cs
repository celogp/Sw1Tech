using System.Collections.Generic;
using Sw1Tech.App.Interfaces.Common;
using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.App.Interfaces
{
    public interface IFinanceiroAppService : IAppService<Financeiro>
    {
        ValidationResult DoAdicionarLstFinanceiro(IEnumerable<Financeiro> lstFinanceiro);
        ValidationResult DoSalvarLstFinanceiro(IEnumerable<Financeiro> lstFinanceiro);
        ValidationResult DoApagarLstFinanceiro(IEnumerable<Financeiro> lstFinanceiro);
    }
}