using Sw1Tech.Domain.Interfaces.Specification;
using System;

namespace Sw1Tech.Domain.Entities.Especification.UsuarioEspec
{
    public class UsuarioNomeNaoPodeSerBrancoOuNulo : ISpecification<Usuario>
    {
        public bool IsSatisfiedBy(Usuario usuario)
        {
            var valido = (!String.IsNullOrEmpty(usuario.Nome) 
                && !String.IsNullOrWhiteSpace(usuario.Nome));
            return valido;
        }
    }
}
