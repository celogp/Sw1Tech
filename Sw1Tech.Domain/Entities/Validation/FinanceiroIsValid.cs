using Sw1Tech.Domain.Entities.Especification.FinanceiroEspec;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Domain.Entities.Validation
{
    public class FinanceiroIsValid : Validation<Financeiro>
    {
        public FinanceiroIsValid()
        {
            base.AddRule(new ValidationRule<Financeiro>(new FinanceiroVlrParcelaNaoPodeSerZeroOuNegativo(), "O Valor da parcela NÃO pode ser zero ou negativo."));
            base.AddRule(new ValidationRule<Financeiro>(new FinanceiroVlrSaldoNaoPodeSerNegativo(), "O Valor do Saldo da parcela NÃO pode ser negativo."));
            base.AddRule(new ValidationRule<Financeiro>(new FinanceiroParceiroIdNaoPodeSerZero(), "O Parceiro NÃO pode ser zero."));
            base.AddRule(new ValidationRule<Financeiro>(new FinanceiroDtMovimentoNaoPodeSerNulo(), "A data de movimento NÃO pode ser nula."));
            base.AddRule(new ValidationRule<Financeiro>(new FinanceiroDtVencimentoNaoPodeSerNulo(), "A data de vencimento NÃO pode ser nula."));
            base.AddRule(new ValidationRule<Financeiro>(new FinanceiroFormaPagamentoIdNaoPodeSerZero(), "A forma de pagamento NÃO pode ser zero."));
        }
    }
}