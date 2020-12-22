using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Interface.Service;
using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Domain.Services.Common;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Domain.Service
{
    public class OcorrenciaService : Service<Ocorrencia>, IOcorrenciaService
    {
        public OcorrenciaService(IOcorrenciaRepository repo) : base(repo)
        {
        }

        new public ValidationResult DoIsValid(Ocorrencia ocorrencia)
        {
            var fiscal = new Entities.Validation.OcorrenciaIsValid();
            ValidationResult.Add(fiscal.Valid(ocorrencia));
            return ValidationResult;
        }
    }
}