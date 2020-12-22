using Sw1Tech.Domain.Interfaces.Specification;
using System;

namespace Sw1Tech.Domain.Entities.Especification.ParceiroEspec
{
    public class ParceiroLocalizacaoIdNaoPodeNegativo : ISpecification<Parceiro>
    {
        public bool IsSatisfiedBy(Parceiro parceiro)
        {
            var valido = (parceiro.LocalizacaoId>0);
            return valido;
        }
    }
}
