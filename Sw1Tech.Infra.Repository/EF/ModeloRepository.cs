using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Infra.Repository.EF.Context;
using Sw1Tech.Domain.Entities;
using Sw1Tech.Infra.EF.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Sw1Tech.Infra.Repository.EF
{
    public class ModeloRepository : Repository<Modelo>, IModeloRepository
    {
        public ModeloRepository(Sw1TechContext context) : base(context)
        {
        }

        public bool DoExisteDependencia(Modelo modelo)
        {
            var query = "SELECT * FROM TMODELO WHERE ID = {0} "
                + "AND EXISTS(SELECT DISTINCT 1 FROM TPRODUTOMODELO WHERE MODELOID = {0})";
            var result = _dbSet.AsNoTracking().FromSql(query, modelo.Id).SingleOrDefault();
            return (result != null);
        }
    }
}