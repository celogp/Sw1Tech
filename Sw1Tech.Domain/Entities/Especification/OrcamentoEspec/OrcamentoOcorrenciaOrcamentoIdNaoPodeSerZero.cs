using Sw1Tech.Domain.Interfaces.Specification;

namespace Sw1Tech.Domain.Entities.Especification.OrcamentoEspec
{
    public class OrcamentoOcorrenciaOrcamentoIdNaoPodeSerZero : ISpecification<OrcamentoOcorrencia>
    {
        public OrcamentoOcorrenciaOrcamentoIdNaoPodeSerZero()
        {
        }

        public bool IsSatisfiedBy(OrcamentoOcorrencia orcamentoOcorrencia)
        {
            var valido = true;
            if (orcamentoOcorrencia.OrcamentoId == 0)
            {
                valido = false;
            }
            return valido;
        }
    }
}