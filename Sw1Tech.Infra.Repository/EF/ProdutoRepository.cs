using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Infra.Repository.EF.Context;
using Sw1Tech.Domain.Entities;
using Sw1Tech.Infra.EF.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Sw1Tech.Infra.Repository.EF
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(Sw1TechContext context) : base(context)
        {
        }

        public bool DoExisteDependencia(Produto produto)
        {
            var query = "SELECT * FROM TPRODUTO WHERE ID = {0} "
                + "AND (EXISTS(SELECT DISTINCT 1 FROM TORCAMENTOITEM WHERE PRODUTOID = {0}) "
                + "OR   EXISTS(SELECT DISTINCT 1 FROM TMODELOKIT WHERE PRODUTOID = {0}))";
            var result = _dbSet.AsNoTracking().FromSql(query, produto.Id).SingleOrDefault();
            return (result != null);
        }
    }
}