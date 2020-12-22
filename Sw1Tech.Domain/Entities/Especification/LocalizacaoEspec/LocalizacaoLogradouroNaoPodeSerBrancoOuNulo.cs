using Sw1Tech.Domain.Interfaces.Specification;
using System;

namespace Sw1Tech.Domain.Entities.Especification.LocalizacaoEspec
{
    public class LocalizacaoLogradouroNaoPodeSerBrancoOuNulo : ISpecification<Localizacao>
    {
        public bool IsSatisfiedBy(Localizacao localizacao)
        {
            var valido = (!String.IsNullOrEmpty(localizacao.Logradouro)
                && !String.IsNullOrWhiteSpace(localizacao.Logradouro));
            return valido;
        }
    }
}
