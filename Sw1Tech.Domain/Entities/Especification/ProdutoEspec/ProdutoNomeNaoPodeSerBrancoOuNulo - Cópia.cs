using Sw1Tech.Domain.Interfaces.Specification;
using System;

namespace Sw1Tech.Domain.Entities.Especification.ProdutoEspec
{
    public class ProdutoNomeNaoPodeSerBrancoOuNulo : ISpecification<Produto>
    {
        public bool IsSatisfiedBy(Produto produto)
        {
            var valido = (!String.IsNullOrEmpty(produto.Nome)
                && !String.IsNullOrWhiteSpace(produto.Nome));
            return valido;
        }
    }
}