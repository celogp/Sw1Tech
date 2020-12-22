using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Infra.Repository.EF.Context;
using Sw1Tech.Domain.Entities;
using Sw1Tech.Infra.EF.Common;
using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Sw1Tech.Infra.Repository.EF
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(Sw1TechContext context) : base(context)
        {
        }

        public Usuario DoLogin(Expression<Func<Usuario, bool>> where = null)
        {
            return DoObterPor(where).FirstOrDefault<Usuario>();
        }

        public bool DoExisteDependencia(Usuario usuario)
        {
            var query = "SELECT * FROM TUSUARIO WHERE ID = {0} "
                + "AND EXISTS(SELECT DISTINCT 1 FROM TORCAMENTO WHERE USUARIOID = {0}) ";
            var result = _dbSet.AsNoTracking().FromSql(query, usuario.Id).SingleOrDefault();
            return (result != null);
        }
    }
}