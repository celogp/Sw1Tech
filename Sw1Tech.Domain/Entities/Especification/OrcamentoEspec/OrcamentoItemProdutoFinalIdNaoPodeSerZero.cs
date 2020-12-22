using Sw1Tech.Domain.Interfaces.Specification;

namespace Sw1Tech.Domain.Entities.Especification.OrcamentoEspec
{
    public class OrcamentoItemProdutoFinalIdNaoPodeSerZero : ISpecification<OrcamentoItem>
    {
        public bool IsSatisfiedBy(OrcamentoItem orcamentoItem)
        {
            var valido = (orcamentoItem.ProdutoId>0);
            return valido;
        }
    }
}