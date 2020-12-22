using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Interfaces.Repository.Common;

namespace Sw1Tech.Domain.Interfaces.Repository
{
    public interface IModeloRepository : IRepository<Modelo>
    {
        bool DoExisteDependencia(Modelo modelo);
    }
}
