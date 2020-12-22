using Sw1Tech.Domain.Interfaces.Specification;
using System;

namespace Sw1Tech.Domain.Entities.Especification.OcorrenciaEspec
{
    public class OcorrenciaNomeNaoPodeSerBrancoOuNulo : ISpecification<Ocorrencia>
    {
        public bool IsSatisfiedBy(Ocorrencia ocorrencia)
        {
            var valido = (!String.IsNullOrEmpty(ocorrencia.Nome)
                && !String.IsNullOrWhiteSpace(ocorrencia.Nome));
            return valido;
        }
    }
}