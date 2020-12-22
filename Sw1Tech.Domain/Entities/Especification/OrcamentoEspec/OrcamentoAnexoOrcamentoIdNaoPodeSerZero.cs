using Sw1Tech.Domain.Interfaces.Specification;

namespace Sw1Tech.Domain.Entities.Especification.OrcamentoEspec
{
    public class OrcamentoAnexoOrcamentoIdNaoPodeSerZero : ISpecification<OrcamentoAnexo>
    {
        public OrcamentoAnexoOrcamentoIdNaoPodeSerZero()
        {
        }

        public bool IsSatisfiedBy(OrcamentoAnexo orcamentoAnexo)
        {
            var valido = true;
            if (orcamentoAnexo.OrcamentoId == 0)
            {
                valido = false;
            }
            return valido;
        }
    }
}