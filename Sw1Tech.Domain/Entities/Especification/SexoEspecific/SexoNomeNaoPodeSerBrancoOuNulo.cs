using Sw1Tech.Domain.Interfaces.Specification;
using System;

namespace Sw1Tech.Domain.Entities.Especification.SexoEspec
{
    public class SexoNomeNaoPodeSerBrancoOuNulo : ISpecification<Sexo>
    {
        public bool IsSatisfiedBy(Sexo sexo)
        {
            var valido = (!String.IsNullOrEmpty(sexo.Nome) 
                && !String.IsNullOrWhiteSpace(sexo.Nome));
            return valido;
        }
    }
}