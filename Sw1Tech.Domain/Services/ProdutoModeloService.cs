using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Interface.Service;
using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Domain.Services.Common;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Domain.Service
{
    public class ProdutoModeloService : Service<ProdutoModelo>, IProdutoModeloService
    {
        public ProdutoModeloService(IProdutoModeloRepository repo) : base(repo)
        {
        }

        new public ValidationResult DoIsValid(ProdutoModelo produtoModelo)
        {
            var fiscal = new Entities.Validation.ProdutoModeloIsValid();
            ValidationResult.Add(fiscal.Valid(produtoModelo));
            return ValidationResult;
        }
    }
}