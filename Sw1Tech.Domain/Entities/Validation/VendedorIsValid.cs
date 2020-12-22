using Sw1Tech.Domain.Entities.Especification.VendedorEspec;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Domain.Entities.Validation
{
    public class VendedorIsValid : Validation<Vendedor>
    {
        public VendedorIsValid()
        {
            base.AddRule(new ValidationRule<Vendedor>(new VendedorNomeNaoPodeSerBrancoOuNulo(), "O Nome para o vendedor não pode ser nulo ou em branco."));
            base.AddRule(new ValidationRule<Vendedor>(new VendedorApelidoNaoPodeSerBrancoOuNulo(), "O Apelido para o vendedor não pode ser nulo ou em branco."));
        }
    }
}