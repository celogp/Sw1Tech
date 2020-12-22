using Sw1Tech.Domain.Interfaces.Specification;
using System;

namespace Sw1Tech.Domain.Entities.Especification.FormaPagamentoEspec
{
    public class FormaPagamentoNomeNaoPodeSerBrancoOuNulo : ISpecification<FormaPagamento>
    {
        public bool IsSatisfiedBy(FormaPagamento formaPagamento)
        {
            var valido = (!String.IsNullOrEmpty(formaPagamento.Nome) 
                && !String.IsNullOrWhiteSpace(formaPagamento.Nome));
            return valido;
        }
    }
}