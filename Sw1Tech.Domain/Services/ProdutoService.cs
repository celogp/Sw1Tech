using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Entities.Validation;
using Sw1Tech.Domain.Interface.Service;
using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Domain.Services.Common;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Domain.Service
{
    public class ProdutoService : Service<Produto>, IProdutoService
    {
        private readonly IProdutoRepository _repo;
        public ProdutoService(IProdutoRepository repo) : base(repo)
        {
            _repo = repo;
        }

        new public ValidationResult DoIsValid(Produto produto)
        {
            var fiscal = new ProdutoIsValid();
            ValidationResult.Add(fiscal.Valid(produto));
            return ValidationResult;
        }

        public ValidationResult DoExisteDependencia(Produto produto)
        {
            if (_repo.DoExisteDependencia(produto))
            {
                ValidationResult.Add("O produto não pode ser apagado porque existe ligação com os movimentos.");
            }
            return ValidationResult;
        }
    }
}