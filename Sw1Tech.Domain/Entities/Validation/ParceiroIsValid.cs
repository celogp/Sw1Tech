using Sw1Tech.Domain.Entities.Especification.ParceiroEspec;
using Sw1Tech.Domain.Interfaces.Repository;
using Sw1Tech.Domain.Validation;

namespace Sw1Tech.Domain.Entities.Validation
{
    public class ParceiroIsValid : Validation<Parceiro>
    {
        public ParceiroIsValid(IParceiroRepository repo)
        {
            base.AddRule(new ValidationRule<Parceiro>(new ParceiroNomeNaoPodeSerBrancoOuNulo(), "O Nome do parceiro não pode ser nulo ou em branco."));
            base.AddRule(new ValidationRule<Parceiro>(new ParceiroRazaoNaoPodeSerBrancoOuNulo(), "A Razão do parceironão pode ser nulo ou em branco."));
            base.AddRule(new ValidationRule<Parceiro>(new ParceiroSexoNaoPodeMaiorQueTres(), "Sexo precisa ser Masculino, Feminino, Outro."));
            base.AddRule(new ValidationRule<Parceiro>(new ParceiroLocalizacaoIdNaoPodeNegativo(), "A Localização do parceiro não pode ser zero ou negativo."));
            base.AddRule(new ValidationRule<Parceiro>(new ParceiroCpfNaoPodeRepetir(repo), "O CPF já existe para outro parceiro."));
            base.AddRule(new ValidationRule<Parceiro>(new ParceiroCnpjNaoPodeRepetir(repo), "O CNPJ já existe para outro parceiro."));
            base.AddRule(new ValidationRule<Parceiro>(new ParceiroIdentidadeNaoPodeRepetir(repo), "A IDENTIDADE já existe para outro parceiro."));
            base.AddRule(new ValidationRule<Parceiro>(new ParceiroInscricaoNaoPodeRepetir(repo), "A INSCRIÇÃO já existe para outro parceiro."));
        }
    }
}