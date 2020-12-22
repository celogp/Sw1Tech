using Sw1Tech.App.Interfaces.Common;
using Sw1Tech.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Sw1Tech.App.Interfaces
{
    public interface ILocalizacaoAppService : IAppService<Localizacao>
    {
        IEnumerable DoObterLocalidade(Expression<Func<Localizacao, bool>> where = null);
        IEnumerable DoObterBairro(Expression<Func<Localizacao, bool>> where = null);
        IEnumerable<Localizacao> DoBuscarPorCEP(string strViaCep);

    }
}