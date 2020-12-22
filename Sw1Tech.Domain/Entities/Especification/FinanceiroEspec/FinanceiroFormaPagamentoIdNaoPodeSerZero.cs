using Sw1Tech.Domain.Interfaces.Specification;

namespace Sw1Tech.Domain.Entities.Especification.FinanceiroEspec
{
    public class FinanceiroFormaPagamentoIdNaoPodeSerZero : ISpecification<Financeiro>
    {
        public bool IsSatisfiedBy(Financeiro financeiro)
        {
            var valido = (financeiro.FormaPagamentoId != 0);
            return valido;
        }
    }
}