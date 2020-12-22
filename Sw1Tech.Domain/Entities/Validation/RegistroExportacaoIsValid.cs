using Sw1Tech.Domain.Entities.Especification.RegistroExportacaoEspec;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Domain.Entities.Validation
{
    public class RegistroExportacaoIsValid : Validation<RegistroExportacao>
    {
        public RegistroExportacaoIsValid()
        {
            base.AddRule(new ValidationRule<RegistroExportacao>(new RegistroExportacaoTabelaNaoPodeSerBrancoOuNula(), "O Nome para a tabela não pode ser nula ou em branco."));
        }
    }
}