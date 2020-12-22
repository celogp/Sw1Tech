using Sw1Tech.Domain.Entities.Especification.OcorrenciaEspec;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Domain.Entities.Validation
{
    public class OcorrenciaIsValid : Validation<Ocorrencia>
    {
        public OcorrenciaIsValid()
        {
            base.AddRule(new ValidationRule<Ocorrencia>(new OcorrenciaNomeNaoPodeSerBrancoOuNulo(), "O Nome para ocorrencia não pode ser nulo ou em branco."));
        }
    }
}