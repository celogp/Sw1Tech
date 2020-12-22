using Sw1Tech.Domain.Interfaces.Specification;
using System;

namespace Sw1Tech.Domain.Entities.Especification.LocalizacaoEspec
{
    public class LocalizacaoBairroNaoPodeSerBrancoOuNulo : ISpecification<Localizacao>
    {
        public bool IsSatisfiedBy(Localizacao localizacao)
        {
            var valido = (!String.IsNullOrEmpty(localizacao.Bairro)
                && !String.IsNullOrWhiteSpace(localizacao.Bairro));
            return valido;
        }
    }
}
