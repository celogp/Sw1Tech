using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Interface.Service;
using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Domain.Services.Common;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Domain.Service
{
    public class UfService : Service<Uf>, IUfService
    {
        public UfService(IUfRepository repo) : base(repo)
        {
        }

        new public ValidationResult DoIsValid(Uf uf)
        {
            var fiscal = new Entities.Validation.UfIsValid();
            ValidationResult.Add(fiscal.Valid(uf));
            return ValidationResult;
        }
    }
}