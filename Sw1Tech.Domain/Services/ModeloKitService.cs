using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Entities.Validation;
using Sw1Tech.Domain.Interface.Service;
using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Domain.Services.Common;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Domain.Service
{
    public class ModeloKitService : Service<ModeloKit>, IModeloKitService
    {
        public ModeloKitService(IModeloKitRepository repo) : base(repo)
        {
        }

        new public ValidationResult DoIsValid(ModeloKit modeloKit)
        {
            var fiscal = new ModeloKitIsValid();
            ValidationResult.Add(fiscal.Valid(modeloKit));
            return ValidationResult;
        }
    }
}