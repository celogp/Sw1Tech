using Microsoft.EntityFrameworkCore;
using Sw1Tech.Domain.Interfaces.Repository.Common;
using Sw1Tech.Infra.Repository.EF.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Sw1Tech.Infra.EF.Common
{
    public class Repository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : class
    {
        private readonly Sw1TechContext _conte;
        protected DbSet<TEntity> _dbSet;
        public Repository(Sw1TechContext context)
        {
            _conte = context;
            _dbSet = _conte.Set<TEntity>();
        }
        public dynamic DoAdicionar(TEntity entity)
        {
            var _inserted = true;
            _dbSet.Add(entity);
            return _inserted;
        }
        public bool DoAtualizar(TEntity entity)
        {
            var _updated = true;
            _dbSet.Update(entity);
            return _updated;
        }
        public bool DoDeletar(TEntity entity)
        {
            var _removed = true;
            _dbSet.Remove(entity);
            return _removed;
        }
        public IEnumerable<TEntity> DoObterPor(Expression<Func<TEntity, bool>> where = null)
        {
            IEnumerable<TEntity> _result = null;
            if (where == null)
            {
                _result = DoObterTodos();
            }else
            {
                _result = _dbSet.AsNoTracking().Where(where).AsQueryable();
            }
            return _result;
        }
        public TEntity DoObterPorId(int id)
        {
            return _dbSet.Find(id);
        }
        public IEnumerable<TEntity> DoObterTodos()
        {
            return _dbSet.AsNoTracking().ToList();
        }
        public void Dispose()
        {
            _conte.Dispose();
            GC.SuppressFinalize(this);
        }
        public bool DoDeletarRange( IEnumerable<TEntity> entities )
        {
            var _removed = true;
            _dbSet.RemoveRange(entities);
            return _removed;
        }
        public bool DoAtualizarRange( IEnumerable<TEntity> entities )
        {
            var _updated = true;
            _dbSet.UpdateRange(entities);
            return _updated;
        }
        
        public dynamic DoAdicionarRange( IEnumerable<TEntity> entities )
        {
            var _inserted = true;
            _dbSet.AddRange(entities);
            return _inserted;
        }

        public bool DoExisteNoBanco(Expression<Func<TEntity, bool>> where = null)
        {
            return (_dbSet.AsNoTracking().Where(where).Count() == 0);
        }
    }
}