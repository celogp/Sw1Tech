using Sw1Tech.Domain.Interfaces.Specification;
using System;

namespace Sw1Tech.Domain.Entities.Especification.ModeloEspec
{
    public class ModeloNomeNaoPodeSerBrancoOuNulo : ISpecification<Modelo>
    {
        public bool IsSatisfiedBy(Modelo modelo)
        {
            var valido = (!String.IsNullOrEmpty(modelo.Nome)
                && !String.IsNullOrWhiteSpace(modelo.Nome));
            return valido;
        }
    }
}
