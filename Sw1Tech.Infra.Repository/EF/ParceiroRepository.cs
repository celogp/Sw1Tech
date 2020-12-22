using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Infra.Repository.EF.Context;
using Sw1Tech.Domain.Entities;
using Sw1Tech.Infra.EF.Common;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace Sw1Tech.Infra.Repository.EF
{
    public class ParceiroRepository : Repository<Parceiro>, IParceiroRepository
    {
        public ParceiroRepository(Sw1TechContext context) : base(context)
        {
        }

        public new IEnumerable<Parceiro> DoObterPor(Expression<Func<Parceiro, bool>> where = null)
        {
            return _dbSet.AsNoTracking().Include(l => l.Localizacao).Where(where).ToList();
        }

        public new IEnumerable<Parceiro> DoObterTodos()
        {
            return _dbSet.AsNoTracking().Include( l => l.Localizacao).ToList();
        }

        public bool DoExisteDependencia(Parceiro parceiro)
        {
            var query = "SELECT * FROM TPARCEIRO WHERE ID = {0} "
                + "AND (EXISTS(SELECT DISTINCT 1 FROM TORCAMENTO WHERE PARCEIROID = {0}) "
                + "OR   EXISTS(SELECT DISTINCT 1 FROM TFINANCEIRO  WHERE PARCEIROID = {0}) )";
            var result = _dbSet.AsNoTracking().FromSql(query, parceiro.Id).SingleOrDefault();
            return (result != null);
        }
    }
}