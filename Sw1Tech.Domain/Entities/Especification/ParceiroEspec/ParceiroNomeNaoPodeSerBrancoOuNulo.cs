using Sw1Tech.Domain.Interfaces.Specification;
using System;

namespace Sw1Tech.Domain.Entities.Especification.ParceiroEspec
{
    public class ParceiroNomeNaoPodeSerBrancoOuNulo : ISpecification<Parceiro>
    {
        public bool IsSatisfiedBy(Parceiro parceiro)
        {
            var valido = (!String.IsNullOrEmpty(parceiro.Nome) 
                && !String.IsNullOrWhiteSpace(parceiro.Nome));
            return valido;
        }
    }
}
