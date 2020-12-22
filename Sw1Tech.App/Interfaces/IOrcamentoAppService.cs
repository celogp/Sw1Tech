using Sw1Tech.App.Interfaces.Common;
using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.App.Interfaces
{
    public interface IOrcamentoAppService : IAppService<Orcamento>
    {
        ValidationResult DoDuplicar(Orcamento orcamento = null);
        ValidationResult DoBloquear(Orcamento orcamento = null);
    }
}