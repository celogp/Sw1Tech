using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Interface.Service;
using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Domain.Services.Common;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Domain.Service
{
    public class FinanceiroService : Service<Financeiro>, IFinanceiroService
    {
        public FinanceiroService(IFinanceiroRepository repo) : base(repo)
        {
        }

        new public ValidationResult DoIsValid(Financeiro financeiro)
        {
            var fiscal = new Entities.Validation.FinanceiroIsValid();
            ValidationResult.Add(fiscal.Valid(financeiro));
            return ValidationResult;
        }

    }
}