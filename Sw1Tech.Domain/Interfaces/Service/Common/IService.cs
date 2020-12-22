using System.Collections.Generic;
using Sw1Tech.Domain.Validation;
using System;
using System.Linq.Expressions;

namespace Sw1Tech.Domain.Interfaces.Service.Common
{
    public interface IService<TEntity>
        where TEntity : class
    {
        ValidationResult DoAdicionar(TEntity entity);
        ValidationResult DoAtualizar(TEntity entity);
        ValidationResult DoDeletar(TEntity entity);
        ValidationResult DoAdicionarRange( IEnumerable<TEntity> entities );
        ValidationResult DoAtualizarRange( IEnumerable<TEntity> entities );
        ValidationResult DoDeletarRange( IEnumerable<TEntity> entities );
        TEntity DoObterPorId(int id);
        IEnumerable<TEntity> DoObterTodos();
        IEnumerable<TEntity> DoObterPor(Expression<Func<TEntity, bool>> where = null);
        ValidationResult DoIsValid(TEntity entity);
    }
}