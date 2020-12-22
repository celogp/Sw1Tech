using Sw1Tech.Domain.Interfaces.Specification;
using System;

namespace Sw1Tech.Domain.Entities.Especification.VendedorEspec
{
    public class VendedorApelidoNaoPodeSerBrancoOuNulo : ISpecification<Vendedor>
    {
        public bool IsSatisfiedBy(Vendedor vendedor)
        {
            var valido = (!String.IsNullOrEmpty(vendedor.Apelido)
                && !String.IsNullOrWhiteSpace(vendedor.Apelido));
            return valido;
        }
    }
}