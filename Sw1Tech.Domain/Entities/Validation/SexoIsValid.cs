using Sw1Tech.Domain.Entities.Especification.SexoEspec;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Domain.Entities.Validation
{
    public class SexoIsValid : Validation<Sexo>
    {
        public SexoIsValid()
        {
            base.AddRule(new ValidationRule<Sexo>(new SexoNomeNaoPodeSerBrancoOuNulo(), "O Nome para o sexo não pode ser nulo ou em branco."));
        }
    }
}