using Sw1Tech.Domain.Interfaces.Specification;

namespace Sw1Tech.Domain.Entities.Especification.FinanceiroEspec
{
    public class FinanceiroParceiroIdNaoPodeSerZero : ISpecification<Financeiro>
    {
        public bool IsSatisfiedBy(Financeiro financeiro)
        {
            var valido = (financeiro.ParceiroId != 0);
            return valido;
        }
    }
}