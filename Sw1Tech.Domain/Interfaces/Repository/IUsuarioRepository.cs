using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Interfaces.Repository.Common;
using System;
using System.Linq.Expressions;

namespace Sw1Tech.Domain.Interfaces.Repository
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario DoLogin(Expression<Func<Usuario, bool>> where = null);
        bool DoExisteDependencia(Usuario usuario);
    }
}
