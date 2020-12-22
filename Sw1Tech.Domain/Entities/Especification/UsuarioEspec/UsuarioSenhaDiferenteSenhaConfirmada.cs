using Sw1Tech.Domain.Interfaces.Specification;
using System;

namespace Sw1Tech.Domain.Entities.Especification.UsuarioEspec
{
    public class UsuarioSenhaDiferenteSenhaConfirmada : ISpecification<Usuario>
    {
        public bool IsSatisfiedBy(Usuario usuario)
        {
            var valido = (usuario.Senha != usuario.SenhaConfirmada);
            return valido;
        }
    }
}
