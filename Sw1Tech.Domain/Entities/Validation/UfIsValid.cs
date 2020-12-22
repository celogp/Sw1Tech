using Sw1Tech.Domain.Entities.Especification.UfEspec;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Domain.Entities.Validation
{
    public class UfIsValid : Validation<Uf>
    {
        public UfIsValid()
        {
            base.AddRule(new ValidationRule<Uf>(new UfSiglaNaoPodeSerBrancoOuNulo(), "A Sigla do estado não pode ser nulo ou em branco."));
            base.AddRule(new ValidationRule<Uf>(new UfNomeNaoPodeSerBrancoOuNulo(), "O Nome do estado não pode ser nulo ou em branco."));
        }
    }
}