using Sw1Tech.Domain.Interfaces.Specification;
using System;

namespace Sw1Tech.Domain.Entities.Especification.LocalizacaoEspec
{
    public class LocalizacaoUfNaoPodeSerBrancoOuNulo : ISpecification<Localizacao>
    {
        public bool IsSatisfiedBy(Localizacao localizacao)
        {
            var valido = (!String.IsNullOrEmpty(localizacao.Uf)
                && !String.IsNullOrWhiteSpace(localizacao.Uf));
            return valido;
        }
    }
}
