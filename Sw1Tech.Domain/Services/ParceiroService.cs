using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Entities.Validation;
using Sw1Tech.Domain.Interface.Service;
using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Domain.Services.Common;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Domain.Service
{
    public class ParceiroService : Service<Parceiro>, IParceiroService
    {
        private readonly IParceiroRepository _repo;
        public ParceiroService(IParceiroRepository repo) : base(repo)
        {
            _repo = repo;
        }

        new public ValidationResult DoIsValid(Parceiro parceiro)
        {
            var fiscal = new ParceiroIsValid(_repo);
            ValidationResult.Add(fiscal.Valid(parceiro));
            return ValidationResult;
        }

        public ValidationResult DoExisteDependencia(Parceiro parceiro)
        {
            if (_repo.DoExisteDependencia(parceiro)){
                ValidationResult.Add("O parceiro não pode ser apagado porque existe ligação com os movimentos.");
            }
            return ValidationResult;
        }
    }
}