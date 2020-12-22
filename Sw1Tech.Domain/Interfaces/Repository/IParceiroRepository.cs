﻿using Sw1Tech.Domain.Entities;
using Sw1Tech.Domain.Interfaces.Repository.Common;

namespace Sw1Tech.Domain.Interfaces.Repository
{
    public interface IParceiroRepository : IRepository<Parceiro>
    {
        bool DoExisteDependencia(Parceiro parceiro);
    }
}
