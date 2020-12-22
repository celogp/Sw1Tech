using Sw1Tech.Domain.Interfaces.Specification;

namespace Sw1Tech.Domain.Entities.Especification.FinanceiroEspec
{
    public class FinanceiroDtVencimentoNaoPodeSerNulo : ISpecification<Financeiro>
    {
        public bool IsSatisfiedBy(Financeiro financeiro)
        {
            var valido = (financeiro.DtVencimento != null);
            return valido;
        }
    }
}