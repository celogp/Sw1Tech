using Sw1Tech.Domain.Entities.Especification.OrcamentoEspec;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Domain.Entities.Validation
{
    public class OrcamentoAnexoIsValid : Validation<OrcamentoAnexo>
    {
        public OrcamentoAnexoIsValid()
        {
            base.AddRule(new ValidationRule<OrcamentoAnexo>(new OrcamentoAnexoOrcamentoIdNaoPodeSerZero(), "OrcamentoId do anexo não pode ser zero ou nulo."));
        }
    }
}