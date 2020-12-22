using Sw1Tech.Domain.Interfaces.Specification;

namespace Sw1Tech.Domain.Entities.Especification.FinanceiroEspec
{
    public class FinanceiroVlrParcelaNaoPodeSerZeroOuNegativo : ISpecification<Financeiro>
    {
        public bool IsSatisfiedBy(Financeiro financeiro)
        {
            var valido = (financeiro.VlrParcela > 0);
            return valido;
        }
    }
}