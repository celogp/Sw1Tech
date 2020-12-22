using Sw1Tech.Domain.Interfaces.Specification;
using System;

namespace Sw1Tech.Domain.Entities.Especification.LocalizacaoEspec
{
    public class LocalizacaoCepNaoPodeSerBrancoOuNulo : ISpecification<Localizacao>
    {
        public bool IsSatisfiedBy(Localizacao localizacao)
        {
            var valido = (!String.IsNullOrEmpty(localizacao.Cep) 
                && !String.IsNullOrWhiteSpace(localizacao.Cep));
            return valido;
        }
    }
}
