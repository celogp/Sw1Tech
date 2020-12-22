using Sw1Tech.Domain.Interfaces.Specification;

namespace Sw1Tech.Domain.Entities.Especification.OrcamentoEspec
{
    public class OrcamentoItemOrcamentoIdNaoPodeSerZero : ISpecification<OrcamentoItem>
    {
        public bool IsSatisfiedBy(OrcamentoItem orcamentoItem)
        {
            var valido = (orcamentoItem.OrcamentoId>0);
            return valido;
        }
    }
}