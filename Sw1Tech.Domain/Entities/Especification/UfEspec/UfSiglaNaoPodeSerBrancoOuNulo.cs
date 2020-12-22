using Sw1Tech.Domain.Interfaces.Specification;
using System;

namespace Sw1Tech.Domain.Entities.Especification.UfEspec
{
    public class UfSiglaNaoPodeSerBrancoOuNulo : ISpecification<Uf>
    {
        public bool IsSatisfiedBy(Uf uf)
        {
            var valido = (!String.IsNullOrEmpty(uf.Sigla) 
                && !String.IsNullOrWhiteSpace(uf.Sigla));
            return valido;
        }
    }
}
