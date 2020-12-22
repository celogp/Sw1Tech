using Sw1Tech.App.Interfaces.Common;
using Sw1Tech.Domain.Entities;
using System.Security.Cryptography;

namespace Sw1Tech.App.Interfaces
{
    public interface IUsuarioAppService : IAppService<Usuario>
    {
        Usuario DoLogin(Usuario usuario);
    }
}