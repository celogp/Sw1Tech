using Sw1Tech.Domain.Entities.Especification.ModeloKitEspec;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Domain.Entities.Validation
{
    public class ModeloKitIsValid : Validation<ModeloKit>
    {
        public ModeloKitIsValid()
        {
            base.AddRule(new ValidationRule<ModeloKit>(new ModeloKitQuantidadeNaoPodeSerZero(), "A Quantidade do modelo não pode ser zero."));
        }
    }
}