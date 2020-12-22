using Sw1Tech.Domain.Entities.Especification.ModeloEspec;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Domain.Entities.Validation
{
    public class ModeloIsValid : Validation<Modelo>
    {
        public ModeloIsValid()
        {
            base.AddRule(new ValidationRule<Modelo>(new ModeloNomeNaoPodeSerBrancoOuNulo(), "O Nome para o modelo não pode ser nulo ou em branco."));
            base.AddRule(new ValidationRule<Modelo>(new ModeloLarguraNaoPodeSerZero(), "A Largura do modelo não pode ser zero."));
            base.AddRule(new ValidationRule<Modelo>(new ModeloComprimentoNaoPodeSerZero(), "O Comprimento do modelo não pode ser zero."));
        }
    }
}