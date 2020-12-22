using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Interfaces.Service.Common;
using Sw1Tech.Domain.Validation;
using System;
using System.Collections;
using System.Linq.Expressions;

namespace Sw1Tech.Domain.Interface.Service
{
    public interface ILocalizacaoService : IService<Localizacao>
    {
        IEnumerable DoObterLocalidade(Expression<Func<Localizacao, bool>> where = null);
        IEnumerable DoObterBairro(Expression<Func<Localizacao, bool>> where = null);
        ValidationResult DoExisteDependencia(Localizacao localizacao);
    }
}
