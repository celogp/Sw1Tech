using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Interfaces.Service.Common;
using Sw1Tech.Domain.Validation;
using System;
using System.Linq.Expressions;

namespace Sw1Tech.Domain.Interface.Service
{
    public interface IUsuarioService : IService<Usuario>
    {
        Usuario DoLogin(Expression<Func<Usuario, bool>> where = null);
        ValidationResult DoExisteDependencia(Usuario usuario);

    }
}
