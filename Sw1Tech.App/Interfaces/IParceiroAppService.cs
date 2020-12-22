using Sw1Tech.App.Interfaces.Common;
using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Validation;
using System.Collections.Generic;

namespace Sw1Tech.App.Interfaces
{
    public interface IParceiroAppService : IAppService<Parceiro>
    {
        ValidationResult DoSalvarLstParceiros(IEnumerable<Parceiro> lstParceiros = null);
    }
}