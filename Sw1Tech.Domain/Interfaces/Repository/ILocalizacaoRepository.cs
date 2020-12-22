using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Interfaces.Repository.Common;
using System;
using System.Collections;
using System.Linq.Expressions;

namespace Sw1Tech.Domain.Interfaces.Repository
{
    public interface ILocalizacaoRepository : IRepository<Localizacao>
    {
        IEnumerable DoObterLocalidade(Expression<Func<Localizacao, bool>> where = null);
        IEnumerable DoObterBairro(Expression<Func<Localizacao, bool>> where = null);
        bool DoExisteDependencia(Localizacao localizacao);
    }
}
