﻿using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Infra.Repository.EF.Context;
using Sw1Tech.Domain.Entities;
using Sw1Tech.Infra.EF.Common;

namespace Sw1Tech.Infra.Repository.EF
{
    public class SexoRepository : Repository<Sexo>, ISexoRepository
    {
        public SexoRepository(Sw1TechContext context) : base(context)
        {
        }
    }
}