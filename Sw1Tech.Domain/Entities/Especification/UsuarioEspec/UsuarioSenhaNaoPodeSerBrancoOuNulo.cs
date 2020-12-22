using Sw1Tech.Domain.Interfaces.Specification;
using System;

namespace Sw1Tech.Domain.Entities.Especification.UsuarioEspec
{
    public class UsuarioSenhaNaoPodeSerBrancoOuNulo : ISpecification<Usuario>
    {
        public bool IsSatisfiedBy(Usuario usuario)
        {
            var valido = (!String.IsNullOrEmpty(usuario.Senha) 
                && !String.IsNullOrWhiteSpace(usuario.Senha));
            return valido;
        }
    }
}
