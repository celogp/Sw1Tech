using Sw1Tech.Domain.Entities.Especification.OrcamentoEspec;
using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Domain.Entities.Validation
{
    public class OrcamentoItemIsValid : Validation<OrcamentoItem>
    {
        public OrcamentoItemIsValid(IOrcamentoItemRepository repo)
        {
            base.AddRule(new ValidationRule<OrcamentoItem>(new OrcamentoItemProdutoIdNaoPodeAlterar(repo), "Produto FINAL não pode ser alterado depois da inclusão."));
            base.AddRule(new ValidationRule<OrcamentoItem>(new OrcamentoItemModeloIdNaoPodeAlterar(repo), "Modelo do produto FINAL não pode ser alterado depois da inclusão."));
            base.AddRule(new ValidationRule<OrcamentoItem>(new OrcamentoItemProdutoFinalIdNaoPodeSerZero(), "Produto FINAL não foi informado."));
            base.AddRule(new ValidationRule<OrcamentoItem>(new OrcamentoItemOrcamentoIdNaoPodeSerZero(), "OrcamentoId no Item não pode ser zero ou nulo."));
        }
    }
}