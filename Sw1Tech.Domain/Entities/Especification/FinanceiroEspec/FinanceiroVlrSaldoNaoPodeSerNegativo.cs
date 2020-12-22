using Sw1Tech.Domain.Interfaces.Specification;

namespace Sw1Tech.Domain.Entities.Especification.FinanceiroEspec
{
    public class FinanceiroVlrSaldoNaoPodeSerNegativo : ISpecification<Financeiro>
    {
        public bool IsSatisfiedBy(Financeiro financeiro)
        {
            var valido = (financeiro.VlrSaldo  >= 0);
            return valido;
        }
    }
}