using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Infra.Repository.EF.Context;
using Sw1Tech.Domain.Entities;
using Sw1Tech.Infra.EF.Common;
using System;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace Sw1Tech.Infra.Repository.EF
{
    public class LocalizacaoRepository : Repository<Localizacao>, ILocalizacaoRepository
    {
        public LocalizacaoRepository(Sw1TechContext context) : base(context)
        {
        }

        public IEnumerable DoObterLocalidade(Expression<Func<Localizacao, bool>> where = null)
        {
            return _dbSet.Where(where).Select(l => new { l.Localidade, l.Uf }).Distinct();
        }

        public IEnumerable DoObterBairro(Expression<Func<Localizacao, bool>> where = null)
        {
            return _dbSet.Where(where).Select(l => new { l.Bairro }).Distinct();
        }

        public bool DoExisteDependencia(Localizacao localizacao)
        {
            var query = "SELECT * FROM TLOCALIZACAO WHERE ID = {0} "
                + "AND EXISTS(SELECT DISTINCT 1 FROM TPARCEIRO WHERE LOCALIZACAOID = {0})";
            var result = _dbSet.AsNoTracking().FromSql(query, localizacao.Id).SingleOrDefault();
            return (result != null);
        }
    }
}