using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Interfaces.Repository.Common;

namespace Sw1Tech.Domain.Interfaces.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        bool DoExisteDependencia(Produto produto);
    }
}
