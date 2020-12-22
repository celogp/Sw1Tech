using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Interface.Service;
using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Domain.Services.Common;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Domain.Service
{
    public class SexoService : Service<Sexo>, ISexoService
    {
        public SexoService(ISexoRepository repo) : base(repo)
        {
        }

        new public ValidationResult DoIsValid(Sexo sexo)
        {
            var fiscal = new Entities.Validation.SexoIsValid();
            ValidationResult.Add(fiscal.Valid(sexo));
            return ValidationResult;
        }
    }
}