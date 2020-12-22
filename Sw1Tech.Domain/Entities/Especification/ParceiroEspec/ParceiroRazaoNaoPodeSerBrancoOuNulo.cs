using Sw1Tech.Domain.Interfaces.Specification;
using System;

namespace Sw1Tech.Domain.Entities.Especification.ParceiroEspec
{
    public class ParceiroRazaoNaoPodeSerBrancoOuNulo : ISpecification<Parceiro>
    {
        public bool IsSatisfiedBy(Parceiro parceiro)
        {
            var valido = (!String.IsNullOrEmpty(parceiro.Razao) 
                && !String.IsNullOrWhiteSpace(parceiro.Razao));
            return valido;
        }
    }
}
