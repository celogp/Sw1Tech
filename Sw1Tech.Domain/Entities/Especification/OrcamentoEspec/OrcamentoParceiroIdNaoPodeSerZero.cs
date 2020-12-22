using Sw1Tech.Domain.Interfaces.Specification;

namespace Sw1Tech.Domain.Entities.Especification.OrcamentoEspec
{
    public class OrcamentoParceiroIdNaoPodeSerZero : ISpecification<Orcamento>
    {
        public bool IsSatisfiedBy(Orcamento orcamento)
        {
            var valido = (orcamento.ParceiroId>0);
            return valido;
        }
    }
}