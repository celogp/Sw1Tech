using Sw1Tech.Domain.Entities.Especification.ProdutoModeloEspec;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Domain.Entities.Validation
{
    public class ProdutoModeloIsValid : Validation<ProdutoModelo>
    {
        public ProdutoModeloIsValid()
        {
            base.AddRule(new ValidationRule<ProdutoModelo>(new ProdutoModeloProdutoModeloIdNaoPodeSerZero(), "O ID do Modelo não pode ser zero."));
        }
    }
}