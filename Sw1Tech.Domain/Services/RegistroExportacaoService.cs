using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Interface.Service;
using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Domain.Services.Common;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Domain.Service
{
    public class RegistroExportacaoService : Service<RegistroExportacao>, IRegistroExportacaoService
    {
        public RegistroExportacaoService(IRegistroExportacaoRepository repo) : base(repo)
        {
        }

        new public ValidationResult DoIsValid(RegistroExportacao registroExportacao)
        {
            var fiscal = new Entities.Validation.RegistroExportacaoIsValid();
            ValidationResult.Add(fiscal.Valid(registroExportacao));
            return ValidationResult;
        }
    }
}