using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Interfaces.Service.Common;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Domain.Interface.Service
{
    public interface IProdutoService : IService<Produto>
    {
        ValidationResult DoExisteDependencia(Produto produto);
    }
}
