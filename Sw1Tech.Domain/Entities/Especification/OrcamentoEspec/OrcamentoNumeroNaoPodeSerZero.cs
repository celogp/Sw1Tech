using Sw1Tech.Domain.Interfaces.Specification;

namespace Sw1Tech.Domain.Entities.Especification.OrcamentoEspec
{
    public class OrcamentoNumeroNaoPodeSerZero : ISpecification<Orcamento>
    {
        public OrcamentoNumeroNaoPodeSerZero()
        {
        }

        public bool IsSatisfiedBy(Orcamento orcamento)
        {
            var valido = true;
            if (orcamento.Numero == 0)
            {
                valido = false;
            }
            return valido;
        }
    }
}