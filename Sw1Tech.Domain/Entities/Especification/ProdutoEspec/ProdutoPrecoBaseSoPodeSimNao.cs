using Sw1Tech.Domain.Interfaces.Specification;

namespace Sw1Tech.Domain.Entities.Especification.ProdutoEspec
{
    public class ProdutoUsarPrecoProdutoBaseSoPodeSimNao : ISpecification<Produto>
    {
        public bool IsSatisfiedBy(Produto produto)
        {
            var valido = ( produto.UsarPrecoProdutoBase.Equals("S") || produto.UsarPrecoProdutoBase.Equals("N") );
            return valido;
        }
    }
}