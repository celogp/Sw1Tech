using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Entities.Validation;
using Sw1Tech.Domain.Interface.Service;
using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Domain.Services.Common;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Domain.Service
{
    public class ModeloService : Service<Modelo>, IModeloService
    {
        private readonly IModeloRepository _repo;
        public ModeloService(IModeloRepository repo) : base(repo)
        {
            _repo = repo;
        }

        new public ValidationResult DoIsValid(Modelo modelo)
        {
            var fiscal = new ModeloIsValid();
            ValidationResult.Add(fiscal.Valid(modelo));
            return ValidationResult;
        }

        public ValidationResult DoExisteDependencia(Modelo modelo)
        {
            if (_repo.DoExisteDependencia(modelo))
            {
                ValidationResult.Add("O Modelo não pode ser apagado porque existe ligação com cadastros.");
            }
            return ValidationResult;
        }
    }
}