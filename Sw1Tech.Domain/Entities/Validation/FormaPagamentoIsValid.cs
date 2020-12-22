using Sw1Tech.Domain.Entities.Especification.FormaPagamentoEspec;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Domain.Entities.Validation
{
    public class FormaPagamentoIsValid : Validation<FormaPagamento>
    {
        public FormaPagamentoIsValid()
        {
            base.AddRule(new ValidationRule<FormaPagamento>(new FormaPagamentoNomeNaoPodeSerBrancoOuNulo(), "O Nome para a forma de pagamento não pode ser nulo ou em branco."));
        }
    }
}