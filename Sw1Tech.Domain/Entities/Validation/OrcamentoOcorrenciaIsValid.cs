using Sw1Tech.Domain.Entities.Especification.OrcamentoEspec;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Domain.Entities.Validation
{
    public class OrcamentoOcorrenciaIsValid : Validation<OrcamentoOcorrencia>
    {
        public OrcamentoOcorrenciaIsValid()
        {
            base.AddRule(new ValidationRule<OrcamentoOcorrencia>(new OrcamentoOcorrenciaOrcamentoIdNaoPodeSerZero(), "OrcamentoId da ocorrencia não pode ser zero ou nulo."));
        }
    }
}