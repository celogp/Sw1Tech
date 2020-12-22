using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Interface.Service;
using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Domain.Services.Common;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Domain.Service
{
    public class FormaPagamentoService : Service<FormaPagamento>, IFormaPagamentoService
    {
        public FormaPagamentoService(IFormaPagamentoRepository repo) : base(repo)
        {
        }

        new public ValidationResult DoIsValid(FormaPagamento formaPagamento)
        {
            var fiscal = new Entities.Validation.FormaPagamentoIsValid();
            ValidationResult.Add(fiscal.Valid(formaPagamento));
            return ValidationResult;
        }
    }
}