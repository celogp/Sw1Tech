using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Sw1Tech.Domain.Interfaces.Repository.Common
{
    public interface IRepository<TEntity> where TEntity : class
    {
        dynamic DoAdicionar(TEntity entity);
        bool DoAtualizar(TEntity entity);
        bool DoDeletar(TEntity entity);
        dynamic DoAdicionarRange( IEnumerable<TEntity> entities );
        bool DoAtualizarRange( IEnumerable<TEntity> entities );
        bool DoDeletarRange( IEnumerable<TEntity> entities );
        TEntity DoObterPorId(int id);
        IEnumerable<TEntity> DoObterTodos();
        IEnumerable<TEntity> DoObterPor(Expression<Func<TEntity, bool>> where = null);
        bool DoExisteNoBanco(Expression<Func<TEntity, bool>> where = null);

    }
}
