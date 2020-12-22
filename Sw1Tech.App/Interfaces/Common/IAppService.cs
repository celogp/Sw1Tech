using Sw1Tech.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Sw1Tech.App.Interfaces.Common
{
    public interface IAppService<TEntity> : IDisposable where TEntity : class
    {
        TEntity DoObterPorId(int id);
        IEnumerable<TEntity> DoObterTodos();
        IEnumerable<TEntity> DoObterPor(Expression<Func<TEntity, bool>> where = null);
        ValidationResult DoAdicionar(TEntity entity);
        ValidationResult DoAtualizar(TEntity entity);
        ValidationResult DoDeletar(TEntity entity);
    }
}
