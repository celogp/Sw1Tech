using Sw1Tech.Domain.Entities.Especification.ProdutoEspec;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Domain.Entities.Validation
{
    public class ProdutoIsValid : Validation<Produto>
    {
        public ProdutoIsValid()
        {
            base.AddRule(new ValidationRule<Produto>(new ProdutoNomeNaoPodeSerBrancoOuNulo(), "O Nome para o produto não pode ser nulo ou em branco."));
            base.AddRule(new ValidationRule<Produto>(new ProdutoUsarPrecoProdutoBaseSoPodeSimNao(), "Usar preço do produto base só pode ser 'S' ou 'N'."));
        }
    }
}