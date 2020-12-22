using Sw1Tech.Domain.Interfaces.Specification;
using System;

namespace Sw1Tech.Domain.Entities.Especification.LocalizacaoEspec
{
    public class LocalizacaoLocalidadeNaoPodeSerBrancoOuNulo : ISpecification<Localizacao>
    {
        public bool IsSatisfiedBy(Localizacao localizacao)
        {
            var valido = (!String.IsNullOrEmpty(localizacao.Localidade)
                && !String.IsNullOrWhiteSpace(localizacao.Localidade));
            return valido;
        }
    }
}
