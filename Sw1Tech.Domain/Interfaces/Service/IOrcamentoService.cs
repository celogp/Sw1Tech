using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Interfaces.Service.Common;

namespace Sw1Tech.Domain.Interface.Service
{
    public interface IOrcamentoService : IService<Orcamento>
    {
        void DoCalculaVlrLiquido(Orcamento orcamento);
    }
}
