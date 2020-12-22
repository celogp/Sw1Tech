using Sw1Tech.Domain.Interfaces.Specification;
using System;

namespace Sw1Tech.Domain.Entities.Especification.RegistroExportacaoEspec
{
    public class RegistroExportacaoTabelaNaoPodeSerBrancoOuNula : ISpecification<RegistroExportacao>
    {
        public bool IsSatisfiedBy(RegistroExportacao registroExportacao)
        {
            var valido = (!String.IsNullOrEmpty(registroExportacao.Tabela)
                && !String.IsNullOrWhiteSpace(registroExportacao.Tabela));
            return valido;
        }

    }
}
