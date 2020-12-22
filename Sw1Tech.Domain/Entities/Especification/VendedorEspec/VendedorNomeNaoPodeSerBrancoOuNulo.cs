using Sw1Tech.Domain.Interfaces.Specification;
using System;

namespace Sw1Tech.Domain.Entities.Especification.VendedorEspec
{
    public class VendedorNomeNaoPodeSerBrancoOuNulo : ISpecification<Vendedor>
    {
        public bool IsSatisfiedBy(Vendedor vendedor)
        {
            var valido = (!String.IsNullOrEmpty(vendedor.Nome) 
                && !String.IsNullOrWhiteSpace(vendedor.Nome));
            return valido;
        }
    }
}