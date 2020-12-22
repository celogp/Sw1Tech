using Sw1Tech.Domain.Entities.Especification.UsuarioEspec;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Domain.Entities.Validation
{
    public class UsuarioIsValid : Validation<Usuario>
    {
        public UsuarioIsValid()
        {
            base.AddRule(new ValidationRule<Usuario>(new UsuarioSenhaNaoPodeSerBrancoOuNulo(), "A senha do usuario não pode ser nula ou em branco."));
            base.AddRule(new ValidationRule<Usuario>(new UsuarioNomeNaoPodeSerBrancoOuNulo(), "O Nome do usuario não pode ser nulo ou em branco."));
            base.AddRule(new ValidationRule<Usuario>(new UsuarioSenhaDiferenteSenhaConfirmada(), "A senha NÃO pode ser igual a NOVA senha."));
        }
    }
}