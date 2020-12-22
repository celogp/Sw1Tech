using Sw1Tech.Domain.Interfaces.Specification;
using System;

namespace Sw1Tech.Domain.Entities.Especification.UfEspec
{
    public class UfNomeNaoPodeSerBrancoOuNulo : ISpecification<Uf>
    {
        public bool IsSatisfiedBy(Uf uf)
        {
            var valido = (!String.IsNullOrEmpty(uf.Nome) 
                && !String.IsNullOrWhiteSpace(uf.Nome));
            return valido;
        }
    }
}
