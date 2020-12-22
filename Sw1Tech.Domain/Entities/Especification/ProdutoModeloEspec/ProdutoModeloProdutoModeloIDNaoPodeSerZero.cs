using Sw1Tech.Domain.Interfaces.Specification;

namespace Sw1Tech.Domain.Entities.Especification.ProdutoModeloEspec
{
    public class ProdutoModeloProdutoModeloIdNaoPodeSerZero : ISpecification<ProdutoModelo>
    {
        public bool IsSatisfiedBy(ProdutoModelo produtomodelo)
        {
            var valido = (produtomodelo.ModeloId>0);
            return valido;
        }
    }
}
